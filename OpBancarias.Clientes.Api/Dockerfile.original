#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OpBancarias.Clientes.Api/OpBancarias.Clientes.Api.csproj", "OpBancarias.Clientes.Api/"]
RUN dotnet restore "OpBancarias.Clientes.Api/OpBancarias.Clientes.Api.csproj"
COPY . .
WORKDIR "/src/OpBancarias.Clientes.Api"
RUN dotnet build "OpBancarias.Clientes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpBancarias.Clientes.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpBancarias.Clientes.Api.dll"]