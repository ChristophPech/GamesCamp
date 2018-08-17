using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Hittable
{
    bool MovementActive = true;
    private float timeDied;
    public bool StrongEnemy = false;
    public float StrongEnemyChance = 0.4f;
    public float Speed = 2f;
    public int Collision_Count = 0;
    public int absorbed_by_blackhole = 0;

    public AudioClip sndDie;
    AudioSource audio;

    public enum MoveType
    {
        Straight,
        Sine,
        BreakDown,
        BreakUp,
        Accelerate,
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
        hitPointsMax = 1;
        base.Start();
        StrongEnemy = (Random.value < StrongEnemyChance);

        if(StrongEnemy)
        {
            transform.Find("GfxFly").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("GfxBug").gameObject.SetActive(false);
        }

        audio = GetComponent<AudioSource>();
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
            Destroy();
        }

        if (StrongEnemy == false && Collision_Count ==1)
        {
            Destroy();
        }
        
        if (timeDied != 0 && Time.time - timeDied > 5)
        {
            Destroy();
            Debug.Log("Enemy destroyed.");
        }

    }

    void OnCollisionEnter2D(Collision2D info)
    {
        Debug.Log("hit:"+ info.relativeVelocity);
        Player p=info.transform.GetComponent<Player>();
        Obstacle o = info.transform.GetComponent<Obstacle>();
        Enemy e = info.transform.GetComponent<Enemy>();
        BlackHole b = info.transform.GetComponent<BlackHole>();
        if (p!=null||o!=null||e!=null||b!=null)
        {
            if (b != null) absorbed_by_blackhole++;
            if (o != null && !o.PlayerTouched) return;
            GetHit();
        }
        //rb.AddForce(info.relativeVelocity * 10, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Ship s = other.transform.GetComponent<Ship>();
        Debug.Log("trigger:" + s);

        if (s != null)
        {
            GetHit();
            s.HandleDamage(transform);
        }
    }

    void GetHit()
    {
        Collision_Count++;
        if (!TakeDamage(1))
        {
            FindObjectOfType<Player>().Charge();
        }
    }

    //Movement 
    public void Movement()
    {
        if (MovementActive == false)
        {
            //rb.AddForce(-Vector3.right * 2.0f);
            return;
        }

        if (transform.position.x < -14)
        {
            Die();
            FindObjectOfType<BlackHole>().EnemyOut();
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
            //Debug.Log(transform.position.x);
            if (transform.position.x > 5)
            {
                rb.velocity = (-transform.right) * Speed;
            }
            else if (transform.position.x < 5 && transform.position.y == 0)
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
            if (transform.position.x > 5)
            {
                rb.velocity = (-transform.right) * Speed;
            }
            else if (transform.position.x < 5 && transform.position.y == 0)
            {
                rb.velocity = (transform.up) * Speed;
            }
            else if (transform.position.y > 2)
            {
                rb.velocity = (-transform.right) * Speed;
            }

        }
        if (moveType == MoveType.Accelerate)
        {
            if (transform.position.x > 5)
            {
                rb.velocity = (-transform.right) * Speed;
            }
            else if (transform.position.x < 5)
            {
                rb.velocity = (-transform.right) * Speed * 2;
            }


        }
    }

    public void Patterns()
    {

    }

    public void Destroy()
    {
        GameObject psgo = Instantiate(prefabSplash, transform.position, Quaternion.Euler(0,90,0));
        psgo.AddComponent<ParticleEffect>();
        Destroy(gameObject);
    }

    public override void Die()
    {
        MovementActive = false;
        isDead = true;
        timeDied = Time.time;

        AudioSource src = Camera.main.GetComponent<AudioSource>();
        src.PlayOneShot(sndDie);
        //Destroy(gameObject);
    }
}
