using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Survey {

	public int id;
	public string title;
	public string date;
	public Store store;
	public Manager manager;
	public List<Task> allTasks = new List<Task>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
