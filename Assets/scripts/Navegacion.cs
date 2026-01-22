using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;


public class Navegacion : MonoBehaviour
{
	public string nombreArchivo = "JuegoGuardado";
	public string nombreDirectorio = "Partidas";
	public GameData datosJuego;
	public static string nombreJugador = "";
	public GameObject menu;
	public GameObject settings;
	public GameObject escenary;
	public GameObject credits;
	public Slider volumeSlider;
	public AudioSource musicSource;
	public GameObject panel;
	public GameObject noName;
	
	private void Awake(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
    // Start is called before the first frame update
	private void Start()
	{
		if (!Directory.Exists(nombreDirectorio)){
			Directory.CreateDirectory(nombreDirectorio);
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream saveFile = File.Create(nombreDirectorio + "/" + nombreArchivo + ".bin");
			formatter.Serialize(saveFile, datosJuego);
			saveFile.Close();
			Debug.Log("Guardado en " + Directory.GetCurrentDirectory().ToString() + "/" + nombreDirectorio + "/" + nombreArchivo + ".bin");
		}
		volumeSlider.value = musicSource.volume;
		volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

	public void IrJuego(){
		nombreJugador = GameObject.Find("txtNombre").GetComponent<InputField>().text;
		
		if(nombreJugador != ""){
			
		StartCoroutine(ShowPanelCoroutine());
		
		SceneManager.LoadScene("EscenarioUno");
			
		}
		else{
		Debug.Log("Falta nombre");
		noName.SetActive(true);
		}

		

	} 
	
	
	public void IrMapa1(){
		
		nombreJugador = GameObject.Find("txtNombre").GetComponent<InputField>().text;
		
		if(nombreJugador != ""){
			
			StartCoroutine(ShowPanelCoroutine());
		
			SceneManager.LoadScene("Map_v1");
			
		}
		
		else{
			Debug.Log("Falta nombre");
			noName.SetActive(true);
		}

	}
	
	public void IrMapa2(){
		
		nombreJugador = GameObject.Find("txtNombre").GetComponent<InputField>().text;
		
		if(nombreJugador != ""){
			
			StartCoroutine(ShowPanelCoroutine());
		
			SceneManager.LoadScene("Map_v2");
			
		}
		
		else{
			Debug.Log("Falta nombre");
			noName.SetActive(true);
		}

	}
	
	public void closeNoName(){
		noName.SetActive(false);
	}
	
	
	public void OpenSettings()
	{
		menu.SetActive(false);
		settings.SetActive(true);
		credits.SetActive(false);
		escenary.SetActive(false);
	}

	public void BackToMenu()
	{
		menu.SetActive(true);
		settings.SetActive(false);
		credits.SetActive(false);
	}

	void ChangeVolume(float value)
	{
		musicSource.volume = value;
	}
	
	public void QuitGame()
	{
		Debug.Log("Cerrando el juego...");
		Application.Quit();
	}
	
	public void openCredits()
	{
		menu.SetActive(false);
		settings.SetActive(false);
		credits.SetActive(true);
		
	}
	
	
	public void openEscenary(){
		menu.SetActive(false);
		settings.SetActive(false);
		credits.SetActive(false);
		escenary.SetActive(true);
	}
	
	public void ShowAndHidePanel()
	{
		StartCoroutine(ShowPanelCoroutine());
	}

	private IEnumerator ShowPanelCoroutine()
	{
		panel.SetActive(true);           // Mostrar panel
		yield return new WaitForSeconds(3f);  // Esperar 3 segundos
		panel.SetActive(false);
	}
	
}
