ARG DOTNET_VERSION=10.0

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-alpine AS build
WORKDIR /src

COPY HelloWorld.sln ./
COPY HelloWorld/HelloWorld/HelloWorld.csproj ./HelloWorld/
RUN dotnet restore HelloWorld.sln
RUN ls -la ./HelloWorld/

COPY . .

RUN dotnet build HelloWorld.sln -c Release -o /app/build --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HelloWorld.dll"]
