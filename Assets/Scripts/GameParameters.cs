using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameParameters : MonoBehaviour
{
    [System.Serializable] 
    public struct TeamPrefabs {
        public Team team;
        public GameObject Pawn;
        public GameObject Bishop;
        public GameObject Rook;
        public GameObject Knight;
        public GameObject King;
        public GameObject Queen;
        public GameObject Map;
    }

    public List<TeamPrefabs> teamPrefabs;
    [SerializeField]
    public enum Team {Deads, Goblins, Samurais};

    public Team whiteTeam = Team.Deads;
    public Team blackTeam = Team.Goblins;


    private void Awake()
    {
        //If there is already a gameManager (coming from the previous scene) we destroy this one
        if (GameObject.FindGameObjectsWithTag("GameParameters").Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
