using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetStore.Data;
using SweetStore.Services;
using SweetStore.ViewModels.Order;


namespace SweetStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardServices _services;

        public DashboardController(DashboardServices services)
        {
            _services = services;   
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var result = _services.GetDashboard();

            return Ok(result);
        }
    }
}
