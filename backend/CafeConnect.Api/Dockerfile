# Use the official .NET 6 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj files and restore dependencies
COPY ["backend/CafeConnect.Api/CafeConnect.Api.csproj", "backend/CafeConnect.Api/"]
COPY ["backend/CafeConnect.Domain/CafeConnect.Domain.csproj", "backend/CafeConnect.Domain/"]
COPY ["backend/CafeConnect.Application/CafeConnect.Application.csproj", "backend/CafeConnect.Application/"]
COPY ["backend/CafeConnect.Infrastructure/CafeConnect.Infrastructure.csproj", "backend/CafeConnect.Infrastructure/"]

RUN dotnet restore "backend/CafeConnect.Api/CafeConnect.Api.csproj"

# Copy the rest of the solution files
COPY . .

# Build the application
RUN dotnet publish "backend/CafeConnect.Api/CafeConnect.Api.csproj" -c Release -o /app/out

# Use the official .NET 6 runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /app/out .

# Expose the port on which the app will run
EXPOSE 8080

# Set the entry point to run the app
ENTRYPOINT ["dotnet", "CafeConnect.Api.dll"]