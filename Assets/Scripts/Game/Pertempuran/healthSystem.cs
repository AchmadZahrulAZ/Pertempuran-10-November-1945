using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystem : MonoBehaviour
{
    public float maxhealth;

    public float currentHealth { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            if(gameObject.tag == "Enemy"){
                Destroy(gameObject);
            }


            else if(gameObject.tag == "Player"){
                Destroy(gameObject);
            }
        }
    }
}
