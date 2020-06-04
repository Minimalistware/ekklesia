# Ekklésia 
A small sized web app written in C# for managing church-related subjects.

## Getting Started

After cloning the repository build the project to make sure everything is okay. Then, run the command to 
create the database and the tables. By default, it will create an SQLite database. 
To change that, navigate to appsettings.json

"Update-Database"

### Prerequisites

```
netcoreapp2.2
```

## Deployment at Heroku

Inside the ekklesia project build the docker file using the following command.

"docker build -t ekklesia:1.2 ."

After that, tag the heroku target image.

"docker tag ekklesia:1.2 registry.heroku.com/ekklesia-app/web"

Push the docker image to heroku.

"heroku container:push web -a ekklesia-app"

Release the container on heroku.

"heroku container:release web -a ekklesia-app"

Change the launchSettings.json file. Replace the value of the ASPNETCORE_ENVIRONMENT property 
from Development to Production. That change will make the app use the Postgres database.

## Working demo

https://ekklesia-app.herokuapp.com/