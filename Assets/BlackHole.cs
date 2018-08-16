using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Hittable
{

    private Ship ship;
    public float DamageZone = -12.5f;
    public float SafeTime = 1f;
    private float timeHit = 1f;
    public float MaxXPos = -2.9f;

    private Vector3 targetPos;

    public override void Start()
    {
        base.Start();

        ship = FindObjectOfType<Ship>();

        targetPos = transform.position;
    }

    public void EnemyOut()
    {
        Debug.Log("out");

        if (transform.position.x < MaxXPos)
        {
            targetPos += new Vector3(0.5f, 0, 0);
        }
    }

    public override void Update()
    {
        base.Update();

        Vector3 dir = targetPos - transform.position;
        if (dir.magnitude > 0 && transform.position.x < MaxXPos)
        {
            transform.position += dir.normalized * Time.deltaTime * 0.25f;
        }

        DamageZone = targetPos.x;
        DamageZone -= 0.5f;
        //Debug.Log(DamageZone);
        //Debug.Log(targetPos.x);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (ship.transform.position.x < DamageZone)
        {
            if (Time.time - timeHit < SafeTime)
            {
                Debug.Log("Still immune:" + (Time.time - timeHit));
                return;
            }
            timeHit = Time.time;
            ship.TakeDamage(1);
        }
    }
}