using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hittable : MonoBehaviour {

    public int hitPointsMax = 8;
    public int hitPointsCur = 0;

    public GameObject prefabHPBar;
    private Slider hpbar;

    // Use this for initialization
    public virtual void Start () {
        hitPointsCur = hitPointsMax;
        
        if (prefabHPBar != null) Instantiate(prefabHPBar, transform);

        if (hpbar == null) hpbar =GetComponentInChildren<Slider>();
        if (hpbar != null)
        {
            hpbar.maxValue = hitPointsMax;
            hpbar.minValue = 0;
            hpbar.wholeNumbers = true;
            hpbar.value = hitPointsCur;
        }
    }

    // Update is called once per frame
    public virtual void Update () {
	}

    public virtual void FixedUpdate() {
    }

    public virtual bool TakeDamage(int damage)
    {
        hitPointsCur -= damage;
        if (hitPointsCur < 0) hitPointsCur = 0;

        if (hpbar != null) {
            hpbar.value = hitPointsCur;
        }

        if (hitPointsCur <= 0) { Die(); return false; }
        return true;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
