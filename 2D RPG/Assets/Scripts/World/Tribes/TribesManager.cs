using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribesManager : MonoBehaviour
{
    #region Public References
    public static bool pursuit;

    public Pathfinding.AIDestinationSetter[] AIDestinationSetter;

    public static GameObject tribe;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < AIDestinationSetter.Length; i++)
            {
                AIDestinationSetter[i].FollowPlayer(true);
            } // Tell enemies in the tribe to pursuit
        }
    } // Tell enemies to pursuit player if he enters the tribe

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < AIDestinationSetter.Length; i++)
            {
                AIDestinationSetter[i].FollowPlayer(false);
            }
        }
    } // Tell enemies to stop pursuit player if he exits the tribe
}