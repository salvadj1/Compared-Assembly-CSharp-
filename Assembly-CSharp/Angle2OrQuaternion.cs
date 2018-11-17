using System;
using UnityEngine;

// Token: 0x0200011B RID: 283
public struct Angle2OrQuaternion
{
	// Token: 0x06000754 RID: 1876 RVA: 0x00021128 File Offset: 0x0001F328
	public static implicit operator global::Angle2OrQuaternion(global::Angle2 v)
	{
		global::Angle2OrQuaternion result;
		result.quat = v.quat;
		return result;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00021144 File Offset: 0x0001F344
	public static implicit operator global::Angle2OrQuaternion(Quaternion v)
	{
		global::Angle2OrQuaternion result;
		result.quat = v;
		return result;
	}

	// Token: 0x040005B1 RID: 1457
	internal Quaternion quat;
}
