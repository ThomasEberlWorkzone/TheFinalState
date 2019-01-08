﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behaviour : MonoBehaviour
{

    private int rotation;
    private int startRotation;
    private bool rotatingUp;
    private const int DIST = 1000;
    private const float VELO_FACTOR = 10000f;


    private int health = 100;

    public AudioClip EnemyHurt;

	void Start ()
    {
        rotation = (int) transform.eulerAngles.z;
        startRotation = (int) transform.eulerAngles.z;

        InvokeRepeating("Rotate", 0, 0.01f);
        InvokeRepeating("Fire",0,1);

    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 direction = DegreeToVector2(rotation);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        if (hit.collider.name.Equals("CommunistIdle"))
        {
            Debug.Log("Hit:" + hit.collider.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            health -= 25;

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(EnemyHurt);
            Destroy(coll.gameObject);
            killPlayer(isAlive());
        }
    }

    private bool isAlive()
    {
        if (health > 0)
            return true;

        return false;
    }

    private void killPlayer(bool isAlive)
    {
        if(!isAlive)
        {
            float xPos = transform.position.x;
            float yPos = transform.position.y;

            Vector3 enemyPos = new Vector3(xPos, yPos, 0);
            Quaternion enemyRot = transform.rotation;
            gameObject.SetActive(isAlive);
            Destroy(this);
            Instantiate(GameObject.FindWithTag("DeadBody"), enemyPos, enemyRot);
        }
    }


    private void Rotate()
    {
        if(rotation == (startRotation - 60))
        {
            rotatingUp = true;
        }
        else if(rotation == (startRotation + 60))
        {
            rotatingUp = false;
        }

        if(rotatingUp)
        {
            rotation += 1;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        else
        {
            rotation -= 1;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad));
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(GameObject.FindGameObjectWithTag("EnemyProjectile"), transform.position, Quaternion.Euler(0,0,rotation-90));
        bullet.GetComponent<Rigidbody2D>().AddForce(DegreeToVector2(rotation-90)*1000);
        bullet.GetComponent<Rigidbody2D>().isKinematic = false;

        Destroy(bullet, 2f);
    }
}
