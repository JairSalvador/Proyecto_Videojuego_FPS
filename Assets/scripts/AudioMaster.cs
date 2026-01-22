using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMaster : MonoBehaviour
{
	public AudioMixer audioMixer; // Arrastra tu AudioMixer aquí
	public Slider volumeSlider;   // Arrastra el Slider aquí

	void Start()
	{
		// Opcional: cargar volumen guardado
		float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
		volumeSlider.value = savedVolume;
		SetVolume(savedVolume);
	}

	public void SetVolume(float volume)
	{
		// Convierte a decibeles (entre -80 y 0)
		float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
		audioMixer.SetFloat("MasterVolume", dB);

		// Guarda el volumen
		PlayerPrefs.SetFloat("Volume", volume);
	}
}
