using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskForm : MonoBehaviour {

	public InputField date;
	public InputField title;
	public InputField comments;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable()
	{
		Init ();
	}

	public void Init()
	{
		date.text = System.DateTime.Now.ToString ();
		title.text = "";
		comments.text = "";
	}

	public void Submit()
	{
		if (StoreManager.instance != null)
			StoreManager.instance.AddTask (date.text, title.text, comments.text); 
 	}
	// Update is called once per frame
	void Update () {
		
	}
}
