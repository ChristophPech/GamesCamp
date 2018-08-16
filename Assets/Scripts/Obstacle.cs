using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Hittable {

    int rot = 0;

    // Use this for initialization
    public override void Start () {
        base.Start();

        rot = Random.value < 0.5 ? -1 : 1;
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
	}

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.AddForce(new Vector3(-10, 0, 0), ForceMode2D.Force);

        if(rot!=0) rb.AddTorque(0.9f* rot, ForceMode2D.Force);

        //transform.position += new Vector3(-10, 0, 0) * Time.fixedDeltaTime;
        if (transform.position.x < -20) Die();
    }

    void OnCollisionEnter2D(Collision2D info)
    {
        Debug.Log("------------> hit:" + info.transform.name);
        rot = 0;
        //rb.AddForce(info.relativeVelocity * 1, ForceMode2D.Impulse);
    }
}
