using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunControler : MonoBehaviour
{
	public int maxAmmo = 30;
	public int currentAmmo;
	public float reloadTime = 2f;
	private bool isReloading = false;
	public bool IsReloading => isReloading;

	public Animator gunAnimator; // Asigna el Animator del arma
	public Text ammoText; // UI para mostrar la munición

	void Start()
	{
		currentAmmo = maxAmmo;
		UpdateAmmoUI();
	}

	void Update()
	{
		if (isReloading) return;

		if (currentAmmo <= 0)
		{
			StartCoroutine(Reload());
			return;
		}
		if (Time.timeScale == 0) return; // Evita que se dispare en pausa

		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
	}

	void Shoot()
	{
		currentAmmo--;
		UpdateAmmoUI();
		// Instancia la bala o hace raycast aquí
	}

	IEnumerator Reload()
	{
		if (isReloading) yield break;
		isReloading = true;

		// ACTIVAS el trigger para que se dispare la animación de recarga
		gunAnimator.SetTrigger("Reload");

		// Esperas a que termine la recarga (debería durar lo mismo que la animación)
		yield return new WaitForSeconds(reloadTime);

		currentAmmo = maxAmmo;
		UpdateAmmoUI();

		isReloading = false;
	}

	void UpdateAmmoUI()
	{
		ammoText.text = currentAmmo + " / " + maxAmmo;
	}
}
