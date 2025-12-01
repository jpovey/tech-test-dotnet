# Code review
- The first and most obvious problem is that the PaymentService violates the Single Responsiblity Principal. A class should have one responsibility and only one reason to change but this one is responsible for many including validation, persistance and applying domain logic. This can make it difficult to understand and maintain.

- The switch statment breaks the open closed principal. To add new payment schemes means the class needs to be modified. A strategy pattern using interfaces can help to resolve this.

- The class is tigtly coupled to data access which makes it hard to test in isolation. If this was a real implementation it would force the tests to rely on a real database which would be expensive to run. The lack of an abstraction also makes testing difficult as it cant be mocked.

- Its hard to test in general due to the lack of interfaces and large amount of busines logic. Ties back to SRP

- Domain logic is performed within the class mixing concerns with persistance.

- Lack of defensive coding. What if an account is not found? or a transaction fails?

- Settings are not injected

- There are zero tests to start with