using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour {

    [System.Serializable]
    public class Wave
    {
        public List<Enemy> enemies = new List<Enemy>();
    }

    public List<Wave> waves = new List<Wave>();

	// Use this for initialization
	void Start () {
        waves = new List<Wave>();

        var waveObj = GameObject.Find("Waves");
        foreach(Transform waveChild in waveObj.transform)
        {
            Wave newWave = new Wave();

            foreach (Transform enemyObj in waveChild)
            {
                Enemy e=enemyObj.GetComponent<Enemy>();
                newWave.enemies.Add(e);
                e.gameObject.SetActive(false);
            }

            waves.Add(newWave);
        }
	}
	
	// Update is called once per frame
	void Update () {
        SpawnSpecificWave();
        if (Input.GetButtonDown("Fire1")) SpawnRandomWave();
	}

    public void SpawnRandomWave()
    {
        Wave w=waves[Random.Range(0, waves.Count)];
        SpawnWave(w);
    }

    // Zum waves test (wird nach wave-erstellung entfernt)
    public void SpawnSpecificWave()
    {
        Wave w0 = waves[0];
        Wave w1 = waves[1];
        Wave w2 = waves[2];
       // Wave w3 = waves[3];
       // Wave w4 = waves[4];

        if(Input.GetKeyDown("1")){
            SpawnWave(w0);
        }
        if(Input.GetKeyDown("2"))
        {
            SpawnWave(w1);
        }
        if (Input.GetKeyDown("3"))
        {
            SpawnWave(w2);
        }
       // if(Input.GetKeyDown("4"))
       // {
       //     SpawnWave(w3);
       // }
       // if(Input.GetKeyDown("5"))
       // {
       //     SpawnWave(w4);
       // }
    }

    public void SpawnWave(Wave w)
    {
        foreach(Enemy e in w.enemies)
        {
            Enemy newEnemy=Instantiate(e.gameObject, e.transform.localPosition, e.transform.localRotation).GetComponent<Enemy>();
            newEnemy.transform.position += new Vector3(15, 0, 0);
            newEnemy.enabled = true;
            newEnemy.gameObject.SetActive(true);
        }
    }
}
