Motoshop is ASP.NET MVC application which performs CRUD operations.
Also the application is separated in four layers:
- Data Layer: here are the models from which are created tables (DbSet).
- Application Service: Here are DTO objects and the logic performing crud operations on the models.
- Web API: Calls the CRUD operations from Application Service.
- MVC (front end): Calls the Web API and shows the retrieved data in the front end.
