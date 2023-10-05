set /p Input=Nome do migration:

dotnet tool install -g dotnet-ef

dotnet ef migrations add "%Input%" -s "src\FastFood.WebApi\FastFood.WebApi.csproj" -p "src\Modules\Atendimento\src\FastFood.Atendimento.Infrastructure\FastFood.Atendimento.Infrastructure.csproj" -c "AtendimentoDbContext"

pause