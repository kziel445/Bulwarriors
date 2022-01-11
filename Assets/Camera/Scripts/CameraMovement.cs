using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 10f;
    [SerializeField] private float zoom = 3f;
    [SerializeField] private float xMinBound = 0, xMaxBound = 33;
    [SerializeField] private float yMinBound = 0, yMaxBound = 33;

    private void Start()
    {
        zoom = zoomController (zoom);
    }
    void Update()
    {
        Vector3 position = transform.position;
        if (Input.GetKey(KeyCode.W)) position.y += cameraSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) position.y -= cameraSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) position.x -= cameraSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) position.x += cameraSpeed * Time.deltaTime;
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && Time.timeScale != 0)
        {
            zoom-=0.2f;
            zoom = zoomController(zoom);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Time.timeScale != 0)
        {
            zoom += 0.2f;
            zoom = zoomController(zoom);
        }
        GetComponent<Camera>().orthographicSize = zoom;
        transform.position = new Vector3(
            Mathf.Clamp(position.x, xMinBound, xMaxBound),
            Mathf.Clamp(position.y, yMinBound, yMaxBound),
            position.z
            );
    }
    private float zoomController(float zoom)
    {
        if (zoom > 10f) return 10f;
        if (zoom < 0.4f) return 0.4f;
        return zoom;
    }
}
