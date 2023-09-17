using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSphere_Shooting : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int HIT_DAMAGE;
    private Transform player; // 플레이어를 추적하기 위한 변수
    private bool isMoving = true; // 총알이 움직이는지 여부를 나타내는 변수
    private Vector3 initialDirection; // 초기 방향을 저장할 변수

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 오브젝트 찾기
        initialDirection = (player.position - transform.position).normalized; // 초기 방향 설정
    }

    void Update()
    {
        if (isMoving)
        {
            // 초기 방향으로 총알 이동
            transform.Translate(initialDirection * bulletSpeed * Time.deltaTime);

            // Ray를 발사하여 충돌 감지
            RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right);

            // Ray에 충돌체가 존재하고 그 충돌체의 태그가 "Player"인 경우
            if (ray.collider != null && ray.collider.tag == "Player")
            {
                // Debug.Log("Player 태그 감지");
                // 총알 파괴 함수 호출
                DestroyBullet();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.TakeDamage(HIT_DAMAGE);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Wall")
        {
            DestroyBullet();
        }
    }
    void DestroyBullet()
    {
        // 총알 파괴
        Destroy(gameObject);
        isMoving = false; // 총알이 파괴되면 움직임을 중지
    }
}
