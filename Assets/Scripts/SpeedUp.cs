using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] int speed = 150;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.one;
            other.GetComponent<Rigidbody>().AddForce(-transform.right * speed, ForceMode.Impulse);
        }
    }
}