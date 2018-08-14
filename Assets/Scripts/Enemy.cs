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
    public int Collision_Count = 0;

    public enum MoveType
    {
        Straight,
        Sine,
    }

    [Range(0, 100f)]
    public float Frequency;

    [Range(0, 1f)]
    public float Phase;

    [Range(1, 100f)]
    public float Amplitude;

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

        if (timeDied != 0 && Time.time - timeDied > 5)
        {
            Destroy(gameObject);
            Debug.Log("Enemy destroyed.");
        }
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
        if (MovementActive == false) {
            //rb.AddForce(-Vector3.right * 2.0f);
            return;
        }

        if (transform.position.x < -20) Die();

        if(moveType == MoveType.Straight)
        {
            //rb.AddForce(-transform.right);
            rb.velocity = (-transform.right) * Speed;
        }
        if (moveType == MoveType.Sine)
        {
            //rb.AddForce(-transform.right);
            float x = (Time.time + (Phase*Mathf.PI*2)) * Frequency;
            float sin = Mathf.Sin(x);
            //Debug.Log("x:" + x + " sin:" + sin);
            rb.velocity = (-transform.right) * Speed + (sin  * Vector3.up * Amplitude);
        }
    }

    public void Patterns()
    {

    }

    public override void Die()
    {
        MovementActive = false;
        timeDied = Time.time;
        //Destroy(gameObject);
    }
}
