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
    }

    public GameObject GetPointedGo() {
        Debug.Log(go.name);
        return go;
    }
}
