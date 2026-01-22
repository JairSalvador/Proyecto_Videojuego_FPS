using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medikit : MonoBehaviour
{
	public int cantidadCura = 2;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			vidasPlayer sistemaVida = other.GetComponent<vidasPlayer>();
			if (sistemaVida != null)
			{
				sistemaVida.Curar(cantidadCura);
				Destroy(gameObject); // Destruye el medikit al recogerlo
			}
		}
	}
}
