using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject controller;

    // public GameObject asteroidExplosionPrefab;
    // public GameObject controller;

    // private Rigidbody2D rb;
    // private float maxX = 10.15f;
    // private float maxY = 6.15f;
    // private float maxSpeed = 2.5f;

    // private int health = 1;

    // public int scale;
    // private int maxScale = 3;

    // public float childAsteroidOffset = 1f;

    private void Awake(){
        // scale = maxScale;
        // rb = GetComponent<Rigidbody2D>();
        gameObject.name = "dot";
        controller = GameObject.Find("MainController");

        // controller = GameObject.Find("GameController");


        // transform.position = new Vector3(Random.Range(-maxX, maxX),Random.Range(-maxY, maxY), 0);

        // Velocity
        // rb.velocity = Quaternion.Euler(0,0,Random.Range(0,360)) * new Vector3(Random.Range(0,maxSpeed),0.0f,0.0f);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "pacman"){
            // Dot thisDot = collision.GetComponent<Dot>();
            // asteroid.takeDamage();
            // controller.updateScore(10);
		    controller.SendMessage("updateScore", 10);

            Destroy(gameObject);
            // Destroy(thisDot);
        }
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "pacman"){
    //         // Dot thisDot = collision.GetComponent<Dot>();
    //         // asteroid.takeDamage();
    //         Destroy(gameObject);
    //         // Destroy(thisDot);
    //     }

    // }
}
