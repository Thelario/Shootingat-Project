using UnityEngine;

public class BasicEnemy : EnemyTemplate
{
	#region Unity Monobehaviour Methods

	private void Start()
    {
		InitializeComponents();

		direction = player.transform.position - transform.position;
		direction.Normalize();
		movement = direction;
	}

	private void FixedUpdate()
	{
		if (!GameStatsManager.Instance.isInUpdateMode)
		{
			MoveCharacter(movement);
		}
		else
		{
			rb2d.velocity = Vector3.zero;
		}
	}

	private void LateUpdate()
	{
		UpdateBirdAnimations();
	}

	#endregion

	#region My Methods

	private void MoveCharacter(Vector2 direction)
	{
		rb2d.MovePosition((Vector2)transform.position + (direction * GameStatsManager.Instance.basicEnemyMoveSpeed * Time.fixedDeltaTime));
	}

	#endregion

}
