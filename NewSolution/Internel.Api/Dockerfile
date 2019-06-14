FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY . /app
WORKDIR /app

ENV ASPNETCORE_URLS=http://*:8090
EXPOSE 8090
ENV LANG=zh_CN.UTF-8
ENV LANGUAGE=zh_CN.UTF-8

ENTRYPOINT ["dotnet", "Internel.Api.dll"]
