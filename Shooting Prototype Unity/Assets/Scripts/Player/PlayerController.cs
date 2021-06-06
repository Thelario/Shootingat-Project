using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public delegate void FirstShoot();
	public static event FirstShoot onFirstShoot;

	#region Fields

	public bool canMove;

	public GameObject bullet;

	private List<Transform> shootPoints;
	private List<ParticleSystem> shootingParticles;
	public Transform[] weapons;

	public AudioSource shootingSoundEffect;

	float timeToShoot = 0f;
	bool canShoot = true;

	bool hasMadeFirstShoot = false;

	#endregion

	#region MonoBehaviour Methods

	private void Start()
	{
		shootPoints = new List<Transform>();
		shootingParticles = new List<ParticleSystem>();

		Powerup.onWeaponUpdate += UpdateWeapon;

		FillShootPoints();
	}

	private void Update()
	{
		if (!GameStatsManager.Instance.isInUpdateMode)
		{
			if (canMove)
			{

#if UNITY_ANDROID

				if (Input.touchCount > 0)
				{
					if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
					{
						GameManager.Instance.shootCounter++;

						RotatePlayer(GameStatsManager.Instance.slowerRotationSpeed);

						timeToShoot += Time.deltaTime;

						if (timeToShoot >= GameStatsManager.Instance.fireRate)
						{
							canShoot = true;
							timeToShoot = 0;
						}
						else canShoot = false;

						if (canShoot)
						{
							Shoot();
						}
					}
				}
				else
				{
					timeToShoot += Time.deltaTime;
					RotatePlayer(GameStatsManager.Instance.normalRotationSpeed);
				}
#endif

#if UNITY_STANDALONE || UNITY_EDITOR

				if (Input.GetMouseButton(0))
				{
					GameManager.Instance.shootCounter++;

					RotatePlayer(GameStatsManager.Instance.slowerRotationSpeed);

					timeToShoot += Time.deltaTime;

					if (timeToShoot >= GameStatsManager.Instance.fireRate)
					{
						canShoot = true;
						timeToShoot = 0;
					}
					else canShoot = false;

					if (canShoot)
					{
						Shoot();
					}
				}
				else
				{
					timeToShoot += Time.deltaTime;
					RotatePlayer(GameStatsManager.Instance.normalRotationSpeed);
				}
#endif
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			PauseMenu.Instance.GameOverPause();
		}
	}

	private void OnDestroy()
	{
		Powerup.onWeaponUpdate -= UpdateWeapon;
	}

	#endregion

	#region My Methods

	void RotatePlayer(float rotationSpeed)
	{
		Vector3 newVector = Vector3.forward * rotationSpeed * Time.deltaTime;

		transform.Rotate(newVector);
	}

	void Shoot()
	{
		if (!hasMadeFirstShoot)
		{
			hasMadeFirstShoot = true;
			onFirstShoot();
		}

		foreach (Transform shootPoint in shootPoints)
		{
			GameObject bullet = PoolManager.Instance.RequestBullet();
			bullet.transform.position = shootPoint.position;
			bullet.transform.rotation = shootPoint.rotation;

			if (bullet.gameObject.transform.localScale != bullet.gameObject.transform.localScale * GameStatsManager.Instance.bulletSize)
			{
				Bullet b = bullet.GetComponent<Bullet>();
				if (!b.hasAlreadyChangedSize)
				{
					bullet.gameObject.transform.localScale *= GameStatsManager.Instance.bulletSize;
					b.hasAlreadyChangedSize = true;
				}
			}

			Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
			rb.AddForce(shootPoint.up * GameStatsManager.Instance.bulletSpeed, ForceMode2D.Impulse);
		}

		foreach (ParticleSystem particles in shootingParticles)
		{
			particles.Play();
		}

		StartCoroutine(CameraShake.Instance.Shake(GameStatsManager.Instance.shakeDuration, GameStatsManager.Instance.shakeMagnitudes[PowerupInventory.Instance.weaponsPowerupCounter]));

		shootingSoundEffect.volume = Random.Range(0.15f, 0.35f);
		shootingSoundEffect.Play();
	}

	void FillShootPoints()
	{
		shootPoints.Clear();
		shootingParticles.Clear();

		foreach (Transform weapon in transform)
		{
			if (weapon.gameObject.activeSelf && weapon.gameObject.name != "PlayerShadow")
			{
				Transform aux = weapon.Find("ShootPoint");
				shootPoints.Add(aux);
				aux = aux.Find("Shooting Particles");
				shootingParticles.Add(aux.GetComponent<ParticleSystem>());
			}
		}
	}

	void UpdateWeapon()
	{
		weapons[PowerupInventory.Instance.weaponsPowerupCounter].transform.gameObject.SetActive(true);

		FillShootPoints();
	}

	#endregion
}
