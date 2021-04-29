using System;
using MySql.Data.MySqlClient;

namespace ElevDB.DataLogic
{
    public static class Nextcloud
    {
        public static PasswordHasher pwdHasher = new PasswordHasher(); // Der oprettes et password hasher objekt som bruges til at hashe passwordet. 

        // Connection string information to nextcloud
        private static string connUser = Startup.StaticConfig["Nextcloud:Values:DatabaseUser"];
        private static string connPassword = Startup.StaticConfig["Nextcloud:Values:DatabasePassword"];
        private static string connDBName = Startup.StaticConfig["Nextcloud:Values:DatabaseName"];
        private static string connHost = Startup.StaticConfig["Nextcloud:Values:Host"];
        private static int connPort = Int32.Parse(Startup.StaticConfig["Nextcloud:Values:DatabasePort"]);
        
        // Connection string bygges, er ude fra den enkelte metode så man senere ville kunne udvide med andre metoder (kunne f.eks. være en password reset funktion eller andet), uden at deklarere den flere gange.
        private static string connectionString = $"Persist Security Info=False;database={connDBName};server={connHost};port={connPort};Connect Timeout=30;user id={connUser};pwd={connPassword}";

        // MySql connection, åbnes/lukkes i metoden. 
        private static MySqlConnection sqlConnection = new MySqlConnection(connectionString);

        public static void AddUserToNextcloud(string username,string firstName, string lastName, string password)
        {
            var hashedPassword = pwdHasher.HashPasswordForSQL(password); // Passwordhasheren kaldes så password kan læses af nextcloud.
            var fullName = firstName + " " + lastName; // firstName og lastName sættes sammen til at blive brugt til display name i nextcloud.

            MySqlCommand sqlCommand = new MySqlCommand(); // Der anvendes parameters.AddWithValue til at forebygge SQL injection.
            sqlCommand.CommandText = "INSERT into oc_users (uid,displayname,password,uid_lower) VALUES(@username,@displayname,@password,@username_lower)";
            sqlCommand.Parameters.AddWithValue("@username",username);
            sqlCommand.Parameters.AddWithValue("@displayname", fullName);
            sqlCommand.Parameters.AddWithValue("@password",hashedPassword);
            sqlCommand.Parameters.AddWithValue("@username_lower",username.ToLower());
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
