using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    public Material whiteMat, blackMat;
    public float yOffset = -0.3f;

    GameObject whiteCube, blackCube;


    void Start()
    {
        GameObject parent = new GameObject("BoardCubes");
        parent.transform.parent = transform;
        Transform parentTransform = parent.transform;
        

        whiteCube = CreateCube(whiteMat);
        blackCube = CreateCube(blackMat);

        for (int x = 0; x < 8; x++) {
            for (int z = 0; z < 8; z++)
            {
                if ((x + z) % 2 == 0) 
                    Instantiate(whiteCube, new Vector3(2 * x, 0, 2 * z), Quaternion.identity, parentTransform);
                else
                    Instantiate(blackCube, new Vector3(2 * x, 0, 2 * z), Quaternion.identity, parentTransform);
            }
        }
        Destroy(whiteCube);
        Destroy(blackCube);

        parentTransform.position = new Vector3(-7, yOffset, -7);
    }


    GameObject CreateCube(Material mat) {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(2, 1, 2);
        cube.GetComponent<MeshRenderer>().material = mat;

        return cube;
    }
}
