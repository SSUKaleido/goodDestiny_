using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootingRange = 5f;
    public float patrolDistance = 5f;
    public float shootingInterval = 2f; // 발사 간격을 3초로 설정
    public float moveSpeed = 2f;

    private float timer = 0f;
    private bool isMovingRight = true;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어와 적 캐릭터 간의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, Player.instance.transform.position);

        // 플레이어가 일정 범위 안에 있고 발사 쿨다운 타이머가 지났을 때 발사
        if (distanceToPlayer <= shootingRange && timer >= shootingInterval)
        {
            Shoot();
            audioSource.Play();
            timer = 0f;
        }

        // 적 캐릭터가 일정 범위 안에서 왔다갔다하도록 처리
        Patrol();

        // 발사 쿨다운 타이머 업데이트
        timer += Time.deltaTime;
    }

    void Shoot()
    {
        // 발사체를 생성하고 발사 위치로 이동시킴
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }

    void Patrol()
    {
        // 적 캐릭터가 일정 범위 안에서 왔다갔다하도록 처리
        if (isMovingRight)
        {
            // 오른쪽으로 이동
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= patrolDistance)
            {
                // 일정 거리까지 이동했을 때, 방향을 바꾸고 스프라이트를 뒤집음
                isMovingRight = false;
                FlipCharacterSprite();
            }
        }
        else
        {
            // 왼쪽으로 이동
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= -patrolDistance)
            {
                // 일정 거리까지 이동했을 때, 방향을 바꾸고 스프라이트를 뒤집음
                isMovingRight = true;
                FlipCharacterSprite();
            }
        }
    }

    void FlipCharacterSprite()
    {
        // 캐릭터 스프라이트를 뒤집음 (좌우 반전)
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
