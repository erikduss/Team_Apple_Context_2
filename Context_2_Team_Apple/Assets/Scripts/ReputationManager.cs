using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ReputationManager
{
    public static int culture = 0;
    public static int nature = 0;
    public static int society = 0;

    public static void AddCulture(int amount)
    {
        culture += amount;
    }

    public static void RemoveCulture(int amount)
    {
        culture -= amount;
    }

    public static void AddNature(int amount)
    {
        nature += amount;
    }

    public static void RemoveNature(int amount)
    {
        nature -= amount;
    }

    public static void AddSociety(int amount)
    {
        society += amount;
    }

    public static void RemoveSociety(int amount)
    {
        society -= amount;
    }
}
