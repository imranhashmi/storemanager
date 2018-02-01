using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

	public GameObject panelSplash;
	public GameObject panelLogin;
	public GameObject panelTop;
	public GameObject panelSidebar;
	public GameObject panelHome;
	public GameObject panelSurveys;
	public GameObject panelTasks;
	public GameObject panelStores;
	public GameObject panelManagers;
	public GameObject panelStats;

	public Text topTitle;
	public InputField username;
	public InputField password;

	public GameObject newtaskButton;
	public GameObject removetaskButton;
	public ToggleGroup sidebarToggles;
	public Toggle[] tg;


	public static StoreManager instance;  

	public List<SurveyItem> allSurveys = new List<SurveyItem>();
	public List<StoreItem> allStores = new List<StoreItem>();
	public List<ManagerItem> allManagers = new List<ManagerItem>();

	public List<TaskItem> allTasks = new List<TaskItem>();

	void disableAllPanels ()
	{ 
		panelHome.SetActive (false);
		panelSurveys.SetActive (false);
			panelTasks.SetActive (false);	//
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
		panelSidebar.SetActive (false);
		sidebarOpened = false;
 		newtaskButton.SetActive (false);
		removetaskButton.SetActive (false);
 	}

	// Use this for initialization
	void Start () {

		panelSplash.SetActive (true);
		StartCoroutine( OpenLogin() );

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

			panelTop.SetActive(true);
			panelHome.SetActive (true);
			topTitle.text = "Home";
			//tg [0].isOn = true;

			if ( taskForm.gameObject.activeSelf)
				taskForm.gameObject.SetActive (false);

		} else {

			// Show a popup message here
			Debug.Log ("Wrong Use\rname/Password !!!");
		} 
	}

	public void OnLogout()
	{	
		panelTop.SetActive (false);
		panelSidebar.SetActive (false);
		sidebarOpened = false;

		foreach (Toggle t in tg)
			t.isOn = false;

		username.text = "";
		password.text = "";

		panelLogin.SetActive (true);
	}

	bool sidebarOpened = false;
	public void ToggleSidebar()
	{
		sidebarOpened = !sidebarOpened;
		panelSidebar.SetActive (sidebarOpened);
	}

	public void ShowHome()
	{
		if( tg[0].isOn )
			ToggleSidebar ();

		panelHome.SetActive ( tg[0].isOn );
		topTitle.text = "Home"; 

		if ( taskForm.gameObject.activeSelf)
			taskForm.gameObject.SetActive (false);

		newtaskButton.SetActive (false);
		removetaskButton.SetActive (false);
	}

	public void ShowSurveys()
	{
		if( tg[1].isOn )
			ToggleSidebar ();

		panelSurveys.SetActive (tg[1].isOn);
		topTitle.text = "Surveys";
		if (allSurveys.Count == 0) {
			ShowTaskForm (); 
		}
		else {
			newtaskButton.SetActive (tg [1].isOn);
			removetaskButton.SetActive (false); 
			foreach (SurveyItem t in allSurveys) {
				t.toggle.isOn = false;
			}
		}
	}

	// Child of Surveys
	public void ShowTasks()
	{
		if( tg[0].isOn )
			ToggleSidebar ();

		panelTasks.SetActive (tg[0].isOn);
		topTitle.text = "Tasks";
		if (allTasks.Count == 0) {
			ShowTaskForm (); 
		}
		else {
			newtaskButton.SetActive (tg [0].isOn);
			removetaskButton.SetActive (false); 
			foreach (TaskItem t in allTasks) {
				t.toggle.isOn = false;
			}
 		}
	}

	public void ShowManagers()
	{ 
		if( tg[2].isOn )
			ToggleSidebar ();

		panelManagers.SetActive (tg[2].isOn);
		topTitle.text = "Managers";
		if (allManagers.Count == 0) {
			ShowTaskForm (); 
		}
		else {
			newtaskButton.SetActive (tg [2].isOn);
			removetaskButton.SetActive (false); 
			foreach (ManagerItem t in allManagers) {
				t.toggle.isOn = false;
			}
		}
	}

	public void ShowStores()
	{ 
		if( tg[3].isOn )
			ToggleSidebar ();

		panelStores.SetActive (tg[3].isOn);
		topTitle.text = "Stores";
		if (allStores.Count == 0) {
			ShowTaskForm (); 
		}
		else {
			newtaskButton.SetActive (tg [3].isOn);
			removetaskButton.SetActive (false); 
			foreach (StoreItem t in allStores) {
				t.toggle.isOn = false;
			}
		}
	}

	public void ShowStats()
	{ 
		if( tg[4].isOn )
			ToggleSidebar ();

		panelStats.SetActive (tg[4].isOn);
		topTitle.text = "Stats";

		if ( taskForm.gameObject.activeSelf)
			taskForm.gameObject.SetActive (false);

		newtaskButton.SetActive (false);
		removetaskButton.SetActive (false);
	} 

	public void ShowTaskForm()
	{
		taskForm.gameObject.SetActive (true);
		newtaskButton.SetActive (false);
		removetaskButton.SetActive (true);
	}

	public Transform content;
	public GameObject taskPrefab;
	public TaskForm taskForm;

	void updateTaskContentRect()
	{
		RectTransform t = content.GetComponent<RectTransform>();
		float contentSizeY = (content.childCount - 1) * taskPrefab.GetComponent<RectTransform> ().sizeDelta.y;
		t.sizeDelta = new Vector2(t.sizeDelta.x, contentSizeY);
	}

	public void AddTask( string date, string title, string comments )
	{

		Task task = new Task (){ date = date, title = title, description = comments };

		GameObject go = (GameObject)Instantiate (taskPrefab);
		go.transform.SetParent (taskPrefab.transform.parent);
		go.SetActive (true);
		TaskItem taskItem = go.GetComponent<TaskItem> ();
		taskItem.Setup (task);

		if (allTasks == null) allTasks = new List<TaskItem> ();
		allTasks.Add (taskItem);

		updateTaskContentRect ();

		taskForm.gameObject.SetActive (false);
		newtaskButton.SetActive (true);
		removetaskButton.SetActive (false);
	}

	public void DeleteTask()
	{
		if (taskForm.gameObject.activeSelf) {
			taskForm.gameObject.SetActive (false);
			newtaskButton.SetActive (true);
			removetaskButton.SetActive (false);
		}
		else {
			// Delete any seleted task from list.
			List<TaskItem> toRemove = new List<TaskItem>();
			foreach (TaskItem t in allTasks) {
				if (t.toggle.isOn)
					toRemove.Add (t);
			}

			foreach (TaskItem tt in toRemove) {
				allTasks.Remove (tt);
				DestroyImmediate (tt.gameObject);
			}

			TaskSelected ();
		}
	} 

	public void TaskSelected()
	{
		int howManySelected = 0;
		foreach (TaskItem t in allTasks) {
			
			if (t.toggle.isOn)
				howManySelected++;

		}	

		if (howManySelected > 0) {
			newtaskButton.SetActive (false);
			removetaskButton.SetActive (true);
		} else {

			newtaskButton.SetActive (true);
			removetaskButton.SetActive (false);

		}
	}

	// Update is called once per frame
	void Update () {
			
	}
}