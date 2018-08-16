using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Hittable {

    private Ship ship;
    private Enemy enemy;
    public float DamageZone = -10;
    public float xpos = -12;
    public float SafeTime = 1f;
    private float timeHit = 1f;

    public override void Start()
    {
        base.Start();

        ship = FindObjectOfType<Ship>();
        enemy = FindObjectOfType<Enemy>();
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

        /*if(enemy.absorbed_by_blackhole)
        {
            transform.position = new Vector3(xpos++, 0, 0);
        }*/
    }
}
