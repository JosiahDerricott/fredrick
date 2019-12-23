using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField]
    Quest p_questRef;

    [SerializeField]
    bool p_isConditionTrigger = false; // If not true, does not run OnTriggerEnter function

    // Only needed if isConditionTrigger is true
    QuestCondition p_questConRef;

    public void CompleteCondition(QuestReceiver entity)
    {
        // Check if quest is active and condition can be met
        if(entity.GetQuestList().Contains(p_questRef))
        {
            var questStep = entity.GetQuestList().Find(q => q == p_questRef).GetCurrentSteps()[0].GetCondtions();

            // Make sure step is active
            if(questStep.Contains(p_questConRef))
            {
                // Complete condition

            }
            else
            {
                Debug.LogWarning("Entity is not at the step to complete this condition!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only continue if entity has questreceiver
        if(other.gameObject.GetComponent<QuestReceiver>() != null && p_isConditionTrigger)
        {
            CompleteCondition(other.gameObject.GetComponent<QuestReceiver>());
        }
    }
}
