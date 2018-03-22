namespace Parte2.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Parte02.Controllers;
    using Parte02.Models;
    using Parte02.Test.Fakes;
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    namespace OdeToFood.Tests.Controllers
    {
        [TestClass]
        public class BancosControllerTests
        {
            [TestMethod]
            public void SaveBancoWorking()
            {
                var db = new FakeBancosRepository();
                var controller = new BancosController(db);

                controller.save(new Banco() { Nombre = "BCP", Direccion = "Cerro Gris", Fecha = DateTime.Now });

                Assert.AreEqual(1, db.Bancos.Count);

            }

            [TestMethod]
            public void CreateBancosControllerInstanceTest()
            {
                var db = new FakeBancosRepository();
                var controller = new BancosController(db);
                Assert.IsNotNull(controller);
            }

            [TestMethod]
            public void SearchBancosConFiltroNombre()
            {
                var db = new FakeBancosRepository();
                db.Bancos = FakeDataBancos();
                var controller = new BancosController(db);

                var result = controller.search(0, "A");
                Assert.IsNotNull(result);
                var model = ((ViewResult)result).Model as List<Banco>;
                Assert.AreEqual(3, model.Count);
            }

            [TestMethod]
            public void SearchBancosConFiltroCodigo()
            {
                var db = new FakeBancosRepository();
                db.Bancos = FakeDataBancos();
                var controller = new BancosController(db);
                
                ViewResult result = controller.search(1, "") as ViewResult;

                var model = result.Model as List<Banco>;
                Assert.AreEqual(1, model.Count);
            }

            [TestMethod]
            public void SearchBancosSinFiltros()
            {
                var db = new FakeBancosRepository();
                db.Bancos = FakeDataBancos();
                var controller = new BancosController(db);

                controller.search(0, "");
                ViewResult result = controller.search(0, "") as ViewResult;

                var model = result.Model as List<Banco>;
                Assert.AreEqual(4, model.Count);
            }

            public List<Banco> FakeDataBancos()
            {
                var db = new List<Banco>();
                db.Add(new Banco() { Codigo = 1, Nombre = "BCP", Direccion = "CERRO GRIS", Fecha = DateTime.Now });
                db.Add(new Banco() { Codigo = 2, Nombre = "INTERBANK", Direccion = "LA MOLINA", Fecha = DateTime.Now });
                db.Add(new Banco() { Codigo = 3, Nombre = "SCOTIABANK", Direccion = "MIRAFLORES", Fecha = DateTime.Now });
                db.Add(new Banco() { Codigo = 4, Nombre = "BBVA", Direccion = "CERRO VERDE", Fecha = DateTime.Now });
                return db;
            }
        }
    }

}
