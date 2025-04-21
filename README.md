# 🧠 Task Manager API (.NET 9) - Sample Project with core concepts

This is a clean, modular API built with ASP.NET Core 9 using MVC architecture and Entity Framework Core (SQLite).  
Includes secure JWT authentication with roles and audit logging.

---

## 🚀 Features

- ✅ Clean Controller → Service → Repository structure
- ✅ JWT authentication + role-based authorization
- ✅ SQLite database (code-first)
- ✅ AutoMapper for DTO mapping
- ✅ Audit logs for login, task updates, etc.
- ✅ Pagination, sorting, filtering support for tasks
- ✅ Seed data + sample stats for dashboard
- ✅ CORS enabled for Angular frontend

---

## 📁 Project Structure

TaskManagerApi/ ├── Controllers/ ├── Services/ ├── Repositories/ ├── Models/ ├── Dtos/ ├── Data/ ├── Program.cs

---

## 🔐 Auth Flow

- Register user → Assign roles
- Login → Receive JWT
- Use `[Authorize(Roles = "Admin")]` for protected endpoints

---

## 🛠️ Getting Started

```bash
dotnet restore
dotnet ef database update
dotnet run
```

✅ Sample Endpoints

Method Route Description
POST /register Register a user
POST /login Login + JWT
GET /api/taskitems Get all tasks
PUT /api/taskitems/{id} Update a task

Tech Stack

.NET 9

Entity Framework Core

AutoMapper

SQLite

JWT + Role-based Auth

REST API
