# Veseta-API
Veseta - Appointment Booking System
Veseta is a robust appointment booking system designed for healthcare professionals and patients. Built on ASP.NET Core 7 and EF Core, it leverages the power of API endpoints, JWT authentication, and Identity for secure access.

Key Features:
User Roles: Admin, Doctor, Patient (Paint)
Database Management: Code-first approach using EF Core for seamless database integration.
Security: JWT authentication ensures secure communication, and user roles manage access levels.
Error Handling: Custom middleware implemented for efficient error handling.
Mapping: Utilizes AutoMapper for simplified object-object mapping.
Usage Scenarios:
Admin:

Manages user roles and system configurations.
Doctor:

Accesses appointments, manages schedules, and interacts with patient data.
Patient (Paint):

System for appointment creation, booking, and interaction with healthcare professionals.
Technologies Used:
ASP.NET Core 7: The robust framework for building scalable, high-performance applications.
Entity Framework (EF) Core: Code-first database management for seamless integration.
JWT: Secure authentication for protected API endpoints.
AutoMapper: Simplifies the mapping between objects.
Identity: Manages user authentication and authorization.
How to Use:
Clone the repository.
Configure database settings in appsettings.json.
Run migrations to create the database.
Launch the application and access it through the specified endpoints.
Explore the power of Veseta for efficient appointment management in healthcare!
