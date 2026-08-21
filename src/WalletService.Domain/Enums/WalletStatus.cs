using System;
using System.Collections.Generic;
using System.Text;

namespace WalletService.Domain.Enums
{
    public enum WalletStatus
    {
        Prcs = 1, // Ожидает открытия
        Actv = 2, // Активен
        Blck = 3, // Заблокирован
        Clsd = 4  // Закрыт (финальный)
    }
}
