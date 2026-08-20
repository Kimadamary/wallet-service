using System;
using System.Collections.Generic;
using System.Text;
using WalletService.Domain.Exceptions;

namespace WalletService.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; private set; }
        public string Mid { get; private set; } = string.Empty;
        public string FullName { get; private set; }= string.Empty;
        public string? ParticipantId { get; private set; }

        private readonly List<Wallet> _wallets = new();
        public IReadOnlyCollection<Wallet> Wallets => _wallets.AsReadOnly();
        


        private Client() { }

        public Client( string mid, string fullname, string? participantId = null)
        {
            if (string.IsNullOrWhiteSpace(mid))
                throw new DomainException("Mid не может быть пустым.");
            if (string.IsNullOrWhiteSpace(fullname))
                throw new DomainException("ФИО не может быть пустым.");

            Id = Guid.NewGuid();
            Mid = mid;
            FullName = fullname;
            ParticipantId = participantId;
        }

        public void SetParticipantId(string participantId)
        {
            if (string.IsNullOrWhiteSpace(participantId))
                throw new DomainException("ParticipantId не может быть пустым.");


            ParticipantId = participantId;
        }
    }
}
