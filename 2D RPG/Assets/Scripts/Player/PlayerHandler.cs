using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public static float health = 100f;

    public static bool[] craftedObjects;

    public int craftableObjectsAmm = 1;

    private void Start()
    {
        craftedObjects = new bool[craftableObjectsAmm];
    }

    public static void DamagePlayer(float dmg)
    {
        health -= dmg;

        Debug.Log(health);
    }
}
