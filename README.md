# 🚀 Portfolio CMS Backend

A secure, scalable, and production-ready RESTful API built with **ASP.NET Core 10**, **PostgreSQL**, **Entity Framework Core**, and **JWT Authentication**.

This backend powers the Portfolio CMS Admin Panel and provides complete content management capabilities for projects, blogs, skills, services, testimonials, profile information, contact messages, and resume management.

---
LIVE =>  https://az-developers.vercel.app/

## 🌐 Live API

**Render**

https://portfolio-cms-backend-z4y4.onrender.com

---

## ✨ Features

- 🔐 JWT Authentication
- 👤 Admin Login
- 📂 Project Management
- 📝 Blog Management
- 💼 Experience Management
- ⚡ Skills Management
- 🛠 Services Management
- 💬 Testimonials Management
- 👨‍💻 Profile Management
- 📄 Resume Management
- 📩 Contact Messages
- 📊 Dashboard Statistics
- 📧 Email Notifications
- 🐳 Docker Support
- 🌍 RESTful API
- 🗄 PostgreSQL Database
- ☁ Hosted on Render

---

# 🛠 Tech Stack

| Technology | Version |
|------------|----------|
| ASP.NET Core | .NET 10 |
| C# | Latest |
| Entity Framework Core | 10 |
| PostgreSQL | Neon |
| JWT Authentication | ✔ |
| BCrypt | ✔ |
| MailKit | ✔ |
| Docker | ✔ |
| Swagger | ✔ |

---

# 📁 Project Structure

```
PortfolioBackend
│
├── Controllers
├── Models
├── DTOs
├── Services
├── Data
├── Migrations
├── Properties
│
├── Program.cs
├── Dockerfile
├── appsettings.json
└── PortfolioBackend.csproj
```

---

# 📦 API Modules

## Authentication

- Login
- JWT Token Generation

---

## Dashboard

- Dashboard Statistics

---

## Projects

- Get Projects
- Create Project
- Update Project
- Delete Project

---

## Blogs

- Get Blogs
- Create Blog
- Update Blog
- Delete Blog

---

## Skills

- Get Skills
- Create Skill
- Update Skill
- Delete Skill

---

## Services

- Get Services
- Create Service
- Update Service
- Delete Service

---

## Experience

- Get Experience
- Create Experience
- Update Experience
- Delete Experience

---

## Testimonials

- Get Testimonials
- Create Testimonial
- Update Testimonial
- Delete Testimonial

---

## Profile

- Get Profile
- Update Profile
- Resume Management
- Social Links

---

## Contact

- Send Contact Message
- View Messages
- Reply Messages

---

# 🔒 Security

- JWT Bearer Authentication
- BCrypt Password Hashing
- Environment Variables
- CORS Protection
- Secure PostgreSQL Connection
- Protected Admin Routes

---

# 🗄 Database

Database Engine

```
PostgreSQL
```

Hosted On

```
Neon Database
```

ORM

```
Entity Framework Core
```

---

# ⚙ Environment Variables

Create the following environment variables before running the application.

```env
ConnectionStrings__DefaultConnection=

Jwt__Key=
Jwt__Issuer=
Jwt__Audience=

EmailSettings__SmtpHost=
EmailSettings__SmtpPort=
EmailSettings__SenderName=
EmailSettings__SenderEmail=
EmailSettings__SenderPassword=
EmailSettings__ReceiverEmail=
```

---

# 🚀 Local Development

Clone repository

```bash
git clone https://github.com/M-Ahmad561Zaheer/Portfolio-CMS-Backend.git
```

Navigate to project

```bash
cd Portfolio-CMS-Backend
```

Restore packages

```bash
dotnet restore
```

Run migrations

```bash
dotnet ef database update
```

Run project

```bash
dotnet run
```

Swagger

```
https://localhost:5245/swagger
```

---

# 🐳 Docker

Build

```bash
docker build -t portfolio-backend .
```

Run

```bash
docker run -p 8080:8080 portfolio-backend
```

---

# ☁ Deployment

Backend is production-ready and can be deployed on:

- Render
- Railway
- Azure App Service
- AWS
- Google Cloud
- DigitalOcean

---

# 📡 Frontend Repository

The frontend React application is available in a separate repository.

---

# 👨‍💻 Author

## Ahmad Zaheer

**Full Stack Developer**

### Technologies

- ASP.NET Core
- React
- PostgreSQL
- Entity Framework Core
- REST APIs
- Docker

GitHub

https://github.com/M-Ahmad561Zaheer

---

# 📄 License

This project is licensed under the **MIT License**.

---

⭐ If you like this project, don't forget to star the repository.
