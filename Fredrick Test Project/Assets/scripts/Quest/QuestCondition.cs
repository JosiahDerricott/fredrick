using UnityEngine;

[System.Serializable]
public class QuestCondition
{
    [SerializeField]
    string p_conditionDesc;

    [SerializeField]
    bool p_isCompleted = false;

    public string GetConditionDesc()
    {
        return p_conditionDesc;
    }

    public bool GetIsComplete()
    {
        return p_isCompleted;
    }

    public void SetConditionDesc(string newDesc)
    {
        p_conditionDesc = newDesc;
    }

    public void SetIsComplete(bool flag)
    {
        p_isCompleted = flag;
    }
}
