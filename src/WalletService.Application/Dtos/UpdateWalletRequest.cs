using System;
using System.Collections.Generic;
using System.Text;

namespace WalletService.Application.Dtos
{
    public record UpdateWalletRequest(
        string? Status,
        string? AccountNumber
    );
}
