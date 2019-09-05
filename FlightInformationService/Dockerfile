FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["FlightInformationService/FlightInformationService.csproj", "FlightInformationService/"]
RUN dotnet restore "FlightInformationService/FlightInformationService.csproj"
COPY . .
WORKDIR "/src/FlightInformationService"
RUN dotnet build "FlightInformationService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FlightInformationService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FlightInformationService.dll"]