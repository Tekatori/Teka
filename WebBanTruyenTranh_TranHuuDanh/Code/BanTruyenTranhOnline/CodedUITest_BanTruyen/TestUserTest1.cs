// Generated by Selenium IDE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Xunit;
public class SuiteTests : IDisposable {
  public IWebDriver driver {get; private set;}
  public IDictionary<String, Object> vars {get; private set;}
  public IJavaScriptExecutor js {get; private set;}
  public SuiteTests()
  {
    driver = new ChromeDriver();
    js = (IJavaScriptExecutor)driver;
    vars = new Dictionary<String, Object>();
  }
  public void Dispose()
  {
    driver.Quit();
  }
  [Fact]
  public void TestUser() {
    driver.Navigate().GoToUrl("http://localhost:30084/");
    driver.Manage().Window.Size = new System.Drawing.Size(1382, 744);
    driver.FindElement(By.LinkText("Trang Chủ")).Click();
    driver.FindElement(By.LinkText("Sản phẩm")).Click();
    driver.FindElement(By.LinkText("Liên hệ")).Click();
    driver.FindElement(By.LinkText("Xin Chào, danh123")).Click();
    driver.FindElement(By.LinkText("Thoát")).Click();
    driver.FindElement(By.LinkText("Đăng Nhập/Đăng Ký")).Click();
    driver.FindElement(By.Name("tenDN")).Click();
    driver.FindElement(By.CssSelector(".toggle-btn:nth-child(3)")).Click();
    driver.FindElement(By.Name("txtdn")).Click();
    driver.FindElement(By.CssSelector(".toggle-btn:nth-child(2)")).Click();
    driver.FindElement(By.Name("tenDN")).Click();
    driver.FindElement(By.Name("tenDN")).SendKeys("danh123");
    driver.FindElement(By.Name("pass")).Click();
    driver.FindElement(By.Name("pass")).SendKeys("123");
    driver.FindElement(By.CssSelector(".btn-submit:nth-child(5)")).Click();
    driver.FindElement(By.Id("menudrop")).Click();
    driver.FindElement(By.LinkText("Action")).Click();
    driver.FindElement(By.CssSelector("span")).Click();
    driver.FindElement(By.Name("txtTuKhoa")).Click();
    driver.FindElement(By.Name("txtTuKhoa")).Click();
    driver.FindElement(By.Name("txtTuKhoa")).SendKeys("độc bộ tiêu dao");
    driver.FindElement(By.CssSelector(".btn-outline-primary")).Click();
    driver.FindElement(By.LinkText("Mua")).Click();
    driver.FindElement(By.CssSelector(".navbar span")).Click();
    driver.FindElement(By.LinkText("Xoá Giỏ")).Click();
    driver.FindElement(By.LinkText("Trang Chủ")).Click();
    driver.FindElement(By.LinkText("Mua")).Click();
    driver.FindElement(By.CssSelector(".navbar > a")).Click();
    driver.FindElement(By.LinkText("Mua Hàng")).Click();
    driver.FindElement(By.Name("diachigiao")).Click();
    driver.FindElement(By.Name("diachigiao")).SendKeys("đâ");
    driver.FindElement(By.Name("ngaygiao")).Click();
    driver.FindElement(By.Name("ngaygiao")).SendKeys("2022-06-16");
    driver.FindElement(By.CssSelector(".btn-secondary")).Click();
  }
}