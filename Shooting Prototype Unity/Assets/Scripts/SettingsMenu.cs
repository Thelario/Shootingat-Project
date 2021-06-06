using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer audioMixer;

	/* VOLUME */
	public void SetVolumen(float volume)
	{
		audioMixer.SetFloat("MyExposedParam", Mathf.Log10(volume) * 20);
	}

	public void GetVolumes()
	{
		audioMixer.GetFloat("MyExposedParam", out DoNotDestroy.previousVolume);
	}
}
