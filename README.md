# CompleteAPI - Production-Ready .NET Backend

A robust, scalable, and secure REST API built with .NET 10. This project demonstrates a production-grade architecture focusing on security, performance, and maintainability.

## 🚀 Core Features

### Security & Authentication
* **Identity Management:** JWT Authentication with a secure Refresh Token flow and token revocation.
* **Data Protection:** Password hashing using BCrypt.
* **Traffic Control:** Custom Rate Limiting (differentiated between login and general endpoints) to prevent brute-force attacks.

### Performance & Reliability
* **Caching:** High-performance data retrieval using Redis.
* **Data Handling:** Advanced Pagination, Filtering, and Sorting logic.
* **Resilience:** Centralized Exception Middleware for consistent error handling and detailed Logging.

### Quality Assurance
* **Automated Testing:** Unit testing suite powered by xUnit and Moq.
* **CI/CD:** Fully automated Build and Test pipeline via GitHub Actions.

## 🛠️ Tech Stack
* **Runtime:** .NET 10
* **Database:** SQLite (Entity Framework Core)
* **Cache:** Redis
* **Documentation:** NSwag / Swagger
* **Testing:** xUnit, Moq
* **Tools:** CORS, DTOs for data abstraction, Interface-based dependencies.

## 🏗️ Architecture
The project follows a clean **Layered Architecture** to ensure separation of concerns:
* **Controllers:** API Endpoints and request handling.
* **Services:** Business logic and core processing.
* **Repositories:** Data access abstraction.
* **DTOs:** Decoupling internal data models from API contracts.

## 🏁 Quick Start

### Prerequisites
* .NET 10 SDK
* Redis (running instance)

### Installation
1. Clone the repository:
   ```bash
   git clone [https://github.com/msenek/CompleteAPI.git](https://github.com/msenek/CompleteAPI.git)

2.  Restore dependencies:
    dotnet restore

3.  update the database:
    dotnet ef database update --project api/

4.  Run the application:
    dotnet run --project api/

    
### 📄 API Documentation
Once the API is running, you can explore the interactive documentation (Swagger) at:
http://localhost:[YOUR_PORT]/swagger

Developed by Mateo Elías Senek.
