using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    [AuthorizeRole]
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IServiceToLead _serviceToLead;
        private readonly IMapper _autoMapper;
        private static Logger _logger;

        public ServicesController(IServiceService serviceService, IMapper autoMapper)
        {
            _serviceService = serviceService;
            _autoMapper = autoMapper;
            _logger = LogManager.GetCurrentClassLogger();
        }

        //api/services
        [HttpPost]
        [AuthorizeRole(Role.Admin)]
        [SwaggerOperation("Add new service")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
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
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public ActionResult<int> AddServiceToLead([FromBody] ServiceToLeadInsertRequest serviceToLeadInsertRequest)
        {
            _logger.Info($"Получен запрос на добавление услуги лиду.");
            var serviceToLeadModel = _autoMapper.Map<ServiceToLeadModel>(serviceToLeadInsertRequest);
            var id = _servicetoLeadService.AddServiceToLead(serviceToLeadModel);
            _logger.Info($"Услуга с id = {id} успешно добавлена лиду с id = .");
            return StatusCode(StatusCodes.Status201Created, id);
        }
    }
}
