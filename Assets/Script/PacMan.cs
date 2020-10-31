using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PacMan : MonoBehaviour {

	public float standardSpeed = 6.0f;
	public float norSpeed;
	public float friSpeed;
	public float speed;
	public int level;
    // public float norSpeed = 5.0f;
	protected Rigidbody2D pacmanRigidBody;
	public Tilemap pathTilemap;

	public GameObject pacmanPrefab;
	public GameObject blinkyPrefab;
    public GameObject clydePrefab;
    public GameObject inkyPrefab;
    public GameObject pinkyPrefab;





	private Vector2 direction = Vector2.zero;

	// Use this for initialization
	void Start () {


		pacmanRigidBody = GetComponent<Rigidbody2D> ();
		standardSpeed = 6.0f;

		
        blinkyPrefab = GameObject.Find("blinky");
        clydePrefab = GameObject.Find("clyde");
        inkyPrefab = GameObject.Find("inky");
        pinkyPrefab = GameObject.Find("pinky");

	}
	
	// Update is called once per frame
	void Update () {
		setSpeed(level);

		Vector2 preDirection = direction;

		readInput ();

		if (!valid(direction)) { direction = preDirection; }
		Move ();
		UpdateOrientation ();
		// Debug.Log(" pacman:" + transform.localPosition);
		// Debug.Log(" level:" + level);

		// HandleMovement ();
		blinkyPrefab.SendMessage("setPacmanPos", transform.localPosition);
		clydePrefab.SendMessage("setPacmanPos", transform.localPosition);
		pinkyPrefab.SendMessage("setPacmanPos", transform.localPosition);
		inkyPrefab.SendMessage("setPacmanPos", transform.localPosition);


	}
	public void getOut(){
        transform.localPosition = new Vector3(6.0f, -18.5f, 0.0f);
    }

	void readInput () {

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {

			direction = Vector2.left;

		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {

			direction = Vector2.right;

		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {

			direction = Vector2.up;

		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {

			direction = Vector2.down;
		}
	}

	bool valid (Vector2 direc){
		
		// Vector3Int localPlace = Vector3Int.FloorToInt (pacmanPrefab.transform.position);
		Vector3Int localPlace = Vector3Int.FloorToInt (transform.localPosition);
		
		Vector3 place = pathTilemap.CellToWorld(localPlace);
		// Debug.Log(" tile:" + pathTilemap.HasTile(localPlace));
		// Debug.Log(" place:" + localPlace);
		// Debug.Log(" pacman:" + pacmanPrefab.transform.position);

		if (!pathTilemap.HasTile(localPlace)){ return false; }

		if (pathTilemap.GetTile(localPlace).name == "dl") {
			if (direc == Vector2.down || direc == Vector2.left){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "dr")
        {
			if (direc == Vector2.down || direc == Vector2.right){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "dlr")
        {
			if (direc == Vector2.down || direc == Vector2.right || direc == Vector2.left){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "lr")
        {
			if (direc == Vector2.left || direc == Vector2.right){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "lud")
        {
			if (direc == Vector2.down || direc == Vector2.left || direc == Vector2.up){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "rud")
        {
			if (direc == Vector2.down || direc == Vector2.right || direc == Vector2.up){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "ud")
        {
			if (direc == Vector2.down || direc == Vector2.up){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "udlr")
        {
			if (direc == Vector2.down || direc == Vector2.right || direc == Vector2.up || direc == Vector2.left){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "ul")
        {
			if (direc == Vector2.up || direc == Vector2.left){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "ulr")
        {
			if (direc == Vector2.right || direc == Vector2.up || direc == Vector2.left){ return true; }
        }
        else if (pathTilemap.GetTile(localPlace).name == "ur")
        {
			if (direc == Vector2.right || direc == Vector2.up){ return true; }
        }

		return false;
		
	}

	public Vector2 nextDirc(Vector3 target){

		// Vector3Int localPlace = Vector3Int.FloorToInt (pacmanPrefab.transform.position);
		// Vector3Int localPlace = Vector3Int.FloorToInt (transform.localPosition);
		Vector2 tryXDir = Vector2.zero;
		Vector2 tryYDir = Vector2.zero;

		Vector3 myPos = transform.localPosition;
		float xDif = myPos.x - target.x;
		float yDif = myPos.y - target.y;
		float totalDif = xDif + yDif;

		// up is pos; right is pos;
		if (xDif > 0){
			if (valid(Vector2.left)) { tryXDir = Vector2.left; }	
		}else{
			if (valid(Vector2.right)) { tryXDir = Vector2.right; }	
		}		
		
		if (yDif > 0){
			if (valid(Vector2.down)) { tryYDir = Vector2.down; }	
		}else{
			if (valid(Vector2.up)) { tryYDir = Vector2.up; }	
		}

		if (tryXDir != Vector2.zero && tryYDir != Vector2.zero)
		{
			if (tryXDir == Vector2.up){ return tryXDir;}
			if (tryYDir == Vector2.left){ return tryYDir;}
			return tryYDir;
		}
		else if (tryXDir != Vector2.zero) {return tryXDir;}
		else {return tryYDir;}
		
		// Vector3 place = pathTilemap.CellToWorld(localPlace);


		// return null;
	}

	void Move () {
		transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
	}

	void UpdateOrientation () {
		if (direction == Vector2.left) {
			transform.localScale = new Vector3 (-1, 1, 1);
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		} else if (direction == Vector2.right) {
			transform.localScale = new Vector3 (1, 1, 1);
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		} else if (direction == Vector2.up) {
			transform.localScale = new Vector3 (1, 1, 1);
			transform.localRotation = Quaternion.Euler (0, 0, 90);
		} else if (direction == Vector2.down) {
			transform.localScale = new Vector3 (1, 1, 1);
			transform.localRotation = Quaternion.Euler (0, 0, 270);
		}
	}


	private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "dot"){
            Dot thisDot = collision.GetComponent<Dot>();
            Destroy(thisDot);
        }
    }

	public void setSpeed(int l){
		if (l == 1){ 
			norSpeed = 0.8f * standardSpeed;
			friSpeed = 0.9f * standardSpeed;
		}
        else if (l > 1 && l <= 4){
			norSpeed = 0.9f * standardSpeed;
			friSpeed = 0.95f * standardSpeed;
		}
        else if (l > 4 && l <= 20){
			norSpeed = standardSpeed;
			friSpeed = standardSpeed;
		}
        else if (l > 20){
			norSpeed = 0.9f * standardSpeed;
			friSpeed = standardSpeed;
		}

		speed = norSpeed;
	}

	public void setLevel(int l){
		level = l;
	}


}






	// void MoveRigidBody(Vector2 moveDirVec) {
        
    //     Vector2 moveVec = moveDirVec * speed * Time.deltaTime;
    //     Vector2 myPos = new Vector2 (transform.position.x, transform.position.y);
 
    //     Vector2 newPos = myPos + moveVec;
 
    //     pacmanRigidBody.MovePosition (newPos);   
    // }
 
    // void HandleMovement() {
    //     Vector2 moveDirVec = Vector2.zero;
 
    //     if (Input.GetKey (KeyCode.UpArrow)) {
    //         moveDirVec = new Vector2 (0, 1);
 
    //     } else if (Input.GetKey (KeyCode.DownArrow)) {
    //         moveDirVec = new Vector2(0, -1);
    //     } else if (Input.GetKey (KeyCode.LeftArrow)) {
    //         moveDirVec = new Vector2(-1, 0);
    //     } else if (Input.GetKey (KeyCode.RightArrow)) {
    //         moveDirVec = new Vector2(1, 0);
    //     }
 
    //     if (moveDirVec != Vector2.zero) {
    //         MoveRigidBody (moveDirVec); 
    //     }
    // }