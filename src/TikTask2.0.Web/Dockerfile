FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TikTask2.0.Web/TikTask2.0.Web.csproj", "TikTask2.0.Web/"]
RUN dotnet restore "TikTask2.0.Web/TikTask2.0.Web.csproj"
COPY src/TikTask2.0.Web/ TikTask2.0.Web/
WORKDIR "/src/TikTask2.0.Web"
RUN dotnet build "TikTask2.0.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TikTask2.0.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TikTask2.0.Web.dll"]
