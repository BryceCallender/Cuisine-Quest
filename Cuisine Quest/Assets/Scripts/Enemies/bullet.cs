using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int speed;
    Vector3 velocity;
    float timer;

	// Use this for initialization
	void Start ()
    {
        timer = 0.0f;

	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += velocity * speed * Time.deltaTime;
        if(timer > 3.0f)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
	}

    public void setVelocity(Vector2 vel)
    {
        velocity = vel;
    }
}
