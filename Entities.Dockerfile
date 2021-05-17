FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY src/Thinktecture.Samples.Configuration/Thinktecture.Samples.Configuration.csproj ./Thinktecture.Samples.Configuration/
COPY src/Thinktecture.Samples.Entities/Thinktecture.Samples.Entities.csproj ./Thinktecture.Samples.Entities/

RUN dotnet restore "./Thinktecture.Samples.Entities/Thinktecture.Samples.Entities.csproj"
COPY ./src/Thinktecture.Samples.Configuration/ ./Thinktecture.Samples.Configuration/
COPY ./src/Thinktecture.Samples.Entities/ ./Thinktecture.Samples.Entities/

WORKDIR "/src/Thinktecture.Samples.Entities"
RUN dotnet build "Thinktecture.Samples.Entities.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Thinktecture.Samples.Entities.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Thinktecture.Samples.Entities.dll"]
