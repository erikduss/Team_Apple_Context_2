using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest
{
    public int questID;

    public string questName;

    public QuestStatus questStatus;

    public int currentQuestStep;

    public int amountOfQuestSteps;

    public List<QuestStep> questSteps = new List<QuestStep>();

    public void QuestCompleted()
    {
        //The quest is complete
    }
}
