FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./ASP NET CORE API.csproj", "src/"]
RUN dotnet restore "src/ASP NET CORE API.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ASP NET CORE API.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "ASP NET CORE API.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ASP NET CORE API.dll"]