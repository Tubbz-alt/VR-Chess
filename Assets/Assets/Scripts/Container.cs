using UnityEngine;
using System.Collections;

public class Container : MonoBehaviour
{
    public Move move;
    GameManager manager;
    private ObjectPointer objectPointer;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
        if (OVRInput.GetDown(OVRInput.Button.One) && move != null)
        {
            manager.SwapPieces(move);
        }
    }
}
