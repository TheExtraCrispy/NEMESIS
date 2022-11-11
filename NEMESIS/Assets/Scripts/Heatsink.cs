using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Heatsink : MonoBehaviour
{
    public float launchForce;
    public float damage;
    public float radius;
    public float force;
    public string causeOfDeath;
    [SerializeField] GameObject explosionPrefab;
    public String[] targetTags;
    private float heatPercent;
    private Health health;

    private void Awake()
    {
        health = gameObject.AddComponent<Health>();
        health.InitializeHealth(1);
        health.Killed += OnHit;
    }

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.up * launchForce);
    }

    private void OnHit(object sender, EventArgs args)
    {
        //Debug.Log("This thing should have exploded");
        Explosive kaboom = Instantiate(explosionPrefab, transform.position, transform.rotation).GetComponent<Explosive>(); ;
        kaboom.targetTags = targetTags;
        kaboom.damage = damage;
        kaboom.blastForce = force;
        kaboom.blastRadius = radius;
        kaboom.causeOfDeath = causeOfDeath;
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        health.Killed -= OnHit;
    }
}
