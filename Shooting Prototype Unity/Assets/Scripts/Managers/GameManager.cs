using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	#region Delegates and Events

	public delegate void PowerupUpdate();
	public static event PowerupUpdate onPowerupUpdate;

	public delegate void StartSpawningNewEnemies();
	public static event StartSpawningNewEnemies onStartSpawnningNewEnemies;

	#endregion

	#region Singleton Pattern

	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("GameManager is NULL");

			return _instance;
		}
	}

	#endregion

	#region Fields

	private int playerScore = 0;
	
	public int[] powerupUpgrades;
	public int[] spawnNewEnemiesScore;

	public int currentPowerupUpgradeCounter;
	public int currentSpawnEnemiesCounter;

	public float shootCounter = 0;

	private bool canStillUpgradePowerup;
	private bool canStillSpawnEnemies;
	
	public Text scoreText;
	
	#endregion

	#region MonoBehaviour Methods

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		currentPowerupUpgradeCounter = 0;
		currentSpawnEnemiesCounter = 0;
		canStillUpgradePowerup = true;
		canStillSpawnEnemies = true;
	}

	#endregion

	#region My Methods

	public int GetScore()
	{
		return playerScore;
	}

	public void UpdateScore()
	{
		playerScore++;
		scoreText.text = "" + playerScore;

		GameStatsManager.Instance.basicEnemyMoveSpeed += playerScore * 0.00005f;
		GameStatsManager.Instance.enemyFollowsWaypointsMoveSpeed += playerScore * 0.00005f;
		GameStatsManager.Instance.tankyEnemyMoveSpeed += playerScore * 0.00005f;

		CheckForPowerupUpdate();
		CheckForNewEnemiesToSpawn();
	}

	public void RestartScore()
	{
		playerScore = 0;
		scoreText.text = "" + playerScore;
	}

	private void CheckForPowerupUpdate()
	{
		if (canStillUpgradePowerup)
		{
			if (playerScore == powerupUpgrades[currentPowerupUpgradeCounter])
			{
				onPowerupUpdate();

				currentPowerupUpgradeCounter++;

				if (currentPowerupUpgradeCounter == powerupUpgrades.Length)
					canStillUpgradePowerup = false;
			}
		}
	}

	private void CheckForNewEnemiesToSpawn()
	{ 
		if (canStillSpawnEnemies)
		{
			if (playerScore == spawnNewEnemiesScore[currentSpawnEnemiesCounter])
			{
				onStartSpawnningNewEnemies();

				currentSpawnEnemiesCounter++;

				if (currentSpawnEnemiesCounter == spawnNewEnemiesScore.Length)
					canStillSpawnEnemies = false;
			}
		}
	}

	#endregion
}