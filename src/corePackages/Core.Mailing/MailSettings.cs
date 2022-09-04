namespace Core.Mailing
{
    public class MailSettings
    {
        public MailSettings()
        {
        }

        public MailSettings(string server, int port, string senderFullName, string senderEmail, string userName, string password, bool isAuthenticate)
        {
            Server = server;
            Port = port;
            SenderFullName = senderFullName;
            SenderEmail = senderEmail;
            UserName = userName;
            Password = password;
            IsAuthenticate = isAuthenticate;
        }

        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderFullName { get; set; }
        public string SenderEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticate { get; set; }
    }
}
