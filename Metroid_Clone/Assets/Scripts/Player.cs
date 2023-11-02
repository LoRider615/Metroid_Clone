using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* Sharkey, Logan
 * 10/26/2023
 * All the code that retains to the player 
 */

public class Player : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody rigidBodyRef;
    public float jumpForce = 3f;
    public int lives = 3;
    public int healthPoints = 99;
    public GameObject playerWeapon;

    public GameObject BulletPrefab;
    private bool facingRight = true;

    void Start()
    {
        rigidBodyRef = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(new Vector3 (0f, 0f, 0f));
            facingRight = true;
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            facingRight = false;
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            HandleJump();
        }
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            ShootABullet(facingRight);
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

    public void LoseALife()
    {
        lives--;
        if (lives == 0) SceneManager.LoadScene(2);
    }

    private void ShootABullet(bool shootRight)
    {
        GameObject BulletInstance = Instantiate(BulletPrefab, transform.position, transform.rotation);
        BulletInstance.GetComponent<Bullet>().goingRight = shootRight;
    }


}
