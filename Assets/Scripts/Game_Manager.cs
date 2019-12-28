using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public float yOffsetPiece = 0.2f;

    GameParameters gameParameters;
    List<GameParameters.TeamPrefabs> teamPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        gameParameters = GameObject.Find("GameParameters").GetComponent<GameParameters>();
        teamPrefabs = gameParameters.teamPrefabs;

        BuildMap();
        BuildTeams();
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
        for (int i = 1; i < 17; i += 2) {
            Instantiate(teamPrefabs[(int)team].Pawn, new Vector3(i - 8, yOffsetPiece, 5), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        }
        Instantiate(teamPrefabs[(int)team].Rook, new Vector3(-7, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        Instantiate(teamPrefabs[(int)team].Rook, new Vector3(7, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        Instantiate(teamPrefabs[(int)team].Knight, new Vector3(-5, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        Instantiate(teamPrefabs[(int)team].Knight, new Vector3(5, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        Instantiate(teamPrefabs[(int)team].Bishop, new Vector3(-3, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        Instantiate(teamPrefabs[(int)team].Bishop, new Vector3(3, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);

        if (isWhite)
        {
            Instantiate(teamPrefabs[(int)team].King, new Vector3(-1, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
            Instantiate(teamPrefabs[(int)team].Queen, new Vector3(1, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        }
        else 
        {
            Instantiate(teamPrefabs[(int)team].King, new Vector3(1, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
            Instantiate(teamPrefabs[(int)team].Queen, new Vector3(-1, yOffsetPiece, 7), Quaternion.Euler(new Vector3(0, 180, 0)), parent);
        }
        
    }
}
