using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStep
{
    public int questStepID;

    public string questStepTitle;
    public string questStepObjective;

    public ResourceValue[] resourceValues;
    public ReputationValue[] reputationValues;

    public QuestStepStatus currentQuestStepStatus = QuestStepStatus.NOT_REACHED;

    public void CompleteQuestStep()
    {
        currentQuestStepStatus = QuestStepStatus.COMPLETED;

        try
        {
            if (resourceValues != null && resourceValues.Length > 0)
            {
                for (int i = 0; i < resourceValues.Length; i++)
                {
                    switch (resourceValues[i].resource)
                    {
                        case Resource.FOOD:
                            ResourceManager.AddFood(resourceValues[i].value);
                            break;
                        case Resource.WOOD:
                            ResourceManager.AddWood(resourceValues[i].value);
                            break;
                        case Resource.METAL:
                            ResourceManager.AddMetal(resourceValues[i].value);
                            break;
                        case Resource.NONE:
                            break;
                    }
                }
            }

            if (reputationValues != null && reputationValues.Length > 0)
            {
                for (int i = 0; i < reputationValues.Length; i++)
                {
                    switch (reputationValues[i].reputation)
                    {
                        case Reputation.CULTURE:
                            ReputationManager.AddCulture(reputationValues[i].value);
                            break;
                        case Reputation.NATURE:
                            ReputationManager.AddNature(reputationValues[i].value);
                            break;
                        case Reputation.SOCIETY:
                            ReputationManager.AddSociety(reputationValues[i].value);
                            break;
                        case Reputation.NONE:
                            break;
                    }
                }
            }
        }
        catch
        {
            throw new System.Exception("Failed to add resources.");
        }
    }
}
