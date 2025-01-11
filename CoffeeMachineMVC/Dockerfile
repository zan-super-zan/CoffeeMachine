# Stage 1: Build the MVC application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the MVC project file and restore dependencies
COPY ["CoffeeMachineMVC/CoffeeMachineMVC.csproj", "CoffeeMachineMVC/"]
COPY ["CoffeeMachineModel/CoffeeMachineModel.shproj", "CoffeeMachineModel/"]
RUN dotnet restore "CoffeeMachineMVC/CoffeeMachineMVC.csproj"

# Copy the remaining source code and build
COPY CoffeeMachineMVC/ ./CoffeeMachineMVC/
COPY CoffeeMachineModel/ ./CoffeeMachineModel/
WORKDIR "/src/CoffeeMachineMVC"
RUN dotnet publish "CoffeeMachineMVC.csproj" -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "CoffeeMachineMVC.dll"]
