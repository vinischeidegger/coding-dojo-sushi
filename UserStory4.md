# User Story 4

You could attach your co-workers to sushi. The next time you are in the sushi restaurant you decide to share lunch menus to optimize the total price. Now you must capture the plates from multiple persons.<br>
- The app should be able to work with orders/plates from multiple persons, without requiring the user to add plates before manually;
- Mix the plates of the participating guests to get the available menus.
- Calculate the optimized price by mergining the orders to menus
<br>

**Example 1:**
Paid Wednesday, 13:45:
- Person A: 1 x Soup, 2 x Gray, 2 x Green, 2 x Red,  1 x Blue = 13.35 Fr.
- Person B: 2 x Gray, 2 x Green, 2 x Yellow, 2 x Red = 15.35 Fr.
- Person C: 2 x Soup, 2 x Gray, 2 x Green, 3 x Yellow, 2 x Red = 18.95 Fr.
<br>Optimized price: 36.85 Fr.<br>Saved: 10.80 Fr.<br>

**Example 2:**
Paid Wednesday, 13:45:
- Person A: 1 x Soup, 2 x Gray, 2 x Green, 1 x Red,  2 x Blue = 12.35 Fr.
- Person B: 2 x Gray, 2 x Green, 2 x Red, 2 x Blue = 12.35 Fr.
- Person C: 2 x Soup, 2 x Green, 3 x Yellow, 2 x Red, 2 x Blue = 17.95 Fr.
<br>Optimized price: 42.65 Fr.<br>Saved: 0.00 Fr.<br>

## Interpretation

In this User Story we need to make the system multi-user, we are going to do that by adding an outer layer to the calculation services and also an order repository that will be able to map orders to users.
We then need to calculate the value for each person and contrast it with the value for all persons withing a single party.
As the concept of party or table is not mentioned in the requirements I'm leaving it aside. My understanding is that each program execution will handle a party or a table of friends.
After creating the code some of the values were not reacheable.

The differences are below:

Example 1:
- Person B: 2 x Gray, 2 x Green, 2 x Yellow, 2 x Red = 16.35 Fr. (Accorfing to requirements from US2 a red or blue plate is needed)
<br>Optmized price: 45.40 Fr.<br>Saved: 3.25 Fr.<br>

Example 2:
<br>Optmized price: 41.65 Fr.<br>Saved: 1.00 Fr.<br>



