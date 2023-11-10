using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Sharkey, Logan
 * 11/10/23
 * Script for regular bullets
 */

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool goingRight;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }


}
