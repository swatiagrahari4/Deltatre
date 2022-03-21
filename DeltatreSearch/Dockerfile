#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5001
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DeltatreSearch/DeltatreSearch.csproj", "DeltatreSearch/"]
RUN dotnet restore "DeltatreSearch/DeltatreSearch.csproj"
COPY . .
WORKDIR "/src/DeltatreSearch"
RUN dotnet build "DeltatreSearch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeltatreSearch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeltatreSearch.dll"]