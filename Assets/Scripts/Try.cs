using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Try : MonoBehaviour
{
    public GameObject go;
    public float force = 1000;
    // Start is called before the first frame update

    void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        go = new GameObject();
    }

    private void Update()
    {
        RaycastHit hit;

        //transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);

        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider != null)
            {
                if (go != hit.collider.gameObject)
                {
                    go = hit.transform.gameObject;
                }
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Go clicked with b1 : " + go.tag);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Rigidbody rb = cube.AddComponent<Rigidbody>();
            cube.transform.position = transform.position;
            rb.AddForce(transform.forward * force);
        }
    }
}
