using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hittable : MonoBehaviour {

    public int hitPointsMax = 8;
    public int hitPointsCur = 0;

    public GameObject prefabHPBar;
    public Slider hpBar;

    public bool isDead = false;

    // Use this for initialization
    public virtual void Start () {
        hitPointsCur = hitPointsMax;
        
        if (prefabHPBar != null) Instantiate(prefabHPBar, transform);
        if (hpBar != null) hpBar.gameObject.SetActive(true);

        if (hpBar == null) hpBar = GetComponentInChildren<Slider>();
        if (hpBar != null)
        {
            hpBar.maxValue = hitPointsMax;
            hpBar.minValue = 0;
            hpBar.wholeNumbers = true;
            hpBar.value = hitPointsCur;
        }
    }

    // Update is called once per frame
    public virtual void Update () {
	}

    public virtual void FixedUpdate() {
    }

    public virtual bool TakeDamage(int damage)
    {
        if (hitPointsCur < 0) return false;
            hitPointsCur -= damage;
        if (hitPointsCur < 0) hitPointsCur = 0;

        if (hpBar != null) {
            hpBar.value = hitPointsCur;
        }

        if (hitPointsCur <= 0) { Die(); return false; }
        return true;
    }

    public virtual void Die()
    {
        isDead = true;
        Destroy(gameObject);
        if (hpBar != null) hpBar.gameObject.SetActive(false);
    }
}
