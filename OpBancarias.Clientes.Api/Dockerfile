#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 7095

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OpBancarias.Clientes.Api/OpBancarias.Clientes.Api.csproj", "OpBancarias.Clientes.Api/"]
COPY ["BankingOperationsAPI/OpBancarias.Api.Core.csproj", "BankingOperationsAPI/"]
COPY ["OpBancarias.Movimientos.Data/OpBancarias.Data.csproj", "OpBancarias.Movimientos.Data/"]
COPY ["OpBancarias.Core.Biz/OpBancarias.Core.Biz.csproj", "OpBancarias.Core.Biz/"]
COPY ["OpBancarias.Cuentas.Api/OpBancarias.Cuentas.Api.csproj", "OpBancarias.Cuentas.Api/"]
COPY ["OpBancarias.Cuentas.Biz/OpBancarias.Cuentas.Biz.csproj", "OpBancarias.Cuentas.Biz/"]
COPY ["OpBancarias.Movimientos.Biz/OpBancarias.Movimientos.Biz.csproj", "OpBancarias.Movimientos.Biz/"]
COPY ["OpBancarias.Movimientos.Api/OpBancarias.Movimientos.Api.csproj", "OpBancarias.Movimientos.Api/"]
COPY ["OpBancarias.Clientes.Biz/OpBancarias.Clientes.Biz.csproj", "OpBancarias.Clientes.Biz/"]
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