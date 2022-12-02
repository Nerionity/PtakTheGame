using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathScript : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;

    void Update()
    {
        if(GlobalVariables.Instance.playerState == "Wind"){
        distanceTravelled += speed + Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        }
    }
}
