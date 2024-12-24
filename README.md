# ECommerce -- ASP.NET Core based simple e-commerce app.

[Postman collection](https://www.postman.com/prubis/prubis/collection/14376166-e6d953b8-65d4-4422-942d-9ba1c346c12a/?action=share&creator=14376166) for backend testing.

### Requirements
1. Any valid Postgres credentials
2. .NET 6

### Testing setup 
1. Run Postgres container: `docker run --name postgres -e POSTGRES_PASSWORD=secret -d -p 5432:5432 -v pgdata:/var/lib/postgresql/data postgres`
2. Debug ECommerce.sln in VS 2022
