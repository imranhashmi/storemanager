using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyItem : MonoBehaviour {

	public Toggle toggle;
	public Image bookmark;	
	public Text date;
	public Text title;
	public Text comments;

	// Use this for initialization
	void Start () {

	}
	static Color[] allColors = {
		Color.black,
		Color.blue,
		Color.cyan,
		Color.gray,
		Color.green,
		Color.grey,
		Color.magenta,
		Color.red,
		Color.white,
		Color.yellow,
	};

	static int colorIndex = 0;

	public void Setup(Survey t )
	{

		if (colorIndex < allColors.Length-1)
			colorIndex++;
		else
			colorIndex = 0;

		bookmark.color = allColors [colorIndex]; 

		date.text = t.date;
		title.text = t.id.ToString();
		comments.text = "";//t.manager.title;
	}

	// Update is called once per frame
	void Update () {

	}
}
