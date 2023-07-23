using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    #region Public Belts References
    public Sprite[] belts;
    public static Sprite[] _belts;

    public GameObject belt;
    public GameObject fullBelt;

    public Transform conveyorBeltsParent;
    public static Transform _conveyorBeltsParent;

    public Sprite[] beltMasks;
    public static Sprite[] _beltMasks = {
        null, null, null, null, null, null, null
    }; // Default _belts to null

    public AnimationClip[] beltAnimations;
    public static AnimationClip[] _beltAnimations;
    #endregion

    #region Generic Info Variables
    Vector2 mousePos;
    Vector2 trueMousePos;

    Vector2 currentPos;

    bool isPreviewActive = false;

    public float XOffset = 0.5f;
    public float YOffset = 0.5f;

    Transform[] conveyorBeltsTransform;
    GameObject[] conveyorBelts;

    List<GameObject> ConveyorBelts;

    string axis;

    bool isDragging = false;

    Vector2 truePreviewPos;
    #endregion

    #region Belts Info Variables
    GameObject previewBelt;

    GameObject instantiatedBelt;

    Collider2D instantiatedBeltCol;

    SpriteRenderer instantiatedBeltSpriteRenderer;

    Vector2 beltPos;

    int placedAmm;
    int maxAmm;

    float previewBeltRotation;
    #endregion

    InventoryHandler inventoryHandler;

    private void Start()
    {
        _conveyorBeltsParent = conveyorBeltsParent;

        _belts = new Sprite[belts.Length];

        for (int a = 0; a < belts.Length; a++) // Make belts[] accessible from other scripts
        {
            _belts[a] = belts[a];
        }

        for (int a = 0; a < beltMasks.Length; a++) // Make beltMasks[] accessible from other scripts
        {
            _beltMasks[a] = beltMasks[a];
        }

        _beltAnimations = beltAnimations;

        inventoryHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHandler>();
    } // Make belts[] accessible from other scripts

    private void Update()
    {
        if (isPreviewActive)
        {
            previewBeltRotation = previewBelt.transform.eulerAngles.z;
        }

        else previewBeltRotation = 0f;

        #region temp
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //ToggleBeltPreview(9999999);
        }
        #endregion

        #region Get Mouse Position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        trueMousePos.x = Mathf.Floor(mousePos.x + XOffset);
        trueMousePos.y = Mathf.Floor(mousePos.y + YOffset);
        #endregion

        if (isPreviewActive)
        {
            previewBelt.transform.position = new Vector3(truePreviewPos.x, truePreviewPos.y, -0.01f);

            if (placedAmm >= maxAmm)
            {
                ToggleBeltPreview();
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    previewBelt.transform.eulerAngles = new Vector3(0f, 0f, previewBeltRotation - 90f);
                } // Rotate belt preview

                if (Input.GetMouseButtonDown(0))
                {
                    if (previewBeltRotation == 0f || previewBeltRotation == 180f)
                    {
                        axis = "x";
                    }

                    else if (previewBeltRotation == 270f || previewBeltRotation == 90f)
                    {
                        axis = "y";
                    }

                    InstantiateBelt();

                    beltPos = new Vector2(instantiatedBelt.transform.position.x, instantiatedBelt.transform.position.y);
                } // Place belt

                else if (Input.GetMouseButton(0) && trueMousePos != currentPos)
                {
                    if (axis == "x")
                    {
                        trueMousePos.x = beltPos.x;
                    }

                    else if (axis == "y")
                    {
                        trueMousePos.y = beltPos.y;
                    }

                    InstantiateBelt();

                    previewBelt.GetComponent<SpriteRenderer>().enabled = false;

                    isDragging = true;
                } // Place belt by dragging

                if (Input.GetMouseButtonUp(0))
                {
                    previewBelt.GetComponent<SpriteRenderer>().enabled = true;

                    isDragging = false;
                } // Stop dragging

                if (isDragging)
                {
                    if (instantiatedBelt != null)
                    {
                        beltPos = new Vector2(instantiatedBelt.transform.position.x, instantiatedBelt.transform.position.y);

                        if (axis == "x")
                        {
                            truePreviewPos.x = beltPos.x;
                        }

                        else if (axis == "y")
                        {
                            truePreviewPos.y = beltPos.y;
                        }
                    }

                    else isDragging = false;
                } // Set positions for dragging

                else if (!isDragging)
                {
                    truePreviewPos = trueMousePos;
                }
            }
        }
    } // Place belts

    void InstantiateBelt()
    {
        placedAmm++;

        #region Instantiate Belt
        instantiatedBelt = Instantiate(belt, new Vector3(trueMousePos.x, trueMousePos.y, -0.01f), Quaternion.identity, conveyorBeltsParent);
        instantiatedBeltCol = instantiatedBelt.GetComponent<PolygonCollider2D>();
        instantiatedBeltCol.enabled = true;

        instantiatedBeltSpriteRenderer = instantiatedBelt.GetComponent<SpriteRenderer>();

        if (previewBeltRotation == 0f)
        {
            instantiatedBeltSpriteRenderer.sprite = belts[0];
        }

        else if (previewBeltRotation == 180f)
        {
            instantiatedBeltSpriteRenderer.sprite = belts[1];
        }

        else if (previewBeltRotation == 270f)
        {
            instantiatedBeltSpriteRenderer.sprite = belts[2];
        }

        else if (previewBeltRotation == 90f)
        {
            instantiatedBeltSpriteRenderer.sprite = belts[3];
        }

        currentPos.x = instantiatedBelt.transform.position.x;
        currentPos.y = instantiatedBelt.transform.position.y;

        conveyorBeltsTransform = conveyorBeltsParent.GetComponentsInChildren<Transform>();

        conveyorBelts = new GameObject[conveyorBeltsTransform.Length];

        for (int i = 0; i < conveyorBelts.Length; i++)
        {
            conveyorBelts[i] = conveyorBeltsTransform[i].gameObject;
        }
        #endregion

        #region Replace Belts On Stack
        for (int i = 0; i < conveyorBelts.Length; i++)
        {
            if (conveyorBelts[i] == instantiatedBelt)
            {
                ConveyorBelts = new List<GameObject>(conveyorBelts);
                ConveyorBelts.Remove(conveyorBelts[i]);
                conveyorBelts = ConveyorBelts.ToArray();
            }
        }

        for (int i = 0; i < conveyorBelts.Length; i++)
        {
            if (conveyorBelts[i].transform.position == instantiatedBelt.transform.position && conveyorBelts[i].name == "ConveyorBelt(Clone)")
            {
                Destroy(conveyorBelts[i].gameObject);
                inventoryHandler.UpdateInventory(fullBelt, false);
            }
        }
        #endregion

        inventoryHandler.UpdateHotbarTryRemove(fullBelt);
    }

    public void ToggleBeltPreview(int maxAmmount = 1)
    {
        placedAmm = 0;
        maxAmm = maxAmmount;

        if (!isPreviewActive)
        {
            isPreviewActive = true;

            previewBelt = Instantiate(fullBelt, new Vector3(trueMousePos.x, trueMousePos.y, -0.01f), Quaternion.identity);
        }

        else if (isPreviewActive)
        {
            isPreviewActive = false;

            Destroy(previewBelt);
        }
    }
}