# E-Commerce — Food Delivery Platform

A full-stack food delivery and e-commerce web application inspired by Talabat, built to provide a convenient online ordering experience. Users can browse restaurants, explore available products, place orders, and track their orders.

## Overview

The project focuses on building a structured web application with a .NET backend and a modern frontend, emphasizing maintainable architecture, organized business logic, and a practical online ordering workflow.

## Features

* Browse restaurants and available products.
* Explore product details and manage orders.
* Place food orders through an online ordering workflow.
* Track order status.
* Organize backend logic using a layered project structure.
* Support payment integration where configured.

## Tech Stack

**Backend**

* C#
* ASP.NET Core
* Entity Framework Core

**Frontend**

* Angular
* TypeScript
* HTML and CSS

**Database**

* SQL Server

**Architecture**

* Layered architecture
* Separation of business logic and infrastructure concerns

## Project Structure

```text
E-Commerce/
├── Core/
│   └── Core business logic and domain components
├── Infrastructure/
│   └── Data access and infrastructure components
├── Shared/
│   └── Shared models and utilities
├── E-Commerce.Web/
│   └── Web application and presentation layer
└── E-Commerce.sln
```

*The descriptions above summarize the intended responsibilities of each layer; adjust them if the actual implementation differs.*

## Getting Started

### Prerequisites

* .NET SDK compatible with the project
* SQL Server
* Node.js and npm, if running the Angular frontend separately
* Angular CLI, if required by the frontend project

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/khaled-eid10/E-Commerce.git
   ```

2. Navigate to the project directory:

   ```bash
   cd E-Commerce
   ```

3. Open `E-Commerce.sln` in Visual Studio or your preferred .NET IDE.

4. Configure your database connection string in the appropriate configuration file.

5. Restore the .NET dependencies:

   ```bash
   dotnet restore
   ```

6. Apply Entity Framework Core migrations if the project uses migrations:

   ```bash
   dotnet ef database update
   ```

7. Run the backend using the appropriate startup project:

   ```bash
   dotnet run --project E-Commerce.Web
   ```

8. If the Angular frontend is maintained separately, navigate to its directory, install dependencies with `npm install`, and start it using the project's configured development script.

## Configuration

Before running the application, configure the required settings, including:

* Database connection string
* Frontend API base URL
* Payment provider credentials, if applicable

**Security:** Never commit passwords, API keys, payment credentials, or other secrets to the repository.

## Future Improvements

* Expand automated testing.
* Improve error handling and application logging.
* Enhance order tracking and order-status notifications.
* Improve deployment and production configuration.
* Strengthen application security and validation.

## Author

**Khaled Eid**

* GitHub: [khaled-eid10](https://github.com/khaled-eid10)

---

*This project was developed as a practical full-stack application to explore web development, backend architecture, database integration, and online ordering workflows.*
