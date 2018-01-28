using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

	public GameObject Splash;
	public GameObject Login;
	public GameObject TopPanel;
	public GameObject Sidebar;
	public GameObject Home;
	public GameObject Tasks;
	public GameObject Team;
	public GameObject Charts;
	public GameObject Orders;

	public InputField username;
	public InputField password;

	void disableAllPanels ()
	{
		Splash.SetActive (false);
		Login.SetActive (false);
		TopPanel.SetActive (false);
		Sidebar.SetActive (false);
		Home.SetActive (false);
		Tasks.SetActive (false);
		Team.SetActive (false);
		Charts.SetActive (false);
		Orders.SetActive (false);

		sidebarOpened = false;
	}

	void Awake(){

		disableAllPanels ();

	}

	// Use this for initialization
	void Start () {
		
		Splash.SetActive (true);

		StartCoroutine( OpenSplash() );
	} 

	IEnumerator OpenSplash()
	{	

		yield return new WaitForSeconds (2f);

		Splash.SetActive (false);
		Login.SetActive (true);

		yield break;
	}

	public void OnLoginSubmit()
	{	

		if (username.text.ToLower () == "admin" && password.text.ToLower () == "admin") {

			Login.SetActive (false);
			TopPanel.SetActive(true);

			Home.SetActive (true);

		} else {

			Debug.Log ("Wrong Use\rname/Password !!!");
		} 
  	}

	public void OnLogout()
	{	
		disableAllPanels ();

		username.text = "";
		password.text = "";

		Login.SetActive (true);
 	}

	bool sidebarOpened = false;
	public void ToggleSidebar()
	{
		sidebarOpened = !sidebarOpened;
		Sidebar.SetActive (sidebarOpened);
	}

	// Update is called once per frame
	void Update () {
			
	}
}
