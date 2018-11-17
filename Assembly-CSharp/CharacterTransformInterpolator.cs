using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
public sealed class CharacterTransformInterpolator : global::CharacterInterpolatorBase<global::CharacterTransformInterpolatorData>, global::IStateInterpolatorSampler<global::CharacterTransformInterpolatorData>, global::IStateInterpolator<global::CharacterTransformInterpolatorData>
{
	// Token: 0x0600087D RID: 2173 RVA: 0x00024458 File Offset: 0x00022658
	public sealed override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		this.SetGoals(pos, (global::Angle2)rot, timestamp);
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x00024468 File Offset: 0x00022668
	public void SetGoals(Vector3 pos, global::Angle2 rot, double timestamp)
	{
		global::CharacterTransformInterpolatorData characterTransformInterpolatorData;
		characterTransformInterpolatorData.origin = pos;
		characterTransformInterpolatorData.eyesAngles = rot;
		base.SetGoals(ref characterTransformInterpolatorData, ref timestamp);
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00024490 File Offset: 0x00022690
	public bool Sample(ref double time, out global::CharacterTransformInterpolatorData result)
	{
		int len = this.len;
		if (len == 0)
		{
			result = default(global::CharacterTransformInterpolatorData);
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
						result.eyesAngles = default(global::Angle2);
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
				double num6 = (double)this.allowableTimeSpan + global::NetCull.sendInterval;
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
					result.eyesAngles = default(global::Angle2);
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

	// Token: 0x06000880 RID: 2176 RVA: 0x00024AF0 File Offset: 0x00022CF0
	public bool SampleWorldVelocity(double time, out Vector3 worldLinearVelocity, out global::Angle2 worldAngularVelocity)
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
				worldAngularVelocity = default(global::Angle2);
				return false;
			}
			double timeStamp = this.tbuffer[num2].timeStamp;
			double num4 = (double)this.allowableTimeSpan + global::NetCull.sendInterval;
			double num5 = timeStamp - num3;
			if (num5 >= num4)
			{
				num5 = num4;
				num3 = timeStamp - num5;
				if (time <= num3)
				{
					worldLinearVelocity = default(Vector3);
					worldAngularVelocity = default(global::Angle2);
					return false;
				}
			}
			worldLinearVelocity = this.tbuffer[num2].value.origin - this.tbuffer[index].value.origin;
			worldAngularVelocity = global::Angle2.Delta(this.tbuffer[index].value.eyesAngles, this.tbuffer[num2].value.eyesAngles);
			worldLinearVelocity.x = (float)((double)worldLinearVelocity.x / num5);
			worldLinearVelocity.y = (float)((double)worldLinearVelocity.y / num5);
			worldLinearVelocity.z = (float)((double)worldLinearVelocity.z / num5);
			worldAngularVelocity.x = (float)((double)worldAngularVelocity.x / num5);
			worldAngularVelocity.y = (float)((double)worldAngularVelocity.y / num5);
			return true;
			Block_7:
			worldLinearVelocity = default(Vector3);
			worldAngularVelocity = default(global::Angle2);
			return false;
		}
		worldLinearVelocity = default(Vector3);
		worldAngularVelocity = default(global::Angle2);
		return false;
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00024CF4 File Offset: 0x00022EF4
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity, out global::Angle2 worldAngularVelocity)
	{
		return this.SampleWorldVelocity(global::Interpolation.time, out worldLinearVelocity, out worldAngularVelocity);
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00024D04 File Offset: 0x00022F04
	public bool SampleWorldVelocity(double time, out global::Angle2 worldAngularVelocity)
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
				worldAngularVelocity = default(global::Angle2);
				return false;
			}
			double timeStamp = this.tbuffer[num2].timeStamp;
			double num4 = (double)this.allowableTimeSpan + global::NetCull.sendInterval;
			double num5 = timeStamp - num3;
			if (num5 >= num4)
			{
				num5 = num4;
				num3 = timeStamp - num5;
				if (time <= num3)
				{
					worldAngularVelocity = default(global::Angle2);
					return false;
				}
			}
			worldAngularVelocity = global::Angle2.Delta(this.tbuffer[index].value.eyesAngles, this.tbuffer[num2].value.eyesAngles);
			worldAngularVelocity.x = (float)((double)worldAngularVelocity.x / num5);
			worldAngularVelocity.y = (float)((double)worldAngularVelocity.y / num5);
			return true;
			Block_7:
			worldAngularVelocity = default(global::Angle2);
			return false;
		}
		worldAngularVelocity = default(global::Angle2);
		return false;
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00024E60 File Offset: 0x00023060
	public bool SampleWorldVelocity(out global::Angle2 worldAngularVelocity)
	{
		return this.SampleWorldVelocity(global::Interpolation.time, out worldAngularVelocity);
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x00024E70 File Offset: 0x00023070
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
			double num4 = (double)this.allowableTimeSpan + global::NetCull.sendInterval;
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

	// Token: 0x06000885 RID: 2181 RVA: 0x00024FDC File Offset: 0x000231DC
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity)
	{
		return this.SampleWorldVelocity(global::Interpolation.time, out worldLinearVelocity);
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00024FEC File Offset: 0x000231EC
	private void Update()
	{
		double time = global::Interpolation.time;
		global::CharacterTransformInterpolatorData characterTransformInterpolatorData;
		if (this.Sample(ref time, out characterTransformInterpolatorData))
		{
			global::Character idMain = base.idMain;
			if (idMain)
			{
				idMain.origin = characterTransformInterpolatorData.origin;
				idMain.eyesAngles = characterTransformInterpolatorData.eyesAngles;
			}
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0002503C File Offset: 0x0002323C
	void SetGoals(ref global::CharacterTransformInterpolatorData sample, ref double timeStamp)
	{
		base.SetGoals(ref sample, ref timeStamp);
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00025048 File Offset: 0x00023248
	void SetGoals(ref global::TimeStamped<global::CharacterTransformInterpolatorData> sample)
	{
		base.SetGoals(ref sample);
	}

	// Token: 0x04000637 RID: 1591
	private bool once;
}
