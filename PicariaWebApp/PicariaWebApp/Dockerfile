FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["PicariaWebApp/PicariaWebApp.csproj", "PicariaWebApp/"]
RUN dotnet restore "PicariaWebApp/PicariaWebApp.csproj"
COPY . .
WORKDIR "/src/PicariaWebApp"
RUN dotnet build "PicariaWebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PicariaWebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PicariaWebApp.dll"]