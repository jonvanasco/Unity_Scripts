using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desktopPlayerMovement : MonoBehaviour {
    // this is just calling for the player sprite
	public Transform player;
	// speed of player movement
    public float speed = 5.0f;
	// bullet prefab
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update(){
		// player movement
        moveCharacter(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        // space to shooot
		if(Input.GetKeyDown("space")){
            shootBullet();
        }
    }
	// player movement function
    void moveCharacter(Vector2 direction){
        player.Translate(direction * speed * Time.deltaTime);
    }
	// shooting bullet function
    public void shootBullet(){
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = player.transform.position;
    }
}