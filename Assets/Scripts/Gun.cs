using System;
using StarterAssets;
using UnityEngine;

public class Gun : MonoBehaviour
{
    StarterAssetsInputs _input;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletPoint;
    [SerializeField] float fireRate = 1f;

    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (_input.shoot)
        {
            Shoot();
            _input.shoot = false;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * fireRate);
        Destroy(bullet, 1f);
    }
}
