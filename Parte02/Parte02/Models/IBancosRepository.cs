using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parte02.Models
{
    public interface IBancosRepository
    {
        List<Banco> Search(int codigo, string nombre);
        int Add(Banco banco);
        void Update(Banco banco);
        Banco Get(int codigo);
    }
}
