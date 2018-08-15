using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Hittable
{
    private Rigidbody2D rb;
    bool MovementActive = true;
    private float timeDied;
    public bool StrongEnemy = false;
    public float Speed = 2f;
    public int Collision_Count = 0;

    public enum MoveType
    {
        Straight,
        Sine,
        BreakDown,
        BreakUp,
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
        
        // Wird ausgeführt wenn ein Enemy drei weitere Enemies beruehrt hat (nur auf Probe)
        if(StrongEnemy == true && Collision_Count == 3)
        {
            Destroy(gameObject);
        }

        if (StrongEnemy == false && Collision_Count ==1)
        {
            Destroy(gameObject);
        }
        
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
        if (!TakeDamage(1)) {
            FindObjectOfType<Player>().Charge();
        }

        Collision_Count++;
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
            float x = (Time.time * Frequency) + (Phase * Mathf.PI * 2);
            float sin = Mathf.Sin(x);
            //Debug.Log(name+" x:" + x + " sin:" + sin);
            rb.velocity = (-transform.right) * Speed + (sin  * Vector3.up * Amplitude);
        }
        if (moveType == MoveType.BreakDown)
        {
            if (transform.position.x > 0)
            {
                rb.velocity = (-transform.right) * Speed;
            }
            else if (transform.position.x < 0 && transform.position.y == 0)
            {
                rb.velocity = (-transform.up) * Speed;
            }
            else if (transform.position.y < -2)
            {
                rb.velocity = (-transform.right) * Speed;
            }

            //Debug.Log(transform.position.y);
        }
        if (moveType == MoveType.BreakUp)
        {
            if (transform.position.x > 0)
            {
                rb.velocity = (-transform.right) * Speed;
            }
            else if (transform.position.x < 0 && transform.position.y == 0)
            {
                rb.velocity = (transform.up) * Speed;
            }
            else if (transform.position.y > 2)
            {
                rb.velocity = (-transform.right) * Speed;
            }
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
