using CommandLine;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace POCAutomacao
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                Console.WriteLine($"ID Grupo: {o.GroupId}");
                Console.WriteLine($"ID Relatório: {o.ReportId}");
                Console.WriteLine($"E-mail: {o.Email}");
                Console.WriteLine($"Senha: {o.Password}");
            })
            .WithNotParsed(errors =>
            {
                Console.WriteLine("Erro(s) de linha de comando:");
                foreach (var error in errors)
                {
                    Console.WriteLine($"- {error}");
                }
                CloseApplication();
            }).Value;

            Console.WriteLine();

            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");

            var driver = new ChromeDriver(chromeOptions);

            driver.Navigate().GoToUrl($"https://app.powerbi.com/groups/{options.GroupId}/reports/{options.ReportId}");

            FillTextField(options.Email, "#email", driver);
            await ClickAndWaitForElement("#submitBtn", driver, TimeSpan.FromSeconds(5)); //Preenche o email e prossegue

            FillTextField(options.Password, "#i0118", driver);
            await ClickAndWaitForElement("#idSIButton9", driver, TimeSpan.FromSeconds(3)); //Preenche a senha e prossegue

            await ClickAndWaitForElement("#idBtn_Back", driver, TimeSpan.FromSeconds(8)); //Marca para que as credenciais não sejam salvas

            await ClickAndWaitForElement("#exportMenuBtn", driver, TimeSpan.FromSeconds(1)); //Abre o menu de opções de arquivo
            await ClickAndWaitForElement("#mat-menu-panel-7 > div > button:nth-child(3)", driver, TimeSpan.FromSeconds(1)); //Escolhe PDF como arquivo
            await ClickAndWaitForElement("#okButton", driver, TimeSpan.FromMinutes(2)); //Confirma exportação

            driver.Quit();
            CloseApplication();
        }

        private static async Task ClickAndWaitForElement(string selector, ChromeDriver driver, TimeSpan? delayEnd)
        {
            driver.FindElement(By.CssSelector(selector)).Click();
            if (delayEnd.HasValue)
                await Task.Delay(delayEnd.Value);
        }

        private static void FillTextField(string value, string selector, ChromeDriver driver)
        {
            var field = driver.FindElement(By.CssSelector(selector));
            while (!field.Displayed)
                if (field.Displayed)
                    break;
            field.SendKeys(value);
        }

        private static void CloseApplication()
        {
            Console.WriteLine("Pressione qualquer tecla para fechar");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}