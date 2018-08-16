using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Hittable {
    public float durationFire = 10f;
    public float durationOpen = 3f;

    public Bullet prefabBullet;
    public GameObject prefabSplash;

    private float timeHit;
    private float timeFire;
    private bool phaseFire;
    private float phaseStart;

    public Player player;
    Animator anim;

    // Use this for initialization
    public override void Start () {
        hitPointsMax = 6;
        base.Start();

        phaseFire = false;
        phaseStart = Time.time;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
	}

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(transform.position.x>9)
        {
            rb.MovePosition(transform.position+(new Vector3(-2,0,0))*Time.fixedDeltaTime);
        }

        if(!phaseFire)
        {
            if(Time.time- phaseStart > durationOpen)
            {
                phaseFire = true;
                phaseStart = Time.time;
                anim.SetBool("Open", false);
                return;
            }
        }

        if (phaseFire)
        {
            if (Time.time - phaseStart > durationFire)
            {
                phaseFire = false;
                phaseStart = Time.time;
                anim.SetBool("Open", true);
                return;
            }

            if (Time.time - timeFire > 0.2)
            {
                timeFire = Time.time;

                Bullet b=Instantiate(prefabBullet, transform.position + new Vector3(-4, 0, 0), Quaternion.identity).GetComponent<Bullet>();


                float t = Time.time - phaseStart;
                float y = Mathf.Sin(t)*0.8f;
                float cosy = Mathf.Cos(t)*1.2f; 
                //Debug.Log("->"+t+" "+y);
                b.transform.forward = new Vector3(-1, y);

                /*b = Instantiate(prefabBullet, transform.position + new Vector3(1, 0, 0), Quaternion.identity).GetComponent<Bullet>();
                b.transform.forward = new Vector3(1, -y);

                b = Instantiate(prefabBullet, transform.position + new Vector3(-1, 0, 0), Quaternion.identity).GetComponent<Bullet>();
                b.transform.forward = new Vector3(-1, cosy);

                b = Instantiate(prefabBullet, transform.position + new Vector3(1, 0, 0), Quaternion.identity).GetComponent<Bullet>();
                b.transform.forward = new Vector3(1, -cosy);

                b = Instantiate(prefabBullet, transform.position + new Vector3(0, 3, 0), Quaternion.identity).GetComponent<Bullet>();
                b.transform.forward = new Vector3(0, 3);

                b = Instantiate(prefabBullet, transform.position + new Vector3(0, -3, 0), Quaternion.identity).GetComponent<Bullet>();
                b.transform.forward = new Vector3(0, -3);*/
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    //void OnTriggerEnter2D(Collider2D other)
    {
        Player p = other.transform.GetComponent<Player>();
        Ship s = other.transform.GetComponent<Ship>();
        Debug.Log("coll:" + p + " "+s);

        if(p!=null)
        {
            if (Time.time - timeHit < 0.5f)
            {
                Debug.Log("Still immune:" + (Time.time - timeHit));
                return;
            }
            timeHit = Time.time;

            if (phaseFire)
            {
                Debug.Log("Firing:" + (Time.time - timeHit));
                return;
            }

            if(other.GetContact(0).otherCollider.transform!=transform)
            {
                Debug.Log("Wrong Part:" + (Time.time - timeHit));
                return;
            }

            TakeDamage(1);

            GameObject psgo = Instantiate(prefabSplash, other.GetContact(0).point, Quaternion.Euler(0, 90, 0));
            psgo.AddComponent<ParticleEffect>();

        }

        if (p != null)
        {
            var hitRB = other.transform.GetComponentInParent<Rigidbody2D>();
            var dir = other.transform.position - transform.position;
            hitRB.AddForce(dir.normalized * 30.0f, ForceMode2D.Impulse);
        }
        //TakeDamage(1);
        //rb.AddForce(info.relativeVelocity * 10, ForceMode2D.Impulse);
    }

    public override void Die()
    {
        player.BossDied();
        base.Die();
    }
}
