using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFacil.Api.Contract.Usuario
{
    public class UsuarioResponseContract : UsuarioRequestContract
    {
        public long Id { get; set; } 

        public long DataCadastro { get; set; } 
    }
}