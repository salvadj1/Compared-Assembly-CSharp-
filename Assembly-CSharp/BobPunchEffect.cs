using System;
using UnityEngine;

// Token: 0x02000259 RID: 601
public class BobPunchEffect : BobEffect
{
	// Token: 0x0600161F RID: 5663 RVA: 0x000536E0 File Offset: 0x000518E0
	protected override void InitializeNonSerializedData()
	{
		this.x = new BobPunchEffect.CurveInfo(this._x);
		this.y = new BobPunchEffect.CurveInfo(this._y);
		this.z = new BobPunchEffect.CurveInfo(this._z);
		this.yaw = new BobPunchEffect.CurveInfo(this._yaw);
		this.pitch = new BobPunchEffect.CurveInfo(this._pitch);
		this.roll = new BobPunchEffect.CurveInfo(this._roll);
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

	// Token: 0x06001620 RID: 5664 RVA: 0x00053C98 File Offset: 0x00051E98
	protected override bool OpenData(out BobEffect.Data data)
	{
		if (!this.glob.valid)
		{
			data = null;
			return false;
		}
		data = new BobPunchEffect.PunchData();
		data.effect = this;
		return true;
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x00053CC0 File Offset: 0x00051EC0
	protected override void CloseData(BobEffect.Data data)
	{
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x00053CC4 File Offset: 0x00051EC4
	protected override BOBRES SimulateData(ref BobEffect.Context ctx)
	{
		if (ctx.dt == 0.0)
		{
			return BOBRES.CONTINUE;
		}
		BobPunchEffect.PunchData punchData = (BobPunchEffect.PunchData)ctx.data;
		if (punchData.time >= this.glob.endTime)
		{
			return BOBRES.EXIT;
		}
		if (punchData.time >= this.glob.endTime)
		{
			return BOBRES.EXIT;
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
		return BOBRES.CONTINUE;
	}

	// Token: 0x04000B2F RID: 2863
	[SerializeField]
	private AnimationCurve _x;

	// Token: 0x04000B30 RID: 2864
	[SerializeField]
	private AnimationCurve _y;

	// Token: 0x04000B31 RID: 2865
	[SerializeField]
	private AnimationCurve _z;

	// Token: 0x04000B32 RID: 2866
	[SerializeField]
	private AnimationCurve _yaw;

	// Token: 0x04000B33 RID: 2867
	[SerializeField]
	private AnimationCurve _pitch;

	// Token: 0x04000B34 RID: 2868
	[SerializeField]
	private AnimationCurve _roll;

	// Token: 0x04000B35 RID: 2869
	private BobPunchEffect.CurveInfo x;

	// Token: 0x04000B36 RID: 2870
	private BobPunchEffect.CurveInfo y;

	// Token: 0x04000B37 RID: 2871
	private BobPunchEffect.CurveInfo z;

	// Token: 0x04000B38 RID: 2872
	private BobPunchEffect.CurveInfo yaw;

	// Token: 0x04000B39 RID: 2873
	private BobPunchEffect.CurveInfo pitch;

	// Token: 0x04000B3A RID: 2874
	private BobPunchEffect.CurveInfo roll;

	// Token: 0x04000B3B RID: 2875
	private BobPunchEffect.CurveInfo glob;

	// Token: 0x0200025A RID: 602
	private class PunchData : BobEffect.Data
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x00054124 File Offset: 0x00052324
		public override void CopyDataTo(BobEffect.Data data)
		{
			base.CopyDataTo(data);
			((BobPunchEffect.PunchData)data).time = this.time;
		}

		// Token: 0x04000B3C RID: 2876
		public float time;
	}

	// Token: 0x0200025B RID: 603
	private struct CurveInfo
	{
		// Token: 0x06001625 RID: 5669 RVA: 0x00054140 File Offset: 0x00052340
		public CurveInfo(AnimationCurve curve)
		{
			if (curve == null)
			{
				this = default(BobPunchEffect.CurveInfo);
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

		// Token: 0x06001626 RID: 5670 RVA: 0x00054498 File Offset: 0x00052698
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

		// Token: 0x04000B3D RID: 2877
		public AnimationCurve curve;

		// Token: 0x04000B3E RID: 2878
		public float endTime;

		// Token: 0x04000B3F RID: 2879
		public float startTime;

		// Token: 0x04000B40 RID: 2880
		public float startValue;

		// Token: 0x04000B41 RID: 2881
		public float endValue;

		// Token: 0x04000B42 RID: 2882
		public float duration;

		// Token: 0x04000B43 RID: 2883
		public float min;

		// Token: 0x04000B44 RID: 2884
		public float max;

		// Token: 0x04000B45 RID: 2885
		public float range;

		// Token: 0x04000B46 RID: 2886
		public int length;

		// Token: 0x04000B47 RID: 2887
		public bool valid;

		// Token: 0x04000B48 RID: 2888
		public bool constant;
	}
}
