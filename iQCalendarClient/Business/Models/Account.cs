namespace iQCalendarClient.Business.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Recipients { get; set; }
        public string ClientPassword { get; set; }
        public string AdminPassword { get; set; }
    }
}
