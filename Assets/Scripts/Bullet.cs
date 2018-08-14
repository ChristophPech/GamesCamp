using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Hittable {

    public float speed = 10.0f;
    public float lifeTime = 3.0f;
    public int damage = 2;

    public float startTime;

	// Use this for initialization
	public override void Start () {
        startTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy e=other.transform.GetComponent<Enemy>();
        if(e!=null)
        {
            Debug.Log("Hit:" + e);
            e.TakeDamage(damage);
            Die();
        }
    }

    // Update is called once per frame
    public override void Update () {
        if(Time.time-startTime>lifeTime)
        {
            Die();
            return;
        }

		Vector3 dir=transform.forward* speed*Time.deltaTime;
        transform.position = transform.position + dir;
    }
}
