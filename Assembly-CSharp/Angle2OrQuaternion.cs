using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
public struct Angle2OrQuaternion
{
	// Token: 0x06000682 RID: 1666 RVA: 0x0001E554 File Offset: 0x0001C754
	public static implicit operator Angle2OrQuaternion(Angle2 v)
	{
		Angle2OrQuaternion result;
		result.quat = v.quat;
		return result;
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0001E570 File Offset: 0x0001C770
	public static implicit operator Angle2OrQuaternion(Quaternion v)
	{
		Angle2OrQuaternion result;
		result.quat = v;
		return result;
	}

	// Token: 0x040004E6 RID: 1254
	internal Quaternion quat;
}
