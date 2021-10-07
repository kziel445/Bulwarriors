using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;

public class CameraMovement : MonoBehaviour
{
    private float cameraSpeed = 10f;
    private float zoom = 1.6f;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            position.y += cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            position.y -= cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position.x += cameraSpeed * Time.deltaTime;
        }
        if(Input.GetAxis("Mouse ScrollWheel")>0 && GetComponent<Camera>().orthographicSize>0.4)
        {
            zoom-=0.2f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponent<Camera>().orthographicSize < 10)
        {
            zoom += 0.2f;
        }
        GetComponent<Camera>().orthographicSize = zoom;
        transform.position = position;
    }
}
