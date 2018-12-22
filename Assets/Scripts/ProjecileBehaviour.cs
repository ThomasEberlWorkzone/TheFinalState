using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecileBehaviour : MonoBehaviour
{
    private float firingCooldown;
    private const int MOUSE_LEFT = 0;
    private const float VELO_FACTOR = 10000f;
    private int ammoInGun;
    private int ammoReserve;

    private GameObject projectile;

    // Use this for initialization
    void Start()
    {
        ammoInGun = 30;
        ammoReserve = 120;
    }

    // todo: fix firing
    void Update()
    {
        if(Input.GetMouseButtonDown(MOUSE_LEFT))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject crosshair = GameObject.FindGameObjectWithTag("Crosshair");

            Fire(player.transform.position, crosshair.transform.position, player.transform.rotation);
        }
    }

    private void Fire(Vector2 playerPosition, Vector2 crosshairPosition, Quaternion rotation)
    {
        float veloLengthFactor = crosshairPosition.magnitude - playerPosition.magnitude;

        if (veloLengthFactor < 0)
            veloLengthFactor *= -1;

        float absolutFactor = VELO_FACTOR / veloLengthFactor;

        if (absolutFactor > 3500f)
            absolutFactor = 3500f;

        GameObject bullet = Instantiate(GameObject.FindGameObjectWithTag("Projectile"), playerPosition, rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce((crosshairPosition - playerPosition) * absolutFactor);
        bullet.GetComponent<Rigidbody2D>().isKinematic = false;

        Destroy(bullet, 4f);

        Collider2D bulletCollider = bullet.GetComponent<Rigidbody2D>().GetComponent<Collider2D>();
        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<PolygonCollider2D>();
        Physics2D.IgnoreCollision(bulletCollider, playerCollider);
    }

    
}
