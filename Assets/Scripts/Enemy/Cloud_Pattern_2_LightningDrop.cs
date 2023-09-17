using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Pattern_2_LightningDrop : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = Vector3.down; 
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    void Update()
    {
        
    }
}
