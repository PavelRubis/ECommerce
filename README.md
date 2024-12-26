# ECommerce -- ASP.NET Core based simple e-commerce app.

### Requirements
1. Any valid Postgres credentials
2. .NET 6
3. Last LTS node.js

### Testing setup
For backend:
1. Run Postgres container: `docker run --name postgres -e POSTGRES_PASSWORD=secret -d -p 5432:5432 -v pgdata:/var/lib/postgresql/data postgres`
2. Debug ECommerce.sln in VS 2022

For frontend:
1.  `cd ./ECommerce/ECommerceFront/ECommerce`
2.  `npm i`
3.   `npm run dev`
