using System;
using System.Collections.Generic;
using System.Text;

using WalletService.Application.Dtos;



namespace WalletService.Application.Services
{
    public interface IWalletService
    {
        Task<List<ClientDto>> GetClientsAsync(CancellationToken ct = default);
        Task<List<WalletDto>> GetClientWalletsAsync(string mid, CancellationToken ct = default);
        Task<WalletDto> SyncWalletAsync(SyncWalletRequest request, CancellationToken ct = default);
        Task<WalletDto> UpdateWalletAsync(string code, UpdateWalletRequest request, CancellationToken ct = default);
    }
}

