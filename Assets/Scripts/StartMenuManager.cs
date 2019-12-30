using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartMenuManager : MonoBehaviour
{
    int teamAmount;
    int teamIndex = 0;
    GameObject previewPiece;
    List<GameParameters.TeamPrefabs> teamPrefabs;
    GameParameters gameParameters;

    Vector3 previewPosition;
    Quaternion previewRotation;

    // Start is called before the first frame update
    void Start()
    {
        gameParameters = GameObject.FindGameObjectWithTag("GameParameters").GetComponent<GameParameters>();
        GameObject.Find("StartButton").GetComponent<Button>().onClick.AddListener(StartGame);
        GameObject.Find("NextButton").GetComponent<Button>().onClick.AddListener(NextTeam);
        GameObject.Find("PreviousButton").GetComponent<Button>().onClick.AddListener(PreviousTeam);
        GameObject.Find("AttackButton").GetComponent<Button>().onClick.AddListener(Attack);

        previewPosition = new Vector3(0.7f, 0, 0);
        previewRotation = Quaternion.Euler(new Vector3(0, 180f, 0));


        teamPrefabs = gameParameters.teamPrefabs;
        teamAmount = teamPrefabs.Count;

        NewTeam();
    }

    private void Attack()
    {
        previewPiece.GetComponent<Animator>().SetTrigger("Attack");
    }

    private void PreviousTeam()
    {
        if (teamIndex > 0)
            teamIndex -= 1;
        else
            teamIndex = teamAmount - 1;
        NewTeam();
    }

    private void NextTeam()
    {
        if (teamIndex < teamAmount - 1)
            teamIndex += 1;
        else
            teamIndex = 0;
        NewTeam();
    }

    private void NewTeam() {
        GameObject.Destroy(previewPiece);
        previewPiece = Instantiate(teamPrefabs[teamIndex].Queen, previewPosition, previewRotation);
        gameParameters.whiteTeam = teamPrefabs[teamIndex].team;
    }


    void StartGame() {
        SceneManager.LoadScene("Game");
    }
}
