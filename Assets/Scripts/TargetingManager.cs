using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingManager : MonoBehaviour
{
    [SerializeField] private RaycastHit hit;
    [SerializeField] private RaycastHit reflectHit;
    [SerializeField] private LayerMask layerMask;

    public LineRenderer firstLine;
    public LineRenderer secondLine;

    private Vector3 touchDownPos;
    private Vector3 touchDownDeltaPos;
    private Vector3 touchUpPos;
    private Vector3 touchUpDeltaPos;

    private GameObject target;
    public bool isTargeting;
    private Touch touch;

    private void OnEnable()
    {
        EventManager.setIsPlayerTargeting += SetIsPlayerTargeting;
    }

    private void OnDisable()
    {
        EventManager.setIsPlayerTargeting -= SetIsPlayerTargeting;
    }

    private void SetIsPlayerTargeting(bool value)
    {
        isTargeting = value;
    }


    void Update()
    {
        target = EventManager.getCurrentPlayer?.Invoke();
        DirectionRay();
        if (Input.touchCount > 0 && EventManager.getGameState?.Invoke() == GameState.start)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchDownPos = touch.position;
                touchDownDeltaPos = touch.deltaPosition;
                //cinemachines[0].GetCinemachineComponent<CinemachineTransposer>().m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget; 
                //cinemachines[1].GetCinemachineComponent<CinemachineTransposer>().m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                touchUpPos = touch.position;
                touchUpDeltaPos = touch.deltaPosition;
                target.transform.rotation = Quaternion.Euler(target.transform.rotation.x, touchDownPos.x - touchUpPos.x, target.transform.rotation.z);
                //target.transform.eulerAngles = Vector3.Lerp(target.transform.eulerAngles, Vector3.up * (touchDownPos.x - touchUpPos.x), .01f);


            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (target.GetComponent<Player>().isCurrentBall) EventManager.throwPlayerBall?.Invoke();

                //cinemachines[0].GetCinemachineComponent<CinemachineTransposer>().m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;
                //cinemachines[1].GetCinemachineComponent<CinemachineTransposer>().m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;
                EventManager.setIsPlayerTargeting?.Invoke(false);
                firstLine.gameObject.SetActive(false);
                secondLine.gameObject.SetActive(false);
            }
        }
    }
    private Vector3 DirectionRay()
    {
        if (isTargeting)
        {
            if (Physics.Raycast(target.transform.position, target.transform.forward, out hit, 100, layerMask))
            {
                firstLine.gameObject.SetActive(true);
                Vector3 reflectDirection = Vector3.Reflect(target.transform.forward, hit.normal);
                firstLine.SetPosition(0, target.transform.position);
                firstLine.SetPosition(1, hit.point);

                if (Physics.Raycast(hit.point, reflectDirection * 100, out reflectHit, 100, layerMask))
                {
                    secondLine.gameObject.SetActive(true);
                    secondLine.SetPosition(0, hit.point);
                    secondLine.SetPosition(1, reflectHit.point);
                }
                else
                {
                    secondLine.gameObject.SetActive(false);
                }
            }
            else
            {
                secondLine.gameObject.SetActive(false);
                firstLine.SetPosition(1, target.transform.up + (target.transform.forward * 100));
            }
        }
        else
        {
            firstLine.gameObject.SetActive(false);
            secondLine.gameObject.SetActive(false);
        }
        return firstLine.transform.forward;
    }
}