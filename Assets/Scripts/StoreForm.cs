using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreForm : BasicForm {
	
	public override void Submit()
	{
		base.Submit();

		if (StoreGui.Instance != null)
			StoreGui.Instance.AddItem (date.text, title.text, comments.text);
 	}

}
