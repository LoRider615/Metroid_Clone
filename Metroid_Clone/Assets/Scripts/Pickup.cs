using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Sharkey, Logan
 * 11/10/23
 * Script for item that unlocks heavy bullets
 */

public class Pickup : MonoBehaviour
{
    public int playerHeal = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }

}
