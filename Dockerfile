FROM  mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
# Create an /app directory as a root folder
WORKDIR /app 
LABEL Renan Rosa

# copy csproj and restore as distinct layers
COPY ./ekklesia/ekklesia.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY . ./
#Build the project and then exporti to ./web/out 
RUN dotnet publish "./ekklesia/ekklesia.csproj" -c Release -o out 

#Generate runtime image. We just need the runtime.
FROM  mcr.microsoft.com/dotnet/core/sdk:2.2 
WORKDIR /app
COPY --from=build-env /app/ekklesia/out .
EXPOSE 80
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ekklesia.dll


#docker build -t ekklesia:1.0 .       
# Tag the heroku target image
#docker tag ekklesia:1.0 registry.heroku.com/ekklesia-app/web
# Push the docker image to heroku
#heroku container:push web -a ekklesia-app
# Release the container on heroku
#heroku container:release web -a ekklesia-app