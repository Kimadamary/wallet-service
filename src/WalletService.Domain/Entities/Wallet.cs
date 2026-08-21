using System;
using System.Collections.Generic;
using System.Text;
using WalletService.Domain.Enums;
using WalletService.Domain.Exceptions;







namespace WalletService.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public string? AccountNumber { get; private set; }
        public  WalletStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Client Client { get; private set; } = null!;

        private Wallet() { }


        private Wallet (Guid clientId, string code, WalletStatus initialStatus, string? accountNumber = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Код кошелька не может быть пустым.");

            if (initialStatus == WalletStatus.Clsd)
                throw new DomainException("При создании кошелька недопустим статус Clsd.");

            Id = Guid.NewGuid();
            ClientId = clientId;
            Code = code;
            AccountNumber = accountNumber;
            Status = initialStatus;
            CreatedAt = DateTime.UtcNow;
        }

        public static Wallet Create(Guid clientId, string code, WalletStatus initialStatus, string? accountNumber = null)
        {
            if (!Enum.IsDefined(typeof(WalletStatus), initialStatus))
            {
                throw new ArgumentException($"Недопустимое значение статуса кошелька: {initialStatus}");
            }

            if (initialStatus == WalletStatus.Clsd)
            {
                throw new InvalidOperationException("Нельзя создать кошелёк сразу в статусе Закрыт.");
            }

            return new Wallet (clientId, code, initialStatus, accountNumber);
        }



        public void SetAccountNumber(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new DomainException("Номер счета не может быть пустым.");

            if (!string.IsNullOrEmpty(AccountNumber))
                throw new DomainException("Номер счета уже был задан");
            AccountNumber = accountNumber;
        }

        public void ChangeStatus(WalletStatus newStatus)
        {
            if (Status == newStatus) return;

            bool isValid = (Status, newStatus) switch
            {
                (WalletStatus.Prcs, WalletStatus.Actv) => true,
                (WalletStatus.Actv, WalletStatus.Blck) => true,
                (WalletStatus.Blck, WalletStatus.Actv) => true,
                (WalletStatus.Blck, WalletStatus.Clsd) => true,
                _ => false

            };

            if (!isValid)
                throw new DomainException($"Недопустимый переход статуса из '{Status}' в '{newStatus}'.");

            Status = newStatus;
            
        }
        public bool IsActive => Status is WalletStatus.Prcs or WalletStatus.Actv or WalletStatus.Blck;
    }

}
