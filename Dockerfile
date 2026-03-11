FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /SimFlow
COPY . ./
RUN dotnet restore SimFlow/SimFlow.csproj
RUN dotnet publish SimFlow/SimFlow.csproj -o out

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /SimFlow
COPY --from=build /SimFlow/out .
# ← change the DLL name to whatever your project actually produces
ENTRYPOINT ["dotnet", "SimFlow.dll"]