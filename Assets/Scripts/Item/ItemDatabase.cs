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
        {"교과서", "스킬의 쿨타임이 10퍼센트 감소합니다."},
        {"수능특강", "스킬의 쿨타임이 30퍼센트 감소합니다."},
        {"전교1등의 공부노트", "스킬의 쿨타임이 50퍼센트 감소합니다."},
        {"부식된 동전", "즉시 250의 골드를 얻습니다."},
        {"은빛 동전", "즉시 500의 골드를 얻습니다."},
        {"금빛 동전", "즉시 1000의 골드를 얻습니다."},
        {"목검", "공격력이 10퍼센트 상승합니다."},
        {"무딘 검", "공격력이 30퍼센트 상승합니다."},
        {"황금빛 검", "공격력이 50퍼센트 상승합니다."},
        {"나무 방패", "최대 체력이 20 증가합니다."},
        {"철 방패", "최대 체력이 50 증가합니다."},
        {"내면의 강함", "최대 체력이 80 증가합니다."},
        {"활", "스킬의 데미지가 증가합니다."},
        {"화살", "스킬의 데미지가 증가합니다."},
        {"체력 물약", "체력을 40 회복합니다."}

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
