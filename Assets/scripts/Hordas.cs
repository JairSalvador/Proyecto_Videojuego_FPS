using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hordas : MonoBehaviour
{
	
	public int enemigosVivos;
	public int numRonda;
	public GameObject[] puntoDeSpawn;
	public GameObject prefabEnemigo;
    // Start is called before the first frame update
    void Start()
    {
	    numRonda = 0;
	    
    }

    // Update is called once per frame
    void Update()
	{
		if(enemigosVivos == 0){
			numRonda++;
			siguienteOleada(numRonda);
		}
        
	}
    
	private void siguienteOleada(int Ronda){
		for(int i = 0; i< Ronda*5; i++){
			int randomPos = Random.Range(0, puntoDeSpawn.Length);
			GameObject puntoEmision = puntoDeSpawn[randomPos];
			GameObject instanciaEnemigo = Instantiate(prefabEnemigo, puntoEmision.transform.position, Quaternion.identity);
			instanciaEnemigo.GetComponent<IAEnemigos>().hordas = GetComponent<Hordas>();
			enemigosVivos++;
		}
		
	}
}
