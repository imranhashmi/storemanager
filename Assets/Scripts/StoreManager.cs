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
	public GameObject removetaskButton;
	public ToggleGroup sidebarToggles;
	public Toggle[] tg;


	public static StoreManager instance;
	public List<TaskManager.Task> allTasks = new List<TaskManager.Task>();

	void disableAllPanels ()
	{ 
		Home.SetActive (false);
		Tasks.SetActive (false);
		Team.SetActive (false);
		Stats.SetActive (false);
		Settings.SetActive (false);  
	}

	void Awake(){

		instance = this;

		disableAllPanels ();

		Splash.SetActive (false);
		Login.SetActive (false);
		TopPanel.SetActive (false);
		Sidebar.SetActive (false);
		sidebarOpened = false;
 		newtaskButton.SetActive (false);
		removetaskButton.SetActive (false);
 	}

	public void ShowHome()
	{
		if( tg[0].isOn )
			ToggleSidebar ();

		Home.SetActive ( tg[0].isOn );
		topTitle.text = "Home"; 

		if ( taskForm.gameObject.activeSelf)
			taskForm.gameObject.SetActive (false);

		newtaskButton.SetActive (false);
		removetaskButton.SetActive (false);
	}

	public void ShowTasks()
	{
		if( tg[1].isOn )
			ToggleSidebar ();

		Tasks.SetActive (tg[1].isOn);
		topTitle.text = "Tasks";
		if (allTasks.Count == 0) {
			ShowTaskForm (); 
		}
		else {
			newtaskButton.SetActive (tg [1].isOn);
			removetaskButton.SetActive (false);
		}
	}

	public void ShowTeam()
	{ 
		if( tg[2].isOn )
			ToggleSidebar ();

		Team.SetActive (tg[2].isOn);
		topTitle.text = "Team";

		if ( taskForm.gameObject.activeSelf)
			taskForm.gameObject.SetActive (false);

		newtaskButton.SetActive (false);
		removetaskButton.SetActive (false);
	}

	public void ShowStats()
	{ 
		if( tg[3].isOn )
			ToggleSidebar ();

		Stats.SetActive (tg[3].isOn);
		topTitle.text = "Stats";

		if ( taskForm.gameObject.activeSelf)
			taskForm.gameObject.SetActive (false);

		newtaskButton.SetActive (false);
		removetaskButton.SetActive (false);
	}

	public void ShowSettings()
	{ 
		if( tg[4].isOn )
			ToggleSidebar ();

		Settings.SetActive (tg[4].isOn);
		topTitle.text = "Settings";

		if ( taskForm.gameObject.activeSelf)
			taskForm.gameObject.SetActive (false);

		newtaskButton.SetActive (false);
		removetaskButton.SetActive (false);
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

	public Transform content;
	public GameObject taskPrefab;
	public TaskForm taskForm;


	public void DeleteTask()
	{
		if (taskForm.gameObject.activeSelf) {
			taskForm.gameObject.SetActive (false);
			newtaskButton.SetActive (true);
			removetaskButton.SetActive (false);
		}
		else {
			// Delete any seleted task from list.
		}
	}

	public void ShowTaskForm()
	{
		taskForm.gameObject.SetActive (true);
		newtaskButton.SetActive (false);
		removetaskButton.SetActive (true);
 	}

	void makeNewTaskPanel(TaskManager.Task task)
	{
		GameObject go = (GameObject)Instantiate (taskPrefab);
		go.transform.SetParent (taskPrefab.transform.parent);
		go.SetActive (true);
		go.GetComponent<TaskItem> ().Setup (task);

		updateTaskContentRect ();
	}

	void updateTaskContentRect()
	{
		RectTransform t = content.GetComponent<RectTransform>();
		float contentSizeY = (content.childCount - 1) * taskPrefab.GetComponent<RectTransform> ().sizeDelta.y;
		t.sizeDelta = new Vector2(t.sizeDelta.x, contentSizeY);
	}

	public void AddTask( string date, string title, string comments )
	{

		TaskManager.Task task = new TaskManager.Task (){ date = date, title = title, description = comments };
		allTasks.Add (task);
		makeNewTaskPanel (task);

 		taskForm.gameObject.SetActive (false);
		newtaskButton.SetActive (true);
		removetaskButton.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
			
	}
}