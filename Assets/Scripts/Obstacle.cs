using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Hittable {
    public Rigidbody2D rb;

    // Use this for initialization
    public override void Start () {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
	}

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.AddForce(new Vector3(-1, 0, 0), ForceMode2D.Force);

        //transform.position += new Vector3(-10, 0, 0) * Time.fixedDeltaTime;
        if (transform.position.x < -20) Die();
    }

    void OnCollisionEnter2D(Collision2D info)
    {
        Debug.Log("------------> hit:" + info.transform.name);
        rb.AddForce(info.relativeVelocity * 1, ForceMode2D.Impulse);
    }
}
