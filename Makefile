# example: make add-migration name=Initial
add-migration:
	dotnet ef migrations add --project ./src/Persistence/Persistence.csproj --startup-project ./src/WebApi/WebApi.csproj --context Persistence.AppDbContext --configuration Debug --verbose $(name) --output-dir Migrations

# example: make apply-migrations
apply-migrations:
	dotnet ef database update --project ./src/Persistence/Persistence.csproj --startup-project ./src/WebApi/WebApi.csproj --context Persistence.AppDbContext --configuration Debug --verbose
