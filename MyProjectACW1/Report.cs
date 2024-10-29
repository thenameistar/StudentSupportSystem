using System; 
using System.Data.SQLite; 

// report class handles functions related to student reports, interactions and database operations
public class Report
{
    // allows students to add or update their self-report
    public static void AddSelfReport(int userId)
    {
        Console.WriteLine("enter your self report:");
        string selfReport = Console.ReadLine(); // read self-report input from student

        using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open(); 
            var command = new SQLiteCommand(connection);

            // SQL command to update self-report for the specified student
            command.CommandText = "UPDATE Students SET SelfReport = @SelfReport WHERE UserID = @UserId";
            command.Parameters.AddWithValue("@SelfReport", selfReport); // assign parameter value for self-report
            command.Parameters.AddWithValue("@UserId", userId); // assign parameter value for user id

            int result = command.ExecuteNonQuery(); // execute command and get affected rows

            if (result > 0)
            {
                Console.WriteLine("self report updated successfully."); 
            }
            else
            {
                Console.WriteLine("an error occurred. self report not updated."); 
            }
        }
    }

    // allows personal supervisors or senior tutors to check a student's report
    public static void CheckStudentReport(string userRole, int userId)
    {
        Console.WriteLine("enter the UserID of the student:");

        if (int.TryParse(Console.ReadLine(), out int studentId)) // parse student id input
        {
            using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Name, SelfReport FROM Students WHERE UserID = @StudentId", connection);
                command.Parameters.AddWithValue("@StudentId", studentId); // assign student id parameter

                using (var reader = command.ExecuteReader()) // execute query and get results
                {
                    if (reader.Read()) // check if data exists
                    {
                        string name = reader["Name"].ToString();
                        string report = reader["SelfReport"].ToString();
                        Console.WriteLine($"student name: {name}");
                        Console.WriteLine($"self report: {report}");
                    }
                    else
                    {
                        Console.WriteLine("no student found with the given UserID.");
                        Menu.ReturnToMenu(userRole, userId); // return to main menu if no student found
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("invalid input. please enter a valid UserID."); // error for invalid input
            Menu.ReturnToMenu(userRole, userId); // return to menu
        }
    }

    // allows personal supervisors to respond to a student's report
    public static void RespondToStudentReport(int supervisorId)
    {
        Console.WriteLine("enter the Student ID for the report you'd like to respond to:");

        if (int.TryParse(Console.ReadLine(), out int studentId)) // parse student id input
        {
            string reportText = GetLatestReport(studentId); // retrieve latest report
            Console.WriteLine($"latest report for student {studentId}: {reportText}");

            Console.WriteLine("enter your response:"); // prompt for response
            string response = Console.ReadLine();

            SaveResponseToDatabase(supervisorId, studentId, response); // save response to interactions table
            Console.WriteLine("response saved successfully.");
        }
        else
        {
            Console.WriteLine("invalid Student ID. returning to menu."); // error message for invalid student id
        }
    }

    // retrieves the latest report for a student
    public static string GetLatestReport(int studentId)
    {
        using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open(); 
            var command = new SQLiteCommand("SELECT SelfReport FROM Students WHERE UserID = @StudentId", connection);
            command.Parameters.AddWithValue("@StudentId", studentId); // assign student id parameter

            using (var reader = command.ExecuteReader()) // execute query and read results
            {
                if (reader.Read())
                {
                    return reader["SelfReport"].ToString(); // return self-report if exists
                }
                else
                {
                    return "no report found for this student."; // message if no report found
                }
            }
        }
    }

    // saves supervisor's response to database in the Interactions table
    private static void SaveResponseToDatabase(int supervisorId, int studentId, string response)
    {
        using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open(); 
            var command = new SQLiteCommand(connection);

            // SQL command to insert new interaction record
            command.CommandText = "INSERT INTO Interactions (StudentID, SupervisorID, Response) VALUES (@studentId, @supervisorId, @response)";
            command.Parameters.AddWithValue("@studentId", studentId); // assign student id
            command.Parameters.AddWithValue("@supervisorId", supervisorId); // assign supervisor id
            command.Parameters.AddWithValue("@response", response); // assign response text
            command.ExecuteNonQuery(); // execute insert command
        }
    }

    // allows senior tutors to view all interactions a personal supervisor has had with students
    public static void ViewPSInteractions(int userId)
    {
        Console.WriteLine("enter the Personal Supervisor ID to view interactions:");

        if (int.TryParse(Console.ReadLine(), out int supervisorId)) // parse supervisor id input
        {
            using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open(); // open database connection
                var command = new SQLiteCommand(connection);

                // SQL command to select interactions for the specified supervisor
                command.CommandText = "SELECT StudentID, Response, Date FROM Interactions WHERE SupervisorID = @SupervisorId";
                command.Parameters.AddWithValue("@SupervisorId", supervisorId); // assign supervisor id

                using (var reader = command.ExecuteReader()) // execute query and read results
                {
                    if (reader.HasRows) // check if there are any interactions
                    {
                        while (reader.Read()) // loop through interactions
                        {
                            Console.WriteLine($"student ID: {reader["StudentID"]}");
                            Console.WriteLine($"response: {reader["Response"]}");
                            Console.WriteLine($"date: {reader["Date"]}");
                            Console.WriteLine("-------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("no interactions found for this Personal Supervisor."); // message if no interactions
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("invalid Personal Supervisor ID."); // error message for invalid supervisor id
        }
    }
}
