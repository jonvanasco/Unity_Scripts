using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour {

	Rigidbody2D rb;
	GameObject target;
	float moveSpeed;
	Vector3 directionToTarget;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Cat");
		rb = GetComponent<Rigidbody2D> ();
		moveSpeed = Random.Range (1f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		MoveMonster ();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		switch (col.gameObject.tag) {

		case "Player":
			MonstersSpawnerControl.spawnAllowed = false;
			Instantiate (explosion, col.gameObject.transform.position, Quaternion.identity);
			Destroy (col.gameObject);
			target = null;
			break;

		case "Bullet":
			Instantiate (explosion, transform.position, Quaternion.identity);
			KillsCounter.killsNumber += 1;
			Destroy (col.gameObject);
			Destroy (gameObject);
			break;
		}
	}

	void MoveMonster ()
	{
		if (target != null) {
			directionToTarget = (target.transform.position - transform.position).normalized;
			rb.velocity = new Vector2 (directionToTarget.x * moveSpeed,
										directionToTarget.y * moveSpeed);
		}
		else
			rb.velocity = Vector3.zero;
	}
}
