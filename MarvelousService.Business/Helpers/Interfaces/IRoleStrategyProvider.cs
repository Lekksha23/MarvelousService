namespace MarvelousService.BusinessLayer.Helpers
{
    public interface IRoleStrategyProvider
    {
        IRoleStrategy GetStrategy(int role);
    }
}