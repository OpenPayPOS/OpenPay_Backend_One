# Development-focused Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Install OpenSSL for certificate generation
RUN apt-get update && apt-get install -y openssl

# Copy the certificate to a known location
RUN mkdir -p /app/certificates
COPY ["Startup/certificates/aspnetapp.pfx", "app/certificates"]

# Set environment variables
ENV DB_HOST=db
ENV DB_PORT=5432
ENV DB_USER=postgres
ENV DB_PASSWORD=postgres
ENV DB_NAME=openpay_db
ENV FILE_UPLOAD_PATH=/uploads
ENV CERTIFICATE_PASSWORD="P()12neujj"

# Set working directory to the Startup project
WORKDIR /app/Startup

# Copy only project files first to leverage layer caching
COPY ["Startup/Startup.csproj", "."]
COPY ["Api/Api.csproj", "../Api/"]
COPY ["Interfaces.Services/Interfaces.Services.csproj", "../Interfaces.Services/"]
COPY ["Interfaces.Common/Interfaces.Common.csproj", "../Interfaces.Common/"]
COPY ["Data/Data.csproj", "../Data/"]
COPY ["Interfaces.Data/Interfaces.Data.csproj", "../Interfaces.Data/"]
COPY ["Services/Services.csproj", "../Services/"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Ensure the working directory is correct for the Startup project
WORKDIR /app/Startup
