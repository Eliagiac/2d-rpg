using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    #region Generic Variables
    public Camera cam;

    private float scrollWheelMovement;

    public float zoomSpeed = 50f;

    bool canZoomIn = true;
    bool canZoomOut = true;

    public int minZoom = 6;
    public int maxZoom = 19;

    private int none = 0;
    #endregion

    void Update()
    {
        scrollWheelMovement = Input.GetAxisRaw("Mouse ScrollWheel");
    } // Get mouse wheel input

    private void FixedUpdate()
    {
        if (!MenuHandler.isMenuOpen)
        {
            Zoom();
        }
    } // Zoom camera

    void Zoom()
    {
        #region Zoom
        if (cam.orthographicSize >= minZoom && cam.orthographicSize <= maxZoom)
        {
            if (scrollWheelMovement > none && canZoomIn)
            {
                cam.orthographicSize -= scrollWheelMovement * Time.fixedDeltaTime * zoomSpeed;

                canZoomOut = true;
            }

            else if (scrollWheelMovement < none && canZoomOut)
            {
                cam.orthographicSize -= scrollWheelMovement * Time.fixedDeltaTime * zoomSpeed;

                canZoomIn = true;
            }                
        }
        #endregion

        #region Zoom - Exceptions
        else if (scrollWheelMovement > none && canZoomIn && canZoomOut == false)
        {
            cam.orthographicSize -= scrollWheelMovement * Time.fixedDeltaTime * zoomSpeed;

            if (cam.orthographicSize < 19)
            {
                canZoomOut = true;
            }
        }

        else if (scrollWheelMovement < none && canZoomOut && canZoomIn == false)
        {
            cam.orthographicSize -= scrollWheelMovement * Time.fixedDeltaTime * zoomSpeed;

            if (cam.orthographicSize > 6)
            {
                canZoomIn = true;
            }
        }

        else if (cam.orthographicSize < 6)
        {
            canZoomIn = false;
        }
     
        else if (cam.orthographicSize > 19)
        {
            canZoomOut = false;
        }
        #endregion

        if (cam.orthographicSize < minZoom)
        {
            cam.orthographicSize = minZoom;
        }

        else if (cam.orthographicSize > maxZoom)
        {
            cam.orthographicSize = maxZoom;
        }
    }
}