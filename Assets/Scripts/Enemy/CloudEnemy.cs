using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloudEnemy : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawnPrefab; // 위치 프리팹
    public Transform[] spawnPositions;
    public GameObject spherePrefab;
    public Transform player; // 플레이어의 Transform을 할당합니다.
    public float detectionRange = 10f; // 플레이어를 감지할 범위를 설정합니다.

    private bool hasSpawned = false;
    private float timer;
    private int bulletsShot = 0;
    private int maxBullets = 10;

    Vector3 lookVec;
    Vector3 tauntVec;

    void Start()
    {
        InvokeRepeating("StartThinking", 1f, 15f);
    }

    void StartThinking()
    {
        // 플레이어와의 거리를 계산하여 공격 여부를 결정합니다.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            StartCoroutine(Think());
        }
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(1f);

        int ranAction = Random.Range(0, 4);
        switch (ranAction)
        {
            case 0:
            case 1:
                StartCoroutine(Shooting());
                break;
            case 2:
            case 3:
                StartCoroutine(SphereRandomBullet());
                break;
        }
    }

    IEnumerator Shooting()
    {
        bulletsShot = 0;
        while (bulletsShot < maxBullets)
        {
            timer += Time.deltaTime;

            if (timer > 0.5)
            {
                timer = 0;

                // 위치 프리팹을 인스턴스화하고 위치를 가져옴
                GameObject bulletSpawn = Instantiate(bulletSpawnPrefab, transform.position, Quaternion.identity);
                Vector3 shootPosition = bulletSpawn.transform.position;

                // 총알 발사
                Shoot(shootPosition);
                bulletsShot++;

                // 위치 프리팹 삭제
                Destroy(bulletSpawn);
            }

            yield return null;
        }
        yield return new WaitForSeconds(2f);
    }

    void Shoot(Vector3 shootPosition)
    {
        Instantiate(bullet, shootPosition, Quaternion.identity);
    }

    IEnumerator SphereRandomBullet()
    {
        if (!hasSpawned)
        {
            int randomIndex1 = Random.Range(0, spawnPositions.Length);
            int randomIndex2 = Random.Range(0, spawnPositions.Length);

            while (randomIndex2 == randomIndex1)
            {
                randomIndex2 = Random.Range(0, spawnPositions.Length);
            }

            GameObject sphere1 = Instantiate(spherePrefab, spawnPositions[randomIndex1].position, Quaternion.identity);
            GameObject sphere2 = Instantiate(spherePrefab, spawnPositions[randomIndex2].position, Quaternion.identity);

            hasSpawned = true;

            yield return new WaitForSeconds(9f);

            Destroy(sphere1);
            Destroy(sphere2);
            hasSpawned = false;
        }
        yield return new WaitForSeconds(2f);
    }
}
