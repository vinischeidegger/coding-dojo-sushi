# User Story 2

As you can see, this can become very expensive. Furtunately there is a lunch menu. The menu is 8.50 Fr.<br>
It includes a soup and four plates. A soup is 2.50 Fr.<br>
The lunch menu is only from Monday to Friday between (including) 11:00 a.m. and (ecluding) 5:00 p.m.<br>
For the calculation the time of payment is used.<br>

Calculate the end price:

**Example:**
<br><br>

|                                        |             |
|----------------------------------------|-------------|
|1 x Soup, 2 x Grey, 2 x Green, 2 x Blue | = 10.40 Fr. |
|1 x Soup, 2 x Grey, 3 x Green, 2 x Red  | = 16.35 Fr. |
|1 x Soup, 2 x Grey, 3 x Green, 2 x Red  | = 28.15 Fr. |

<br><br>

## Interpretation

The system should have different calculation strategies, one for menu calculations and another for the regular prices.
There should be some analysis on whether the payment date of the week and time is within a certain range.
Ther should also be some analysis of the chosen plates to group them in suitable menu options.

Open Questions:
1) The User story says that the lunch includes a soup and four plates. Are soups considered to be plates? If a user asks for 5 soups should them be grouped into a single menu?
    Assumption #1 - Soups are not considered plates. A menu is consisted of a soup + 4 sushi plates for this user story.
2) How should the remaining plates be grouped into the menus? Grouping menus in a different order can generate different prices.
    Assumption #2 - Plates should be grouped searching optimal prices (minimum payment values)
3) Can we have menus with a soup and less than 4 plates? Yes. If the menu is cheaper than the regular values, we can have 1 soup + N plates, where N is up to 4.
   Assumption #3 - Although a menu includes a soup and four plates, the client can group a soup and N<=4 sushi plates. In a practical way if the restaurant wouldn't give the discount the client could request the extra plates and throw them away just to pay less, which does not make sense.
4) Using the time of payment could probably be unpratical - securing an indefinite amount of money and calculate the transaction value at the exact same time can be complex - for the simplicity of the exercise we have named the method that calculate the value to be paid as PayBill and wwe are setting up the date time inside the method.
    Assumption #4 - payment time is a time defined within the PayBill method execution.
5)  In order to facilitate testing a IClock interface was defined, allowing time travel
    Assumption #5 - IClock should be injected with DateTime.Now