using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    AlphaBeta ab = new AlphaBeta();
    private bool _kingDead = false;
    float timer = 0;
    static Board _board;

	void Start () {}

	void Update ()
    {
        if (_kingDead)
        {
            Debug.Log("WINNER!");
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
        if (!playerTurn && timer < 3)
        {
            timer += Time.deltaTime;
        }
        else if (!playerTurn && timer >= 3)
        {
            Move move = ab.GetMove();
            _DoAIMove(move);
            timer = 0;
        }
	}

    public bool playerTurn = true;

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
            _kingDead = true;
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

        firstTile.CurrentPiece.MovePiece(new Vector3(-move.secondPosition.Position.x*2+7, 0, move.secondPosition.Position.y*2-7));

        if (secondTile.CurrentPiece != null)
        {
            if (secondTile.CurrentPiece.Type == Piece.pieceType.KING)
            {
                _kingDead = true;
            }
                
            Destroy(secondTile.CurrentPiece.gameObject);
        }
            

        secondTile.CurrentPiece = move.pieceMoved;
        firstTile.CurrentPiece = null;
        secondTile.CurrentPiece.position = secondTile.Position;
        secondTile.CurrentPiece.HasMoved = true;

        playerTurn = !playerTurn;
    }
}
