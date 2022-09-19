using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    private Rigidbody2D rb;
    public float accelerationTime = 2f;
    public float maxSpeed = 5f;
    private Vector2 movement;
    private float timeLeft;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime, Space.World);

        ////Debug.Log(timeLeft);
        //timeLeft -= Time.deltaTime;
        //if (timeLeft <= 0)
        //{
        //    movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        //    timeLeft += accelerationTime;
        //}
    }

    void FixedUpdate()
    {
        //rb.AddForce(movement * maxSpeed);
    }
}
