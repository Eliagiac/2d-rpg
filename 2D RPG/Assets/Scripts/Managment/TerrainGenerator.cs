using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    #region Public Grass References
    public GameObject[] sprites;

    public Transform grassParent;
    #endregion

    #region Grass Info Variables
    GameObject currentSprite;

    Vector3 currentTileCoords;
    #endregion

    #region Grass Generation Rules
    public int terrainHeight = 4;
    public int terrainLenght = 4;

    public int tileXOffset = 0;
    public int tileYOffset = 0;
    #endregion


    #region Public Trees References
    public GameObject treePrefab;

    public Sprite[] appleTreeSprites;

    public Transform appleTreeParent;
    #endregion

    #region Trees Info Variables
    int isTree;

    GameObject tree;

    SpriteRenderer spriteRenderer;

    Sprite currentTreeSprite;

    bool hasFound2;
    #endregion

    #region Trees Generation Rules
    public int treeProbability = 100;
    int trueTreeProbability = 100;

    public int extraTreeSpawnProbabilityDistance1 = 5;
    public int extraTreeSpawnProbabilityDistance2 = 10;

    public float extraTreeSpawnProbabilityMultiplier1 = 2f;
    public float extraTreeSpawnProbabilityMultiplier2 = 1.5f;

    public float closeToForestProbabilityMultiplier = 5f;
    #endregion


    #region Public Forests References
    public GameObject forestPrefab;

    public Transform forestParent;
    #endregion

    #region Forests Info Variables
    int isForest;

    int forestAmm;

    public int maxForestAmm = 3000;
    public int minForestAmm = 1;

    public int forestMinDimension = 15;
    public int forestMaxDimension = 20;

    int[] forestsDimensions;
    #endregion

    #region Forests Generation Rules
    public int forestProbability = 2;

    public float forestProbabilityMultiplier = 2f;
    #endregion

    void Start()
    {
        InstantiateTiles();
    } // Generate grass and trees

    void InstantiateTiles()
    {
        forestAmm = 0;

        for (int x = 0; x < terrainLenght; x += 1) // Go through x points
        {
            for (int y = 0; y < terrainHeight; y += 1) // Go through y points
            {
                currentTileCoords = new Vector3(x - tileXOffset, y - tileYOffset, 0);

                #region Generate Forests
                isForest = Random.Range(1, (int)((terrainLenght * terrainHeight) / forestProbabilityMultiplier));

                if (isForest <= forestProbability && forestAmm <= (terrainLenght * terrainHeight) / maxForestAmm)
                {
                    Instantiate(forestPrefab, new Vector3(currentTileCoords.x, currentTileCoords.y, -6.1f), Quaternion.identity, forestParent);

                    forestAmm++;
                }
                #endregion
            }
        }

        if (forestAmm < minForestAmm)
        {
            Instantiate(forestPrefab, new Vector3(currentTileCoords.x, currentTileCoords.y, -6.1f), Quaternion.identity, forestParent);
        }

        forestsDimensions = new int[forestParent.childCount];

        for (int i = 0; i < forestParent.childCount; i++)
        {
            forestsDimensions[i] = Random.Range(forestMinDimension, forestMaxDimension);
        }

        for (int x = 0; x < terrainLenght; x += 1) // Go through x points
        {
            for (int y = 0; y < terrainHeight; y += 1) // Go through y points
            {
                #region Generate Grass
                currentSprite = sprites[Random.Range(0, 4)];

                currentTileCoords = new Vector3(x - tileXOffset, y - tileYOffset, 0);

                Instantiate(currentSprite, currentTileCoords, Quaternion.identity, grassParent);
                #endregion

                #region Generate Trees
                hasFound2 = false;

                trueTreeProbability = Random.Range(1, treeProbability + 1);

                for (int i = 0; i < appleTreeParent.childCount; i++)
                {
                    if ((new Vector2(appleTreeParent.GetChild(i).transform.position.x, appleTreeParent.GetChild(i).transform.position.y) - new Vector2(currentTileCoords.x, currentTileCoords.y)).sqrMagnitude <= extraTreeSpawnProbabilityDistance1 * extraTreeSpawnProbabilityDistance1)
                    {
                        trueTreeProbability = (int)(treeProbability / extraTreeSpawnProbabilityMultiplier1);

                        break;
                    }

                    else if ((new Vector2(appleTreeParent.GetChild(i).transform.position.x, appleTreeParent.GetChild(i).transform.position.y) - new Vector2(currentTileCoords.x, currentTileCoords.y)).sqrMagnitude <= extraTreeSpawnProbabilityDistance2 * extraTreeSpawnProbabilityDistance2)
                    {
                        trueTreeProbability = (int)(treeProbability / extraTreeSpawnProbabilityMultiplier2);

                        hasFound2 = true;
                    }

                    else if (!hasFound2)
                    {
                        trueTreeProbability = treeProbability;
                    }
                }

                for (int i = 0; i < forestParent.childCount; i++)
                {
                    if ((new Vector2(forestParent.GetChild(i).transform.position.x, forestParent.GetChild(i).transform.position.y) - new Vector2(currentTileCoords.x, currentTileCoords.y)).sqrMagnitude <= forestsDimensions[i] * forestsDimensions[i])
                    {
                        trueTreeProbability = (int)(trueTreeProbability / closeToForestProbabilityMultiplier);

                        break;
                    }
                }

                isTree = Random.Range(1, trueTreeProbability + 1);

                currentTreeSprite = appleTreeSprites[Random.Range(0, 5)];

                if (isTree == 1)
                {
                    tree = Instantiate(treePrefab, new Vector3(currentTileCoords.x, currentTileCoords.y, -6.1f), Quaternion.identity, appleTreeParent);

                    spriteRenderer = tree.GetComponent<SpriteRenderer>();

                    spriteRenderer.sprite = currentTreeSprite;
                } // Generate tree
                #endregion
            }
        }
    }
}