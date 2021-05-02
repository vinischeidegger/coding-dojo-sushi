# User Story 3

As not everybody wants a soup, there is the possibility to order a lunch menu without a soup:<br>
- A menu can also consist 5 plates
- At least one of the plates must be red or blue
- This menu is also available from Monday to Friday from 11 a.m. to 5 p.m.
<br>

Calculate the optimized price.

<br><br>

## Interpretation

As the hours for the menu are the same, we are basically adding a different option to form the menu. <br>
Now we also have the need to analyze, when we are not creating a soup menu, that one of the plates that compounds the menu is either red or blue.
Different than from User Story 2, apparently you cannot round up plates if your total has surpassed the price of the menu (although practically it would make sense) - i.e. you have 1 red + 3 greens (1.95 + (4.95 x 3)) = 16.80, but you didn't make to 5 plates you cannot consider yourself in the menu (although pratically you could order one more and throw it away, and would still save 8.30).

Anyway, sticking to the logic and keeping it simple, we now can consider 5 plates!