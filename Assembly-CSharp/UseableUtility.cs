using System;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public static class UseableUtility
{
	// Token: 0x06000DA7 RID: 3495 RVA: 0x0003591C File Offset: 0x00033B1C
	public static void LogError<T>(T a, Object b)
	{
		if (UseableUtility.log_enabled)
		{
			Debug.LogError(a, b);
		}
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x00035934 File Offset: 0x00033B34
	public static void LogWarning<T>(T a, Object b)
	{
		if (UseableUtility.log_enabled)
		{
			Debug.LogWarning(a, b);
		}
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0003594C File Offset: 0x00033B4C
	public static void Log<T>(T a, Object b)
	{
		if (UseableUtility.log_enabled)
		{
			Debug.Log(a, b);
		}
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x00035964 File Offset: 0x00033B64
	public static void LogError<T>(T a)
	{
		if (UseableUtility.log_enabled)
		{
			Debug.LogError(a);
		}
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0003597C File Offset: 0x00033B7C
	public static void LogWarning<T>(T a)
	{
		if (UseableUtility.log_enabled)
		{
			Debug.LogWarning(a);
		}
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x00035994 File Offset: 0x00033B94
	public static void Log<T>(T a)
	{
		if (UseableUtility.log_enabled)
		{
			Debug.Log(a);
		}
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x000359AC File Offset: 0x00033BAC
	public static bool Succeeded(this UseResponse response)
	{
		bool flag = (int)response >= 0;
		if (!flag)
		{
			UseableUtility.LogWarning<string>("Did not succeed " + response);
		}
		return flag;
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x000359E0 File Offset: 0x00033BE0
	public static bool ThrewException<E>(this UseResponse response, out E e, bool doNotClear) where E : Exception
	{
		if ((int)response < -16 || (int)response > -10)
		{
			e = (E)((object)null);
			return false;
		}
		return Useable.GetLastException<E>(out e, doNotClear);
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00035A0C File Offset: 0x00033C0C
	public static bool ThrewException(this UseResponse response, out Exception e, bool doNotClear)
	{
		if ((int)response < -16 || (int)response > -10)
		{
			e = null;
			return false;
		}
		return Useable.GetLastException(out e, doNotClear);
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x00035A2C File Offset: 0x00033C2C
	public static bool ThrewException(this UseResponse response, out Exception e)
	{
		return response.ThrewException(out e, false);
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x00035A38 File Offset: 0x00033C38
	public static bool Checked(this UseResponse response)
	{
		return (int)response < -16 || (int)response > 0;
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x00035A4C File Offset: 0x00033C4C
	public static void OnDestroy(IUseable self, Useable useable)
	{
		if (useable && useable.occupied)
		{
			useable.Eject();
		}
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x00035A6C File Offset: 0x00033C6C
	public static void OnDestroy(IUseable self)
	{
		MonoBehaviour monoBehaviour = self as MonoBehaviour;
		if (monoBehaviour)
		{
			UseableUtility.OnDestroy(self, monoBehaviour.GetComponent<Useable>());
		}
	}

	// Token: 0x04000872 RID: 2162
	public const UseResponse kMinSuccess = UseResponse.Pass_Unchecked;

	// Token: 0x04000873 RID: 2163
	public const UseResponse kMinException = UseResponse.Fail_CheckException;

	// Token: 0x04000874 RID: 2164
	public const UseResponse kMaxException = UseResponse.Fail_Vacancy;

	// Token: 0x04000875 RID: 2165
	public const UseResponse kMinSucessChecked = UseResponse.Pass_Checked;

	// Token: 0x04000876 RID: 2166
	public const UseResponse kMaxFailedChecked = (UseResponse)(-17);

	// Token: 0x04000877 RID: 2167
	private static bool log_enabled;
}
