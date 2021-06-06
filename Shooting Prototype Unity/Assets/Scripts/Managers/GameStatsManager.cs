using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
	#region Singleton Pattern

	private static GameStatsManager _instance;
	public static GameStatsManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("GameStats is NULL");

			return _instance;
		}
	}

	#endregion

	#region Stats

		#region Player Controller Stats

		public float[] shakeMagnitudes;

		public float normalRotationSpeed; // 100f
		public float slowerRotationSpeed; // 15f

		public float fireRate; // 0.25f
		public float bulletSpeed; // 40f

		public float shakeDuration; // 0.15f
		public float shakeMagnitude; // 0.4f

		public float bulletSize = 1f;

		#endregion

		#region Enemies Stats

		public float basicEnemyMoveSpeed; // 1f
		public float enemyFollowsWaypointsMoveSpeed; // 3.25f
		public float tankyEnemyMoveSpeed; // 0.75f

		#endregion

		#region Game Loop Stats

		public float[] timesToSpawnEnemies; // 2 - 1 - 1 - 10
	
		public bool stoppedGame;

		public bool isInUpdateMode = false;

		#endregion

	#endregion

	#region MonoBehaviour Methods

	private void Awake()
	{
		_instance = this;
	}

	#endregion

	#region My Methods

	

	#endregion
}
