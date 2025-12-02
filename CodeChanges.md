# Code Changes

# Test strategy
Normally the first thing you should do is write a set of tests that capture the behaviour of the existing logic to ensure that any new code changes that are made do not unintentionally break current functionality. I do not think there is an appropriate amount of time to do this so will focus on improve the existing code using TDD.

# Implementation
### Options Pattern
Introduced the Options pattern to select the `DataStoreType` instead of reading directly from configuration each time.
This reduces repeated I/O access which can be expensive when repeated.

### AccountDataStore
Introduce `AccountDataStoreProvider` to select the appropriate `IAccountDataStore` based on the `DataStoreType` selected from the options. If this was production code this would use traditonal DI to select the data store on startup. This is intentionally named a provider rather than a factory because it supplies a resolved instance rather than constructing a new one on each request modelling the DI behaviour.

### AccountManager
Extracted account-related domain logic (e.g. debiting an account) into a dedicated AccountManager.
This isolates domain behaviour from orchestration logic and improves testability and readability.

### PaymentSchemeStrategy
Abstract the payment scheme strategies out of the switch statement and use the `IPaymentSchemeStrategyFactory` to select the appropriate implementation at runtime. This fixes the issue with the open closed principal. Each scheme’s business rules now live in an isolated, testable class.
Implemented with a cache to optimise performance as all the concrete versions are stateless singletons.
Note: I have not implemented the busines logic tests for each strategy due to time constraints

### PaymentService
Abstract the many responsibilities into their own classes to fix the issue with the single responsibility principal and significantly improves structure, readability and maintainability. This class now acts as an orchestrator and delegates specific business logic to each dependency.

Null Account Handling perfomed earlier before validation and can be removed from each payment scheme strategy.

