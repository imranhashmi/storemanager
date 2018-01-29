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
	public GameObject Stats;
	public GameObject Settings;

	public Text topTitle;
	public InputField username;
	public InputField password;

	public GameObject newtaskButton;
	public ToggleGroup sidebarToggles;
	public Toggle[] tg;

	void disableAllPanels ()
	{ 
		Home.SetActive (false);
		Tasks.SetActive (false);
		Team.SetActive (false);
		Stats.SetActive (false);
		Settings.SetActive (false);  
	}

	void Awake(){

		disableAllPanels ();

		Splash.SetActive (false);
		Login.SetActive (false);
		TopPanel.SetActive (false);
		Sidebar.SetActive (false);
		sidebarOpened = false;
		//sidebarToggles.SetAllTogglesOff ();
		newtaskButton.SetActive (false);

 	}

	public void ShowHome()
	{
		Home.SetActive ( tg[0].isOn );
		topTitle.text = "Home";
	}

	public void ShowTasks()
	{
		Tasks.SetActive (tg[1].isOn);
		topTitle.text = "Tasks";
		newtaskButton.SetActive (tg [1].isOn);
	}

	public void ShowTeam()
	{ 
		Team.SetActive (tg[2].isOn);
		topTitle.text = "Team";
	}

	public void ShowStats()
	{ 
		Stats.SetActive (tg[3].isOn);
		topTitle.text = "Stats";
	}

	public void ShowSettings()
	{ 
		Settings.SetActive (tg[4].isOn);
		topTitle.text = "Settings";
	}


	// Use this for initialization
	void Start () {
		
		Splash.SetActive (true);
		StartCoroutine( OpenLogin() );

	} 

	IEnumerator OpenLogin()
	{	
		yield return new WaitForSeconds (1f);

		Splash.SetActive (false);
		Login.SetActive (true);

		yield break;
	}

	public void OnLoginSubmit()
	{	
		if (username.text.ToLower () == "admin" && password.text.ToLower () == "admin") {

			disableAllPanels ();

			TopPanel.SetActive(true);
			Home.SetActive (true);
			topTitle.text = "Home";
			tg [0].isOn = true;

		} else {

			// Show a popup message here
			Debug.Log ("Wrong Use\rname/Password !!!");
		} 
  	}

	public void OnLogout()
	{	
		//disableAllPanels ();
		TopPanel.SetActive (false);
		Sidebar.SetActive (false);
		sidebarOpened = false;

		foreach (Toggle t in tg)
			t.isOn = false;

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


	public void MakeNewTask()
	{



	}
	// Update is called once per frame
	void Update () {
			
	}
}
