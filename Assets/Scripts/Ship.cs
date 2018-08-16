using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ship : Hittable {
    private Player player;
    private float timeHit;

    // Use this for initialization
    public override void Start () {
        hitPointsMax = 10;
        base.Start();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    public override void FixedUpdate () {
        base.FixedUpdate();

        if (player.moveType == Player.MoveType.Normal)
        {
            rb.AddForce(new Vector2(-200f, 0));
        }
    }

    public void HandleDamage(Transform other)
    {
        Enemy e = other.GetComponent<Enemy>();
        Boss b = other.GetComponent<Boss>();
        Obstacle o = other.GetComponent<Obstacle>();

        if (b == null && o == null) { return; }
        Debug.Log("trigger:" + e + " " + b);

        if (Time.time - timeHit < 0.5f)
        {
            Debug.Log("Still immune:" + (Time.time - timeHit));
            return;
        }
        timeHit = Time.time;
        TakeDamage(1);
        //rb.AddForce(info.relativeVelocity * 10, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HandleDamage(other.transform);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        HandleDamage(other.transform);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        HandleDamage(other.transform);
    }
    void OnCollisionStay2D(Collision2D other)
    {
        HandleDamage(other.transform);
    }

    public override void Die()
    {
        Debug.Log("Game Over");
        transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene("GameOver");
        //Destroy(gameObject);
    }
}
