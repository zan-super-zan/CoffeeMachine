using CoffeeMachineModel;

namespace CoffeMachineAPI.Services
{
    public interface ICoffeeMachineService
    {
        Task PowerOnAsync();
        Task PowerOffAsync();
        Task AddCoffeeAsync(uint coffeeAmountGrams);
        Task AddWaterAsync(uint waterAmountMl);
        Task CleanAsync();
        Task BrewAsync(string type);
        Task<MachineStateDto> GetCurrentStateAsync();
        Task<IEnumerable<MachineEventDto>> GetEventsAsync();
    }
}
