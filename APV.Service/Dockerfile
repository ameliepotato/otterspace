#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APV.Service/APV.Service.csproj", "APV.Service/"]
RUN dotnet restore "APV.Service/APV.Service.csproj"
COPY . .
WORKDIR "/src/APV.Service"
RUN dotnet build "APV.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APV.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APV.Service.dll"]