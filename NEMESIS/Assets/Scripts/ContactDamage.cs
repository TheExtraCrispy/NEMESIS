using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ContactDamage : MonoBehaviour
{
    public string[] targetTags;
    [SerializeField] float contactDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        if (targetTags.Contains(collidingObject.tag)){
            collidingObject.GetComponent<Health>().DamageHealth(contactDamage, "contact");
        }
    }
}
