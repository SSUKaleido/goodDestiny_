using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Sword, Range };
    public Type type;
    public int damage;
    public float rate;

    public BoxCollider swordArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;


    public void Use()
    {
        if (type == Type.Sword)
        {
            StopCoroutine("Swing"); //: 코루틴 정지함수
            StartCoroutine("Swing"); //: 코루틴 실행함수
        }
        else if (type == Type.Range)
        {
            StartCoroutine("Shot");
        }
    }

    //일반함수: 메인루틴(Use) -> 서브루틴(Swing) -> 메인루틴(Use()) 로 교차실행
    //코르틴 함수: 메인루틴(Use) + 코루틴(Swing) 로 동시(Co-Op)실행

    //IEnumerator: 열거형 함수 클래스
    //yield: 결과를 전달하는 키워드(코루틴 내 1개이상)
    //(yield +) break:코루틴탈출, null:1프레임대기
    //yield 키워드를 여러개 사용하여 시간차 로직작성가능
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f); // 0.1초 대기
        swordArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.5f);
        swordArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        //#1.총알 발사
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
    }
}
