using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSens = 100f; // mouse sensitivity

    public Transform playerBody;

    float xRotation = 0f; // initial rotation

    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked; //locks cursor to screen 
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime; //creates variables for x and y mouse axis'
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY; //moves camera angle up and down  
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamps rotation of mouse to +90 and -90 degrees up and down respectively

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //tracks x rotation
        playerBody.Rotate(Vector3.up * mouseX); //rotates player body using mouse
        
    }
}
