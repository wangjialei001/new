FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY . /app
WORKDIR /app

ENV ASPNETCORE_URLS=http://*:8070
EXPOSE 8070
ENV LANG=zh_CN.UTF-8
ENV LANGUAGE=zh_CN.UTF-8
ENV TZ Asia/Shanghai
ENTRYPOINT ["dotnet", "Log.Api.dll"]
