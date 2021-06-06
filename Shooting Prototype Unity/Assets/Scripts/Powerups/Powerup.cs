using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	public delegate void WeaponUpdate();
	public static event WeaponUpdate onWeaponUpdate;

	public int id;
	public string title;
	public string description;
	public Sprite icon;
	public PowerupType type;

	public void ApplyPowerup()
	{
		switch (type)
		{
			case PowerupType.BIGGER_BULLETS:
				BiggerBulletsPowerup();
				break;
			case PowerupType.INCREASED_FIRERATE:
				MoreFireRatePowerup();
				break;
			case PowerupType.SLOWER_ROTATION:
				SlowerRotationPowerup();
				break;
			case PowerupType.DOUBLE_SHOOT:
			case PowerupType.TRIPLE_SHOOT:
			case PowerupType.QUADRA_SHOOT:
				UpdateWeaponPowerup();
				break;
		}
	}

	void BiggerBulletsPowerup()
	{
		GameStatsManager.Instance.bulletSize = 2f;
		PowerupInventory.Instance.EndUpgrading(type);
	}

	void MoreFireRatePowerup()
	{
		GameStatsManager.Instance.fireRate /= 2f;
		PowerupInventory.Instance.EndUpgrading(type);
	}

	void SlowerRotationPowerup()
	{
		GameStatsManager.Instance.normalRotationSpeed += 15f;
		GameStatsManager.Instance.slowerRotationSpeed += 10f;
		PowerupInventory.Instance.EndUpgrading(type);
	}

	void UpdateWeaponPowerup()
	{
		onWeaponUpdate();
		PowerupInventory.Instance.EndUpgrading(type);
	}
}
