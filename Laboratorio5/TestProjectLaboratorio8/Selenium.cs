using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProjectLaboratorio8
{
    public class Selenium
    {
        IWebDriver driver;
        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void PruebaIngresoCrearPaises()
        {
            /// Arrange
            /// Crea el driver de Chrome
            string URL = "https://localhost:7194/";
            /// Pone la pantalla en full screen
            driver.Manage().Window.Maximize();

            /// Act
            /// Se va a la url indicada
            driver.Url = URL;
            /// Hacer click en "Paises"
            IWebElement paisesLink = driver.FindElement(By.LinkText("Paises"));
            paisesLink.Click();
            /// Hacer click en "Crear País"
            IWebElement crearPaisButton = driver.FindElement(By.LinkText("Crear País"));
            crearPaisButton.Click();
            /// Llenar el formulario
            /// Campo de nombre
            IWebElement nombreInput = driver.FindElement(By.Id("Nombre"));
            nombreInput.SendKeys("Holanda");
            /// Campo de Continente
            IWebElement continenteInput = driver.FindElement(By.Id("Continente"));
            SelectElement opcion = new SelectElement(continenteInput);
            opcion.SelectByIndex(3); // El 3 es Europa
            /// Campo de idioma
            IWebElement idiomaInput = driver.FindElement(By.Id("Idioma"));
            idiomaInput.SendKeys("Holandes");
            /// Hacer click en "Crear País" nuevamente
            IWebElement crearPaisSubmit = driver.FindElement(By.CssSelector("input[type='submit']"));
            crearPaisSubmit.Click();
            /// Se obtiene el mensaje de confirmación
            IWebElement mensajeConfirmacion = driver.FindElement(By.TagName("h3"));

            /// Assert
            string mensajeExito = "El pais Holanda fue creado con éxito";
            Assert.AreEqual(mensajeConfirmacion.Text, mensajeExito, "El mensaje no es el esperado");
        }
    }
}