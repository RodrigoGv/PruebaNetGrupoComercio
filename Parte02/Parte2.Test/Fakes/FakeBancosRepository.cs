using Parte02.Models;
using System.Collections.Generic;


namespace Parte02.Test.Fakes
{
    public class FakeBancosRepository : IBancosRepository
    {
        public List<Banco> Bancos { get; set; }

        public FakeBancosRepository()
        {
            Bancos = new List<Banco>();
        }

        private int nextCodigo()
        {

            return (Bancos.Count == 0) ? 1 : Bancos[Bancos.Count - 1].Codigo;
        }

        public int Add(Banco banco)
        {
            banco.Codigo = nextCodigo();
            Bancos.Add(banco);
            return banco.Codigo;
        }

        public Banco Get(int codigo)
        {
            return Bancos.Find(x => x.Codigo == codigo);
        }

        public List<Banco> Search(int id, string nombre)
        {
            List<Banco> lista = Bancos.FindAll(x => (x.Codigo == id || id == 0) && (x.Nombre.Contains(nombre) || nombre == ""));
            return lista;
        }

        public void Update(Banco banco)
        {
            var index = Bancos.FindIndex(x => x.Codigo == banco.Codigo);
            Bancos[index].Direccion = banco.Direccion;
            Bancos[index].Nombre = banco.Nombre;
        }
    }
}
