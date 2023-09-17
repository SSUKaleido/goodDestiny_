using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    #region Singleton
    public static ItemDatabase instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public bool isItemGet;
    public List<Item> itemDB = new List<Item>();
    #region Dictionary
    public Dictionary<string, string> itemInfo = new Dictionary<string, string>()
    {
        {"������", "��ų�� ��Ÿ���� 10�ۼ�Ʈ �����մϴ�."},
        {"����Ư��", "��ų�� ��Ÿ���� 30�ۼ�Ʈ �����մϴ�."},
        {"����1���� ���γ�Ʈ", "��ų�� ��Ÿ���� 50�ۼ�Ʈ �����մϴ�."},
        {"�νĵ� ����", "��� 250�� ��带 ����ϴ�."},
        {"���� ����", "��� 500�� ��带 ����ϴ�."},
        {"�ݺ� ����", "��� 1000�� ��带 ����ϴ�."},
        {"���", "���ݷ��� 10�ۼ�Ʈ ����մϴ�."},
        {"���� ��", "���ݷ��� 30�ۼ�Ʈ ����մϴ�."},
        {"Ȳ�ݺ� ��", "���ݷ��� 50�ۼ�Ʈ ����մϴ�."},
        {"���� ����", "�ִ� ü���� 20 �����մϴ�."},
        {"ö ����", "�ִ� ü���� 50 �����մϴ�."},
        {"������ ����", "�ִ� ü���� 80 �����մϴ�."},
        {"Ȱ", "��ų�� �������� �����մϴ�."},
        {"ȭ��", "��ų�� �������� �����մϴ�."},
        {"ü�� ����", "ü���� 40 ȸ���մϴ�."}

    };
    #endregion

    private void Update()
    {
        if (isItemGet)
        {
            StartCoroutine(SetBool());
        }
    }
    IEnumerator SetBool()
    {
        yield return new WaitForSeconds(0.1f);
        isItemGet = false;
    }
    
}
