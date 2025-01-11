using CoffeMachineAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeMachineAPI.Controllers
{
    [ApiController]
    [Route("api/coffee-machine")]
    public class CoffeeMachineController : ControllerBase
    {
        private readonly ICoffeeMachineService _coffeeMachineService;

        public CoffeeMachineController(ICoffeeMachineService coffeeMachineService)
        {
            _coffeeMachineService = coffeeMachineService;
        }

        [HttpPost("power-on")]
        public async Task<IActionResult> PowerOn()
        {
            try
            {
                await _coffeeMachineService.PowerOnAsync();
                return Ok(new { message = "Coffee machine powered on." });
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        [HttpPost("power-off")]
        public async Task<IActionResult> PowerOff()
        {
            try
            {
                await _coffeeMachineService.PowerOffAsync();
                return Ok(new { message = "Coffee machine powered off." });
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
        public class AddCoffeeRequest
        {
            public uint AmountInGrams { get; set; }
        }

        [HttpPut("coffee")]
        public async Task<IActionResult> AddCoffee([FromBody] AddCoffeeRequest request)
        {
            try
            {
                await _coffeeMachineService.AddCoffeeAsync(request.AmountInGrams);
                return Ok(new { message = "Coffee added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        public class AddWaterRequest
        {
            public uint AmountInMl { get; set; }
        }
        [HttpPut("water")]
        public async Task<IActionResult> AddWater([FromBody] AddWaterRequest request)
        {
            try
            {
                await _coffeeMachineService.AddWaterAsync(request.AmountInMl);
                return Ok(new { message = "Water added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("clean")]
        public async Task<IActionResult> Clean()
        {
            try
            {
                await _coffeeMachineService.CleanAsync();
                return Ok(new { message = "Cleaning process started/completed." });
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
        public class BrewRequest
        {
            public string Type { set; get; } = string.Empty;
        }

        [HttpPost("brew")]
        public async Task<IActionResult> Brew([FromBody] BrewRequest request)
        {
            try
            {
                await _coffeeMachineService.BrewAsync(request.Type);
                var state = await _coffeeMachineService.GetCurrentStateAsync();
                return Ok(new
                {
                    message = $"Brewed {request.Type} successfully.",
                    remainingCoffee = state.CoffeeAmountGrams,
                    remainingWater = state.WaterAmountMl
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
        [HttpGet("state")]
        public async Task<IActionResult> GetState()
        {
            var state = await _coffeeMachineService.GetCurrentStateAsync();
            return Ok(state);
        }
        [HttpGet("events")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = await _coffeeMachineService.GetEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {error = ex.Message});
            }
        }
    }
}
