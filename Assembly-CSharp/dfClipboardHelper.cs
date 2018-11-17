using System;
using System.Reflection;
using UnityEngine;

// Token: 0x020007C8 RID: 1992
public class dfClipboardHelper
{
	// Token: 0x0600448C RID: 17548 RVA: 0x00100220 File Offset: 0x000FE420
	private static PropertyInfo GetSystemCopyBufferProperty()
	{
		if (global::dfClipboardHelper.m_systemCopyBufferProperty == null)
		{
			Type typeFromHandle = typeof(GUIUtility);
			global::dfClipboardHelper.m_systemCopyBufferProperty = typeFromHandle.GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic);
			if (global::dfClipboardHelper.m_systemCopyBufferProperty == null)
			{
				throw new Exception("Can'time access internal member 'GUIUtility.systemCopyBuffer' it may have been removed / renamed");
			}
		}
		return global::dfClipboardHelper.m_systemCopyBufferProperty;
	}

	// Token: 0x17000D36 RID: 3382
	// (get) Token: 0x0600448D RID: 17549 RVA: 0x00100270 File Offset: 0x000FE470
	// (set) Token: 0x0600448E RID: 17550 RVA: 0x001002CC File Offset: 0x000FE4CC
	public static string clipBoard
	{
		get
		{
			string result;
			try
			{
				PropertyInfo systemCopyBufferProperty = global::dfClipboardHelper.GetSystemCopyBufferProperty();
				result = (string)systemCopyBufferProperty.GetValue(null, null);
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}
		set
		{
			try
			{
				PropertyInfo systemCopyBufferProperty = global::dfClipboardHelper.GetSystemCopyBufferProperty();
				systemCopyBufferProperty.SetValue(null, value, null);
			}
			catch
			{
			}
		}
	}

	// Token: 0x04002448 RID: 9288
	private static PropertyInfo m_systemCopyBufferProperty;
}
