using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Hittable
{
    private Rigidbody2D rb;
    bool MovementActive = true;
    private float timeDied;
    public float Speed = 2f;

    public enum MoveType
    {
        Straight,
        Sine,
    }

    public MoveType moveType = MoveType.Straight;

    // Use this for initialization
    public override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitPointsMax = 1;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Movement();

        if(timeDied!=0&&Time.time- timeDied > 5) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D info)
    {
        Debug.Log("hit:"+ info.relativeVelocity);
        Rigidbody2D rb =GetComponent<Rigidbody2D>();
        TakeDamage(1);
        //rb.AddForce(info.relativeVelocity * 10, ForceMode2D.Impulse);
    }

    //Movement 
    public void Movement()
    {
        if (MovementActive == false) return;

        if (transform.position.x < -20) Die();

        if(moveType == MoveType.Straight)
        {
            //rb.AddForce(-transform.right);
            rb.velocity = (-transform.right) * Speed * Time.fixedDeltaTime;
        }
    }

    public override void Die()
    {
        MovementActive = false;
        timeDied = Time.time;
        //Destroy(gameObject);
    }
}
