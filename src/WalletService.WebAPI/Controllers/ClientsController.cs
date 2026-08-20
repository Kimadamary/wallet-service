using Microsoft.AspNetCore.Mvc;
using WalletService.Application.Services;




namespace WalletService.WebAPI.Controllers
{

    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public ClientsController(IWalletService walletService) 
        { 
            _walletService = walletService;
        
        }
        [HttpGet]
        public async Task<IActionResult> GetClients(CancellationToken ct)
        {
            var clients = await _walletService.GetClientsAsync(ct);
            return Ok(clients);
        }
        [HttpGet("{mid}/wallets")]
        public async Task<IActionResult> GetClientEallets(string mid, CancellationToken ct)
        {
            var wallets = await _walletService.GetClientWalletsAsync(mid, ct);
            return Ok(wallets);
        }
    }
}
