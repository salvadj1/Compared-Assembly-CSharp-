using System;
using UnityEngine;

// Token: 0x020003C3 RID: 963
public sealed class RigidbodyInterpolator : global::StateInterpolator<global::PosRot>, global::IStateInterpolatorWithLinearVelocity, global::IStateInterpolator<global::PosRot>, global::IStateInterpolatorSampler<global::PosRot>
{
	// Token: 0x060021CA RID: 8650 RVA: 0x0007C6FC File Offset: 0x0007A8FC
	public sealed override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		global::PosRot posRot;
		posRot.position = pos;
		posRot.rotation = rot;
		this.SetGoals(ref posRot, ref timestamp);
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x0007C724 File Offset: 0x0007A924
	public void SetGoals(global::PosRot frame, double timestamp)
	{
		this.SetGoals(ref frame, ref timestamp);
	}

	// Token: 0x060021CC RID: 8652 RVA: 0x0007C730 File Offset: 0x0007A930
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

	// Token: 0x060021CD RID: 8653 RVA: 0x0007C994 File Offset: 0x0007AB94
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

	// Token: 0x060021CE RID: 8654 RVA: 0x0007CB00 File Offset: 0x0007AD00
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity)
	{
		return this.SampleWorldVelocity(global::Interpolation.time, out worldLinearVelocity);
	}

	// Token: 0x060021CF RID: 8655 RVA: 0x0007CB10 File Offset: 0x0007AD10
	protected override void Syncronize()
	{
		double time = global::Interpolation.time;
		global::PosRot posRot;
		if (this.Sample(ref time, out posRot))
		{
			this.target.MovePosition(posRot.position);
			this.target.MoveRotation(posRot.rotation);
		}
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x0007CB58 File Offset: 0x0007AD58
	void SetGoals(ref global::TimeStamped<global::PosRot> sample)
	{
		base.SetGoals(ref sample);
	}

	// Token: 0x04001004 RID: 4100
	public Rigidbody target;

	// Token: 0x04001005 RID: 4101
	public bool exterpolate;

	// Token: 0x04001006 RID: 4102
	public float allowDifference = 0.1f;
}
