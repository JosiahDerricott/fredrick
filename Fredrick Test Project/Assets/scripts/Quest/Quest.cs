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

    public string _name, _desc;

    // Queue will be more efficient later
    public List<QuestStep> _currentQuestSteps = new List<QuestStep>();

    // Queue will be more efficient later
    public List<QuestStep> _completedQuestSteps = new List<QuestStep>();

    bool isComplete;

    void Start()
    {
        /* Once we implement saving, this is field 
         * that will be serialized and retrieved */
        isComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        return isComplete;
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
        isComplete = flag;
    }

    /*
     * 
     *  Returns true if step could be completed.
     *  Returns false if no steps were left to complete.
     * 
     */
    public bool CompleteStep()
    {
        if (_currentQuestSteps.Count > 0)
        {
            // Create temp of step to be completed
            QuestStep temp = _currentQuestSteps[0];

            // Remove step
            _currentQuestSteps.RemoveAt(0);

            // Set as completed
            _completedQuestSteps.Add(temp);

            return true;
        }

        return false;
    }
}
