using CoffeeMachineModel;
using CoffeMachineAPI.Data;
using CoffeMachineAPI.PostgresModel;
using Microsoft.EntityFrameworkCore;

namespace CoffeMachineAPI.Services
{
    public class CoffeeMachineService : ICoffeeMachineService
    {
        private readonly CoffeeMachineContext _context;

        private readonly IDictionary<string, CoffeeRecipe> _recipeMap;

        public CoffeeMachineService(CoffeeMachineContext context, List<CoffeeRecipe> coffeeRecipes)
        {
            _context = context;
            _recipeMap = coffeeRecipes.ToDictionary(r => r.Type.ToUpper(), r => r);
        }
        public async Task AddCoffeeAsync(uint coffeeAmountGrams)
        {
            if (coffeeAmountGrams == 0)
                throw new ArgumentException("Amount of coffee to add must be greater than 0.");
            var state = await GetOrCreateMachineStateAsync();

            state.CoffeeAmountGrams += coffeeAmountGrams;

            await LogEventAsync($"Added {coffeeAmountGrams}g of coffee.");

            await _context.SaveChangesAsync();
        }

        public async Task AddWaterAsync(uint waterAmountMl)
        {
            if (waterAmountMl == 0)
                throw new ArgumentException("Amount of water to add must be greater than 0.");

            var state = await GetOrCreateMachineStateAsync();

            state.WaterAmountMl += waterAmountMl;

            await LogEventAsync($"Added {waterAmountMl}ml of water.");

            await _context.SaveChangesAsync();
        }

        public async Task BrewAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Coffee type must be specified.");

            var normalizedType = type.ToUpper();
            
            if (!_recipeMap.TryGetValue(normalizedType, out var recipe))
                throw new ArgumentException($"Unknown coffee type: {type}");

            var state = await GetOrCreateMachineStateAsync();

            if (!state.IsPoweredOn)
                throw new InvalidOperationException("Cannot brew coffee while the machine is powered off.");

            if (state.CoffeeAmountGrams < recipe.CoffeeGrams || state.WaterAmountMl < recipe.WaterMl)
                throw new InvalidOperationException("Not enough coffee or water to brew the requested type.");

            state.CoffeeAmountGrams -= recipe.CoffeeGrams;
            state.WaterAmountMl -= recipe.WaterMl;

            await LogEventAsync($"Brewed {type}. (Coffee: -{recipe.CoffeeGrams}g, Water: -{recipe.WaterMl}ml)");

            await _context.SaveChangesAsync();
        }

        public async Task CleanAsync()
        {
            var state = await GetOrCreateMachineStateAsync();

            if (!state.IsPoweredOn)
                throw new InvalidOperationException("Cannot clean while the machine is powered off.");

            await LogEventAsync("Automatic cleaning started.");
            Thread.Sleep(1000); // simulate cleaning
            await LogEventAsync("Automatic cleaning completed.");

            await _context.SaveChangesAsync();
        }

        public async Task<MachineStateDto> GetCurrentStateAsync()
        {
            var state = await GetOrCreateMachineStateAsync();
            return new MachineStateDto()
            {
                CoffeeAmountGrams = state.CoffeeAmountGrams,
                WaterAmountMl = state.WaterAmountMl,
                IsPoweredOn = state.IsPoweredOn
            };
        }

        public async Task<IEnumerable<MachineEventDto>> GetEventsAsync()
        {
            var events = await _context.Events
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync();

            return events.Select(e => new MachineEventDto
            {
                Timestamp = e.Timestamp,
                EventDescription = e.EventDescription
            });
        }

        public async Task PowerOffAsync()
        {
            var state = await GetOrCreateMachineStateAsync();

            if (!state.IsPoweredOn)
                throw new InvalidOperationException("Machine is already powered off.");

            state.IsPoweredOn = false;
            
            await LogEventAsync("Coffee machine powered off.");

            await _context.SaveChangesAsync();
        }

        public async Task PowerOnAsync()
        {
            var state = await GetOrCreateMachineStateAsync();
            if (state.IsPoweredOn)
                throw new InvalidOperationException("Machine is already powered on.");

            state.IsPoweredOn = true;

            await LogEventAsync("Coffee machine powered on.");

            await _context.SaveChangesAsync();
        }

        #region Private Helper Methods
        private async Task LogEventAsync(string description)
        {
            var newEvent = new MachineEvent()
            {
                Timestamp = DateTime.UtcNow,
                EventDescription = description
            };
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
        }

        private async Task<MachineState> GetOrCreateMachineStateAsync()
        {
            var state = await _context.MachineStates.FirstOrDefaultAsync();

            if (state != null)
                return state;

            state = new MachineState()
            {
                IsPoweredOn = false,
                CoffeeAmountGrams = 0,
                WaterAmountMl = 0
            };

            _context.MachineStates.Add(state);

            await _context.SaveChangesAsync();

            return state;
        }

        #endregion
    }
}
