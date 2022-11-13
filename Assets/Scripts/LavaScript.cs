using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.DOJump(transform.position + new Vector3(-40, -5, 40), 15f, 1, 1.5f);
            StartCoroutine(showWinPanel());
            StartCoroutine(increaseEarningsTextSlowly());
        }
    }

    IEnumerator showWinPanel()
    {
        if (EventManager.getGameState?.Invoke() != GameState.end)
        {
            EventManager.setGameState(GameState.end);
            EventManager.playMoneyParticle?.Invoke();
            yield return new WaitForSeconds(3);
            EventManager.showWinPanel?.Invoke();
        }
    }

    IEnumerator increaseEarningsTextSlowly()
    {
        yield return new WaitForSeconds(1f);
        EventManager.gainMoney?.Invoke();
        EventManager.gainBalls?.Invoke();
    }
}