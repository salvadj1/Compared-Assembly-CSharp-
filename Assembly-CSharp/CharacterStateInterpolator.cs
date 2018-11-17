using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
public sealed class CharacterStateInterpolator : CharacterInterpolatorBase<CharacterStateInterpolatorData>, IStateInterpolator<CharacterStateInterpolatorData>, IStateInterpolatorWithLinearVelocity, IStateInterpolatorWithAngularVelocity, IStateInterpolatorWithVelocity, IStateInterpolatorSampler<CharacterStateInterpolatorData>, IStateInterpolatorSampler<CharacterTransformInterpolatorData>
{
	// Token: 0x06000795 RID: 1941 RVA: 0x00020998 File Offset: 0x0001EB98
	public sealed override void SetGoals(Vector3 pos, Quaternion rot, double timestamp)
	{
		this.SetGoals(pos, (Angle2)rot, base.idMain.stateFlags, timestamp);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x000209C0 File Offset: 0x0001EBC0
	public void SetGoals(Vector3 pos, Angle2 rot, CharacterStateFlags state, double timestamp)
	{
		CharacterStateInterpolatorData characterStateInterpolatorData;
		characterStateInterpolatorData.origin = pos;
		characterStateInterpolatorData.eyesAngles = rot;
		characterStateInterpolatorData.state = state;
		base.SetGoals(ref characterStateInterpolatorData, ref timestamp);
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x000209F0 File Offset: 0x0001EBF0
	public bool Sample(ref double time, out CharacterTransformInterpolatorData result)
	{
		CharacterStateInterpolatorData characterStateInterpolatorData;
		bool result2 = this.Sample(ref time, out characterStateInterpolatorData);
		result.eyesAngles = characterStateInterpolatorData.eyesAngles;
		result.origin = characterStateInterpolatorData.origin;
		return result2;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00020A24 File Offset: 0x0001EC24
	public bool Sample(ref double time, out CharacterStateInterpolatorData result)
	{
		int len = this.len;
		if (len == 0)
		{
			result = default(CharacterStateInterpolatorData);
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
					goto Block_21;
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
						if (num4 > 1.0)
						{
							result.state = this.tbuffer[num2].value.state;
						}
						else if (num4 < 0.0)
						{
							result.state = this.tbuffer[index].value.state;
						}
						else
						{
							result.state = this.tbuffer[index].value.state;
							result.state.flags = (result.state.flags | (ushort)((byte)(this.tbuffer[num2].value.state.flags & 67)));
							if (result.state.grounded != this.tbuffer[num2].value.state.grounded)
							{
								result.state.grounded = false;
							}
						}
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
					if (num8 > 1.0)
					{
						result.state = this.tbuffer[num2].value.state;
					}
					else if (num8 < 0.0)
					{
						result.state = this.tbuffer[index].value.state;
					}
					else
					{
						result.state = this.tbuffer[index].value.state;
						result.state.flags = (result.state.flags | (ushort)((byte)(this.tbuffer[num2].value.state.flags & 67)));
						if (result.state.grounded != this.tbuffer[num2].value.state.grounded)
						{
							result.state.grounded = false;
						}
					}
				}
			}
			return true;
			Block_21:
			result = this.tbuffer[this.tbuffer[this.len - 1].index].value;
			return true;
		}
		index = this.tbuffer[0].index;
		num3 = this.tbuffer[index].timeStamp;
		result = this.tbuffer[index].value;
		return true;
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00021254 File Offset: 0x0001F454
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

	// Token: 0x0600079A RID: 1946 RVA: 0x00021458 File Offset: 0x0001F658
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

	// Token: 0x0600079B RID: 1947 RVA: 0x000215C4 File Offset: 0x0001F7C4
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity)
	{
		return this.SampleWorldVelocity(Interpolation.time, out worldLinearVelocity);
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x000215D4 File Offset: 0x0001F7D4
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

	// Token: 0x0600079D RID: 1949 RVA: 0x00021730 File Offset: 0x0001F930
	public bool SampleWorldVelocity(out Angle2 worldAngularVelocity)
	{
		return this.SampleWorldVelocity(Interpolation.time, out worldAngularVelocity);
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x00021740 File Offset: 0x0001F940
	public bool SampleWorldVelocity(out Vector3 worldLinearVelocity, out Angle2 worldAngularVelocity)
	{
		return this.SampleWorldVelocity(Interpolation.time, out worldLinearVelocity, out worldAngularVelocity);
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00021750 File Offset: 0x0001F950
	protected override void Syncronize()
	{
		double time = Interpolation.time;
		CharacterStateInterpolatorData characterStateInterpolatorData;
		if (this.Sample(ref time, out characterStateInterpolatorData))
		{
			Character idMain = base.idMain;
			if (idMain)
			{
				idMain.origin = characterStateInterpolatorData.origin;
				idMain.eyesAngles = characterStateInterpolatorData.eyesAngles;
				CharacterStateFlags stateFlags = idMain.stateFlags;
				idMain.stateFlags = characterStateInterpolatorData.state;
				if (!stateFlags.Equals(characterStateInterpolatorData.state))
				{
					if (!this.once)
					{
						idMain.Signal_State_FlagsChanged(true);
						this.once = true;
					}
					else
					{
						idMain.Signal_State_FlagsChanged(false);
					}
				}
			}
		}
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x000217EC File Offset: 0x0001F9EC
	void SetGoals(ref CharacterStateInterpolatorData sample, ref double timeStamp)
	{
		base.SetGoals(ref sample, ref timeStamp);
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x000217F8 File Offset: 0x0001F9F8
	void SetGoals(ref TimeStamped<CharacterStateInterpolatorData> sample)
	{
		base.SetGoals(ref sample);
	}

	// Token: 0x04000564 RID: 1380
	private bool once;
}
