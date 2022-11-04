using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string[] targetTags;
    public float damage;
    public float impactForce;
    public float speed;
    public float lifetime;
    float lifeRemaining;
    public float maxDistance;
    public float pierceCount;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = (Vector2)transform.up * speed;
        lifeRemaining = lifetime;
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        lifeRemaining -= Time.deltaTime;
        if(lifeRemaining <= 0)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in targetTags)
        {
            if (collision.CompareTag(tag))
            {
                GameObject targetObject = collision.gameObject;
                if(targetObject.TryGetComponent(out Health targetHealth))
                {
                    targetHealth.ModifyHealth(-damage);
                    collision.attachedRigidbody.AddForce(rb.velocity.normalized * impactForce);
                    --pierceCount;
                    if (pierceCount <= 0)
                    {
                        Deactivate();
                    }
                }
                else
                {
                    Deactivate();
                }
            }
        }
    }
}
