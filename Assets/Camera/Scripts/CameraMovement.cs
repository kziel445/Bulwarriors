using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.MonoBehaviours;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 10f;
    [SerializeField] private float zoom = 1.6f;

    private void Start()
    {
        zoom = zoomController (zoom);
    }
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
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            zoom-=0.2f;
            zoom = zoomController(zoom);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            zoom += 0.2f;
            zoom = zoomController(zoom);
        }
        GetComponent<Camera>().orthographicSize = zoom;
        transform.position = position;
    }
    private float zoomController(float zoom)
    {
        if (zoom > 10f) return 10f;
        if (zoom < 0.4f) return 0.4f;
        return zoom;
    }
}
