using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BasicItem<T> : MonoBehaviour
{
	public Toggle toggle;
	public Image bookmark;	
	public Text date;
	public Text title;
	public Text comments;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

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
	public T item = default(T);

	public virtual void Setup(T t )
	{
		item = t;

		if (colorIndex < allColors.Length-1)
			colorIndex++;
		else
			colorIndex = 0;

		bookmark.color = allColors [colorIndex];  
	}

	public virtual void ConfirmSelected()
	{
		Debug.Log("BasicItem->ConfirmSelected()");
	} 

	public virtual void Edit()
	{
		Debug.Log("BasicItem->Edit()"); 
	}
}

