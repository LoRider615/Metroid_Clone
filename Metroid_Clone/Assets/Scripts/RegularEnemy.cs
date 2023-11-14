using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Sharkey, Logan
 * 11/10/23
 * Script for handling the regular enemy movement
 */

public class RegularEnemy : MonoBehaviour
{
    public float travelDistanceRight = 0;
    public float travelDistanceLeft = 0;
    public float speed = 3;

    private float startingX;
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        startingX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            if (transform.position.x <= startingX + travelDistanceRight) 
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                movingRight = false;
            }
        }
        else
        {
            if (transform.position.x >= startingX + travelDistanceLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                movingRight = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet") 
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Heavy Bullet") 
        {
            Destroy(this.gameObject);
        }
    }
}
