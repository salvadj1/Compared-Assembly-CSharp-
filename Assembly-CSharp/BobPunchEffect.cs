using System;
using UnityEngine;

// Token: 0x0200028C RID: 652
public class BobPunchEffect : global::BobEffect
{
	// Token: 0x06001773 RID: 6003 RVA: 0x00057A88 File Offset: 0x00055C88
	protected override void InitializeNonSerializedData()
	{
		this.x = new global::BobPunchEffect.CurveInfo(this._x);
		this.y = new global::BobPunchEffect.CurveInfo(this._y);
		this.z = new global::BobPunchEffect.CurveInfo(this._z);
		this.yaw = new global::BobPunchEffect.CurveInfo(this._yaw);
		this.pitch = new global::BobPunchEffect.CurveInfo(this._pitch);
		this.roll = new global::BobPunchEffect.CurveInfo(this._roll);
		this.glob.valid = (this.x.valid || this.y.valid || this.z.valid || this.yaw.valid || this.pitch.valid || this.roll.valid);
		this.glob.constant = (this.glob.valid && ((!this.x.valid || this.x.constant) && (!this.y.valid || this.y.constant) && (!this.z.valid || this.z.constant) && (!this.yaw.valid || this.yaw.constant) && (!this.pitch.valid || this.pitch.constant) && (!this.roll.valid || this.roll.constant)));
		if (this.glob.constant)
		{
			this.glob.valid = false;
			this.glob.startTime = 0f;
			this.glob.endTime = 0f;
			this.glob.duration = 0f;
		}
		else
		{
			this.glob.startTime = float.PositiveInfinity;
			this.glob.endTime = float.NegativeInfinity;
			if (this.x.valid && !this.x.constant)
			{
				if (this.x.startTime < this.glob.startTime)
				{
					this.glob.startTime = this.x.startTime;
				}
				if (this.x.endTime > this.glob.endTime)
				{
					this.glob.endTime = this.x.endTime;
				}
			}
			if (this.z.valid && !this.z.constant)
			{
				if (this.z.startTime < this.glob.startTime)
				{
					this.glob.startTime = this.z.startTime;
				}
				if (this.z.endTime > this.glob.endTime)
				{
					this.glob.endTime = this.z.endTime;
				}
			}
			if (this.yaw.valid && !this.yaw.constant)
			{
				if (this.yaw.startTime < this.glob.startTime)
				{
					this.glob.startTime = this.yaw.startTime;
				}
				if (this.yaw.endTime > this.glob.endTime)
				{
					this.glob.endTime = this.yaw.endTime;
				}
			}
			if (this.pitch.valid && !this.pitch.constant)
			{
				if (this.pitch.startTime < this.glob.startTime)
				{
					this.glob.startTime = this.pitch.startTime;
				}
				if (this.pitch.endTime > this.glob.endTime)
				{
					this.glob.endTime = this.pitch.endTime;
				}
			}
			if (this.roll.valid && !this.roll.constant)
			{
				if (this.roll.startTime < this.glob.startTime)
				{
					this.glob.startTime = this.roll.startTime;
				}
				if (this.roll.endTime > this.glob.endTime)
				{
					this.glob.endTime = this.roll.endTime;
				}
			}
			if (this.roll.valid && !this.roll.constant)
			{
				if (this.roll.startTime < this.glob.startTime)
				{
					this.glob.startTime = this.roll.startTime;
				}
				if (this.roll.endTime > this.glob.endTime)
				{
					this.glob.endTime = this.roll.endTime;
				}
			}
			if (this.glob.startTime == float.PositiveInfinity)
			{
				this.glob.startTime = 0f;
				this.glob.endTime = 0f;
				this.glob.duration = 0f;
				this.glob.valid = false;
			}
			else
			{
				this.glob.duration = this.glob.endTime - this.glob.startTime;
			}
		}
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x00058040 File Offset: 0x00056240
	protected override bool OpenData(out global::BobEffect.Data data)
	{
		if (!this.glob.valid)
		{
			data = null;
			return false;
		}
		data = new global::BobPunchEffect.PunchData();
		data.effect = this;
		return true;
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x00058068 File Offset: 0x00056268
	protected override void CloseData(global::BobEffect.Data data)
	{
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x0005806C File Offset: 0x0005626C
	protected override global::BOBRES SimulateData(ref global::BobEffect.Context ctx)
	{
		if (ctx.dt == 0.0)
		{
			return global::BOBRES.CONTINUE;
		}
		global::BobPunchEffect.PunchData punchData = (global::BobPunchEffect.PunchData)ctx.data;
		if (punchData.time >= this.glob.endTime)
		{
			return global::BOBRES.EXIT;
		}
		if (punchData.time >= this.glob.endTime)
		{
			return global::BOBRES.EXIT;
		}
		if (this.x.valid)
		{
			if (this.x.constant || punchData.time <= this.x.startTime)
			{
				punchData.force.x = (double)this.x.startValue;
			}
			else if (punchData.time >= this.x.endValue)
			{
				punchData.force.x = (double)this.x.endValue;
			}
			else
			{
				punchData.force.x = (double)this.x.curve.Evaluate(punchData.time);
			}
		}
		if (this.y.valid)
		{
			if (this.y.constant || punchData.time <= this.y.startTime)
			{
				punchData.force.y = (double)this.y.startValue;
			}
			else if (punchData.time >= this.y.endValue)
			{
				punchData.force.y = (double)this.y.endValue;
			}
			else
			{
				punchData.force.y = (double)this.y.curve.Evaluate(punchData.time);
			}
		}
		if (this.z.valid)
		{
			if (this.z.constant || punchData.time <= this.z.startTime)
			{
				punchData.force.z = (double)this.z.startValue;
			}
			else if (punchData.time >= this.z.endValue)
			{
				punchData.force.z = (double)this.z.endValue;
			}
			else
			{
				punchData.force.z = (double)this.z.curve.Evaluate(punchData.time);
			}
		}
		if (this.pitch.valid)
		{
			if (this.pitch.constant || punchData.time <= this.pitch.startTime)
			{
				punchData.torque.x = (double)this.pitch.startValue;
			}
			else if (punchData.time >= this.pitch.endValue)
			{
				punchData.torque.x = (double)this.pitch.endValue;
			}
			else
			{
				punchData.torque.x = (double)this.pitch.curve.Evaluate(punchData.time);
			}
		}
		if (this.yaw.valid)
		{
			if (this.yaw.constant || punchData.time <= this.yaw.startTime)
			{
				punchData.torque.y = (double)this.yaw.startValue;
			}
			else if (punchData.time >= this.yaw.endValue)
			{
				punchData.torque.y = (double)this.yaw.endValue;
			}
			else
			{
				punchData.torque.y = (double)this.yaw.curve.Evaluate(punchData.time);
			}
		}
		if (this.roll.valid)
		{
			if (this.roll.constant || punchData.time <= this.roll.startTime)
			{
				punchData.torque.z = (double)this.roll.startValue;
			}
			else if (punchData.time >= this.roll.endValue)
			{
				punchData.torque.z = (double)this.roll.endValue;
			}
			else
			{
				punchData.torque.z = (double)this.roll.curve.Evaluate(punchData.time);
			}
		}
		punchData.time += (float)ctx.dt;
		return global::BOBRES.CONTINUE;
	}

	// Token: 0x04000C52 RID: 3154
	[SerializeField]
	private AnimationCurve _x;

	// Token: 0x04000C53 RID: 3155
	[SerializeField]
	private AnimationCurve _y;

	// Token: 0x04000C54 RID: 3156
	[SerializeField]
	private AnimationCurve _z;

	// Token: 0x04000C55 RID: 3157
	[SerializeField]
	private AnimationCurve _yaw;

	// Token: 0x04000C56 RID: 3158
	[SerializeField]
	private AnimationCurve _pitch;

	// Token: 0x04000C57 RID: 3159
	[SerializeField]
	private AnimationCurve _roll;

	// Token: 0x04000C58 RID: 3160
	private global::BobPunchEffect.CurveInfo x;

	// Token: 0x04000C59 RID: 3161
	private global::BobPunchEffect.CurveInfo y;

	// Token: 0x04000C5A RID: 3162
	private global::BobPunchEffect.CurveInfo z;

	// Token: 0x04000C5B RID: 3163
	private global::BobPunchEffect.CurveInfo yaw;

	// Token: 0x04000C5C RID: 3164
	private global::BobPunchEffect.CurveInfo pitch;

	// Token: 0x04000C5D RID: 3165
	private global::BobPunchEffect.CurveInfo roll;

	// Token: 0x04000C5E RID: 3166
	private global::BobPunchEffect.CurveInfo glob;

	// Token: 0x0200028D RID: 653
	private class PunchData : global::BobEffect.Data
	{
		// Token: 0x06001778 RID: 6008 RVA: 0x000584CC File Offset: 0x000566CC
		public override void CopyDataTo(global::BobEffect.Data data)
		{
			base.CopyDataTo(data);
			((global::BobPunchEffect.PunchData)data).time = this.time;
		}

		// Token: 0x04000C5F RID: 3167
		public float time;
	}

	// Token: 0x0200028E RID: 654
	private struct CurveInfo
	{
		// Token: 0x06001779 RID: 6009 RVA: 0x000584E8 File Offset: 0x000566E8
		public CurveInfo(AnimationCurve curve)
		{
			if (curve == null)
			{
				this = default(global::BobPunchEffect.CurveInfo);
			}
			else
			{
				this.curve = curve;
			}
			this.length = curve.length;
			switch (this.length)
			{
			case 0:
				this.endTime = 0f;
				this.startTime = 0f;
				this.duration = 0f;
				this.min = 0f;
				this.max = 0f;
				this.range = 0f;
				this.startValue = 0f;
				this.endValue = 0f;
				this.valid = false;
				this.constant = false;
				break;
			case 1:
				this.startTime = curve[0].time;
				this.endTime = this.startTime;
				this.duration = 0f;
				this.min = curve[0].value;
				this.max = this.min;
				this.startValue = this.min;
				this.endValue = this.min;
				this.range = 0f;
				this.valid = true;
				this.constant = true;
				break;
			case 2:
				this.startTime = curve[0].time;
				this.endTime = curve[1].time;
				this.duration = this.endTime - this.startTime;
				this.startValue = curve[0].value;
				this.endValue = curve[1].value;
				if (this.endValue < this.startValue)
				{
					this.range = this.startValue - this.endValue;
					this.min = this.endValue;
					this.max = this.startValue;
				}
				else
				{
					this.range = this.endValue - this.startValue;
					this.min = this.startValue;
					this.max = this.endValue;
				}
				this.valid = true;
				this.constant = (this.range == 0f);
				break;
			default:
				this.startTime = curve[0].time;
				this.endTime = curve[this.length - 1].time;
				this.duration = this.endTime - this.startTime;
				this.min = (this.startValue = curve[0].value);
				this.max = this.min;
				this.endValue = this.startValue;
				for (int i = 1; i < this.length; i++)
				{
					this.endValue = curve[i].value;
					if (this.endValue > this.max)
					{
						this.max = this.endValue;
					}
					if (this.endValue < this.min)
					{
						this.min = this.endValue;
					}
				}
				this.range = this.max - this.min;
				this.valid = true;
				this.constant = (this.range == 0f);
				break;
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00058840 File Offset: 0x00056A40
		public override string ToString()
		{
			return string.Format("[CurveInfo startTime={0}, duration={1}, min={2}, max={3}, length={4}, valid={5}, constant={6}]", new object[]
			{
				this.startTime,
				this.duration,
				this.min,
				this.max,
				this.length,
				this.valid,
				this.constant
			});
		}

		// Token: 0x04000C60 RID: 3168
		public AnimationCurve curve;

		// Token: 0x04000C61 RID: 3169
		public float endTime;

		// Token: 0x04000C62 RID: 3170
		public float startTime;

		// Token: 0x04000C63 RID: 3171
		public float startValue;

		// Token: 0x04000C64 RID: 3172
		public float endValue;

		// Token: 0x04000C65 RID: 3173
		public float duration;

		// Token: 0x04000C66 RID: 3174
		public float min;

		// Token: 0x04000C67 RID: 3175
		public float max;

		// Token: 0x04000C68 RID: 3176
		public float range;

		// Token: 0x04000C69 RID: 3177
		public int length;

		// Token: 0x04000C6A RID: 3178
		public bool valid;

		// Token: 0x04000C6B RID: 3179
		public bool constant;
	}
}
