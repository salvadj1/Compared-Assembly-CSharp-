using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x020004CB RID: 1227
[Serializable]
public class BobForceCurve
{
	// Token: 0x06002A94 RID: 10900 RVA: 0x000A93E4 File Offset: 0x000A75E4
	private void Gasp()
	{
		this.infoX = new BobForceCurve.CurveInfo(this.forceX);
		this.infoY = new BobForceCurve.CurveInfo(this.forceY);
		this.infoZ = new BobForceCurve.CurveInfo(this.forceZ);
		this.calc = (this.infoX.calc || this.infoY.calc || this.infoZ.calc);
		int length = this.sourceMask.length;
		bool flag;
		if (length == 1)
		{
			if (this.sourceMask[0].value == 1f)
			{
				flag = false;
			}
			else if (this.sourceMask[0].value == 0f)
			{
				this.calc = false;
				flag = false;
			}
			else
			{
				flag = true;
			}
		}
		else
		{
			flag = (length != 0);
		}
		length = this.sourceScale.length;
		bool flag2;
		bool flag3;
		if (length == 1)
		{
			flag2 = (this.sourceScale[0].value != 1f);
			flag3 = (this.sourceScale[0].value == 0f);
		}
		else if (length == 0)
		{
			flag2 = false;
			flag3 = false;
		}
		else
		{
			flag2 = true;
			flag3 = false;
		}
		this.mask = flag;
		this.scale = flag2;
		this.scaleFixed = flag3;
		this.once = true;
	}

	// Token: 0x06002A95 RID: 10901 RVA: 0x000A956C File Offset: 0x000A776C
	public void Calculate(ref Vector3G v, ref double pow, ref double dt, ref Vector3G sum)
	{
		if (!this.once)
		{
			this.Gasp();
		}
		if (!this.calc)
		{
			return;
		}
		float num = (!this.mask) ? 1f : this.sourceMask.Evaluate((float)pow);
		bool flag = num == 0f || num == -0f;
		float num2 = (!this.scaleFixed) ? ((!this.scale) ? 1f : this.sourceScale.Evaluate((float)pow)) : 0f;
		bool flag2 = !this.scaleFixed && num2 != 0f && num2 != -0f;
		Vector3G vector3G;
		if (this.infoX.calc)
		{
			if (flag2 && !this.infoX.constant)
			{
				v.x += pow * dt * (double)num2 * (double)this.positionScale.x;
				if (v.x > (double)this.infoX.duration)
				{
					v.x -= (double)this.infoX.duration;
				}
				else if (v.x < (double)(-(double)this.infoX.duration))
				{
					v.x += (double)this.infoX.duration;
				}
			}
			vector3G.x = (double)((!flag) ? (this.forceX.Evaluate((float)v.x) * this.outputScale.x) : 0f);
		}
		else
		{
			vector3G.x = 0.0;
		}
		if (this.infoY.calc)
		{
			if (flag2 && !this.infoY.constant)
			{
				v.y += pow * dt * (double)num2 * (double)this.positionScale.y;
				if (v.y > (double)this.infoY.duration)
				{
					v.y -= (double)this.infoY.duration;
				}
				else if (v.y < (double)(-(double)this.infoY.duration))
				{
					v.y += (double)this.infoY.duration;
				}
			}
			vector3G.y = (double)((!flag) ? (this.forceY.Evaluate((float)v.y) * this.outputScale.y) : 0f);
		}
		else
		{
			vector3G.y = 0.0;
		}
		if (this.infoZ.calc)
		{
			if (flag2 && !this.infoZ.constant)
			{
				v.z += pow * dt * (double)num2 * (double)this.positionScale.z;
				if (v.z > (double)this.infoZ.duration)
				{
					v.z -= (double)this.infoZ.duration;
				}
				else if (v.z < (double)(-(double)this.infoZ.duration))
				{
					v.z += (double)this.infoZ.duration;
				}
			}
			vector3G.z = (double)((!flag) ? (this.forceZ.Evaluate((float)v.z) * this.outputScale.z) : 0f);
		}
		else
		{
			vector3G.z = 0.0;
		}
		if (!flag)
		{
			sum.x += vector3G.x * (double)num;
			sum.y += vector3G.y * (double)num;
			sum.z += vector3G.z * (double)num;
		}
	}

	// Token: 0x0400170A RID: 5898
	public Vector3 positionScale = Vector3.one;

	// Token: 0x0400170B RID: 5899
	public AnimationCurve forceX;

	// Token: 0x0400170C RID: 5900
	public AnimationCurve forceY;

	// Token: 0x0400170D RID: 5901
	public AnimationCurve forceZ;

	// Token: 0x0400170E RID: 5902
	public Vector3 outputScale = Vector3.one;

	// Token: 0x0400170F RID: 5903
	public AnimationCurve sourceMask;

	// Token: 0x04001710 RID: 5904
	public AnimationCurve sourceScale;

	// Token: 0x04001711 RID: 5905
	private float duration;

	// Token: 0x04001712 RID: 5906
	private float offset;

	// Token: 0x04001713 RID: 5907
	private BobForceCurve.CurveInfo infoX;

	// Token: 0x04001714 RID: 5908
	private BobForceCurve.CurveInfo infoY;

	// Token: 0x04001715 RID: 5909
	private BobForceCurve.CurveInfo infoZ;

	// Token: 0x04001716 RID: 5910
	private bool once;

	// Token: 0x04001717 RID: 5911
	private bool calc;

	// Token: 0x04001718 RID: 5912
	private bool mask;

	// Token: 0x04001719 RID: 5913
	private bool scale;

	// Token: 0x0400171A RID: 5914
	private bool scaleFixed;

	// Token: 0x0400171B RID: 5915
	public BobForceCurveTarget target;

	// Token: 0x0400171C RID: 5916
	public BobForceCurveSource source;

	// Token: 0x020004CC RID: 1228
	private struct CurveInfo
	{
		// Token: 0x06002A96 RID: 10902 RVA: 0x000A996C File Offset: 0x000A7B6C
		public CurveInfo(AnimationCurve curve)
		{
			int num = (curve != null) ? curve.length : 0;
			if (num == 0)
			{
				this.calc = false;
				this.constant = true;
				this.duration = 0f;
				this.offset = 0f;
			}
			else if (num == 1)
			{
				this.calc = (curve[0].value != 0f);
				this.constant = true;
				this.duration = 0f;
				this.offset = 0f;
			}
			else
			{
				Keyframe keyframe = curve[0];
				Keyframe keyframe2 = curve[num - 1];
				this.calc = true;
				this.constant = false;
				this.duration = keyframe2.time - keyframe.time;
				this.offset = curve[0].time;
				this.duration *= 8f;
			}
		}

		// Token: 0x0400171D RID: 5917
		public float duration;

		// Token: 0x0400171E RID: 5918
		public float offset;

		// Token: 0x0400171F RID: 5919
		public bool calc;

		// Token: 0x04001720 RID: 5920
		public bool constant;
	}
}
