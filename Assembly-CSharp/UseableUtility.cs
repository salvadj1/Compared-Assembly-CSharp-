using System;
using UnityEngine;

// Token: 0x0200022A RID: 554
public static class UseableUtility
{
	// Token: 0x06000EFB RID: 3835 RVA: 0x00039CC4 File Offset: 0x00037EC4
	public static void LogError<T>(T a, Object b)
	{
		if (global::UseableUtility.log_enabled)
		{
			Debug.LogError(a, b);
		}
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x00039CDC File Offset: 0x00037EDC
	public static void LogWarning<T>(T a, Object b)
	{
		if (global::UseableUtility.log_enabled)
		{
			Debug.LogWarning(a, b);
		}
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x00039CF4 File Offset: 0x00037EF4
	public static void Log<T>(T a, Object b)
	{
		if (global::UseableUtility.log_enabled)
		{
			Debug.Log(a, b);
		}
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x00039D0C File Offset: 0x00037F0C
	public static void LogError<T>(T a)
	{
		if (global::UseableUtility.log_enabled)
		{
			Debug.LogError(a);
		}
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x00039D24 File Offset: 0x00037F24
	public static void LogWarning<T>(T a)
	{
		if (global::UseableUtility.log_enabled)
		{
			Debug.LogWarning(a);
		}
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x00039D3C File Offset: 0x00037F3C
	public static void Log<T>(T a)
	{
		if (global::UseableUtility.log_enabled)
		{
			Debug.Log(a);
		}
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x00039D54 File Offset: 0x00037F54
	public static bool Succeeded(this global::UseResponse response)
	{
		bool flag = (int)response >= 0;
		if (!flag)
		{
			global::UseableUtility.LogWarning<string>("Did not succeed " + response);
		}
		return flag;
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x00039D88 File Offset: 0x00037F88
	public static bool ThrewException<E>(this global::UseResponse response, out E e, bool doNotClear) where E : Exception
	{
		if ((int)response < -16 || (int)response > -10)
		{
			e = (E)((object)null);
			return false;
		}
		return global::Useable.GetLastException<E>(out e, doNotClear);
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x00039DB4 File Offset: 0x00037FB4
	public static bool ThrewException(this global::UseResponse response, out Exception e, bool doNotClear)
	{
		if ((int)response < -16 || (int)response > -10)
		{
			e = null;
			return false;
		}
		return global::Useable.GetLastException(out e, doNotClear);
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x00039DD4 File Offset: 0x00037FD4
	public static bool ThrewException(this global::UseResponse response, out Exception e)
	{
		return response.ThrewException(out e, false);
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x00039DE0 File Offset: 0x00037FE0
	public static bool Checked(this global::UseResponse response)
	{
		return (int)response < -16 || (int)response > 0;
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x00039DF4 File Offset: 0x00037FF4
	public static void OnDestroy(global::IUseable self, global::Useable useable)
	{
		if (useable && useable.occupied)
		{
			useable.Eject();
		}
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x00039E14 File Offset: 0x00038014
	public static void OnDestroy(global::IUseable self)
	{
		MonoBehaviour monoBehaviour = self as MonoBehaviour;
		if (monoBehaviour)
		{
			global::UseableUtility.OnDestroy(self, monoBehaviour.GetComponent<global::Useable>());
		}
	}

	// Token: 0x04000995 RID: 2453
	public const global::UseResponse kMinSuccess = global::UseResponse.Pass_Unchecked;

	// Token: 0x04000996 RID: 2454
	public const global::UseResponse kMinException = global::UseResponse.Fail_CheckException;

	// Token: 0x04000997 RID: 2455
	public const global::UseResponse kMaxException = global::UseResponse.Fail_Vacancy;

	// Token: 0x04000998 RID: 2456
	public const global::UseResponse kMinSucessChecked = global::UseResponse.Pass_Checked;

	// Token: 0x04000999 RID: 2457
	public const global::UseResponse kMaxFailedChecked = (global::UseResponse)-17;

	// Token: 0x0400099A RID: 2458
	private static bool log_enabled;
}
