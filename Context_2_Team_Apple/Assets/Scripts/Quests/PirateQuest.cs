using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateQuest : IQuest
{
    /*
     * These variables do not need to be changed.
     * The ID will be automatically generated.
     * The Quest Status will automatically update and will always start on "not accepted".
     * The amount of steps will be loaded from the quest step list on load.
     */

    //The name of this quest
    private string _questname = "PirateQuest";

    public int questID { get; set; }

    public string questName { get => _questname; set => _questname = value; }

    public QuestStatus questStatus { get; set; }

    public int currentQuestStep { get; set; }

    public int amountOfQuestSteps { get; set; }

    public List<QuestStep> questSteps { get; set; }

    public PirateQuest()
    {
        amountOfQuestSteps = 0;
        currentQuestStep = 0;
        questSteps = new List<QuestStep>();

        //ADD QUEST STEPS HERE.

        //EXAMPLE:
        //First String = Title of the quest step (This will show as title for any dialog).
        //Second String = Dialog text for the quest step.
        AddQuestStep("Pirate", "Hey! These runts think they should have the mainland to themselves. What makes them so high and mighty. Them living is an insult to society. We need you to help us get rid of these pests.", null);
        AddQuestStep("...", "Being the natural pirate you are, you shoot the cannons even faster than the pirates could.", new QuestStepReward(0, 0, 0, -20, 40, -20));
        AddQuestStep("Pirate", "Wow! You're a natural, thanks for the help.", new QuestStepReward(100, 100, 100, 0, 0, 0));
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
