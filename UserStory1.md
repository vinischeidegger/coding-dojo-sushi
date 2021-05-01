# User Story 1

To keep an eye on the costs, you want to develop a small app which calculates the total price depending on the chosen plates. **A user interface is NOT necessary. Parsing inputs is also NOT necessary. Use static inputs.**<br><br>Calculate the price of the chosen plates.
<br><br>

**Example:**
<br><br>

|                                                  |            |
|--------------------------------------------------|------------|
|5 x Blue                                          | = 4.75 Fr. |
|5 x Grey                                          | = 24.75 Fr.|
|1 x Grey, 1 x Green, 1 x Yellow, 1 x Red, 1 x Blue| = 14.75 Fr.|

<br><br>

## Interpretation

The base story stipulates that:
Plate prices are based on colors - the prices are stipulated in the PDF. Following the [Single Responsibility Principle](https://en.wikipedia.org/wiki/Single-responsibility_principle), we cannot have price calculation AND price stipulation in a single class, therefore there is a predefined need for a service that provide prices per plate color.

The solution here was to create the Class *PlatePriceService*, responsible to provide the prices as shown in the Requirements.
Using TDD, the class was created based on the tests needed for the story to work.

When the Price provider was ready it was the time to build the Calculator service.
The calculator service was also built using TDD. No user interface was built.

The solution was created following MVC pattern, although only the Model (*Restaurant.Domain*) and Controller (*Restaurant.BillCalculator.Application*) projects were created.

The inputs for the calculation are *Plate* classes (from Domain). During the tests we defined a set of static plates (one for each color), that were reused accross all tests.