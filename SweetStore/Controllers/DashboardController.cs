using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _services.GetDashboard();

            return Ok(result);
        }
    }
}
