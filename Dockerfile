FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-Sender

#COPY
WORKDIR /app
COPY . .

#Restore
RUN dotnet restore AzurePractice.RequestSender.sln

#Build dotnet
WORKDIR /app/AzurePractice.RequestSender
RUN dotnet build AzurePractice.RequestSender.csproj -c Release --no-restore
#End Build dotnet

#Pulbish
RUN dotnet publish AzurePractice.RequestSender.csproj -c Release -o /app/out --no-build --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 80

COPY --from=build-Sender /app/out .

ENTRYPOINT ["dotnet", "AzurePractice.RequestSender.dll"]
