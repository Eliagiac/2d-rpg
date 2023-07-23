using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingHandler : MonoBehaviour
{
    public GameObject[] craftableObjects;
    public static GameObject[] _craftableObjects;

    public GameObject[] craftingIngredients1;
    public int[] craftingIngredientsAmmount1;

    Sprite[] foundIngredients;
    int[] foundValues;

    InventoryHandler inventoryHandler;

    int amm = 1;

    private void Start()
    {
        _craftableObjects = craftableObjects;

        inventoryHandler = GameObject.Find("Caveman Character").GetComponent<InventoryHandler>();

        foundIngredients = new Sprite[inventoryHandler.inventoryObjects.Length + inventoryHandler.hotbarSlots.Length];
        foundValues = new int[inventoryHandler.inventoryValues.Length + inventoryHandler.hotbarValues.Length];
    }

    public void OnCraftButton(Button button)
    {
        foundIngredients = new Sprite[inventoryHandler.inventoryObjects.Length + inventoryHandler.hotbarSlots.Length];
        foundValues = new int[inventoryHandler.inventoryValues.Length + inventoryHandler.hotbarValues.Length];

        for (int i = 0; i < craftableObjects.Length; i++)
        {
            if (craftableObjects[i].GetComponent<SpriteRenderer>().sprite == button.image.sprite)
            {
                for (int x = 0; x < inventoryHandler.inventoryObjects.Length; x++)
                {
                    for (int n = 0; n < foundIngredients.Length - inventoryHandler.hotbarSlots.Length; n++)
                    {
                        if (foundIngredients[n] == null)
                        {
                            if (inventoryHandler.inventoryObjects[x] != null)
                            {
                                foundIngredients[n] = inventoryHandler.inventoryObjects[x];
                                foundValues[n] = inventoryHandler.inventoryValues[x];

                                break;
                            }

                            else break;
                        }

                        else if (inventoryHandler.inventoryObjects[x] == foundIngredients[n])
                        {
                            foundValues[n] += inventoryHandler.inventoryValues[x];

                            break;
                        }
                    }
                }

                for (int x = 0; x < inventoryHandler.hotbarObjects.Length; x++)
                {
                    for (int n = 0; n < foundIngredients.Length; n++)
                    {
                        if (foundIngredients[n] == null)
                        {
                            if (inventoryHandler.hotbarObjects[x] != null)
                            {
                                foundIngredients[n] = inventoryHandler.hotbarObjects[x];
                                foundValues[n] = inventoryHandler.hotbarValues[x];

                                break;
                            }

                            else break;
                        }

                        else if (inventoryHandler.hotbarObjects[x] == foundIngredients[n])
                        {
                            foundValues[n] += inventoryHandler.hotbarValues[x];

                            break;
                        }
                    }
                }

                if (button.image.sprite == craftableObjects[0].GetComponent<SpriteRenderer>().sprite)
                {
                    for (int x = 0; x < craftingIngredients1.Length; x++)
                    {
                        for (int n = 0; n < foundIngredients.Length; n++)
                        {
                            if (foundIngredients[n] == craftingIngredients1[x].GetComponent<SpriteRenderer>().sprite)
                            {
                                if (foundValues[n] >= craftingIngredientsAmmount1[x])
                                {
                                    break;
                                }

                                else
                                {
                                    return;
                                }
                            }

                            if (n == foundIngredients.Length - 1)
                            {
                                return;
                            }
                        }
                    }

                    for (int x = 0; x < craftingIngredients1.Length; x++)
                    {
                        inventoryHandler.UpdateInventoryRemove(craftingIngredients1[x], craftingIngredientsAmmount1[x]);
                    }

                    Craft(0);

                    PlayerHandler.craftedObjects[0] = true;
                }

                break;
            }
        }
    }

    public void Craft(int slot)
    {
        inventoryHandler.UpdateInventory(craftableObjects[slot], false);
    }
}
