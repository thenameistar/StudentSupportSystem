using System; 
using System.Data.SQLite; 

// class that handles the user menu display based on their role
public class Menu
{
    
    public static void DisplayMenu(string userRole, int userId)
    {
        switch (userRole) // check user role
        {
            case "Student":
                DisplayStudentMenu(userId); // show student menu if user is a student
                break;

            case "PS":
                DisplayPSMenu(userId); // show personal supervisor menu if user is PS
                break;

            case "ST":
                DisplaySTMenu(userId); // show senior tutor menu if user is ST
                break;

            default:
                Console.WriteLine("invalid user role"); // message for invalid roles
                break;
        }
    }

    // menu for student options
    private static void DisplayStudentMenu(int userId)
    {
        Console.WriteLine("student menu:");
        Console.WriteLine("1. self report");
        Console.WriteLine("2. book meeting with personal supervisor");
        Console.WriteLine("enter your choice:");

        string choice = Console.ReadLine(); // get user choice

        switch (choice)
        {
            case "1":
                Report.AddSelfReport(userId); // add a self-report if option 1 is selected
                break;

            case "2":
                Meeting.BookMeeting(userId, "Student"); // book meeting if option 2 is selected
                break;

            default:
                Console.WriteLine("invalid choice. please try again"); // message for invalid choice
                break;
        }
    }

    // menu for personal supervisor options
    private static void DisplayPSMenu(int userId)
    {
        Console.WriteLine("personal supervisor menu:");
        Console.WriteLine("1. check student report");
        Console.WriteLine("2. book meeting with student");
        Console.WriteLine("3. respond to report");
        Console.WriteLine("enter your choice:");

        string choice = Console.ReadLine(); // get user choice

        switch (choice)
        {
            case "1":
                Report.CheckStudentReport("PS", userId); // check student report if option 1 is selected
                break;

            case "2":
                Meeting.BookMeeting(userId, "PS"); // book meeting if option 2 is selected
                break;

            case "3":
                Report.RespondToStudentReport(userId); // respond to report if option 3 is selected
                break;

            default:
                Console.WriteLine("invalid choice. please try again"); // message for invalid choice
                break;
        }
    }

    // menu for senior tutor options
    private static void DisplaySTMenu(int userId)
    {
        Console.WriteLine("senior tutor menu:");
        Console.WriteLine("1. check student report");
        Console.WriteLine("2. book meeting with student");
        Console.WriteLine("3. view PS interactions"); 
        Console.WriteLine("enter your choice:");

        string choice = Console.ReadLine(); // get user choice

        switch (choice)
        {
            case "1":
                Report.CheckStudentReport("ST", userId); // check student report if option 1 is selected
                break;

            case "2":
                Meeting.BookMeeting(userId, "ST"); // book meeting if option 2 is selected
                break;

            case "3":
                Report.ViewPSInteractions(userId); // view PS interactions if option 3 is selected
                break;

            default:
                Console.WriteLine("invalid choice. please try again"); // message for invalid choice
                break;
        }
    }

    // method to return to the appropriate menu based on user role
    public static void ReturnToMenu(string userRole, int userId)
    {
        switch (userRole)
        {
            case "PS":
                DisplayPSMenu(userId); // show PS menu if user is PS
                break;

            case "ST":
                DisplaySTMenu(userId); // show ST menu if user is ST
                break;

            default:
                Console.WriteLine("an error occurred. returning to the main menu"); // fallback message
                break;
        }
    }
}
