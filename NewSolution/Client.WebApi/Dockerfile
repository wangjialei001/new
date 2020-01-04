FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Client.WebApi/Client.WebApi.csproj", "Client.WebApi/"]
RUN dotnet restore "Client.WebApi/Client.WebApi.csproj"
COPY . .
WORKDIR "/src/Client.WebApi"
RUN dotnet build "Client.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Client.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Client.WebApi.dll"]