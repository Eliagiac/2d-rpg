using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public static bool isMenuOpen = false;

    public int menusAmm;
    public GameObject[] menus;

    void Update()
    {
        if (InventoryHandler.isInventoryOpen || MagicComputerHandler.isCraftingUIOpen || QuestsButtonHandler.isQuestsUIOpen)
        {
            isMenuOpen = true;
        }

        else isMenuOpen = false;
    }
}
