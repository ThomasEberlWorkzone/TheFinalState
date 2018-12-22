using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behaviour : MonoBehaviour
{

    private int rotation;
    private int startRotation;
    private bool rotatingUp;
    private const int DIST = 1000;

    private int health = 100;

	void Start ()
    {
        rotation = (int) transform.eulerAngles.z;
        startRotation = (int) transform.eulerAngles.z;

        InvokeRepeating("Rotate", 0, 0.01f);
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void UpdateLineOfSight()
    {

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            health -= 25;

            setPlayerInactive(isAlive());
        }
    }

    private bool isAlive()
    {
        if (health > 0)
            return true;

        return false;
    }

    private void setPlayerInactive(bool isAlive)
    {
        gameObject.SetActive(isAlive);
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
}
