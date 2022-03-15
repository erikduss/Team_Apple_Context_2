using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStep
{
    public int questStepID;

    public string questStepTitle;
    public string questStepObjective;

    public QuestStepStatus currentQuestStepStatus = QuestStepStatus.NOT_REACHED;

    public void CompleteQuestStep()
    {
        currentQuestStepStatus = QuestStepStatus.COMPLETED;
    }
}
