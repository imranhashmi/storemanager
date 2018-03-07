using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BasicForm : MonoBehaviour
{ 
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

	public virtual void Submit()
	{
		Debug.Log("Form->Submit");
	}
	// Update is called once per frame
	void Update () {

	}
}

