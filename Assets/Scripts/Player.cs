using System.Collections;
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
    public Text HighscoreText;

    private Rigidbody2D rb;
    private Ship ship;
    public static int score;
    public static int Highscore;
    private Transform skybox;

    public AudioClip musicNormal;
    public AudioClip musicBoss;

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
        score = 0;
        rb = GetComponent<Rigidbody2D>();
        ship = FindObjectOfType<Ship>();
        score = 0;
        skybox = GameObject.Find("Skybox").transform;
        NormalMode();
        HighscoreText.text = "" + Highscore;
    }

    private void Update()
    {
        if (moveType == MoveType.Normal)
        {
            skybox.Rotate(Vector3.forward, -Time.deltaTime*12f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            //Time.timeScale = 0;
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }

            //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
    }
    void FixedUpdate() {
        //hor = Input.GetAxis("Horizontal");
        //ver = Input.GetAxis("Vertical");
        //transform.position += new Vector3(hor, ver) * Time.deltaTime * KeySpeed;

        rb.bodyType = moveType == MoveType.Normal ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
        ship.rb.bodyType = moveType == MoveType.Boss ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;

        if (moveType == MoveType.Normal) MoveBody(rb, ship.rb);
        if (moveType == MoveType.Boss) MoveBody(ship.rb,rb);


        if(Input.GetKeyDown(KeyCode.B))
        {
            SpawnBoss();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        scoreText.text = "" + score;
    }

    public void Charge()
    {
        chargeBar.value += 1;
        score += 50;
        if (chargeBar.value >= chargeBar.maxValue) SpawnBoss();
        transform.Find("50").GetComponent<Animator>().SetTrigger("50");
    }

    public void NormalMode()
    {
        moveType = MoveType.Normal;
        chargeBar.minValue = 0;
        chargeBar.maxValue = 50;
        chargeBar.wholeNumbers = true;
        chargeBar.value = 0;
        chargeBar.gameObject.SetActive(true);
        hpBarBoss.gameObject.SetActive(false);

        rb.angularVelocity = 0;
        rb.rotation = 0;

        AudioSource src= Camera.main.GetComponent<AudioSource>();
        src.clip = musicNormal;
        src.Play();
    }

    public void BossDied()
    {
        bossEntity = null;
        score += 1000;
        transform.Find("1000").GetComponent<Animator>().SetTrigger("1000");
        NormalMode();
    }

    void SpawnBoss()
    {
        if (bossEntity != null) return;
        bossEntity = Instantiate(prefabBoss, new Vector3(19, 0, 0), Quaternion.identity).GetComponent<Boss>();
        bossEntity.hpBar = hpBarBoss;
        bossEntity.player = this;
        moveType = MoveType.Boss;
        ship.rb.angularVelocity = 0;

        chargeBar.gameObject.SetActive(false);
        hpBarBoss.gameObject.SetActive(true);

        AudioSource src = Camera.main.GetComponent<AudioSource>();
        src.clip = musicBoss;
        src.Play();
    }

    public void MoveBody(Rigidbody2D rb, Rigidbody2D other)
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
            rb.MovePosition(newPos);

            //Vector3 dir=newPos-rb.transform.position;
            //rb.AddForce(dir * 1000.0f,ForceMode2D.Force);

            //rb.velocity = velocity*(1.0f/Time.fixedDeltaTime);
            //Debug.Log(distance);
        }

        rb.angularVelocity = 0;
        rb.MoveRotation(0);

        Vector3 dir=other.transform.position - rb.transform.position;
        if(dir.magnitude>4)
        {
            other.AddForce(-dir*200.0f);
        }
    }
}
