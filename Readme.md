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

- [ ] Create the `Customer` class.
- [ ] Create the `ICustomerRepository` interface and define the operations.
- [ ] Define the configuration object which will include the database connection string
- [ ] Create the `ICustomerService` interface in the `FunkyBank.Services` project and define the supported operations.
- [ ] Implement the `ICustomerService`.

