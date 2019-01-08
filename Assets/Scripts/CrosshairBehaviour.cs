using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairBehaviour : MonoBehaviour {

    // Use this for initialization
    private const float MAX_DISTANCE = 500;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.LeftShift))
        {

            Vector3 mousePos = Input.mousePosition;
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (MouseMoved())
            {
                float sensitivity = 0.05f;
                Vector3 vp = Camera.main.ScreenToViewportPoint(mousePos);
                vp.x -= 0.5f;
                vp.y -= 0.5f;
                vp.x *= sensitivity;
                vp.y *= sensitivity;
                vp.x += 0.5f;
                vp.y += 0.5f;
                Vector3 sp = Camera.main.ViewportToWorldPoint(vp);
                Camera.main.transform.position = sp;
            }          
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float xPos = player.transform.position.x;
            float yPos = player.transform.position.y;

            Camera.main.transform.position = new Vector3(xPos, yPos, -10);
        }

    }

    private bool MouseMoved()
    {
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            return true;
        }

        return false;
    }
}
