#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OpBancarias.Movimientos.Api/OpBancarias.Movimientos.Api.csproj", "OpBancarias.Movimientos.Api/"]
RUN dotnet restore "OpBancarias.Movimientos.Api/OpBancarias.Movimientos.Api.csproj"
COPY . .
WORKDIR "/src/OpBancarias.Movimientos.Api"
RUN dotnet build "OpBancarias.Movimientos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpBancarias.Movimientos.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpBancarias.Movimientos.Api.dll"]