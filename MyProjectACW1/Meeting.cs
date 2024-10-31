using System;
using System.Data.SQLite;

public class Meeting
{
    private string connectionString = DatabaseConfig.ConnectionString;

    public static void BookMeeting(int userId, string userRole)
    {
        string meetingDate;
        int meetingWithUserId;

        if (userRole == "Student")
        {
            //  fetch the supervisor's UserID for a student
            meetingWithUserId = GetPersonalSupervisorId(userId);
            Console.WriteLine("Booking a meeting with your Personal Supervisor.");
        }
        else
        {
            Console.WriteLine("Enter the UserID of the person you want to meet with:");
            if (!int.TryParse(Console.ReadLine(), out meetingWithUserId))
            {
                Console.WriteLine("Invalid input. Please enter a valid UserID.");
                return;
            }
        }

        Console.WriteLine("Enter the date for the meeting (format: DD-MM-YYYY):");
        meetingDate = Console.ReadLine();

        // Check if a meeting already exists on this date with the same Personal Supervisor
        using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open();
            var checkCommand = new SQLiteCommand(
                "SELECT COUNT(*) FROM Meetings WHERE MeetingWithUserID = @MeetingWithUserId AND MeetingDate = @MeetingDate",
                connection);
            checkCommand.Parameters.AddWithValue("@MeetingWithUserId", meetingWithUserId);
            checkCommand.Parameters.AddWithValue("@MeetingDate", meetingDate);

            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

            if (count > 0)
            {
                Console.WriteLine("A meeting is already scheduled on this date with your Personal Supervisor. Please choose another date.");
                return;
            }

            // If no meeting exists on the date, proceed with booking
            var command = new SQLiteCommand(
                "INSERT INTO Meetings (UserID, MeetingWithUserID, MeetingDate) VALUES (@UserId, @MeetingWithUserId, @MeetingDate)",
                connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@MeetingWithUserId", meetingWithUserId);
            command.Parameters.AddWithValue("@MeetingDate", meetingDate);

            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine("Meeting booked successfully.");
            }
            else
            {
                Console.WriteLine("An error occurred. Meeting not booked.");
            }
        }
    }

    private static int GetPersonalSupervisorId(int studentId)
    {
        using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open();
            var command = new SQLiteCommand("SELECT PersonalSupervisorID FROM Students WHERE UserID = @StudentId", connection);
            command.Parameters.AddWithValue("@StudentId", studentId);

            var result = command.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
}
