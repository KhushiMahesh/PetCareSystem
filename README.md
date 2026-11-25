# 🐾 Pawberry Animal Hospital

A web-based animal hospital management system designed to simplify pet healthcare management and veterinary operations.

## Overview

Pawberry Animal Hospital is a role-based web application connecting pet owners and veterinarians through a centralized platform.

The system provides a centralized solution for managing pet profiles, appointments, medical records, vaccinations, emergency cases, and billing.

It consists of two main portals:

- **Pet Owner Portal:** Allows pet owners to manage their pets and access their healthcare information.
- **Veterinarian Portal:** Allows veterinarians to manage appointments, medical records, vaccinations, emergencies, and billing.

## Features

### 🐶 Pet Owner Portal

- User registration and login.
- Add, view, and manage pet profiles.
- Select existing breeds or enter custom breed information.
- Book and manage appointments.
- View pet medical records.
- View vaccination records and schedules.
- Access emergency case information.
- View invoices and billing details.

### 🩺 Veterinarian Portal

- Secure veterinarian login.
- Dedicated veterinarian dashboard.
- View and manage appointments.
- Update appointment statuses.
- Maintain medical records and treatment information.
- Manage vaccination records and schedules.
- Handle emergency cases.
- Generate and manage invoices through the billing module.

## Technology Stack

| Component | Technology |
|---|---|
| Framework | ASP.NET Core Blazor Web App |
| Programming Language | C# |
| Frontend | Razor Components, HTML, CSS, JavaScript |
| Backend | ASP.NET Core |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Database Provider | Npgsql |
| Authentication | ASP.NET Core Identity |
| Authorization | Role-Based Authorization |
| Version Control | Git and GitHub |
| Development Environment | Visual Studio Code |

## Project Structure


PetCareSystem/
│
├── src/
│   └── PetCareSystem.Web/
│       │
│       ├── Components/
│       │   ├── Account/
│       │   ├── Layout/
│       │   └── Pages/
│       │
│       ├── Data/
│       ├── Helpers/
│       ├── Models/
│       ├── Properties/
│       ├── Services/
│       ├── wwwroot/
│       │
│       ├── Program.cs
│       └── PetCareSystem.Web.csproj
│
├── .gitignore
└── README.md


## Database and Authentication

The application uses **PostgreSQL** as its primary database.

**Entity Framework Core** is used as the Object-Relational Mapper (ORM), with the Npgsql provider to communicate with PostgreSQL.

**ASP.NET Core Identity** handles user registration and authentication. Role-based authorization ensures that pet owners and veterinarians can access the appropriate features.

The database connection is configured through `ConnectionStrings:DefaultConnection`.

Database credentials should be stored securely using environment variables or user secrets and should never be committed to the GitHub repository.

## Getting Started

### Prerequisites

Before running the application, ensure that the following are installed:

- .NET 10 SDK
- PostgreSQL
- Visual Studio Code or Visual Studio
- Git

### 1. Clone the Repository


git clone https://github.com/KhushiMahesh/PetCareSystem.git


### 2. Navigate to the Project Directory


cd PetCareSystem


### 3. Configure the Database

Set the PostgreSQL connection string using a PowerShell environment variable:


$env:ConnectionStrings__DefaultConnection = "Host=localhost;Port=5432;Database=PetCareSystem;Username=YOUR_USERNAME;Password=YOUR_PASSWORD"


Replace `YOUR_USERNAME` and `YOUR_PASSWORD` with your PostgreSQL credentials.

### 4. Restore Dependencies


dotnet restore .\src\PetCareSystem.Web\PetCareSystem.Web.csproj


### 5. Build the Application

dotnet build .\src\PetCareSystem.Web\PetCareSystem.Web.csproj


### 6. Run the Application


dotnet run --project .\src\PetCareSystem.Web\PetCareSystem.Web.csproj


Once the application starts, open the local URL displayed in the terminal.

The current development URL is:

`http://localhost:5065`

## Application Architecture

The application follows a component-based architecture using Blazor and ASP.NET Core.

- **Presentation Layer:** Razor components provide the user interface.
- **Application Layer:** Services handle application operations and business logic.
- **Data Layer:** Entity Framework Core manages database communication with PostgreSQL.
- **Authentication Layer:** ASP.NET Core Identity manages user accounts and role-based access.

## Future Enhancements

- Automated appointment notifications.
- Vaccination reminders.
- Advanced reporting and analytics.
- Online payment gateway integration.
- Enhanced emergency management.
- Additional administrative features.

## License

This project is developed for educational and application development purposes.