#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

RUN apt-get update && apt-get install -y unzip
ADD http://aww001-0m0176.jp.ykgw.net/content/files/NW/Internet/ZscalerRootCertificate-2048-SHA256.zip /tmp
Run unzip /tmp/ZscalerRootCertificate-2048-SHA256.zip -d /usr/local/share/ca-certificates/
RUN chmod 644 /usr/local/share/ca-certificates/ZscalerRootCertificate-2048-SHA256.crt && update-ca-certificates

COPY ["TodoList/TodoList.csproj", "TodoList/"]
RUN dotnet restore "TodoList/TodoList.csproj"
COPY . .
WORKDIR "/src/TodoList"
RUN dotnet build "TodoList.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoList.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoList.dll"]
