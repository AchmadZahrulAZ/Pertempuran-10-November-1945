using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot_p : MonoBehaviour
{
    [SerializeField]private GameObject bulletPrefab;
    [SerializeField]private Transform firePoint;

    [Range(0.1f, 2f)]
    [SerializeField]private float fireRate;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && timer <= 0f){
            Shoot();
            timer = fireRate;
        }
        else{
            timer -= Time.deltaTime;
        }
        
    }

    private void Shoot(){
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
