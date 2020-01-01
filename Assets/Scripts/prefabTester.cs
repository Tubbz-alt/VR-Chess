using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prefabTester : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject prefab;
    public GameObject target;

    public Button moveButton, attackButton, restartButton;

    ActionManager am;

    public Vector3 spawnPosition;

    GameObject piece;
    void Start()
    {
        moveButton.GetComponent<Button>().onClick.AddListener(Move);
        attackButton.GetComponent<Button>().onClick.AddListener(Attack);
        restartButton.GetComponent<Button>().onClick.AddListener(CreatePiece);

        CreatePiece();


    }

    void CreatePiece() {
        if (piece != null)
            Destroy(piece);
        piece = Instantiate(prefab, spawnPosition, Quaternion.identity);

        am = piece.GetComponent<ActionManager>();
    }

    void Move() {
        am.MoveTo(target.transform.position.x, target.transform.position.z, false);
    }
    void Attack() {
        am.MoveTo(target.transform.position.x, target.transform.position.z, true);
    }

}
