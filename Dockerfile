FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FastFood.WebApi/FastFood.WebApi.csproj", "src/FastFood.WebApi/"]
RUN dotnet restore "src/FastFood.WebApi/FastFood.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FastFood.WebApi"
RUN dotnet build "FastFood.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FastFood.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastFood.WebApi.dll"]
