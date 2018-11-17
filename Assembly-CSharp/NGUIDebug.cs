using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200078F RID: 1935
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	// Token: 0x060045DC RID: 17884 RVA: 0x00114CD8 File Offset: 0x00112ED8
	public static void Log(string text)
	{
		if (Application.isPlaying)
		{
			if (NGUIDebug.mLines.Count > 20)
			{
				NGUIDebug.mLines.RemoveAt(0);
			}
			NGUIDebug.mLines.Add(text);
			if (NGUIDebug.mInstance == null)
			{
				GameObject gameObject = new GameObject("_NGUI Debug");
				NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
				Object.DontDestroyOnLoad(gameObject);
			}
		}
		else
		{
			Debug.Log(text);
		}
	}

	// Token: 0x060045DD RID: 17885 RVA: 0x00114D50 File Offset: 0x00112F50
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

	// Token: 0x060045DE RID: 17886 RVA: 0x00114E88 File Offset: 0x00113088
	private void OnGUI()
	{
		int i = 0;
		int count = NGUIDebug.mLines.Count;
		while (i < count)
		{
			GUILayout.Label(NGUIDebug.mLines[i], new GUILayoutOption[0]);
			i++;
		}
	}

	// Token: 0x04002655 RID: 9813
	private static List<string> mLines = new List<string>();

	// Token: 0x04002656 RID: 9814
	private static NGUIDebug mInstance = null;
}
