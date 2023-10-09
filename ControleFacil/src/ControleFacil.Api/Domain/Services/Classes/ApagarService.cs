using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class ApagarService : IService<ApagarRequestContract, ApagarResponseContract, long>
    {
        private readonly IApagarRepository _apagarRepository;
        private readonly IMapper _mapper;

        public ApagarService(
            IApagarRepository apagarRepository,
            IMapper mapper)
        {
            _apagarRepository = apagarRepository;
            _mapper = mapper;
        }

        public async Task<ApagarResponseContract> Adicionar(ApagarRequestContract entidade, long idUsuario)
        {
            Validar(entidade);
            
            Apagar apagar = _mapper.Map<Apagar>(entidade);

            apagar.DataCadastro = DateTime.Now;
            apagar.IdUsuario = idUsuario;

            apagar = await _apagarRepository.Adicionar(apagar);

            return _mapper.Map<ApagarResponseContract>(apagar);
        }

        public async Task<ApagarResponseContract> Atualizar(long id, ApagarRequestContract entidade, long idUsuario)
        {
            Validar(entidade);
            
            Apagar apagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            var contrato = _mapper.Map<Apagar>(entidade);
            contrato.IdUsuario = apagar.IdUsuario;
            contrato.Id = apagar.Id;
            contrato.DataCadastro = apagar.DataCadastro;
            
            contrato = await _apagarRepository.Atualizar(contrato);
           
            return _mapper.Map<ApagarResponseContract>(contrato);
            
            // também da pra fazer dessa forma:
            
            // apagar.Descricao = entidade.Descricao;
            // apagar.Observacao = entidade.Observacao;
            // apagar.ValorInicial = entidade.ValorInicial;
            // apagar.ValorPago = entidade.ValorPago;
            // apagar.DataPagamento = entidade.DataPagamento;
            // apagar.DataReferencia = entidade.DataReferencia;
            // apagar.DataVencimento = entidade.DataVencimento;
            // apagar.idNaturezaDeLancamento = entidade.idNaturezaDeLancamento;

            //apagar = await _apagarRepository.Atualizar(apagar);

            //return _mapper.Map<ApagarResponseContract>(apagar);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            Apagar apagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _apagarRepository.Deletar(apagar);
        }

        public async Task<IEnumerable<ApagarResponseContract>> Obter(long idUsuario)
        {
            var titulosApagar = await _apagarRepository.ObterPeloIdUsuario(idUsuario);
            return titulosApagar.Select(titulo => _mapper.Map<ApagarResponseContract>(titulo));
        }

        public async Task<ApagarResponseContract> Obter(long id, long idUsuario)
        {
            Apagar apagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            
            return _mapper.Map<ApagarResponseContract>(apagar);
        }

        private async Task<Apagar> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var apagar = await _apagarRepository.Obter(id);

            if (apagar is null || apagar.IdUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrada nenhum título a pagar pelo id {id}");
            }

            return apagar;
        }

         private void Validar(ApagarRequestContract entidade)
        {
            if(entidade.ValorInicial < 0 || entidade.ValorPago <0)
            {
                throw new BadRequestException("Os campos ValorInicial e ValorPago não podem ser negativos.");
            }
        }
    }
}