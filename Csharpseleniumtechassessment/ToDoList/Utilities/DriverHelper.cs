using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Csharpseleniumtechassessment.ToDoList.Utilities
{
    public class DriverHelper
    {
        public static IWebDriver Driver { get; set; }

        //Driver only catered for chrome, For future refinement of adding browser selection

        public static void InitializeDriver(bool reuseDriver = false)
        {
            if (Driver == null || !reuseDriver)
            {
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                Driver = new ChromeDriver(options);
                Driver.Manage().Window.Maximize();
            }
        }

        public static void QuitDriver()
        {
            if (Driver != null)
            {
                Driver.Quit();
            }
        }

        //Wait set to 10 sec, for future refinement
        public static IWebElement WaitForElement(By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv => drv.FindElement(locator));
        }
    }
}

