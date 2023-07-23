using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingSlotHandler : MonoBehaviour
{
    public void OnPointerEnter(Transform slot)
    {
        ToggleCraftingIngredientsText(slot.GetChild(0).gameObject, slot.GetChild(1).gameObject, true);
    }

    public void OnPointerExit(Transform slot)
    {
        ToggleCraftingIngredientsText(slot.GetChild(0).gameObject, slot.GetChild(1).gameObject, false);
    }

    public static void ToggleCraftingIngredientsText(GameObject craftingIngredientsBase, GameObject craftingIngredientsText, bool active = true)
    {
        craftingIngredientsBase.SetActive(active);
        craftingIngredientsText.SetActive(active);
    }
}
