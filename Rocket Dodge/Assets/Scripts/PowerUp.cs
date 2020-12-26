using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {
    private float begin;
    // public GameObject pickUpEffect;
    public Text howLong;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pickUp(other);
        }
    }
    

    void pickUp(Collider2D player)
    {
        //Instantiate(pickUpEffect, transform.position, transform.rotation);
       while(Time.time > 5.0f)
        {
            howLong.text = "You are Invincible";
        }

        Destroy(gameObject);
    }

   
	
}
