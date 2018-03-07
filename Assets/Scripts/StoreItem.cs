using UnityEngine;
using System.Collections;

public class StoreItem : BasicItem<Store> { 

	public override void Setup(Store t )
	{
		base.Setup(t);

		date.text = t.manager;
		title.text = t.title;
		comments.text = t.address;
	}

	public override void ConfirmSelected()
	{
		base.ConfirmSelected();

		StoreGui.Instance.ItemSelected ();	
	} 
	 
}
