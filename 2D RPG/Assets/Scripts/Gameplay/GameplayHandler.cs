using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayHandler : MonoBehaviour
{
    public bool scanOnStart = true;

    int whenToScan = 1;

    public Transform appleTreesParent;

    Transform[] obstacles;

    int[] ammOfObstacles;

    void Update()
    {
        if (scanOnStart && Time.frameCount == whenToScan)
        {
            AstarPath.active.Scan();
        }

        if (Time.frameCount == 1)
        {
            obstacles = new Transform[1]; // + any other obstacle that will be added in the future
            ammOfObstacles = new int[1]; // + any other obstacle that will be added in the future

            obstacles[0] = appleTreesParent;

            for (int i = 0; i < obstacles.Length; i++)
            {
                ammOfObstacles[i] = obstacles[i].childCount;
            }
        }

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i].childCount < ammOfObstacles[i])
            {
                ammOfObstacles[i] = obstacles[i].childCount;

                AstarPath.active.Scan();
            }
        }
    }
}