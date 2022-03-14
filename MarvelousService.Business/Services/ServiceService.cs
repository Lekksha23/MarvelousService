using AutoMapper;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Interfaces;

namespace MarvelousService.BusinessLayer.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;

            _mapper = mapper;
        }


        public int AddService(ServiceModel serviceModel)
        {
            var service = _mapper.Map<Service>(serviceModel);

            //добавиьт логирование?

            return _serviceRepository.AddService(service);
        }

        public List<ServiceModel> GetByLeadId(int id)
        {
            //логирование получить запрос на получение лида по LeadId

            var lead = _serviceRepository.GetByLeadId(id);

            return _mapper.Map<List<ServiceModel>>(lead);

        }

        public ServiceModel GetServiceById(int id)
        {
            // логирование получить запрос на получение услуги по id
            
            var service = _serviceRepository.GetServiceById(id);

            return  _mapper.Map<ServiceModel>(service);
        }
    }
}
