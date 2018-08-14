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
            }

            waves.Add(newWave);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) SpawnRandomWave();
	}

    public void SpawnRandomWave()
    {
        Wave w=waves[Random.Range(0, waves.Count)];
        SpawnWave(w);
    }

    public void SpawnWave(Wave w)
    {
        foreach(Enemy e in w.enemies)
        {
            Enemy newEnemy=Instantiate(e.gameObject, e.transform.localPosition, e.transform.localRotation).GetComponent<Enemy>();
            newEnemy.transform.position += new Vector3(15, 0, 0);
            newEnemy.enabled = true;
        }
    }
}
