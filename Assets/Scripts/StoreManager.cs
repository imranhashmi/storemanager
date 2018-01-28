using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

	public GameObject Splash;
	public GameObject Login;
	public GameObject StoreMenu;

	public InputField username;
	public InputField password;

	// Use this for initialization
	void Start () {
		Splash.SetActive (true);
	}

	IEnumerator OnOpenSplash()
	{	
		
		Splash.SetActive (false);
		Login.SetActive (true);

		yield break;
	}

	IEnumerator OnLoginSubmit()
	{	

		if (username.text.ToLower () == "admin" && password.text.ToLower () == "admin") {

			Login.SetActive (false);
			StoreMenu.SetActive(true);

		} else {

			Debug.Log ("Wrong Use\rname/Password !!!");
		}

		Splash.SetActive (false);
		yield break;
	}

	// Update is called once per frame
	void Update () {
			
	}
}
