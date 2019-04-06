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

- [ ] Create customer function.
- [ ] Edit customer function.
- [ ] Delete customer function.
- [ ] Get customers function.
- [ ] Get specific customer function.
- [ ] Add dependency injection.


### References

* *How to return required data from an insert or an update statement*

https://www.sqlservercentral.com/articles/the-output-clause-for-insert-and-delete-statements


