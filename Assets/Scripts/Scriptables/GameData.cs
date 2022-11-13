using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    #region Earnings
    [Header("Earnings")]
    public float money;
    public int balls;
    #endregion

    #region Level
    [Header("Level")]
    public int level = 1;
    #endregion

    [Space(20)]
    #region Market
    [Header("Market", order = 0)]
    [Space(5)]

    [Header("Market Levels", order = 1)]
    public float maxLevel = 20;
    public int incomeLevel = 1;
    public int cloneLevel = 1;

    [Header("Market Incomes")]
    public int incomeCost = 40;
    public int cloneCost = 40;
    #endregion

}