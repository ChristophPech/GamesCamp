using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : Hittable {
    public Rigidbody2D rb;
    private Player player;
    private float timeHit;

    // Use this for initialization
    public override void Start () {
        hitPointsMax = 10;
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    public override void FixedUpdate () {
        base.FixedUpdate();

        if (player.moveType == Player.MoveType.Normal)
        {
            rb.AddForce(new Vector2(-1000f, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy e = other.transform.GetComponent<Enemy>();
        Boss b = other.transform.GetComponent<Boss>();


        if(b==null) {return;}
        Debug.Log("trigger:" + e + " " + b);

        if (Time.time-timeHit<0.5f)
        {
            Debug.Log("Still immune:" + (Time.time - timeHit));
            return;
        }
        timeHit = Time.time;
        TakeDamage(1);
        //rb.AddForce(info.relativeVelocity * 10, ForceMode2D.Impulse);
    }

    public override void Die()
    {
        Debug.Log("Game Over");
        transform.parent.gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
