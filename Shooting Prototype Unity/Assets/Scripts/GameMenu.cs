using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
	public List<GameObject> panels;

    public void Quit()
	{
		Application.Quit();
	}
	public void NewGame()
	{
		SceneManager.LoadScene(1);
	}

	public void GoToSettings()
    {
		GoToNewPanel("Settings_Panel");
    }

	public void GoToAbout()
    {
		GoToNewPanel("About_Panel");
    }

	public void GoToInitialMenu()
    {
		GoToNewPanel("Initial_Menu_Panel");
    }

	public void GoToNewPanel(string panelName)
    {
		foreach(GameObject go in panels)
        {
			if (go.name == panelName)
            {
				go.SetActive(true);
            }
			else
            {
				go.SetActive(false);
            }
        }
    }
}
