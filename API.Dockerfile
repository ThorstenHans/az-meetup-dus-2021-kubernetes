FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY src/Thinktecture.Samples.Configuration/Thinktecture.Samples.Configuration.csproj ./Thinktecture.Samples.Configuration/
COPY src/Thinktecture.Samples.Entities/Thinktecture.Samples.Entities.csproj ./Thinktecture.Samples.Entities/
COPY src/Thinktecture.Samples.WebAPI/Thinktecture.Samples.WebAPI.csproj ./Thinktecture.Samples.WebAPI/

RUN dotnet restore "./Thinktecture.Samples.WebAPI/Thinktecture.Samples.WebAPI.csproj"
COPY ./src/Thinktecture.Samples.Configuration/ ./Thinktecture.Samples.Configuration/
COPY ./src/Thinktecture.Samples.Entities/ ./Thinktecture.Samples.Entities/
COPY ./src/Thinktecture.Samples.WebAPI/ ./Thinktecture.Samples.WebAPI/

WORKDIR "/src/Thinktecture.Samples.WebAPI"
RUN dotnet build "Thinktecture.Samples.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Thinktecture.Samples.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Thinktecture.Samples.WebAPI.dll"]
