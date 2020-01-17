using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    AlphaBeta ab = new AlphaBeta();
    public bool kingDead = false;
    float timer = 0;
    float delay = 0;
    static Board _board;

    public bool playerTurn;

    void Start () {
        playerTurn = GameObject.Find("GameParameters").GetComponent<GameParameters>().playerStart;
    }

	void Update ()
    {
        if (kingDead)
        {
            Debug.Log("WINNER!");
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
        else
        {
            if (!playerTurn && timer < delay)
            {
                timer += Time.deltaTime;
            }
            else if (!playerTurn && timer >= delay)
            {
                Move move = ab.GetMove();
                _DoAIMove(move);
                timer = 0;
            }
        }
	}

    public static void boardSetup() {
        _board = Board.Instance;
        _board.SetupBoard();
    }

    void _DoAIMove(Move move)
    {
        Tile firstPosition = move.firstPosition;
        Tile secondPosition = move.secondPosition;

        if (secondPosition.CurrentPiece && secondPosition.CurrentPiece.Type == Piece.pieceType.KING)
        {
            SwapPieces(move);
            kingDead = true;
        }
        else
        {
            SwapPieces(move);
        }
    }

    public void SwapPieces(Move move)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Highlight");
        foreach (GameObject o in objects)
        {
            Destroy(o);
        }

        Tile firstTile = move.firstPosition;
        Tile secondTile = move.secondPosition;

        float x1 = -move.firstPosition.Position.x * 2 + 7;
        float z1 = move.firstPosition.Position.y * 2 - 7;
        float x2 = -move.secondPosition.Position.x * 2 + 7;
        float z2 = move.secondPosition.Position.y * 2 - 7;

        delay = 3 + Mathf.Sqrt((x1 - x2) * (x1 - x2) + (z1 - z2) * (z1 - z2)) / firstTile.CurrentPiece.GetComponent<ActionManager>().movingSpeed;

        if (secondTile.CurrentPiece != null)
        {
            firstTile.CurrentPiece.MovePiece(new Vector3(x2, 0.275f, z2), true);

            StartCoroutine(DestroyPiece(secondTile.CurrentPiece));            
        }
        else
        {
            firstTile.CurrentPiece.MovePiece(new Vector3(x2, 0.275f, z2), false);
        }

        secondTile.CurrentPiece = move.pieceMoved;
        firstTile.CurrentPiece = null;
        secondTile.CurrentPiece.position = secondTile.Position;
        secondTile.CurrentPiece.HasMoved = true;

        playerTurn = !playerTurn;
    }

    IEnumerator DestroyPiece(Piece pieceToDestroy)
    {
        yield return new WaitForSeconds(delay-2.3f);
        pieceToDestroy.destroyType();
        Destroy(pieceToDestroy.gameObject);

        if (pieceToDestroy.Type == Piece.pieceType.KING)
        {
            kingDead = true;
        }
    }
}
