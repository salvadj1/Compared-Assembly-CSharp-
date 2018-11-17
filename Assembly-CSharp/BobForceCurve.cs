using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000586 RID: 1414
[Serializable]
public class BobForceCurve
{
	// Token: 0x06002E46 RID: 11846 RVA: 0x000B117C File Offset: 0x000AF37C
	private void Gasp()
	{
		this.infoX = new global::BobForceCurve.CurveInfo(this.forceX);
		this.infoY = new global::BobForceCurve.CurveInfo(this.forceY);
		this.infoZ = new global::BobForceCurve.CurveInfo(this.forceZ);
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

	// Token: 0x06002E47 RID: 11847 RVA: 0x000B1304 File Offset: 0x000AF504
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

	// Token: 0x040018C7 RID: 6343
	public Vector3 positionScale = Vector3.one;

	// Token: 0x040018C8 RID: 6344
	public AnimationCurve forceX;

	// Token: 0x040018C9 RID: 6345
	public AnimationCurve forceY;

	// Token: 0x040018CA RID: 6346
	public AnimationCurve forceZ;

	// Token: 0x040018CB RID: 6347
	public Vector3 outputScale = Vector3.one;

	// Token: 0x040018CC RID: 6348
	public AnimationCurve sourceMask;

	// Token: 0x040018CD RID: 6349
	public AnimationCurve sourceScale;

	// Token: 0x040018CE RID: 6350
	private float duration;

	// Token: 0x040018CF RID: 6351
	private float offset;

	// Token: 0x040018D0 RID: 6352
	private global::BobForceCurve.CurveInfo infoX;

	// Token: 0x040018D1 RID: 6353
	private global::BobForceCurve.CurveInfo infoY;

	// Token: 0x040018D2 RID: 6354
	private global::BobForceCurve.CurveInfo infoZ;

	// Token: 0x040018D3 RID: 6355
	private bool once;

	// Token: 0x040018D4 RID: 6356
	private bool calc;

	// Token: 0x040018D5 RID: 6357
	private bool mask;

	// Token: 0x040018D6 RID: 6358
	private bool scale;

	// Token: 0x040018D7 RID: 6359
	private bool scaleFixed;

	// Token: 0x040018D8 RID: 6360
	public global::BobForceCurveTarget target;

	// Token: 0x040018D9 RID: 6361
	public global::BobForceCurveSource source;

	// Token: 0x02000587 RID: 1415
	private struct CurveInfo
	{
		// Token: 0x06002E48 RID: 11848 RVA: 0x000B1704 File Offset: 0x000AF904
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

		// Token: 0x040018DA RID: 6362
		public float duration;

		// Token: 0x040018DB RID: 6363
		public float offset;

		// Token: 0x040018DC RID: 6364
		public bool calc;

		// Token: 0x040018DD RID: 6365
		public bool constant;
	}
}
