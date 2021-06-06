using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	private static PauseMenu _instance;
	public static PauseMenu Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("PauseMenu is NULL");

			return _instance;
		}
	}

	public static bool GameIsPaused = false;

	public GameObject pauseMenuUI;
	public GameObject GameOverMenuUI;

	private void Awake()
	{
		_instance = this;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !GameOverMenuUI.activeInHierarchy)
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Restart()
	{
		Time.timeScale = 1f;
		GameIsPaused = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void GameOverPause()
	{
		GameOverMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void GoToMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
