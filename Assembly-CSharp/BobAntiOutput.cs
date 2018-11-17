using System;
using UnityEngine;

// Token: 0x020004CA RID: 1226
[Serializable]
public class BobAntiOutput
{
	// Token: 0x06002A8D RID: 10893 RVA: 0x000A91AC File Offset: 0x000A73AC
	private static Vector3 GetVector3(ref Vector3 v, BobAntiOutputAxes axes)
	{
		Vector3 result;
		switch (axes & (BobAntiOutputAxes)3)
		{
		default:
			result.x = v.x;
			break;
		case (BobAntiOutputAxes)2:
			result.x = v.y;
			break;
		case (BobAntiOutputAxes)3:
			result.x = v.z;
			break;
		}
		switch ((axes & (BobAntiOutputAxes)12) >> 2)
		{
		case (BobAntiOutputAxes)1:
			result.y = v.x;
			goto IL_A9;
		case (BobAntiOutputAxes)3:
			result.y = v.z;
			goto IL_A9;
		}
		result.y = v.y;
		IL_A9:
		switch ((axes & (BobAntiOutputAxes)48) >> 4)
		{
		case (BobAntiOutputAxes)1:
			result.z = v.x;
			return result;
		case (BobAntiOutputAxes)2:
			result.z = v.y;
			return result;
		}
		result.z = v.z;
		return result;
	}

	// Token: 0x06002A8E RID: 10894 RVA: 0x000A92BC File Offset: 0x000A74BC
	public Vector3 Positional(Vector3 v)
	{
		return Vector3.Scale(BobAntiOutput.GetVector3(ref v, this.positionalAxes), this.positional);
	}

	// Token: 0x06002A8F RID: 10895 RVA: 0x000A92D8 File Offset: 0x000A74D8
	public Vector3 Rotational(Vector3 v)
	{
		return Vector3.Scale(BobAntiOutput.GetVector3(ref v, this.rotationalAxes), this.rotational);
	}

	// Token: 0x06002A90 RID: 10896 RVA: 0x000A92F4 File Offset: 0x000A74F4
	public void Add(Transform transform, ref Vector3 lp, ref Vector3 lr)
	{
		if (!this.wasAdded)
		{
			this.lastPos = Vector3.Scale(BobAntiOutput.GetVector3(ref lp, this.positionalAxes), this.positional);
			transform.localPosition = this.lastPos;
			this.lastRot = Vector3.Scale(BobAntiOutput.GetVector3(ref lr, this.rotationalAxes), this.rotational);
			transform.localEulerAngles = this.lastRot;
			this.wasAdded = true;
		}
	}

	// Token: 0x06002A91 RID: 10897 RVA: 0x000A9368 File Offset: 0x000A7568
	public void Subtract(Transform transform)
	{
		if (this.wasAdded)
		{
			transform.localPosition -= this.lastPos;
			transform.localEulerAngles -= this.lastRot;
			this.wasAdded = false;
		}
	}

	// Token: 0x06002A92 RID: 10898 RVA: 0x000A93B8 File Offset: 0x000A75B8
	public void Reset()
	{
		this.wasAdded = false;
	}

	// Token: 0x04001703 RID: 5891
	public BobAntiOutputAxes positionalAxes;

	// Token: 0x04001704 RID: 5892
	public Vector3 positional;

	// Token: 0x04001705 RID: 5893
	public BobAntiOutputAxes rotationalAxes;

	// Token: 0x04001706 RID: 5894
	public Vector3 rotational;

	// Token: 0x04001707 RID: 5895
	private bool wasAdded;

	// Token: 0x04001708 RID: 5896
	private Vector3 lastPos;

	// Token: 0x04001709 RID: 5897
	private Vector3 lastRot;
}
