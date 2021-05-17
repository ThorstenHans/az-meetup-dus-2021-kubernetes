FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY src/Thinktecture.Samples.Configuration/Thinktecture.Samples.Configuration.csproj ./Thinktecture.Samples.Configuration/
COPY src/Thinktecture.Samples.Entities/Thinktecture.Samples.Entities.csproj ./Thinktecture.Samples.Entities/
COPY src/Thinktecture.Samples.Jobs.Cleanup/Thinktecture.Samples.Jobs.Cleanup.csproj ./Thinktecture.Samples.Jobs.Cleanup/

RUN dotnet restore "./Thinktecture.Samples.Jobs.Cleanup/Thinktecture.Samples.Jobs.Cleanup.csproj"
COPY ./src/Thinktecture.Samples.Configuration/ ./Thinktecture.Samples.Configuration/
COPY ./src/Thinktecture.Samples.Entities/ ./Thinktecture.Samples.Entities/
COPY ./src/Thinktecture.Samples.Jobs.Cleanup/ ./Thinktecture.Samples.Jobs.Cleanup/

WORKDIR "/src/Thinktecture.Samples.Jobs.Cleanup"
RUN dotnet build "Thinktecture.Samples.Jobs.Cleanup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Thinktecture.Samples.Jobs.Cleanup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Thinktecture.Samples.Jobs.Cleanup.dll"]
