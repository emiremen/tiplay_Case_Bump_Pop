using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Chain : MonoBehaviour
{
    private TextMeshPro chainText;
    private List<GameObject> balls;
    private int chainTextCapacity;
    private float loseCounter = 5;
    private bool isCounterStarted;

    private void Start()
    {
        balls = new List<GameObject>();
        chainText = GetComponentInChildren<TextMeshPro>();
        chainTextCapacity = int.Parse(chainText.text);
    }

    private void Update()
    {
        if (isCounterStarted)
        {
            loseCounter -= Time.deltaTime;
            if (loseCounter <= 0 && balls.Count < chainTextCapacity)
            {
                EventManager.showGameOverPanel?.Invoke();
                return;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isCounterStarted)
            {
                isCounterStarted = true;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Force);
            if (!balls.Contains(other.gameObject))
            {
                balls.Add(other.gameObject);
                if (int.Parse(chainText.text) > 0)
                {
                    chainText.text = (int.Parse(chainText.text) - 1).ToString();
                }
            }


            if (balls.Count >= chainTextCapacity)
            {
                for (int i = 0; i < balls.Count; i++)
                {
                    balls[i].GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Force);
                }

                StartCoroutine(breakChains());

                //CapsuleCollider[] chaincollider = GetComponentsInChildren<CapsuleCollider>();
                //foreach (Collider collider in chaincollider)
                //{
                //    collider.enabled = false;

                //}
            }
        }
    }

    IEnumerator breakChains()
    {
        yield return new WaitForSeconds(1);
        Joint[] chainJoint = GetComponentsInChildren<Joint>();
        foreach (Joint joint in chainJoint)
        {
            joint.breakForce = 1;

        }
        Physics.IgnoreLayerCollision(3, 6, false);
    }
}