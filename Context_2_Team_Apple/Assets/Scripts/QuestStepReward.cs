using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStepReward
{
    public ResourceValue[] resourceValues = new ResourceValue[3];
    public ReputationValue[] reputationValues = new ReputationValue[3];

    public QuestStepReward(int Wood, int Food, int Metal, int Culture, int Nature, int Society)
    {
        resourceValues[0] = new ResourceValue(Resource.WOOD, Wood);
        resourceValues[1] = new ResourceValue(Resource.FOOD, Food);
        resourceValues[2] = new ResourceValue(Resource.METAL, Metal);

        reputationValues[0] = new ReputationValue(Reputation.CULTURE, Culture);
        reputationValues[1] = new ReputationValue(Reputation.NATURE, Nature);
        reputationValues[2] = new ReputationValue(Reputation.SOCIETY, Society);
    }
}
