using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Enemy"))
        {
            return;
        }
        Debug.Log("Hit");
        if(other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Player hit");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().TakeDamage();
        }
        gameObject.SetActive(false);
        Destroy(this);
    }
}
