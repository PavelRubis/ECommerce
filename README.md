# ECommerce -- ASP.NET Core based simple e-commerce app.

### Requirements
1. Any valid Postgres credentials
2. .NET 6
3. Last LTS node.js

### Dev test setup:
1. Run Postgres container: `docker run --name postgres -e POSTGRES_PASSWORD=secret -d -p 5432:5432 -v pgdata:/var/lib/postgresql/data postgres`
2. Execute `git clone https://github.com/PavelRubis/ECommerce.git`
3. Go to solution folder `cd ./ECommerce`
4. Add migration `dotnet ef migrations add inital -s .\ECommerce.Web\ECommerce.WEB.csproj -p .\ECommerce.DAL\ECommerce.DAL.csproj`
5. Apply migration `dotnet ef database update -s .\ECommerce.Web\ECommerce.Web.csproj -p .\ECommerce.DAL\ECommerce.DAL.csproj`
6. Open, build and run `ECommerce.sln` in VS2022.
7. Go to frontend folder `cd ./ECommerceFront/ECommerce`
8. Instal dependencies `npm i`
9. Run frontend  `npm run dev`

Two accounts will be created during migration:
`admin admin` and `user user`
