using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyForm : BasicForm {

	public override void Submit()
	{
		base.Submit();

		if (SurveyGui.Instance != null)
			SurveyGui.Instance.AddItem (date.text, title.text, comments.text);
	} 
	 
}
