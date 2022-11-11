using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Explosive : MonoBehaviour
{
    public float blastRadius;
    [SerializeField] float expansionRate; //how much the radius expands per second
    public float blastForce;
    public float damage;
    public string causeOfDeath;
    [SerializeField] float damageDropoff; //what percent of damage you will take at the farthest edge
    
    public string[] targetTags;
    private List<Collider2D> targetHits = new List<Collider2D>();
    float currentRadius;

    

    private void Start()
    {
    }
    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentRadius);
        if (currentRadius >= blastRadius)
        {
            targetHits.Clear();
            Destroy(gameObject);
        }
        foreach (Collider2D hit in hits)
        {
            if (targetTags.Contains(hit.tag) && !targetHits.Contains(hit))
            {
                targetHits.Add(hit);
                hit.attachedRigidbody.AddExplosionForce(blastForce, transform.position, currentRadius);
                if (hit.TryGetComponent<Health>(out Health targetHealth))
                {
                    float dropoffFactor = damageDropoff / (currentRadius / blastRadius);
                    targetHealth.DamageHealth(Mathf.Min(damage, damage * dropoffFactor), causeOfDeath);
                }
            }
        }
        currentRadius += expansionRate * Time.deltaTime;
        //Debug.DrawLine(transform.position, transform.right * currentRadius, Color.red, 300);
        transform.localScale = Vector3.one * currentRadius * 2;
    }

    private void RemoveExplosion()
    {
        Destroy(gameObject);
    }
}
