# Stage 1: Build the API application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the API project file and restore dependencies
COPY ["CoffeMachineAPI/CoffeMachineAPI.csproj", "CoffeMachineAPI/"]
COPY ["CoffeeMachineModel/CoffeeMachineModel.shproj", "CoffeeMachineModel/"]
RUN dotnet restore "CoffeMachineAPI/CoffeMachineAPI.csproj"

# Copy the remaining source code and build
COPY CoffeMachineAPI/ ./CoffeMachineAPI/
COPY CoffeeMachineModel/ ./CoffeeMachineModel/
WORKDIR "/src/CoffeMachineAPI"
RUN dotnet publish "CoffeMachineAPI.csproj" -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "CoffeMachineAPI.dll"]
