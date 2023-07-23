using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribesGenerator : MonoBehaviour
{
    #region Public References
    public GameObject tribeTier1;
    public Transform tribesParent;
    #endregion

    #region Spawning Tribes Rules
    public int ammOfTribesTier1 = 20;

    Vector2[] tribeCoordsTier1;

    public float radiusTier1 = 50f;

    //bool[] isTribePosOK;

    int[] tries = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    }; // Define tries

    public int maxTries = 100;

    public int tribesTier1MinDistance = 20;
    #endregion

    #region Borders
    GameObject instantiatedTribe;

    SpriteRenderer borders;

    float r;
    float g;
    float b;
    #endregion

    #region Enemy Variables
    int ammOfEnemiesTier1;

    Transform enemiesParent;
    public GameObject enemy;

    Vector2 enemyPos;

    GameObject instantiatedEnemy;
    Transform[] enemySkirts;

    public int enemySpawnRange = 8;
    #endregion

    #region Extra Variables
    public static int _ammOfTribesTier1; // ammOfTribesTier1 but accessible from other scripts
    #endregion

    void Start()
    {
        GenerateTribesTier1();

        _ammOfTribesTier1 = ammOfTribesTier1; // make ammOfTribesTier1 variable accessible from other scripts
    } // Generate tribes
    
    void GenerateTribesTier1()
    {
        #region Define Variables
        tribeCoordsTier1 = new Vector2[ammOfTribesTier1];
        #endregion

        for (int i = 0; i < ammOfTribesTier1; i++)
        {
            #region Generate Tribes
            generatePos:
            tribeCoordsTier1[i] = Random.insideUnitCircle.normalized * radiusTier1;
            tribeCoordsTier1[i] = new Vector2(Mathf.Floor(tribeCoordsTier1[i].x), Mathf.Floor(tribeCoordsTier1[i].y));

            for (int x = 0; x < 20; x++) // Check tribes distances
            {
                if ((tribeCoordsTier1[i] - tribeCoordsTier1[x]).sqrMagnitude < tribesTier1MinDistance * tribesTier1MinDistance && x < i && tries[i] < maxTries)
                {
                    tries[i] += 1;

                    goto generatePos;
                }

                else if (tries[i] >= maxTries)
                {
                    ammOfTribesTier1 = i;

                    return;
                }
            }

            instantiatedTribe = Instantiate(tribeTier1, new Vector3(tribeCoordsTier1[i].x, tribeCoordsTier1[i].y, -0.0001f), Quaternion.identity, tribesParent);
            #endregion

            #region Color Borders
            borders = instantiatedTribe.transform.Find("GFX").GetComponent<SpriteRenderer>();

            r = Random.Range(0f, 1f);
            g = Random.Range(0f, 1f);
            b = Random.Range(0f, 1f);

            borders.color = new Color(r, g, b, 1);
            #endregion

            #region Spawn Enemies
            ammOfEnemiesTier1 = Random.Range(2, 4);

            enemiesParent = instantiatedTribe.transform.Find("Enemies");

            for (int x = 0; x < ammOfEnemiesTier1; x++)
            {
                #region Generate Enemy pos
                enemyPos = (Random.insideUnitCircle * enemySpawnRange) + new Vector2(instantiatedTribe.transform.position.x, instantiatedTribe.transform.position.y);

                enemyPos = new Vector2(Mathf.Floor(enemyPos.x), Mathf.Floor(enemyPos.y));
                #endregion

                instantiatedEnemy = Instantiate(enemy, enemyPos, Quaternion.identity, enemiesParent);

                if (instantiatedEnemy != null)
                {
                    enemySkirts = new Transform[4];

                    for (int n = 0; n < enemySkirts.Length; n++)
                    {
                        enemySkirts[n] = instantiatedEnemy.transform.GetChild(n).Find("Skirt");

                        enemySkirts[n].GetComponent<SpriteRenderer>().color = new Color(r, g, b, 1);
                    }
                }
            }
            #endregion
        }
    }
}