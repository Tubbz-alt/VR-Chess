using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPointer : MonoBehaviour
{
    public GameObject pointer;

    public GameObject go;
    
    void Start()
    {
        GameObject go = null;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider != null)
            {
                go = hit.transform.gameObject;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One)) {
            GetPointedGo();
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Highlight");
            foreach (GameObject o in objects)
            {
                Destroy(o);
            }
        }
    }

    public GameObject GetPointedGo() {
        return go;
    }
}
