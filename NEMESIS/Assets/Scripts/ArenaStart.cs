using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaStart : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject arena;
    [SerializeField] GameObject textMesh;
    [SerializeField] float rotationAccel;
    [SerializeField] float maxRotation;
    [SerializeField] float timeToStart;
    private bool isEntered = false;
    private float timeLeft;
    private float rotationSpeed = 0;


    void Start()
    {
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTERED BY: " + collision.name);
        isEntered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("EXITED BY: " + collision.name);
        isEntered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEntered)
        {
            timeLeft -= Time.deltaTime;
            rotationSpeed += Time.deltaTime * rotationAccel;
        }
        else
        {
            timeLeft += Time.deltaTime;
            rotationSpeed += Time.deltaTime * rotationAccel;
        }

        timeLeft = Mathf.Clamp(timeLeft, 0, timeToStart);
        rotationSpeed = Mathf.Clamp(rotationSpeed, 0, maxRotation);

        transform.Rotate(Vector3.forward, rotationSpeed*Time.deltaTime);

        if (timeLeft == 0)
        {
            Debug.Log("ARENA SPAWN");
            Instantiate(arena, transform.position, arena.transform.rotation);
            Destroy(textMesh);
            Destroy(gameObject);
        }
    }
}
