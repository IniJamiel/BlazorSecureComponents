FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY FEBlazorDemo.csproj .
RUN dotnet restore FEBlazorDemo.csproj
COPY . .
RUN dotnet --version
RUN dotnet build FEBlazorDemo.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish FEBlazorDemo.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf