using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DataModels;

public class BasicListGui<T>: MonoBehaviour {

	private static BasicListGui<T> instance = null;
	public static BasicListGui<T> Instance {
		get { return instance; }
 	}

 	public RectTransform contentRect;
	public GameObject prefab;
	public BasicForm form;

	public Button addButton;
	public Button removeButton;

	void Awake()
	{
		instance = this;
		addButton.gameObject.SetActive (false);
		removeButton.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	} 

	IEnumerator fillList()
	{		
		foreach ( T s in allItems) { 
			GameObject go = (GameObject)GameObject.Instantiate (prefab);
			go.transform.SetParent (contentRect.transform);
			go.name = s.GetType().ToString() + " " + contentRect.childCount.ToString ();
			go.transform.localScale = new Vector3 (1f, 1f, 1f);
			go.SetActive (true);

//			if (s.title.IsNullOrEmpty ())
//				s.title = go.name;

			BasicItem<T> item = go.GetComponent<BasicItem<T>> ();
			item.Setup(s); 

			updateContentRect ();

			yield return new WaitForEndOfFrame ();
		}
	}

	protected void updateContentRect()
	{
		RectTransform t = contentRect.GetComponent<RectTransform>();
		float contentSizeY = contentRect.childCount * prefab.GetComponent<RectTransform> ().sizeDelta.y;
		t.sizeDelta = new Vector2(t.sizeDelta.x, contentSizeY);
	} 

	List<T> allItems = null;
	public void Show(List<T> tItems)
	{ 
		allItems = new List<T>(tItems);

		if (allItems.Count == 0) {
			ShowForm();
		}
		else {
			form.gameObject.SetActive (false);
			addButton.gameObject.SetActive (true);
			removeButton.gameObject.SetActive (false); 
			Reset ();
			StartCoroutine ("fillList");
		}
	}

	public void ShowForm()
	{
		form.gameObject.SetActive (true);
		addButton.gameObject.SetActive (false);
		removeButton.gameObject.SetActive (true);
	}

	public void Reset()
	{
		StopCoroutine ("fillList");
		contentRect.transform.ClearChildren ();
	}

	public virtual void AddItem(string date, string title, string comments)
	{
		
	}

	public virtual void DeleteItem()
	{
		
	} 

	public virtual void ItemSelected()
	{
		
	}
}
