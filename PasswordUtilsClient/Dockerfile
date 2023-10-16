FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PasswordUtilsClient/PasswordUtilsClient.csproj", "PasswordUtilsClient/"]
RUN dotnet restore "PasswordUtilsClient/PasswordUtilsClient.csproj"
COPY . .
WORKDIR "/src/PasswordUtilsClient"
RUN dotnet build "PasswordUtilsClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PasswordUtilsClient.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PasswordUtilsClient.dll"]
