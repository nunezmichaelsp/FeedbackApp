# Feedback Application

This project manages Feedback with Customer Name, Category and Description, for registered users.

## Getting Started

This project will be an update of the services related to the management of Feedbacks, using .NET core and MVC for the UI.
https://github.com/nunezmichaelsp/CustomerFeedbackManagment

### Prerequisites

* [Git](https://git-scm.com/downloads)
* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* .Net Core (Check the current version for this project [here](./Directory.Build.props))
* [SQL Server Developer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [SQL Sever Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
* [NVM (Node Version Manager)](https://github.com/coreybutler/nvm-windows)

### Installing

Clone repository and open `.sln` file in Visual Studio 2022. Build Solution.

#### Setting up SQL Server

1. Download SQL Server Developer if you haven't.
2. After installing, write down your server name (Such as `localhost` or `MSSQLSERVER`).

#### `secrets.json` structure

```
{
"ConnectionStrings": {
    "DefaultConnection": "{sql-server-connection-string}"
  }
}
```
All of the keys presented in the `secrets.json` are not available in the `appsettings.json` or `appsettings.Development.json` due to security reasons.

## Running the project

### Through Visual Studio

1. Clone the repository from GitHub with the previously provided link.
2. Include User Secrets with the Sql server connection string to you your local server.
3. Inside Visual studio, go to Package Manager Console, Select the Proyect FeedbackApp.Data as Default project.
4. Run the command: Update-Database -StartupProject FeedbackApp
5. Select Debug and HTTP config.
6. Set `FeedbackApp.Web` as Starting project.
7. Press F5 to start debugging (Press Ctrl+F5 to run without debugging).

### Coding conventions

The conventions used are determined by the Sonar rules in the [`Directory.Build.props`](./Directory.Build.props) and the [`.editorconfig`](./.editorconfig) file. Additionally StyleCop analyzer was included as a support tool in the development environment.

### Versioning Standards

Given a version number MAJOR.MINOR.PATCH, increment the:

MAJOR version when you make incompatible API changes,
MINOR version when you add functionality in a backwards compatible manner, and
PATCH version when you make backwards compatible bug fixes.

Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.

### Branching Standards

To keep the code organized and related to the changes that are requested via JIRA, we will adhere to the Git-Flow workflow definition for branches.

We will have the following branches:

Master: This is the production branch and will always have the latest stable code ready to be deployed to production.

Develop: This is the development branch and will always have the latest code (stable or not) and will contain all the code for the next release and is where all feature branches will target.

Release: These are the branches where the UAT (User Acceptance Tests) testing is performed and they are created based on the latest from the develop branch. The code here is frozen from adding new feature code and only bugfixes will be merged to this branch. These branches are short-lived and are deleted as soon as the UAT process is complete and all the features are accepted and all bugs found are also resolved; once the validation process is complete, the branch is merged both to develop and master and the commit to master is tagged with the version of the release.

Hotfix: This branch is meant only to be created when a critical bug is found in production and an urgent patch is required, otherwise the bug should be reported as a code improvement ticket to be planned and resolved in a future release (depending on the urgency). These branches are also short-lived and behave the same way as release branches with the exception that instead of being created based on the develop branch they are initiated from the master branch.

Feature: These branches are always created from the develop branch and are always merged to the develop branch. These branches are only meant to be created when a new feature is being developed even when fixing a bug that has been identified before the UAT process started.

Bugfix: These branches are only meant to be created when bugs are found in the UAT process and should always be started either from a hotfix branch or a release branch.

## Architecture Standards

All enterprise applications will be implemented using a layered architecture focusing specifically in the following technologies:

1. Domain Driven Design.
2. Command Query Responsibility Segregation Pattern (if needed).
3. Layered Architecture implementing at least Domain, Data, Crosscutting, Application and API/UI layers, with same name projects for testing proyects with .Test postfix.
4. All our components must be built using an Ubiquitous language, this practice will enable all developers and business to speak in the “language”.
5. Readability must be always be followed when naming our entities, variables, services, repositories, controllers, API contracts and other components of our application.
6. Clean code and descriptive names are prefered over redundant and confussing comments. A comment in code must be justified.
7. Logging sink for all environments must be created. Serilog was used for better loggin mecanism and traces analisys.
8. CI/CD configuration, YAML, Environment Variables (if needed), Cloud Sercets are part of the software development proccess.
9. Microservices architecture is recommended for enterprise systems.
10. Unit testing with xUnit and Moq, and Integration Tests to cover 80% of the code for better maintainability.

## Authors

Michael Nunez as EXSQ Outcoding assesment.

## License

This project  is licensed under the MIT License - see the (LICENSE.md) file for details.
