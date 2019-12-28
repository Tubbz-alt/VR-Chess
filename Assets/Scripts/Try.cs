using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Try : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update

    void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                string tag = hit.transform.gameObject.tag;
            }
        }        
    }
}
