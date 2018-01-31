using UnityEngine;
using System.Collections;
using UnityEditor;

public class CustomAnchorMaker {
	
	
	
	[MenuItem("CONTEXT/RectTransform/Set Custom Anchor")]
    static void SetCustomAnchor (MenuCommand command) {
        RectTransform trans = (RectTransform)command.context;
        Undo.RecordObject ( trans, "Custom Anchor");
		trans.SetCustomAnchor();
    }
}
