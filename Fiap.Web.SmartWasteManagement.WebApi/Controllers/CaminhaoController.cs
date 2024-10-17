using AutoMapper;
using Fiap.Web.SmartWasteManagement.Models;
using Fiap.Web.SmartWasteManagement.Services;
using Fiap.Web.SmartWasteManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.SmartWasteManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CaminhaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICaminhaoService _caminhaoService;

        public CaminhaoController(IMapper mapper, ICaminhaoService caminhaoService)
        {
            _mapper = mapper;
            _caminhaoService = caminhaoService;
        }

        [HttpGet]
        [Authorize(Roles =  "operador, analista, gerente")]
        public IActionResult GetAll([FromQuery] int pagina = 1, [FromQuery] int tamanho = 5)
        {
            var caminhoes = _caminhaoService.ListarCaminhoes(pagina, tamanho);
            var caminhoesModel = _mapper.Map<IEnumerable<CaminhaoViewModel>>(caminhoes);
            var paginacao = new CaminhaoPaginacaoViewModel
            {
                Caminhoes = caminhoesModel,
                CurrentPage = pagina,
                PageSize = tamanho
            };
            return Ok(paginacao);
        }

        [HttpGet("ref")]
        [Authorize(Roles = "operador, analista, gerente")]
        public IActionResult GetAllReferencia([FromQuery] int referencia = 0, [FromQuery] int tamanho = 5)
        {
            var caminhoes = _caminhaoService.ListarCaminhoesReferencia(referencia, tamanho);
            var caminhoesModel = _mapper.Map<IEnumerable<CaminhaoViewModel>>(caminhoes);
            var paginacao = new CaminhaoPaginacaoReferenciaViewModel
            {
                Caminhoes = caminhoesModel,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = caminhoesModel.Last().Codigo
            };
            return Ok(paginacao);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "analista, gerente")]
        public IActionResult GetById([FromRoute] int id)
        {
            var caminhao = _caminhaoService.ObterCaminhaoPorId(id);
            if (caminhao == null)
            {
                return NotFound();
            }
            var caminhaoModel = _mapper.Map<CaminhaoViewModel>(caminhao);
            return Ok(caminhaoModel);
        }

        [HttpPost]
        [Authorize(Roles = "gerente")]
        public ActionResult Post([FromBody]CaminhaoViewModel caminhaoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var caminhao = _mapper.Map<CaminhaoModel>(caminhaoModel);
            _caminhaoService.CriarCaminhao(caminhao);
            return CreatedAtAction(nameof(GetById), new { id = caminhao.Codigo }, caminhao);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "gerente")]
        public ActionResult Put([FromRoute] int id, [FromBody] CaminhaoViewModel caminhaoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var caminhao = _mapper.Map<CaminhaoModel>(caminhaoModel);
            _caminhaoService.AtualizarCaminhao(caminhao);
            return NoContent();
        }

    }
}
