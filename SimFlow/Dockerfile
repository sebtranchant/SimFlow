FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /SimFlow
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /SimFlow
COPY --from=build /SimFlow/out .
ENTRYPOINT ["dotnet", "SimFlow.dll"]