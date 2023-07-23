using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGive : MonoBehaviour
{
    InventoryHandler inventoryHandler;
    int n = 0;
    public GameObject belt;

    void Start()
    {
        inventoryHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHandler>();
    }

    public void OnClickGive()
    {
        n = 1;
    }

    private void Update()
    {
        if (n == 1)
        {
            for (int i = 0; i < 1; i++)
            {
                inventoryHandler.UpdateInventory(belt, false);
            }
        }
    }
}
