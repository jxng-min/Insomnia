using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _Singleton;
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
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bed1), new string[] {"잠들고 싶은 기분이 아니다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bed2), new string[] {"잠들고 싶은 기분이 아니다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bed3), new string[] {"잠들고 싶은 기분이 아니다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bed4), new string[] {"잠들고 싶은 기분이 아니다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Guitar1), new string[] {"집에 이사오면서 구매한 한정판 어쿠스틱 기타다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Guitar2), new string[] {"예전에 큰 맘 먹고 샀던 일렉 기타다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Guitar3), new string[] {"예전에 큰 맘 먹고 샀던 일렉 기타다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Guitar4), new string[] {"예전에 큰 맘 먹고 샀던 일렉 기타다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Emp1), new string[] {"기타에 연결해서 사용하는 앰프다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Emp2), new string[] {"기타에 연결해서 사용하는 앰프다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box1), new string[] {"이사를 하고 나서 정리하지 못한 짐들이다.", "빠르게 정리해야 할텐데."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box2), new string[] {"이사를 하고 나서 정리하지 못한 짐들이다.", "빠르게 정리해야 할텐데."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box3), new string[] {"이사를 하고 나서 정리하지 못한 짐들이다.", "빠르게 정리해야 할텐데."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box4), new string[] {"이사를 하고 나서 정리하지 못한 짐들이다.", "빠르게 정리해야 할텐데."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Box5), new string[] {"이사를 하고 나서 정리하지 못한 짐들이다.", "빠르게 정리해야 할텐데."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bag1), new string[] {"언젠가 사용한 적이 있던 여행 가방이다.", "자물쇠로 굳게 잠겨있다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Bag2), new string[] {"이사올 때 가지고 왔던 가방이다.", "자물쇠로 굳게 잠겨있다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet1), new string[] {"많은 옷이 들어있는 옷장이다.", "별로 특별한 것은 없어 보인다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet2), new string[] {"많은 옷이 들어있는 옷장이다.", "별로 특별한 것은 없어 보인다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet3), new string[] {"많은 옷이 들어있는 옷장이다.", "별로 특별한 것은 없어 보인다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet4), new string[] {"많은 옷이 들어있는 옷장이다.", "별로 특별한 것은 없어 보인다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Closet5), new string[] {"임시로 물건들을 넣어둔 장롱이다.", "크게 특별한 것은 없다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Shirt_Closet), new string[] {"많은 옷이 걸려있는 옷장이다.", "내부에 셔츠들이 들어있는 것을 제외하고는 특별할 것이 없다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Book_Case1), new string[] {"많은 책들이 꽂혀있다.", "잠이 오지 않을 때는 독서가 제격이다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Book_Case2), new string[] {"많은 책들이 꽂혀있다.", "잠이 오지 않을 때는 독서가 제격이다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Book_Case3), new string[] {"많은 책들이 꽂혀있다.", "잠이 오지 않을 때는 독서가 제격이다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Computer), new string[] {"오후에 작업할 때 사용했던 컴퓨터다.", "본체 전원을 끄는 것을 잊었었다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.TV), new string[] {"TV가 꺼져있다. 보고 싶은 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Light1), new string[] {"특별할 것이 없는 조명이다.", "조명을 켤 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Light2), new string[] {"특별할 것이 없는 조명이다.", "조명을 켤 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Light3), new string[] {"특별할 것이 없는 조명이다.", "조명을 켤 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Light4), new string[] {"특별할 것이 없는 조명이다.", "조명을 켤 기분이 아니야."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair1), new string[] {"특별한 것이 없는 의자다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair2), new string[] {"특별한 것이 없는 의자다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair3), new string[] {"특별한 것이 없는 의자다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair4), new string[] {"특별한 것이 없는 의자다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair5), new string[] {"특별한 것이 없는 의자다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair6), new string[] {"특별한 것이 없는 의자다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Chair7), new string[] {"특별한 것이 없는 의자다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Table1), new string[] {"이사오면서 큰 맘먹고 산 식탁이다.", "혼자 살면서 쓰기엔 좀 부담스럽다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Table2), new string[] {"새 책상을 사기 전에 사용하던 오래된 책상이다.", "내일은 꼭 버려야겠다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Book_Table), new string[] {"책을 읽을 수 있는 책상이다.", "책상 위에 조명이 있어 조명을 키고 보면 분위기가 제법 산다."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Terrace), new string[] {"테라스로 나가 밖을 볼 기분이 아니야.", "모기도 물릴거고.."});
        m_talk_data.Add(Convert.ToInt32(ObjectName.Exit), new string[] {"집 밖으로 나가긴 싫어."});


        // Quest Data
        m_talk_data.Add(10 + Convert.ToInt32(ObjectName.Computer), new string[] {"모니터를 들여다보니 메모장이 켜져있다.", "메모장에는 짧은 글이 적혀있다.", "\"나도 모르겠어. 어쩔 수 없던거야.\"", "\"너가 나한테 그런 말을 해서는 안됐어.\"", "대체 누가 이 글을 쓴거지.."});
    
        m_talk_data.Add(20 + Convert.ToInt32(ObjectName.Table2), new string[] {"오래된 책상의 서랍을 열었다.", "[니퍼]를 얻었다."});
        m_talk_data.Add(21 + Convert.ToInt32(ObjectName.Guitar1), new string[] {"기타의 줄을 자를 수 있을 것 같다.", "기타의 줄을 자를까?", "기타의 줄을 자르고 기타의 몸통에 손을 넣었다.", "[여행가방 열쇠]를 획득했다."});

        m_talk_data.Add(30 + Convert.ToInt32(ObjectName.Bag1), new string[] {"언젠가 사용한 적이 있던 여행 가방이다.", "자물쇠로 잠겨 있지만 열쇠를 사용해서 열 수 있을 것 같다.", "열쇠를 사용할까?", "[여행가방 열쇠]를 사용했다.", "가방 내부에는 신변 보호를 위해 여행갈 때 챙겨두었던 삼단봉이 있었다.", "[삼단봉]을 획득했다.", "가방 속에 손을 더 넣자 수첩을 발견했다.", "수첩을 넘기다보니 수상한 내용을 발견했다.", "\"여기에 보관하면 아무도 찾지 못할거야..\"", "\"옷들 사이에 있으니깐..\""});
        m_talk_data.Add(31 + Convert.ToInt32(ObjectName.Bed1), new string[] {"침대 밑을 살펴보니 반짝거리는 것이 보인다.", "삼단봉을 펼치면 꺼낼 수 있을 것 같다.", "삼단봉을 사용할까?", "삼단봉을 펼쳐서 반짝거리는 것을 찾았다.", "[스마트폰]을 획득했다."});
    
        m_talk_data.Add(40 + Convert.ToInt32(ObjectName.Bed4), new string[] {"탐색을 하다보니 힘이 들어서 침대에 누웠다.", "스마트폰을 보니 친구들한테서 연락이 와 있었다.", "\"너 무슨 일 있어?\"", "\"왜 며칠동안 연락도 안보고 학교도 안나와..??\"", "생각해보니 학교를 가야 했지만 갈 힘이 남아있지 않았다.", "어느 순간 눈물이 나와 스마트폰을 내려놓았다."});

        m_talk_data.Add(50 + Convert.ToInt32(ObjectName.Book_Case1), new string[] {"책장을 자세히 살펴보니 신경쓰이는 책이 있다.", "이 책을 꺼낼까?", "[사랑의 비법서]를 획득했다.", "\'사랑의 비법서\'라.. 책을 읽을만한 곳을 찾아보자."});
        m_talk_data.Add(51 + Convert.ToInt32(ObjectName.Book_Table), new string[] {"이 곳이라면 책을 읽을 수 있을 것 같다.", "[사랑의 비법서]를 읽을까?", "책의 도입부가 눈에 보인다.", "\"1. 연인에게 사랑한다고 자주 말해주기\"", "\"2. 연인과 연락을 자주 하기\"", "\"3. 연인과의 미래를 믿어주기\"", "...", "더는 읽고 싶지 않아..", "책을 덮었다."});

        m_talk_data.Add(60 + Convert.ToInt32(ObjectName.Book_Case3), new string[] {"책장의 책들을 살펴볼까?", "어떤 책 사이에 끼어 있는 편지를 발견했다.", "2024/XX/XX\nOO에게 OO가\n\"직접 말하면 너가 상처 받을 것 같아서 편지로 쓰게 되었어.\"", "\"나 사실.. 너가 나한테 하는 행동들이 좀 부담스러워.\n몇번이나 말했지만 바뀌지 않는 너를 보면 좀 지쳐.\"", "\"그래서 그런데 우리 헤어지자. 나 진짜 힘들어. 미안하고 잘 지내.\"", "...", "편지를 찢었다."});

        m_talk_data.Add(70 + Convert.ToInt32(ObjectName.Bed4), new string[] {"뭔가 기분이 좋지 않다.", "침대에 누웠다.", "그리고 눈을 감고 있다가 나도 모르게 잠에 들었다."});
    
        m_talk_data.Add(80 + Convert.ToInt32(ObjectName.Chair3), new string[] {"의자 밑을 자세히 살펴보니 반짝이는 것이 있다.", "[반지]를 획득했다.", "이 반지.. 내 손에는 들어가지 않아."});
    
        m_talk_data.Add(90 + Convert.ToInt32(ObjectName.TV), new string[] {"TV가 있는 선반을 살펴보니 반짝이는 것이 있다.", "[이사용 가방 열쇠]를 획득했다."});
        m_talk_data.Add(91 + Convert.ToInt32(ObjectName.Bag2), new string[] {"이사올 때 비상용으로 사용했던 가방이다.", "자물쇠로 굳게 잠겨있지만 열쇠를 사용하면 열 수 있을 것 같다.", "열쇠를 사용할까?", "[이사용 가방 열쇠]를 사용하여 이사용 가방을 열었다.", "가방 속에는 피가 묻은 수건과 식칼, 오래되어 보이는 CD가 있다.", "[오래된 CD]를 얻었다."});
        m_talk_data.Add(92 + Convert.ToInt32(ObjectName.TV), new string[] {"TV에 연결된 CD 플레이어에 CD를 넣었다.", "CD를 재생하니 예전 기억들이 떠올랐다."});

        m_talk_data.Add(100 + Convert.ToInt32(ObjectName.Bed4), new string[] {"힘들어서 침대에서 쉬고 싶어졌다.", "침대에 누우니 어렴풋이 잊었던 기억이 떠올랐다.", "나에게는 소중한 여자친구가 있었다.", "나와 그녀는 다툼이 있었고, 그게 좀 크게 번졌다.", "그녀는 나에게 이별을 말했고 나는 납득할 수 없었다.", "그래서 난 그녀를 불렀다.", "그리고 어떻게 되었더라..", "...", "그녀의 행방을 찾아보는게 좋겠어."});

        m_talk_data.Add(110 + Convert.ToInt32(ObjectName.Box1), new string[] {"이사용 박스가 있다. 이사용 박스를 조사할까?", "이사용 박스를 하나하나 옮기기 시작했다.", "옮기던 도중에 무거운 박스가 있다.", "무거운 박스를 열어볼까?", "....", "[여자친구의 머리]를 획득했다.", "박스를 다시 덮었다."});
    
        m_talk_data.Add(120 + Convert.ToInt32(ObjectName.Bed4), new string[] {"토막난 여자친구의 시신을 보고나니 속이 안좋아졌다.", "...", "내가 그녀를 죽였었다.", "난 이제 어떻게 해야 할까?"});
    
        m_talk_data.Add(130 + Convert.ToInt32(ObjectName.Exit), new string[] {"뭔가 지금이라면 집 밖으로 나갈 수 있을 것 같은 기분이다.","집 밖으로 나설까?"});
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
