using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Contract.Apagar
{
    public class ApagarRequestContract
    {
        public long idNaturezaDeLancamento { get; set; }
        
        public string Descricao { get; set; } = string.Empty;

        public string Observacao { get; set; } = string.Empty;

        public double ValorInicial { get; set; }

        public double ValorPago {get; set; }

        public DateTime? DataReferencia { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime? DataPagamento { get; set; }
        
    }
}