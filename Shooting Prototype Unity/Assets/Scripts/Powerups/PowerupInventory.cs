using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupType
{
	BIGGER_BULLETS,
	INCREASED_FIRERATE,
	SLOWER_ROTATION,
	DOUBLE_SHOOT,
	TRIPLE_SHOOT,
	QUADRA_SHOOT
}

public class PowerupInventory : MonoBehaviour
{
	private static PowerupInventory _instance;
	public static PowerupInventory Instance
	{
		get
		{
			if (_instance == null)
				Debug.Log("PowerupInventory is NULL");

			return _instance;
		}
	}

	public List<Button> weaponsPowerups;
	public List<Button> upgradesPowerups;

	public int weaponsPowerupCounter = 0;
	public int upgradesPowerupCounter = 0;

	public GameObject leftButtonPlaceholder;
	public GameObject rightButtonPlaceholder;

	private Button button1;
	private Button button2;

	public Canvas PowerupsCanvas;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		GameManager.onPowerupUpdate += Upgrade;
	}

	private void OnDestroy()
	{
		GameManager.onPowerupUpdate -= Upgrade;
	}

	public void Upgrade()
	{
		PowerupsCanvas.gameObject.SetActive(true);

		foreach (Button b in weaponsPowerups)
		{
			Powerup powerup2 = b.GetComponent<Powerup>();
			if (powerup2.id == weaponsPowerupCounter)
			{
				button2 = Instantiate(b, leftButtonPlaceholder.transform, false);
				break;
			}
		}

		foreach (Button b in upgradesPowerups)
		{
			Powerup powerup1 = b.GetComponent<Powerup>();
			if (powerup1.id == upgradesPowerupCounter)
			{
				button1 = Instantiate(b, rightButtonPlaceholder.transform, false);
				break;
			}
		}

		GameStatsManager.Instance.isInUpdateMode = true;
	}

	public void EndUpgrading(PowerupType type)
	{
		switch(type)
		{
			case PowerupType.BIGGER_BULLETS:
			case PowerupType.INCREASED_FIRERATE:
			case PowerupType.SLOWER_ROTATION:
				upgradesPowerups.Remove(upgradesPowerups.Find(powerup => powerup.GetComponent<Powerup>().id == button1.GetComponent<Powerup>().id));
				upgradesPowerupCounter++;
				break;
			case PowerupType.DOUBLE_SHOOT:
			case PowerupType.TRIPLE_SHOOT:
			case PowerupType.QUADRA_SHOOT:
				weaponsPowerups.Remove(button2);
				weaponsPowerupCounter++;
				break;
		}

		foreach (Transform t in rightButtonPlaceholder.transform)
			Destroy(t.gameObject);

		foreach (Transform t in leftButtonPlaceholder.transform)
			Destroy(t.gameObject);

		GameStatsManager.Instance.isInUpdateMode = false;

		PowerupsCanvas.gameObject.SetActive(false);
	}
}
