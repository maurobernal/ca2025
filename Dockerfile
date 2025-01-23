#---------------------------------------------------
#   ProjectName: GAG.Api_Operations
#   Author: Mauro Bernal
#   Date: 2023/10/03 14:00
#   Contacts: mbernal@globalassistgroup.com
#  docker build -t  ca:api .
#
#---------------------------------------------------
#https://learn.microsoft.com/en-us/dotnet/core/compatibility/containers/8.0/aspnet-port
#now default port is 8080
#################################################################################################
FROM maurobernal/net9-base AS base
USER root
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_HTTP_PORTS=80;8080


##################################################################################################
FROM maurobernal/net9-build AS build

##Pass Solution
WORKDIR /src/
COPY . .

##Build
WORKDIR /src/
#ENTRYPOINT ["tail", "-f", "/dev/null"]
RUN dotnet build src/Web/Web.csproj -c Release -o /app/build

###########################################################################################
FROM build AS publish
##Dependecy
USER root
RUN dotnet publish src/Web/Web.csproj -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt-get install -y libfontconfig1
ENTRYPOINT ["dotnet", "ca.Web.dll", "--environment=Production"]