using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Generic Variables
    public Transform player;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    Vector3 targetPosition;
    Vector3 targetPositionSmothed;
    #endregion

    private void FixedUpdate()
    {
        MoveCamera();
    } // Move the camera

    void MoveCamera()
    {
        targetPosition = player.position + offset;
        targetPositionSmothed = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.position = targetPositionSmothed;
    }
}
