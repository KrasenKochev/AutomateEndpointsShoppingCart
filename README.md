# Shopping Cart API Test Suite

This is a small project written in **C#** that tests the functionality of a shopping cart API using **MSTest**. The base project provides a few core endpoints, and two new endpoints were implemented and tested as part of this task. Also part of the task is to not modify ShoppingCart, and the existing endpoints.

## Project Structure

- **Language:** C#
- **Test Framework:** MSTest
- **IDE Used:** Visual Studio Code (VS Code)

### Folder and Class Overview

- **Helpers/**  
  Contains helper classes for interacting with the API and performing common validations:

  - `CartHelper.cs` — Contains methods to add items, remove items, get cart contents, and complete orders.
  - `ApiResponseHelper.cs` — Provides reusable assertions and methods to parse and validate HTTP responses.

- **Models/**  
  Data transfer objects (DTOs) representing key entities like `CartItemDto` and `StoreItemDto`.

- **Constants/**  
  Defines constants such as API URLs and endpoint paths used throughout the tests (`Urls.cs`).

- **TestData/**  
  Includes predefined test data to use during testing (e.g., known store items).

- **TestScenarios/**  
  Contains scenario tests that cover end-to-end workflows of the shopping cart API, such as adding items, removing them, and completing orders.
  ToDo: expand the scenarious
- **BaseTest.cs**  
  Base class that sets up the HTTP client and common test initialization/cleanup logic.

## Existing Endpoints

These endpoints were already provided and tested:

- `GET /store/items` — Returns a list of available items in the store.
- `POST /cart/add` — Adds a specific item to the cart.
- `POST /order/complete` — Completes the order by clearing all items in the cart.

## Added Endpoints

Two new endpoints were added and tested as part of this project:

- `GET /cart/items` — Returns a list of items currently in the cart.
- `POST /cart/remove` — Removes a specific item from the cart.

## Running the Tests

You can also start listening on: http://localhost:4080 so that you can test the endpoints using api calls and test the program ( with Postman for instance),
by typing in the terminal:

dotnet run --urls http://localhost:4080

To run all tests:

1. Open a terminal in the `ShoppingCartTests` directory.
2. Run the following command:

````bash
dotnet test

To run tests by categories:

```bash
dotnet test --filter TestCategory=Basic
dotnet test --filter TestCategory=Scenario

To generate a simple report:

```bash
dotnet test --logger "trx"

ToDo:

-Refactor helper classes and test setup to improve encapsulation and abstraction. Unify the overall style of tests, assertions and methods used.
 Also to consolidate the helper functions and models to use strings/int. Check on the access modifiers, if needed.
-Expand scenario tests to cover more edge cases and user interactions like removing partial quantities, using negative quantities, volume/performance, and other.
-Implement more convinient report viewing/generating tool


````
