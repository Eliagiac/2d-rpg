using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltManager : MonoBehaviour
{
    #region Info Variables
    Sprite[] belts;

    string objPos;
    string objDir;

    string exitingObjPos;

    SpriteRenderer thisObjSpriteRenderer;
    GameObject obj;
    SpriteRenderer objSpriteRenderer;

    DestroyingSystem destroyingSystem;

    Sprite[] beltMasks;

    AnimationClip[] beltAnimations;

    bool isCurved;

    Animation animation;

    bool wasCurved;
    #endregion

    private void Start()
    {
        destroyingSystem = GameObject.FindGameObjectWithTag("Building&DestroyingSystem").GetComponent<DestroyingSystem>();

        belts = BuildingSystem._belts;

        thisObjSpriteRenderer = GetComponent<SpriteRenderer>();

        beltMasks = BuildingSystem._beltMasks;

        beltAnimations = BuildingSystem._beltAnimations;

        AnimateBeltArrows();

        animation = gameObject.GetComponentInChildren<Animation>();

        animation[beltAnimations[0].name].time = Time.realtimeSinceStartup;
        animation[beltAnimations[1].name].time = Time.realtimeSinceStartup;
    } // Get reference to belt sprites

    private void OnMouseOver()
    {
        destroyingSystem.DestroyObject(DestroyingSystem.destroyRange, 2f, gameObject);
    } // Destroy belt on right mouse hold

    private void OnMouseExit()
    {
        destroyingSystem.ExitDestroyObject();
    } // Stop destroiyng belt

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Belt"))
        {
            obj = col.gameObject;
            objSpriteRenderer = obj.GetComponent<SpriteRenderer>();

            #region Set Belt Pos
            if (col.gameObject.transform.position.x > transform.position.x)
            {
                objPos = "east";
            }

            else if (col.gameObject.transform.position.x < transform.position.x)
            {
                objPos = "west";
            }

            else if (col.gameObject.transform.position.y > transform.position.y)
            {
                objPos = "north";
            }

            else if (col.gameObject.transform.position.y < transform.position.y)
            {
                objPos = "south";
            }
            #endregion

            #region Set Belt Dir
            if (objSpriteRenderer.sprite == belts[0] || objSpriteRenderer.sprite == belts[10] || objSpriteRenderer.sprite == belts[11] || objSpriteRenderer.sprite == belts[12] || objSpriteRenderer.sprite == belts[13] || objSpriteRenderer.sprite == belts[14] || objSpriteRenderer.sprite == belts[28] || objSpriteRenderer.sprite == belts[29])
            {
                objDir = "up";
            }

            else if (objSpriteRenderer.sprite == belts[2] || objSpriteRenderer.sprite == belts[4] || objSpriteRenderer.sprite == belts[8] || objSpriteRenderer.sprite == belts[18] || objSpriteRenderer.sprite == belts[19] || objSpriteRenderer.sprite == belts[20] || objSpriteRenderer.sprite == belts[30] || objSpriteRenderer.sprite == belts[24])
            {
                objDir = "right";
            }

            else if (objSpriteRenderer.sprite == belts[1] || objSpriteRenderer.sprite == belts[6] || objSpriteRenderer.sprite == belts[7] || objSpriteRenderer.sprite == belts[15] || objSpriteRenderer.sprite == belts[16] || objSpriteRenderer.sprite == belts[17] || objSpriteRenderer.sprite == belts[26] || objSpriteRenderer.sprite == belts[27])
            {
                objDir = "down";
            }

            else if (objSpriteRenderer.sprite == belts[3] || objSpriteRenderer.sprite == belts[5] || objSpriteRenderer.sprite == belts[9] || objSpriteRenderer.sprite == belts[21] || objSpriteRenderer.sprite == belts[22] || objSpriteRenderer.sprite == belts[23] || objSpriteRenderer.sprite == belts[25] || objSpriteRenderer.sprite == belts[31])
            {
                objDir = "left";
            }
            #endregion

            AdjustBeltSprite();
        }
    } // Get reference to belt object, it's position and it's direction and change it's sprite

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Belt")
        {
            obj = col.gameObject;
            objSpriteRenderer = obj.GetComponent<SpriteRenderer>();

            #region Set Belt Pos
            if (col.gameObject.transform.position.x > transform.position.x)
            {
                exitingObjPos = "east";
            }

            else if (col.gameObject.transform.position.x < transform.position.x)
            {
                exitingObjPos = "west";
            }

            else if (col.gameObject.transform.position.y > transform.position.y)
            {
                exitingObjPos = "north";
            }

            else if (col.gameObject.transform.position.y < transform.position.y)
            {
                exitingObjPos = "south";
            }
            #endregion

            AdjustBeltSprite(false);
        }
    } // Get reference to belt object, it's position and it's direction and change it's sprite

    void AdjustBeltSprite(bool entering = true)
    {
        if (entering)
        {
            switch (objPos)
            {
                case "north":

                    switch (objDir)
                    {
                        case "up":

                            if (thisObjSpriteRenderer.sprite == belts[0])
                            {
                                thisObjSpriteRenderer.sprite = belts[14];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[10])
                            {
                                thisObjSpriteRenderer.sprite = belts[11];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[28])
                            {
                                thisObjSpriteRenderer.sprite = belts[12];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[29])
                            {
                                thisObjSpriteRenderer.sprite = belts[13];
                            }

                            break;

                        case "down":

                            if (thisObjSpriteRenderer.sprite == belts[1])
                            {
                                thisObjSpriteRenderer.sprite = belts[15];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[2])
                            {
                                thisObjSpriteRenderer.sprite = belts[30];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[3])
                            {
                                thisObjSpriteRenderer.sprite = belts[31];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[4])
                            {
                                thisObjSpriteRenderer.sprite = belts[20];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[5])
                            {
                                thisObjSpriteRenderer.sprite = belts[21];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[6])
                            {
                                thisObjSpriteRenderer.sprite = belts[15];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[20])
                            {
                                thisObjSpriteRenderer.sprite = belts[8];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[21])
                            {
                                thisObjSpriteRenderer.sprite = belts[9];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[17])
                            {
                                thisObjSpriteRenderer.sprite = belts[16];
                            }

                            break;

                        case "right":

                            if (thisObjSpriteRenderer.sprite == belts[0])
                            {
                                thisObjSpriteRenderer.sprite = belts[14];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[10])
                            {
                                thisObjSpriteRenderer.sprite = belts[11];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[28])
                            {
                                thisObjSpriteRenderer.sprite = belts[12];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[29])
                            {
                                thisObjSpriteRenderer.sprite = belts[13];
                            }

                            break;

                        case "left":

                            if (thisObjSpriteRenderer.sprite == belts[0])
                            {
                                thisObjSpriteRenderer.sprite = belts[14];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[10])
                            {
                                thisObjSpriteRenderer.sprite = belts[11];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[28])
                            {
                                thisObjSpriteRenderer.sprite = belts[12];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[29])
                            {
                                thisObjSpriteRenderer.sprite = belts[13];
                            }

                            break;
                    }

                    break;

                case "east":

                    switch (objDir)
                    {
                        case "right":

                            if (thisObjSpriteRenderer.sprite == belts[2])
                            {
                                thisObjSpriteRenderer.sprite = belts[20];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[18])
                            {
                                thisObjSpriteRenderer.sprite = belts[19];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[24])
                            {
                                thisObjSpriteRenderer.sprite = belts[4];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[30])
                            {
                                thisObjSpriteRenderer.sprite = belts[8];
                            }

                            break;

                        case "left":

                            if (thisObjSpriteRenderer.sprite == belts[0])
                            {
                                thisObjSpriteRenderer.sprite = belts[28];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[1])
                            {
                                thisObjSpriteRenderer.sprite = belts[26];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[3])
                            {
                                thisObjSpriteRenderer.sprite = belts[23];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[5])
                            {
                                thisObjSpriteRenderer.sprite = belts[23];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[7])
                            {
                                thisObjSpriteRenderer.sprite = belts[17];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[9])
                            {
                                thisObjSpriteRenderer.sprite = belts[23];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[14])
                            {
                                thisObjSpriteRenderer.sprite = belts[12];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[21])
                            {
                                thisObjSpriteRenderer.sprite = belts[22];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[13])
                            {
                                thisObjSpriteRenderer.sprite = belts[14];
                            }

                            break;

                        case "up":

                            if (thisObjSpriteRenderer.sprite == belts[2])
                            {
                                thisObjSpriteRenderer.sprite = belts[20];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[18])
                            {
                                thisObjSpriteRenderer.sprite = belts[19];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[24])
                            {
                                thisObjSpriteRenderer.sprite = belts[4];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[30])
                            {
                                thisObjSpriteRenderer.sprite = belts[8];
                            }

                            break;


                        case "down":

                            if (thisObjSpriteRenderer.sprite == belts[2])
                            {
                                thisObjSpriteRenderer.sprite = belts[20];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[18])
                            {
                                thisObjSpriteRenderer.sprite = belts[19];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[24])
                            {
                                thisObjSpriteRenderer.sprite = belts[4];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[30])
                            {
                                thisObjSpriteRenderer.sprite = belts[8];
                            }

                            break;
                    }

                    break;

                case "south":

                    switch (objDir)
                    {
                        case "up":

                            if (thisObjSpriteRenderer.sprite == belts[0])
                            {
                                thisObjSpriteRenderer.sprite = belts[10];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[2])
                            {
                                thisObjSpriteRenderer.sprite = belts[24];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[3])
                            {
                                thisObjSpriteRenderer.sprite = belts[25];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[8])
                            {
                                thisObjSpriteRenderer.sprite = belts[20];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[9])
                            {
                                thisObjSpriteRenderer.sprite = belts[21];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[12])
                            {
                                thisObjSpriteRenderer.sprite = belts[11];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[13])
                            {
                                thisObjSpriteRenderer.sprite = belts[11];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[14])
                            {
                                thisObjSpriteRenderer.sprite = belts[11];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[20])
                            {
                                thisObjSpriteRenderer.sprite = belts[4];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[21])
                            {
                                thisObjSpriteRenderer.sprite = belts[5];
                            }

                            break;

                        case "down":

                            if (thisObjSpriteRenderer.sprite == belts[1])
                            {
                                thisObjSpriteRenderer.sprite = belts[17];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[15])
                            {
                                thisObjSpriteRenderer.sprite = belts[16];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[26])
                            {
                                thisObjSpriteRenderer.sprite = belts[6];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[27])
                            {
                                thisObjSpriteRenderer.sprite = belts[7];
                            }

                            break;

                        case "right":

                            if (thisObjSpriteRenderer.sprite == belts[1])
                            {
                                thisObjSpriteRenderer.sprite = belts[17];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[15])
                            {
                                thisObjSpriteRenderer.sprite = belts[16];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[26])
                            {
                                thisObjSpriteRenderer.sprite = belts[6];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[27])
                            {
                                thisObjSpriteRenderer.sprite = belts[7];
                            }

                            break;

                        case "left":

                            if (thisObjSpriteRenderer.sprite == belts[1])
                            {
                                thisObjSpriteRenderer.sprite = belts[17];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[15])
                            {
                                thisObjSpriteRenderer.sprite = belts[16];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[26])
                            {
                                thisObjSpriteRenderer.sprite = belts[6];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[27])
                            {
                                thisObjSpriteRenderer.sprite = belts[7];
                            }

                            break;
                    }

                    break;

                case "west":

                    switch (objDir)
                    {
                        case "right":

                            if (thisObjSpriteRenderer.sprite == belts[0])
                            {
                                thisObjSpriteRenderer.sprite = belts[29];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[1])
                            {
                                thisObjSpriteRenderer.sprite = belts[27];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[2])
                            {
                                thisObjSpriteRenderer.sprite = belts[18];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[4])
                            {
                                thisObjSpriteRenderer.sprite = belts[20];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[6])
                            {
                                thisObjSpriteRenderer.sprite = belts[17];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[8])
                            {
                                thisObjSpriteRenderer.sprite = belts[18];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[12])
                            {
                                thisObjSpriteRenderer.sprite = belts[14];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[14])
                            {
                                thisObjSpriteRenderer.sprite = belts[13];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[20])
                            {
                                thisObjSpriteRenderer.sprite = belts[19];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[17])
                            {
                                thisObjSpriteRenderer.sprite = belts[7];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[10])
                            {
                                thisObjSpriteRenderer.sprite = belts[13];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[11])
                            {
                                thisObjSpriteRenderer.sprite = belts[13];
                            }

                            break;

                        case "left":

                            if (thisObjSpriteRenderer.sprite == belts[3])
                            {
                                thisObjSpriteRenderer.sprite = belts[21];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[23])
                            {
                                thisObjSpriteRenderer.sprite = belts[22];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[25])
                            {
                                thisObjSpriteRenderer.sprite = belts[5];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[31])
                            {
                                thisObjSpriteRenderer.sprite = belts[9];
                            }

                            break;

                        case "up":

                            if (thisObjSpriteRenderer.sprite == belts[3])
                            {
                                thisObjSpriteRenderer.sprite = belts[21];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[23])
                            {
                                thisObjSpriteRenderer.sprite = belts[22];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[25])
                            {
                                thisObjSpriteRenderer.sprite = belts[5];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[31])
                            {
                                thisObjSpriteRenderer.sprite = belts[13];
                            }

                            break;

                        case "down":

                            if (thisObjSpriteRenderer.sprite == belts[3])
                            {
                                thisObjSpriteRenderer.sprite = belts[21];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[23])
                            {
                                thisObjSpriteRenderer.sprite = belts[22];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[25])
                            {
                                thisObjSpriteRenderer.sprite = belts[5];
                            }

                            else if (thisObjSpriteRenderer.sprite == belts[31])
                            {
                                thisObjSpriteRenderer.sprite = belts[13];
                            }

                            break;
                    }

                    break;
            }
        }

        else
        {
            switch (exitingObjPos)
            {
                case "north":

                    if (thisObjSpriteRenderer.sprite == belts[11])
                    {
                        thisObjSpriteRenderer.sprite = belts[10];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[14])
                    {
                        thisObjSpriteRenderer.sprite = belts[0];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[15])
                    {
                        thisObjSpriteRenderer.sprite = belts[1];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[16])
                    {
                        thisObjSpriteRenderer.sprite = belts[17];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[14])
                    {
                        thisObjSpriteRenderer.sprite = belts[0];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[8])
                    {
                        thisObjSpriteRenderer.sprite = belts[20];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[9])
                    {
                        thisObjSpriteRenderer.sprite = belts[21];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[12])
                    {
                        thisObjSpriteRenderer.sprite = belts[28];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[13])
                    {
                        thisObjSpriteRenderer.sprite = belts[29];
                    }

                    break;

                case "east":

                    if (thisObjSpriteRenderer.sprite == belts[19])
                    {
                        thisObjSpriteRenderer.sprite = belts[18];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[20])
                    {
                        thisObjSpriteRenderer.sprite = belts[2];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[22])
                    {
                        thisObjSpriteRenderer.sprite = belts[21];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[23])
                    {
                        thisObjSpriteRenderer.sprite = belts[3];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[6])
                    {
                        thisObjSpriteRenderer.sprite = belts[17];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[7])
                    {
                        thisObjSpriteRenderer.sprite = belts[17];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[12])
                    {
                        thisObjSpriteRenderer.sprite = belts[14];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[4])
                    {
                        thisObjSpriteRenderer.sprite = belts[24];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[8])
                    {
                        thisObjSpriteRenderer.sprite = belts[30];
                    }

                    break;

                case "south":

                    if (thisObjSpriteRenderer.sprite == belts[10])
                    {
                        thisObjSpriteRenderer.sprite = belts[0];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[11])
                    {
                        thisObjSpriteRenderer.sprite = belts[14];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[16])
                    {
                        thisObjSpriteRenderer.sprite = belts[15];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[17])
                    {
                        thisObjSpriteRenderer.sprite = belts[1];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[4])
                    {
                        thisObjSpriteRenderer.sprite = belts[20];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[5])
                    {
                        thisObjSpriteRenderer.sprite = belts[21];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[6])
                    {
                        thisObjSpriteRenderer.sprite = belts[26];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[7])
                    {
                        thisObjSpriteRenderer.sprite = belts[27];
                    }

                    break;

                case "west":

                    if (thisObjSpriteRenderer.sprite == belts[18])
                    {
                        thisObjSpriteRenderer.sprite = belts[2];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[19])
                    {
                        thisObjSpriteRenderer.sprite = belts[20];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[21])
                    {
                        thisObjSpriteRenderer.sprite = belts[3];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[22])
                    {
                        thisObjSpriteRenderer.sprite = belts[23];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[13])
                    {
                        thisObjSpriteRenderer.sprite = belts[14];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[5])
                    {
                        thisObjSpriteRenderer.sprite = belts[25];
                    }

                    else if (thisObjSpriteRenderer.sprite == belts[9])
                    {
                        thisObjSpriteRenderer.sprite = belts[31];
                    }

                    break;
            }
        }
        
        // Check near belts' psoitions and directions - change this belt's sprite
    }

    void AnimateBeltArrows()
    {
        #region Curved Animation
        if (thisObjSpriteRenderer.sprite == belts[4] || thisObjSpriteRenderer.sprite == belts[5] || thisObjSpriteRenderer.sprite == belts[6] || thisObjSpriteRenderer.sprite == belts[7] || thisObjSpriteRenderer.sprite == belts[8] || thisObjSpriteRenderer.sprite == belts[9] || thisObjSpriteRenderer.sprite == belts[12] || thisObjSpriteRenderer.sprite == belts[13] || thisObjSpriteRenderer.sprite == belts[24] || thisObjSpriteRenderer.sprite == belts[25] || thisObjSpriteRenderer.sprite == belts[26] || thisObjSpriteRenderer.sprite == belts[27] || thisObjSpriteRenderer.sprite == belts[28] || thisObjSpriteRenderer.sprite == belts[29] || thisObjSpriteRenderer.sprite == belts[30] || thisObjSpriteRenderer.sprite == belts[31])
        {
            isCurved = true;
        }

        else
        {
            isCurved = false;

            for (int i =0; i < transform.GetChild(0).GetChild(0).childCount; i++)
            {
                transform.GetChild(0).GetChild(0).GetChild(i).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
        #endregion

        #region Belt Mask
        if (thisObjSpriteRenderer.sprite == belts[11] || thisObjSpriteRenderer.sprite == belts[16] || thisObjSpriteRenderer.sprite == belts[19] || thisObjSpriteRenderer.sprite == belts[22])
        {
            transform.GetChild(0).GetComponent<SpriteMask>().sprite = beltMasks[4];
        }

        else if (thisObjSpriteRenderer.sprite == belts[2] || thisObjSpriteRenderer.sprite == belts[3])
        {
            transform.GetChild(0).GetComponent<SpriteMask>().sprite = beltMasks[5];
        }

        else if (thisObjSpriteRenderer.sprite == belts[0] || thisObjSpriteRenderer.sprite == belts[1])
        {
            transform.GetChild(0).GetComponent<SpriteMask>().sprite = beltMasks[6];
        }

        else if (thisObjSpriteRenderer.sprite == belts[4] || thisObjSpriteRenderer.sprite == belts[5] || thisObjSpriteRenderer.sprite == belts[6] || thisObjSpriteRenderer.sprite == belts[7] || thisObjSpriteRenderer.sprite == belts[8] || thisObjSpriteRenderer.sprite == belts[9] || thisObjSpriteRenderer.sprite == belts[12] || thisObjSpriteRenderer.sprite == belts[13])
        {
            transform.GetChild(0).GetComponent<SpriteMask>().sprite = beltMasks[0];
        }

        else if (thisObjSpriteRenderer.sprite == belts[18] || thisObjSpriteRenderer.sprite == belts[20] || thisObjSpriteRenderer.sprite == belts[21] || thisObjSpriteRenderer.sprite == belts[23])
        {
            transform.GetChild(0).GetComponent<SpriteMask>().sprite = beltMasks[1];
        }

        else if (thisObjSpriteRenderer.sprite == belts[10] || thisObjSpriteRenderer.sprite == belts[14] || thisObjSpriteRenderer.sprite == belts[15] || thisObjSpriteRenderer.sprite == belts[17])
        {
            transform.GetChild(0).GetComponent<SpriteMask>().sprite = beltMasks[3];
        }

        else if (thisObjSpriteRenderer.sprite == belts[24] || thisObjSpriteRenderer.sprite == belts[25] || thisObjSpriteRenderer.sprite == belts[26] || thisObjSpriteRenderer.sprite == belts[27] || thisObjSpriteRenderer.sprite == belts[28] || thisObjSpriteRenderer.sprite == belts[29] || thisObjSpriteRenderer.sprite == belts[30] || thisObjSpriteRenderer.sprite == belts[31])
        {
            transform.GetChild(0).GetComponent<SpriteMask>().sprite = beltMasks[2];
        }

        //else if (connected curved)
        //{
        //    transform.GetChild(0).GetComponent<SpriteMask>().sprite = beltMasks[2];
        //}
        #endregion

        #region Belt Arrows Direction
        if (thisObjSpriteRenderer.sprite == belts[0] || thisObjSpriteRenderer.sprite == belts[14])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[10] || thisObjSpriteRenderer.sprite == belts[11])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 180f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[18])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[19])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, -90f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[20])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[1] || thisObjSpriteRenderer.sprite == belts[16] || thisObjSpriteRenderer.sprite == belts[17])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if(thisObjSpriteRenderer.sprite == belts[15])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 180f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[22])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 90f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[21])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, -90f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[23])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[2])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, -90f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[3])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, -90f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 180f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[4] || thisObjSpriteRenderer.sprite == belts[24])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[8] || thisObjSpriteRenderer.sprite == belts[30])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 90f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 180f, -90f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[5] || thisObjSpriteRenderer.sprite == belts[25])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[9] || thisObjSpriteRenderer.sprite == belts[31])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[6] || thisObjSpriteRenderer.sprite == belts[26])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 180f, -90f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[7] || thisObjSpriteRenderer.sprite == belts[27])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, -90f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[12] || thisObjSpriteRenderer.sprite == belts[28])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 90f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if (thisObjSpriteRenderer.sprite == belts[13] || thisObjSpriteRenderer.sprite == belts[29])
        {
            transform.GetChild(0).transform.localEulerAngles = new Vector3(0f, 180f, 90f);
            transform.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        #endregion
    }

    private void Update()
    {
        AnimateBeltArrows();

        if (isCurved != wasCurved)
        {
            animation.clip = beltAnimations[isCurved ? 1 : 0];

            animation.Play();
        }

        wasCurved = isCurved;

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = ((int)(transform.position.y * -100));

        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = ((int)(transform.position.y * -100)) + 1;
        }

        transform.GetChild(0).GetComponent<SpriteMask>().frontSortingOrder = ((int)(transform.position.y * -100)) + 2;
        transform.GetChild(0).GetComponent<SpriteMask>().backSortingOrder = (int)(transform.position.y * -100);

        //animator.GetCurrentAnimatorStateInfo(0).normalizedTime = Time.frameCount;

        //Debug.Log(clipInfo[0].clip.name);
        //(clipInfo[0].clip.name, 0, baseBelt.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime % 1)
    }
}
