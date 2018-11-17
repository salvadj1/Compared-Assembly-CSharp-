using System;
using UnityEngine;

// Token: 0x020003CF RID: 975
public struct PosRot
{
	// Token: 0x0600221D RID: 8733 RVA: 0x0007E010 File Offset: 0x0007C210
	public static void Lerp(ref global::PosRot a, ref global::PosRot b, float t, out global::PosRot v)
	{
		v.position = Vector3.Lerp(a.position, b.position, t);
		v.rotation = Quaternion.Slerp(a.rotation, b.rotation, t);
	}

	// Token: 0x0600221E RID: 8734 RVA: 0x0007E050 File Offset: 0x0007C250
	public static void Lerp(ref global::PosRot a, ref global::PosRot b, double t, out global::PosRot v)
	{
		global::PosRot.Lerp(ref a, ref b, (float)t, out v);
	}

	// Token: 0x0400103E RID: 4158
	public Vector3 position;

	// Token: 0x0400103F RID: 4159
	public Quaternion rotation;
}
