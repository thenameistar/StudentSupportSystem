using System;
using System.Data.SQLite; 

// static class for storing the database connection string
public static class DatabaseConfig
{
    
    public static readonly string ConnectionString = "Data Source=studentsupport.db";
}

// class to initialise and set up the database tables and initial data
public class DBInitialiser
{
    // method to set up database schema and insert initial data if the tables are empty
    public static void InitialiseDatabase()
    {
        using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open(); // open the connection to the database
            var command = new SQLiteCommand(connection); // create a command to execute SQL queries

            // create UserAuth table to store user IDs and their roles (Student, PS, or ST)
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS UserAuth (
                    UserID INTEGER PRIMARY KEY,
                    Role TEXT NOT NULL CHECK(Role IN ('Student', 'PS', 'ST'))
                )";
            command.ExecuteNonQuery(); // execute the command

            // create Students table with columns for ID, name, self-report, and supervisor ID
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Students (
                    UserID INTEGER,
                    Name TEXT,
                    SelfReport TEXT,
                    PersonalSupervisorID INTEGER,
                    FOREIGN KEY (UserID) REFERENCES UserAuth(UserID),
                    FOREIGN KEY (PersonalSupervisorID) REFERENCES PersonalSupervisors(UserID)
                )";
            command.ExecuteNonQuery();

            // create PersonalSupervisors table for storing supervisor info linked to UserAuth table
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS PersonalSupervisors (
                    UserID INTEGER,
                    Name TEXT,
                    FOREIGN KEY (UserID) REFERENCES UserAuth(UserID)
                )";
            command.ExecuteNonQuery();

            // create SeniorTutors table for storing senior tutor info linked to UserAuth table
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS SeniorTutors (
                    UserID INTEGER,
                    Name TEXT,
                    FOREIGN KEY (UserID) REFERENCES UserAuth(UserID)
                )";
            command.ExecuteNonQuery();

            // create Meetings table to log meeting details for students, supervisors, and tutors
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Meetings (
                    MeetingID INTEGER PRIMARY KEY,
                    UserID INTEGER,
                    MeetingRole TEXT CHECK(MeetingRole IN ('Student', 'PS', 'ST')),
                    MeetingDate DATETIME,
                    MeetingWithUserID INTEGER,
                    FOREIGN KEY (UserID) REFERENCES UserAuth(UserID),
                    FOREIGN KEY (MeetingWithUserID) REFERENCES UserAuth(UserID)
                )";
            command.ExecuteNonQuery();

            // create Interactions table to store interactions between students and supervisors
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Interactions (
                    InteractionID INTEGER PRIMARY KEY,
                    StudentID INTEGER,
                    SupervisorID INTEGER,
                    Response TEXT,
                    Date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (StudentID) REFERENCES Students(UserID),
                    FOREIGN KEY (SupervisorID) REFERENCES PersonalSupervisors(UserID)
                )";
            command.ExecuteNonQuery();

            // check if the UserAuth table is empty before inserting initial data
            command.CommandText = "SELECT COUNT(*) FROM UserAuth";
            var count = Convert.ToInt32(command.ExecuteScalar()); // get count of records in UserAuth

            if (count == 0) // if empty, insert default records
            {
                // insert initial UserAuth data for various roles
                command.CommandText = @"
                    INSERT INTO UserAuth (UserID, Role) VALUES
                    (101, 'Student'),
                    (102, 'Student'),
                    (103, 'Student'),
                    (201, 'PS'),
                    (202, 'PS'),
                    (301, 'ST')";
                command.ExecuteNonQuery();

                // insert initial student data, linking each student to a personal supervisor
                command.CommandText = @"
                    INSERT INTO Students (UserID, Name, SelfReport, PersonalSupervisorID) VALUES
                    (101, 'Katie Knight', '[NO REPORT]', 201),
                    (102, 'Kaye Mortimer', '[NO REPORT]', 201),
                    (103, 'Harry Wallis', '[NO REPORT]', 202)";
                command.ExecuteNonQuery();

                // insert personal supervisors data
                command.CommandText = @"
                    INSERT INTO PersonalSupervisors (UserID, Name) VALUES
                    (201, 'John Whelan'),
                    (202, 'Adil Khan')";
                command.ExecuteNonQuery();

                // insert senior tutor data
                command.CommandText = @"
                    INSERT INTO SeniorTutors (UserID, Name) VALUES
                    (301, 'Darren McKie')";
                command.ExecuteNonQuery();

                connection.Close(); // close the database connection after setup
            }
        }
    }
}
