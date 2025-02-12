# WodItEasy

## Overview
WodItEasy is a web application built using **React** for the frontend and **ASP.NET Core** for the backend. It follows **DDD (Domain-Driven Design)** and **Clean Architecture** principles. The application is fully containerized using **Docker** and utilizes **MS SQL Server** as the database.

## Features
- **Identity Management**  
  Users can register and log in.
- **Workout Scheduling**  
  Administrators can create, update, and delete workouts, which users can access.
- **Participation in Workouts**  
  Athletes can join, cancel, and rejoin scheduled workouts.
- **Calories Tracking (Upcoming Feature)**  
  Users will be able to track their calorie intake and nutrition.
- **1RM Calculator & Daily Results Logging (Upcoming Feature)**  
  Users will be able to monitor their progress using a one-rep max calculator and logging daily workout results.

## Tech Stack
### Frontend
- React (Vite)

### Backend
- ASP.NET Core
- Entity Framework Core
- MS SQL Server

### Infrastructure
- Docker (Containerized environment)
- Docker Compose

### Other Tools & Libraries
- AutoMapper
- MediatR
- Scrutor
- XUnit
- FakeItEasy
- FluentValidator
- FluentAssertions

## Backend Architecture

### **Domain Layer**
- The Domain Layer contains the core business logic, rules, and validation. It remains independent of any external frameworks or dependencies, ensuring a clean separation of concerns.

```
WodItEasy.Domain/
│── Common/                  # Shared base classes and utilities used across the domain
│── Exceptions/              # Custom domain-specific exceptions
│── Factories/               # Factories for creating domain entities and aggregates
│── Models/                  # Core domain entities and value objects
│── DomainConfiguration.cs   # Extension method for registering domain services in the DI container 
```

### **Application Layer**
- Implements the application's use cases and business logic. It follows the CQRS (Command Query Responsibility Segregation) pattern.

```
WodItEasy.Application/
│── Behaviors/                    # MediatR pipeline behavior for validation
│── Common/                       # Shared utilities and base classes
│── Contracts/                    # Interfaces for repositories and some services
│── Exceptions/                   # Custom application-specific exceptions
│── Features/                     # Feature-specific command and query handlers (CQRS)
│── Mapping/                      # AutoMapper additional logic
│── ApplicationConfiguration.cs   # Extension method for registering application services in the DI container
│── ApplicationSettings.cs        # The JWT secret string
│── Result.cs                     # Utility class for encapsulating command (and in some cases query) results
```
## Getting Started
### Prerequisites
Ensure you have the following installed:
- Docker & Docker Compose
- Node.js (for frontend development)
- .NET SDK (for backend development)
- SQL Server (if running without Docker)

### Installation & Setup
#### Clone the Repository
```sh
git clone https://github.com/aleksandarMilev/wod-it-easy.git
cd WodItEasy
```

#### Run with Docker
```sh
docker-compose up --build
```

#### Run Frontend Locally
```sh
cd client
npm install
npm run dev
```

#### Run Backend Locally
```sh
cd server
dotnet restore
dotnet run
```

## License
[MIT License](LICENSE)

---
🚧 **Work in Progress** 🚧  
***This project is still under development. Active improvements are ongoing.***
