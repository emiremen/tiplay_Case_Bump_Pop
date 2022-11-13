using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameData gameData;
    private float money;
    private int balls;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI ballsText;

    [SerializeField] private TextMeshProUGUI cloneLevelTxt;
    [SerializeField] private TextMeshProUGUI cloneCostTxt;
    [SerializeField] private TextMeshProUGUI incomeLevelTxt;
    [SerializeField] private TextMeshProUGUI incomeCostTxt;

    [SerializeField] private GameObject marketPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    [SerializeField] private ParticleSystem moneyParticle;
    [SerializeField] private ParticleSystem ballParticle;

    private void OnEnable()
    {
        EventManager.gainMoney += GainMoney;
        EventManager.gainBalls += GainBalls;
        EventManager.showGameOverPanel += ShowGameOverPanel;
        EventManager.showWinPanel += ShowWinPanel;
        EventManager.playMoneyParticle += PlayMoneyParticle;
        EventManager.playBallParticle += PlayBallParticle;
    }

    private void OnDisable()
    {
        EventManager.gainMoney -= GainMoney;
        EventManager.gainBalls -= GainBalls;
        EventManager.showGameOverPanel -= ShowGameOverPanel;
        EventManager.showWinPanel -= ShowWinPanel;
        EventManager.playMoneyParticle -= PlayMoneyParticle;
        EventManager.playBallParticle -= PlayBallParticle;
    }



    private void Start()
    {
        gameData = EventManager.getGameData?.Invoke();

        cloneLevelTxt.text = $"Level {gameData.cloneLevel}";
        incomeLevelTxt.text = $"Level {gameData.incomeLevel}";
        cloneCostTxt.text = gameData.cloneCost.ToString();
        incomeCostTxt.text = gameData.incomeCost.ToString();

        money = gameData.money;
        moneyText.text = money.ToString("0.#");
        balls = gameData.balls;
        ballsText.text = balls.ToString();

        marketPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void GainMoney()
    {
        money += 0.5f + (.5f * ((float)gameData.incomeLevel / 5f));
        gameData.money = money;
        moneyText.text = money.ToString("0.#");
        moneyText.transform.DOComplete();
        moneyText.transform.DOShakeScale(.5f, .15f);
    }
    private void GainBalls()
    {
        balls++;
        gameData.balls = balls;
        ballsText.text = balls.ToString();
        ballsText.transform.DOComplete();
        ballsText.transform.DOShakeScale(.5f, .15f);
    }
    private void PlayMoneyParticle()
    {
        moneyParticle.Play();
    }
    private void PlayBallParticle()
    {
        ballParticle.Play();
    }


    private void SaveMoneyData()
    {
        gameData.money = money;
        gameData.balls = balls;
    }

    private void ShowGameOverPanel()
    {
        SaveMoneyData();
        gameOverPanel.SetActive(true);
        EventManager.setGameState?.Invoke(GameState.end);
    }

    private void ShowWinPanel()
    {
        SaveMoneyData();
        winPanel.SetActive(true);
        gameData.level++;
        EventManager.setGameState?.Invoke(GameState.end);
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReviveButton()
    {
        gameOverPanel.SetActive(false);

        EventManager.setGameState?.Invoke(GameState.start);
    }

    public void IncreaseClone()
    {
        if (money >= gameData.cloneCost && gameData.cloneLevel < gameData.maxLevel)
        {
            cloneLevelTxt.transform.parent.DOComplete();
            cloneLevelTxt.transform.parent.DOShakeScale(.5f, .15f);
            gameData.cloneLevel++;
            gameData.money -= gameData.cloneCost;
            gameData.cloneCost += 20;
            SetIncrementalsValues();
        }
    }

    public void IncreaseIncome()
    {
        if (money >= gameData.incomeCost && gameData.incomeLevel < gameData.maxLevel)
        {
            incomeLevelTxt.transform.parent.DOComplete();
            incomeLevelTxt.transform.parent.DOShakeScale(.5f, .15f);
            gameData.incomeLevel++;
            gameData.money -= gameData.incomeCost;
            gameData.incomeCost += 20;
            SetIncrementalsValues();
        }
    }

    private void SetIncrementalsValues()
    {
        cloneCostTxt.text = gameData.cloneCost.ToString();
        cloneLevelTxt.text = $"Level {gameData.cloneLevel}";
        incomeCostTxt.text = gameData.incomeCost.ToString();
        incomeLevelTxt.text = $"Level {gameData.incomeLevel}";

        money = gameData.money;
        moneyText.text = money.ToString("0.#");
        balls = gameData.balls;
        ballsText.text = balls.ToString();
    }

    public void TapToStart()
    {
        StartCoroutine(TapToStartDelayed());
    }

    public IEnumerator TapToStartDelayed()
    {
        yield return new WaitForSeconds(.1f);
        EventManager.setGameState?.Invoke(GameState.start);
        marketPanel.SetActive(false);
    }
}