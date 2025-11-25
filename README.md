# 🍔 Talabat E-Commerce Simulation API  
A modular, scalable Web API project inspired by Talabat’s e-commerce system.  
Built with **ASP.NET Core**, applying clean architectural principles to ensure maintainability, testability, and performance.

---

## 🚀 **Project Overview**
This project simulates key operations of an online food/e-commerce platform, including product management, basket operations, order processing, and user authentication.

The system is implemented using **Onion Architecture**, enforcing a strict separation of concerns between API, Core, and Infrastructure layers.

---

## 🧱 **Core Technologies**
- **ASP.NET Core Web API**
- **Entity Framework Core (EF Core)**
- **Dapper**
- **Redis (In-Memory Data Store & Caching)**
- **LINQ**
- **JWT Authentication + Role-Based Access Control (RBAC)**
- **Onion Architecture**
- **Specification Pattern**
- **Serilog Logging**
- **MVC Admin Dashboard**

---

## 📌 **Key Features**
### ✔ Clean Architecture (Onion Architecture)
- API layer: Controllers, DTOs, Mappings  
- Core layer: Entities, Specifications, Business Rules  
- Infrastructure: Data access, EF/Dapper repos, caching layer  

### ✔ Authentication & Security
- JWT Authentication  
- Role-Based Access Control  
- Custom Exception Handling  

### ✔ Data Access
- EF Core + Dapper Hybrid Approach  
- Repository & Unit of Work Pattern  
- LINQ for querying  
- Specification Pattern for flexible filtering & querying  

### ✔ Performance Optimizations
- Redis caching for products and common queries  
- Efficient DB operations  
- Reduced unnecessary round-trips  

### ✔ Business Modules Delivered
- **Products**  
- **Basket**  
- **Orders**  
- **Account / Authentication**

### ✔ Admin Dashboard (MVC)
A simple UI for product management:
- Create / Edit / Delete products  
- Image upload support  
- Category/brand management  

---
------------------------------
------------------------------

---

## 🔧 **Dependency Injection**
All services, repositories, caching layers, and validators are registered centrally using **AddScoped / AddTransient** DI patterns.

---

## 📦 **Installation**
1. Clone the repository:


2. Restore dependencies:


3. Update connection strings (appsettings.json)

4. Run the project:

## 👨‍💻 **Author**
**Mohamed Nasser**  
.NET Backend Developer  
LinkedIn: (https://www.linkedin.com/in/mohamed-nasser-734734224/)
GitHub:(https://github.com/xma7med) 

