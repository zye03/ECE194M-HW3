using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Energizer : MonoBehaviour {

    // private GameManager gm;
    public GameObject energizerPrefab;
    public GameObject controller;
	// Use this for initialization
	void Start ()
	{
	    // gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // if( gm == null )    Debug.Log("Energizer did not find Game Manager!");
	}
    void Awake(){
        controller = GameObject.Find("MainController");
    }

	private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "pacman"){
            // Dot thisDot = collision.GetComponent<Dot>();
            // asteroid.takeDamage();
            // controller.updateScore(10);
		    controller.SendMessage("updateScore", 50);

            Destroy(gameObject);
            // Destroy(thisDot);
        }
    }
}
