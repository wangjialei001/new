FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
VOLUME ["systemDir:/app/logs/api.logs","/Logs"]

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Log.Api/Log.Api.csproj", "Log.Api/"]
COPY ["New.Model/New.Model.csproj", "New.Model/"]
COPY ["New.Common/New.Common.csproj", "New.Common/"]
RUN dotnet restore "Log.Api/Log.Api.csproj"
COPY . .
WORKDIR "/src/Log.Api"
RUN dotnet build "Log.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Log.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Log.Api.dll"]