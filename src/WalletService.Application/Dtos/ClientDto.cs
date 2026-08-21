using System;
using System.Collections.Generic;
using System.Text;

namespace WalletService.Application.Dtos
{
    public record ClientDto(Guid Id, string Mid, string FullName, string? ParticipantId);
}
