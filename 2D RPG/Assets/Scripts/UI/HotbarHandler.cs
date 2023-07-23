using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarHandler : MonoBehaviour
{
    public GameObject hotbarUI;

    BuildingSystem buildingSystem;
    InventoryHandler inventoryHandler;

    #region Buildable Objects References
    public Sprite belt;
    #endregion

    private void Start()
    {
        buildingSystem = GameObject.FindGameObjectWithTag("Building&DestroyingSystem").GetComponent<BuildingSystem>();
        inventoryHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (InventoryHandler.isHotbarOn)
            {
                hotbarUI.SetActive(false);

                InventoryHandler.isHotbarOn = false;
            }

            else if (!InventoryHandler.isHotbarOn)
            {
                hotbarUI.SetActive(true);

                InventoryHandler.isHotbarOn = true;
            }
        }

        if (MenuHandler.isMenuOpen)
        {
            hotbarUI.SetActive(false);
        }

        else if (!MenuHandler.isMenuOpen)
        {
            hotbarUI.SetActive(InventoryHandler.isHotbarOn);
        }

        if (hotbarUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (inventoryHandler.hotbarSlots[0].image.sprite == belt)
                {
                    buildingSystem.ToggleBeltPreview(inventoryHandler.hotbarTexts[0].text == "" ? 1 : int.Parse(inventoryHandler.hotbarTexts[0].text));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (inventoryHandler.hotbarSlots[1].image.sprite == belt)
                {
                    buildingSystem.ToggleBeltPreview(inventoryHandler.hotbarTexts[1].text == "" ? 1 : int.Parse(inventoryHandler.hotbarTexts[1].text));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (inventoryHandler.hotbarSlots[2].image.sprite == belt)
                {
                    buildingSystem.ToggleBeltPreview(inventoryHandler.hotbarTexts[2].text == "" ? 1 : int.Parse(inventoryHandler.hotbarTexts[2].text));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (inventoryHandler.hotbarSlots[3].image.sprite == belt)
                {
                    buildingSystem.ToggleBeltPreview(inventoryHandler.hotbarTexts[3].text == "" ? 1 : int.Parse(inventoryHandler.hotbarTexts[3].text));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (inventoryHandler.hotbarSlots[4].image.sprite == belt)
                {
                    buildingSystem.ToggleBeltPreview(inventoryHandler.hotbarTexts[4].text == "" ? 1 : int.Parse(inventoryHandler.hotbarTexts[4].text));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                if (inventoryHandler.hotbarSlots[5].image.sprite == belt)
                {
                    buildingSystem.ToggleBeltPreview(inventoryHandler.hotbarTexts[5].text == "" ? 1 : int.Parse(inventoryHandler.hotbarTexts[5].text));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                if (inventoryHandler.hotbarSlots[6].image.sprite == belt)
                {
                    buildingSystem.ToggleBeltPreview(inventoryHandler.hotbarTexts[6].text == "" ? 1 : int.Parse(inventoryHandler.hotbarTexts[6].text));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                if (inventoryHandler.hotbarSlots[7].image.sprite == belt)
                {
                    buildingSystem.ToggleBeltPreview(inventoryHandler.hotbarTexts[7].text == "" ? 1 : int.Parse(inventoryHandler.hotbarTexts[7].text));
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                if (inventoryHandler.hotbarSlots[8].image.sprite == belt)
                {
                    buildingSystem.ToggleBeltPreview(inventoryHandler.hotbarTexts[8].text == "" ? 1 : int.Parse(inventoryHandler.hotbarTexts[8].text));
                }
            }
        }
    }
}
