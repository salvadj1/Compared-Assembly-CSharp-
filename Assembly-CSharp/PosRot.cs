using System;
using UnityEngine;

// Token: 0x02000322 RID: 802
public struct PosRot
{
	// Token: 0x06001EBB RID: 7867 RVA: 0x00078C14 File Offset: 0x00076E14
	public static void Lerp(ref PosRot a, ref PosRot b, float t, out PosRot v)
	{
		v.position = Vector3.Lerp(a.position, b.position, t);
		v.rotation = Quaternion.Slerp(a.rotation, b.rotation, t);
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x00078C54 File Offset: 0x00076E54
	public static void Lerp(ref PosRot a, ref PosRot b, double t, out PosRot v)
	{
		PosRot.Lerp(ref a, ref b, (float)t, out v);
	}

	// Token: 0x04000ED8 RID: 3800
	public Vector3 position;

	// Token: 0x04000ED9 RID: 3801
	public Quaternion rotation;
}
