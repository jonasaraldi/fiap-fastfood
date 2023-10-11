set /p Input=Modulo:

cd src/Modules

mkdir "%Input%"
mkdir "%Input%\src"
mkdir "%Input%\test"

cd "%Input%\src"
dotnet new classlib -n "FastFood.%Input%.Endpoints" -f net7.0
dotnet new classlib -n "FastFood.%Input%.Application" -f net7.0
dotnet new classlib -n "FastFood.%Input%.Domain" -f net7.0
dotnet new classlib -n "FastFood.%Input%.Infrastructure" -f net7.0

dotnet add "FastFood.%Input%.Application/FastFood.%Input%.Application.csproj" reference "FastFood.%Input%.Domain/FastFood.%Input%.Domain.csproj"
dotnet add "FastFood.%Input%.Infrastructure/FastFood.%Input%.Infrastructure.csproj" reference "FastFood.%Input%.Domain/FastFood.%Input%.Domain.csproj"
dotnet add "FastFood.%Input%.Infrastructure/FastFood.%Input%.Infrastructure.csproj" reference "FastFood.%Input%.Application/FastFood.%Input%.Application.csproj"
dotnet add "FastFood.%Input%.Endpoints/FastFood.%Input%.Endpoints.csproj" reference "FastFood.%Input%.Application/FastFood.%Input%.Application.csproj"

dotnet add "../../../FastFood.WebApi/FastFood.WebApi.csproj" reference "FastFood.%Input%.Endpoints/FastFood.%Input%.Endpoints.csproj"

cd ..

cd "test"
dotnet new xunit -n "FastFood.%Input%.Tests" -f net7.0

dotnet add "FastFood.%Input%.Tests/FastFood.%Input%.Tests.csproj" reference "../src/FastFood.%Input%.Application/FastFood.%Input%.Application.csproj"
dotnet add "FastFood.%Input%.Tests/FastFood.%Input%.Tests.csproj" reference "../src/FastFood.%Input%.Domain/FastFood.%Input%.Domain.csproj"

pause