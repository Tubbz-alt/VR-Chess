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
    GameObject previewPieceBlack;
    GameObject previewPieceWhite;

    Vector3 previewPositionBlack;
    Vector3 previewPositionWhite;
    Quaternion previewRotation;


    int teamAmount;
    int teamIndexBlack = 0;
    int teamIndexWhite = 1;

    int prefabIndex = 0;

    Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        gameParameters = GameObject.FindGameObjectWithTag("GameParameters").GetComponent<GameParameters>();
        GameObject.Find("StartButtonBlack").GetComponent<Button>().onClick.AddListener(StartGameBlack);
        GameObject.Find("StartButtonWhite").GetComponent<Button>().onClick.AddListener(StartGameWhite);

        //Buttons to handle previews
        GameObject.Find("NextButtonBlack").GetComponent<Button>().onClick.AddListener(NextTeamBlack);
        GameObject.Find("PreviousButtonBlack").GetComponent<Button>().onClick.AddListener(PreviousTeamBlack);
        GameObject.Find("NextButtonWhite").GetComponent<Button>().onClick.AddListener(NextTeamWhite);
        GameObject.Find("PreviousButtonWhite").GetComponent<Button>().onClick.AddListener(PreviousTeamWhite);


        //GameObject.Find("PreviousPrefabButton").GetComponent<Button>().onClick.AddListener(PreviousPrefab);
        //GameObject.Find("NextPrefabButton").GetComponent<Button>().onClick.AddListener(NextPrefab);

        GameObject.Find("AttackButton").GetComponent<Button>().onClick.AddListener(Attack);
        GameObject.Find("MoveButton").GetComponent<Button>().onClick.AddListener(Move);

        //text = GameObject.Find("PrefabName").GetComponent<Text>();

        previewPositionBlack = new Vector3(3f, 0, 0);
        previewPositionWhite = new Vector3(-3f, 0, 0);
        previewRotation = Quaternion.Euler(new Vector3(0, 180f, 0));


        teamPrefabs = gameParameters.teamPrefabs;
        teamAmount = teamPrefabs.Count;

        NewPrefabBlack();
        NewPrefabWhite();
    }

    private void Attack()
    {
        previewPieceBlack.GetComponent<Animator>().SetTrigger("Attack");
        previewPieceWhite.GetComponent<Animator>().SetTrigger("Attack");
    }

    private void Move()
    {
        StartCoroutine(MoveEnumerator());
    }

    IEnumerator MoveEnumerator() {
        previewPieceBlack.GetComponent<Animator>().SetBool("Moving", true);
        previewPieceWhite.GetComponent<Animator>().SetBool("Moving", true);
        yield return new WaitForSeconds(2f);
        previewPieceBlack.GetComponent<Animator>().SetBool("Moving", false);
        previewPieceWhite.GetComponent<Animator>().SetBool("Moving", false);
    }

    private void PreviousTeamBlack()
    {
        if (teamIndexBlack > 0)
            teamIndexBlack -= 1;
        else
            teamIndexBlack = teamAmount - 1;
        NewPrefabBlack();
    }

    private void NextTeamBlack()
    {
        if (teamIndexBlack < teamAmount - 1)
            teamIndexBlack += 1;
        else
            teamIndexBlack = 0;
        NewPrefabBlack();
    }

    private void PreviousTeamWhite()
    {
        if (teamIndexWhite > 0)
            teamIndexWhite -= 1;
        else
            teamIndexWhite = teamAmount - 1;
        NewPrefabWhite();
    }

    private void NextTeamWhite()
    {
        if (teamIndexWhite < teamAmount - 1)
            teamIndexWhite += 1;
        else
            teamIndexWhite = 0;
        NewPrefabWhite();
    }

    private void NewPrefabBlack() {
        GameObject.Destroy(previewPieceBlack);
        previewPieceBlack = Instantiate(teamPrefabs[teamIndexBlack].GetPrefab((GameParameters.Pieces)prefabIndex), previewPositionBlack, previewRotation);
        gameParameters.blackTeam = teamPrefabs[teamIndexBlack].team;
        //text.text =  ((GameParameters.Team)teamIndexBlack).ToString() +"\n" + ((GameParameters.Pieces)prefabIndex).ToString();
    }

    private void NewPrefabWhite()
    {
        GameObject.Destroy(previewPieceWhite);
        previewPieceWhite = Instantiate(teamPrefabs[teamIndexWhite].GetPrefab((GameParameters.Pieces)prefabIndex), previewPositionWhite, previewRotation);
        gameParameters.whiteTeam = teamPrefabs[teamIndexWhite].team;
        //text.text = ((GameParameters.Team)teamIndexWhite).ToString() + "\n" + ((GameParameters.Pieces)prefabIndex).ToString();
    }

    private void PreviousPrefab()
    {
        if (prefabIndex > 0)
            prefabIndex -= 1;
        else
            prefabIndex = 5;
        NewPrefabBlack();
    }

    private void NextPrefab() {
        if (prefabIndex < 4)
            prefabIndex += 1;
        else
            prefabIndex = 0;
        NewPrefabBlack();
    }


    void StartGameBlack() {
        gameParameters.playerStart = false;
        SceneManager.LoadScene("Game");
    }

    void StartGameWhite()
    {
        gameParameters.playerStart = true;
        SceneManager.LoadScene("Game");
    }
}
