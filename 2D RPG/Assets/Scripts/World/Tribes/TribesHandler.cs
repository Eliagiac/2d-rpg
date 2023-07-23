using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribesHandler : MonoBehaviour
{
    #region Public References
    Transform[] tribes;
    List<Transform> _tribes;
    TribesManager[] tribesManager;

    public GameObject tribesParent;
    #endregion

    private void Update()
    {
        if (Time.frameCount == 1)
        {
            tribesParent.GetComponent<TribesGenerator>();

            tribes = new Transform[TribesGenerator._ammOfTribesTier1];

            for (int i = 0; i < tribesParent.transform.childCount; i++)
            {
                tribes[i] = tribesParent.transform.GetChild(i);
            } // Get reference to the tribes

            tribesManager = new TribesManager[tribes.Length];

            for (int i = 0; i < tribes.Length; i++)
            {
                tribesManager[i] = tribes[i].GetComponent<TribesManager>();

                tribesManager[i].AIDestinationSetter = tribes[i].GetComponentsInChildren<Pathfinding.AIDestinationSetter>();
            } // Get reference to AIDestinationSetter
        } // Happens immediatly after start
    } // Get reference to enemyMovment after start
}
