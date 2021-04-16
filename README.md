# AspNetFundamentalDemo

This is a web API project to get start with ASP.NET Core 5.0. The app performs some basic `Http` method like get, post, put and delete. Demo implementing DI (Dependency Injection) as well as IoC (Inversion of Control). xUnit testing.

It's also configured to run inside a Docker container. 

- Build the image: `docker build . -t web-api-demo`
- Create a docker network for communicating between container inside docker: `docker network create demo-net`

- Run mongodb, our api will connect to it later on: `docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=kafka -e MONGO_INITDB_ROOT_PASSWORD=123 --network=demo-net mongo`

- Run our app: `docker run -it --rm --name my-api -p 9051:80 -e MongoDbSettings:Host=mongo -e MongoDbSettings:Password=123 --network=demo-net web-api-demo`

Done! Now we can try to send some requests at `http://localhost:9051/` (See `Controllers/ChampionController.cs`) 	ㄟ( ▔, ▔ )ㄏ.
