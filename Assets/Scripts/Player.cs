using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float hor = 0;
    public float ver = 0;

    public float KeySpeed = 0;
    [Range(0, 0.5f)]
    public float MouseSpeed = 0.2f;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        Debug.Log("Player - Start");
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate() {

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        //transform.position += new Vector3(hor, ver) * Time.deltaTime * KeySpeed;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane=new Plane(new Vector3(0,0,1),0);
        float distance;
        if(plane.Raycast(ray, out distance))
        {
            Vector3 rayWorldPosition = ray.origin + ray.direction * distance;
            rayWorldPosition.z = 0;
            //transform.position = rayWorldPosition;

            Vector3 newPos = transform.position * (1.0f- MouseSpeed) + rayWorldPosition * MouseSpeed;
            Vector3 velocity=newPos - transform.position;
            //transform.position = newPos;
            rb.MovePosition(newPos);

            //rb.velocity = velocity*(1.0f/Time.fixedDeltaTime);
            //Debug.Log(distance);
        }


    }
}
