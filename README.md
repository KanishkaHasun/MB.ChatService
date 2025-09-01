# MB.ChatService
Prerequisites 
1) Redis
2) MS SQL Server
3) .NET 8 or later

Techniques / Patterns / Architecture 
1) Clean Architecture with background worker service.
2) CQRS with MediatR pattern for request/response handling.
3) Repository Pattern with Entity Framework for SQL data handling
4) Middleware for global exception handling
5) Dependency Injection for object creation

Testing Instructions 
1) Run both the API and BackgroundWorker projects.
2) Redis is configured for local setup (can be connected to a cloud service if needed).
3) Seed data is provided for testing purposes.
4) First, call the Create Chat API. To poll, call the Get-Status API, which also returns the chat status.

Notes 
1) Branches were created based on features implemented.
2) By passing a user ID to the Create Chat API, multiple chats can be created for a single user.
3) Both user ID and username are optional, allowing users to create anonymous chats.
