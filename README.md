# 🚀 E-commerce Web API & Admin Dashboard
### ASP.NET Core 8 | Onion & Clean Architecture

This repository contains the backend services and administrative dashboard for a full-featured **E-commerce application**, built using **ASP.NET Core 8** and strictly adhering to **Onion / Clean Architecture** principles.

The project simulates a **production-ready system**, with a strong focus on **scalability, security, performance, and maintainability**.

> ⚠️ **Note:** The client-side Angular application is **not included** in this repository.  
> This repository focuses on the **Web API** and **Admin Dashboard** only.

---

## 📝 Project Overview

The solution implements core e-commerce functionalities, integrates external services such as **Stripe** and **Redis**, and manages all administrative operations (**Products, Users, Roles**) through a dedicated **ASP.NET Core MVC Admin Dashboard**.

---

## ⭐ Key Features

### 🧠 Architecture & Design Patterns
- **Onion & Clean Architecture** for strict separation of concerns  
  *(Domain, Application/Services, Persistence, Presentation)*  
- **Repository Pattern, Unit of Work, and Specification Pattern**  
- Dependency Injection for modularity and scalability  
- **AutoMapper** for clean DTO ↔ Entity mapping  

---

### 🛍 Product & Catalog Management
- Full **CRUD** operations for **Products, Brands, and Types**  
- Advanced querying using **Specification Pattern**  
  *(Filtering, Sorting, Searching, Pagination)*  
- Secure image upload & management with validation  
  *(via PictureSettings helper)*  

---

### 🧺 Basket & Caching
- Shopping Basket module implemented using **Redis**  
- Dedicated **Caching Service** to improve API performance  

---

### 💳 Payment Integration (Stripe)
- Integrated **Stripe Payment Intent API**  
- **Stripe Webhooks** for asynchronous payment updates  
- Automatic order status handling  
  *(PaymentReceived / PaymentFailed)*  

---

### 📦 Order Management
- Snapshot of product data at order time  
- Delivery methods & shipping cost calculation  

---

### 🔐 Authentication & Authorization
- **JWT Authentication** with **ASP.NET Core Identity**  
- Role-Based Access Control (RBAC)  

---

### 📊 Admin Dashboard (ASP.NET Core MVC)
- Secure MVC Admin Dashboard  
- Manage **Users, Roles, Products, Brands, and Types**  
- CRUD operations with validation  
- UI built using **Bootstrap** and partial views  

---

## 🛠 Tech Stack

| Category | Technology |
|--------|------------|
| Backend | ASP.NET Core Web API, ASP.NET Core MVC |
| Database | Entity Framework Core, SQL Server |
| Caching | Redis |
| Authentication | ASP.NET Core Identity, JWT |
| Payments | Stripe API |
| Mapping | AutoMapper |

---

## ⚙️ Getting Started

### Prerequisites
- .NET SDK 8.0+
- SQL Server
- Redis Server
- Stripe API Keys (Test mode)

---

### Setup Instructions

#### 1️⃣ Clone the repository
```bash
git clone https://github.com/Aya-Waheed2050/ECommerce.Web.git

2️⃣ Update Configuration

Update connection strings in appsettings.Development.json

Configure Stripe Secret Key and Webhook Secret

Configure Redis connection

3️⃣ Apply Database Migrations
dotnet ef database update --project Infrastructure

4️⃣ Run the Applications

Run Web API

dotnet run --project E_Commerce


Run Admin Dashboard

dotnet run --project AdminDashBoard

🎯 Why This Project?

This project demonstrates the ability to:

Build scalable backend systems

Apply enterprise-level architecture patterns

Integrate Stripe & Redis

Implement a secure Admin Dashboard

Follow clean code best practices

🔗 Repository

Backend & Admin Dashboard:
https://github.com/Aya-Waheed2050/ECommerce.Web
