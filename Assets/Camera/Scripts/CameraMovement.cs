using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;

public class CameraMovement : MonoBehaviour
{
    private float cameraSpeed = 10f;

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
            GetComponent<Camera>().orthographicSize-=0.2f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponent<Camera>().orthographicSize < 10)
        {
            GetComponent<Camera>().orthographicSize += 0.2f;
        }
        transform.position = position;
    }
}
