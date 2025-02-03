# Plácido's Code Challenge - Real Estate Listing API

## Overview
This is my implementation of the Code Challenge.
Here I implemented as requested the following practices:
- A small "Clean Code" Architecture
- Dependency Injection
- Unit of Work
- Repository pattern
- A new DELETE endpoint
- Unit testing
- Docker file to run the code inside a container
- Changed the database from InMemory to SQLite (SQLite supports transactions)

Please note the following:
- Use `docker build` in the `Dockerfile` directory to create an image.
- Use `docker run` to run the created image.
- Use `dotnet test` to run the unit tests.
- Use `dotnet run` to execute the API.


Here is a brief resume of what the challenge asked me to.

Candidate Enhancement Tasks
---------------------------

As a candidate for the Senior C# Developer position, you are expected to expand upon the base project with the following implementations:

### Input Validations

-   **Enhance Input Validation:** Ensure all incoming data via API endpoints meet predefined formats and constraints. Implement robust validation logic to prevent invalid data submissions.

### Design Patterns

-   **Dependency Injection (DI):** Utilize DI extensively to decouple the application's dependencies. Enhance the current setup by refining service registrations and their usages throughout the application.
-   **Repository Pattern:** Implement the Repository Pattern to abstract data access logic into reusable classes. This should help isolate the data layer, making the system easier to maintain and test.
-   **Unit of Work:** Incorporate the Unit of Work pattern to manage transactional operations. This ensures that operations involving multiple repositories are done within a single transaction context.a

### CRUD Operations

-   **Implement DELETE Functionality:** Add a DELETE endpoint to allow users to remove listings from the memory. Ensure that this operation adheres to RESTful standards and includes appropriate validation and error handling to manage the integrity of the database.

### Dockerization

-   **Containerize the Application:** Dockerize the application to ensure it can run in a containerized environment. This includes creating a `Dockerfile` and possibly a `docker-compose.yml` if necessary, to define how the application should be built and run in Docker.
