using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    [ApiController]
    [Route("titulos-a-pagar")]
    public class ApagarController : BaseController
    {
        private readonly IService<ApagarRequestContract, ApagarResponseContract, long> _apagarService;

        public ApagarController(IService<ApagarRequestContract, ApagarResponseContract, long> apagarService)
        {
            _apagarService = apagarService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(ApagarRequestContract contrato) 
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Created("", await _apagarService.Adicionar(contrato, _idUsuario));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Obter() 
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.Obter(_idUsuario));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(long id) 
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.Obter(id, _idUsuario));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, ApagarRequestContract contrato) 
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.Atualizar(id, contrato, _idUsuario));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id) 
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                await _apagarService.Inativar(id, _idUsuario);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}