using CoffeeMachineModel;

namespace CoffeeMachineMVC.Clients
{
    public interface ICoffeeMachineClient
    {
        Task<MachineStateDto> GetStateAsync();
        Task PowerOnAsync();
        Task PowerOffAsync();
        Task AddCoffeeAsync(uint grams);
        Task AddWaterAsync(uint ml);
        Task CleanAsync();
        Task BrewAsync(string coffeeType);
        Task<IList<MachineEventDto>> GetEventsAsync();
    }
}
