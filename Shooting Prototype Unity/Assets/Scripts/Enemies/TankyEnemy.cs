using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankyEnemy : EnemyTemplate
{
	public delegate void OnTankyEnemyKilled(Vector3 enemyCentralPos);
	public static event OnTankyEnemyKilled onTankyEnemyKilled;

	public float maxHealth;
	private float currentHealth;

	public AudioSource tankyEnemyHitSFX;

	#region Unity Monobehaviour Methods

	void Start()
    {
		InitializeComponents();

		currentHealth = maxHealth;

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
		rb2d.MovePosition((Vector2)transform.position + (direction * GameStatsManager.Instance.tankyEnemyMoveSpeed * Time.fixedDeltaTime));
	}

	public void TakeDamage()
	{
		currentHealth--;

		tankyEnemyHitSFX.volume = Random.Range(0.15f, 0.35f);
		tankyEnemyHitSFX.Play();

		if (currentHealth <= 0)
		{
			GameManager.Instance.UpdateScore();
			Die();
		}
	}

	void Die()
	{
		onTankyEnemyKilled(transform.position);
		Destroy(gameObject);
	}

	#endregion
}
