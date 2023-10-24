using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teamShoot_p : MonoBehaviour
{
    [SerializeField]private GameObject bulletPrefab;
    [SerializeField]private Transform firePoint;

    [Range(0.1f, 2f)]
    [SerializeField]private float fireRate;
    private float timer;
    public float damage;

    public string teamdamage;

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0f){
            Shoot();
            timer = fireRate;
        }
        else{
            timer -= Time.deltaTime;
        }
        
    }

    private void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<bullet_p>().damage = damage;
        bullet.GetComponent<bullet_p>().tagCollide = teamdamage;
    }
}
