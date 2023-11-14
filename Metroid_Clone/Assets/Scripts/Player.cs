using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
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
    public int maxHP = 99;
    public int healthPoints = 99;
    public GameObject playerWeapon;
    private Vector3 startPosition;
    private bool allowFire = true;
    public GameObject BulletPrefab;
    private bool facingRight = true;
    public GameObject HeavyBulletPrefab;
    private bool heavyBullets = false;
    private bool recentDamage = false;

    Renderer playerRen;
    Renderer weaponRen;

    void Start()
    {
        playerRen = GetComponent<Renderer>();
        weaponRen = playerWeapon.GetComponent<Renderer>();
        rigidBodyRef = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //Turns and moves the character right
            transform.rotation = Quaternion.Euler(new Vector3 (0f, 0f, 0f));
            facingRight = true;
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Turns and moves the character left
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            facingRight = false;
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            //Jumps
            HandleJump();
        }
        if (Input.GetKeyDown(KeyCode.Return) && allowFire)
        {
            //Shoot in the direction the player is facing
            if (heavyBullets) StartCoroutine(ShootHeavyBullet(facingRight));
            else StartCoroutine(ShootABullet(facingRight));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && allowFire)
        {
            //Shoot to the right
            if (heavyBullets) StartCoroutine(ShootHeavyBullet(true));
            else StartCoroutine(ShootABullet(true));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && allowFire)
        {
            //Shoot to the left
            if (heavyBullets) StartCoroutine(ShootHeavyBullet(false));
            else StartCoroutine(ShootABullet(false));
        }
        if (healthPoints > maxHP)
        {
            healthPoints = maxHP;
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

    public void gameOver()
    { 
        if (healthPoints == 0) SceneManager.LoadScene(2);
    }

    IEnumerator ShootHeavyBullet(bool shootRight)
    {
        allowFire = false;
        GameObject HeavyBulletInstance = Instantiate(HeavyBulletPrefab, playerWeapon.transform.position, playerWeapon.transform.rotation);
        HeavyBulletInstance.GetComponent<HeavyBullet>().goingRight = shootRight;
        yield return new WaitForSeconds(0.5f);
        allowFire = true;
    }

    IEnumerator ShootABullet(bool shootRight)
    {
        allowFire = false;
        GameObject BulletInstance = Instantiate(BulletPrefab, playerWeapon.transform.position, playerWeapon.transform.rotation);
        BulletInstance.GetComponent<Bullet>().goingRight = shootRight;
        yield return new WaitForSeconds(0.5f);
        allowFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door A")
        {
            transform.position = other.gameObject.GetComponent<Door>().teleportPoint.transform.position;
            startPosition = transform.position;
        }
        if (other.gameObject.tag == "HeavyBulletItem")
        {
            heavyBullets = true;
        }
        if (other.gameObject.tag == "Enemy" && recentDamage == false)
        {
            recentDamage = true;
            healthPoints -= 15;
            gameOver();
            StartCoroutine(DamageBlink());
        }
        if (other.gameObject.tag == "Jetpack")
        {
            jumpForce += 5;
        }
        if (other.gameObject.tag == "HealthPickup")
        {
            healthPoints += other.gameObject.GetComponent<Pickup>().playerHeal;
        }
        if (other.gameObject.tag == "HealthBoost")
        {
            maxHP = 199;
            healthPoints = maxHP;
        }
    }

    IEnumerator DamageBlink()
    {
        for (int i = 0; i < 8; i++)
        {
            playerRen.enabled = false;
            weaponRen.enabled = false;
            yield return new WaitForSeconds(0.3f);
            playerRen.enabled = true;
            weaponRen.enabled = true;
            yield return new WaitForSeconds(0.3f);
        }
        recentDamage = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HardEnemy" && recentDamage == false)
        {
            recentDamage = true;
            healthPoints -= 35;
            gameOver();
            StartCoroutine(DamageBlink());
        }
    }
}
