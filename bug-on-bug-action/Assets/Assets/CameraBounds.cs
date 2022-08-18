using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public BoxCollider2D mapBounds;

    private float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float camOrthsize;
    private float cameraRatio;
    public Camera cam;

    private void Start()
    {
        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;
        camOrthsize = cam.orthographicSize;
        cameraRatio = camOrthsize * cam.aspect;
    }
    // Update is called once per frame
    void Update()
    {
        camY = Mathf.Clamp(transform.position.y, yMin + camOrthsize, yMax - camOrthsize);
        camX = Mathf.Clamp(transform.position.x, xMin + cameraRatio, xMax - cameraRatio);
        Vector3 newPos = new Vector3(camX, camY, transform.position.z);
        transform.position = newPos;
    }
}