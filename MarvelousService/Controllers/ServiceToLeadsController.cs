using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts;
using MarvelousService.API.Models;
using MarvelousService.API.Models.Request;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/servicesToLead")]
    public class ServiceToLeadsController : Controller
    {
        private readonly IServiceToService _serviceService;
        private readonly IServiceToLeadService _serviceToLeadService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<ServicesController> _logger;

        public ServiceToLeadsController(IServiceToService serviceService, IMapper autoMapper, IServiceToLeadService serviceToLead, ILogger<ServicesController> logger)
        {
            _serviceService = serviceService;
            _serviceToLeadService = serviceToLead;
            _autoMapper = autoMapper;
            _logger = logger;
        }

        //api/servicesToLead/
        [HttpPost]
        [AuthorizeRole(Role.Regular,Role.Vip)]       
        [SwaggerOperation("Add new serviceToLead")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> AddServiceToLead([FromBody] ServiceToLeadInsertRequestAdd serviceInsertRequest)
        {
            _logger.LogInformation($"Получен запрос на добавление новой услуги.");

            var serviceToLeadModel = _autoMapper.Map<ServiceToLeadModel>(serviceInsertRequest);
            var id = await _serviceToLeadService.AddServiceToLead(serviceToLeadModel);

            _logger.LogInformation($"Услуга с id = {id} успешно добавлена.");

            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/servicesToLead/
        [HttpGet("id")]
        [AuthorizeRole(Role.Regular, Role.Vip)]
        [SwaggerOperation("Get servicesToLead by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<ServiceToLeadResponse>))]
        public async Task<ActionResult<List<ServiceToLeadResponse>>> GetServiceToLeadByIdId(long id)
        {
            _logger.LogInformation($"Запрос на получение всех услуг по id = {id}");

            var serviceToLeadModel = await _serviceToLeadService.GetServiceToLeadById(id);
            var result = _autoMapper.Map<List<ServiceToLeadResponse>>(serviceToLeadModel);

            _logger.LogInformation($"Услуги по id = {id} получены");

            return Ok(result);
        }
    }
}
