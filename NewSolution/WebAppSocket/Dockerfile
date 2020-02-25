FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["WebAppSocket/WebAppSocket.csproj", "WebAppSocket/"]
RUN dotnet restore "WebAppSocket/WebAppSocket.csproj"
COPY . .
WORKDIR "/src/WebAppSocket"
RUN dotnet build "WebAppSocket.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebAppSocket.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebAppSocket.dll"]