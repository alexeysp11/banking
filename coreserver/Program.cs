namespace Banking.CoreServer
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Started core server"); 

            (new Banking.Network.BankingHttpServer()).CreateWebServer();
            System.Console.ReadLine();
        }
    }
}
