using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Sharkey, Logan
 * 10/26/2023
 * All the code that retains to the player 
 */

public class Player : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody rigidBodyRef;
    public float jumpForce = 3f;
    public int lives = 3;

    private bool facingRight = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            facingRight = true;
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            facingRight = false;
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            HandleJump();
        }
    }

    private void HandleJump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.down, out hit, 1.3f))
        {
            //Checks if player is touching the ground, if not, then jump. 
            rigidBodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }



    }





}
