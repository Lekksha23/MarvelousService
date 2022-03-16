using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts;
using MarvelousService.API.Models;
using MarvelousService.API.Models.ExceptionModel;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : Controller
    {
        private readonly IServiceToService _serviceService;
        private readonly IServiceToLeadService _serviceToLeadService;
        private readonly IMapper _autoMapper;
        private static Logger _logger;

        public ServicesController(IServiceToService serviceService, IMapper autoMapper, IServiceToLeadService serviceToLead)
        {
            _serviceService = serviceService;
            _serviceToLeadService = serviceToLead;
            _autoMapper = autoMapper;
            _logger = LogManager.GetCurrentClassLogger();
        }

        //api/services
        [HttpPost]
        [AuthorizeRole(Role.Admin)]
        [SwaggerOperation("Add new service")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status404NotFound)]
        public ActionResult<int> AddService([FromBody] ServiceInsertRequest serviceInsertRequest)
        {
            _logger.Info($"Получен запрос на добавление новой услуги.");
            var serviceModel = _autoMapper.Map<ServiceModel>(serviceInsertRequest);
            var id = _serviceService.AddService(serviceModel);
            _logger.Info($"Услуга с id = {id} успешно добавлена.");
            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/services
        [HttpPost("toLead")]
        [SwaggerOperation("Add service to lead")]
        [ProducesResponseType(typeof(ServiceToLeadResponse), StatusCodes.Status201Created)]
        public ActionResult<ServiceToLeadResponse> AddServiceToLead([FromBody] ServiceToLeadInsertRequest serviceToLeadInsertRequest)
        {
            _logger.Info($"Получен запрос на добавление услуги лиду с id = .");
            var serviceToLeadModel = _autoMapper.Map<ServiceToLeadModel>(serviceToLeadInsertRequest);
            var serviceToLead = _serviceToLeadService.AddServiceToLead(serviceToLeadModel);
            _logger.Info($"Услуга с id = {serviceToLeadModel.ServiceId} успешно добавлена лиду с id = .");
            return StatusCode(StatusCodes.Status201Created, serviceToLead);
        }
    }
}
