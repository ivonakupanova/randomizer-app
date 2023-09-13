#syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

#Copy csproj and restore as distinct layers
COPY  Randomizer-sample/*.csproj ./
RUN dotnet restore

#Copy everything else and build
COPY . ./Randomizer-sample

WORKDIR /app/Randomizer-sample
RUN dotnet restore
RUN dotnet publish  -c Release -o /app/out

WORKDIR /app/Randomizer-sample
#Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Randomizer-sample.dll"]