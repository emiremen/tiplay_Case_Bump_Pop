using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class GroundBall : MonoBehaviour
{
    private GameData gameData;
    [SerializeField] private GameObject ball;

    void Start()
    {
        gameData = EventManager.getGameData?.Invoke();
    }

    private void Update()
    {
        if ((EventManager.getCurrentPlayer.Invoke().transform.position - transform.position).magnitude < 50f)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.LookAt(Camera.main.transform.position);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            transform.tag = "Player";
            Destroy(GetComponent<GroundBall>());
            Destroy(transform.GetChild(0).gameObject);
            gameObject.AddComponent<Player>();
            for (int i = 0; i < 4 + gameData.cloneLevel; i++)
            {
                
                GameObject spawnedBall = EventManager.callObjectFromPool?.Invoke("Ball");
                spawnedBall.transform.position = transform.position;
                spawnedBall.transform.rotation = transform.rotation;
                EventManager.gainMoney?.Invoke();
                spawnedBall.transform.GetComponentInChildren<TextMeshPro>().text = "$" + (0.5f + (.5f * ((float)gameData.incomeLevel / 5f)));
                spawnedBall.transform.tag = "Player";
                spawnedBall.GetComponent<Collider>().isTrigger = false;
                //spawnedBall.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)) * 50, ForceMode.Impulse);
                spawnedBall.GetComponent<Rigidbody>().velocity = (new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)) * 100);
                spawnedBall.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
            }
        }
    }
}
