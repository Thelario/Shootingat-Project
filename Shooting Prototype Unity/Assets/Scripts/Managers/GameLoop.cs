using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
	#region Fields

	public GameObject[] enemiesPrefabs;
	private int enemyPrefabsCounter;

	public Text countdownText;

	public Camera cam;
	
	public float leftCenterLimit;
	public float centerUpLimit;
	public float centerDownLimit;
	public float rightCenterLimit;

	public float minDistanceToSpawnHorizontally;
	public float minDistanceToSpawnVertically;

	public float forceAppliedToEnemiesWhenSpawnned;

	PlayerController player;

	#endregion

	#region Mono Behaviour Methods

	void Start()
    {
		player = FindObjectOfType<PlayerController>();
		player.canMove = false;
		enemyPrefabsCounter = 0;

		TankyEnemy.onTankyEnemyKilled += CreateEnemiesWhenTankyEnemyKilled;
		GameManager.onStartSpawnningNewEnemies += StartSpawnningNewEnemies;

		StartCoroutine(StartCountDown());
	}

	private void OnDestroy()
	{
		TankyEnemy.onTankyEnemyKilled -= CreateEnemiesWhenTankyEnemyKilled;
		GameManager.onStartSpawnningNewEnemies -= StartSpawnningNewEnemies;
	}

	#endregion

	#region My Methods

	IEnumerator StartCountDown()
	{
		if (player.canMove)
			player.canMove = false;

		for (int i = 3; i >= 0; i--)
		{
			if (i == 0)
			{
				countdownText.text = "GO!!!";
			}
			else
			{
				countdownText.text = "" + i;
			}

			yield return new WaitForSeconds(1f);
		}

		countdownText.gameObject.SetActive(false);
		player.canMove = true;

		StartSpawnningNewEnemies();
	}

	IEnumerator SpawnerLoop(float timeDelayed)
	{
		GameObject enemyToSpawn = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];

		if (!GameStatsManager.Instance.isInUpdateMode)
		{
			Vector2 enemyPositionToSpawn = PickEnemyPositionToSpawn();

			CreateEnemy(enemyPositionToSpawn, enemyToSpawn);

			yield return new WaitForSeconds(timeDelayed);

			StartCoroutine(SpawnerLoop(timeDelayed));
		}
		else
		{
			yield return new WaitForSeconds(3f);

			StartCoroutine(SpawnerLoop(timeDelayed));
		}
	}

	Vector2 PickEnemyPositionToSpawn()
	{
		int side = Random.Range(1, 4);

		float x;
		float y;

		float height = cam.orthographicSize;
		float width = height * cam.aspect;

		switch (side)
		{
			case 1:
				x = Random.Range(leftCenterLimit, -width);
				y = Random.Range(centerDownLimit, centerUpLimit);
				break;
			case 2:
				x = Random.Range(width, rightCenterLimit);
				y = Random.Range(centerDownLimit, centerUpLimit);
				break;
			case 3:
				x = Random.Range(leftCenterLimit, rightCenterLimit);
				y = Random.Range(height, centerUpLimit);
				break;
			case 4:
				x = Random.Range(leftCenterLimit, rightCenterLimit);
				y = Random.Range(centerDownLimit, -height);
				break;
			default:
				x = 25;
				y = 25;
				break;
		}

		return new Vector2(x, y);
	}

	void CreateEnemy(Vector2 enemyPosition, GameObject enemy)
	{
		 Instantiate(enemy, enemyPosition, Quaternion.identity);
	}

	public void CreateEnemiesWhenTankyEnemyKilled(Vector3 centralPositionFromWhereToSpawn)
	{
		int numberOfEnemiesToSpawn = Random.Range(3, 6);
		
		for (int i = 0; i < numberOfEnemiesToSpawn; i++)
		{
			float x = Random.Range(-2f, 2f);
			float y = Random.Range(-2f, 2f);

			Vector2 enemyPos = new Vector2(centralPositionFromWhereToSpawn.x + x, centralPositionFromWhereToSpawn.y + y);

			int randomFollows = Random.Range(1, 3);

			CreateEnemy(enemyPos, enemiesPrefabs[randomFollows]);
		}
	}

	public void StartSpawnningNewEnemies()
	{
		StartCoroutine(SpawnerLoop(GameStatsManager.Instance.timesToSpawnEnemies[enemyPrefabsCounter]));

		enemyPrefabsCounter++;
	}

	#endregion
}
