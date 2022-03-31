using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TraversableLocation currentlyActiveLocation = TraversableLocation.LOCATION_1;
    [SerializeField] private List<GameObject> locations = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI metalAmountText;
    [SerializeField] private TextMeshProUGUI foodAmountText;
    [SerializeField] private TextMeshProUGUI woodAmountText;

    [SerializeField] private TextMeshProUGUI yearText;

    [SerializeField] private GameObject pirateQuestObject;
    [SerializeField] private GameObject diverQuestObject;
    [SerializeField] private GameObject fishermanQuestObject;
    [SerializeField] private GameObject explorerQuestObject;
    [SerializeField] private GameObject crowQuestObject;
    [SerializeField] private GameObject hermitQuestObject;
    [SerializeField] private GameObject deerQuestObject;

    private int lastKnownFoodAmount = 0;
    private int lastKnownWoodAmount = 0;
    private int lastKnownMetalAmount = 0;

    private int amountOfQuestsCompleted = 0;

    private QuestManager questmanager;

    // Start is called before the first frame update
    void Start()
    {
        //ChangeLocation(TraversableLocation.LOCATION_1);
        questmanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();
        ResourceAmountUpdateChecker();
    }

    // Update is called once per frame
    void Update()
    {
        ResourceAmountUpdateChecker();
    }

    private void ResourceAmountUpdateChecker()
    {
        if(lastKnownWoodAmount != ResourceManager.wood || lastKnownMetalAmount != ResourceManager.metal || lastKnownFoodAmount != ResourceManager.food)
        {
            metalAmountText.text = ResourceManager.metal.ToString();
            foodAmountText.text = ResourceManager.food.ToString();
            woodAmountText.text = ResourceManager.wood.ToString();

            lastKnownFoodAmount = ResourceManager.food;
            lastKnownMetalAmount = ResourceManager.metal;
            lastKnownWoodAmount = ResourceManager.wood;
        }
    }

    //Execute this function in the Quest Manager after checking for the next step if the quest is complete.
    public void UpdateYear()
    {
        ChangeWorldObjects();
        yearText.text = amountOfQuestsCompleted.ToString();
    }

    private void ChangeWorldObjects()
    {
        amountOfQuestsCompleted = 0;

        foreach (IQuest quest in questmanager.allAvailableQuests)
        {
            if(quest.questStatus == QuestStatus.COMPLETED)
            {
                amountOfQuestsCompleted++;

                switch (quest.questID)
                {
                    case 0: //EXAMPLE PLACEHOLDER
                        
                        break;
                    case 1: //DIVER QUEST
                        EnableGameObject(diverQuestObject);
                        break;
                    case 2: //PIRATE QUEST
                        EnableGameObject(pirateQuestObject);
                        break;
                    case 3: //EXPLORER QUEST
                        EnableGameObject(explorerQuestObject);
                        break;
                    case 4: //FISHER QUEST
                        EnableGameObject(fishermanQuestObject);
                        break;
                    case 5:
                        break;
                    case 6: //DEER QUEST
                        EnableGameObject(deerQuestObject);
                        break;
                    case 7: //CROWS QUEST
                        EnableGameObject(crowQuestObject);
                        break;
                    case 8: //HERMIT QUEST
                        EnableGameObject(hermitQuestObject);
                        break;
                    case 9: //NOMAD QUEST
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void EnableGameObject(GameObject item)
    {
        if (!item.activeInHierarchy)
        {
            item.SetActive(true);
        }
    }

    private void DisableGameObject(GameObject item)
    {
        if (item.activeInHierarchy)
        {
            item.SetActive(false);
        }
    }

    public void ChangeLocation(TraversableLocation newLocation)
    {
        foreach(GameObject item in locations)
        {
            item.SetActive(false);
        }

        switch (newLocation)
        {
            case TraversableLocation.LOCATION_1:
                locations[0].SetActive(true);
                break;
            case TraversableLocation.LOCATION_2:
                locations[1].SetActive(true);
                break;
            case TraversableLocation.LOCATION_3:
                locations[2].SetActive(true);
                break;
        }

        currentlyActiveLocation = newLocation;
    }
}
