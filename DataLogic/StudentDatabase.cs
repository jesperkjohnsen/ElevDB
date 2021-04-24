using ElevDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ElevDB.DataLogic
{
    public static class StudentDatabase
    {


        // Connection string information to student database
        private static string connUser = Startup.StaticConfig["StudentDatabase:DatabaseUser"];
        private static string connPassword = Startup.StaticConfig["StudentDatabase:DatabasePassword"];
        private static string connDBName = Startup.StaticConfig["StudentDatabase:DatabaseName"];
        private static string connHost = Startup.StaticConfig["StudentDatabase:DatabaseHost"];
        private static int connPort = Int32.Parse(Startup.StaticConfig["StudentDatabase:DatabasePort"]);

        // Connection string bygges, ud fra ovenstående parametre, bliver brugt i metoderne for at undgå kode redundans.
        private static string connectionString = $"Persist Security Info=False;database={connDBName};server={connHost};Connect Timeout=30;user id={connUser};pwd={connPassword}";

        // MySql connection, åbnes/lukkes i metoderne, de bør altid følge denne rækkefølge: Open, Execute Query, Close.
        // Ved brug af datareader bearbejdes retur objekt inden forbindelsen lukkes (se f.eks. GetStudentById).
        private static MySqlConnection sqlConnection = new MySqlConnection(connectionString);
        

        public static Student GetStudentById(int studentId)
        {
            Student student = new Student(); // Vi opretter den student som returneres til sidst og bruges af siden/siderne.

            // Kommandoen oprettes, og henter alt student data (bortset fra studentId da denne haves i forvejen).
            MySqlCommand sqlCommand = new MySqlCommand(); // NB, nedenstående kunne godt forkortes ned til at foregå ved oprettelsen, men undertegnede mener at efterfølgende opbygning giver bedre overblik)
            sqlCommand.CommandText = "Select firstName, lastName, address, zipCode, city, cprNumber, email, phoneNumber, nextcloudUsername, nextcloudOneTimePassword" +
                " from Students where studentId = @studentId";
            sqlCommand.Parameters.AddWithValue("@studentId", studentId); // Parameters.AddWithValue er en indbygget funktion til at hjælpe med at undgå at blive offer for SQL injection. 
            sqlCommand.Connection = sqlConnection;
           
            sqlConnection.Open();
            using(MySqlDataReader rdr = sqlCommand.ExecuteReader())
            {
                
                if (rdr.HasRows) // Hvis der kommer noget data ved søgning sættes det på objektet
                    {
                    while (rdr.Read())
                    {// Værdien i parentes ved rdr.GetString stemmer overens med rækkefølgen i kommandoens select statement.
                        student.StudentId = studentId; // StudentId er det vi også brugte til at foretage søgningen.
                        student.FirstName = rdr.GetString(0);
                        student.LastName = rdr.GetString(1);
                        student.Address = rdr.GetString(2);
                        student.Zipcode = rdr.GetString(3);
                        student.City = rdr.GetString(4);
                        student.CprNumber = rdr.GetString(5);
                        student.Email = rdr.GetString(6);
                        student.PhoneNumber = rdr.GetString(7);
                        student.NextcloudUsername = rdr.GetString(8);
                        student.NextcloudOneTimePassword = rdr.GetString(9);
                    }
                }
            }
            sqlConnection.Close();
            return student;
        }

        public static void UpdateStudent(Student student)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.CommandText = "UPDATE Students SET firstName = @firstName, lastName = @lastName, address = @address, zipCode = @zipCode, city = @city, cprNumber = @cprNumber, email = @email," +
                "phoneNumber = @phoneNumber where studentId = @studentId";
            sqlCommand.Parameters.AddWithValue("@firstName", student.FirstName);
            sqlCommand.Parameters.AddWithValue("@lastName", student.LastName);
            sqlCommand.Parameters.AddWithValue("@address", student.Address);
            sqlCommand.Parameters.AddWithValue("@zipCode", student.Zipcode);
            sqlCommand.Parameters.AddWithValue("@city", student.City);
            sqlCommand.Parameters.AddWithValue("@cprNumber", student.CprNumber);
            sqlCommand.Parameters.AddWithValue("@email", student.Email);
            sqlCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@studentId", student.StudentId);
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();            
        }

        public static void CreateStudent(Student student)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.CommandText = "INSERT INTO Students (firstName,lastName,address,zipCode,city,cprNumber,email,phoneNumber) VALUES(" +
                "@firstName,@lastName,@address,@zipCode,@city,@cprNumber,@email,@phoneNumber)";
            sqlCommand.Parameters.AddWithValue("@firstName", student.FirstName);
            sqlCommand.Parameters.AddWithValue("@lastName", student.LastName);
            sqlCommand.Parameters.AddWithValue("@address", student.Address);
            sqlCommand.Parameters.AddWithValue("@zipCode", student.Zipcode);
            sqlCommand.Parameters.AddWithValue("@city", student.City);
            sqlCommand.Parameters.AddWithValue("@cprNumber", student.CprNumber);
            sqlCommand.Parameters.AddWithValue("@email", student.Email);
            sqlCommand.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@studentId", student.StudentId);
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static void DeleteStudent(Student student)
        {
            // Bemærk at grades tabellen har kaskadevis sletning i SQL således at hvis en studerende i student tabellen slettes, slettes den eller de karakterer de har fået.
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "DELETE FROM Students where studentId = @studentId";
            sqlCommand.Parameters.AddWithValue("@studentId", student.StudentId);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public static List<Student> GetStudents(string firstName)
        {
            List<Student> students = new List<Student>();

            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "Select StudentId,firstName,lastName,email from Students where firstName LIKE @firstName";
            sqlCommand.Parameters.AddWithValue("@firstName", (firstName + "%"));

            sqlConnection.Open();
            using (MySqlDataReader rdr = sqlCommand.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Student student = new Student();
                        student.StudentId = rdr.GetInt32(0);
                        student.FirstName = rdr.GetString(1);
                        student.LastName = rdr.GetString(2);
                        student.Email = rdr.GetString(3);
                        students.Add(student);
                    }
                }
                
            }
            sqlConnection.Close();

            return students;
        }

        public static Staff Login(string username, string password)
        {
            Staff staff = new Staff();
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "Select staffId, teacher, administration from Staff where username = @username AND password = @password";
            sqlCommand.Parameters.AddWithValue("@username", username);
            sqlCommand.Parameters.AddWithValue("@password", password);
            sqlConnection.Open();
            using (MySqlDataReader rdr = sqlCommand.ExecuteReader())
            {
                while (rdr.Read())
                {
                        staff.staffId = rdr.GetInt32(0);
                        staff.Teacher = rdr.GetBoolean(1);
                        staff.Administration = rdr.GetBoolean(2);
                        staff.Username = username;
                        staff.Password = password;
                }
            }
            sqlConnection.Close();

            return staff;
        }

        public static Staff GetStaffById(int staffId)
        {
            Staff staff = new Staff();

            MySqlCommand sqlCommand = new MySqlCommand(); // NB, nedenstående kunne godt forkortes ned til at foregå ved oprettelsen, men undertegnede mener at efterfølgende opbygning giver bedre overblik)
            sqlCommand.CommandText = "Select firstName, lastName, email, teacher, administration, username from Staff where staffId = @staffId";
            sqlCommand.Parameters.AddWithValue("@staffId", staffId); // Parameters.AddWithValue er en indbygget funktion til at hjælpe med at undgå at blive offer for SQL injection. 
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();
            using (MySqlDataReader rdr = sqlCommand.ExecuteReader())
            {

                if (rdr.HasRows) // Hvis der kommer noget data ved søgning sættes det på objektet
                {
                    while (rdr.Read())
                    {// Værdien i parentes ved rdr.GetString stemmer overens med rækkefølgen i kommandoens select statement.
                        staff.staffId = staffId;
                        staff.FirstName = rdr.GetString(0);
                        staff.LastName = rdr.GetString(1);
                        staff.Email = rdr.GetString(2);
                        staff.Teacher = rdr.GetBoolean(3);
                        staff.Administration = rdr.GetBoolean(4);
                        staff.Username = rdr.GetString(5);
                    }
                }
            }
            sqlConnection.Close();

            return staff;
        }

        public static List<Staff> GetStaff(string firstName)
        {
            List<Staff> allstaff = new List<Staff>();

            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "Select staffId,firstName,lastName,email from Staff where firstName LIKE @firstName";
            sqlCommand.Parameters.AddWithValue("@firstName", (firstName + "%"));

            sqlConnection.Open();
            using (MySqlDataReader rdr = sqlCommand.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Staff staff = new Staff();
                        staff.staffId = rdr.GetInt32(0);
                        staff.FirstName = rdr.GetString(1);
                        staff.LastName = rdr.GetString(2);
                        allstaff.Add(staff);
                    }
                }

            }
            sqlConnection.Close();

            return allstaff;
        }
    }
}
