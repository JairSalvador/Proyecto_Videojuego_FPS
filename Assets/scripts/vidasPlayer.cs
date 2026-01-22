using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class vidasPlayer : MonoBehaviour
{
	
	public Image vidas;
	private float anchoVidas;
	public static int vida;
	private bool haMuerto;
	public GameObject gameOver;
	private const int vidasInc = 6;
	public static int puedePerderVida = 1;
	public Text txtPuntos;
	public Text txtRecord;
	public Text nombreR;

	public GameObject fm;
	public PlayerInput playerInput;

	
	
    // Start is called before the first frame update
    void Start()
	{
		anchoVidas = vidas.GetComponent<RectTransform>().sizeDelta.x;
		haMuerto = false;
		vida = vidasInc;
		gameOver.SetActive(false);
		txtRecord.text = "Record: " + FileManagent.record.ToString();
		nombreR.text = FileManagent.nombreR;
		//playerInput = GetComponent<PlayerInput>();
	}
    
	private void Update(){
		txtPuntos.text = "Puntos: " + ManagerDisparo.puntosPlayer.ToString();
	}

	public void TomarDaño(int daño){
		if (vida > 0 && puedePerderVida == 1){
			puedePerderVida = 0;
			vida -= daño;
			dibujaVida(vida);
			
		}
		if (vida <=0 &&  !haMuerto){
			haMuerto = true;
			if (ManagerDisparo.puntosPlayer > FileManagent.record)
			{
				Debug.Log("Si es mayor el record");
				fm.GetComponent<FileManagent>().SaveFile();
			}
			StartCoroutine(ejecutaMuerte());
			
			Debug.Log("Muerto");
		}
	}
	
	private void dibujaVida(int vida){
		RectTransform transformaImagen = vidas.GetComponent<RectTransform>();
		transformaImagen.sizeDelta = new Vector2(anchoVidas*(float)vida/(float)vidasInc,transformaImagen.sizeDelta.y);
	}
	
	
	IEnumerator ejecutaMuerte(){
		yield return new WaitForSeconds(1.2f);
		//var input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
		if (playerInput != null)
		{
			
			//playerInput.DeactivateInput(); // Más seguro que solo .Disable()
			//playerInput.enabled = false;
			playerInput.actions = null;

		}
		gameOver.SetActive(true);
		yield return null;
		SceneManager.LoadScene("Menu");
	}
	
	public void Curar(int cantidad)
	{
		if (vida < vidasInc && !haMuerto)
		{
			vida += cantidad;
			if (vida > vidasInc) vida = vidasInc; // No exceder el máximo
			Debug.Log("Curado: nueva vida = " + vida);
			dibujaVida(vida);
		}
	}
	
}
