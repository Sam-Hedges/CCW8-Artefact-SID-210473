# CCW8-Artefact-SID-210473
My Uni submission for the below brief

## Brief
You are tasked to develop a short console program that allows the user to shop for some items!

### Overview
You will need to develop several classes that hold the functionality for buying (and potentially selling) items for currency:

#### Item class:
- Name of the object (string)
- Description of the object (string)
- Value of the item (float)

#### Shop class:
- List of sellable items
- Function that initialises all (at least 5) items with their data (name, value and description)
- Function that presents the sellable items in the console for the player to buy from
- Basket that holds items that the player chose but not yet bought
- Function that adds the price of all items in basket and checks if the players has enough money
if yes then the money is paid and items can be transferred to the player
if no the player should be presented with the information (missing money and how much) and allowed to reset the basket to start over

#### Player class:
- Money that is used to buy items (float)
- List of bought items
- Function to show stored items
- The above functionality is the requirement for this assignment will result in an A (7/10).

#### To go beyond try implementing the following:
- The ability to do multiple buy runs with persistent player inventory states
- If starting money is 100 and first buy used 20 then the second time the player will only have 80
- The ability to edit basket anytime by removing objects one by one
- Ability to sell items back to the shop at a reduced rate
- Items are now limited and have a stack system
- If 0 stack it cannot be bought again (potentially remove from list) sold items need to be re-added to shop stack
