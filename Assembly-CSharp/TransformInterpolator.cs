using System;
using UnityEngine;

// Token: 0x020003D3 RID: 979
public sealed class TransformInterpolator : global::StateInterpolator<global::PosRot>, global::IStateInterpolatorWithLinearVelocity, global::IStateInterpolator<global::PosRot>, global::IStateInterpolatorSampler<global::PosRot>
{
	// Token: 0x06002239 RID: 8761 RVA: 0x0007E528 File Offset: 0x0007C728
	public sealed override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		global::PosRot posRot;
		posRot.position = pos;
		posRot.rotation = rot;
		this.SetGoals(ref posRot, ref timestamp);
	}

	// Token: 0x0600223A RID: 8762 RVA: 0x0007E550 File Offset: 0x0007C750
	public void SetGoals(global::PosRot frame, double timestamp)
	{
		this.SetGoals(ref frame, ref timestamp);
	}

	// Token: 0x0600223B RID: 8763 RVA: 0x0007E55C File Offset: 0x0007C75C
	public bool Sample(ref double time, out global::PosRot result)
	{
		int len = this.len;
		if (len == 0)
		{
			result = default(global::PosRot);
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
					global::PosRot.Lerp(ref this.tbuffer[index].value, ref this.tbuffer[num2].value, t, out result);
				}
				else
				{
					result = this.tbuffer[index].value;
				}
			}
			else
			{
				double timeStamp = this.tbuffer[num2].timeStamp;
				double num4 = (double)this.allowDifference + global::NetCull.sendInterval;
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
				global::PosRot.Lerp(ref this.tbuffer[index].value, ref this.tbuffer[num2].value, t2, out result);
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

	// Token: 0x0600223C RID: 8764 RVA: 0x0007E7C0 File Offset: 0x0007C9C0
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
			double num4 = (double)this.allowDifference + global::NetCull.sendInterval;
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

	// Token: 0x0600223D RID: 8765 RVA: 0x0007E92C File Offset: 0x0007CB2C
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity)
	{
		return this.SampleWorldVelocity(global::Interpolation.time, out worldLinearVelocity);
	}

	// Token: 0x0600223E RID: 8766 RVA: 0x0007E93C File Offset: 0x0007CB3C
	protected override void Syncronize()
	{
		double time = global::Interpolation.time;
		global::PosRot posRot;
		if (this.Sample(ref time, out posRot))
		{
			this.target.position = posRot.position;
			this.target.rotation = posRot.rotation;
		}
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x0007E984 File Offset: 0x0007CB84
	void SetGoals(ref global::TimeStamped<global::PosRot> sample)
	{
		base.SetGoals(ref sample);
	}

	// Token: 0x04001043 RID: 4163
	public Transform target;

	// Token: 0x04001044 RID: 4164
	public bool exterpolate;

	// Token: 0x04001045 RID: 4165
	public float allowDifference = 0.1f;
}
