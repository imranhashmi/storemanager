using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

	public class Task
	{
		public int id;
		public string title;
		public string description;
		public string date;
	}

	public static TaskManager instance; 
 
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
