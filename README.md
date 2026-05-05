# SweetShop-Backend
Backend API for a dessert shop system built with ASP.NET Core. Supports product management, categories, and order processing with clean architecture practices.


## 🚀 Features
* Product management (CRUD)
* Category management
* Order system with multiple items
* Order total price calculation
* User authentication (JWT)
* Role-based authorization (Admin/User)
* Pagination and filtering
* Dashboard with statistics (orders, revenue)
* Image upload support
* Clean architecture using Controllers & Services
* DTOs and input validation
* Global exception handling (middleware)

## 🛠️ Technologies

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger (OpenAPI)
  
## 📦 API Endpoints (Examples)

### Account
* POST /api/Account/register
* POST /api/Account/login
* PUT /api/Account/users/{id}/role (Admin only)
### Products

* GET /api/Product
* POST /api/Product
* PUT /api/Product/{ProductId}
* DELETE /api/Product/{id}

### Orders

* POST /api/Order
* GET /api/Order (Admin only)
* GET /api/Order/MyOrders
* GET /api/Order/{id}
* PUT /api/Order/{id}/status (Admin only)

### Category

* POST /api/Category
* GET /api/Category

### Dashboard

* GET /api/Dashboard

## 🔐 Authentication & Authorization

* JWT-based authentication
* Role-based authorization (Admin / User)
* Protected endpoints using [Authorize]
  
## 🏗️ Architecture
* Layered architecture (Controllers, Services, Data)
* Separation of concerns
* Dependency Injection

## ▶️ How to Run

1. Clone the repository
2. Update connection string in appsettings.json
3. Run migrations
4. Start the project

## 🧠 Notes

* Authentication and Authorization implemented using JWT
* Role-based access control (Admin/User)
* Focused on scalable backend design and clean architecture

✨ This project was built as part of backend practice and portfolio development.

