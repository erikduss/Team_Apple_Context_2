using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour
{
     
    public static ResourceManager Instance {get; private set; }
    private int wood = 50;
    private int metal = 50;
    private int food = 50;

    [SerializeField] int baseAmount;
    [SerializeField] int amountToAdd;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("wood" + wood);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }

    public void AddWood(int amount)
    {
        wood += amount;
    }

    public void TakeWood(int amount)
    {
        wood -= amount;
    }

    public void TakeMetal(int amount)
    {
        metal -= amount;
    }

    public void TakeFood(int amount)
    {
        food -= amount;
    }

    public void AddMetal(int amount)
    {
        metal += amount;
    }

    public void AddFood(int amount)
    {
        food += amount;
    }
}
