using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Start : MonoBehaviour
{
    public float yOffsetPiece = 0.2f;

    GameParameters gameParameters;
    List<GameParameters.TeamPrefabs> teamPrefabs;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
        teamPrefabs = gameParameters.teamPrefabs;

        BuildMap();
        BuildTeams();

        GameManager.boardSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildMap() {
        Instantiate(teamPrefabs[(int)gameParameters.whiteTeam].Map);
        Instantiate(teamPrefabs[(int)gameParameters.blackTeam].Map, Vector3.zero, Quaternion.Euler(new Vector3(0, 180, 0)));
    }


    void BuildTeams() {
        Transform pieces = new GameObject("Pieces").transform;
        Transform Pieces = new GameObject("WhitePieces").transform;
        Pieces.parent = pieces;
        BuildTeam(Pieces, gameParameters.whiteTeam, true);

        Pieces = new GameObject("BlackPieces").transform;
        Pieces.parent = pieces;
        BuildTeam(Pieces, gameParameters.blackTeam, false);
        Pieces.rotation = Quaternion.Euler(new Vector3(0, 180, 0));


    }

    void BuildTeam(Transform parent, GameParameters.Team team, bool isWhite) {
        GameObject go;
        
        int yPos = 1;
        if(isWhite) yPos = 6;


        for (int i = 1; i < 17; i += 2) {
            go = Instantiate(teamPrefabs[(int)team].Pawn, new Vector3(i - 8, yOffsetPiece, 5), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
            go.GetComponent<Piece>().position = new Vector2(GetXPos(isWhite, (i-1)/2), yPos);
            go.name = GetXPos(isWhite, (i - 1) / 2) + " " + yPos;
            go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;
        }

        if (isWhite)
        {
            yPos = 7;
            go = Instantiate(teamPrefabs[(int)team].King, new Vector3(-1, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
            go.GetComponent<Piece>().position = new Vector2(4, yPos);
            go.name = "4 " + yPos;
            go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;

            go = Instantiate(teamPrefabs[(int)team].Queen, new Vector3(1, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
            go.GetComponent<Piece>().position = new Vector2(3, yPos);
            go.name = "3 " + yPos;
            go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;
        }
        else
        {
            yPos = 0;
            go = Instantiate(teamPrefabs[(int)team].King, new Vector3(1, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
            go.GetComponent<Piece>().position = new Vector2(4, yPos);
            go.name = "4 " + yPos;
            go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;

            go = Instantiate(teamPrefabs[(int)team].Queen, new Vector3(-1, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
            go.GetComponent<Piece>().position = new Vector2(3, yPos);
            go.name = "3 " + yPos;
            go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;
        }

        go = Instantiate(teamPrefabs[(int)team].Rook, new Vector3(-7, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        go.GetComponent<Piece>().position = new Vector2(GetXPos(isWhite, 0), yPos);
        go.name = GetXPos(isWhite, 0) + " " + yPos;
        go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;

        go = Instantiate(teamPrefabs[(int)team].Rook, new Vector3(7, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        go.GetComponent<Piece>().position = new Vector2(GetXPos(isWhite, 7), yPos);
        go.name = GetXPos(isWhite, 7) + " " + yPos;
        go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;

        go = Instantiate(teamPrefabs[(int)team].Knight, new Vector3(-5, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        go.GetComponent<Piece>().position = new Vector2(GetXPos(isWhite, 1), yPos);
        go.name = GetXPos(isWhite, 1) + " " + yPos;
        go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;

        go = Instantiate(teamPrefabs[(int)team].Knight, new Vector3(5, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        go.GetComponent<Piece>().position = new Vector2(GetXPos(isWhite, 6), yPos);
        go.name = GetXPos(isWhite, 6) + " " + yPos;
        go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;

        go = Instantiate(teamPrefabs[(int)team].Bishop, new Vector3(-3, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        go.GetComponent<Piece>().position = new Vector2(GetXPos(isWhite, 2), yPos);
        go.name = GetXPos(isWhite, 2) + " " + yPos;
        go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;

        go = Instantiate(teamPrefabs[(int)team].Bishop, new Vector3(3, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        go.GetComponent<Piece>().position = new Vector2(GetXPos(isWhite, 5), yPos);
        go.name = GetXPos(isWhite, 5) + " " + yPos;
        go.GetComponent<Piece>().Player = isWhite ? Piece.playerColor.WHITE : Piece.playerColor.BLACK;
    }

    int GetXPos(bool isWhite, int x) {
        return isWhite ? 7 - x : x;
    }
}
