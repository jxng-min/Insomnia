using System;
using System.Collections;
using System.Collections.Generic;
using _Singleton;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    Dictionary<int, QuestData> m_quest_list;

    private void Start()
    {
        m_quest_list = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()
    {
        m_quest_list.Add(10, new QuestData("퀘스트1", new int[] {Convert.ToInt32(ObjectName.Computer)}));
        m_quest_list.Add(20, new QuestData("퀘스트2", new int[] {Convert.ToInt32(ObjectName.Table2), Convert.ToInt32(ObjectName.Guitar1)}));
        m_quest_list.Add(30, new QuestData("퀘스트3", new int[] {Convert.ToInt32(ObjectName.Bag1), Convert.ToInt32(ObjectName.Bed1)}));
        m_quest_list.Add(40, new QuestData("퀘스트4", new int[] {Convert.ToInt32(ObjectName.Bed4)}));
        m_quest_list.Add(50, new QuestData("퀘스트5", new int[] {Convert.ToInt32(ObjectName.Book_Case1), Convert.ToInt32(ObjectName.Book_Table)}));
        m_quest_list.Add(60, new QuestData("퀘스트6", new int[] {Convert.ToInt32(ObjectName.Book_Case3)}));
        m_quest_list.Add(70, new QuestData("퀘스트7", new int[] {Convert.ToInt32(ObjectName.Bed4)}));
        m_quest_list.Add(80, new QuestData("퀘스트8", new int[] {Convert.ToInt32(ObjectName.Chair3)}));
        m_quest_list.Add(90, new QuestData("퀘스트9", new int[] {Convert.ToInt32(ObjectName.TV), Convert.ToInt32(ObjectName.Bag2), Convert.ToInt32(ObjectName.TV)}));
        m_quest_list.Add(100, new QuestData("퀘스트10", new int[] {Convert.ToInt32(ObjectName.Bed4)}));
        m_quest_list.Add(110, new QuestData("퀘스트11", new int[] {Convert.ToInt32(ObjectName.Box1)}));
        m_quest_list.Add(120, new QuestData("퀘스트12", new int[] {Convert.ToInt32(ObjectName.Bed4)}));
        m_quest_list.Add(130, new QuestData("퀘스트13", new int[] {Convert.ToInt32(ObjectName.Exit)}));
        m_quest_list.Add(140, new QuestData("종료", new int[] {Convert.ToInt32(ObjectName.Temp)}));
    }

    public int GetQuestDialogueIndex(int object_id)
    {
        return DataManager.Instance.m_now_player.m_quest_id + DataManager.Instance.m_now_player.m_quest_action_id;
    }

    public string CheckQuest(int id)
    {
        if(id == m_quest_list[DataManager.Instance.m_now_player.m_quest_id].m_npc_id[DataManager.Instance.m_now_player.m_quest_action_id])
            DataManager.Instance.m_now_player.m_quest_action_id++;

        ControlObject();

        if(DataManager.Instance.m_now_player.m_quest_action_id == m_quest_list[DataManager.Instance.m_now_player.m_quest_id].m_npc_id.Length)
            NextQuest();

        return m_quest_list[DataManager.Instance.m_now_player.m_quest_id].m_quest_name;
    }

    private void NextQuest()
    {
        DataManager.Instance.m_now_player.m_quest_id += 10;
        DataManager.Instance.m_now_player.m_quest_action_id = 0;
    }

    private void ControlObject()
    {
        switch(DataManager.Instance.m_now_player.m_quest_id)
        {
        case 20:
            if(DataManager.Instance.m_now_player.m_quest_action_id == 1)
                DataManager.Instance.m_now_player.m_items.Add("니퍼");
            else if(DataManager.Instance.m_now_player.m_quest_action_id == 2)
                DataManager.Instance.m_now_player.m_items.Add("여행가방 열쇠");
            break;
        
        case 30:
            if(DataManager.Instance.m_now_player.m_quest_action_id == 1)
                DataManager.Instance.m_now_player.m_items.Add("삼단봉");
            else if(DataManager.Instance.m_now_player.m_quest_action_id == 2)
                DataManager.Instance.m_now_player.m_items.Add("스마트폰");
            break;

        case 50:
            if(DataManager.Instance.m_now_player.m_quest_action_id == 1)
                DataManager.Instance.m_now_player.m_items.Add("사랑의 비법서");
            break;

        case 80:
            if(DataManager.Instance.m_now_player.m_quest_action_id == 1)
                DataManager.Instance.m_now_player.m_items.Add("반지");
            break;

        case 90:
            if(DataManager.Instance.m_now_player.m_quest_action_id == 1) 
                DataManager.Instance.m_now_player.m_items.Add("이사용 가방 열쇠");
            else if(DataManager.Instance.m_now_player.m_quest_action_id == 2)
                DataManager.Instance.m_now_player.m_items.Add("오래된 CD");
            break;

        case 110:
            if(DataManager.Instance.m_now_player.m_quest_action_id == 1)
                DataManager.Instance.m_now_player.m_items.Add("여자친구의 머리");
            break;
        }
    }
}