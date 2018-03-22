using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parte02.Controllers;
using Parte02.Models;
using Parte02.Test.Fakes;

namespace Parte2.Test.Controllers
{
    [TestClass]
    public class OrdenesPagoControllerTest
    {
        [TestMethod]
        public void SaveBancoWorking()
        {
            var db = new FakeOrdenesPagoRepository();
            var controller = new OrdenesPagoController(db);

            controller.save(new OrdenPago()
            {
                CodBanco = 1,
                CodSucursal = 1,
                CodMoneda = 1,
                CodEstado = 1,
                Codigo = 0,
                Estado = "Registrado",
                Moneda = "Soles",
                Monto = 5,
                NomBanco = "BCP",
                NomSucursal = "Benavides"
            });

            Assert.AreEqual(1, db.OrdenesPago.Count);

        }

        [TestMethod]
        public void CreateBancosControllerInstanceTest()
        {
            var db = new FakeBancosRepository();
            var controller = new BancosController(db);
            Assert.IsNotNull(controller);
        }
    }
}
