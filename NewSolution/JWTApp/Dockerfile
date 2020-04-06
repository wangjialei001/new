FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["JWTApp/JWTApp.csproj", "JWTApp/"]
RUN dotnet restore "JWTApp/JWTApp.csproj"
COPY . .
WORKDIR "/src/JWTApp"
RUN dotnet build "JWTApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JWTApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JWTApp.dll"]