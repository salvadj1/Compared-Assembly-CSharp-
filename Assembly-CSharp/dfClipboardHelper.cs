using System;
using System.Reflection;
using UnityEngine;

// Token: 0x020006F6 RID: 1782
public class dfClipboardHelper
{
	// Token: 0x06004070 RID: 16496 RVA: 0x000F761C File Offset: 0x000F581C
	private static PropertyInfo GetSystemCopyBufferProperty()
	{
		if (dfClipboardHelper.m_systemCopyBufferProperty == null)
		{
			Type typeFromHandle = typeof(GUIUtility);
			dfClipboardHelper.m_systemCopyBufferProperty = typeFromHandle.GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic);
			if (dfClipboardHelper.m_systemCopyBufferProperty == null)
			{
				throw new Exception("Can'time access internal member 'GUIUtility.systemCopyBuffer' it may have been removed / renamed");
			}
		}
		return dfClipboardHelper.m_systemCopyBufferProperty;
	}

	// Token: 0x17000CB2 RID: 3250
	// (get) Token: 0x06004071 RID: 16497 RVA: 0x000F766C File Offset: 0x000F586C
	// (set) Token: 0x06004072 RID: 16498 RVA: 0x000F76C8 File Offset: 0x000F58C8
	public static string clipBoard
	{
		get
		{
			string result;
			try
			{
				PropertyInfo systemCopyBufferProperty = dfClipboardHelper.GetSystemCopyBufferProperty();
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
				PropertyInfo systemCopyBufferProperty = dfClipboardHelper.GetSystemCopyBufferProperty();
				systemCopyBufferProperty.SetValue(null, value, null);
			}
			catch
			{
			}
		}
	}

	// Token: 0x0400223F RID: 8767
	private static PropertyInfo m_systemCopyBufferProperty;
}
