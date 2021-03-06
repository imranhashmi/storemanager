﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DataModels;

public class AppController : MonoBehaviour {

	public GameObject panelSplash;
	public GameObject panelLogin;
	public GameObject panelTop;
	public GameObject panelHome;
	public GameObject panelSurveys;
	public GameObject panelTasks;
	public GameObject panelStores;
	public GameObject panelManagers;
	public GameObject panelStats;
	public GameObject panelSidebar;

	public Text topTitle;
	public InputField username;
	public InputField password;

	public ToggleGroup sidebarToggles;
	public Toggle[] tg;

	public static AppController instance;  

	public List<Survey> allSurveys = new List<Survey>();
	public List<Store> allStores = new List<Store>(	);
	public List<Manager> allManagers = new List<Manager>();

	public List<Task> allTasks = new List<Task>();

	void disableAllPanels ()
	{ 
		panelHome.SetActive (false);
		panelSurveys.SetActive (false);
		panelTasks.SetActive (false);
		panelStores.SetActive (false); 
		panelManagers.SetActive (false); 
		panelStats.SetActive (false);
	}

	void Awake(){
		instance = this;

		disableAllPanels ();

		panelSplash.SetActive (false);
		panelLogin.SetActive (false);
		panelTop.SetActive (false);

		//sidebarAnimator.SetTrigger("SlideOut");	// We hide the sidebar in scene before running the app
		sidebarOpened = false; 		
 	}

	// Use this for initialization
	void Start () {

		panelSplash.SetActive (true);
		StartCoroutine( OpenLogin() );

	} 

	// Update is called once per frame
	void Update () {

	}

	IEnumerator OpenLogin()
	{	
		yield return new WaitForSeconds (1f);

		panelSplash.SetActive (false);
		panelLogin.SetActive (true);

		yield break;
	}

	public void OnLoginSubmit()
	{	
		if (username.text.ToLower () == "admin" && password.text.ToLower () == "admin") {

			disableAllPanels ();

			panelLogin.SetActive (false);
			panelTop.SetActive(true);
			panelHome.SetActive (true);
			topTitle.text = "Home";
			tg [0].isOn = true;

		} else {

			// Show a popup message here
			Debug.Log ("Wrong Use\rname/Password !!!");
		} 
	}

	public void OnLogout()
	{	
		panelTop.SetActive (false);

		sidebarAnimator.SetTrigger("SlideOut");
		sidebarOpened = false;

		sidebarToggles.SetAllTogglesOff();

		username.text = "";
		password.text = "";

		panelLogin.SetActive (true);
	}

	public Animator sidebarAnimator;
 	bool sidebarOpened = false;
	public void ToggleSidebar()
	{
		sidebarOpened = !sidebarOpened;
		sidebarAnimator.SetTrigger(sidebarOpened?"SlideIn":"SlideOut");
	}

	public void ShowHome()
	{
		panelHome.SetActive ( tg[0].isOn );

		if (tg [0].isOn) {
			ToggleSidebar ();
		}
	}

	public void ShowSurveys()
	{		
		panelSurveys.SetActive (tg[1].isOn);

		panelTasks.SetActive ( false );
		TaskGui.Instance.Reset ();

		if (tg [1].isOn) {
			ToggleSidebar ();
			SurveyGui.Instance.Show (allSurveys); 
		} else {
			SurveyGui.Instance.Reset ();		
		}
	}

	// Child of Surveys
	public void ShowTasks(List<Task> currentTasks)
	{
		AppController.instance.allTasks = currentTasks;

		panelSurveys.SetActive ( false );
		SurveyGui.Instance.Reset ();

		panelTasks.SetActive ( true );
		TaskGui.Instance.Show (allTasks);
	}

	public void ShowManagers()
	{ 
		panelManagers.SetActive (tg[2].isOn);

		if (tg [2].isOn) {
			ToggleSidebar ();
			ManagerGui.Instance.Show (allManagers);
		} else
			ManagerGui.Instance.Reset ();
	}

	public void ShowStores()
	{ 
		panelStores.SetActive (tg[3].isOn);

		if (tg [3].isOn) {
			ToggleSidebar ();		
			StoreGui.Instance.Show (allStores);
		} else
			StoreGui.Instance.Reset ();
	}

	public void ShowStats()
	{ 
		panelStats.SetActive (tg[4].isOn);

		if (tg [4].isOn) {
			ToggleSidebar ();
		} 
	} 
	  
}