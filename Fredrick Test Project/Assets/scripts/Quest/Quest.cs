using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour
{
    [SerializeField]
    string p_name, p_desc;

    // Queue will be more efficient later
    [SerializeField]
    List<QuestStep> p_currentQuestSteps = new List<QuestStep>();

    // Queue will be more efficient later
    [SerializeField]
    List<QuestStep> p_completedQuestSteps = new List<QuestStep>();

    bool p_isComplete;

    void Start()
    {
        /* Once we implement saving, this is field 
         * that will be serialized and retrieved */
        p_isComplete = false;
    }

    public string GetName()
    {
        return p_name;
    }

    public string GetDesc()
    {
        return p_desc;
    }

    public bool GetIsComplete()
    {
        return p_isComplete;
    }

    public List<QuestStep> GetCurrentSteps()
    {
        return p_currentQuestSteps;
    }

    public List<QuestStep> GetCompleteSteps()
    {
        return p_completedQuestSteps;
    }

    public void SetName(string newName)
    {
        p_name = newName;
    }

    public void SetDesc(string newDesc)
    {
        p_desc = newDesc;
    }

    public void SetIsComplete(bool flag)
    {
        p_isComplete = flag;
    }

    /*
     * 
     *  Returns true if step could be completed.
     *  Returns false if no steps were left to complete.
     * 
     */
    public bool CompleteStep()
    {
        if (p_currentQuestSteps.Count > 0)
        {
            // Create temp of step to be completed
            QuestStep temp = p_currentQuestSteps[0];

            // Remove step
            p_currentQuestSteps.RemoveAt(0);

            // Set as completed
            p_completedQuestSteps.Add(temp);

            return true;
        }

        return false;
    }

    public void Print()
    {
        Debug.Log("Quest Name: " + GetName());
        Debug.Log("Quest Desc: " + GetDesc());

        if (p_currentQuestSteps != null && p_currentQuestSteps.Count > 0)
            Debug.Log("Current Step: " + p_currentQuestSteps[0].GetStepDesc());
        else
            Debug.Log("All steps complete, should not be current!");
    }
}

[System.Serializable]
public class QuestStep
{
    [SerializeField]
    int _stepNumber;

    [SerializeField]
    string _stepDesc;

    List<QuestCondition> _conditions;

    public int GetStepNumber()
    {
        return _stepNumber;
    }

    public string GetStepDesc()
    {
        return _stepDesc;
    }

    public List<QuestCondition> GetCondtions()
    {
        return _conditions;
    }
}
