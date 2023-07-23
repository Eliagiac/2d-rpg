using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryHandler : MonoBehaviour
{
    #region Generic Variables
    public GameObject inventory;

    public static bool isInventoryOpen;

    public Button[] slots;
    public TextMeshProUGUI[] texts;

    public GameObject hotbarUI;

    int firstAvailableSlot;

    GameObject item;

    Sprite itemSprite;

    int numberOfItems;

    public Image currentItemHeld;
    public TextMeshProUGUI currentItemHeldText;

    Sprite tempSprite;
    string tempText;

    Sprite tempSpriteHotbar;
    string tempTextHotbar = "";

    Color no = new Color(0, 0, 0, 0);
    Color yes = new Color(1, 1, 1, 1);

    Button tempSlotButton;
    TextMeshProUGUI tempSlotText;

    int tempValue = 0;

    public Sprite[] inventoryObjects;
    public int[] inventoryValues;

    public Sprite[] hotbarObjects;
    public int[] hotbarValues;

    int tempAmm;
    #endregion

    #region Hotbar Variables
    public static bool isHotbarOn = true;

    public Button[] hotbarSlots;
    public TextMeshProUGUI[] hotbarTexts;

    int overedButton;
    #endregion

    private void Start()
    {
        inventoryObjects = new Sprite[slots.Length];
        inventoryValues = new int[slots.Length];

        hotbarObjects = new Sprite[hotbarSlots.Length];
        hotbarValues = new int[hotbarSlots.Length];
    }

    void Update()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].text == "0")
            {
                texts[i].text = "";
            }
        }

        for (int i = 0; i < hotbarTexts.Length; i++)
        {
            if (hotbarTexts[i].text == "0")
            {
                hotbarTexts[i].text = "";
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (MenuHandler.isMenuOpen == false)
            {
                inventory.SetActive(true);

                isInventoryOpen = true;
            }

            else if (MenuHandler.isMenuOpen)
            {
                if (currentItemHeld.enabled)
                {
                    if (currentItemHeldText.text != "")
                    {
                        UpdateInventory(currentItemHeld.gameObject, false, true, int.Parse(currentItemHeldText.text));
                    }

                    else UpdateInventory(currentItemHeld.gameObject, false, true, 1);
                }

                inventory.SetActive(false);

                isInventoryOpen = false;
            }
        }

        if (isInventoryOpen)
        {
            currentItemHeld.transform.position = Input.mousePosition;
        }

        if (tempSlotButton != null & tempSlotText != null)
        {
            OnSlotClick(tempSlotButton, tempSlotText);
        }

        #region UpdateHotbar()
        if (isInventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UpdateHotbar(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UpdateHotbar(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UpdateHotbar(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                UpdateHotbar(4);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                UpdateHotbar(5);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                UpdateHotbar(6);
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                UpdateHotbar(7);
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                UpdateHotbar(8);
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                UpdateHotbar(9);
            }
        }
        #endregion

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].image.color == yes)
            {
                inventoryObjects[i] = slots[i].image.sprite;
                
                if (texts[i].text == "")
                {
                    inventoryValues[i] = 1;
                }

                else
                {
                    inventoryValues[i] = int.Parse(texts[i].text);
                }
            }

            else
            {
                inventoryObjects[i] = null;
            }
        }

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarObjects[i] = hotbarSlots[i].image.sprite;

            if (hotbarSlots[i].image.color == yes)
            {
                if (hotbarTexts[i].text == "")
                {
                    hotbarValues[i] = 1;
                }

                else
                {
                    hotbarValues[i] = int.Parse(hotbarTexts[i].text);
                }
            }
        }
    } // Open inventory, Update current item held position, Call OnSlotClick() & UpdtateHotbar(), Update inventoryObjects & inventoryValues

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            item = other.gameObject;

            UpdateInventory(item);
        }
    } // Check collision with item and upddate inventory

    public void UpdateInventory(GameObject item, bool destroy = true, bool disableCurrentItemHeld = false, int amm = 1, GameObject objectToDestroy = null)
    {
        if (objectToDestroy == null)
        {
            objectToDestroy = item;
        }

        if (amm > 0)
        {
            if (item.GetComponent<SpriteRenderer>() != null)
            {
                itemSprite = item.GetComponent<SpriteRenderer>().sprite;
            }

            else if (item.GetComponent<Image>() != null)
            {
                itemSprite = item.GetComponent<Image>().sprite;
            }

            for (int i = 0; i < hotbarSlots.Length; i++)
            {
                if (hotbarSlots[i].image.sprite == itemSprite && hotbarSlots[i].image.color == yes)
                {
                    if (hotbarTexts[i].text == "")
                    {
                        hotbarTexts[i].text = (1 + amm).ToString();
                    }

                    else
                    {
                        hotbarTexts[i].text = (int.Parse(hotbarTexts[i].text) + amm).ToString();
                    }

                    if (destroy)
                    {
                        Destroy(objectToDestroy);
                    }

                    if (disableCurrentItemHeld)
                    {
                        currentItemHeld.enabled = false;
                        currentItemHeld.sprite = null;
                        currentItemHeldText.text = "";
                    }

                    return;
                }
            }

            firstAvailableSlot = -1;

            for (int i = 0; i < slots.Length; i++)
            {
                numberOfItems = 0;

                if (itemSprite == slots[i].image.sprite)
                {
                    if (texts[i].text != "")
                    {
                        numberOfItems = int.Parse(texts[i].text) + amm;
                    }

                    else if (texts[i].text == "")
                    {
                        texts[i].text = "1";

                        numberOfItems = int.Parse(texts[i].text) + amm;
                    }

                    texts[i].text = numberOfItems.ToString();

                    if (destroy)
                    {
                        Destroy(objectToDestroy);
                    }

                    if (disableCurrentItemHeld)
                    {
                        currentItemHeld.enabled = false;
                        currentItemHeld.sprite = null;
                        currentItemHeldText.text = "";
                    }

                    return;
                }

                else
                {
                    if (slots[i].image.color == no)
                    {
                        if (firstAvailableSlot < 0)
                        {
                            firstAvailableSlot = i;
                        }
                    }
                }
            }

            slots[firstAvailableSlot].image.sprite = itemSprite;
            slots[firstAvailableSlot].image.color = yes;

            if (amm != 1)
            {
                texts[firstAvailableSlot].text = amm.ToString();
            }

            else texts[firstAvailableSlot].text = "";

            if (destroy)
            {
                Destroy(objectToDestroy);
            }

            if (disableCurrentItemHeld)
            {
                currentItemHeld.enabled = false;
                currentItemHeld.sprite = null;
                currentItemHeldText.text = "";
            }
        }
    } // Collect item and update inventory slots

    public void UpdateInventoryRemove(GameObject item, int amm = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].image.sprite == item.GetComponent<SpriteRenderer>().sprite)
            {
                if (texts[i].text == "")
                {
                    if (amm == 1)
                    {
                        slots[i].image.color = no;
                        slots[i].image.sprite = null;

                        return;
                    }

                    else
                    {
                        amm--;

                        slots[i].image.color = no;
                        slots[i].image.sprite = null;
                    }
                }

                else
                {
                    if (int.Parse(texts[i].text) >= amm)
                    {
                        texts[i].text = (int.Parse(texts[i].text) - amm).ToString();

                        if (int.Parse(texts[i].text) == 0)
                        {
                            slots[i].image.color = no;
                            slots[i].image.sprite = null;
                        }

                        else if (int.Parse(texts[i].text) == 1)
                        {
                            texts[i].text = "";
                        }

                        return;
                    }

                    else
                    {
                        amm -= int.Parse(texts[i].text);

                        slots[i].image.color = no;
                        slots[i].image.sprite = null;
                    }
                }
            }

            if (texts[i].text == "0")
            {
                texts[i].text = "";
            }
        }

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (hotbarSlots[i].image.sprite == item.GetComponent<SpriteRenderer>().sprite)
            {
                if (hotbarTexts[i].text == "")
                {
                    if (amm == 1)
                    {
                        hotbarSlots[i].image.color = no;
                        hotbarSlots[i].image.sprite = null;

                        return;
                    }

                    else
                    {
                        amm--;

                        hotbarSlots[i].image.color = no;
                        hotbarSlots[i].image.sprite = null;
                    }
                }

                else
                {
                    if (int.Parse(hotbarTexts[i].text) >= amm)
                    {
                        hotbarTexts[i].text = (int.Parse(hotbarTexts[i].text) - amm).ToString();

                        if (int.Parse(hotbarTexts[i].text) == 0)
                        {
                            hotbarSlots[i].image.color = no;
                            hotbarSlots[i].image.sprite = null;
                        }

                        else if (int.Parse(hotbarTexts[i].text) == 1)
                        {
                            hotbarTexts[i].text = "";
                        }

                        return;
                    }

                    else
                    {
                        amm -= int.Parse(hotbarTexts[i].text);

                        hotbarSlots[i].image.color = no;
                        hotbarSlots[i].image.sprite = null;
                    }
                }
            }

            if (hotbarTexts[i].text == "0")
            {
                hotbarTexts[i].text = "";
            }
        }
    }

    public void OnSlotClickButton(Button slotButton)
    {
        tempSlotButton = slotButton;
    }

    public void OnSlotClickText(TextMeshProUGUI slotText)
    {
        tempSlotText = slotText;
    }

    public void OnSlotClick(Button slotButton, TextMeshProUGUI slotText)
    {
        if (!currentItemHeld.enabled)
        {
            if (slotButton.image.color == yes)
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    if (slotText.text != "")
                    {
                        currentItemHeld.sprite = slotButton.image.sprite;
                        currentItemHeld.enabled = true;

                        if (slotText.text != "")
                        {
                            tempValue = int.Parse(slotText.text) / 2;
                        }

                        if (tempValue == 0)
                        {
                            tempValue = 1;
                        }

                        currentItemHeldText.text = tempValue.ToString();

                        if (currentItemHeldText.text == "1")
                        {
                            currentItemHeldText.text = "";
                        }

                        if (int.Parse(slotText.text) - tempValue != 0)
                        {
                            slotText.text = (int.Parse(slotText.text) - tempValue).ToString();

                            if (slotText.text == "1")
                            {
                                slotText.text = "";
                            }
                        }

                        else
                        {
                            slotButton.image.color = no;
                            slotButton.image.sprite = null;
                            slotText.text = "";
                        }

                        tempValue = 0;
                    }

                    else
                    {
                        currentItemHeld.sprite = slotButton.image.sprite;
                        currentItemHeld.enabled = true;
                        currentItemHeldText.text = slotText.text;

                        slotButton.image.color = no;
                        slotButton.image.sprite = null;
                        slotText.text = "";
                    }
                }

                else
                {
                    currentItemHeld.sprite = slotButton.image.sprite;
                    currentItemHeld.enabled = true;
                    currentItemHeldText.text = slotText.text;

                    slotButton.image.color = no;
                    slotButton.image.sprite = null;
                    slotText.text = "";
                }
            }
        }

        else if (currentItemHeld.enabled)
        {
            if (slotButton.image.color == yes)
            {
                if (slotButton.image.sprite == currentItemHeld.sprite)
                {
                    if (slotText.text != "" && currentItemHeldText.text != "")
                    {
                        slotText.text = (int.Parse(slotText.text) + int.Parse(currentItemHeldText.text)).ToString();
                    }

                    else
                    {
                        if (slotText.text == "" && currentItemHeldText.text != "")
                        {
                            slotText.text = (1 + int.Parse(currentItemHeldText.text)).ToString();
                        }

                        else if (slotText.text != "" && currentItemHeldText.text == "")
                        {
                            slotText.text = (int.Parse(slotText.text) + 1).ToString();
                        }

                        else if (slotText.text == "" && currentItemHeldText.text == "")
                        {
                            slotText.text = "2";
                        }
                    }

                    currentItemHeld.enabled = false;
                    currentItemHeld.sprite = null;
                    currentItemHeldText.text = "";
                }

                else
                {
                    tempSprite = currentItemHeld.sprite;
                    tempText = currentItemHeldText.text;

                    currentItemHeld.sprite = slotButton.image.sprite;
                    currentItemHeldText.text = slotText.text;

                    slotButton.image.sprite = tempSprite;
                    slotText.text = tempText;

                    tempSprite = null;
                    tempText = "";
                }
            }

            else if (slotButton.image.color == no)
            {
                slotButton.image.sprite = currentItemHeld.sprite;
                slotButton.image.color = yes;
                slotText.text = currentItemHeldText.text;

                currentItemHeld.enabled = false;
                currentItemHeld.sprite = null;
                currentItemHeldText.text = "";
            }
        }

        tempSlotButton = null;
        tempSlotText = null;
    }

    public void OnMouseEnterButton(Button button)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == button)
            {
                overedButton = i + 1;
            }
        }
    }

    public void OnMouseExitButton(Button button)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == button)
            {
                overedButton = 0;
            }
        }
    }

    void UpdateHotbar(int key)
    {
        if (overedButton != 0)
        {
            if (slots[overedButton - 1].image.color == yes)
            {
                if (hotbarSlots[key - 1].image.color == yes)
                {
                    if (hotbarSlots[key - 1].image.sprite == slots[overedButton - 1].image.sprite)
                    {
                        if (hotbarTexts[key - 1].text != "" && texts[overedButton - 1].text != "")
                        {
                            hotbarTexts[key - 1].text = (int.Parse(hotbarTexts[key - 1].text) + int.Parse(texts[overedButton - 1].text)).ToString();

                            slots[overedButton - 1].image.color = no;
                            slots[overedButton - 1].image.sprite = null;
                            texts[overedButton - 1].text = "";
                        }

                        else if (hotbarTexts[key - 1].text == "" && texts[overedButton - 1].text != "")
                        {
                            hotbarTexts[key - 1].text = (1 + int.Parse(texts[overedButton - 1].text)).ToString();

                            slots[overedButton - 1].image.color = no;
                            slots[overedButton - 1].image.sprite = null;
                            texts[overedButton - 1].text = "";
                        }

                        else if (hotbarTexts[key - 1].text != "" && texts[overedButton - 1].text == "")
                        {
                            hotbarTexts[key - 1].text = (int.Parse(hotbarTexts[key - 1].text) + 1).ToString();

                            slots[overedButton - 1].image.color = no;
                            slots[overedButton - 1].image.sprite = null;
                            texts[overedButton - 1].text = "";
                        }

                        else
                        {
                            hotbarTexts[key - 1].text = "2";

                            slots[overedButton - 1].image.color = no;
                            slots[overedButton - 1].image.sprite = null;
                            texts[overedButton - 1].text = "";
                        }
                    }

                    else
                    {
                        tempSpriteHotbar = hotbarSlots[key - 1].image.sprite;
                        tempTextHotbar = hotbarTexts[key - 1].text;

                        hotbarSlots[key - 1].image.sprite = slots[overedButton - 1].image.sprite;
                        hotbarTexts[key - 1].text = texts[overedButton - 1].text;

                        slots[overedButton - 1].image.sprite = tempSpriteHotbar;
                        texts[overedButton - 1].text = tempTextHotbar;

                        tempSpriteHotbar = null;
                        tempTextHotbar = "";
                    }
                }

                else
                {
                    hotbarSlots[key - 1].image.sprite = slots[overedButton - 1].image.sprite;
                    hotbarSlots[key - 1].image.color = yes;
                    hotbarTexts[key - 1].text = texts[overedButton - 1].text;

                    slots[overedButton - 1].image.color = no;
                    slots[overedButton - 1].image.sprite = null;
                    texts[overedButton - 1].text = "";
                }
            }

            else
            {
                if (hotbarSlots[key - 1].image.color == yes)
                {
                    slots[overedButton - 1].image.sprite = hotbarSlots[key - 1].image.sprite;
                    slots[overedButton - 1].image.color = yes;
                    texts[overedButton - 1].text = hotbarTexts[key - 1].text;

                    hotbarSlots[key - 1].image.sprite = null;
                    hotbarSlots[key - 1].image.color = no;
                    hotbarTexts[key - 1].text = "";
                }
            }
        }
    }
    
    public void UpdateHotbarTryRemove(GameObject item, int amm = 1)
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (hotbarSlots[i].image.sprite == item.GetComponent<SpriteRenderer>().sprite)
            {
                if (hotbarTexts[i].text == "")
                {
                    if (amm == 1)
                    {
                        hotbarSlots[i].image.color = no;
                        hotbarSlots[i].image.sprite = null;

                        return;
                    }

                    else
                    {
                        amm--;

                        hotbarSlots[i].image.color = no;
                        hotbarSlots[i].image.sprite = null;
                    }
                }

                else
                {
                    if (int.Parse(hotbarTexts[i].text) >= amm)
                    {
                        hotbarTexts[i].text = (int.Parse(hotbarTexts[i].text) - amm).ToString();

                        if (int.Parse(hotbarTexts[i].text) == 0)
                        {
                            hotbarSlots[i].image.color = no;
                            hotbarSlots[i].image.sprite = null;
                        }

                        else if (int.Parse(hotbarTexts[i].text) == 1)
                        {
                            hotbarTexts[i].text = "";
                        }

                        return;
                    }

                    else
                    {
                        amm -= int.Parse(hotbarTexts[i].text);

                        hotbarSlots[i].image.color = no;
                        hotbarSlots[i].image.sprite = null;
                    }
                }
            }

            if (hotbarTexts[i].text == "0")
            {
                hotbarTexts[i].text = "";
            }
        }
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].image.sprite == item.GetComponent<SpriteRenderer>().sprite)
            {
                if (texts[i].text == "")
                {
                    if (amm == 1)
                    {
                        slots[i].image.color = no;
                        slots[i].image.sprite = null;

                        return;
                    }

                    else
                    {
                        amm--;

                        slots[i].image.color = no;
                        slots[i].image.sprite = null;
                    }
                }

                else
                {
                    if (int.Parse(texts[i].text) >= amm)
                    {
                        texts[i].text = (int.Parse(texts[i].text) - amm).ToString();

                        if (int.Parse(texts[i].text) == 0)
                        {
                            slots[i].image.color = no;
                            slots[i].image.sprite = null;
                        }

                        else if (int.Parse(texts[i].text) == 1)
                        {
                            texts[i].text = "";
                        }

                        return;
                    }

                    else
                    {
                        amm -= int.Parse(texts[i].text);

                        slots[i].image.color = no;
                        slots[i].image.sprite = null;
                    }
                }
            }

            if (texts[i].text == "0")
            {
                texts[i].text = "";
            }
        }
    }
}
    