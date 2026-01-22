using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour

{
	public GameObject pauseMenuUI;
	public GameObject settingsMenu;
	public PlayerInput playerInput;
	//private bool isPaused = false;
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f; // reanuda el tiempo
		Cursor.lockState = CursorLockMode.Locked; // oculta y bloquea el cursor
		Cursor.visible = false;
		//isPaused = false;
		//ManagerDisparo.GetComponent<CameraController>().enabled = false;
	}

	public void Pause()
	{
		pauseMenuUI.SetActive(true);
		settingsMenu.SetActive(false);
		Time.timeScale = 0f; // pausa el tiempo
		Cursor.lockState = CursorLockMode.None; // libera el cursor
		Cursor.visible = true;
		//isPaused = true;
	}
	
	
	public void settings(){
		settingsMenu.SetActive(true);
		pauseMenuUI.SetActive(false);
		Time.timeScale = 0f; // pausa el tiempo
		Cursor.lockState = CursorLockMode.None; // libera el cursor
		Cursor.visible = true;
		//isPaused = true;	
	}
	
	public void exit(){
		if (playerInput != null)
		{
			
			//playerInput.DeactivateInput(); // Más seguro que solo .Disable()
			//playerInput.enabled = false;
			playerInput.actions = null;

		}
		SceneManager.LoadScene("Menu");
	}
}
