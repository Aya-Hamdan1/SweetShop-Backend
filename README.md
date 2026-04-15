# SweetShop-Backend
Backend API for a dessert shop system built with ASP.NET Core. Supports product management, categories, and order processing with clean architecture practices.


## 🚀 Features

* Product management (Create, Update, Delete, Get)
* Category management
* Order system with multiple products per order
* Order total price calculation
* Dashboard (basic statistics like total orders and total sales) 
* Clean structure using Controllers and Services
* Input validation and DTOs

## 🛠️ Technologies

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server

## 📦 API Endpoints (Examples)

### Products

* GET /api/Product
* POST /api/Product
* PUT /api/Product/{ProductId}
* DELETE /api/Product/{id}

### Orders

* POST /api/Order
* GET /api/Order
* GET /api/Order/{id}

### Category

* POST /api/Category
* GET /api/Category

### Dashboard

* GET /api/Dashboard


## 🧠 Notes

* Authentication is not implemented in this version.
* This project focuses on backend logic and API design.

## ▶️ How to Run

1. Clone the repository
2. Update connection string in appsettings.json
3. Run migrations
4. Start the project

---

✨ This project was built as part of backend practice and portfolio development.

