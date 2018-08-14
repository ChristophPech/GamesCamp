using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Hittable
{
    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    void OnCollisionEnter2D(Collision2D info)
    {
        Debug.Log("hit:"+ info.relativeVelocity);
        Rigidbody2D rb =GetComponent<Rigidbody2D>();
        rb.AddForce(info.relativeVelocity * 10, ForceMode2D.Impulse);
    }
}
