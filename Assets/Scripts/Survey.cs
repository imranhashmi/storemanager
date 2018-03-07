using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Survey {

	public int id;
	public string title;
	public string date;
	public string comments;
	public Store store;
	public Manager manager;
	public List<Task> allTasks;

	public Survey(){
		store = new Store();
		manager = new Manager();
		allTasks = new List<Task>();
	}

}
