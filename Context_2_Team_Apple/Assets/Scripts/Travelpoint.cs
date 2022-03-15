using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travelpoint : MonoBehaviour, IClickable
{

    [SerializeField] private TraversableLocation destination;

    private GameManager gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void ObjectClicked()
    {
        gameController.ChangeLocation(destination);
    }
}
