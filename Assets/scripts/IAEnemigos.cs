using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemigos : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agente;
	private GameObject player;
	private float velEnemigo;
	private float dist;
	private float frecAtaque = 1.5f, tiempoSigAtaque = 0, iniciaConteo;
	public static int vidaEnemigo;
	
	public AudioClip sonidoRevelacion; 
	
	private AudioSource audioSource; // <--- Aquí
	public AudioClip sonidoAparecer; // <--- Aquí (Arrastras el clip desde Unity)
	public AudioClip sonidoAtacar;
	
	public float tiempoEntreRevelaciones = 10f; // <<< cada cuantos segundos revelan su posición
	private float siguienteRevelacion; // <<< contador interno

	
	
	public Hordas hordas;
	
    // Start is called before the first frame update
    void Start()
	{
		player = GameObject.Find("Capsule");
		dist = Vector3.Distance(player.transform.position, transform.position);
		agente.speed = Random.Range(1.0f,5.0f);
		vidaEnemigo = 1;
		
		
		audioSource = GetComponent<AudioSource>(); // Lo agarra automáticamente
        
		if (sonidoAparecer != null)
		{
			audioSource.PlayOneShot(sonidoAparecer);
		}
	}


    // Update is called once per frame
    void Update()
	{
		dist = Vector3.Distance(player.transform.position, transform.position);
		
		if (tiempoSigAtaque > 0)
		{
			tiempoSigAtaque = frecAtaque + iniciaConteo - Time.time;
		} else
		{
			tiempoSigAtaque = 0;
			agente.SetDestination(player.transform.position);
			vidasPlayer.puedePerderVida = 1;
		}
		//if(dist <= 15){
			
		//} 
		if (Time.time >= siguienteRevelacion)
		{
			RevelarPosicion();
			siguienteRevelacion = Time.time + tiempoEntreRevelaciones; // Resetea el timer
		}
        
	}
    
	private void OnTriggerEnter(Collider obj){
		if (obj.tag == "Player")//Daño que le genera el enemigo al player	
		{
			tiempoSigAtaque = frecAtaque;
			iniciaConteo = Time.time;
			obj.transform.GetComponentInChildren<vidasPlayer>().TomarDaño(1);
			if (sonidoAtacar != null)
			{
				audioSource.PlayOneShot(sonidoAtacar);
			}
			
			
			
			
		}
		
	}
	
	public void TomarDaño(int daño){
		vidaEnemigo -= daño;
		if (vidaEnemigo <= 0)
		{
			hordas.enemigosVivos--;
			Destroy(gameObject);
		}
	}
	
	
	private void RevelarPosicion()
	{
		if (sonidoRevelacion != null)
		{
			audioSource.PlayOneShot(sonidoRevelacion);
		}
	}
    
}
