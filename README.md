# WodItEasy

## 🚀 Overview  
WodItEasy is a **web application** designed to help users manage and participate in workouts. Built with **React** on the frontend and **ASP.NET Core** on the backend, it follows **Domain-Driven Design** and **Clean Architecture** principles. The application is fully containerized using **Docker** and utilizes **MS SQL Server** as the database.

---

## 🎯 Features  
- 🔐 **Identity Management** – User registration and authentication.  
- 📅 **Workout Scheduling** – Admins can create, update, and delete workouts.  
- 🏋️ **Workout Participation** – Athletes can join, cancel, and rejoin workouts.  
- 🔥 **Calories Tracking** *(Upcoming)* – Track calorie intake and nutrition.  
- 📊 **1RM Calculator & Daily Results Logging** *(Upcoming)* – Monitor progress using a one-rep max calculator and daily workout logs.  

---

## 🛠 Tech Stack  
### **Frontend**  
- React (Vite)  

### **Backend**  
- ASP.NET Core  
- Entity Framework Core  
- MS SQL Server  

### **Infrastructure**  
- Docker

### **Other Tools & Libraries**  
- AutoMapper  
- MediatR  
- Scrutor  
- XUnit
- FakeItEasy  
- FluentValidator
- FluentAssertions  
- Swagger

---

## 🏛 Backend Architecture  

### 📂 **Domain Layer**  
The **Domain Layer** contains core business logic, rules, and validation. It is independent of any external frameworks and technologies.  
```plaintext
WodItEasy.Domain/
│── Common/                  # Shared base classes and utilities
│── Exceptions/              # Custom domain-specific exceptions
│── Factories/               # Factories for domain entities
│── Models/                  # Core domain entities, value objects and enumerations
│── DomainConfiguration.cs   # Registers domain services in DI
```

### ⚙️ **Application Layer**  
Implements **use cases** and business logic, following the **CQRS (Command Query Responsibility Segregation) pattern**.  
```plaintext
WodItEasy.Application/
│── Behaviors/                    # MediatR pipeline behavior
│── Common/                       # Shared utilities and base classes
│── Contracts/                    # Repository & service interfaces
│── Exceptions/                   # Custom application-specific exceptions
│── Features/                     # CQRS command/query handlers for each app feature
│── Mapping/                      # AutoMapper additional logic
│── ApplicationConfiguration.cs   # Registers application services in DI
│── ApplicationSettings.cs        # JWT secret settings
│── Result.cs                     # Util class for simplifying result returning from commands and, in some cases, queries.
```

### 🏗 **Infrastructure Layer**  
Handles **database access, authentication, and external service integrations**.  
```plaintext
WodItEasy.Infrastructure/
│── Identity/                       # User authentication & JWT
│── Persistence/                    # ApplictionDbContext, Fluent API configurations & Repository implementations
│── IInitializer.cs                 # Database initialization interface
│── InfrastructureConfiguration.cs  # Registers infrastructure services in DI
```

### 🌍 **Web Layer**  
The **Web Layer** is the entry point for HTTP requests, handling API routing, middleware, and presentation logic.  
```plaintext
WodItEasy.Web/
│── Areas/                          # Admin-specific endpoints
│── Common/                         # Shared utilities and base classes
│── Extensions/                     # Extension methods for IConfiguration and IApplicationBuilder
│── Features/                       # Controllers for API endpoints
│── Middlewares/                    # Custom Middlewares 
│── Services/                       # Current user service implementation
│── WebConfiguration.cs             # Registers Web layer services in DI
```

### 🚀 **Startup Layer**  
Responsible for **bootstrapping and running** the application.  
```plaintext
WodItEasy.StartUp/
│── ApplicationInitializer.cs       # Calls the Initialize() on each IInitializer.cs implementation
│── appsettings.json                # App configuration settings
│── Program.cs                      # Entry point of the application
```

---

## ⚡ Getting Started  
### 📌 Prerequisites  
Ensure you have the following installed:
- 🐳 Docker
  
Or:
- 🟢 Node.js 
- 🔵 .NET SDK
- 🗄️ MS SQL Server

### 📥 Installation & Setup  
#### 1️⃣ With docker
```sh
git clone https://github.com/aleksandarMilev/wod-it-easy.git
cd wod-it-easy
docker-compose up --build -d
```

#### 2️⃣ If you want to run the client locally:
```sh
git clone https://github.com/aleksandarMilev/wod-it-easy.git
cd wod-it-easy/client
npm install
npm run dev
```

#### 3️⃣ If you want to run the server locally:
```sh
git clone https://github.com/aleksandarMilev/wod-it-easy.git
cd wod-it-easy/server
dotnet restore
dotnet run
```

### 📝 Configuration Notes  

You can configure the **URLs** and **Database** connection in the following files:

#### Client Configuration:
- **Server Endpoint**: In the `.env` file, set the server URL where the client makes requests. If not specified, it defaults to **http://localhost:8080**.
- **Client URL**: In the `Dockerfile`, configure the client’s URL. By default, it runs on **http://localhost:80**.

#### Server Configuration:
- **Server URLs**: In the `launchSettings.json` file, set the server URLs. If not specified, it defaults to:
  - **http://localhost:5097** for HTTP
  - **https://localhost:7141** for HTTPS
  - **http://localhost:8080** for Docker (HTTP)
  - **https://localhost:8081** for Docker (HTTPS)

- **Database Connection**: In the `appsettings.json` file, configure the database connection string. If not specified, the default is:
  
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=sqlserver,1433;Database=WodItEasy;User Id=sa;Password=!Passw0rd;TrustServerCertificate=True;"
  }

---

## 📜 License  
This project is licensed under MIT License.  

---

🚧 **Work in Progress** 🚧  
***This project is not finished yet! I am actively working on it!***
