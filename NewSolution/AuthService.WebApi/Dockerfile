FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["AuthService.WebApi/AuthService.WebApi.csproj", "AuthService.WebApi/"]
RUN dotnet restore "AuthService.WebApi/AuthService.WebApi.csproj"
COPY . .
WORKDIR "/src/AuthService.WebApi"
RUN dotnet build "AuthService.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AuthService.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthService.WebApi.dll"]