using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyTemplate : MonoBehaviour
{
	protected Rigidbody2D rb2d;
	protected PlayerController player;
	protected Animator animator;

	protected Vector2 movement;
	protected Vector2 direction;

	protected void InitializeComponents()
	{
		rb2d = GetComponent<Rigidbody2D>();
		player = FindObjectOfType<PlayerController>();
		animator = GetComponent<Animator>();
	}

	protected void UpdateBirdAnimations()
	{
		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
	}
}
