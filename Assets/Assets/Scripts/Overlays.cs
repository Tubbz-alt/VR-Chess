using UnityEngine;
using System.Collections;

public class Overlays : MonoBehaviour
{
    private ObjectPointer objectPointer;

    void Start()
    {
        objectPointer = GameObject.Find("RaycastStart").GetComponent<ObjectPointer>();
    }

    void Update()
    {
        if (objectPointer.go.name.Equals(this.name))
        {
            Action();
        }
    }

    void Action()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Highlight");
            foreach (GameObject o in objects)
            {
                Destroy(o);
            }
        }
    }
}
