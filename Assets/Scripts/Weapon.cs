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
            StopCoroutine("Swing"); //: �ڷ�ƾ �����Լ�
            StartCoroutine("Swing"); //: �ڷ�ƾ �����Լ�
        }
        else if (type == Type.Range)
        {
            StartCoroutine("Shot");
        }
    }

    //�Ϲ��Լ�: ���η�ƾ(Use) -> �����ƾ(Swing) -> ���η�ƾ(Use()) �� ��������
    //�ڸ�ƾ �Լ�: ���η�ƾ(Use) + �ڷ�ƾ(Swing) �� ����(Co-Op)����

    //IEnumerator: ������ �Լ� Ŭ����
    //yield: ����� �����ϴ� Ű����(�ڷ�ƾ �� 1���̻�)
    //(yield +) break:�ڷ�ƾŻ��, null:1�����Ӵ��
    //yield Ű���带 ������ ����Ͽ� �ð��� �����ۼ�����
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f); // 0.1�� ���
        swordArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.5f);
        swordArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        //#1.�Ѿ� �߻�
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
    }
}
