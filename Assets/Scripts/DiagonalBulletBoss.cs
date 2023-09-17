using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DigonalBulletBoss : MonoBehaviour
{
    public float speed = 15;
    public float distance;
    public LayerMask isLayer;
    public float zRotation = 30f; // Z 축 회전 각도를 조정하는 변수
    public Vector3 diagonalDirection;
    private float yRotation = 0f;

    void Start()
    {
        Invoke("DestroyBullet", 5);
        diagonalDirection = Quaternion.Euler(0, yRotation, zRotation) * Vector3.right;
    }

    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.tag == "Player")
            {
                Debug.Log("명중!!!");
            }
            DestroyBullet();
        }
        if (transform.rotation.y == 0)
            yRotation = 0f;
        else
            yRotation = 180f;

        if (transform.rotation.y == 0)
            transform.Translate(diagonalDirection.normalized * speed * Time.deltaTime);
        else
            transform.Translate(diagonalDirection.normalized * speed * Time.deltaTime);
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