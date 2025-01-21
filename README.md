# EventHub

## Overview

**EventHub** is an API for working with events.

## Startup instructions

- **Step 0**: Make sure you have .NET 8, PostgreSQL installed
- **Step 1**: Clone repository:
```bash
git clone https://github.com/Vladislav8653/EventHub
```
- **Step 2**: Find `Presentation/appsettings.json` and configure field **sqlConnection** in **ConnectionStrings** option. This is connection string to your database.
- **Step 3**: Create Migrations via terminal from project root:
```bash
dotnet ef migrations add mg1 --project Infrastructure --startup-project Presentation
```
- **Step 4**: Update database via terminal from project root:
```bash
dotnet ef database update --startup-project Presentation
```
- **Step 5**: Enjoy😋
