using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnenmy : MonoBehaviour
{
    public int healthPoints = 10;
    public float speed = 5f;
    public GameObject playerRef;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef.transform.position.x < transform.position.x) 
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (playerRef.transform.position.x > transform.position.x)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            healthPoints -= 1;
            if (healthPoints <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (other.gameObject.tag == "Heavy Bullet")
        {
            healthPoints -= 3;
            if (healthPoints <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
