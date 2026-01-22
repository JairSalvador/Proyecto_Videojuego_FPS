using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolContrroller : MonoBehaviour
{
	public int maxAmmo = 10;
	public int currentAmmo;
	public float reloadTime = 2f;
	private bool isReloading = false;

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
		if (Time.timeScale == 0) return;
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

	System.Collections.IEnumerator Reload()
	{
		isReloading = true;
		gunAnimator.SetTrigger("Reload"); // Asegúrate de tener este trigger en el Animator
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
