set /p Input=Nome do migration:

dotnet tool install -g dotnet-ef

dotnet ef migrations add "%Input%" -s "..\..\FastFood.WebApi\FastFood.WebApi.csproj" -p "src\FastFood.Catalogo.Infrastructure\FastFood.Catalogo.Infrastructure.csproj" -c "CatalogoDbContext"

pause