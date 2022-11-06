#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Invoice.Services.API/Invoice.Services.API.csproj", "Invoice.Services.API/"]
RUN dotnet restore "Invoice.Services.API/Invoice.Services.API.csproj"
COPY . .
WORKDIR "/src/Invoice.Services.API"
RUN dotnet build "Invoice.Services.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Invoice.Services.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Invoice.Services.API.dll"]