using System;
using System.Collections.Generic;
using System.Text;

namespace WalletService.Application.Dtos
{
    public record SyncWalletRequest(
        string Mid,
        string? ParticipantId,
        string WalletCode,
        string Status,
        string? AccountNumber
    );



 
}
