using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class AreceberService : IService<AreceberRequestContract, AreceberResponseContract, long>
    {
        private readonly IAreceberRepository _areceberRepository;
        private readonly IMapper _mapper;

        public AreceberService(
            IAreceberRepository areceberRepository,
            IMapper mapper)
        {
            _areceberRepository = areceberRepository;
            _mapper = mapper;
        }

        public async Task<AreceberResponseContract> Adicionar(AreceberRequestContract entidade, long idUsuario)
        {
            
            Validar(entidade);
            
            Areceber areceber = _mapper.Map<Areceber>(entidade);

            areceber.DataCadastro = DateTime.Now;
            areceber.IdUsuario = idUsuario;

            areceber = await _areceberRepository.Adicionar(areceber);

            return _mapper.Map<AreceberResponseContract>(areceber);
        }

        public async Task<AreceberResponseContract> Atualizar(long id, AreceberRequestContract entidade, long idUsuario)
        {
            Validar(entidade);
            
            Areceber areceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            var contrato = _mapper.Map<Areceber>(entidade);
            contrato.IdUsuario = areceber.IdUsuario;
            contrato.Id = areceber.Id;
            contrato.DataCadastro = areceber.DataCadastro;
            
            contrato = await _areceberRepository.Atualizar(contrato);
           
            return _mapper.Map<AreceberResponseContract>(contrato);
            
            // também da pra fazer dessa forma:
            
            // areceber.Descricao = entidade.Descricao;
            // areceber.Observacao = entidade.Observacao;
            // areceber.ValorInicial = entidade.ValorInicial;
            // areceber.ValorPago = entidade.ValorPago;
            // areceber.DataPagamento = entidade.DataPagamento;
            // areceber.DataReferencia = entidade.DataReferencia;
            // areceber.DataVencimento = entidade.DataVencimento;
            // areceber.idNaturezaDeLancamento = entidade.idNaturezaDeLancamento;

            //areceber = await _areceberRepository.Atualizar(areceber);

            //return _mapper.Map<AreceberResponseContract>(areceber);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            Areceber areceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _areceberRepository.Deletar(areceber);
        }

        public async Task<IEnumerable<AreceberResponseContract>> Obter(long idUsuario)
        {
            var titulosAreceber = await _areceberRepository.ObterPeloIdUsuario(idUsuario);
            return titulosAreceber.Select(titulo => _mapper.Map<AreceberResponseContract>(titulo));
        }

        public async Task<AreceberResponseContract> Obter(long id, long idUsuario)
        {
            Areceber areceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            
            return _mapper.Map<AreceberResponseContract>(areceber);
        }

        private async Task<Areceber> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var areceber = await _areceberRepository.Obter(id);

            if (areceber is null || areceber.IdUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrada nenhum título a receber pelo id {id}");
            }

            return areceber;
        }

        private void Validar(AreceberRequestContract entidade)
        {
            if(entidade.ValorInicial < 0 || entidade.ValorRecebido <0)
            {
                throw new BadRequestException("Os campos ValorInicial e ValorRecebimento não podem ser negativos.");
            }
        }
    }
}