using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFFModel : MonoBehaviour
{
    //prototype was 
    // y = 3.8
    // x = 4.4

    private float yVal = 74f;
    private float xVal = 98.5f;

    private float maxY = 74f;
    private float minY = -74f;

    private float maxX = 98.5f;
    private float minX = -98.5f;

    private Vector2 currentPosition;
    private Vector2 currentDestination;

    public float nat = 0;
    public float cul = 0;
    public float soc = 0;

    [SerializeField] private GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = indicator.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateDestination();

        if(currentPosition != currentDestination)
        {
            indicator.transform.localPosition = Vector2.MoveTowards(indicator.transform.localPosition, currentDestination, 2f);
            currentPosition = indicator.transform.localPosition;
        }
        
    }

    private void CalculateDestination()
    {
        //Culture point = -4.4f, -3.8f (between -4.4f and 0) ---- TOP
        //Nature point = 0, 3.8f (between -3.8f and 0) --- LEFT
        //Society point = 4.4f, -3.8f (between 0 and 4.4f) --- RIGHT

        //Vertical is not shared like horizontal is
        float destY = ((ReputationManager.nature * 2) * yVal) / 100;

        float fixedCulture = (-ReputationManager.culture * xVal) / 100;
        float fixedSociety = (ReputationManager.society * xVal) / 100;

        float destX = fixedCulture + fixedSociety;

        if (destY > maxY) destY = maxY;
        if (destY < minY) destY = minY;

        if (destX > maxX) destX = maxX;
        if (destX < minX) destX = minX;

        //For displaying in inspector
        nat = ReputationManager.nature;
        cul = ReputationManager.culture;
        soc = ReputationManager.society;

        currentDestination = new Vector2(destX, destY);

        Debug.Log(destY + "__" + destX);
    }
}
