using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ResourceManager
{
    public static int wood = 50;
    public static int metal = 50;
    public static int food = 50;

    public static void AddWood(int amount)
    {
        wood += amount;
    }

    public static void TakeWood(int amount)
    {
        wood -= amount;
    }

    public static void TakeMetal(int amount)
    {
        metal -= amount;
    }

    public static void TakeFood(int amount)
    {
        food -= amount;
    }

    public static void AddMetal(int amount)
    {
        metal += amount;
    }

    public static void AddFood(int amount)
    {
        food += amount;
    }
}
