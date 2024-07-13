using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject projectile1;
    public GameObject projectile2;
    Animator myAnim;
    // float gameStage;
    float enemyNo;
    float enemy2No;
    // public Rigidbody2D myRB;
    // public float fireRate = 0.5f;
    // public float ammo;
    // float maxAmmo = 4;
    // float nextShoot = 0f;
    public float shotRate;
    float maxHealth;
    float nextShot;

    // Start is called before the first frame update
    void Start()
    {
        // myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        maxHealth = GetComponent<enemyHealth>().getHealth();
        // gameStage = 1f;
        enemyNo = 0f;
        enemy2No = 0f;
        nextShot = 0f;
        // theHearts = GetComponent<Image>();
        // myHearts = heartsAnim.GetComponent<Animator>();
        // fRight = false;
        // Instantiate(projectile, shootPoint.position, Quaternion.identity);
        // Euler(new Vector3(0,-180,0))
    }

    // Update is called once per frame
    void Update()
    {
        float currentHealth = GetComponent<enemyHealth>().getHealth();
        // Debug.Log(maxHealth);
        // Debug.Log()

        if (currentHealth > maxHealth/2 && Time.time>=nextShot && enemyNo <= 8){
            nextShot = Time.time + shotRate;
            myAnim.SetTrigger("shooting");
            Instantiate(projectile1, shootPoint.position, Quaternion.identity);
            enemyNo += 1;
        }
        else if (currentHealth <= maxHealth/2 && Time.time>=nextShot && enemy2No <= 8){
            nextShot = Time.time + shotRate;
            myAnim.SetTrigger("shooting");
            Instantiate(projectile2, shootPoint.position, Quaternion.identity);
            enemy2No += 1;
        }
    }
}
