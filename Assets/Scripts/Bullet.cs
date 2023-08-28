using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15;

    void Start()
    {
        Invoke("DestroyBullet", 5);
    }

    void Update()
    {
        if(transform.rotation.y == 0)  
           transform.Translate(transform.right * speed * Time.deltaTime);   
        else
            transform.Translate(transform.right *(-1) * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
