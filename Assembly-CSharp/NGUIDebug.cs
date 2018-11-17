using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000874 RID: 2164
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	// Token: 0x06004A49 RID: 19017 RVA: 0x0011E658 File Offset: 0x0011C858
	public static void Log(string text)
	{
		if (Application.isPlaying)
		{
			if (global::NGUIDebug.mLines.Count > 20)
			{
				global::NGUIDebug.mLines.RemoveAt(0);
			}
			global::NGUIDebug.mLines.Add(text);
			if (global::NGUIDebug.mInstance == null)
			{
				GameObject gameObject = new GameObject("_NGUI Debug");
				global::NGUIDebug.mInstance = gameObject.AddComponent<global::NGUIDebug>();
				Object.DontDestroyOnLoad(gameObject);
			}
		}
		else
		{
			Debug.Log(text);
		}
	}

	// Token: 0x06004A4A RID: 19018 RVA: 0x0011E6D0 File Offset: 0x0011C8D0
	public static void DrawBounds(Bounds b)
	{
		Vector3 center = b.center;
		Vector3 vector = b.center - b.extents;
		Vector3 vector2 = b.center + b.extents;
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector2.x, vector.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector2.x, vector.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector2.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
	}

	// Token: 0x06004A4B RID: 19019 RVA: 0x0011E808 File Offset: 0x0011CA08
	private void OnGUI()
	{
		int i = 0;
		int count = global::NGUIDebug.mLines.Count;
		while (i < count)
		{
			GUILayout.Label(global::NGUIDebug.mLines[i], new GUILayoutOption[0]);
			i++;
		}
	}

	// Token: 0x0400288C RID: 10380
	private static List<string> mLines = new List<string>();

	// Token: 0x0400288D RID: 10381
	private static global::NGUIDebug mInstance = null;
}
