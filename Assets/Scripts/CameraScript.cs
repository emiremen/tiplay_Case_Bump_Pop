using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachine1;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        EventManager.setCinemachineTarget += SetCinemachineTarget;
    }

    private void OnDisable()
    {
        EventManager.setCinemachineTarget -= SetCinemachineTarget;
    }
    private void SetCinemachineTarget(Transform transform1)
    {
        cinemachine1.Follow = transform1;
        cinemachine1.LookAt = transform1;
        if (cinemachine1.Priority == 1)
        {
            //cinemachine1.Priority = 2;
            //cinemachine2.Priority = 1;
        }
        else
        {
            //cinemachine2.Follow = transform1;
            //cinemachine2.LookAt = transform1;
            //cinemachine2.Priority = 2;
            //cinemachine1.Priority = 1;
        }
    }
}
