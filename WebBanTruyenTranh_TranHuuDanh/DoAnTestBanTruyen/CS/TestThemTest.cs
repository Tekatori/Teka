// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
[TestFixture]
public class TestThemTest {
  private IWebDriver driver;
  public IDictionary<string, object> vars {get; private set;}
  private IJavaScriptExecutor js;
  [SetUp]
  public void SetUp() {
    driver = new ChromeDriver();
    js = (IJavaScriptExecutor)driver;
    vars = new Dictionary<string, object>();
  }
  [TearDown]
  protected void TearDown() {
    driver.Quit();
  }
  [Test]
  public void testThem() {
    driver.Navigate().GoToUrl("http://localhost:30084/Main/addsp");
    driver.Manage().Window.Size = new System.Drawing.Size(1382, 744);
    driver.FindElement(By.LinkText("Xuống Ngay →")).Click();
    driver.FindElement(By.Id("MaSP")).Click();
    driver.FindElement(By.Id("MaSP")).SendKeys("SP030");
    driver.FindElement(By.Id("TenSP")).Click();
    driver.FindElement(By.Id("MaSP")).Click();
    driver.FindElement(By.Id("MaSP")).SendKeys("SP050");
    driver.FindElement(By.Id("TenSP")).Click();
    driver.FindElement(By.Id("MaSP")).Click();
    driver.FindElement(By.Id("MaSP")).SendKeys("SP0501");
    driver.FindElement(By.Id("TenSP")).Click();
    driver.FindElement(By.Id("TenSP")).SendKeys("WWGWGE");
    driver.FindElement(By.Id("GiaTien")).Click();
    driver.FindElement(By.Id("GiaTien")).SendKeys("30000");
    driver.FindElement(By.Id("Tags")).Click();
    driver.FindElement(By.Id("Tags")).SendKeys("ÙHJAF");
    driver.FindElement(By.Name("MoTa")).Click();
    driver.FindElement(By.CssSelector(".editor-field:nth-child(10)")).Click();
    driver.FindElement(By.Id("Tags")).SendKeys("FHFHW");
    driver.FindElement(By.Name("MoTa")).Click();
    driver.FindElement(By.Name("upload")).Click();
    driver.FindElement(By.Name("upload")).SendKeys("C:\\fakepath\\z3460630648812_a63a2215d0879b048ccae81542402504.jpg");
    driver.FindElement(By.CssSelector("p > .btn")).Click();
    driver.FindElement(By.LinkText("Xuống Ngay →")).Click();
  }
}
