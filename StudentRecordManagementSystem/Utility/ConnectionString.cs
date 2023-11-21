namespace StudentRecordManagementSystem.Utility
{
    public class ConnectionString
    {
        private static string cName = "Server=DESKTOP-PED26SR\\SQLEXPRESS; Database=StudentMenagement;Trusted_Connection=True;TrustServerCertificate=True";
        public static string CName
        {
            get => cName;
        }
    }
}
