using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotator : MonoBehaviour
{
    Vector3 mousePosition;
    // cam rotation x
	float x = 16;
    // cam rotation y
	float y = -30;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            x += 1 * Input.GetAxis("Mouse X");
            y += 1 * Input.GetAxis("Mouse Y");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            x -= 1 * Input.GetAxis("Horizontal");
            y += 1 * Input.GetAxis("Vertical");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }        
    }

    void LateUpdate()
    {
        // method for handling the camera rotation around the character
        y = Mathf.Clamp(y, -45, 15);
        transform.eulerAngles = new Vector3(y, x, 0.0f);
    }

}
