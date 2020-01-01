using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartMenuManager : MonoBehaviour
{  
    List<GameParameters.TeamPrefabs> teamPrefabs;
    GameParameters gameParameters;
    GameObject previewPiece;

    Vector3 previewPosition;
    Quaternion previewRotation;


    int teamAmount;
    int teamIndex = 0;

    int prefabIndex = 0;

    Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        gameParameters = GameObject.FindGameObjectWithTag("GameParameters").GetComponent<GameParameters>();
        GameObject.Find("StartButton").GetComponent<Button>().onClick.AddListener(StartGame);
        //Buttons to handle previews
        GameObject.Find("NextButton").GetComponent<Button>().onClick.AddListener(NextTeam);
        GameObject.Find("PreviousButton").GetComponent<Button>().onClick.AddListener(PreviousTeam);
        GameObject.Find("PreviousPrefabButton").GetComponent<Button>().onClick.AddListener(PreviousPrefab);
        GameObject.Find("NextPrefabButton").GetComponent<Button>().onClick.AddListener(NextPrefab);

        GameObject.Find("AttackButton").GetComponent<Button>().onClick.AddListener(Attack);
        GameObject.Find("MoveButton").GetComponent<Button>().onClick.AddListener(Move);

        text = GameObject.Find("PrefabName").GetComponent<Text>();

        previewPosition = new Vector3(0.7f, 0, 0);
        previewRotation = Quaternion.Euler(new Vector3(0, 180f, 0));


        teamPrefabs = gameParameters.teamPrefabs;
        teamAmount = teamPrefabs.Count;

        NewPrefab();
    }

    private void Attack()
    {
        previewPiece.GetComponent<Animator>().SetTrigger("Attack");
    }

    private void Move()
    {
        StartCoroutine(MoveEnumerator());
    }

    IEnumerator MoveEnumerator() {
        previewPiece.GetComponent<Animator>().SetBool("Moving", true);
        yield return new WaitForSeconds(2f);
        previewPiece.GetComponent<Animator>().SetBool("Moving", false);
    }

    private void PreviousTeam()
    {
        if (teamIndex > 0)
            teamIndex -= 1;
        else
            teamIndex = teamAmount - 1;
        NewPrefab();
    }

    private void NextTeam()
    {
        if (teamIndex < teamAmount - 1)
            teamIndex += 1;
        else
            teamIndex = 0;
        NewPrefab();
    }

    private void NewPrefab() {
        GameObject.Destroy(previewPiece);
        previewPiece = Instantiate(teamPrefabs[teamIndex].GetPrefab((GameParameters.Pieces)prefabIndex), previewPosition, previewRotation);
        gameParameters.whiteTeam = teamPrefabs[teamIndex].team;
        text.text =  ((GameParameters.Team)teamIndex).ToString() +"\n" + ((GameParameters.Pieces)prefabIndex).ToString();
    }

    private void PreviousPrefab()
    {
        if (prefabIndex > 0)
            prefabIndex -= 1;
        else
            prefabIndex = 5;
        NewPrefab();
    }

    private void NextPrefab() {
        if (prefabIndex < 4)
            prefabIndex += 1;
        else
            prefabIndex = 0;
        NewPrefab();
    }


    void StartGame() {
        SceneManager.LoadScene("Game");
    }
}
