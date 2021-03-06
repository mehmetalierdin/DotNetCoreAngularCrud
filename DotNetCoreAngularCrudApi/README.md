# DotnetCoreAngularDataService

DotnetCoreAngularDataService project is used to get data from json and creates API.

## To Run the project

Note : You can use Postman application to check the api results.

## There are 3 ways to run the API project : 
##	1 - Local run
DotnetCoreAngularDataService.sln contains three .csproj projects. These are DotnetCoreAngularDataService, DotnetCoreAngularDataService.Framework and DotnetCoreAngularDataService.Test. DotnetCoreAngularDataService is the main project that contains controllers and datas(json). DotnetCoreAngularDataService.Framework contains helper, model and service classes. DotnetCoreAngularDataService.Test is the test project which contains the tests.

DotnetCoreAngularDataService should be Startup Project and run it. The application will start over : "http://localhost:5000". The login page's url is "localhost:5000/api/login/" and http method is POST.
		
![alt text](screenshots/dotnetcoreangularcruddataservice-login.png "Login API")

After authentication is successfull, you can access another API endpoints. There are two types of Endpoints. First endpoint shows all presentations and second one shows a presentation that you mentioned in url. You have to add authorization token that you get from login endpoint to header using "Bearer keyword".

First endpoint is : "localhost:5000/api/data/" -> HTTPGET

![alt text](screenshots/dotnetcoreangularcruddataservice-allpresentations.png "List All Presentations")

Second endpoint is : localhost:5000/api/data/incididunt sint eiusmod labore -> HTTPGET

![alt text](screenshots/dotnetcoreangularcruddataservice-selectedpresentation.png "List selected presentation")

##	2 - From Docker
DotnetCoreAngularDataService project is dockerized so you can access the endpoints over docker. To run the project over docker you have to follow this steps : 
	- Open CMD and path the project's location. Use the command to build the project : "docker build -t dotnetcoreangularcruddataservice.dll ." This command is going to create image and container.
			
![alt text](screenshots/dotnetcoreangularcruddataservice-docker-build.png "Docker Build")
			
After this step is completed, the image id will be generated by docker,

![alt text](screenshots/dotnetcoreangularcruddataservice-docker-imageid.png "Docker Build")
			
Then use the command to run the project : "docker run -p 1881:80 a9d[docker image id]" 
			
![alt text](screenshots/dotnetcoreangularcruddataservice-docker-run.png "Docker Run")
			
You can access the API endpoints over docker now :

![alt text](screenshots/dotnetcoreangularcruddataservice-run-from-docker.png "Dockerized")
			
##	3 - From Heroku
DotnetCoreAngularDataService project also contains Heroku integration. To run project from heroku, you have to run this commands after "docker build" step (mentioned above) :

	- docker tag dotnetcoreangularcruddataservice.dll registry.heroku.com/dotnetcoreangulardataservice/web
	- docker push registry.heroku.com/dotnetcoreangulardataservice/web
	- heroku container:release web --app dotnetcoreangulardataservice

Then you can access the endpoints from https://dotnetcoreangulardataservice.herokuapp.com/

![alt text](screenshots/dotnetcoreangularcruddataservice-herokudeploy.png "Heroku Deploy")				
![alt text](screenshots/dotnetcoreangularcruddataservice-heroku.png "Heroku Deploy")

## Test Project 

Coded using XUnit

![alt text](screenshots/dotnetcoreangularcruddataservice-testresult.png "Test Results")


