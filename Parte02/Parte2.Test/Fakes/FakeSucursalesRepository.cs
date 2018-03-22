using Microsoft.AspNetCore.Mvc.Rendering;
using Parte02.Models;
using System.Collections.Generic;

namespace Parte02.Test.Fake
{
    public class FakeSucursalesRepository : ISucursalesRepository
    {
        public List<Sucursal> Sucursales { get; set; }
        public FakeSucursalesRepository()
        {
            Sucursales = new List<Sucursal>();
        }

        private int nextCodigo(int banco)
        {
            var sucursalesBanco = Sucursales.FindLast(x => x.CodBanco == banco);
            return (sucursalesBanco is null) ? 1 : sucursalesBanco.Codigo + 1;
        }
        public int Add(Sucursal sucursal)
        {
            sucursal.Codigo = nextCodigo(sucursal.CodBanco);
            Sucursales.Add(sucursal);
            return sucursal.Codigo;
        }

        public Sucursal Get(int banco, int codigo)
        {

            return Sucursales.Find(x => x.CodBanco == banco && x.Codigo == codigo);
        }

        public List<Sucursal> Search(int banco, int id, string nombre)
        {

            var lista = Sucursales.FindAll(x => (x.CodBanco == banco || banco == 0) &&
                                            (x.Codigo == id || id == 0) &&
                                             (x.Nombre.Contains(nombre) || nombre == "")
            );
            return lista;
        }

        public void Update(Sucursal sucursal)
        {
            var index = Sucursales.FindIndex(x => x.Codigo == sucursal.Codigo);
            Sucursales[index].CodBanco = sucursal.CodBanco;
            Sucursales[index].Direccion = sucursal.Direccion;
            Sucursales[index].NomBanco = sucursal.NomBanco;
            Sucursales[index].Nombre = sucursal.Nombre;
            Sucursales[index].Fecha = sucursal.Fecha;


        }

        public List<SelectListItem> getBancosDDL()
        {
            return new List<SelectListItem>();
        }
    }
}
