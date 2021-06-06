using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public GameObject explosionParticles;

	public bool hasAlreadyChangedSize = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag != "WaypointRombo" && collision.gameObject.tag != "WaypointEsquinas")
		{
			if (collision.gameObject.tag == "Enemy")
			{
				GameObject go = Instantiate(explosionParticles, collision.transform.position, Quaternion.identity);
				Destroy(go, 1f);
				Destroy(collision.gameObject);
				GameManager.Instance.UpdateScore();
			}
			else if (collision.gameObject.tag == "Tanky Enemy")
			{
				collision.GetComponent<TankyEnemy>().TakeDamage();
			}

			StartCoroutine(Hide(0f));
		}
	}

	private void OnEnable()
	{
		StartCoroutine(Hide(2f));
	}

	private IEnumerator Hide(float timeBeforeHiding)
	{
		yield return new WaitForSeconds(timeBeforeHiding);

		gameObject.SetActive(false);
	}
}
