namespace MarvelousService.API.Producer.Interface
{
    public interface IServiceProducer
    {
        Task NotifyServiceAdded(int id);
        Task NotifyServiceToLeadAdded(int id);
    }
}
