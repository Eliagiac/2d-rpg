using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTreeLogic : MonoBehaviour
{
    public GameObject appleTree;

    public GameObject log;
    public GameObject apple;
    public GameObject leaf;

    GameObject[] objects;

    int[] ammounts;

    Vector3[] itemPos;

    int n = 0;

    public float itemsMinDistance = 0.25f;

    Transform itemsParent;

    [HideInInspector]
    public Vector3 destroyedAppleTreePosition;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;

    int index = 0;

    bool haveItemsDropped = true;
    
    DestroyingSystem destroyingSystem;

    private void Start()
    {
        destroyingSystem = GameObject.FindGameObjectWithTag("Building&DestroyingSystem").GetComponent<DestroyingSystem>();
        itemsParent = GameObject.FindGameObjectWithTag("ItemsParent").transform;
    }

    private void OnMouseEnter()
    {
        DestroyingSystem.getPos = true;
    }

    void OnMouseOver()
    {
        destroyingSystem.DestroyObject(DestroyingSystem.destroyRange, 1f, appleTree);

        haveItemsDropped = false;
    }

    private void OnMouseExit()
    {
        destroyingSystem.ExitDestroyObject();
    }

    public void GenerateItemsPositions(int ammOfItems)
    {
        itemPos = new Vector3[ammOfItems];

        for (int i = 0; i < ammOfItems; i++)
        {
            generatePos:
            itemPos[i] = new Vector3(destroyedAppleTreePosition.x + Random.Range(-2f, 2f), destroyedAppleTreePosition.y + Random.Range(-2f, 2f), -6.1f);

            for (int x = 0; x < ammOfItems; x++) // Check items distances
            {
                if ((itemPos[i] - itemPos[x]).magnitude < itemsMinDistance && (itemPos[i] - itemPos[x]).magnitude != 0)
                {
                    goto generatePos;
                }
            }
        }
    }

    public void DropItems(GameObject obj1, int amm1, GameObject obj2, int amm2, GameObject obj3, int amm3)
    {
        objects = new GameObject[3];
        objects[0] = obj1;
        objects[1] = obj2;
        objects[2] = obj3;

        ammounts = new int[3];
        ammounts[0] = amm1;
        ammounts[1] = amm2;
        ammounts[2] = amm3;

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                for (int x = 0; x < ammounts[index]; x++)
                {
                    Instantiate(objects[i], itemPos[n], Quaternion.identity);

                    n++;
                }
            }

            index++;
        }

        index = 0;
        n = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.frameCount <= 2)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}