using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicComputerHandler : MonoBehaviour
{
    public GameObject craftingUI;

    public static bool isCraftingUIOpen = false;

    public GameObject[] craftingSlotBases;
    public GameObject[] craftingSlotTexts;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenOrCloseCraftingUI(false);

            for (int i = 0; i < craftingSlotBases.Length; i++)
            {
                if (craftingSlotBases[i].activeSelf)
                {
                    CraftingSlotHandler.ToggleCraftingIngredientsText(craftingSlotBases[i], craftingSlotTexts[i], false);
                }
            }
        }
    } //Close crafting UI

    private void OnMouseDown()
    {
        if (!MenuHandler.isMenuOpen)
        {
            OpenOrCloseCraftingUI(true);
        }
    } //Open crafting UI

    void OpenOrCloseCraftingUI(bool option)
    {
        craftingUI.SetActive(option);

        isCraftingUIOpen = option;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            Destroy(collision.gameObject);
        }
    } //Destroy trees on collision
}
