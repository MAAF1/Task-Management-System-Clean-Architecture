# Task Management System

A full-stack task management application built with **ASP.NET Core Web API** and **Angular**, following **Clean Architecture** principles.  
The system allows users to create, update, delete, and manage tasks with authentication, validation, filtering, and role-based capabilities.

## Overview

This project was built to practice and demonstrate modern backend and full-stack development concepts using .NET technologies.  
It focuses on building a maintainable and scalable system using:

- **ASP.NET Core Web API**
- **Angular**
- **Clean Architecture**
- **Entity Framework Core**
- **SQL Server**
- **JWT Authentication**
- **ASP.NET Identity**
- **xUnit Testing**

## Features

- User authentication and authorization using **JWT**
- User management with **ASP.NET Identity**
- Create, update, delete, and list tasks
- Filter tasks by status and assigned user
- Input validation and proper error handling
- Clean Architecture structure
- Repository and Unit of Work patterns
- SQL Server database using **Code First** approach
- Angular frontend for task operations
- Unit testing with **xUnit**

## Task Entity

Each task contains:

- **Id**
- **Title** *(required, max 100 characters)*
- **Description** *(optional)*
- **Status** *(Pending, In Progress, Completed)*
- **Created Date**
- **Due Date** *(optional)*
- **Assigned To** *(optional)*

---

## Architecture

The backend follows **Clean Architecture**, separating the project into layers such as:

- **Domain**
- **Application**
- **Infrastructure**
- **Presentation / API**

This helps improve:

- maintainability
- separation of concerns
- testability
- scalability

Key patterns used:

- Repository Pattern
- Unit of Work
- Dependency Injection
- Validation
- Centralized Error Handling

---

## Tech Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- JWT Authentication
- Fluent Validation / Data Annotations


### Frontend
- Angular
- TypeScript


### Tools
- Git
- Visual Studio 
- Postman

---

## Project Structure

```bash
```text
Task-Management-System-Clean-Architecture/
│
├── TaskManagementSystemCleanArchitecture/
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   ├── Persistence/
│   └── Persentation (API)/
│
├── frontend/
│   └── AngularApp/
│
└── README.md
```
# Example Use Cases
```
A user logs in and creates a new task
A user updates task status from Pending to In Progress
A user filters tasks by Completed
A user assigns tasks to another user
Validation errors are shown in the UI if invalid input is entered
```
# Learning Goals
## This project was built to strengthen practical skills in:

- building RESTful APIs with ASP.NET Core
- applying Clean Architecture in a real project
- working with Entity Framework Core and SQL Server
- implementing JWT authentication and identity managementintegrating Angular with a backend API

# Future Improvements
## Possible future enhancements:
- refresh token support
- role-based dashboards
- task comments and attachments
- notifications
- Docker support
- CI/CD pipeline
- deployment to cloud environment

Author
Muhammad Abdulghani

- GitHub: [MAAF1](https://github.com/MAAF1)
- LinkedIn: [Muhammad Abdulghani](https://www.linkedin.com/in/muhammad-abdulghani-058b59284/)
