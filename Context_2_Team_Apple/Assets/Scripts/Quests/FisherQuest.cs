using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherQuest : IQuest
{
    /*
     * These variables do not need to be changed.
     * The ID will be automatically generated.
     * The Quest Status will automatically update and will always start on "not accepted".
     * The amount of steps will be loaded from the quest step list on load.
     */

    //The name of this quest
    private string _questname = "FisherQuest";

    public int questID { get; set; }

    public string questName { get => _questname; set => _questname = value; }

    public QuestStatus questStatus { get; set; }

    public int currentQuestStep { get; set; }

    public int amountOfQuestSteps { get; set; }

    public List<QuestStep> questSteps { get; set; }

    public FisherQuest()
    {
        amountOfQuestSteps = 0;
        currentQuestStep = 0;
        questSteps = new List<QuestStep>();

        //ADD QUEST STEPS HERE.

        //EXAMPLE:
        //First String = Title of the quest step (This will show as title for any dialog).
        //Second String = Dialog text for the quest step.
        AddQuestStep("Fisherman", "Hey, friend. There are pirates about that destroyed some of our fishing ships. Would you like to help us make new ones?", null);
        AddQuestStep("...", "You decide to help the fisher. ", new QuestStepReward(100, -30, 0, 10, -30, 20));
        AddQuestStep("Fisherman", "Thanks for helping!", new QuestStepReward(-100, 100, 0, 0, 0, 0));
    }

    //This function will automatically get called when a quest step has been completed.
    public void CheckForNextSteps()
    {
        if(currentQuestStep < amountOfQuestSteps-1)
        {
            if(questSteps[currentQuestStep].currentQuestStepStatus == QuestStepStatus.COMPLETED)
            {
                currentQuestStep++;

                questSteps[currentQuestStep].currentQuestStepStatus = QuestStepStatus.ONGOING;
            }
        }
        else
        {
            QuestCompleted();
        }
    }

    private void AddQuestStep(string _questStepTitle, string _questStepObjective, QuestStepReward reward)
    {
        QuestStep tempStep = new QuestStep();

        tempStep.questStepID = amountOfQuestSteps;

        tempStep.questStepTitle = _questStepTitle;
        tempStep.questStepObjective = _questStepObjective;

        if(reward != null)
        {
            tempStep.resourceValues = reward.resourceValues;
            tempStep.reputationValues = reward.reputationValues;
        }

        tempStep.currentQuestStepStatus = QuestStepStatus.NOT_REACHED;

        questSteps.Add(tempStep);
        amountOfQuestSteps++;
    }

    public void QuestCompleted()
    {
        questStatus = QuestStatus.COMPLETED;
        Debug.Log("Quest is complete");
    }

    public string GetCurrentTitle()
    {
        string title = questSteps[currentQuestStep].questStepTitle;
        return title;
    }

    public string GetCurrentDialog()
    {
        string title = questSteps[currentQuestStep].questStepObjective;
        return title;
    }
}
