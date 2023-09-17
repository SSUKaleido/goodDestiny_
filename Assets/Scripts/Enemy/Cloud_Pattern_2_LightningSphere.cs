using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Pattern_2_LightningSphere : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject Sphere;

    private Vector3 bulletPos;

    private float timer;
    // private int bulletsShot = 0;
    private int maxBullets = 10;

    private bool isShooting = false;

    void Start()
    {
        bulletPos = Sphere.transform.position;
    }

    void Update()
    {
        if (!isShooting)
        {
            timer += Time.deltaTime;

            if (timer >= 3f)
            {
                timer = 0;
                isShooting = true;
                StartCoroutine(ShootBullets());
            }
        }
    }

    IEnumerator ShootBullets()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
            yield return new WaitForSeconds(0.5f); 
        }

        Destroy(Sphere);
    }
}
