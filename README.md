# E-Commerce Application

## Overview

This is an **E-commerce Application** built using **ASP.NET Core 8** following the **Clean Architecture** pattern. The application supports core e-commerce functionalities like product browsing, shopping cart management, user authentication, order placement, and more. It is designed to be modular, scalable, and maintainable with a separation of concerns that promotes clean and testable code.

## Features

- **User Authentication & Authorization**: Secure user registration, login, and role-based authorization using **JWT** tokens.
- **Product Management**: Display a catalog of products, with functionality for searching and filtering.
- **Shopping Cart**: Add, update, and remove items from the cart.
- **RESTful APIs**: Expose RESTful services for front-end or mobile apps.
- **Unit Testing**: Write tests to ensure the application works as expected.

## Technologies

- **.NET 8**
- **MS SQL Server**
- **Docker Compose**
- **Azurite (Azure Blob Storage)**

## Architecture

This project uses **Clean Architecture** to organize the code in a modular and scalable way.

## How to Start the Project

Follow these steps to clone the project, start the required services, and access the API documentation via Swagger.

### Prerequisites

- **Docker** and **Docker Compose**
- **Git**

### Steps

1. **Clone the repository**:

   First, clone the project from the Git repository:

   ```bash
   git clone https://github.com/JakubMalinowski378/E-commerce.git
   cd E-commerce
2. **Run Docker Compose**:
   ```bash
   docker-compose up
3. **Access Swagger UI**:
   Once the containers are up and running, the API documentation can be accessed through Swagger UI at the following URL: https://localhost:63517/swagger/index.html Use this interface to explore and test the API endpoints.
