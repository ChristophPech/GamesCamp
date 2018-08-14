using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public Rigidbody2D rb;
    private Player player;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player.moveType == Player.MoveType.Normal)
        {
            rb.AddForce(new Vector2(-1000f, 0));
        }
    }
}
