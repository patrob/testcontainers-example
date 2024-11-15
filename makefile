apiProjectPath=src/TestcontainersExample.Web/TestcontainersExample.Web.csproj
dataProjectPath=src/TestcontainersExample.Data/TestcontainersExample.Data.csproj

up:
	docker compose up -d

down:
	docker compose down

add_migration:
	dotnet ef migrations add "$(name)" --context "ApplicationDbContext" --startup-project $(apiProjectPath) --project $(dataProjectPath)

migrate:
	export ASPNETCORE_ENVIRONMENT=Local																							&& \
	dotnet ef database update --context "ApplicationDbContext" --startup-project $(apiProjectPath) --project $(dataProjectPath)
