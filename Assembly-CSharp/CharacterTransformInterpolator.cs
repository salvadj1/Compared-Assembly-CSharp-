using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
public sealed class CharacterTransformInterpolator : CharacterInterpolatorBase<CharacterTransformInterpolatorData>, IStateInterpolatorSampler<CharacterTransformInterpolatorData>, IStateInterpolator<CharacterTransformInterpolatorData>
{
	// Token: 0x060007AB RID: 1963 RVA: 0x00021884 File Offset: 0x0001FA84
	public sealed override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		this.SetGoals(pos, (Angle2)rot, timestamp);
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x00021894 File Offset: 0x0001FA94
	public void SetGoals(Vector3 pos, Angle2 rot, double timestamp)
	{
		CharacterTransformInterpolatorData characterTransformInterpolatorData;
		characterTransformInterpolatorData.origin = pos;
		characterTransformInterpolatorData.eyesAngles = rot;
		base.SetGoals(ref characterTransformInterpolatorData, ref timestamp);
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x000218BC File Offset: 0x0001FABC
	public bool Sample(ref double time, out CharacterTransformInterpolatorData result)
	{
		int len = this.len;
		if (len == 0)
		{
			result = default(CharacterTransformInterpolatorData);
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
					goto Block_15;
				}
			}
			result = this.tbuffer[index].value;
			return true;
			Block_5:
			if (num2 == -1)
			{
				if (this.extrapolate && num < this.len - 1)
				{
					num2 = index;
					index = this.tbuffer[num + 1].index;
					double num4 = (num3 - this.tbuffer[index].timeStamp) / (num3 - this.tbuffer[index].timeStamp);
					if (num4 == 0.0)
					{
						result = this.tbuffer[index].value;
					}
					else if (num4 == 1.0)
					{
						result = this.tbuffer[num2].value;
					}
					else
					{
						double num5 = 1.0 - num4;
						result.origin.x = (float)((double)this.tbuffer[index].value.origin.x * num5 + (double)this.tbuffer[num2].value.origin.x * num4);
						result.origin.y = (float)((double)this.tbuffer[index].value.origin.y * num5 + (double)this.tbuffer[num2].value.origin.y * num4);
						result.origin.z = (float)((double)this.tbuffer[index].value.origin.z * num5 + (double)this.tbuffer[num2].value.origin.z * num4);
						result.eyesAngles = default(Angle2);
						result.eyesAngles.yaw = (float)((double)this.tbuffer[index].value.eyesAngles.yaw + (double)Mathf.DeltaAngle(this.tbuffer[index].value.eyesAngles.yaw, this.tbuffer[num2].value.eyesAngles.yaw) * num4);
						result.eyesAngles.pitch = Mathf.DeltaAngle(0f, (float)((double)this.tbuffer[index].value.eyesAngles.pitch + (double)Mathf.DeltaAngle(this.tbuffer[index].value.eyesAngles.pitch, this.tbuffer[num2].value.eyesAngles.pitch) * num4));
					}
				}
				else
				{
					result = this.tbuffer[index].value;
				}
			}
			else
			{
				double timeStamp = this.tbuffer[num2].timeStamp;
				double num6 = (double)this.allowableTimeSpan + NetCull.sendInterval;
				double num7 = timeStamp - num3;
				if (num7 > num6)
				{
					num3 = timeStamp - (num7 = num6);
					if (num3 >= time)
					{
						result = this.tbuffer[index].value;
						return true;
					}
				}
				double num8 = (time - num3) / num7;
				if (num8 == 0.0)
				{
					result = this.tbuffer[index].value;
				}
				else if (num8 == 1.0)
				{
					result = this.tbuffer[num2].value;
				}
				else
				{
					double num9 = 1.0 - num8;
					result.origin.x = (float)((double)this.tbuffer[index].value.origin.x * num9 + (double)this.tbuffer[num2].value.origin.x * num8);
					result.origin.y = (float)((double)this.tbuffer[index].value.origin.y * num9 + (double)this.tbuffer[num2].value.origin.y * num8);
					result.origin.z = (float)((double)this.tbuffer[index].value.origin.z * num9 + (double)this.tbuffer[num2].value.origin.z * num8);
					result.eyesAngles = default(Angle2);
					result.eyesAngles.yaw = (float)((double)this.tbuffer[index].value.eyesAngles.yaw + (double)Mathf.DeltaAngle(this.tbuffer[index].value.eyesAngles.yaw, this.tbuffer[num2].value.eyesAngles.yaw) * num8);
					result.eyesAngles.pitch = Mathf.DeltaAngle(0f, (float)((double)this.tbuffer[index].value.eyesAngles.pitch + (double)Mathf.DeltaAngle(this.tbuffer[index].value.eyesAngles.pitch, this.tbuffer[num2].value.eyesAngles.pitch) * num8));
				}
			}
			return true;
			Block_15:
			result = this.tbuffer[this.tbuffer[this.len - 1].index].value;
			return true;
		}
		index = this.tbuffer[0].index;
		num3 = this.tbuffer[index].timeStamp;
		result = this.tbuffer[index].value;
		return true;
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00021F1C File Offset: 0x0002011C
	public bool SampleWorldVelocity(double time, out Vector3 worldLinearVelocity, out Angle2 worldAngularVelocity)
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
				worldAngularVelocity = default(Angle2);
				return false;
			}
			double timeStamp = this.tbuffer[num2].timeStamp;
			double num4 = (double)this.allowableTimeSpan + NetCull.sendInterval;
			double num5 = timeStamp - num3;
			if (num5 >= num4)
			{
				num5 = num4;
				num3 = timeStamp - num5;
				if (time <= num3)
				{
					worldLinearVelocity = default(Vector3);
					worldAngularVelocity = default(Angle2);
					return false;
				}
			}
			worldLinearVelocity = this.tbuffer[num2].value.origin - this.tbuffer[index].value.origin;
			worldAngularVelocity = Angle2.Delta(this.tbuffer[index].value.eyesAngles, this.tbuffer[num2].value.eyesAngles);
			worldLinearVelocity.x = (float)((double)worldLinearVelocity.x / num5);
			worldLinearVelocity.y = (float)((double)worldLinearVelocity.y / num5);
			worldLinearVelocity.z = (float)((double)worldLinearVelocity.z / num5);
			worldAngularVelocity.x = (float)((double)worldAngularVelocity.x / num5);
			worldAngularVelocity.y = (float)((double)worldAngularVelocity.y / num5);
			return true;
			Block_7:
			worldLinearVelocity = default(Vector3);
			worldAngularVelocity = default(Angle2);
			return false;
		}
		worldLinearVelocity = default(Vector3);
		worldAngularVelocity = default(Angle2);
		return false;
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00022120 File Offset: 0x00020320
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity, out Angle2 worldAngularVelocity)
	{
		return this.SampleWorldVelocity(Interpolation.time, out worldLinearVelocity, out worldAngularVelocity);
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00022130 File Offset: 0x00020330
	public bool SampleWorldVelocity(double time, out Angle2 worldAngularVelocity)
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
				worldAngularVelocity = default(Angle2);
				return false;
			}
			double timeStamp = this.tbuffer[num2].timeStamp;
			double num4 = (double)this.allowableTimeSpan + NetCull.sendInterval;
			double num5 = timeStamp - num3;
			if (num5 >= num4)
			{
				num5 = num4;
				num3 = timeStamp - num5;
				if (time <= num3)
				{
					worldAngularVelocity = default(Angle2);
					return false;
				}
			}
			worldAngularVelocity = Angle2.Delta(this.tbuffer[index].value.eyesAngles, this.tbuffer[num2].value.eyesAngles);
			worldAngularVelocity.x = (float)((double)worldAngularVelocity.x / num5);
			worldAngularVelocity.y = (float)((double)worldAngularVelocity.y / num5);
			return true;
			Block_7:
			worldAngularVelocity = default(Angle2);
			return false;
		}
		worldAngularVelocity = default(Angle2);
		return false;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0002228C File Offset: 0x0002048C
	public bool SampleWorldVelocity(out Angle2 worldAngularVelocity)
	{
		return this.SampleWorldVelocity(Interpolation.time, out worldAngularVelocity);
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0002229C File Offset: 0x0002049C
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
			double num4 = (double)this.allowableTimeSpan + NetCull.sendInterval;
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
			worldLinearVelocity = this.tbuffer[num2].value.origin - this.tbuffer[index].value.origin;
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

	// Token: 0x060007B3 RID: 1971 RVA: 0x00022408 File Offset: 0x00020608
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity)
	{
		return this.SampleWorldVelocity(Interpolation.time, out worldLinearVelocity);
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00022418 File Offset: 0x00020618
	private void Update()
	{
		double time = Interpolation.time;
		CharacterTransformInterpolatorData characterTransformInterpolatorData;
		if (this.Sample(ref time, out characterTransformInterpolatorData))
		{
			Character idMain = base.idMain;
			if (idMain)
			{
				idMain.origin = characterTransformInterpolatorData.origin;
				idMain.eyesAngles = characterTransformInterpolatorData.eyesAngles;
			}
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00022468 File Offset: 0x00020668
	void SetGoals(ref CharacterTransformInterpolatorData sample, ref double timeStamp)
	{
		base.SetGoals(ref sample, ref timeStamp);
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00022474 File Offset: 0x00020674
	void SetGoals(ref TimeStamped<CharacterTransformInterpolatorData> sample)
	{
		base.SetGoals(ref sample);
	}

	// Token: 0x0400056C RID: 1388
	private bool once;
}
