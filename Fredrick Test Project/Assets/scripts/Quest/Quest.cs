using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour
{
    [System.Serializable]
    public struct QuestStep
    {
        public int _stepNumber;
        public string _stepDesc;
    }

    [SerializeField]
    string _name, _desc;

    // Queue will be more efficient later
    [SerializeField]
    List<QuestStep> p_currentQuestSteps;

    // Queue will be more efficient later
    [SerializeField]
    List<QuestStep> p_completedQuestSteps;

    bool p_isComplete;

    void Start()
    {
        /* Once we implement saving, this is field 
         * that will be serialized and retrieved */
        p_isComplete = false;
        p_currentQuestSteps = new List<QuestStep>();
        p_completedQuestSteps = new List<QuestStep>();
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDesc()
    {
        return _desc;
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
        _name = newName;
    }

    public void SetDesc(string newDesc)
    {
        _desc = newDesc;
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
            Debug.Log("Current Step: " + p_currentQuestSteps[0]);
        else
            Debug.Log("All steps complete, should not be current!");
    }
}
