# Offline Messaging Back-End
A RESTFul back-end made with ASP .NET Framework with MVC (Model View Controller) to allow users to message offline. Eg. __gmail__

# How It Works
Users can register, login, send messages, read previous messages, block each other.
* Users can register, login and after these steps, they can obtain a bearer token to be validated by the back-end to process their request.
* Users can message each other as long as they know each other's ID.
* Messages contain the text of the message and the sent date.
* Users can access their previous messages concerning either a specific user or all messages categorized by userID and sorted by sentDate for each category. 
* If user A blocks user B, messages sent by user B to user A is not readable by user A after blocking has been made.


# Back-End Features
* Is RESTFul (JSON format in requests, responses).
* Users have individual accounts for authorization that can register, login, get a bearer token with __Identity__ library.
* Requests are protected with a bearer token, a bearer token must be sent in the header for the backend to process the request. Tokens are obtained from __/token__ endpoint with user credentials.
* Documentation is generated dynamically for the back-end when the project is run with __HelpPage__ library.
* Logs the internal errors in the backend such as invalid requests due to primary/foreign keys to a log database.
* Uses local MSSQL databases for data storage, access, modification along with rules such as primary/foreign keys. `SQLToCreateExtraDatabases`: Folder contains the .sql files to create additional databases needed.
* Controllers are open to dependency injection, therefore, the structure is testable. Testing is made with __XUnit__ in the `OfflineMessagingAPI.Tests` project folder.

# Project Details
Technologies involved:
* __ASP .NET Framework with MVC (Model View Controller)__
* __Identity Framework:__ Individual User Accounts and Authorization.
* __Entity Framework:__ Converts databases to a MVC model.
* __HelpPage:__ Endpoint Documentation. 
* __MSSQL:__ Data Storage, Access, Modification, Rules As Primary/Foreign Keys.
* __XUnit:__ Testing.
* __Made With Microsoft Visual Studio 2019__

# Files
* `SQLToCreateExtraDatabases`: Folder contains the .sql files and demonstration on how to create extra databases needed to get the system working.
* `OfflineMessagingAPI.Tests`: Project folder for Unit testing with XUnit. Controllers associated with API endpoints are written concerning dependency injection. Therefore, Mocking is/(can be) used for testing. 

# Demo
I made a PowerPoint presentation demonstrating API calls with the postman. Each endpoint is tested and requests with responses are shown. [Powerpoint Here.](APITestsWithPostman.pptx)