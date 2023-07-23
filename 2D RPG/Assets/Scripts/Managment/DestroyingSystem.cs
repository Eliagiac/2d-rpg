using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingSystem : MonoBehaviour
{
    #region Public References and Accessible From Other Scripts Info
    public static Animator animator;
    public Animator setAnimator;

    public static GameObject destroyingBar;
    public GameObject setDestroyingBar;

    public static float destroyRange;
    public float setDestroyRange = 10f;

    public static Transform player;
    public Transform setPlayer;

    public static bool isAnimationFinished;

    public RectTransform destroyBar;

    public static bool getPos;

    AppleTreeLogic appleTreeLogic;

    InventoryHandler inventoryHandler;

    public GameObject fullConveyorBelt;

    bool hasUpdatedInventory;
    #endregion

    private void Start()
    {
        animator = setAnimator;
        destroyingBar = setDestroyingBar;
        destroyRange = setDestroyRange;
        player = setPlayer;

        inventoryHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHandler>();
    } // Make references accessible from other scripts

    private void Update()
    {
        if (InventoryHandler.isHotbarOn)
        {
            destroyBar.anchoredPosition = new Vector3(0, 65, 0);
        }

        else if (!InventoryHandler.isHotbarOn)
        {
            destroyBar.anchoredPosition = new Vector3(0, 0, 0);
        }
    }    

    public void ExitDestroyObject()
    {
        destroyingBar.SetActive(false);

        animator.SetBool("Destroy", false);
    }

    public void DestroyObject(float destroyRange, float animSpeed, GameObject obj)
    {
        animator.speed = animSpeed;

        if (!MenuHandler.isMenuOpen)
        {
            if ((player.position - obj.transform.position).sqrMagnitude <= destroyRange * destroyRange)
            {
                if (Input.GetMouseButton(1))
                {
                    destroyingBar.SetActive(true);

                    animator.SetBool("Destroy", true);
                }

                if (Input.GetMouseButtonUp(1))
                {
                    destroyingBar.SetActive(false);

                    animator.SetBool("Destroy", false);
                }

                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    isAnimationFinished = true;

                    if (obj.GetComponent<AppleTreeLogic>() != null)
                    {
                        appleTreeLogic = obj.GetComponent<AppleTreeLogic>();

                        appleTreeLogic.destroyedAppleTreePosition = obj.transform.position;

                        if (obj.GetComponent<SpriteRenderer>().sprite == appleTreeLogic.sprite1)
                        {
                            appleTreeLogic.GenerateItemsPositions(9);

                            appleTreeLogic.DropItems(appleTreeLogic.log, 1, null, 0,appleTreeLogic.leaf, 8);
                        }

                        else if (obj.GetComponent<SpriteRenderer>().sprite == appleTreeLogic.sprite2)
                        {
                            appleTreeLogic.GenerateItemsPositions(10);

                            appleTreeLogic.DropItems(appleTreeLogic.log, 1, appleTreeLogic.apple, 1, appleTreeLogic.leaf, 8);
                        }

                        else if (obj.GetComponent<SpriteRenderer>().sprite == appleTreeLogic.sprite3)
                        {
                            appleTreeLogic.GenerateItemsPositions(11);

                            appleTreeLogic.DropItems(appleTreeLogic.log, 1, appleTreeLogic.apple, 2, appleTreeLogic.leaf, 8);
                        }

                        else if (obj.GetComponent<SpriteRenderer>().sprite == appleTreeLogic.sprite4)
                        {
                            appleTreeLogic.GenerateItemsPositions(12);

                            appleTreeLogic.DropItems(appleTreeLogic.log, 1, appleTreeLogic.apple, 3, appleTreeLogic.leaf, 8);
                        }

                        else if (obj.GetComponent<SpriteRenderer>().sprite == appleTreeLogic.sprite5)
                        {
                            appleTreeLogic.GenerateItemsPositions(13);

                            appleTreeLogic.DropItems(appleTreeLogic.log, 1, appleTreeLogic.apple, 4, appleTreeLogic.leaf, 8);
                        }
                    }

                    else
                    {
                        for (int i = 0; i < BuildingSystem._belts.Length; i++)
                        {
                            if (obj.GetComponent<SpriteRenderer>().sprite == BuildingSystem._belts[i])
                            {
                                inventoryHandler.UpdateInventory(fullConveyorBelt, true, false, 1, obj);

                                hasUpdatedInventory = true;

                                break;
                            }
                        }

                        if (!hasUpdatedInventory)
                        {
                            inventoryHandler.UpdateInventory(obj, true);
                        }
                    }

                    Destroy(obj);

                    ExitDestroyObject();
                }
            }
        }
    }
}