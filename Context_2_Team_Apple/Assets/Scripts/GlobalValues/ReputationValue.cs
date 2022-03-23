using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationValue
{
    public Reputation reputation;
    public int value;

    public ReputationValue(Reputation _reputation, int _value)
    {
        reputation = _reputation;
        value = _value;
    }
}
