# University Learning Management System - Backend

## Overview

The University Learning Management System is a robust backend solution designed to manage various aspects of university academic activities and communication. This backend system handles user management, academic structures, course management, assignments, exams, and real-time interactions. It is built with ASP.NET Core and uses a range of modern technologies to provide a scalable and efficient solution.

## Features

### User Roles

1. **Admin**
   - Full access to all functions and data.
   - Can add, update, delete, and manage all types of users.

2. **Staff**
   - Manages academic structures and course-related data.
   - Cannot manage admin users but has access to other types.

3. **Professor**
   - Manages courses and assignments within their course cycles.
   - Can add announcements and create quizzes or midterms.

4. **Instructor**
   - Manages sections and assignments within their sections.
   - Can add announcements and quizzes.

5. **Student**
   - Enrolls in courses, submits assignments, and takes exams.
   - Can view course materials and interact through comments and posts.

### Key Features

- **Academic Structure Management:**
  - Admins and staff can add, update, and delete faculties, departments, academic years, and course categories.
  - Staff can add courses and define groups within each academic year.

- **Course Cycles and Sections:**
  - Courses are taught in cycles defined by a combination of course, group, and professor.
  - Sections within each course cycle are managed by instructors, who provide mentorship and handle assignments.

- **Lectures:**
  - Professors and instructors can upload lectures, including videos, PDFs, Word documents, and images.
  - Students can access these materials based on their enrollment in the course cycle or section.

- **Assignments:**
  - Professors and instructors can create assignments for their courses or sections.
  - Students can submit assignments and receive feedback.

- **Exams:**
  - **Quizzes:** Created by instructors for sections or by professors for course cycles.
  - **Midterms:** Administered at the course cycle level by professors.
  - **Semester Exams:** Global exams created by staff for all students in an academic year.
  - **Final Exams:** Similar to semester exams, also global for courses within an academic year.

- **Posts and Announcements:**
  - Professors and instructors can post announcements to sections or course cycles.
  - Students can reply to posts and interact with course-related announcements.

- **Chat Functionality:**
  - Real-time messaging for student groups within academic years.

- **Notifications:**
  - Automatic notifications for exams, posts, lectures, and other important actions.

## Technologies

- **Backend:** ASP.NET Core
- **Real-Time Communication:** SignalR
- **Data Access:** Linq, EF Core
- **Database:** MS-SQL Server
- **Authentication:** JWT (Json Web Token)
- **Architecture Patterns:** Unit of Work/Repository, Dependency Injection, CQRS, Mediator

## Installation

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/university-learning-management-system.git1.
   
2. **Navigate to the Project Directory:**

   ```bash
   cd university-learning-management-system

## Project Setup

1. **Open the Project Directory:**

   Open the `Project` directory in your preferred Integrated Development Environment (IDE) such as Visual Studio or Visual Studio Code.

2. **Restore NuGet Packages:**

   Ensure all necessary NuGet packages are restored by running the following command in your terminal:

   ```bash
   dotnet restore
3. **Apply Database Migrations:**

    Apply any pending Entity Framework Core migrations to set up your database schema by executing:
    
   ```bash
   dotnet ef database update
4. **Run the Server:**

   Start the backend server by running:
 
   ```bash
   dotnet run

## Usage

- Access the backend API endpoints through `http://localhost:5000`.
- Use the provided APIs to manage academic structures, user roles, and course materials.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.
