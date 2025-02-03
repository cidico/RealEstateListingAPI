FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5236

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RealEstateListingApi.csproj", "./"]
RUN dotnet restore "RealEstateListingApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "RealEstateListingApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RealEstateListingApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish --self-contained true -r linux-musl-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./RealEstateListingApi"]