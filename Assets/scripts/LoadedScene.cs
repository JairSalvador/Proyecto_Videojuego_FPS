using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadedScene : MonoBehaviour
{
	public GameObject loadingPanel;      // Panel de carga (con texto y slider)
	public Slider progressBar;           // Asigna tu Slider desde el Inspector

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadSceneAsync("EscenarioUno"));
	}

	private IEnumerator LoadSceneAsync(string sceneName)
	{
		loadingPanel.SetActive(true);    // Mostrar pantalla de carga
		progressBar.value = 0f;

		yield return new WaitForSeconds(0.5f); // Pequeña pausa opcional

		AsyncOperation operation = SceneManager.LoadSceneAsync("EscenarioUno");
		operation.allowSceneActivation = false;

		while (!operation.isDone)
		{
			// Cálculo del progreso (Unity solo llega hasta 0.9, así que lo normalizamos)
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			progressBar.value = progress;

			if (operation.progress >= 0.9f)
			{
				// Aquí puedes esperar un clic o un segundo extra antes de activar la escena
				yield return new WaitForSeconds(0.5f);
				operation.allowSceneActivation = true;
			}

			yield return null;
		}
	}
}