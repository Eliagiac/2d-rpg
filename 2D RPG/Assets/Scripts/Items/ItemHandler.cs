using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    DestroyingSystem destroyingSystem;

    private void Start()
    {
        destroyingSystem = GameObject.FindGameObjectWithTag("Building&DestroyingSystem").GetComponent<DestroyingSystem>();
    }

    private void OnMouseOver()
    {
        if (!gameObject.CompareTag("Weapon"))
        {
            destroyingSystem.DestroyObject(DestroyingSystem.destroyRange + 5f, 4f, gameObject);
        }
    }

    private void OnMouseExit()
    {
        destroyingSystem.ExitDestroyObject();
    }
}
