FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Internel.Api/Internel.Api.csproj", "Internel.Api/"]
RUN dotnet restore "Internel.Api/Internel.Api.csproj"
COPY . .
WORKDIR "/src/Internel.Api"
RUN dotnet build "Internel.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Internel.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Internel.Api.dll"]