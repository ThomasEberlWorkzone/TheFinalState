﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private const float movementFactor = 0.25f;

    private Rigidbody2D rb2d;
    private Vector2 newLocation;

	// Use this for initialization
	void Start ()
    {
        Cursor.visible = false;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        updatePlayerPositon();        
        updatePlayerRotation();
    }

    private void updatePlayerPositon()
    {
        //get input of user of game object
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        newLocation = new Vector2((xInput * movementFactor + transform.position.x), (yInput * movementFactor + transform.position.y));

        float xPos = transform.position.x;
        float yPos = transform.position.y;
        Camera.main.transform.position = new Vector3(xPos, yPos, -10);
    }

    //todo fix mouse rotation, so that player always turns towards crosshair
    private void updatePlayerRotation()
    {
        Vector3 positionPlayerOnScreen = transform.position;
        Vector3 positionMouseOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject cursor = GameObject.FindGameObjectWithTag("Crosshair");
        cursor.transform.position = new Vector3(positionMouseOnScreen.x, positionMouseOnScreen.y);

        float angle = AngleBetweenTwoPoints(positionPlayerOnScreen, positionMouseOnScreen);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90));
    }

    private float AngleBetweenTwoPoints(Vector3 p1, Vector3 p2)
    {
        return   Mathf.Atan2(p1.y - p2.y, p1.x - p2.x) * Mathf.Rad2Deg - 90;
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(newLocation);
        rb2d.angularVelocity = 0;
    }
}
