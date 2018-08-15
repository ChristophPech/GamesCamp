using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Hittable {
    public float durationFire = 10f;
    public float durationOpen = 3f;

    public Bullet prefabBullet;

    private float timeHit;
    private float timeFire;
    private bool phaseFire;
    private float phaseStart;

    public Player player;

    // Use this for initialization
    public override void Start () {
        hitPointsMax = 6;
        base.Start();

        phaseFire = false;
        phaseStart = Time.time;
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
	}

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(!phaseFire)
        {
            if(Time.time- phaseStart > durationOpen)
            {
                phaseFire = true;
                phaseStart = Time.time;
                return;
            }
        }

        if (phaseFire)
        {
            if (Time.time - phaseStart > durationFire)
            {
                phaseFire = false;
                phaseStart = Time.time;
                return;
            }

            if (Time.time - timeFire > 0.2)
            {
                timeFire = Time.time;

                Bullet b=Instantiate(prefabBullet, transform.position + new Vector3(-1, 0, 0), Quaternion.identity).GetComponent<Bullet>();

                float t = Time.time - phaseStart;
                float y = Mathf.Sin(t)*0.8f;
                //Debug.Log("->"+t+" "+y);
                b.transform.forward = new Vector3(-1, y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player p = other.transform.GetComponent<Player>();
        Ship s = other.transform.GetComponent<Ship>();
        Debug.Log("trigger:" + p + " "+s);

        if(p!=null)
        {
            if (Time.time - timeHit < 0.5f)
            {
                Debug.Log("Still immune:" + (Time.time - timeHit));
                return;
            }
            timeHit = Time.time;

            TakeDamage(1);
        }

        if (p != null)
        {
            var hitRB = other.GetComponentInParent<Rigidbody2D>();
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
