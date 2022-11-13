using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class EventManager {
    public static Func<List<Transform>> getCheckPoints;
    public static Action<Transform> setCinemachineTarget;
    public static Action<Transform> removeCheckPointFromList;
    public static Func<int> getCheckPointsCount;
    public static Action<Transform> setBalls;
    public static Action throwPlayerBall;
    public static Action setPlayerForTargeting;
    public static Func<GameObject> getCurrentPlayer;
    public static Action<bool> setIsPlayerTargeting;
    public static Action<bool> setIsCurrentPlayer;
    public static Func<CinemachineVirtualCamera[]> getCinemachines;
    public static Func<string,GameObject> callObjectFromPool;
    public static Action playMoneyParticle;
    public static Action playBallParticle;

    #region Score
    public static Action gainMoney;
    public static Action gainBalls;
    public static Action<Vector3> spawnMoney;
    #endregion


    #region UI
    public static Action showGameOverPanel;
    public static Action showWinPanel;
    #endregion

    #region GameManagement
    public static Action<GameState> setGameState;
    public static Func<GameState> getGameState;
    public static Func<GameData> getGameData;
    #endregion

}
