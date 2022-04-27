using System.Reflection;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class RoleStrategyProvider : IRoleStrategyProvider
    {
        private List<IRoleStrategy> _roles = new List<IRoleStrategy>();

        public IRoleStrategy GetStrategy(int role)
        {
            if (_roles.Count == 0)
            {
                _roles = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(type => typeof(IRoleStrategy).IsAssignableFrom(type) && type.IsClass)
                                   .Select(type => Activator.CreateInstance(type))
                                   .Cast<IRoleStrategy>()
                                   .ToList();
            }
            return _roles.FirstOrDefault(r => r.Id == role);
        }
    }
}
