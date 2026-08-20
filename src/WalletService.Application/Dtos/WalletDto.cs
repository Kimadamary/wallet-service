using System;
using System.Collections.Generic;
using System.Text;

namespace WalletService.Application.Dtos
{
    public record WalletDto(Guid Id, Guid ClientId, string Code, string? AccountNumber, string Status, DateTime CreatedAt);
}
