FROM microsoft/dotnet:2.2-sdk AS build
LABEL maintainer="mehmetalierdin"

WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet publish --output /output --configuration Release

FROM microsoft/dotnet:2.2-sdk

COPY --from=build /output /app

WORKDIR /app  
EXPOSE 80
CMD ASPNETCORE_URLS=http://*:$PORT dotnet DotNetCoreAngularCrudDataService.dll