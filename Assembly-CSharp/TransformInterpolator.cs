using System;
using UnityEngine;

// Token: 0x02000326 RID: 806
public sealed class TransformInterpolator : StateInterpolator<PosRot>, IStateInterpolatorWithLinearVelocity, IStateInterpolator<PosRot>, IStateInterpolatorSampler<PosRot>
{
	// Token: 0x06001ED7 RID: 7895 RVA: 0x0007912C File Offset: 0x0007732C
	public sealed override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		PosRot posRot;
		posRot.position = pos;
		posRot.rotation = rot;
		this.SetGoals(ref posRot, ref timestamp);
	}

	// Token: 0x06001ED8 RID: 7896 RVA: 0x00079154 File Offset: 0x00077354
	public void SetGoals(PosRot frame, double timestamp)
	{
		this.SetGoals(ref frame, ref timestamp);
	}

	// Token: 0x06001ED9 RID: 7897 RVA: 0x00079160 File Offset: 0x00077360
	public bool Sample(ref double time, out PosRot result)
	{
		int len = this.len;
		if (len == 0)
		{
			result = default(PosRot);
			return false;
		}
		int index;
		double num3;
		if (len != 1)
		{
			int num = 0;
			int num2 = -1;
			for (;;)
			{
				index = this.tbuffer[num].index;
				num3 = this.tbuffer[index].timeStamp;
				if (num3 > time)
				{
					num2 = index;
				}
				else
				{
					if (num3 == time)
					{
						break;
					}
					if (num3 < time)
					{
						goto Block_5;
					}
				}
				if (++num >= this.len)
				{
					goto Block_11;
				}
			}
			result = this.tbuffer[index].value;
			return true;
			Block_5:
			if (num2 == -1)
			{
				if (this.exterpolate && num < this.len - 1)
				{
					num2 = index;
					index = this.tbuffer[num + 1].index;
					double t = (time - this.tbuffer[index].timeStamp) / (this.tbuffer[num2].timeStamp - this.tbuffer[index].timeStamp);
					PosRot.Lerp(ref this.tbuffer[index].value, ref this.tbuffer[num2].value, t, out result);
				}
				else
				{
					result = this.tbuffer[index].value;
				}
			}
			else
			{
				double timeStamp = this.tbuffer[num2].timeStamp;
				double num4 = (double)this.allowDifference + NetCull.sendInterval;
				double num5 = timeStamp - num3;
				if (num5 > num4)
				{
					num3 = timeStamp - (num5 = num4);
					if (num3 >= time)
					{
						result = this.tbuffer[index].value;
						return true;
					}
				}
				double t2 = (time - num3) / num5;
				PosRot.Lerp(ref this.tbuffer[index].value, ref this.tbuffer[num2].value, t2, out result);
			}
			return true;
			Block_11:
			result = this.tbuffer[this.tbuffer[this.len - 1].index].value;
			return true;
		}
		index = this.tbuffer[0].index;
		num3 = this.tbuffer[index].timeStamp;
		result = this.tbuffer[index].value;
		return true;
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x000793C4 File Offset: 0x000775C4
	public bool SampleWorldVelocity(double time, out Vector3 worldLinearVelocity)
	{
		int len = this.len;
		if (len != 0 && len != 1)
		{
			int num = 0;
			int num2 = -1;
			int index;
			double num3;
			for (;;)
			{
				index = this.tbuffer[num].index;
				num3 = this.tbuffer[index].timeStamp;
				if (num3 <= time)
				{
					break;
				}
				num2 = index;
				if (++num >= this.len)
				{
					goto Block_7;
				}
			}
			if (num2 == -1)
			{
				worldLinearVelocity = default(Vector3);
				return false;
			}
			double timeStamp = this.tbuffer[num2].timeStamp;
			double num4 = (double)this.allowDifference + NetCull.sendInterval;
			double num5 = timeStamp - num3;
			if (num5 >= num4)
			{
				num5 = num4;
				num3 = timeStamp - num5;
				if (time <= num3)
				{
					worldLinearVelocity = default(Vector3);
					return false;
				}
			}
			worldLinearVelocity = this.tbuffer[num2].value.position - this.tbuffer[index].value.position;
			worldLinearVelocity.x = (float)((double)worldLinearVelocity.x / num5);
			worldLinearVelocity.y = (float)((double)worldLinearVelocity.y / num5);
			worldLinearVelocity.z = (float)((double)worldLinearVelocity.z / num5);
			return true;
			Block_7:
			worldLinearVelocity = default(Vector3);
			return false;
		}
		worldLinearVelocity = default(Vector3);
		return false;
	}

	// Token: 0x06001EDB RID: 7899 RVA: 0x00079530 File Offset: 0x00077730
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity)
	{
		return this.SampleWorldVelocity(Interpolation.time, out worldLinearVelocity);
	}

	// Token: 0x06001EDC RID: 7900 RVA: 0x00079540 File Offset: 0x00077740
	protected override void Syncronize()
	{
		double time = Interpolation.time;
		PosRot posRot;
		if (this.Sample(ref time, out posRot))
		{
			this.target.position = posRot.position;
			this.target.rotation = posRot.rotation;
		}
	}

	// Token: 0x06001EDD RID: 7901 RVA: 0x00079588 File Offset: 0x00077788
	void SetGoals(ref TimeStamped<PosRot> sample)
	{
		base.SetGoals(ref sample);
	}

	// Token: 0x04000EDD RID: 3805
	public Transform target;

	// Token: 0x04000EDE RID: 3806
	public bool exterpolate;

	// Token: 0x04000EDF RID: 3807
	public float allowDifference = 0.1f;
}
