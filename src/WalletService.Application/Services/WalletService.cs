using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WalletService.Application.Abstractions;
using WalletService.Application.Dtos;
using WalletService.Domain.Entities;
using WalletService.Domain.Enums;
using WalletService.Domain.Exceptions;




namespace WalletService.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IApplicationDbContext _context;

        public WalletService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientDto>> GetClientsAsync(CancellationToken ct = default)
        {
            return await _context.Clients
                .Select(c => new ClientDto(c.Id, c.Mid, c.FullName, c.ParticipantId)).ToListAsync(ct);
        }
        
        public async Task<List<WalletDto>> GetClientWalletsAsync(string mid, CancellationToken ct = default)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Mid == mid, ct)
                ?? throw new DomainException($"Клиент с MID '{mid}' не найден");

            return await _context.Wallets.Where(w => w.ClientId == client.Id)
                .Select(w => new WalletDto(w.Id, w.ClientId, w.Code, w.AccountNumber, w.Status.ToString(),
                w.CreatedAt)).ToListAsync(ct);
        }

        public async Task<WalletDto> SyncWalletAsync(SyncWalletRequest request, CancellationToken ct = default)
        {
            var client = await _context.Clients.Include(c => c.Wallets).FirstOrDefaultAsync(c => c.Mid == request.Mid, ct)
                ?? throw new DomainException($"Клиент с MID '{request.Mid}' не найден.");

            if (!string.IsNullOrEmpty(request.ParticipantId) && client.ParticipantId != request.ParticipantId)
            {
                var exists = await _context.Clients
                    .AnyAsync(c => c.ParticipantId == request.ParticipantId && c.Id != client.Id, ct);
                if (exists) throw new DomainException($"ParticipantId '{request.ParticipantId}' уже принадлежит другому клиенту.");
                client.SetParticipantId(request.ParticipantId);
            }

            if (!Enum.TryParse<WalletStatus>(request.Status, true, out var newStatus))
                throw new DomainException($"Недопустимый статус '{request.Status}'.");

            var activeWallet = client.Wallets.FirstOrDefault(w => w.IsActive);

            if(activeWallet == null)
            {
                var newWallet = Wallet.Create(client.Id, request.WalletCode, newStatus, request.AccountNumber);
                _context.Wallets.Add(newWallet); 
                await _context.SaveChangesAsync(ct);
                return new WalletDto(newWallet.Id, newWallet.ClientId, newWallet.Code,
                    newWallet.AccountNumber, newWallet.Status.ToString(), newWallet.CreatedAt);
            }


            if(activeWallet.Code != request.WalletCode)
            {
                throw new DomainException($"Код активного кошелька '{activeWallet.Code}' " +
                    $"не совпадает с переданным '{request.WalletCode}'.");
            }


            if(!string.IsNullOrEmpty(request.AccountNumber) && activeWallet.AccountNumber == null)
            {
                activeWallet.SetAccountNumber(request.AccountNumber);
            }
            activeWallet.ChangeStatus(newStatus);
            await _context.SaveChangesAsync(ct);

            return new WalletDto(activeWallet.Id,activeWallet.ClientId,activeWallet.Code,
                activeWallet.AccountNumber, activeWallet.Status.ToString(), activeWallet.CreatedAt);

        }
        public async Task<WalletDto> UpdateWalletAsync(string code, UpdateWalletRequest request, CancellationToken ct = default)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Code == code, ct)
                ?? throw new DomainException($"Кошелек с кодом {code} не найден.");

            if (!string.IsNullOrEmpty(request.AccountNumber))
            {
                wallet.SetAccountNumber(request.AccountNumber);
            }


        }
    }
}
