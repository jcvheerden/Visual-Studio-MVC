FROM mcr.microsoft.com/dotnet/aspnet:10.0-nanoserver-ltsc2022 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:10.0-nanoserver-ltsc2022 AS build
ARG configuration=Release
WORKDIR /src
COPY ["prjHealthCareSystem/prjHealthCareSystem.csproj", "prjHealthCareSystem/"]
RUN dotnet restore "prjHealthCareSystem\prjHealthCareSystem.csproj"
COPY . .
WORKDIR "/src/prjHealthCareSystem"
RUN dotnet build "prjHealthCareSystem.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "prjHealthCareSystem.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "prjHealthCareSystem.dll"]
