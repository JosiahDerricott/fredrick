using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReceiver : MonoBehaviour
{
    List<Quest> p_questList = new List<Quest>();
    Quest p_curQuest;

    void Start()
    {
        /* TODO: Read from save data quest list
         * Read from save data into p_curQuest 
         */
    }

    void Update()
    {

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
