﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour {
    private float timeDie;
	// Use this for initialization
	void Start () {
        timeDie = Time.time + 3.0f;
        foreach (var ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }

    }

    // Update is called once per frame
    void Update () {
        if (Time.time > timeDie) Destroy(gameObject);
	}
}
