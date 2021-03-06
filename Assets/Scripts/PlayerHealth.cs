﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Healthpool of player")]
    [Range(100, 500)]
    private float health = 300;

    private float startHealth;

    [SerializeField]
    [Tooltip("Damage taken by an incoming projectile")]
    [Range(20, 100)]
    private float damageTakenByProjectiles = 20;

    [SerializeField]
    private Material firstColor;

    [SerializeField]
    private Material secondColor;

    [SerializeField]
    [Range(0, 100)]
    private int blinks = 20;

    [SerializeField]
    [Range(0, 10)]
    private int blinkTime = 2;

    private GameObject camera;

    public Image healthbar;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        startHealth = health;
    }

    private void Update()
    {
        healthbar.GetComponent<Image>().fillAmount = health / startHealth;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("hit");
            Destroy(collision.gameObject);
            health -= damageTakenByProjectiles;
            camera.GetComponent<CameraBehaviour>().InduceStress(1);
            StartCoroutine(Blink());
            if (health <= 0) Destroy(this.gameObject);
        }
    }
    IEnumerator Blink()
    {
        for(int i = 0; i < blinks; i++)
        {
            GetComponent<Renderer>().material = secondColor;
            yield return new WaitForSeconds(0.5f * blinkTime / blinks);
            GetComponent<Renderer>().material = firstColor;
            yield return new WaitForSeconds(0.5f * blinkTime / blinks);
        }
        GetComponent<Renderer>().material = firstColor;
        blinks += 10;
    }

    public void AddHealth(float effect)
    {
        health += effect;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetStartHeath()
    {
        return startHealth;
    }
}
