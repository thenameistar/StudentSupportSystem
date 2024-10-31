# Student Support System

## Project Description
The **Student Support System** is a console-based application designed to assist students, personal supervisors (PS), and senior tutors (ST) with managing student progress, booking meetings, and tracking interactions. 
This project was developed to demonstrate understanding of database management, user roles, and interaction tracking.

## Features
- **Student**: 
  - Submit self-reports.
  - Book meetings with personal supervisors.
  
- **Personal Supervisor (PS)**:
  - View student reports.
  - Book meetings with students.
  - Respond to student reports.
  
- **Senior Tutor (ST)**:
  - View student reports.
  - Book meetings with personal supervisors.
  - View interactions between students and personal supervisors.


### Prerequisites
-.NET 8.0 SDK 
- SQLite library installed via NuGet in Visual Studio


## Database Structure
The project uses an SQLite database (`studentsupport.db`) with the following tables:
- `UserAuth`: Stores UserID and roles (Student, PS, ST).
- `Students`: Stores student details, self-reports, and assigned personal supervisor IDs.
- `PersonalSupervisors`: Stores personal supervisor details.
- `SeniorTutors`: Stores senior tutor details.
- `Meetings`: Tracks scheduled meetings between users.
- `Interactions`: Logs responses between students and personal supervisors.
