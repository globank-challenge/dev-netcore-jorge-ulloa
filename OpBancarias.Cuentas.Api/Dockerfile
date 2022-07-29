#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 7132

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OpBancarias.Cuentas.Api/OpBancarias.Cuentas.Api.csproj", "OpBancarias.Cuentas.Api/"]
COPY ["OpBancarias.Cuentas.Biz/OpBancarias.Cuentas.Biz.csproj", "OpBancarias.Cuentas.Biz/"]
COPY ["OpBancarias.Movimientos.Data/OpBancarias.Data.csproj", "OpBancarias.Movimientos.Data/"]
COPY ["OpBancarias.Movimientos.Biz/OpBancarias.Movimientos.Biz.csproj", "OpBancarias.Movimientos.Biz/"]
COPY ["OpBancarias.Core.Biz/OpBancarias.Core.Biz.csproj", "OpBancarias.Core.Biz/"]
COPY ["OpBancarias.Movimientos.Api/OpBancarias.Movimientos.Api.csproj", "OpBancarias.Movimientos.Api/"]
COPY ["BankingOperationsAPI/OpBancarias.Api.Core.csproj", "BankingOperationsAPI/"]
RUN dotnet restore "OpBancarias.Cuentas.Api/OpBancarias.Cuentas.Api.csproj"
COPY . .
WORKDIR "/src/OpBancarias.Cuentas.Api"
RUN dotnet build "OpBancarias.Cuentas.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpBancarias.Cuentas.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpBancarias.Cuentas.Api.dll"]