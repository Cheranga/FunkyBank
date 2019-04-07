# Notes

## Phase 1 - Adding the solution and the project structure

- [x] Create a blank solution.
- [x] Create the solution folders (API, Services, Domain and Infrastructure)
- [x] Create the Azure functions project in the **API** solution folder.
- [x] Create a class library project `FuncyBank.Services` in the **Services** solution folder.
- [x] Create a class library project `FuncyBank.DataAccess.Core` in the **Domain** solution folder.
- [x] Create a class library project `FuncyBank.DataAccess.Dapper` in the **Infrastructure** solution folder.
- [x] Add the solution to GIT source control.
- [x] Create a repository in GitHub `FuncyCustomers` and push the changes to it.

---

## Phase 2 - Adding the business entities, abstractions and implementing the services

Now we'll add the domain classes which we'll be using. Because we are focusing on Customers in this project,
obviously we'll be adding a class called `Customer`. This will contain the data which the business will be storing or will have business rules against it.

- [x] Create the `Customer` class.
- [x] Create the `ICustomerRepository` interface and define the operations.
- [x] Define the configuration object which will include the database connection string
- [x] Create the `ICustomerService` interface in the `FunkyBank.Services` project and define the supported operations.
- [x] Implement the `ICustomerService`.

---

## Phase 3 - Add the DbUp project to create and migrate the database

- [x] Create a console application.
- [x] Install Dapper.
- [x] Write the scripts to create the `Customers` table.
- [x] Run it manually and verify the database and the table are created.

---

## Phase 4 - Add Azure functions

- [x] Create customer function.
- [x] Edit customer function.
- [x] Delete customer function.
- [x] Get customers function.
- [x] Get specific customer function.
- [x] Add dependency injection.


### References

* *How to return required data from an insert or an update statement*

https://www.sqlservercentral.com/articles/the-output-clause-for-insert-and-delete-statements

* *When migrating using DbUp when specifying the connection string, I found out that unless you provide an SQL server authenticated login it will error out with allow remote connections blah blah error*
  * So created another new login in SQL Server (https://www.ibm.com/support/knowledgecenter/SSHLNR_8.1.4/com.ibm.pm.doc/install/sql_config_agent_grant_permission_sqlserver.htm)
  * Used a connection string with that user name and password when passing the connection string to the DbUp console.

Once the database was created/migrated in your application you can use the normal localdbconnection, without specifying any user name/password details

* *When you need to pass command line arguments certain values need to be escaped*

https://ss64.com/nt/syntax-esc.html

* *Although you import the source code from GitHub to Azure Repo and, if you are pushing the changes to GitHub itself, the CI pipeline must be pointing to the GitHub repo NOT to the Azure repo*

* *Linking AKV secrets in your Azure Devops pipeline*

https://docs.microsoft.com/en-us/azure/devops/pipelines/library/variable-groups?view=azure-devops&tabs=designer#link-secrets-from-an-azure-key-vault
