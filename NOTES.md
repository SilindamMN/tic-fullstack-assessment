# NOTES.md

 1. Time Spent

The following is my estimated time breakdown for the assessment.

| Part | Time Spent |
|---|---:|
| Part 1: SQL queries, database setup and index | 1 hour |
| Part 2: Backend API and business rules | 2 hours |
| Part 3: Vue 3 and TypeScript frontend | 1 hour 30 minutes |
| Part 4: Support documentation and QA tests | 1 hour |
| README.md and NOTES.md | 30 minutes |
| Total assessment time | 6 hours |

The assessment work and environment setup are recorded separately, as requested in the brief.

 2. Tools and AI Assistance

- Visual Studio: Backend development and debugging.
- Visual Studio Code: Frontend development and documentation.
- .NET 10 / ASP.NET Core Web API: Backend development.
- Vue 3 and TypeScript: Frontend development.
- Axios: Sending HTTP requests from the frontend to the backend API.
- SQL Server: Database storage and query testing.
- ASP.NET Monster: Hosting the remote SQL Server database.
- Git and GitHub: Version control and source code management.
- xUnit: Automated backend testing.
- ChatGPT: Assistance with frontend implementation, setting up the unit test project structure, troubleshooting issues, and preparing documentation.

AI assistance was used as a development aid. I reviewed and adapted the suggestions to fit the requirements of the assessment.

 3. Assumptions

- The supplied schema and seed data represent the initial database state.
- Existing redemption point deductions are already reflected in participant balances.
- A participant cannot create another redemption for the same reward while an existing redemption is Pending.
- New redemption requests use UTC timestamps.
- Failed redemptions should refund the participant's points according to the specified business rules.
- The API should return clear validation messages without exposing internal exception details.

 4. Technical Decisions

# Backend and database

The backend uses ASP.NET Core Web API with C# and Entity Framework Core to communicate with SQL Server.

I used the supplied database schema and seed data to create the database and did not use EF Core migrations. The application maps to the existing database tables.

I used a layered project structure to separate responsibilities and make the code easier to maintain and test.

# Database index

I added an index on [enter the actual table and column or columns] to improve the performance of relevant queries. The index is intended to help when retrieving or filtering redemption records.

# Frontend

The frontend uses Vue 3 and TypeScript. Axios is used to communicate with the backend API. The interface includes the redemption list, filters, and a form for creating new redemptions.

# Testing and logging

I used xUnit for automated backend tests covering redemption business rules. Logging is used to help investigate application activity and errors.

 5. Unfinished Work and Possible Improvements

With more time, I would:

- Expand automated tests to cover all validation rules and API endpoints.
- Add integration tests against a test database.
- Improve monitoring and investigation of redemptions that remain Pending.
- Add authentication and authorisation if required by the application's future needs.
- Improve handling and monitoring of external provider failures.
- Consider introducing more Domain-Driven Design practices if the application's business logic becomes more complex.

Authentication was not required by the assessment brief and was not implemented.

 6. Challenges and Uncertainties

Configuring the remote SQL Server connection, connecting the Vue frontend to the API, and setting up the automated test project required troubleshooting.

I also needed to ensure that the API followed the required validation order and returned the appropriate HTTP status codes and clear error messages.

I used the supplied requirements and seed data to guide my implementation and documented assumptions where necessary.
