FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0 AS build 
WORKDIR webapp

EXPOSE 80
EXPOSE 5024

COPY ./*.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o ITLCarreras

FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0
WORkDIR /webapp

COPY --from=build /webapp/ITLCarreras .
ENTRYPOINT = ["dotnet", "CarrerasITL.json"]