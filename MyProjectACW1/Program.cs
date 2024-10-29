using System; 
using System.Data.SQLite;


class Program
{
    
    static void Main(string[] args)
    {
        // initializes the database if it hasn't been set up yet
        DBInitialiser.InitialiseDatabase();

        // prompts the user to enter their UserID
        Console.WriteLine("Please enter your UserID:");
        int userId;

        // loop to validate user input until a valid UserID is entered
        while (!int.TryParse(Console.ReadLine(), out userId))
        {
            Console.WriteLine("Invalid input. Please enter a valid UserID:");
        }

        // retrieves the role of the user based on their UserID
        string userRole = User.GetUserRole(userId);

        // checks if the UserID exists and the role is valid
        if (string.IsNullOrEmpty(userRole))
        {
            Console.WriteLine("UserID not found or invalid role. Please try again.");
        }
        else
        {
            // if the UserID and role are valid, displays the menu based on the user role
            Menu.DisplayMenu(userRole, userId);
        }
    }
}


