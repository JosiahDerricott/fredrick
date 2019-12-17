using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    List<Quest> p_questsToGive = new List<Quest>();

    void Start() {}

    public void AddQuest(Quest questToAdd)
    {
        if (!p_questsToGive.Contains(questToAdd))
            p_questsToGive.Add(questToAdd);
        else
            Debug.LogError("Cannot add Quest that the entity already has!");
    }

    public void RemoveQuest(Quest questToRemove)
    {
        if (p_questsToGive.Contains(questToRemove))
            p_questsToGive.Remove(questToRemove);
        else
            Debug.LogError("Cannot remove a quest the entity doesn't have!");
    }

    public void ClearQuests()
    {
        p_questsToGive.Clear();
    }

    public void GiveQuest(ref QuestReceiver receiver)
    {
        // TODO: Add try catch that checks if the player already has the quests
        if(p_questsToGive.Count > 0)
        {
            receiver.AddQuest(p_questsToGive[0]);

            // Maybe have timed quests at some point?
            p_questsToGive.RemoveAt(0);
        }
    }
}
