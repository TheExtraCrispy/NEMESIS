using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Extension to rigidbody2d to add 2d alternative for AddExplosionForce from 3d rigidbodies, from stack overflow article here: https://stackoverflow.com/questions/34250868/unity-addexplosionforce-to-2d
public static class Rigidbody2DExt
{
    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0f, ForceMode2D mode = ForceMode2D.Force)
    {
        Vector2 explosionDirection = rb.position - explosionPosition;
        float explosionDistance = explosionDirection.magnitude;

        if(upwardsModifier == 0)
        {
            explosionDirection /= explosionDistance;
        }
        else
        {
            explosionDirection.y += upwardsModifier;
            explosionDirection.Normalize();
        }
        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDirection, mode);
    }
}
