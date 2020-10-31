using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class MainController : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject energizerPrefab;
    public GameObject pacmanPrefab;
    public GameObject blinkyPrefab;
    public GameObject clydePrefab;
    public GameObject inkyPrefab;
    public GameObject pinkyPrefab;

    // public GameObject tileMap;
    // public Tilemap tileMap;

    // Tilemap tilemap;
    public int[] scatterLen = {0, 0, 0, 0};
    public int[] chaseLen   = {0, 0, 0};

    
    
    public Tilemap dotTilemap;
    public Tilemap pathTilemap;

    private GameObject highScoreTitle;
    // public TMP_Text highScoreTitle;
    private GameObject highScoreText;
    private GameObject scoreTitle;
    private GameObject scoreText;

    public int score = 0;
    public int highScore = 0;

    public int level = 1;

    public int[] dotLimit = {0, 0, 0, 0}; // Blinky, Pinky, Inky, Clyde
    public int PinkyDotCount = 0;
    public int InkyDotCount = 0;
    public int ClydeDotCount = 0;
    public int GlobalDotCount = 0; // Pinky == 7, Inky == 17, Clyde ==32

    public int waitTime;
    public int eatenDots = 0;



    // Start is called before the first frame update
    void Awake()
    {
        highScoreTitle = GameObject.Find("HighScoreTitle");
        highScoreText = GameObject.Find("HighScoreText");
        scoreTitle = GameObject.Find("ScoreTitle");
        scoreText = GameObject.Find("ScoreText");

        highScore = PlayerPrefs.GetInt("highScore");
        
        pacmanPrefab = GameObject.Find("PacmanPrefab");
        blinkyPrefab = GameObject.Find("blinky");
        clydePrefab = GameObject.Find("clyde");
        inkyPrefab = GameObject.Find("inky");
        pinkyPrefab = GameObject.Find("pinky");

		Debug.Log(" level:" + level);

        initLevel(level);

 
    }
    void Start()
    {}
    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<TMP_Text>().text = "" + (score);
        // highScoreTitle.text = "Extra Lives: ";
        if (highScore < score){
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        highScoreText.GetComponent<TMP_Text>().text = "" + (highScore);
        if ( dotLimit[1] == eatenDots){pinkyPrefab.SendMessage("getOut");}
        if ( dotLimit[2] == eatenDots){inkyPrefab.SendMessage("getOut");}
        if ( dotLimit[3] == eatenDots){clydePrefab.SendMessage("getOut");}

    }

    public void initLevel(int l){
        setDotLimit(level);
        setWaitTime(level);
        setScatterTime(level);
        setChaseTime(level);

        pacmanPrefab.SendMessage("setLevel", level);
        blinkyPrefab.SendMessage("setLevel", level);
        clydePrefab.SendMessage("setLevel", level);
        inkyPrefab.SendMessage("setLevel", level);
        pinkyPrefab.SendMessage("setLevel", level);


        for (int x = dotTilemap.cellBounds.xMin; x < dotTilemap.cellBounds.xMax; x++) {
            for (int y = dotTilemap.cellBounds.yMin; y < dotTilemap.cellBounds.yMax; y++) {
                // TileBase tile = allTiles[n];
                Vector3Int localPlace = (new Vector3Int(x, y, (int)dotTilemap.transform.position.y));
                Vector3 place = dotTilemap.CellToWorld(localPlace);
                if (dotTilemap.HasTile(localPlace)) {
                    // Debug.Log(" tile:" + dotTilemap.GetTile(localPlace).name);
                    if (dotTilemap.GetTile(localPlace).name == "dot") {
                        GameObject newDot = Instantiate(dotPrefab);
                        newDot.transform.position = place +  new Vector3(0.5f,0.5f, 0);
                    }
                    else if (dotTilemap.GetTile(localPlace).name == "energizer")
                    {
                        GameObject newEnergizer = Instantiate(energizerPrefab);
                        newEnergizer.transform.position = place +  new Vector3(0.5f,0.5f, 0);
                    }
                }
            }
        }

        StartCoroutine(co1());
        StartCoroutine(co2());
        StartCoroutine(co3()); //
        StartCoroutine(co4());
        StartCoroutine(co5());
        StartCoroutine(co6());
        StartCoroutine(co7());

    }

    IEnumerator co1()
    {
        yield return new WaitForSeconds(scatterLen[0]);
        setMode(1);
    }

    IEnumerator co2()
    {
        yield return new WaitForSeconds(chaseLen[0]);
        setMode(0);
    }

    IEnumerator co3()
    {
        yield return new WaitForSeconds(scatterLen[1]);
        setMode(1);
    }


    IEnumerator co4()
    {
        yield return new WaitForSeconds(chaseLen[1]);
        setMode(0);
    }


    IEnumerator co5()
    {
        yield return new WaitForSeconds(scatterLen[2]);
        setMode(1);
    }
    IEnumerator co6()
    {
        yield return new WaitForSeconds(chaseLen[2]);
        setMode(0);
    }
    IEnumerator co7()
    {
        yield return new WaitForSeconds(scatterLen[3]);
        setMode(1);
    }

    public void updateScore(int s){
        score += s;
        eatenDots ++;
    }

    public void setMode(int m){
        blinkyPrefab.SendMessage("setMode", m);
        clydePrefab.SendMessage("setMode", m);
        inkyPrefab.SendMessage("setMode", m);
        pinkyPrefab.SendMessage("setMode", m);

    }

    public void setDotLimit(int l){
        // Blinky, Pinky, Inky, Clyde
        if (l == 1){ 
            dotLimit[0] = 0;
            dotLimit[1] = 0;
            dotLimit[2] = 30;
            dotLimit[3] = 60;
        }
        else if (l == 2){
            dotLimit[0] = 0;
            dotLimit[1] = 0;
            dotLimit[2] = 0;
            dotLimit[3] = 50;            
        }
        else if (l > 2){
            dotLimit[0] = 0;
            dotLimit[1] = 0;
            dotLimit[2] = 0;
            dotLimit[3] = 0;
        }
    }

    public void setWaitTime(int l){
        if (l < 5) { waitTime = 4;}
        else {waitTime = 3;}
    }



    public void setScatterTime(int l){
        if (l == 1){ 
            scatterLen[0] = 7; 
            scatterLen[1] = 14; 
            scatterLen[2] = 19; 
            scatterLen[3] = 26; 
        }
        else if (l > 1 && l <= 4){
            scatterLen[0] = 7; 
            scatterLen[1] = 14; 
            scatterLen[2] = 19; 
            scatterLen[3] = 20;         
        }
        else if (l > 4){
            scatterLen[0] = 5; 
            scatterLen[1] = 10; 
            scatterLen[2] = 15; 
            scatterLen[3] = 16;         
        }
    }    
    
    public void setChaseTime(int l){
        if (l == 1){ 
            chaseLen[0] = 20; 
            chaseLen[1] = 40; 
            chaseLen[2] = 60; 
        }
        else if (l > 1 && l <= 4){
            chaseLen[0] = 20; 
            chaseLen[1] = 40; 
            chaseLen[2] = 1073;         }
        else if (l > 4){
            chaseLen[0] = 20; 
            chaseLen[1] = 40; 
            chaseLen[2] = 1077; 
        }
    }
}
