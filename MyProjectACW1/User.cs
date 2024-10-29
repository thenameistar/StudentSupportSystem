using System.Data.SQLite; 

public class User
{
    // Properties to hold the user's ID and role
    public int UserId { get; set; }
    public string UserRole { get; set; }

    // Constructor to initialize the User object with a user ID and role
    public User(int userId, string userRole)
    {
        UserId = userId;   // Set the UserId property to the given userId
        UserRole = userRole;   // Set the UserRole property to the given userRole
    }

    // This static method retrieves the role of a user based on their user ID
    public static string GetUserRole(int userId)
    {
    
        using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open();  // Open the database connection

            // SQL command selects the role for a given user ID
            var command = new SQLiteCommand("SELECT Role FROM UserAuth WHERE UserID = @UserId", connection);

            // Add a parameter to the command 
            command.Parameters.AddWithValue("@UserId", userId);

            // Execute the command and retrieve the result
            var result = command.ExecuteScalar();

            // Return the result as a string. If result is null, return null
            return result?.ToString();
        }
    }
}
