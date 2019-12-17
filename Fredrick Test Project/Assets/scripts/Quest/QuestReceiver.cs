using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReceiver : MonoBehaviour
{
    [SerializeField]
    List<Quest> p_questList = new List<Quest>();

    [SerializeField]
    Quest p_curQuest;

    void Start()
    {
        /* 
         * TODO: Read from save data quest list
         * Read from save data into p_curQuest 
         */
    }

    public void AddQuest(Quest questToAdd)
    {
        if (!p_questList.Contains(questToAdd))
            p_questList.Add(questToAdd);
        else
            Debug.LogError("Cannot add the same quest twice!");
    }

    public void RemoveQuest(Quest questToRemove)
    {
        /* TODO: Have buffers to stop bad things */
        p_questList.Remove(questToRemove);
    }

    public void ClearQuests()
    {
        p_questList.Clear();
    }

    public Quest GetCurQuest()
    {
        return p_curQuest;
    }

    public void SetCurQuest(Quest activeQuest)
    {
        if(p_questList.Contains(activeQuest))
        {
            p_curQuest = activeQuest;
        }
        else
        {
            Debug.LogError("Cannot set active quest to a quest the entity does not have!");
        }
    }

    /*
     * Prints current quest to the console.
     */
    public void ViewCurrentQuest()
    {
        p_curQuest.Print();
    }

    /*
     * Prints all quests to the console.
     */
    public void ViewAllQuests()
    {
        foreach(Quest quest in p_questList)
        {
            quest.Print();
            Debug.Log('\n');
        }
    }
}
