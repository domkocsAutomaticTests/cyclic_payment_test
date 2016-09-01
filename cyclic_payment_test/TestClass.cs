using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class NewDomestic
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private int dom = 0, sep = 0, swi = 0, zus = 0, tax = 0, dd = 0;
        private DateTime ma = DateTime.Today;
        /*private DateTime = GetTomorrow();
        private DateTime datum = DateTime.Today.AddYears(10);
        */
        private string[] datum = { "2017", "08", "30" }; // = 20200901;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = @"https://clicpltest.egroup.hu";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            { }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        [Test]
        public void DomesticCyclicTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/Login/Login");
            driver.FindElement(By.LinkText("Login with RSA token [DEMO]")).Click();
            driver.FindElement(By.Id("loginId")).Clear();
            driver.FindElement(By.Id("loginId")).SendKeys("100003");
            driver.FindElement(By.Id("login")).Click();
            try
            {
                driver.FindElement(By.Id("submit")).Click();
            }
            catch { }
            dom = int.Parse(driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[1]/td[2]")).Text);
            driver.Navigate().GoToUrl(baseURL + "/Domestic/New");
            driver.FindElement(By.Id("Input_BnName-Search")).Click(); Thread.Sleep(3000);
            driver.FindElement(By.Id("gvDomesticPartnerSearch_DXDataRow0")).Click(); Thread.Sleep(1000);
            driver.FindElement(By.Id("Input_Details")).Clear();
            driver.FindElement(By.Id("Input_Details")).SendKeys("automaticTest");
            driver.FindElement(By.Id("Input_ExtRef")).Clear();
            driver.FindElement(By.Id("Input_ExtRef")).SendKeys("automaticTest");
            driver.FindElement(By.Id("Input_Amount_formatted")).Clear();
            driver.FindElement(By.Id("Input_Amount_formatted")).SendKeys("1000,00");
            /*Cyclic payment +*/
            driver.FindElement(By.Id("Input_StandingOrder")).Click();
            driver.FindElement(By.Id("calendarStartingDate")).Clear();
            driver.FindElement(By.Id("calendarStartingDate")).SendKeys(datum[0].ToString() + "-" + datum[1].ToString() + "-" + datum[2].ToString());
            driver.FindElement(By.Id("Input_CyclePeriod")).Clear();
            driver.FindElement(By.Id("Input_CyclePeriod")).SendKeys("1");
            driver.FindElement(By.Id("actionButton_Save")).Click();
            try
            {
                driver.FindElement(By.Id("actionButton_Save")).Click();
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            catch (Exception)
            {
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            Thread.Sleep(1000);
            try
            {
                Assert.AreEqual((dom + 1).ToString(), driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[1]/td[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }

        [Test]
        public void SepaCyclicTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/Login/Login");
            driver.FindElement(By.LinkText("Login with RSA token [DEMO]")).Click();
            driver.FindElement(By.Id("loginId")).Clear();
            driver.FindElement(By.Id("loginId")).SendKeys("100003");
            driver.FindElement(By.Id("login")).Click();
            try
            {
                driver.FindElement(By.Id("submit")).Click();
            }
            catch { }
            sep = int.Parse(driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[2]/td[2]")).Text);
            driver.Navigate().GoToUrl(baseURL + "/Sepa/New");
            driver.FindElement(By.Id("Input_BnName")).Clear();
            driver.FindElement(By.Id("Input_BnName")).SendKeys("bnname1/nbnname2");
            driver.FindElement(By.Id("Input_BnAccount_formatted")).Clear();
            driver.FindElement(By.Id("Input_BnAccount_formatted")).SendKeys("FR1420041010050500013M02606");
            driver.FindElement(By.Id("Input_BnBicCode")).Clear();
            driver.FindElement(By.Id("Input_BnBicCode")).SendKeys("AGRIFRPPFEC ");
            driver.FindElement(By.Id("Input_BnAddress")).Clear();
            driver.FindElement(By.Id("Input_BnAddress")).SendKeys("bnaddress1/nbnaddress2");
            new SelectElement(driver.FindElement(By.Id("Input_CountryId"))).SelectByText("FRANCE");
            driver.FindElement(By.Id("Input_Details")).Clear();
            driver.FindElement(By.Id("Input_Details")).SendKeys("automaticTest");
            driver.FindElement(By.Id("Input_ExtRef")).Clear();
            driver.FindElement(By.Id("Input_ExtRef")).SendKeys("automaticTest");
            driver.FindElement(By.Id("Input_Amount_formatted")).Clear();
            driver.FindElement(By.Id("Input_Amount_formatted")).SendKeys("1000,00");
            /*Cyclic payment +*/
            driver.FindElement(By.Id("Input_StandingOrder")).Click();
            driver.FindElement(By.Id("calendarStartingDate")).Clear();
            driver.FindElement(By.Id("calendarStartingDate")).SendKeys(datum[0].ToString() + "-" + datum[1].ToString() + "-" + datum[2].ToString());
            driver.FindElement(By.Id("Input_CyclePeriod")).Clear();
            driver.FindElement(By.Id("Input_CyclePeriod")).SendKeys("1");
            driver.FindElement(By.Id("actionButton_Save")).Click();
            try
            {
                driver.FindElement(By.Id("actionButton_Save")).Click();
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            catch (Exception)
            {
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            Thread.Sleep(1000);
            try
            {
                Assert.AreEqual((sep + 1).ToString(), driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[2]/td[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        [Test]
        public void SwiftCyclicTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/Login/Login");
            driver.FindElement(By.LinkText("Login with RSA token [DEMO]")).Click();
            driver.FindElement(By.Id("loginId")).Clear();
            driver.FindElement(By.Id("loginId")).SendKeys("100003");
            driver.FindElement(By.Id("login")).Click();
            try
            {
                driver.FindElement(By.Id("submit")).Click();
            }
            catch { }
            swi = int.Parse(driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[3]/td[2]")).Text);
            driver.Navigate().GoToUrl(baseURL + "/Swift/New");
            driver.FindElement(By.Id("Input_BnName-Search")).Click(); Thread.Sleep(3000);
            driver.FindElement(By.Id("gvForeignPartnerSearch_DXDataRow14")).Click(); Thread.Sleep(1000);
            driver.FindElement(By.Id("Input_Details")).Clear();
            driver.FindElement(By.Id("Input_Details")).SendKeys("automaticTest");
            driver.FindElement(By.Id("Input_ExtRef")).Clear();
            driver.FindElement(By.Id("Input_ExtRef")).SendKeys("automaticTest");
            driver.FindElement(By.Id("Input_Amount_formatted")).Clear();
            driver.FindElement(By.Id("Input_Amount_formatted")).SendKeys("1000,00");
            /*Cyclic payment +*/
            driver.FindElement(By.Id("Input_StandingOrder")).Click();
            driver.FindElement(By.Id("calendarStartingDate")).Clear();
            driver.FindElement(By.Id("calendarStartingDate")).SendKeys(datum[0].ToString() + "-" + datum[1].ToString() + "-" + datum[2].ToString());
            driver.FindElement(By.Id("Input_CyclePeriod")).Clear();
            driver.FindElement(By.Id("Input_CyclePeriod")).SendKeys("1");
            driver.FindElement(By.Id("actionButton_Save")).Click();
            try
            {
                driver.FindElement(By.Id("actionButton_Save")).Click();
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            catch (Exception)
            {
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            Thread.Sleep(1000);
            try
            {
                Assert.AreEqual((swi + 1).ToString(), driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[3]/td[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        [Test]
        public void ZUSCyclicTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/Login/Login");
            driver.FindElement(By.LinkText("Login with RSA token [DEMO]")).Click();
            driver.FindElement(By.Id("loginId")).Clear();
            driver.FindElement(By.Id("loginId")).SendKeys("100003");
            driver.FindElement(By.Id("login")).Click();
            try
            {
                driver.FindElement(By.Id("submit")).Click();
            }
            catch { }
            zus = int.Parse(driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[4]/td[2]")).Text);
            driver.Navigate().GoToUrl(baseURL + "/DomesticZus/New");
            driver.FindElement(By.Id("Input_Amount_formatted")).Clear();
            driver.FindElement(By.Id("Input_Amount_formatted")).SendKeys("1000,00");
            driver.FindElement(By.Id("Input_DecisionAgreement")).Clear();
            driver.FindElement(By.Id("Input_DecisionAgreement")).SendKeys("40");
            ma.ToString().ToCharArray();
            char[] tomb = new char[6];
            int szam = 0;
            for (int i = 0; i < 8; i++)
            {
                if (i == 4)
                {
                    i = 6;
                }
                string id = "zusDate" + ((szam + 1).ToString());
                driver.FindElement(By.Id(id)).SendKeys((ma.ToString().ToCharArray()[i]).ToString());
                szam++;
            }
            driver.FindElement(By.Id("Input_DeclarationNumber")).Clear();
            driver.FindElement(By.Id("Input_DeclarationNumber")).SendKeys("40");
            driver.FindElement(By.Id("Input_PayerSupplementaryIdNumber")).SendKeys("YXN083441");
            /*Cyclic payment +*/
            driver.FindElement(By.Id("Input_StandingOrder")).Click();
            driver.FindElement(By.Id("Input_GenerateBefore")).Clear();
            driver.FindElement(By.Id("Input_GenerateBefore")).SendKeys("1");
            driver.FindElement(By.Id("calendarStartingDate")).Clear();
            driver.FindElement(By.Id("calendarStartingDate")).SendKeys(datum[0].ToString() + "-" + datum[1].ToString() + "-" + datum[2].ToString());
            driver.FindElement(By.Id("Input_CyclePeriod")).Clear();
            driver.FindElement(By.Id("Input_CyclePeriod")).SendKeys("1");
            driver.FindElement(By.Id("actionButton_Save")).Click();
            try
            {
                driver.FindElement(By.Id("actionButton_Save")).Click();
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            catch (Exception)
            {
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            Thread.Sleep(1000);
            try
            {
                Assert.AreEqual((zus + 1).ToString(), driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[4]/td[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        [Test]
        public void TaxCyclicTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/Login/Login");
            driver.FindElement(By.LinkText("Login with RSA token [DEMO]")).Click();
            driver.FindElement(By.Id("loginId")).Clear();
            driver.FindElement(By.Id("loginId")).SendKeys("100003");
            driver.FindElement(By.Id("login")).Click();
            try
            {
                driver.FindElement(By.Id("submit")).Click();
            }
            catch { }
            tax = int.Parse(driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[5]/td[2]")).Text);
            driver.Navigate().GoToUrl(baseURL + "/DomesticTax/New");
            driver.FindElement(By.Id("dk1-Input_BnAddressBnName")).Click();
            driver.FindElement(By.Id("dk1-Aleksandrów-Kujawski - Urząd Skarbowy")).Click();
            new SelectElement(driver.FindElement(By.Id("Input_BnAddressBnName"))).SelectByText("Aleksandrów Kujawski - Urząd Skarbowy");
            driver.FindElement(By.Id("Input_Amount_formatted")).Clear();
            driver.FindElement(By.Id("Input_Amount_formatted")).SendKeys("1000,00");
            new SelectElement(driver.FindElement(By.Id("Input_BnAccount"))).SelectByText("42101010780024112227000000 - Other");
            Thread.Sleep(500);
            driver.FindElement(By.Id("Input_LiabilityIdentification")).Clear();
            driver.FindElement(By.Id("Input_LiabilityIdentification")).SendKeys("YXN083441");
            /*Cyclic payment +*/
            driver.FindElement(By.Id("Input_StandingOrder")).Click();
            driver.FindElement(By.Id("Input_GenerateBefore")).Clear();
            driver.FindElement(By.Id("Input_GenerateBefore")).SendKeys("1");
            driver.FindElement(By.Id("calendarStartingDate")).Clear();
            driver.FindElement(By.Id("calendarStartingDate")).SendKeys(datum[0].ToString() + "-" + datum[1].ToString() + "-" + datum[2].ToString());
            driver.FindElement(By.Id("Input_CyclePeriod")).Clear();
            driver.FindElement(By.Id("Input_CyclePeriod")).SendKeys("1");
            driver.FindElement(By.Id("actionButton_Save")).Click();
            try
            {
                driver.FindElement(By.Id("actionButton_Save")).Click();
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            catch (Exception)
            {
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            Thread.Sleep(1000);
            try
            {
                Assert.AreEqual((tax + 1).ToString(), driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[5]/td[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        [Test]
        public void DirectDebitCyclicTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/Login/Login");
            driver.FindElement(By.LinkText("Login with RSA token [DEMO]")).Click();
            driver.FindElement(By.Id("loginId")).Clear();
            driver.FindElement(By.Id("loginId")).SendKeys("100003");
            driver.FindElement(By.Id("login")).Click();
            try
            {
                driver.FindElement(By.Id("submit")).Click();
            }
            catch { }
            dd = int.Parse(driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[7]/td[2]")).Text);
            driver.Navigate().GoToUrl(baseURL + "/DirectDebit/New");
            driver.FindElement(By.Id("Input_BnName-Search")).Click(); Thread.Sleep(3000);
            driver.FindElement(By.Id("gvDirectDebitPartnerSearch_DXDataRow0")).Click(); Thread.Sleep(1000);
            driver.FindElement(By.Id("Input_Details")).Clear();
            driver.FindElement(By.Id("Input_Details")).SendKeys("automaticTest");
            driver.FindElement(By.Id("Input_ExtRef")).Clear();
            driver.FindElement(By.Id("Input_ExtRef")).SendKeys("automaticTest");
            driver.FindElement(By.Id("Input_Amount_formatted")).Clear();
            driver.FindElement(By.Id("Input_Amount_formatted")).SendKeys("1000,00");
            driver.FindElement(By.Id("Input_StandingOrder")).Click();
            driver.FindElement(By.Id("Input_GenerateBefore")).Clear();
            driver.FindElement(By.Id("Input_GenerateBefore")).SendKeys("1");
            driver.FindElement(By.Id("calendarStartingDate")).Clear();
            driver.FindElement(By.Id("calendarStartingDate")).SendKeys(datum[0].ToString() + "-" + datum[1].ToString() + "-" + datum[2].ToString());
            driver.FindElement(By.Id("Input_CyclePeriod")).Clear();
            driver.FindElement(By.Id("Input_CyclePeriod")).SendKeys("1");
            driver.FindElement(By.Id("actionButton_Save")).Click();
            try
            {
                driver.FindElement(By.Id("actionButton_Save")).Click();
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            catch (Exception)
            {
                driver.Navigate().GoToUrl(baseURL + "/Home/Dashboard");
            }
            Thread.Sleep(1000);
            try
            {
                Assert.AreEqual((dd + 1).ToString(), driver.FindElement(By.XPath(".//*[@id='main']/div[5]/div[2]/div/div/div/table/tbody/tr[7]/td[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}