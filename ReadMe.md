

# TurfMate - Sports Turf Booking System 🎾⚽🏏

Welcome to **TurfMate** — your ultimate solution for booking sports turfs and facilities!

TurfMate is a modern, full-stack web application that provides a seamless experience for users to discover, view availability, and book sports turfs, while offering administrators comprehensive management capabilities.

---

## ✨ Features

### 🎯 For Users

* 🔐 **Secure Authentication** — JWT-based login/register system
* 🔍 **Browse Turfs** — View available sports facilities with details
* 📅 **Smart Booking** — Real-time availability checking and time slot selection
* 💰 **Transparent Pricing** — Clear pricing with automatic cost calculation
* 📱 **Responsive Design** — Works perfectly on desktop, tablet, and mobile
* 👤 **Personal Dashboard** — Manage your bookings and profile

### 🛠 For Administrators

* 🎪 **Turf Management** — Full CRUD operations for turf facilities
* 📊 **Booking Management** — View and manage all bookings
* 👥 **User Management** — Monitor and manage user accounts
* 📈 **Analytics** — Booking insights and reports
* 🔒 **Role-based Access** — Secure admin panel

---

## 🚀 Quick Start

### Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Node.js 18+](https://nodejs.org/)
* [SQL Server 2019+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [Git](https://git-scm.com/)

---

### 🏃‍♂️ Run Locally

**Clone the repository**

```bash
git clone https://github.com/your-username/turfmate.git
cd turfmate
```

#### Setup Backend

```bash
cd TurfMate.Api
dotnet restore
dotnet ef database update
dotnet run
```

📡 API runs on: **[https://localhost:5000](https://localhost:5000)**

#### Setup Frontend

```bash
cd ../turfmate-client
npm install
npm run dev
```

🌐 Frontend runs on: **[http://localhost:3000](http://localhost:3000)**

---

### 🔑 Default Admin Account

* **Email**: `admin@turfmate.com`
* **Password**: `admin123`

---

## 🏗 Technology Stack

### Backend Technologies

* **Framework**: ASP.NET Core 8.0
* **Database**: SQL Server with Entity Framework Core
* **Authentication**: JWT Bearer Tokens
* **Security**: BCrypt.Net-Next
* **API Documentation**: Swagger/OpenAPI
* **Data Access**: Dapper + Stored Procedures

### Frontend Technologies

* **Framework**: React 18 with TypeScript
* **Build Tool**: Vite
* **Styling**: Tailwind CSS
* **Routing**: React Router DOM
* **State Management**: React Context + Custom Hooks
* **HTTP Client**: Axios

---

## 📁 Project Structure

```
TurfMate/
├── 📂 TurfMate.Api/                          # ASP.NET Core Web API
│   ├── 📂 Controllers/                       # API Endpoints
│   ├── 📂 Models/                            # Data Models & DTOs
│   ├── 📂 Repositories/                      # Data Access Layer
│   ├── 📂 Services/                          # Business Logic Layer
│   ├── 📂 Data/                              # Database Context
│   ├── 📂 Migrations/                        # EF Core Migrations
│   ├── Program.cs                            # Application Entry
│   └── appsettings.json                      # Configuration
│
├── 📂 turfmate-client/                       # React TypeScript Frontend
│   ├── 📂 src/
│   │   ├── 📂 components/                    # React Components
│   │   ├── 📂 pages/                         # Page Components
│   │   ├── 📂 services/                      # API Services
│   │   ├── 📂 types/                         # TypeScript Definitions
│   │   ├── 📂 hooks/                         # Custom React Hooks
│   │   ├── 📂 contexts/                      # React Contexts
│   │   └── 📂 utils/                         # Utility Functions
│   ├── package.json
│   └── vite.config.ts
│
├── 📂 Database/                              # SQL Scripts
├── 📂 Documentation/                         # Project Docs
└── 📂 Tests/                                 # Test Projects
```

---

## 🔌 API Endpoints

### Authentication

| Method | Endpoint             | Description       | Access |
| ------ | -------------------- | ----------------- | ------ |
| POST   | `/api/auth/register` | User registration | Public |
| POST   | `/api/auth/login`    | User login        | Public |

*(More endpoints documented in Swagger)*

---














=======================
TurfMate/
┣ TurfMate.Api/              # ASP.NET MVC Web API backend
┃ ┣ Controllers/
┃ ┃ ┣ AuthController.cs
┃ ┃ ┣ TurfController.cs
┃ ┃ ┣ BookingController.cs
┃ ┣ Models/
┃ ┃ ┣ User.cs
┃ ┃ ┣ Turf.cs
┃ ┃ ┣ Booking.cs
┃ ┃ ┣ DTOs/                  # Add DTOs for TypeScript compatibility
┃ ┃ ┃ ┣ UserDto.cs
┃ ┃ ┃ ┣ TurfDto.cs
┃ ┃ ┃ ┣ BookingDto.cs
┃ ┃ ┃ ┣ AuthDto.cs
┃ ┣ Repositories/
┃ ┃ ┣ UserRepository.cs
┃ ┃ ┣ TurfRepository.cs
┃ ┃ ┣ BookingRepository.cs
┃ ┣ Services/
┃ ┃ ┣ AuthService.cs
┃ ┃ ┣ TurfService.cs
┃ ┃ ┣ BookingService.cs
┃ ┣ TurfMate.Api.csproj
┃ ┗ Web.config

┣ turfmate-client/           # React + TypeScript frontend
┃ ┣ src/
┃ ┃ ┣ components/
┃ ┃ ┃ ┣ auth/
┃ ┃ ┃ ┃ ┣ Login.tsx
┃ ┃ ┃ ┃ ┣ Register.tsx
┃ ┃ ┃ ┣ turf/
┃ ┃ ┃ ┃ ┣ TurfList.tsx
┃ ┃ ┃ ┃ ┣ TurfCard.tsx
┃ ┃ ┃ ┃ ┣ TurfDetails.tsx
┃ ┃ ┃ ┣ booking/
┃ ┃ ┃ ┃ ┣ TurfBooking.tsx
┃ ┃ ┃ ┃ ┣ BookingForm.tsx
┃ ┃ ┃ ┃ ┣ BookingList.tsx
┃ ┃ ┃ ┣ admin/
┃ ┃ ┃ ┃ ┣ AdminDashboard.tsx
┃ ┃ ┃ ┃ ┣ UserManagement.tsx
┃ ┃ ┃ ┃ ┣ TurfManagement.tsx
┃ ┃ ┃ ┣ common/
┃ ┃ ┃ ┃ ┣ Header.tsx
┃ ┃ ┃ ┃ ┣ Footer.tsx
┃ ┃ ┃ ┃ ┣ LoadingSpinner.tsx
┃ ┃ ┃ ┃ ┣ Modal.tsx
┃ ┃ ┣ types/
┃ ┃ ┃ ┣ user.types.ts
┃ ┃ ┃ ┣ turf.types.ts
┃ ┃ ┃ ┣ booking.types.ts
┃ ┃ ┃ ┣ api.types.ts
┃ ┃ ┣ services/
┃ ┃ ┃ ┣ api.ts
┃ ┃ ┃ ┣ auth.service.ts
┃ ┃ ┃ ┣ turf.service.ts
┃ ┃ ┃ ┣ booking.service.ts
┃ ┃ ┣ hooks/
┃ ┃ ┃ ┣ useAuth.ts
┃ ┃ ┃ ┣ useTurf.ts
┃ ┃ ┃ ┣ useBooking.ts
┃ ┃ ┣ contexts/
┃ ┃ ┃ ┣ AuthContext.tsx
┃ ┃ ┣ utils/
┃ ┃ ┃ ┣ constants.ts
┃ ┃ ┃ ┣ helpers.ts
┃ ┃ ┃ ┣ validation.ts
┃ ┃ ┣ pages/
┃ ┃ ┃ ┣ Home.tsx
┃ ┃ ┃ ┣ Dashboard.tsx
┃ ┃ ┃ ┣ Turfs.tsx
┃ ┃ ┃ ┣ Bookings.tsx
┃ ┃ ┣ styles/
┃ ┃ ┃ ┣ globals.css
┃ ┃ ┃ ┣ components.css
┃ ┃ ┣ App.tsx
┃ ┃ ┣ main.tsx
┃ ┃ ┣ vite-env.d.ts
┃ ┣ public/
┃ ┃ ┣ vite.svg
┃ ┣ package.json
┃ ┣ tsconfig.json
┃ ┣ vite.config.ts
┃ ┣ tailwind.config.js       # If using Tailwind CSS
┃ ┗ README.md

┗ README.md                 # Project documentation

==============
