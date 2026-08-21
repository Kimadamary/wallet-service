using Microsoft.AspNetCore.Mvc;
using WalletService.Application.Services;
using WalletService.Application.Dtos;






namespace WalletService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;

        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncWallets([FromBody] SyncWalletRequest request, CancellationToken ct)
        {
            var result = await _walletService.SyncWalletAsync(request, ct);
            return Ok(result);
        }

        [HttpPatch("{code}")]
        public async Task<IActionResult> UpdateWallets(string code, [FromBody] UpdateWalletRequest request , CancellationToken ct)
        {
            var result = await _walletService.UpdateWalletAsync(code, request, ct);
            return Ok(result);
        }

    }
}
