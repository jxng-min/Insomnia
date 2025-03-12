using System.Collections.Generic;
using UnityEngine;
using System;

public enum ObjectName
{
    Bed1 = 100, Bed2 = 200, Bed3 = 300, Bed4 = 400,
    Guitar1 = 1100, Guitar2 = 1200, Guitar3 = 1300, Guitar4 = 1400,
    Emp1 = 2100, Emp2 = 2200,
    Box1 = 3100, Box2 = 3200, Box3 = 3300, Box4 = 3400, Box5 = 3500,
    Bag1 = 4100, Bag2 = 4200,
    Closet1 = 5100, Closet2 = 5200, Closet3 = 5300, Closet4 = 5400, Closet5 = 5500,
    Shirt_Closet = 6100,
    Book_Case1 = 7100, Book_Case2 = 7200, Book_Case3 = 7300,
    Computer = 8100, TV = 8200,
    Light1 = 9100, Light2 = 9200, Light3 = 9300, Light4 = 9400,
    Chair1 = 10100, Chair2 = 10200, Chair3 = 10300, Chair4 = 10400, Chair5 = 10500, Chair6 = 10600, Chair7 = 10700,
    Table1 = 11100, Table2 = 11200, Book_Table = 11300,
    Terrace = 12100, Exit = 12200, Temp = 12300,
    None = 10000,
}

public class DialogueManager : Singleton<DialogueManager>
{
    private Dictionary<int, string[]> m_talk_data;

    private void Start()
    {
        m_talk_data = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        // Dialogue Data
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bed1), new string[] {"눕고 싶은 기분이 아니야.", "소리의 원인을 찾아야 해."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bed2), new string[] {"옵션으로 제공되는 침대야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bed3), new string[] {"여자친구랑 같이 자고는 했던 침대야.", "보고싶네."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bed4), new string[] {"눕고 싶은 기분이 아니야.", "원인을 찾아야 해."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Guitar1), new string[] {"집에 이사오면서 구매한 한정판 어쿠스틱 기타야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Guitar2), new string[] {"예전에 큰 맘 먹고 샀던 일렉 기타야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Guitar3), new string[] {"예전에 큰 맘 먹고 샀던 일렉 기타야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Guitar4), new string[] {"예전에 큰 맘 먹고 샀던 일렉 기타야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Emp1), new string[] {"기타에 연결해서 사용하는 앰프다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Emp2), new string[] {"기타에 연결해서 사용하는 앰프다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box1), new string[] {"이사를 하고 나서 정리하지 못한 짐들이야.", "컨디션이 괜찮아지면 정리해야겠어."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box2), new string[] {"이사를 하고 나서 정리하지 못한 짐들이야.", "쌓여있는 짐들을 보면 정리 하기 싫어."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box3), new string[] {"이사를 하고 나서 정리하지 못한 짐들이야.", "컨디션이 괜찮아지면 정리해야겠어."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box4), new string[] {"이사를 하고 나서 정리하지 못한 짐들이야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box5), new string[] {"이사를 하고 나서 정리하지 못한 짐들이야.", "컨디션이 괜찮아지면 정리해야겠어."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bag1), new string[] {"언젠가 사용한 적이 있던 캐리어야.", "자물쇠로 굳게 잠겨있어."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bag2), new string[] {"이사올 때 가지고 왔던 가방이야.", "자물쇠로 굳게 잠겨있어."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet1), new string[] {"많은 옷이 들어있는 옷장이야.", "별로 특별한 것은 없어 보여."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet2), new string[] {"많은 옷이 들어있는 옷장이다.", "별로 특별한 것은 없어 보여."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet3), new string[] {"많은 옷이 들어있는 옷장이야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet4), new string[] {"많은 옷이 들어있는 옷장이야.", "별로 특별한 것은 없어 보여."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet5), new string[] {"임시로 물건들을 넣어둔 장롱이야.", "쓸만한 물건은 없어."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Shirt_Closet), new string[] {"많은 옷이 걸려있는 옷장이야.", "나 셔츠 되게 많았네."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Book_Case1), new string[] {"많은 책들이 꽂혀있어.", "책을 읽고 싶은 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Book_Case2), new string[] {"많은 책들이 꽂혀있어.", "읽고 싶은 책이 없어."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Book_Case3), new string[] {"많은 책들이 꽂혀있어.", "잠이 오지 않을 때는 독서가 제격이다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Computer), new string[] {"오후에 공부할 때 사용했던 컴퓨터야.", "전원을 안껐었네."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.TV), new string[] {"TV가 꺼져있어. 보고 싶은 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Light1), new string[] {"특별할 것이 없는 조명이야.", "조명을 켤 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Light2), new string[] {"특별할 것이 없는 조명이야.", "조명을 켤 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Light3), new string[] {"특별할 것이 없는 조명이야.", "조명을 켤 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Light4), new string[] {"특별할 것이 없는 조명이야.", "조명을 켤 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair1), new string[] {"특별한 것이 없는 의자야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair2), new string[] {"특별한 것이 없는 의자야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair3), new string[] {"특별한 것이 없는 의자야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair4), new string[] {"특별한 것이 없는 의자야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair5), new string[] {"특별한 것이 없는 의자야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair6), new string[] {"특별한 것이 없는 의자야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair7), new string[] {"특별한 것이 없는 의자야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Table1), new string[] {"이사오면서 큰 맘먹고 산 식탁이야.", "아무리봐도 혼자 사용하기엔 너무 커."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Table2), new string[] {"새 책상을 사기 전에 사용하던 오래된 책상이야.", "내일은 꼭 버려야겠어."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Book_Table), new string[] {"책을 읽을 수 있는 책상이야.", "조명을 키고 책을 읽으면 분위기가 괜찮지."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Terrace), new string[] {"테라스로 나가 밖을 볼 기분이 아니야.", "모기도 물릴거고.."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Exit), new string[] {"집 밖으로 나가긴 싫어.", "소리의 원인을 찾아야 해."});


        // Quest Data
        m_talk_data.Add(10 + Convert.ToInt32(ObjectName.Computer), new string[] {"모니터를 들여다보니 인터넷이 켜져있어.", "검색창에 짧은 글이 적혀있어. 읽어볼까?", "\"썩은 고기를 처리하는 방법\"", "대체 누가 이걸 왜 검색한거지..?"});
    
        m_talk_data.Add(20 + Convert.ToInt32(ObjectName.Table2), new string[] {"오래된 책상의 서랍을 열었어.", "서랍에서 [니퍼]를 얻었어."});
        m_talk_data.Add(21 + Convert.ToInt32(ObjectName.Guitar1), new string[] {"기타의 줄을 자를 수 있을 것 같은데..", "기타의 줄을 자를까?", "기타의 줄을 자르고 기타의 몸통에 손을 넣었다.", "[여행가방 열쇠]를 획득했어."});

        m_talk_data.Add(30 + Convert.ToInt32(ObjectName.Bag1), new string[] {"언젠가 사용한 적이 있던 여행 가방이야.", "자물쇠로 잠겨 있지만 열쇠를 사용해서 열 수 있을 것 같아.", "열쇠를 사용할까?", "[여행가방 열쇠]를 사용했어.", "여행갈 때 챙겨두었던 셀카봉이 있네.", "[셀카봉]을 획득했어.", "가방 속에 손을 더 넣자 수첩을 발견했다.", "수첩을 넘기다보니 수상한 내용을 발견했다.", "\"여기에 보관하면 아무도 찾지 못할거야..\"", "\"옷들 사이에 있으니깐..\""});
        m_talk_data.Add(31 + Convert.ToInt32(ObjectName.Bed1), new string[] {"침대 밑을 살펴보니 반짝거리는 것이 보여.", "셀카봉을 펼치면 꺼낼 수 있을 것 같아.", "[셀카봉]을 사용할까?", "셀카봉 펼쳐서 반짝거리는 것을 꺼냈다.", "[스마트폰]을 획득했어."});
    
        m_talk_data.Add(40 + Convert.ToInt32(ObjectName.Bed4), new string[] {"조사를 하다보니 힘이 들어서 침대에 누웠다.", "스마트폰을 보면서 좀 쉴까?", "친구들한테 연락이 엄청 와 있네..", "\"ㅋㅋㅋ A 얘는 학교도 안나오냐?\"", "\"내가 볼 땐 고독사했다 ㅋㅋ\"", "\"야 전화받어 술이나 한잔 하자.\"", "생각해보니 스마트폰이 왜 침대 밑에 있는지 모르겠네."});

        m_talk_data.Add(50 + Convert.ToInt32(ObjectName.Book_Case1), new string[] {"책장을 자세히 살펴보니 신경쓰이는 책이 있어.", "이 책을 꺼낼까?", "[모든 순간이 너였다]를 획득했어.", "\'모든 순간이 너였다\'라.. 책을 읽을만한 곳을 찾아보자."});
        m_talk_data.Add(51 + Convert.ToInt32(ObjectName.Book_Table), new string[] {"이 곳이라면 책을 읽을 수 있을 것 같아.", "[모든 순간이 너였다]를 읽을까?", "책의 도입부에 연인끼리 지켜야 할 규칙이 적혀있다.", "\"1. 연인에게 사랑한다고 자주 말해주기\"", "\"2. 연인과 연락을 자주 하기\"", "\"3. 연인과의 미래를 믿어주기\"", "...", "으.. 더는 읽고 싶지 않아.", "OO이한테 선물받고 처음 읽어보네."});

        m_talk_data.Add(60 + Convert.ToInt32(ObjectName.Book_Case3), new string[] {"책장의 책들을 살펴볼까?", "어떤 책 사이에 끼어 있는 편지를 발견했어.", "2024/XX/XX\nA에게 OO가\n\"직접 말하면 너가 상처 받을 것 같아서 편지로 쓰게 되었어.\"", "\"나 사실.. 너가 나한테 하는 행동들이 좀 부담스러워.\n나한테 너무 집착하는 것 같아서 좀 질려.\"", "\"그래서 그런데 우리 헤어지자. 나 진짜 힘들어. 미안했고 잘 지내.\"", "...", "맞아.. OO가 헤어지자고 했었지."});

        m_talk_data.Add(70 + Convert.ToInt32(ObjectName.Bed4), new string[] {"머리가 아파서 좀 쉬고 싶어.", "침대에 누웠다.", "그리고 눈을 감고 있다가 나도 모르게 잠에 들었다."});
    
        m_talk_data.Add(80 + Convert.ToInt32(ObjectName.Chair3), new string[] {"의자 밑을 자세히 살펴보니 반짝이는 것이 있어.", "[반지]를 획득했어.", "이 반지.. 내 손에는 들어가지 않아.", "OO이껀가? 왜 우리집에 있지?", "돌려줘야겠다."});
    
        m_talk_data.Add(90 + Convert.ToInt32(ObjectName.TV), new string[] {"TV가 있는 선반을 살펴보니 반짝이는 것이 있어.", "[이사용 가방 열쇠]를 획득했어."});
        m_talk_data.Add(91 + Convert.ToInt32(ObjectName.Bag2), new string[] {"이사올 때 비상용으로 사용했던 가방이야.", "자물쇠로 굳게 잠겨있지만 열쇠를 사용하면 열 수 있을 것 같아.", "열쇠를 사용할까?", "[이사용 가방 열쇠]를 사용하여 이사용 가방을 열었어.", "가방 속에는 오래되어 보이는 CD가 있다.", "[오래된 CD]를 얻었어."});
        m_talk_data.Add(92 + Convert.ToInt32(ObjectName.TV), new string[] {"TV에 연결된 CD 플레이어에 CD를 넣었어.", "CD를 재생하니 예전 기억들이 떠오르네."});

        m_talk_data.Add(100 + Convert.ToInt32(ObjectName.Bed4), new string[] {"힘들어서 침대에서 쉬고 싶어졌어.", "침대에 누우니 어렴풋이 잊었던 기억이 떠오르네.", "OO이가 우리 집에 놀러왔었어.", "놀던 도중에 사소한 다툼이 있었었고...??", "그녀는 나에게 헤어지자고 했다.", "그리고 어떻게 되었더라..", "...", "그녀의 행방을 찾아보는게 좋겠어."});

        m_talk_data.Add(110 + Convert.ToInt32(ObjectName.Box1), new string[] {"이사용 박스가 있어. 이사용 박스를 조사할까?", "이사용 박스를 하나하나 옮기기 시작했다.", "옮기던 도중에 무거운 박스를 발견했다.", "무거운 박스를 열어볼까?", "...", "[피묻은 식칼]을 획득했어."});
    
        m_talk_data.Add(120 + Convert.ToInt32(ObjectName.Bed4), new string[] {"생각해보니 내가 OO를 죽였어.", "...", "내가 그녀를 죽였어.", "어쩌지?"});
    
        m_talk_data.Add(130 + Convert.ToInt32(ObjectName.Exit), new string[] {"뭔가 지금이라면 집 밖으로 나갈 수 있을 것 같은 기분이야.","집 밖으로 나설까?"});
        m_talk_data.Add(140 + Convert.ToInt32(ObjectName.Temp), new string[] {""});
    }

    public string GetDialogue(int id, int dialogue_idx)
    {
        Debug.Log("id: " + id);
        if(id == Convert.ToInt32(ObjectName.None))
            return null;
        
        if(!m_talk_data.ContainsKey(id))
        {
            if(id % 100 != 0)
                return GetDialogue(id - id % 100, dialogue_idx);
            else
                return GetDialogue(id - 100, dialogue_idx);
        }

        if(dialogue_idx == m_talk_data[id].Length)
            return null;
        else
            return m_talk_data[id][dialogue_idx];
    }
}
