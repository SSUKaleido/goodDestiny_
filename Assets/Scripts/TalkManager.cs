using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public static TalkManager instance;
    Dictionary<int, string[]> talkData;
    
    void Awake()
    {
        instance = this;
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(0, new string[] { "여름방학이 끝난지 얼마 되지 않은 8월 어느 날, 지방의 한 고등학교", "\"하... 지난 3년을 열심히 공부 안하고 날렸더니 내신이 엉망이네.\"", 
                                        "\"지금 성적으로는 인서울은 꿈도 못 꾸는데...\"", "\"결국 수능만이 유일한 희망인가?\"", "\"이렇게 된 이상, 100일의 기적 만들어보자!\"" });
        talkData.Add(1, new string[] { "\"아무리 늦게 시작했다고 해도 9모에서 이 성적은 처참한데...\"", "\"아직 개념적인 부분도 완벽하지 않고, 단순한 함정에도 다 걸리고 있어.\"",
                                        "\"이대로는 안된다는 걸 알지만 어디에서부터 해야할 지도 모르겠어...\"","\"아무것도 안 하고 보낸 지난 인생이 너무 아까워.\"","\"나는 왜 이럴까.\"","그녀의 부정적인 감정은 연기처럼 흐려졌다 이윽고 구름처럼 모였다."});
        talkData.Add(2, new string[] { "\"그래, 자책해봤자 바뀌는 건 하나도 없어.\"", "\"지금 성적이 안 좋은 건 오히려 발전 가능성이 많다는 뜻이야.\"", "\"후회는 수능이 끝난 뒤에 하자.\"", "\"남은 두 달 동안 최선을 다해보자.\"" });
        talkData.Add(3, new string[] { "D-30", "\"어느새 수능이 한 달 남았구나.\"", "\"어느 정도 마음이 정리되니까 공부가 일사천리로 나아가는 것 같아.\"", "\"막판 스퍼트로 킬러 문항 위주로 가볼까?\"" });
        talkData.Add(4, new string[] { "\"확실히 9월보다는 나아졌지만 여전히 부족해.\"", "\"이건 단순히 문제가 어렵다는 느낌이 아니야.\"", "\"내가 내 실력을 믿지 못하고 있어...\"" });
        talkData.Add(5, new string[] { "\"그 이후로는 내게 큰 걸림돌은 없었다.\"", "\"부모님도 나를 많이 배려해 주셨고, 나는 그 기대에 부응할 수 있었다.\"", "\"결론적으로, 나는 인서울! 그 중 숭실대라는 곳에 입학했다!\"",
                                           "\"우리 고향과 서울은 멀어서 나는 자취를 시작했다. 어른이 된 것 같은 기분이야.\"","\"물론 대학은 인생의 종착지가 아니야. 현실에 안주하지 말고 계속 나아가자.\"",
                                            "\"그치만 오늘은 술도 좀 마셨고 잠쉬 쉬어 가 도 되지않을ㅇㄴㄹㄻ\""});
    }
    public string GetTalk(int storyIndex,int talkIndex)
    {
        if (talkIndex == talkData[storyIndex].Length)
            return null;
        else
            return talkData[storyIndex][talkIndex];
    }

}
