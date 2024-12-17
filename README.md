# Tech Shop

**Tech Shop** is a robust eCommerce platform designed to manage products, orders, categories, and users with distinct admin and customer functionalities. The system leverages modern technologies, including .NET MVC and Entity Framework, alongside design patterns for maintainability, security, and scalability.

## Table of Contents
- [Project Overview](#project-overview)
- [Video](#video)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [User Roles](#user-roles)
- [System Design](#system-design)
- [Installation](#installation)


## Project Overview

PhoneTech is an advanced eCommerce system built with **ASP.NET Core MVC**, **Entity Framework Core**, and **ASP.NET Identity**. The project is built using a Code-First approach, incorporating best practices like the Repository Pattern and Dependency Injection for a clean and maintainable architecture.

## Demo Video
[Click here to watch the demo video](https://youtu.be/LIz7pvUHpk8)


## Features

### Admin Role
- **Product Management:** Create, update, delete, and manage product details (e.g., name, price, stock, images, categories).
- **Order Management:** Track and update the status of all orders, and cancel orders if necessary.
- **Category Management:** Create, update, and delete product categories.

### Customer Role
- **Product Browsing & Shopping:** Browse products by category, search for products, add them to the cart, and proceed to checkout.
- **Cart Management:** Update product quantities and remove items.
- **Order History & Tracking:** Track order status and view past orders.
- **Product Ratings:** Rate and review products after delivery.
- **Authentication:** Sign up, log in, and manage personal accounts.

### Other Features
- **Advanced Search & Filters:** Search products by name and category, with filter options for easy browsing.
- **Responsive Design:** Front-end layout optimized for mobile and tablet devices using CSS and Flexbox.
- **Pagination & Alerts:** Paginated product listings and order history, with alert notifications for important actions.

## Technologies Used
- **Back-end:** ASP.NET Core MVC, Entity Framework Core, ASP.NET Identity
- **Front-end:** HTML, CSS, JavaScript
- **Database:** SQL Server (via Entity Framework Code First)
- **Design Patterns:** MVC Architecture, Repository Pattern, Dependency Injection

## User Roles
- **Admin:** Has full control over products, categories, and orders.
- **Customer:** Can browse products, manage cart, place orders, and review products.

## System Design
The system follows the **Model-View-Controller (MVC)** architecture:
- **Model:** Represents the data (entities such as Product, Order).
- **View:** Handles the presentation layer (HTML, CSS).
- **Controller:** Manages request handling and business logic.

The platform uses **Code-First Migrations** for database management, ensuring seamless updates across environments. **Data Annotations** are applied for validation to maintain data integrity.

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/phonetech-shop.git
    ```
2. Navigate to the project directory:
    ```bash
    cd phonetech-shop
    ```

3. Update the database:
    ```bash
    database-update
    ```
