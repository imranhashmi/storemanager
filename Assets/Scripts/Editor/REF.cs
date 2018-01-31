using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine.UI;
		
public static class REF  {
	
	public static T GetComponentStaticValue<T>(this Type theClassType, string propName) {		
		
 		PropertyInfo propertyInfo = theClassType.GetProperty( propName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
		if (propertyInfo == null)
		{
			PropertyInfo[] properties = theClassType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			foreach (PropertyInfo info in properties)
			{
			    if (info.Name.Equals(propName))
				{
					propertyInfo = info;
					break;
				}
			}
		}
		
		if (propertyInfo != null)
			return (T)propertyInfo.GetValue(null, null);
		
		Debug.LogError("property value not "+typeof( T));
		return default(T);
	}

	public static void SetComponentStaticValue<T>(this Type theClassType, string propName, T propVal) {		
 		PropertyInfo propertyInfo = theClassType.GetProperty( propName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
		if (propertyInfo == null)
		{
			PropertyInfo[] properties = theClassType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			foreach (PropertyInfo info in properties)
			{
			    if (info.Name.Equals(propName))
				{
					propertyInfo = info;
					break;
				}
			}
		}
		if (propertyInfo != null)
			propertyInfo.SetValue(null, propVal, null);
		else
			Debug.LogError("property value not "+typeof( T));
	}
	
	public static T GetComponentValue<T>(this Component cObject, string vName)
	{
			if (vName.IndexOf('.') == -1)
			{
				return GetObjectValue<T>( cObject, vName);
			}
			string[] varnames = vName.Split('.');
			Type compType = cObject.GetType();
			PropertyInfo objProp = compType.GetProperty( varnames[0]);
			if ( objProp != null)
			{
				if (objProp.CanWrite && objProp.CanRead)
				{
		            object temp = objProp.GetValue(cObject, null);
					return GetObjectValue<T>( temp, varnames[1]);
				}
			}
			FieldInfo objField = compType.GetField( varnames[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			if( objField != null)
			{
	            object temp = objField.GetValue(cObject);
				return GetObjectValue<T>( temp, varnames[1]);
			}		
		Debug.LogError(typeof( T)+" property not found : " + vName);
		return default(T);
	}

	public static T GetObjectValue<T>(object cObject, string vName)
	{
		
			Type compType = cObject.GetType();
			while( compType != null)
			{
				PropertyInfo objProp = compType.GetProperty( vName, typeof( T));
				if ( objProp != null)
				{
					object[] pca = objProp.GetCustomAttributes(true);
					if (pca.Length > 0)
					{
						if (pca[0].GetType() != typeof(HideInInspector))
							Debug.Log("1122="+objProp.Name+"___"+ pca[0] +"   "+pca.Length);
					}
					if (objProp.CanWrite && objProp.CanRead)
					{
		            	object temp = objProp.GetValue(cObject, null);
						return (T)temp;
					}
				}
				FieldInfo objField = compType.GetField( vName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				if( objField != null)
				{
					if (typeof( T).IsAssignableFrom( objField.FieldType))
					{
					// check if hidden
					/*object[] pca = objField.GetCustomAttributes(true);
					if (pca.Length > 0)
					{
						if (pca[0].GetType() != typeof(HideInInspector))
							Debug.Log("3344");
					}		*/	
		            object temp = objField.GetValue(cObject);
					return (T)temp;
					}
					else
					{
								Debug.LogError("property value not "+typeof( T));
								return default(T);
							
			        }
				}		
				compType = compType.BaseType;
				if (compType == typeof(object)) 
					break;
			}
		Debug.LogError(typeof( T)+" property not found : " + vName);
		return default(T);
	}

	public static void SetComponentValue<T>(this Component cObject, string vName, T val)
	{
			
			if (vName.IndexOf('.') == -1)
			{
	            object temp = cObject;
				SetObjectValue<T>( ref temp, vName, val);
				return;
			}
			string[] varnames = vName.Split('.');
			Type compType = cObject.GetType();
			PropertyInfo objProp = compType.GetProperty( varnames[0]);
			if ( objProp != null)
			{
				if (objProp.CanWrite && objProp.CanRead)
				{
		            object temp = objProp.GetValue(cObject, null);
	            	SetObjectValue<T>(ref temp, varnames[1], val);
		            objProp.SetValue(cObject, temp, null);
					return;
				}
			}
			FieldInfo objField = compType.GetField( varnames[0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			if( objField != null)
			{
	            object temp = objField.GetValue(cObject);
            	SetObjectValue<T>(ref temp, varnames[1], val);
				objField.SetValue( cObject, temp);
			}		
	}

	public static void SetObjectValue<T>(ref object cObject, string vName, T val)
	{
			Type compType = cObject.GetType();
			while( compType != null)
			{
				PropertyInfo objProp = compType.GetProperty( vName, typeof( T));
				if ( objProp != null)
				{
					object[] pca = objProp.GetCustomAttributes(true);
					if (pca.Length > 0)
					{
						if (pca[0].GetType() != typeof(HideInInspector))
							Debug.Log("1122="+objProp.Name+"___"+ pca[0] +"   "+pca.Length);
					}
					if (objProp.CanWrite && objProp.CanRead)
					{
		            	objProp.SetValue( cObject, val, null);
						return;
					}
				}
				FieldInfo objField = compType.GetField( vName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				if( objField != null)
				{
					if (typeof( T).IsAssignableFrom( objField.FieldType))
					{
					// check if hidden
					/*object[] pca = objField.GetCustomAttributes(true);
					if (pca.Length > 0)
					{
						if (pca[0].GetType() != typeof(HideInInspector))
							Debug.Log("3344");
					}*/			
		            objField.SetValue(cObject, val);
					return;
					}
					else
					{
								Debug.LogError("property value not "+typeof( T));
								return;
							
			        }
				}
			compType = compType.BaseType;
			if (compType == typeof(object)) 
				break;
		}
		Debug.LogError(typeof( T)+"   "+vName+" property not found");
		return;
	}	
	
	public class ComponentField{
		public Component component;
		public FieldInfo fieldInfo;
	}
	
	public static List<ComponentField> GetGameObjectsAllFields( object objVal)
	{
		List<ComponentField> res = new List<ComponentField>();
 		GameObject[] gos = UnityEngine.Resources.FindObjectsOfTypeAll<GameObject>() ;
		foreach( GameObject go in gos)
		{
			res.AddRange( GetGameObjectAllFields( go, objVal));
		}
		return res;
	}
	
	public static List<ComponentField> GetGameObjectAllFields(GameObject cObject, object objVal)
	{
		List<ComponentField> res = new List<ComponentField>();
		Component[] comps = cObject.GetComponents<Component>();
		foreach( Component comp in comps)
		{
			var fields = GetObjectAllFields( comp, objVal);
			foreach( var field in fields)
			{
				res.Add( new ComponentField() { component = comp, fieldInfo = field});
			}
		}
		return res;
	}
	
	public static List<FieldInfo> GetObjectAllFields(object cObject, object objVal)
	{
		List<FieldInfo> res = new List<FieldInfo>();
		if (cObject == null)
		{
			Debug.LogError("Null Object!!!");
			return res;
		}
			
		Type compType = cObject.GetType();
		while( compType != null)
		{
			FieldInfo[] objFields = compType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			foreach( FieldInfo objField in objFields)
				if (objField.GetValue(cObject) == objVal)
					res.Add(objField);
			compType = compType.BaseType;
			if (compType == typeof(object)) 
				break;
		}
		return res;
	}
		
}