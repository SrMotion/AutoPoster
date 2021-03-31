using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutoPoster
{
    class Program
    {
		public static string bruh, tagg, pass, email; public static int totalpost, startnumber;
		public static void Login()
        {
			Console.Write("[+]E-Mail / Telephone:");
			email = Console.ReadLine();
			Console.Write("[+]Password:");
			pass = Console.ReadLine();
			Console.Write("[+]Tag People:");
			tagg = Console.ReadLine();
		    backnumber:
			Console.Write("[+]Which number to start from. (Write 1 for the first posts):");
			startnumber = int.TryParse(Console.ReadLine(), out int output) ? output : 123324274;
			if (startnumber == 123324274)
			{
				goto backnumber;//not integer go back :D
			}
		    lastpost:
			Console.Write("[+]Last Post To Be Taken:");
			totalpost = int.TryParse(Console.ReadLine(), out int gayr) ? gayr : 123324274;
			if (totalpost == 123324274)
			{
				goto lastpost;
			}
			Console.WriteLine("[+]Line 1 of the Items to be Written to the Post:");
			bruh = Console.ReadLine() + "\n";
			Console.WriteLine("[+]Line 2 of the Items to be Written to the Post:");
			bruh += Console.ReadLine() + "\n";
		}
		private static async Task Main(string[] args)
		{
			if (!File.Exists("chromedriver.exe"))
			{
				new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/808692794101071912/810508321496498226/chromedriver.exe", "chromedriver.exe");//if not exits download the driver.
			}
			Login();
			ChromeOptions options = new ChromeOptions();
			options.AddArgument("log-level=3");
			options.AddArgument("--silent");
			options.AddArguments("--disable-extensions");
			options.AddArguments("--disable-notifications");
			options.AddArguments("--disable-application-cache");
			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
			driver.Url = "https://www.facebook.com/";
			wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='email']"))); // wait until find the path.
			driver.FindElement(By.XPath("//*[@id='email']")).SendKeys(email);
			wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='pass']")));
			driver.FindElement(By.XPath("//*[@id='pass']")).SendKeys(pass);
			wait.Until(ExpectedConditions.ElementExists(By.Name("login")));
			driver.FindElement(By.Name("login")).Click();
			await Task.Delay(8000);
			foreach (int post in Enumerable.Range(startnumber, totalpost))
			{
				try
				{
					wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[starts-with(@id, 'mount_0_0')]/div/div[1]/div/div[3]/div/div/div[1]/div[1]/div/div[2]/div/div/div[3]/div/div[2]/div/div/div/div[1]/div/div[1]/span")));//Share Post Button
					driver.FindElement(By.XPath("//*[starts-with(@id, 'mount_0_0')]/div/div[1]/div/div[3]/div/div/div[1]/div[1]/div/div[2]/div/div/div[3]/div/div[2]/div/div/div/div[1]/div/div[1]/span")).Click();
					await Task.Delay(2000);
					wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[starts-with(@id, 'mount_0_0')]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[1]/div/div/div[2]/div[1]/div[1]/div[1]/div/div/div/div/div[2]/div/div/div/div")));//Post Textbox XPath
					driver.FindElement(By.XPath("//*[starts-with(@id, 'mount_0_0')]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[1]/div/div/div[2]/div[1]/div[1]/div[1]/div/div/div/div/div[2]/div/div/div/div")).SendKeys(bruh + "#" + post.ToString());
					await Task.Delay(2000);
					wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[starts-with(@id, 'mount_0_0')]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[1]/div/div/div[3]/div[1]/div[2]/div/div[2]/span/div/div/div/div/div[1]")));//Tag Button XPath
					driver.FindElement(By.XPath("//*[starts-with(@id, 'mount_0_0')]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[1]/div/div/div[3]/div[1]/div[2]/div/div[2]/span/div/div/div/div/div[1]")).Click();
					await Task.Delay(2000);
					wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[2]/div/div/div[2]/div/div/div[1]/div/div/label")));//Tag Box XPath
					driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[2]/div/div/div[2]/div/div/div[1]/div/div/label")).SendKeys(tagg);
					await Task.Delay(2000);
					wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[2]/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/ul/div/div[2]/li/div/div[1]/div")));//Tagged First People(bw) XPath
					driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[2]/div/div/div[2]/div/div/div[2]/div/div[1]/div/div/ul/div/div[2]/li/div/div[1]/div")).Click();
					wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[2]/div/div/div[2]/div/div/div[1]/div/div/div/div/span")));//OK Button XPath
					driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[2]/div/div/div[2]/div/div/div[1]/div/div/div/div/span")).Click();
					await Task.Delay(1000);
					wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[starts-with(@id, 'mount_0_0')]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[1]/div/div/div[3]/div[2]/div")));//Post Button XPath
					driver.FindElement(By.XPath("//*[starts-with(@id, 'mount_0_0')]/div/div[1]/div/div[4]/div/div/div[1]/div/div[2]/div/div/div/form/div/div[1]/div/div/div[3]/div[2]/div")).Click();
					await Task.Delay(2000);
					Console.Write("[+]Finished Post: " + post + "\nTime: {0:HH:mm:ss}", DateTime.Now.ToString() + "\n\n");
					await Task.Delay(8000);
				}
				catch
				{
					Console.Write("[+]I Think you got post ban waiting 1200000 ms");
					await Task.Delay(1200000);
					driver.Navigate().Refresh();
					await Task.Delay(4000);
				}
			}
		}
	}
}
