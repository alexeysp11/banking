namespace Banking.CoreServer
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Start core server"); 

            // string testPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\coreserver\\config.json"); 
            // System.Console.WriteLine("test path: " + testPath); 
            // string fullpath = @"C:\Users\123\Documents\projects\banking\coreserver\config.json"; 
            string fullpath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\coreserver\\config.json"); 
            (new Banking.Network.BankingHttpServer(fullpath)).CreateWebServer();
            System.Console.ReadLine();
        }
    }
}
