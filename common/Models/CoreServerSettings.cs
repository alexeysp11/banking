namespace Banking.Common.Models
{
    public sealed class CoreServerSettings
    {
        public string[] Atm { get; set; }
        public string[] Eftpos { get; set; }
        
        public string[] HttpPathsAtm { get; set; }
        public string[] HttpPathsEftpos { get; set; }
    }
}
