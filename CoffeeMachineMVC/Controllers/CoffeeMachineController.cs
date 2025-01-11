using CoffeeMachineMVC.Clients;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachineMVC.Controllers
{
    public class CoffeeMachineController : Controller
    {
        private readonly ICoffeeMachineClient _coffeeClient;

        public CoffeeMachineController(ICoffeeMachineClient coffeeClient)
        {
            _coffeeClient = coffeeClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var state = await _coffeeClient.GetStateAsync();
            return View(state);
        }

        [HttpPost]
        public async Task<IActionResult> PowerOn()
        {
            try
            {
                await _coffeeClient.PowerOnAsync();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PowerOff()
        {
            try
            {
                await _coffeeClient.PowerOffAsync();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddCoffee(uint amount)
        {
            await _coffeeClient.AddCoffeeAsync(amount);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddWater(uint amount)
        {
            await _coffeeClient.AddWaterAsync(amount);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Brew(string type)
        {
            try
            {
                await _coffeeClient.BrewAsync(type);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Clean()
        {
            try
            {
                await _coffeeClient.CleanAsync();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Events()
        {
            var events = await _coffeeClient.GetEventsAsync();
            return View(events);
        }
    }
}
