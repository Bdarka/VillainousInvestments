using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int cameraSpeed;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * cameraSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * cameraSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.up * cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.up * cameraSpeed * Time.deltaTime;
        }
    }
}
