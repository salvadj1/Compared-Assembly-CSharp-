using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
public sealed class TransformInterpolatorEarly : StateInterpolator<PosRot>, IStateInterpolatorWithLinearVelocity, IStateInterpolator<PosRot>, IStateInterpolatorSampler<PosRot>
{
	// Token: 0x06001EDF RID: 7903 RVA: 0x000795A8 File Offset: 0x000777A8
	public sealed override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		PosRot posRot;
		posRot.position = pos;
		posRot.rotation = rot;
		this.SetGoals(ref posRot, ref timestamp);
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x000795D0 File Offset: 0x000777D0
	public void SetGoals(PosRot frame, double timestamp)
	{
		this.SetGoals(ref frame, ref timestamp);
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x000795DC File Offset: 0x000777DC
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

	// Token: 0x06001EE2 RID: 7906 RVA: 0x00079840 File Offset: 0x00077A40
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

	// Token: 0x06001EE3 RID: 7907 RVA: 0x000799AC File Offset: 0x00077BAC
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity)
	{
		return this.SampleWorldVelocity(Interpolation.time, out worldLinearVelocity);
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x000799BC File Offset: 0x00077BBC
	protected override void Syncronize()
	{
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x000799C0 File Offset: 0x00077BC0
	public void Update()
	{
		if (!base.running)
		{
			return;
		}
		double time = Interpolation.time;
		PosRot posRot;
		if (this.Sample(ref time, out posRot))
		{
			this.target.position = posRot.position;
			this.target.rotation = posRot.rotation;
		}
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x00079A14 File Offset: 0x00077C14
	void SetGoals(ref TimeStamped<PosRot> sample)
	{
		base.SetGoals(ref sample);
	}

	// Token: 0x04000EE0 RID: 3808
	public Transform target;

	// Token: 0x04000EE1 RID: 3809
	public bool exterpolate;

	// Token: 0x04000EE2 RID: 3810
	public float allowDifference = 0.1f;
}
