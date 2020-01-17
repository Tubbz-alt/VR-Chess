using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : MonoBehaviour
{
    public enum pieceType { KING, QUEEN, BISHOP, ROOK, KNIGHT, PAWN, UNKNOWN = -1};
    public enum playerColor { BLACK, WHITE, UNKNOWN = -1};

    [SerializeField] private pieceType _type = pieceType.UNKNOWN;
    [SerializeField] private playerColor _player = playerColor.UNKNOWN;
    public pieceType Type
    {
        get { return _type; }
        set { _type = value; }
    }
    public playerColor Player
    {
        get { return _player; }
        set { _player = value; }
    }

    public Sprite pieceImageWhite = null;
    public Sprite pieceImageBlack = null;

    private playerColor humanColor;

    public Vector2 position;
    private Vector3 moveTo;
    private GameManager manager;
    private ObjectPointer objectPointer;

    private MoveFactory factory = new MoveFactory(Board.Instance);
    private List<Move> moves = new List<Move>();

    private bool _hasMoved = false;
    public bool HasMoved
    {
        get { return _hasMoved; }
        set { _hasMoved = value; }
    }

    void Action()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) && _player == humanColor && manager.playerTurn)
        {
            moves.Clear();
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Highlight");
            foreach (GameObject o in objects)
            {
                Destroy(o);
            }
            moves = factory.GetMoves(this, position);
            foreach (Move move in moves)
            {
                if (move.pieceKilled == null)
                {
                    GameObject instance = Instantiate(Resources.Load("MoveCube")) as GameObject;
                    instance.transform.position = new Vector3(-move.secondPosition.Position.x*2+7, 0.275f, move.secondPosition.Position.y*2-7);
                    instance.transform.localScale = new Vector3(2, 0.15f, 2);
                    instance.name = "Movecube " + (-move.secondPosition.Position.x * 2 + 7) + " " + (move.secondPosition.Position.y * 2 - 7);
                    instance.GetComponent<Container>().move = move;
                }
                else if (move.pieceKilled != null)
                {
                    GameObject instance = Instantiate(Resources.Load("KillCube")) as GameObject;
                    instance.transform.position = new Vector3(-move.secondPosition.Position.x*2+7, 0.275f, move.secondPosition.Position.y*2-7);
                    instance.transform.localScale = new Vector3(2, 0.15f, 2);
                    instance.name = "KillCube " + (-move.secondPosition.Position.x * 2 + 7) + " " + (move.secondPosition.Position.y * 2 - 7);
                    instance.GetComponent<Container>().move = move;
                }
            }
            GameObject i = Instantiate(Resources.Load("CurrentPiece")) as GameObject;
            i.transform.position = new Vector3(this.transform.position.x, 0.275f, this.transform.position.z);
            i.transform.localScale = new Vector3(2, 0.15f, 2);
        }
    }


    public void MovePiece(Vector3 position, bool attack)
    {
        moveTo = position;
        this.GetComponent<ActionManager>().MoveTo(position.x, position.z, attack);
    }

    void printType()
    {
        GameObject go = new GameObject("type of " + this.name);
        go.transform.position = new Vector3(this.transform.position.x, 2.5f, this.transform.position.z);

        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = _player == playerColor.WHITE ? pieceImageWhite : pieceImageBlack;
    }

    public void destroyType()
    {
        if (GameObject.Find("type of " + this.name) != null)
        {
            GameObject go = GameObject.Find("type of " + this.name);
            Destroy(go);
        }
    }

    void Start()
    {
        moveTo = this.transform.position;
        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            humanColor = GameObject.Find("GameParameters").GetComponent<GameParameters>().playerStart ? playerColor.WHITE : playerColor.BLACK;
        }
       
        objectPointer = GameObject.Find("RaycastStart").GetComponent<ObjectPointer>();
    }

    void Update()
    {
        if (objectPointer != null && objectPointer.go.name.Equals(this.name))
        {
            if (GameObject.Find("type of " + this.name) == null)
            {
                printType();
            }

            if (!manager.kingDead)
            {
                Action();
            }
        }
        else 
        {
            destroyType();
        }
    }
}
