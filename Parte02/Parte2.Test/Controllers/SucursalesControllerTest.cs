using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parte02.Controllers;
using Parte02.Models;
using Parte02.Test.Fake;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Parte2.Test.Controllers
{
    [TestClass]
    public class SucursalesControllerTest
    {
        [TestMethod]
        public void SaveSucursalWorking()
        {
            var db = new FakeSucursalesRepository();
            var controller = new SucursalesController(db);

            controller.save(new Sucursal() { Nombre = "BENAVIDES", CodBanco = 1, NomBanco = "BCB", Direccion = "CERRO GRIS", Fecha = DateTime.Now });

            Assert.AreEqual(1, db.Sucursales.Count);

        }

        [TestMethod]
        public void CreateSucursalesControllerInstanceTest()
        {
            var db = new FakeSucursalesRepository();
            var controller = new SucursalesController(db);
            Assert.IsNotNull(controller);
        }


        [TestMethod]
        public void SearchBancosConFiltroNombre()
        {
            var db = new FakeSucursalesRepository();
            FakeDataSucursales(db.Sucursales);
            var controller = new SucursalesController(db);
            var result = controller.search(0, 0, "A") as PartialViewResult;
            var model = result.Model as SucursalView;
            Assert.AreEqual(4, model.sucursales.Count);
        }

        [TestMethod]
        public void SearchBancosConFiltroCodigoBanco()
        {
            var db = new FakeSucursalesRepository();
            FakeDataSucursales(db.Sucursales);
            var controller = new SucursalesController(db);

            controller.search(1, 0, "");
            var result = controller.search(1, 0, "") as PartialViewResult;
            var model = result.Model as SucursalView;
            Assert.AreEqual(2, model.sucursales.Count);
        }

        [TestMethod]
        public void SearchBancosConFiltroCodigoSucursal()
        {
            var db = new FakeSucursalesRepository();
            FakeDataSucursales(db.Sucursales);
            var controller = new SucursalesController(db);

            controller.search(1, 0, "");
            var result = controller.search(1, 1, "") as PartialViewResult;
            var model = result.Model as SucursalView;
            Assert.AreEqual(1, model.sucursales.Count);
        }

        [TestMethod]
        public void SearchBancosSinFiltroCodigo()
        {
            var db = new FakeSucursalesRepository();
            FakeDataSucursales(db.Sucursales);
            var controller = new SucursalesController(db);

            controller.search(0, 0, "");
            var result = controller.search(0, 0, "") as PartialViewResult;
            var model = result.Model as SucursalView;
            Assert.AreEqual(5, model.sucursales.Count);
        }

        public void FakeDataSucursales(List<Sucursal> db)
        {
            db.Add(new Sucursal() { Codigo = 1, Nombre = "BENAVIDES", CodBanco = 1, NomBanco = "BCB", Direccion = "CERRO GRIS", Fecha = DateTime.Now });
            db.Add(new Sucursal() { Codigo = 2, Nombre = "ANGAMOS", CodBanco = 1, NomBanco = "BCB", Direccion = "CERRO CERO", Fecha = DateTime.Now });
            db.Add(new Sucursal() { Codigo = 1, Nombre = "ARAMBURU", CodBanco = 2, NomBanco = "INTERBANK", Direccion = "CERRO UNO", Fecha = DateTime.Now });
            db.Add(new Sucursal() { Codigo = 2, Nombre = "RICARDO PALMA", CodBanco = 2, NomBanco = "INTERBANK", Direccion = "CERRO DOS", Fecha = DateTime.Now });
            db.Add(new Sucursal() { Codigo = 1, Nombre = "28 DE JULIO", CodBanco = 3, NomBanco = "SCOTIABANK", Direccion = "CERRO TRES", Fecha = DateTime.Now });
        }
    }
}
