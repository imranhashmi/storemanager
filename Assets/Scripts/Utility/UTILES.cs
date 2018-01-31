using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine.UI;
		
public static class UTILES  { 

	public static void Shuffle<T>(this IList<T> list, System.Random rnd)
	{
		for(var i=0; i < list.Count; i++)
			list.Swap(i, rnd.Next(i, list.Count));
	}

	public static void Swap<T>(this IList<T> list, int i, int j)
	{
		var temp = list[i];
		list[i] = list[j];
		list[j] = temp;
	}

	// Shuffle a list based on 	Fisher-Yates shuffle algorithm
	public static void Shuffle<T>(this IList<T> list)  
	{  
		System.Random rng = new System.Random();  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
	public static int Intersected<T>(this List<T> list, List<T> targetList)
	{
		for (int i = 0; i < list.Count; i++)
		{
			int index = targetList.IndexOf (list [i]);
			if (index > -1)
				return index;
				
		}
		return -1;
	}

	public static T[] Populate<T>(this T[] array, Func<T> provider)
	{
	    for (int i = 0; i < array.Length; i++)
	    {
	        array[i] = provider();
	    }
	    return array;
	}		
	public static bool ValidIndex<T>(this T[] array, int index)
	{
	    return index >= 0 && index < array.Length;
	}		
	
	public static bool Find<T>(this T[] array, T obj)
	{
	    for (int i = 0; i < array.Length; i++)
	    {
	        if (array[i].Equals( obj))
				return true;
	    }
	    return false;
	}		
	
	public static T GetMember<T>(this T[] array, string objName) where T : UnityEngine.Object
	{
	    for (int i = 0; i < array.Length; i++)
	    {
	        if (array[i].name.Equals( objName))
				return array[i];
	    }
	    return null;
	}		
	
	public static int IndexOf<T>(this T[] array, T obj)
	{
	    for (int i = 0; i < array.Length; i++)
	    {
	        if (array[i].Equals( obj))
				return i;
	    }
	    return -1;
	}		
	
	public static void SetEnabled(this MonoBehaviour[] array, bool val)
	{
	    for (int i = 0; i < array.Length; i++)
	    {
			if (array[i])
	        	array[i].enabled = val;
	    }
	}		
	
	public static string toString(this object[] array,string divider = " ")
	{
		string res = "";
	    for (int i = 0; i < array.Length; i++)
	    {
	        res += " ["+i+"]   "+array[i].ToString()+divider;
	    }
	    return res;
	}		
	
	public static string ToText(this object[] array)
	{
		string res = "";
	    for (int i = 0; i < array.Length; i++)
	    {
	        res += (i>0 ? "\n":"" )+ array[i].ToString();
	    }
	    return res;
	}		
	
	public static bool IsArabic(this string inStr)
	{
		char[] chars = inStr.ToCharArray();
		foreach(char ch in chars)
			if(ch >= '\u0627' && ch <= '\u0649')
				return true;
		return false;
	}
	
	public static string ToArabic(this string inStr)
	{
	    return CharSetControl.fixArabicSymbols(inStr);
	}		
	
	public static string FlipWords(this string inStr)
	{
		string[] words = inStr.Trim().Split(' ');
		for (int i = 0; i < words.Length/2; i++)
		{
			int iIn = words.Length-1-i;
			string tempStr = words[i];
			words[i] = words[iIn];
			words[iIn] = tempStr;
		}
		return string.Join(" ", words);
	}		

	public static void enableChildrenByName(this Transform go, bool val, string cName) {
		foreach(Transform trans in go)
		{
			if (trans.name == cName)
			{
				trans.gameObject.SetActive( val);
			}
			else
				trans.enableChildrenByName( val,  cName);
		}
	}
	
	public static void enableByNameContain(this Transform go, bool val, string cName) {
		foreach(Transform trans in go)
		{
			if (trans.name.Contains( cName))
			{
				trans.gameObject.SetActive( val);
			}
			else
				trans.enableChildrenByName( val,  cName);
		}
	}
	public static void enableByType<T>(this Transform go, bool val) where T : Component  {
		foreach(Transform trans in go)
		{
			if (trans.GetComponent<T>())
			{
				trans.gameObject.SetActive( val);
			}
			else
				trans.enableByType<T>( val);
		}
	}
	
	public static T findChildByName<T>(this Transform go, string cName) where T : Component 
	{
		T res = null;
		foreach(Transform trans in go)
		{
			if (trans.name == cName)
			{
				res = trans.GetComponent<T>();
				if (res)
					return res;
			}
			res = trans.findChildByName<T>( cName);
			if (res)
				return res;
		}
		return null;
	}
	
	public static void GetComponentsBy<T>(this Transform go,ref List<T> result) where T : Component 
	{
		T res = go.GetComponent<T>();
		if (res)
			result.Add( res);
		foreach(Transform trans in go)
		{
			trans.GetComponentsBy<T>(ref result);
		}
	}
	
	public static T GetComponentBy<T>(this Transform go) where T : Component 
	{
		T res = go.GetComponent<T>();
		if (res)
			return res;
		foreach(Transform trans in go)
		{
			res = trans.GetComponentBy<T>();
			if (res)
				return res;
		}
		return res;
	}
	
	public static T[] GetComponentsBy<T>(this Transform go) where T : Component 
	{
		List<T> result = new List<T>();
		go.GetComponentsBy<T>(ref result);
		return result.ToArray();
	}
	
	public static T findChildByName<T>(this GameObject go, string cName) where T : Component 
	{
		return go.transform.findChildByName<T>(cName);
	}
	
	public static T findObjectByName<T>(string cName) where T : Component 
	{
		T[] list = GameObject.FindObjectsOfType(typeof(T)) as T[];
		foreach( T o in list)
			if ( cName.Equals( o.name))
				return o;
		return null;
	}
	
	public static T findChildByName<T>(this MonoBehaviour go, string cName) where T : Component 
	{
		return go.transform.findChildByName<T>(cName);
	}
	
	public static string GetPath(this Transform current) {
	    if (current.parent == null)
	       return "/" +current.name;
	    return current.parent.GetPath() + "/" + current.name;
	}
	
	public static string GetPathWithoutRoot(this Transform current) {
	    if (current.parent == null)
	       return "";
		string pPath = current.parent.GetPathWithoutRoot();
		if (pPath.NotNullOrEmpty())
			pPath += "/";
	    return pPath + current.name;
	}
	
	public static string GetPath(this GameObject current) {
	    return current.transform.GetPath();
	}
	
	public static string GetPathWithoutRoot(this GameObject current) {
	    return current.transform.GetPathWithoutRoot();
	}
	
	public static void AddAsRoot(this GameObject go, GameObject root) {
		go.transform.parent = root.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale    = Vector3.one;		
	}
	
    public static Transform Find(string fullPathName)
    {
        GameObject [] objs = Resources.FindObjectsOfTypeAll(typeof( GameObject)) as GameObject[];
		string insfullpath = fullPathName.Insert( 1, "_inst_");
 
        foreach (GameObject obj in objs)
        {
            if (obj.GetPath().Equals( insfullpath))
            {
                return obj.transform;
            }
        } 
 
        foreach (GameObject obj in objs)
        {
            if (obj.GetPath().Equals( fullPathName))
            {
                return obj.transform;
            }
        } 
        return null;
    }
	
    public static GameObject FindGO(string fullPathName)
    {
		Transform res = Find( fullPathName);
		return (res == null) ? null : res.gameObject;
	}
	
	public static void ClearChildren(this Transform go)
	{
		List<Transform> children = new List<Transform>();
		foreach(Transform trans in go)
		{
			children.Add(trans);			
		}
		foreach(Transform trans in children)
		{
			UnityEngine.Object.DestroyImmediate(trans.gameObject,true);
		}
	}
	
	public static T GetParent<T>(this Transform go) where T : Component 
	{
		Transform parent = go.parent;
		while(parent)
		{
			T res = parent.GetComponent<T>();
			if (res)
				return ( res);
			parent = parent.parent;
		}
        return null;
	}
	
	public static T GetParent<T>(this GameObject go) where T : Component
	{
		return go.transform.GetParent<T>();
	}
	
	public static bool isAncector(this Transform go, Transform parent)
	{
		if (go.parent == null)
			return false;
		if (go.parent == parent)
			return true;
		return go.parent.isAncector(parent);
	}
	
	public static bool isAncector(this GameObject go, Transform parent)
	{
		return go.transform.isAncector(parent);
	}
	
	public static bool isAncector(this MonoBehaviour go, Transform parent)
	{
		return go.transform.isAncector(parent);
	}
	
	static void findAllComponents<T>(this Transform go, ref List<T> res) where T : Component 
	{
		T c = go.GetComponent<T>();
		if (c)
			res.Add(c);
		foreach(Transform trans in go)
		{
			trans.findAllComponents<T>(ref res);
		}
	}
	
	public static T GetComponetInParent<T>(this Transform go) where T : Component 
	{
		T res = go.GetComponent<T>();
		if (res || go.parent == null)
			return res;
		return go.parent.GetComponetInParent<T>();
	}
	
	public static T[] getAllComponents<T>(this Transform go) where T : Component 
	{
		List<T> res = new List<T>();
		go.findAllComponents<T>(ref res);
		return res.ToArray();
	}
	
	public static T[] getAllComponents<T>(this GameObject go) where T : Component 
	{
		return go.transform.getAllComponents<T>();
	}
	
	public static T[] getAllComponents<T>(this MonoBehaviour go) where T : Component 
	{
		return go.transform.getAllComponents<T>();
	}
	
	public static GameObject findChildByName(this GameObject go, string name)
	{
		if (go == null)
		{
			return null;
		}

		foreach (Transform child in go.transform)
		{
				if (child.name == name)
				{
						return child.gameObject;
				}
				GameObject res = child.gameObject.findChildByName( name);
				if(res != null)
				{
					return res;
				}
		}
		return null;
	}
	
	public static string getStringData(this Texture2D go )
	{
		int iconWidth = go.width ;
		int iconHight = go.height ;
		byte[] byteArray = go.EncodeToPNG();
		string iconData = Convert.ToBase64String(byteArray);
		
		string result = iconWidth.ToString() + "imageW" + iconHight.ToString() + "imageH" + iconData ;
		return result ;
	}
	
	public static void setFromStringData(this Texture2D go , string data )
	{
		if( data == "" )
		{
			Debug.LogError("empty data");
			return;
		}
		
		int wIndex = data.IndexOf("imageW");
		if( wIndex == -1 )
		{
			Debug.LogError("no image width in data");
			return;
		}
		int hIndex = data.IndexOf("imageH");
		if( hIndex == -1 )
		{
			Debug.LogError("no image height in data");
			return;
		}
		
		string wstring = data.Substring(0,wIndex ) ;
		int w = int.Parse( wstring );
		//string hstring = data.Substring( wIndex + 6 , hIndex - wIndex - 6 );
		int h = int.Parse( wstring );
		
		string d = data.Substring( hIndex + 6);
		byte[] byteArray = Convert.FromBase64String( d );
		
		go = new Texture2D( w , h , TextureFormat.RGB24 , false );
		go.LoadImage( byteArray );
	}

	
    public static void SetCompnentEnable<T>(this Component cObject, bool enabled) where T: MonoBehaviour
    {
		if (cObject == null) return;
		cObject.gameObject.SetCompnentEnable<T>(enabled);
	}
	
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(this Component cObject, object message)
    {
		if (cObject == null)
			Debug.LogError( message);
		else
			Debug.LogError( message, cObject);
	}
	
	
    public static void SetCompnentEnable<T>(this GameObject cObject, bool enabled) where T: MonoBehaviour
    {
		if (cObject == null) return;
		T comp = cObject.GetComponent<T>();
		if (comp) comp.enabled = enabled;
	}
	/*public static void loadTexture(this UITextureDynamic go )
	{
		StackToExec.addToLoad(go);	
	}*/

	public static byte[] Serialize( Color[] colors )
	{
		if( colors == null )
			return null;

		ushort len = (ushort)(2 + colors.Length * sizeof(float) * 4) ;
		byte[] data = new byte[ len ];
		try
		{
			byte[] d ;
			d = System.BitConverter.GetBytes( (ushort)colors.Length ) ;
			int shift = 0;
			System.Array.Copy( d , 0 , data , shift , 2 );shift+=2;

			for( int i = 0 ; i < colors.Length ; i++ )
			{
				d = System.BitConverter.GetBytes( colors[i].r ) ;
				System.Array.Copy( d , 0 , data , shift , 4 );shift+=4;
				d = System.BitConverter.GetBytes( colors[i].g ) ;
				System.Array.Copy( d , 0 , data , shift , 4 );shift+=4;
				d = System.BitConverter.GetBytes( colors[i].b ) ;
				System.Array.Copy( d , 0 , data , shift , 4 );shift+=4;
				d = System.BitConverter.GetBytes( colors[i].a ) ;
				System.Array.Copy( d , 0 , data , shift , 4 );shift+=4;
			}
		}
		catch( System.Exception e )
		{
			Debug.LogException( e );
		}
		return data ;
	}
	public static Color[] DeserializeColor(byte[] data )
	{
		Color[] colors = null;
		try
		{
			int shift = 0;
			ushort count = System.BitConverter.ToUInt16( data ,shift);
			shift+=2;
			colors = new Color[count];
			for( int i = 0 ; i < count ; i++ )
			{
				colors[i].r = System.BitConverter.ToSingle( data , shift );shift+=4;
				colors[i].g = System.BitConverter.ToSingle( data , shift );shift+=4;
				colors[i].b = System.BitConverter.ToSingle( data , shift );shift+=4;
				colors[i].a = System.BitConverter.ToSingle( data , shift );shift+=4;
			}
		}
		catch(System.Exception e )
		{
			Debug.LogException(e);
		}
		return colors;
	}
	
	public static T Clone<T> (this T obj) where T : UnityEngine.Object
	{
		var res = UnityEngine.Object.Instantiate(obj) as T;
		res.name = obj.name;
		return res;
	}
	
	
	public static GameObject CloneInPlace (this GameObject obj)
	{
		GameObject res = obj.Clone();
		res.transform.SetParent(obj.transform.parent);
		res.transform.localPosition = obj.transform.localPosition;
		res.transform.localRotation = obj.transform.localRotation;
		res.transform.localScale = obj.transform.localScale;
		RectTransform rect = res.GetComponent<RectTransform>();
		if (rect)
		{			
			RectTransform rect1 = obj.GetComponent<RectTransform>();
			rect.anchoredPosition = rect1.anchoredPosition;
			rect.sizeDelta = rect1.sizeDelta;
			rect.anchorMin = rect1.anchorMin;
			rect.anchorMax = rect1.anchorMax;
		}
		return res;
	}
	
	public static GameObject CloneWithParent(this GameObject obj, Transform parent, Vector3 position, Quaternion rotation)
	{
		GameObject res = obj.Clone();
		res.transform.SetParent(parent);
		res.transform.position = position;
		res.transform.rotation = rotation;
		res.transform.localScale = obj.transform.localScale;
		return res;
	}
	
	public static GameObject CloneAtTrans (this GameObject obj, Transform target)
	{
		GameObject res = obj.Clone();
		res.transform.SetParent(obj.transform.parent);
		res.transform.position = target.position;
		res.transform.rotation = target.rotation;
		res.transform.localScale = obj.transform.localScale;
		return res;
	}
	
	public static T CloneInPlace<T> (this T obj) where T : UnityEngine.Component
	{
		GameObject res = obj.gameObject.CloneInPlace();
		return res.GetComponent<T>();
	}
	
	public static RectTransform GetRect<T> (this T obj) where T : UnityEngine.Component
	{
		return obj.GetComponent<RectTransform>();
	}
	
	public static RectTransform GetRect (this GameObject obj)
	{
		return obj.GetComponent<RectTransform>();
	}
		
	public static Graphic GetGraphic<T> (this T obj) where T : UnityEngine.Component
	{
		return obj.GetComponent<Graphic>();
	}
	
	public static string ToGUIHex(this Color color)
	{
		Color32 clr = color;
		return clr.ToGUIHex();
	}
	
	public static string ToGUIHex(this Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
		return "#"+hex;
	}
	
	public static bool Between(this int val, int min, int max)
	{
		return  (val >= min && val <= max);
	}
	
	public static bool Between(this float val, float min, float max)
	{
		return  (val >= min && val <= max);
	}
	
	public static bool Between(this Vector2 val, float min, float max)
	{
		return  val.x.Between(min,max) && val.y.Between(min,max);
	}

	public static bool Between(this Vector3 val, float min, float max)
	{
		return  val.x.Between(min,max) && val.y.Between(min,max);
	}

	public static string Coloring(this string text, Color32 color)
	{
		if (string.IsNullOrEmpty(text))
			return text;
		return  string.Format( "<color={1}>{0}</color>", text, ToGUIHex(color));
	}
	
	public static string Styling(this string text, FontStyle fontStyle)
	{
		if (string.IsNullOrEmpty(text) || fontStyle == FontStyle.Normal)
			return text;
		switch (fontStyle)
		{
		case FontStyle.Bold : return  string.Format( "<b>{0}</b>", text);
		case FontStyle.BoldAndItalic : return  string.Format( "<b><i>{0}</i></b>", text);
		case FontStyle.Italic : return  string.Format( "<i>{0}</i>", text);
		default: return text;
		}

	}

	public static string Styling(this string text, bool bold, bool italic)
	{
		if (string.IsNullOrEmpty(text))
			return text;
		return text.Styling(bold? (italic ? FontStyle.BoldAndItalic : FontStyle.Bold):(italic ? FontStyle.Italic : FontStyle.Normal));
	}

	public static string Styling(this string text, Color color, bool bold, bool italic)
	{
		if (string.IsNullOrEmpty(text))
			return text;
		return text.Coloring(color).Styling(bold? (italic ? FontStyle.BoldAndItalic : FontStyle.Bold):(italic ? FontStyle.Italic : FontStyle.Normal));
	}
	
	public static float GetAlpha(this Graphic cObject)
	{
		return cObject.color.a;
	}
	
	
	public static void SetAlpha(this Graphic cObject, float alpha)
	{
		Color clr = cObject.color; clr.a = alpha;
		cObject.color = clr;
	}

	public static bool NotNullOrEmpty(this string text)
	{
		return !(string.IsNullOrEmpty(text));
	}
	
	public static bool IsNullOrEmpty(this string text)
	{
		return string.IsNullOrEmpty(text);
	}
	
	public static int ToInt(this float val)
	{
		return Mathf.RoundToInt( val);
	}

	public static int ToInt(this double val)
	{
		return Mathf.RoundToInt( (float)val);
	}

	public static int ToInt(this bool val)
	{
		return val ? 1 : 0;
	}
	
	public static float Frec(this float val)
	{
		return val - ((int)val);
	}
	public static float Clamp01(this float val)
	{
		return Mathf.Clamp01( val);
	}
	
	public static int ToInt(this string text, int defVal = 0)
	{
		int res = defVal;
		if (int.TryParse( text, out res))
			return res;
		return defVal;
	}
	
	public static long ToLong(this string text, long defVal = 0)
	{
		long res = defVal;
		if (long.TryParse( text, out res))
			return res;
		return defVal;
	}
	
	public static void Replace<T>(this List<T> list, T oldItem, T newItem)
	{
		int index = list.IndexOf( oldItem); 
		if (index<0 || index > list.Count-1) 
		{
			Debug.LogError("index out of the range" + index);
			return;
		}
		list[index] = newItem;
	}
	
	public static T Popup<T>(this List<T> list, int index)
	{
		if (index<0 || index > list.Count-1) 
		{
			Debug.LogError("index out of the range" + index);
			return default(T);
		}
		T item = list[index];
		list.RemoveAt(index);
		return item;
	}
	
	public static T PopupLast<T>(this List<T> list)
	{
		int lastIndex = list.Count-1;
		if (lastIndex<0) 
		{
			Debug.LogError("list is empty");
			return default(T);
		}
		return list.Popup(lastIndex);
	}
	
	public static T PopupFirst<T>(this List<T> list)
	{
		if (list.Count==0) 
		{
			Debug.LogError("list is empty");
			return default(T);
		}
		return list.Popup(0);
	}

	public static T FirstItem<T>(this List<T> list)
	{
		if (list.Count == 0) 
		{
			Debug.LogError("list is empty");
			return default(T);
		}
		return list[0];
	}
	public static T LastItem<T>(this List<T> list)
	{
		int lastIndex = list.Count-1;
		if (lastIndex<0) 
		{
			Debug.LogError("list is empty");
			return default(T);
		}
		return list[lastIndex];
	}
	
	internal static Bounds InternalGetBounds (Vector3[] corners, ref Matrix4x4 viewWorldToLocalMatrix)
	{
		Vector3 vector = new Vector3 (3.402823E+38f, 3.402823E+38f, 3.402823E+38f);
		Vector3 vector2 = new Vector3 (-3.402823E+38f, -3.402823E+38f, -3.402823E+38f);
		for (int i = 0; i < 4; i++) {
			Vector3 vector3 = viewWorldToLocalMatrix.MultiplyPoint3x4 (corners [i]);
			vector = Vector3.Min (vector3, vector);
			vector2 = Vector3.Max (vector3, vector2);
		}
		Bounds result = new Bounds (vector, Vector3.zero);
		result.Encapsulate (vector2);
		return result;
	}

	public static Bounds GetBounds (this RectTransform trans)
	{
		Vector3[] m_Corners = new Vector3[4];
		Bounds result;
		trans.GetWorldCorners (m_Corners);
		Matrix4x4 worldToLocalMatrix = trans.worldToLocalMatrix ;
		result = InternalGetBounds (m_Corners, ref worldToLocalMatrix);
		return result;
	}

	public static Vector2 GetAchoredCenter(this RectTransform trans, out Vector2 center)
	{
        RectTransform parent = trans.parent.GetRect();
		
		Vector3[] fourCornersArray1 = new Vector3[4];
		parent.GetWorldCorners( fourCornersArray1);
		float width1 = fourCornersArray1[2].x - fourCornersArray1[0].x;
		float height1 = fourCornersArray1[2].y - fourCornersArray1[0].y;
		
		Vector3[] fourCornersArray = new Vector3[4];
		trans.GetWorldCorners( fourCornersArray);
		
		center = (fourCornersArray[2] + fourCornersArray[0]) * 0.5f;
		
		fourCornersArray[0] -= fourCornersArray1[0];
		fourCornersArray[2] -= fourCornersArray1[0];
		
		
		fourCornersArray[0].x /= width1;
		fourCornersArray[0].y /= height1;
		fourCornersArray[2].x /= width1;
		fourCornersArray[2].y /= height1;
		
		return (fourCornersArray[0] + fourCornersArray[2])*0.5f;
	}
	
	
    public static bool IsVisible (this RectTransform trans, RectTransform parent) {
		
		Vector3[] fourCornersArray1 = new Vector3[4];
		parent.GetWorldCorners( fourCornersArray1);
		float width1 = fourCornersArray1[2].x - fourCornersArray1[0].x;
		float height1 = fourCornersArray1[2].y - fourCornersArray1[0].y;
		
		Vector3[] fourCornersArray = new Vector3[4];
		trans.GetWorldCorners( fourCornersArray);
		
		fourCornersArray[0] -= fourCornersArray1[0];
		fourCornersArray[2] -= fourCornersArray1[0];
		
		fourCornersArray[0].x /= width1;
		fourCornersArray[0].y /= height1;
		fourCornersArray[2].x /= width1;
		fourCornersArray[2].y /= height1;
		
		return fourCornersArray[0].Between(0,1) || fourCornersArray[2].Between(0,1);
    }
	
    public static void SetCustomAnchor (this RectTransform trans) {
        RectTransform parent = (RectTransform)trans.parent;
		
		Vector3[] fourCornersArray1 = new Vector3[4];
		parent.GetWorldCorners( fourCornersArray1);
		float width1 = fourCornersArray1[2].x - fourCornersArray1[0].x;
		float height1 = fourCornersArray1[2].y - fourCornersArray1[0].y;
		
		Vector3[] fourCornersArray = new Vector3[4];
		trans.GetWorldCorners( fourCornersArray);
		
		fourCornersArray[0] -= fourCornersArray1[0];
		fourCornersArray[2] -= fourCornersArray1[0];
		
		
		fourCornersArray[0].x /= width1;
		fourCornersArray[0].y /= height1;
		fourCornersArray[2].x /= width1;
		fourCornersArray[2].y /= height1;
		
		trans.anchorMin = fourCornersArray[0];
		trans.anchorMax = fourCornersArray[2];
		trans.anchoredPosition = Vector2.zero;
		trans.sizeDelta = Vector2.zero;
    }
	
    public static void setDirty(this UnityEngine.Object cObject)
    {
#if UNITY_EDITOR	
		UnityEditor.EditorUtility.SetDirty(cObject);
#endif
	}
	
	public static void SetFadeEnable(this GameObject cObject, bool enabled)
	{
		//if (cObject == null) return;
		PanelFadeEffect comp = cObject.GetComponent<PanelFadeEffect>();
		if (comp == null) comp = cObject.AddComponent<PanelFadeEffect>();
		comp.Enabled = enabled;
	}

	public static Texture2D LoadPNG(string filePath) {

		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists(filePath))     {
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData);
		}
		return tex;
	}	 
	
	public static Text TextComponent( this Button val) {
		return val.gameObject.GetComponentInChildren<Text> ();
	}


}

public static class MATHUTILES  {
	
	public static Rect Scale(this Rect bounds, float sval)
	{
			Rect res = bounds;
			res.x *= sval; res.y *= sval; res.width *= sval; res.height *= sval;
			return res;
	}
	
	public static Rect bounds(this Texture tex)
	{
			return new Rect(0,0,tex.width,tex.height);
	}
	
	public static bool Contains(this Rect r1, Rect r2)
	{
		if (r2.x < r1.x || r2.y < r1.y || (r2.width+r2.x > r1.width) || (r2.height+r2.y > r1.height))
			return false;
		return true;
	}
	
	public static Vector2 TopLeft(this Rect rect)
	{
		return new Vector2( rect.xMin, rect.yMin);
	}
	
	public static Vector2 TopRight(this Rect rect)
	{
		return new Vector2( rect.xMax, rect.yMin);
	}
	
	public static Vector2 buttomRight(this Rect rect)
	{
		return new Vector2( rect.xMax, rect.yMax);
	}
	
	public static Vector2 buttomLeft(this Rect rect)
	{
		return new Vector2( rect.xMin, rect.yMax);
	}
	
	public static bool Collide(this Rect r1, Rect r2)
	{
		return r1.Contains(r2.TopLeft()) || r1.Contains(r2.buttomRight()) || r1.Contains(r2.TopRight()) || r1.Contains(r2.buttomLeft()) || 
			   r2.Contains(r1.TopLeft()) || r2.Contains(r1.buttomRight()) || r2.Contains(r1.TopRight()) || r2.Contains(r1.buttomLeft());
	}
		
	public static int ScaleNumberTo (this int score, int oldMax, int newMax = 100 )
	{
		float s = (float)score/(float)oldMax;
		return Mathf.RoundToInt(s*100);
	}
	
	public static bool IsWithin(this int value, int minimum, int maximum)
	{
		return value >= minimum && value <= maximum;
	}


}
				
public static class TIMEUTILES  {
	public static long NowMillSecs
	{
		get
		{
			return DateTime.Now.GetMillSecs();
		}
	}
	
	public static long GetMillSecs(this DateTime date)
	{
			return date.Ticks / TimeSpan.TicksPerMillisecond;
	}
	
	public static DateTime MillSecsDateTime(long millSecs) {
			return new DateTime( millSecs*TimeSpan.TicksPerMillisecond);
	}
	public static string AgeName(int age) {
		string ageStr = "Invalid";
		switch (age)
		{
			case 1 : ageStr = "13"; break;
			case 2 : ageStr = "14-15"; break;
			case 3 : ageStr = "16-19"; break;
		}
		return string.Format("ﺔﻨﺳ ({0})", ageStr);
	}

	public static void GetAge(DateTime sessionDate, DateTime birthday, out int years, out int months, out int days)
	{
    	years = sessionDate.Year - birthday.Year;
    	if (sessionDate < birthday.AddYears(years)) years--;
		DateTime d = birthday.AddYears(years);
		months = 0;	for(  d = d.AddMonths(1); d< sessionDate; d = d.AddMonths(1)){ months++; }	d = d.AddMonths(-1);
		days = 0;	for(  d = d.AddDays(1); d< sessionDate; d = d.AddDays(1)) { days++; }
	}
	
	
	public static string TimeToStr(this float t)
	{
		string time  = "";
		
		int hours  = t.ToInt();
		hours /= 3600;
		
		int minutes  = t.ToInt();
		minutes = (minutes / 60) - (hours * 60);
	
		int seconds  = t.ToInt();
		seconds = seconds % 60;
		
		if (hours > 0)
			time += (((hours < 10) ? "0" : "") + hours.ToString("0")) + ":" + (((minutes < 10) ? "0" : "") + minutes.ToString("0")) + ":" + (((seconds < 10) ? "0" : "") + seconds.ToString("0"));
		else
		if (minutes > 0)
			time += (((minutes < 10) ? "0" : "") + minutes.ToString("0")) + ":" + (((seconds < 10) ? "0" : "") + seconds.ToString("0"));
			else
				time += (((seconds < 10) ? "0" : "") + seconds.ToString("0"));
		return time;
	}
	public static DateTime LongToDateTime ( long t )
	{
		DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return start.AddMilliseconds(t);
	}

	public static long DateTimeToLong ( string st )
	{
		DateTime t = Convert.ToDateTime( st );
		return DateTimeToLong( t );
	}

	public static long DateTimeToLong ( DateTime t )
	{
		DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return Convert.ToInt64( t.Subtract( start ).TotalMilliseconds );
	}

	public static int DateTimeToInt ( DateTime t )
	{
		DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return Convert.ToInt32( t.Subtract( start ).TotalMinutes );
	}

	public static DateTime IntToDateTime ( int t )
	{
		DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return start.AddMinutes(t);
	}
}

public static class STRINGUTILES  {
	public static string Encode(this string text) {
		var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
		return System.Convert.ToBase64String(plainTextBytes);
	}

	public static string Decode(this string text) {
		var base64EncodedBytes = System.Convert.FromBase64String(text);
		return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	}

}
