using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    Text text;
    private GameObject player;
    private object bottleAmount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")

        {

            Destroy(this.gameObject);
        }
        else if (collision.tag == "Player")
        {
            text = GetComponent<Text>();
            Destroy(this.gameObject);

        }

    }

}
