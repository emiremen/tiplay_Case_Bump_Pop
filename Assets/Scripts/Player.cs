using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using PathCreation;
using Cinemachine;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    //[SerializeField] PathCreator pathCreator;
    private List<Transform> checkPoints;

    public bool isCurrentBall;

    [SerializeField] private bool isThrowed, isCollided;

    private void OnEnable()
    {
        EventManager.throwPlayerBall += ThrowPlayerBall;
        EventManager.getCurrentPlayer += GetCurrentPlayer;
        EventManager.setIsCurrentPlayer += SetIsCurrentPlayer;
    }

    private void OnDisable()
    {
        EventManager.throwPlayerBall -= ThrowPlayerBall;
        EventManager.getCurrentPlayer -= GetCurrentPlayer;
        EventManager.setIsCurrentPlayer -= SetIsCurrentPlayer;
        isThrowed = false;
        isCollided = false;
    }
    
    private GameObject GetCurrentPlayer()
    {
        return gameObject;
    }

    private void SetIsCurrentPlayer(bool value)
    {
        isCurrentBall = value;
    }

    private void Start()
    {
        isThrowed = false;
        isCollided = false;
        rb = GetComponent<Rigidbody>();
        checkPoints = EventManager.getCheckPoints?.Invoke();
        // yield return new WaitForSeconds(0.1f);
        EventManager.setBalls?.Invoke(transform);
        if (true)
        {

        }
    }

    private void Update()
    {
        transform.GetComponent<Renderer>().material.mainTextureOffset = rb.velocity;
        if (rb.velocity.magnitude < 1f)
        {
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            EventManager.setIsPlayerTargeting?.Invoke(true);
            if (isThrowed == true && isCollided == false && EventManager.getGameState?.Invoke() != GameState.end)
            {
                EventManager.showGameOverPanel?.Invoke();
            }
            isThrowed = false;
        }
        else
        {
            EventManager.setIsPlayerTargeting?.Invoke(false);
        }
    }


    private void ThrowPlayerBall()
    {
        if (!isThrowed)
        {
            isThrowed = true;
            //rb.AddForce(transform.forward * 150, ForceMode.Impulse);
            rb.velocity += (transform.forward * 150);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("GroundBall"))
        {
            isCollided = true;
        }
    }

}
