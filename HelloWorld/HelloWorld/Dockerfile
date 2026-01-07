ARG DOTNET_VERSION=10.0

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet restore "HelloWorld.csproj"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HelloWorld.dll"]