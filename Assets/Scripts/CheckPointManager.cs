using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    [SerializeField] private List<Transform> balls;
    [SerializeField] private List<Transform> checkPoints;
    private int currentBallIndex;
    private int previousBallIndex;

    private void OnEnable()
    {
        EventManager.getCheckPoints += GetCheckPoints;
        EventManager.removeCheckPointFromList += RemoveCheckPointFromList;
        EventManager.getCheckPointsCount += GetCheckPointsCount;
        EventManager.setBalls += SetBalls;
    }
    private void OnDisable()
    {
        EventManager.getCheckPoints -= GetCheckPoints;
        EventManager.removeCheckPointFromList -= RemoveCheckPointFromList;
        EventManager.getCheckPointsCount -= GetCheckPointsCount;
        EventManager.setBalls -= SetBalls;
    }

    private void SetBalls(Transform _transform)
    {
        balls.Add(_transform);
    }

    private List<Transform> GetCheckPoints()
    {
        return checkPoints;
    }

    private int GetCheckPointsCount()
    {
        return checkPoints.Count;
    }

    private void RemoveCheckPointFromList(Transform _transform)
    {
        checkPoints.Remove(_transform);
        Destroy(_transform.gameObject);
    }



    void Start()
    {
        InvokeRepeating("BallDistanceCalc", 0, .5f);
    }

    void BallDistanceCalc()
    {
        float dist = 10000;

        for (int i = 0; i < balls.Count; i++)
        {
            if (Vector3.Distance(checkPoints[0].position, balls[i].position) < dist)
            {
                dist = Vector3.Distance(checkPoints[0].position, balls[i].position);
                currentBallIndex = i;
                balls[i].GetComponent<Player>().isCurrentBall = true;
            }
        }
        for (int i = 0; i < balls.Count; i++)
        {
            if (currentBallIndex == i)
            {
                balls[i].GetComponent<Player>().enabled = true;
                balls[i].GetComponent<Player>().isCurrentBall = true;
            }
            else
            {
                balls[i].GetComponent<Player>().enabled = false;
                balls[i].GetComponent<Player>().isCurrentBall = false;
            }
        }
        if (currentBallIndex != previousBallIndex)
        {
            EventManager.setCinemachineTarget?.Invoke(balls[currentBallIndex]);
            previousBallIndex = currentBallIndex;
        }
    }
}
