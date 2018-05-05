using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PAET.Infraestructure;
using System.Collections.Generic;
using System.Data.Entity;
using PAET.Test.Interfaces;
using PAET.Services.Interfaces;
using PAET.Services;
using PAET.Test.FakeContext;
using Moq;
using AutoMapper;
using PAET.DominioBase.Entidades_Dominio;
using PAET;
using System.Security.Cryptography;
using System.Text;
using PAET.Services.Services;
using PAET.Comun;

namespace PAET.Test
{
    /// <summary>
    /// Descripción resumida de LoginTest
    /// </summary>
    [TestClass]
    public class LoginTest
    {
        private ICandidatosService _candidatoservice;

        [TestMethod]
        public void ValidarUsuario()
        {
            SHA512 shaM = new SHA512Managed();
            Byte[] _password = shaM.ComputeHash(Encoding.GetEncoding(1252).GetBytes("prueba$23"));
            IQueryable<Candidatos> _candidato = new List<Candidatos> { new Candidatos { IdCandidato = 1, IdCategoria = 1, IdTitulacion = 1, IdExperiencia = 1, Nombre = "Julio", Apellido1 = "Gómez", Apellido2 = "Huerta", NIFNIE = "33527105M", Apodo = "GomezJ", Email = "jgasparh@gmail.com", Telefono = "607698429", Pwd = _password, Activo = true } }.AsQueryable();

            var mockSet = new Mock<DbSet<Candidatos>>();
            mockSet.As<IQueryable<Candidatos>>().Setup(m => m.Provider).Returns(_candidato.Provider);
            mockSet.As<IQueryable<Candidatos>>().Setup(m => m.Expression).Returns(_candidato.Expression);
            mockSet.As<IQueryable<Candidatos>>().Setup(m => m.ElementType).Returns(_candidato.ElementType);
            mockSet.As<IQueryable<Candidatos>>().Setup(m => m.GetEnumerator()).Returns(() => _candidato.GetEnumerator());

            var mockContext = new Mock<TestEntities>();
            mockContext.Setup(c => c.Candidatos).Returns(mockSet.Object);
            mockContext.Setup(c => c.Set<Candidatos>()).Returns(mockSet.Object);

            var service = new CandidatosService(mockContext.Object);

            ResultadoAccion resultado = service.AccesoCorrecto("GomezJ", "prueba$23");

            Assert.AreEqual(ResultadoAccion.CodigoResultado.OK, resultado.ResultCode);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

    }
}
