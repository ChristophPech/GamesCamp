using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float coolDown=0.2f;
    private float shootStart=-100;
    public GameObject prefabBullet;
    public Transform bulletStart;
    public Transform bulletHolder;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time - shootStart < coolDown) return;
        shootStart = Time.time;
        var bullet=GameObject.Instantiate(prefabBullet, bulletStart.position, bulletStart.rotation).GetComponent<Bullet>();
        bullet.transform.SetParent(bulletHolder);
    }

    public override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipFront = (move.x > 0.01f);
        bool flipBack = (move.x < -0.01f);
        if(flipFront) transform.localEulerAngles = new Vector3(0,0,0);
        if (flipBack) transform.localEulerAngles = new Vector3(0, 180, 0);


        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
        //Debug.Log(targetVelocity);
    }
}

