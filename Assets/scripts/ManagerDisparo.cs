using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Photon.Pun;

[RequireComponent(typeof(LineRenderer))]

public class ManagerDisparo : MonoBehaviour
{
	
	public bool puedeDisparar = true;
	public Camera playerCamera;
	public AudioSource gunshotAudio;
	public AudioSource pistolAudio;
	
	
	//public Transform origenRayo;
	public GameObject objPistola, objRifle;
	
	public Arma pistola;
	public Arma rifle;
	
	[SerializeField] private float rango;
	[SerializeField] private float frecuenciaDisparo;
	[SerializeField] private int dañoCausado;
	[SerializeField] private int capacidadArma;
	
	
	//private float rango = 77.7f;
	public float duracion = 0.1f; //Duracion de disparo
	//public float frecDisparo = 0.25f;
	private float tiempoDisparo;
	//private LineRenderer rayoLaser;
	
	//private int dañoCausado = 1;
	
	[SerializeField]private LayerMask lMask, otro;
	
	public ParticleSystem particulasDisparo;
	public GameObject impacto;
	
	public static int puntosPlayer;
	public GameObject pistolaImg;
	public GameObject rifleImg;
	public GunControler gunController;
	
	//public PhotonView pv;

	// Awake is called when the script instance is being loaded.
	private void Awake()
	{
		//rayoLaser = GetComponent<LineRenderer>();
		pistola = new Arma(30f, 1.0f, 1,5);
		rifle = new Arma(50f, 0.25f, 2, 10);
		
		ocultarArmas();
		objPistola.SetActive(true);
		
		puntosPlayer = 0;

	}
    
    void Update()
	{
		if (Time.timeScale == 0) return;
		
		cambiaArma();
		tiempoDisparo += Time.deltaTime;
		if(Input.GetButtonDown("Fire1" ) && tiempoDisparo > frecuenciaDisparo && puedeDisparar){
			if (gunController.IsReloading){
				Debug.Log("No debe disparar");
				return;
			}
			StartCoroutine(DisparaCoroutine());
	    }
	}
    
    
    
	private void cambiaArma(){
		if (Input.GetKeyUp(KeyCode.Alpha1))
		{
			ocultarArmas();
			objPistola.SetActive(true);
			pistolaImg.SetActive(true);
			rifleImg.SetActive(false);
			rango = pistola.alcance;
			frecuenciaDisparo = pistola.frecuenciaDisparo;
			dañoCausado = pistola.danoCausado;
			capacidadArma = pistola.capacidad;
			
		}
		if (Input.GetKeyUp(KeyCode.Alpha2))
		{
			ocultarArmas();
			objRifle.SetActive(true);
			pistolaImg.SetActive(false);
			rifleImg.SetActive(true);
			
			rango = rifle.alcance;
			frecuenciaDisparo = rifle.frecuenciaDisparo;
			dañoCausado = rifle.danoCausado;
			capacidadArma = rifle.capacidad;
			
		}
	}
	    
	IEnumerator DisparaCoroutine()
	{
		puedeDisparar = false;
		tiempoDisparo = 0;

		// Reproduce animaciones
		particulasDisparo.Play();
		gunshotAudio.Play();
		pistolAudio.Play();

		// Tiempo de espera durante la animación de disparo (ajústalo según la duración real)
		yield return new WaitForSeconds(0.25f);

		Vector3 origen = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;
		if (Physics.Raycast(origen, playerCamera.transform.forward, out hit, rango, lMask))
		{
			GameObject objImpacto = Instantiate(impacto, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(objImpacto, 1.2f);

			IAEnemigos enemigo = hit.transform.GetComponent<IAEnemigos>();
			if (enemigo != null)
			{
				enemigo.TomarDaño(dañoCausado);
				puntosPlayer++;
			}

			Destroy(hit.transform.gameObject);
		}
		else if (Physics.Raycast(origen, playerCamera.transform.forward, out hit, rango, otro))
		{
			if (hit.rigidbody != null)
			{
				hit.rigidbody.AddForce(hit.normal * 70.0f);
			}

			GameObject objImpacto = Instantiate(impacto, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(objImpacto, 1.2f);
		}

		puedeDisparar = true;
	}

	
	private void ocultarArmas(){
		objPistola.SetActive(false);
		objRifle.SetActive(false);
	}
	
}

