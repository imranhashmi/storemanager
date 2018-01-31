using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;
using UnityEditor;
namespace UI
{
	public class ArabicTextEditor
	{
		
		static string time_elapsed_string(float seconds)
		{
				long etime = (long)(seconds);

				if (etime < 1)
				{
						return "Now";
				}

				long[] durations  = new long[]{ 12 * 30 * 24 * 60 * 60 ,
						30 * 24 * 60 * 60,
						24 * 60 * 60     ,
						60 * 60          ,
						60               ,
						1                
				};

				string[] durationsNames  = new string[]{ "Y",
						"M",
						"D",
						"h",
						"m",
						"s"
				};
				//if (etime<60)
				//return "Now";

				for (int i=0;i<durations.Length;i++)
				{
						float d = etime / durations[i];
						if (d >= 1)
						{
								int r = Mathf.RoundToInt(d);
								return r + " " + durationsNames[i];
						}
				}
				return  " not sure ";
		}

		public static bool ProgrssProgressBar( string title, string info, float progress, float startTime)
		{
				float elapsedTime = Time.realtimeSinceStartup - startTime;
				float remainingTime = progress <=0 ? -1 : (1f - progress) / progress * elapsedTime;
				return(EditorUtility.DisplayCancelableProgressBar (string.Format ("{0} {1}% E:{2} R:{3}", title, (progress * 100).ToString("0.0"), time_elapsed_string(elapsedTime), time_elapsed_string(remainingTime)), info, progress));
		}

		[MenuItem("Tools/Fix Bold")]
		static void FixAllBold(MenuCommand command)
		{
			Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Resources/Fonts/arialbd.ttf");
			Text[] texts = UnityEngine.Resources.FindObjectsOfTypeAll<Text>() ;
			float t = Time.realtimeSinceStartup;
			for( int i=0; i<texts.Length;i++)
			{	
			 Text text = texts[i];
				if( ProgrssProgressBar("Arbizing Texts","Arbizing : " + UTILES.GetPath( text.gameObject),(float)i/ texts.Length, t ))
				{
						break;
				}

			if ((text.fontStyle  == FontStyle.Bold) ||
				 (text.fontStyle  == FontStyle.BoldAndItalic))
					{
						text.font = font;
						text.setDirty();
					}
			}
			EditorUtility.ClearProgressBar();
		}

		[MenuItem("Tools/Arabize All")]
		static void ArabizeAll(MenuCommand command)
		{
			Text[] texts = UnityEngine.Resources.FindObjectsOfTypeAll<Text>() ;
			float t = Time.realtimeSinceStartup;
			for( int i=0; i<texts.Length;i++)
			{	
			 Text text = texts[i];
				if( ProgrssProgressBar("Arbizing Texts","Arbizing : " + UTILES.GetPath( text.gameObject),(float)i/ texts.Length, t ))
				{
						break;
				}

			if ((text.GetType().ToString() == "UnityEngine.UI.Text") &&
				 (AssetDatabase.GetAssetPath(text).IsNullOrEmpty()))
					ArabizeText( text);
			}
			EditorUtility.ClearProgressBar();
		}
		[MenuItem("CONTEXT/Text/Arabize")]
        static void DoArabizeText(MenuCommand command)
        {
            ArabizeText((Text)command.context);
        }
		[MenuItem("CONTEXT/ArabicText/DeArabize")]
		static void DoDeArabizeText(MenuCommand command)
		{
			DeArabizeText((ArabicText)command.context);
		}
        [MenuItem("CONTEXT/InputField/Arabize")]
        static void DoArabizeInput(MenuCommand command)
        {
            InputField inputField = (InputField)command.context;
            if (inputField.textComponent)
                inputField.textComponent = ArabizeText(inputField.textComponent);
            if (inputField.placeholder is Text)
                ArabizeText(inputField.placeholder as Text);
        }
        static Text ArabizeText(Text text)
        {
            var fields = REF.GetGameObjectsAllFields(text);
            GameObject tempGo = new GameObject("TempTextContainer");
            ArabicText tempArabicText = tempGo.AddComponent<ArabicText>();
            EditorUtility.CopySerialized(text, tempArabicText);
            GameObject textGo = text.gameObject;
            UnityEngine.Object.DestroyImmediate(text);
            ArabicText arabicText = textGo.AddComponent<ArabicText>();
            EditorUtility.CopySerialized(tempArabicText, arabicText);
			foreach( var field in fields)
			{
				field.fieldInfo.SetValue( field.component, arabicText);
				field.component.setDirty();
				field.component.gameObject.setDirty();
			}
            UnityEngine.Object.DestroyImmediate(tempGo);
            return arabicText;
        }
		static Text DeArabizeText(ArabicText text)
		{
			var fields = REF.GetGameObjectsAllFields(text);
			GameObject tempGo = new GameObject("TempTextContainer");
			Text tempArabicText = tempGo.AddComponent<Text>();
			EditorUtility.CopySerialized(text, tempArabicText);
			GameObject textGo = text.gameObject;
			UnityEngine.Object.DestroyImmediate(text);
			Text arabicText = textGo.AddComponent<Text>();
			EditorUtility.CopySerialized(tempArabicText, arabicText);
			foreach( var field in fields)
			{
				field.fieldInfo.SetValue( field.component, arabicText);
				field.component.setDirty();
				field.component.gameObject.setDirty();
			}
			UnityEngine.Object.DestroyImmediate(tempGo);
			return arabicText;
		}
	}
}
