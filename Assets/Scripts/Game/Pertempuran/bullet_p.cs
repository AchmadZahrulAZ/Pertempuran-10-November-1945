using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_p : MonoBehaviour
{
    public float bulletSpeed;
    public string tagCollide;
    public float damage { get; set;} //bisa diset pada playerShoot_p.cs
    [SerializeField]private float lifeTime;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);   
    }

    private void FixedUpdate() {
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tagCollide)){
            Debug.Log("Collide with " + collision.tag);
            collision.GetComponent<healthSystem>().takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
