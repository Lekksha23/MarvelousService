namespace MarvelousService.API.Producer.Interface
{
    public interface IResourceProducer
    {
        Task NotifyResourceAdded(int id);
        Task NotifyLeadResourceAdded(int id);
    }
}
