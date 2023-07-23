using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsButtonHandler : MonoBehaviour
{
    public Image questsUI;

    public static bool isQuestsUIOpen;

    public GameObject inventory;
    public GameObject craftingUI;

    public void OpenOrCloseQuestsUI()
    {
        questsUI.gameObject.SetActive(!isQuestsUIOpen);

        isQuestsUIOpen = !isQuestsUIOpen;

        #region Only 1 menu is open per time
        if (InventoryHandler.isInventoryOpen)
        {
            inventory.SetActive(false);
            InventoryHandler.isInventoryOpen = false;
        }

        if (MagicComputerHandler.isCraftingUIOpen)
        {
            craftingUI.SetActive(false);
            MagicComputerHandler.isCraftingUIOpen = false;
        }
        #endregion
    }
}
