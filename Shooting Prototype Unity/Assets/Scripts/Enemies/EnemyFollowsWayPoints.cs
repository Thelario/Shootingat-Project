using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfBird { ROMBO, ESQUINAS }

public class EnemyFollowsWayPoints : EnemyTemplate
{
	#region Fields

	public TypeOfBird type;

	private List<Transform> waypoints;
	
	private GameObject waypointsParent;

	private float currentEnemyMoveSpeed;
	private string collisionWaypointTagName;

	private int waypointCounter;
	private int nextWaypoint;
	private int randomMinimumWaypointsBeforePlayer;

	#endregion

	#region Mono Behaviours Methods 

	void Awake()
	{
		waypoints = new List<Transform>();
	}

	void Start()
    {
		currentEnemyMoveSpeed = GameStatsManager.Instance.enemyFollowsWaypointsMoveSpeed;

		InitializeComponents();

		SetUpWaypoints();

		GetNextDirection();
	}

	private void FixedUpdate()
	{
		MoveCharacter(movement);
	}

	private void LateUpdate()
	{
		UpdateBirdAnimations();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == collisionWaypointTagName)
		{
			ChangeDirection();
		}

	}

	#endregion

	#region My Methods

	void SetUpWaypoints()
	{
		if (type == TypeOfBird.ROMBO)
		{
			waypointsParent = GameObject.Find("Waypoints_Rombo");
			collisionWaypointTagName = "WaypointRombo";
		}
		else
		{
			waypointsParent = GameObject.Find("Waypoints_Esquinas");
			collisionWaypointTagName = "WaypointEsquinas";
		}

		float minimumDistanceFromWaypoint = float.MaxValue;

		nextWaypoint = 0;
		waypointCounter = 0;

		foreach (Transform t in waypointsParent.transform)
		{
			waypoints.Add(t);

			Vector3 distance = t.transform.position - transform.position;

			if (distance.magnitude < minimumDistanceFromWaypoint)
			{
				nextWaypoint = waypoints.Count - 1;
				minimumDistanceFromWaypoint = distance.magnitude;
			}
		}

		randomMinimumWaypointsBeforePlayer = Random.Range(waypoints.Count, waypoints.Count * 3);
	}

	void MoveCharacter(Vector2 direction)
	{
		if (!GameStatsManager.Instance.isInUpdateMode)
			rb2d.MovePosition((Vector2)transform.position + (direction * currentEnemyMoveSpeed * Time.fixedDeltaTime));
		else
			rb2d.velocity = Vector3.zero;
	}

	void ChangeDirection()
	{
		waypointCounter++;
		nextWaypoint++;

		if (nextWaypoint >= waypoints.Count)
			nextWaypoint = 0;

		if (waypointCounter >= randomMinimumWaypointsBeforePlayer)
		{
			GetPlayersPosition();
			currentEnemyMoveSpeed = GameStatsManager.Instance.basicEnemyMoveSpeed;
		}
		else
		{
			GetNextDirection();
		}
	}

	void GetPlayersPosition()
	{
		direction = player.transform.position - transform.position;
		direction.Normalize();
		movement = direction;
	}

	void GetNextDirection()
	{
		direction = waypoints[nextWaypoint].transform.position - transform.position;
		direction.Normalize();
		movement = direction;
	}

	#endregion
}
