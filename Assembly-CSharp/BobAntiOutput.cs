using System;
using UnityEngine;

// Token: 0x02000585 RID: 1413
[Serializable]
public class BobAntiOutput
{
	// Token: 0x06002E3F RID: 11839 RVA: 0x000B0F44 File Offset: 0x000AF144
	private static Vector3 GetVector3(ref Vector3 v, global::BobAntiOutputAxes axes)
	{
		Vector3 result;
		switch (axes & (global::BobAntiOutputAxes)3)
		{
		default:
			result.x = v.x;
			break;
		case (global::BobAntiOutputAxes)2:
			result.x = v.y;
			break;
		case (global::BobAntiOutputAxes)3:
			result.x = v.z;
			break;
		}
		switch ((axes & (global::BobAntiOutputAxes)12) >> 2)
		{
		case (global::BobAntiOutputAxes)1:
			result.y = v.x;
			goto IL_A9;
		case (global::BobAntiOutputAxes)3:
			result.y = v.z;
			goto IL_A9;
		}
		result.y = v.y;
		IL_A9:
		switch ((axes & (global::BobAntiOutputAxes)48) >> 4)
		{
		case (global::BobAntiOutputAxes)1:
			result.z = v.x;
			return result;
		case (global::BobAntiOutputAxes)2:
			result.z = v.y;
			return result;
		}
		result.z = v.z;
		return result;
	}

	// Token: 0x06002E40 RID: 11840 RVA: 0x000B1054 File Offset: 0x000AF254
	public Vector3 Positional(Vector3 v)
	{
		return Vector3.Scale(global::BobAntiOutput.GetVector3(ref v, this.positionalAxes), this.positional);
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x000B1070 File Offset: 0x000AF270
	public Vector3 Rotational(Vector3 v)
	{
		return Vector3.Scale(global::BobAntiOutput.GetVector3(ref v, this.rotationalAxes), this.rotational);
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x000B108C File Offset: 0x000AF28C
	public void Add(Transform transform, ref Vector3 lp, ref Vector3 lr)
	{
		if (!this.wasAdded)
		{
			this.lastPos = Vector3.Scale(global::BobAntiOutput.GetVector3(ref lp, this.positionalAxes), this.positional);
			transform.localPosition = this.lastPos;
			this.lastRot = Vector3.Scale(global::BobAntiOutput.GetVector3(ref lr, this.rotationalAxes), this.rotational);
			transform.localEulerAngles = this.lastRot;
			this.wasAdded = true;
		}
	}

	// Token: 0x06002E43 RID: 11843 RVA: 0x000B1100 File Offset: 0x000AF300
	public void Subtract(Transform transform)
	{
		if (this.wasAdded)
		{
			transform.localPosition -= this.lastPos;
			transform.localEulerAngles -= this.lastRot;
			this.wasAdded = false;
		}
	}

	// Token: 0x06002E44 RID: 11844 RVA: 0x000B1150 File Offset: 0x000AF350
	public void Reset()
	{
		this.wasAdded = false;
	}

	// Token: 0x040018C0 RID: 6336
	public global::BobAntiOutputAxes positionalAxes;

	// Token: 0x040018C1 RID: 6337
	public Vector3 positional;

	// Token: 0x040018C2 RID: 6338
	public global::BobAntiOutputAxes rotationalAxes;

	// Token: 0x040018C3 RID: 6339
	public Vector3 rotational;

	// Token: 0x040018C4 RID: 6340
	private bool wasAdded;

	// Token: 0x040018C5 RID: 6341
	private Vector3 lastPos;

	// Token: 0x040018C6 RID: 6342
	private Vector3 lastRot;
}
