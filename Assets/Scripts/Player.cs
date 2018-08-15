﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float hor = 0;
    public float ver = 0;

    public float KeySpeed = 0;
    [Range(0, 0.5f)]
    public float MouseSpeed = 0.2f;

    public Boss prefabBoss;
    private Boss bossEntity;
    public Slider hpBarBoss;
    public Slider chargeBar;
    public Text scoreText;

    private Rigidbody2D rb;
    private Ship ship;

    public enum MoveType
    {
        None,
        Normal,
        Boss
    }

    public MoveType moveType;

    // Use this for initialization
    void Start () {
        Debug.Log("Player - Start");
        rb = GetComponent<Rigidbody2D>();
        ship = FindObjectOfType<Ship>();

        NormalMode();
    }

    // Update is called once per frame
    void FixedUpdate() {

        //hor = Input.GetAxis("Horizontal");
        //ver = Input.GetAxis("Vertical");
        //transform.position += new Vector3(hor, ver) * Time.deltaTime * KeySpeed;

        rb.bodyType = moveType == MoveType.Normal ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
        ship.rb.bodyType = moveType == MoveType.Boss ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;

        if (moveType == MoveType.Normal) MoveBody(rb);
        if (moveType == MoveType.Boss) MoveBody(ship.rb);


        if(Input.GetKeyDown(KeyCode.B))
        {
            SpawnBoss();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

    }

    public void Charge()
    {
        chargeBar.value += 1;
        if (chargeBar.value >= chargeBar.maxValue) SpawnBoss();
    }

    public void NormalMode()
    {
        moveType = MoveType.Normal;
        chargeBar.minValue = 0;
        chargeBar.maxValue = 10;
        chargeBar.wholeNumbers = true;
        chargeBar.value = 0;
        chargeBar.gameObject.SetActive(true);
        hpBarBoss.gameObject.SetActive(false);
    }

    public void BossDied()
    {
        bossEntity = null;
        NormalMode();
    }

    void SpawnBoss()
    {
        if (bossEntity != null) return;
        bossEntity = Instantiate(prefabBoss, new Vector3(6, 0, 0), Quaternion.identity).GetComponent<Boss>();
        bossEntity.hpBar = hpBarBoss;
        bossEntity.player = this;
        moveType = MoveType.Boss;
        ship.rb.angularVelocity = 0;

        chargeBar.gameObject.SetActive(false);
        hpBarBoss.gameObject.SetActive(true);
    }

    public void MoveBody(Rigidbody2D rb)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(new Vector3(0, 0, 1), 0);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 rayWorldPosition = ray.origin + ray.direction * distance;
            rayWorldPosition.z = 0;
            //transform.position = rayWorldPosition;

            Vector3 newPos = rb.transform.position * (1.0f - MouseSpeed) + rayWorldPosition * MouseSpeed;
            Vector3 velocity = newPos - rb.transform.position;
            //transform.position = newPos;
            rb.MovePosition(newPos);

            //rb.velocity = velocity*(1.0f/Time.fixedDeltaTime);
            //Debug.Log(distance);
        }
    }
}
