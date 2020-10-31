using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Inky : MonoBehaviour
{

    public Tilemap pathTilemap;

    public int modes; // 0 == chase; 1 == scatter; 2 == frighrened; 3 == random
    public int scatterTimes = 0; 

    public float standardSpeed = 6.0f;
	public float norSpeed;
	public float friSpeed;
	public float speed;

    public int stucked = 0;

    public int level;
    public int[] scatterLen = {0, 0, 0, 0};
    public int[] chaseLen   = {0, 0, 0};

	private Vector2 direction = Vector2.zero;
    private Vector2 preDirection;
    public Vector3 pacmanPos;
    public Vector3 prePos;
    public Vector3 target;
    public Vector3 corner;


    // Start is called before the first frame update
    void Start()
    {
		// preDirection = direction;
        // corner = = new Vector3(-8.0f, 8.0f, 0.0f);
        // corner = = new Vector3(20.0f, 8.0f, 0.0f);
        corner = new Vector3(-8.0f, -28.0f, 0.0f);
        // corner = = new Vector3(20.0f, -28.0f, 0.0f);
  


    }

    public void getOut(){
        transform.localPosition = new Vector3(6.0f, -6.5f, 0.0f);
    }
    public void setMode(int m){
        modes = m;
    }

    // Update is called once per frame
    void Update()
    {
        setSpeed(level);

        preDirection = direction;


        // if (prePos == transform.localPosition)
        // {
        //     stucked ++;
        // }
        // if (stucked >= 90){
        //     modes = 3;
        //     stucked = 0;
        // }
        // if (stucked <= -270)
        // {
        //     modes = 0;
        //     stucked =0;
        // }

        // float step = speed * Time.deltaTime;
        // direction = Vector2.MoveTowards(transform.position, pacmanPos, step);
        if (modes == 0)
        {
            direction = nextDirc(corner);
            // stucked -= 1;

        }else{
            direction = nextDirc(pacmanPos);
        }
        Move();
        // Debug.Log(" pacman:" + prePos +","+transform.localPosition);
        // prePos = transform.localPosition;

    }

    public void reverseDirection(){

    }

    public void checkScatterTimes(){
        if (scatterTimes == 4)
        {
            //
        }
    }

    public void returnToPen(){

    }

    public void setScatterTime(int l){
        if (l == 1){ 
            scatterLen[0] = 7; 
            scatterLen[1] = 7; 
            scatterLen[2] = 5; 
            scatterLen[3] = 5; 
        }
        else if (l > 1 && l <= 4){
            scatterLen[0] = 7; 
            scatterLen[1] = 7; 
            scatterLen[2] = 5; 
            scatterLen[3] = 0;         
        }
        else if (l > 4){
            scatterLen[0] = 5; 
            scatterLen[1] = 5; 
            scatterLen[2] = 5; 
            scatterLen[3] = 0;         
        }
    }    
    
    public void setChaseTime(int l){
        if (l == 1){ 
            chaseLen[0] = 20; 
            chaseLen[1] = 20; 
            chaseLen[2] = 20; 
        }
        else if (l > 1 && l <= 4){
            chaseLen[0] = 20; 
            chaseLen[1] = 20; 
            chaseLen[2] = 1033;         }
        else if (l > 4){
            chaseLen[0] = 20; 
            chaseLen[1] = 20; 
            chaseLen[2] = 1037; 
        }
    }


	private void setSpeed(int l){
		if (l == 1){ 
			norSpeed = 0.75f * standardSpeed;
			friSpeed = 0.5f  * standardSpeed;
		}
        else if (l > 1 && l <= 4){
			norSpeed = 0.85f * standardSpeed;
			friSpeed = 0.55f * standardSpeed;
		}
        else if (l > 4 && l <= 20){
			norSpeed = 0.95f * standardSpeed;
			friSpeed = 0.6f  *standardSpeed;
		}
        else if (l > 20){
			norSpeed = 0.95f * standardSpeed;
			friSpeed = 0.95f * standardSpeed;
		}

		speed = norSpeed;
	}

    void Move () {
		transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
	}

	bool valid (Vector2 direc){
        Vector2 inv = (Vector2)(preDirection * -1);

        if (direc == inv){return false;}
		
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
		else if (tryYDir != Vector2.zero) {return tryYDir;}
		else {
            // if (preDirection == Vector2.left || preDirection == Vector2.right) {
            //     if (valid(Vector2.up)) { return Vector2.up;}
            //     if (valid(Vector2.down)) { return Vector2.down;}
            // // }else if (preDirection == Vector2.up || preDirection == Vector2.down)
            // }else
            // {
            //     if (valid(Vector2.right)) { return Vector2.right;}
            //     if (valid(Vector2.left)) { return Vector2.left;}
            // }
            return Vector2.zero;
        }
		
		// Vector3 place = pathTilemap.CellToWorld(localPlace);


		// return null;
	}

    public Vector2 randDirc(){
        Vector3Int localPlace = Vector3Int.FloorToInt (transform.localPosition);
		
		Vector3 place = pathTilemap.CellToWorld(localPlace);
		// Debug.Log(" tile:" + pathTilemap.HasTile(localPlace));
		// Debug.Log(" place:" + localPlace);
		// Debug.Log(" pacman:" + pacmanPrefab.transform.position);
        if (!pathTilemap.HasTile(localPlace)){ return direction; }

		if (pathTilemap.GetTile(localPlace).name == "lr" || pathTilemap.GetTile(localPlace).name == "ud") {
			return direction;
        }else{
            // var ran = new Random();

            int seed = Random.Range(0,4);
            if (seed == 0){return Vector2.up;}
            else if (seed == 1){return Vector2.left;}
            else if (seed == 2){return Vector2.down;}
            else{return Vector2.right;}
        }
    }

    public void setPacmanPos(Vector3 pos){
        pacmanPos = pos;
    }

	public void setLevel(int l){
		level = l;
	}










}
