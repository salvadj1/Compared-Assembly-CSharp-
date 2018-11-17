using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class RecoilSimulation : global::IDLocalCharacter
{
	// Token: 0x06000B47 RID: 2887 RVA: 0x0002C490 File Offset: 0x0002A690
	private void LateUpdate()
	{
		global::Angle2 angles;
		if (this.ExtractRecoil(out angles))
		{
			base.ApplyAdditiveEyeAngles(angles);
		}
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x0002C4B4 File Offset: 0x0002A6B4
	private bool ExtractRecoil(out global::Angle2 offset)
	{
		offset = default(global::Angle2);
		if (this.recoilImpulses != null)
		{
			int count = this.recoilImpulses.Count;
			if (count > 0)
			{
				float deltaTime = Time.deltaTime;
				global::RecoilSimulation.Recoil[] buffer = this.recoilImpulses.Buffer;
				for (int i = count - 1; i >= 0; i--)
				{
					if (buffer[i].Extract(ref offset, deltaTime))
					{
						this.recoilImpulses.RemoveAt(i);
						while (--i >= 0)
						{
							if (buffer[i].Extract(ref offset, deltaTime))
							{
								this.recoilImpulses.RemoveAt(i);
							}
						}
						if (this.recoilImpulses.Count == 0)
						{
							base.enabled = false;
						}
					}
				}
				return offset.pitch != 0f || offset.yaw != 0f;
			}
		}
		return false;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
	public void AddRecoil(float duration, float pitch, float yaw)
	{
		global::Angle2 angle = default(global::Angle2);
		angle.pitch = pitch;
		angle.yaw = yaw;
		this.AddRecoil(duration, ref angle);
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x0002C5D0 File Offset: 0x0002A7D0
	public void AddRecoil(float duration, float pitch)
	{
		global::Angle2 angle = default(global::Angle2);
		angle.pitch = pitch;
		this.AddRecoil(duration, ref angle);
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x0002C5F8 File Offset: 0x0002A7F8
	public void AddRecoil(float duration, global::Angle2 angle)
	{
		this.AddRecoil(duration, ref angle);
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x0002C604 File Offset: 0x0002A804
	public void AddRecoil(float duration, ref global::Angle2 angle2)
	{
		if (duration > 0f && (angle2.pitch != 0f || angle2.yaw != 0f))
		{
			if (this.recoilImpulses == null)
			{
				this.recoilImpulses = new global::GrabBag<global::RecoilSimulation.Recoil>(4);
				Debug.Log("Created GrabBag<Recoil>", this);
			}
			if (this.recoilImpulses.Add(new global::RecoilSimulation.Recoil(ref angle2, duration)) == 0)
			{
				base.enabled = true;
			}
		}
	}

	// Token: 0x0400079A RID: 1946
	[NonSerialized]
	private global::GrabBag<global::RecoilSimulation.Recoil> recoilImpulses;

	// Token: 0x02000177 RID: 375
	private struct Recoil
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x0002C67C File Offset: 0x0002A87C
		public Recoil(ref global::Angle2 angle, float duration)
		{
			this.angle = angle;
			this.timeScale = 1f / duration;
			this.fraction = 0f;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002C6B0 File Offset: 0x0002A8B0
		public bool Extract(ref global::Angle2 sum, float deltaTime)
		{
			float num = this.fraction + (this.fraction - this.fraction * this.fraction);
			this.fraction += deltaTime * this.timeScale;
			if (this.fraction >= 1f)
			{
				num = 1f - num;
				sum.pitch += this.angle.pitch * num;
				sum.yaw += this.angle.yaw * num;
				return true;
			}
			num = this.fraction + (this.fraction - this.fraction * this.fraction) - num;
			sum.pitch += this.angle.pitch * num;
			sum.yaw += this.angle.yaw * num;
			return false;
		}

		// Token: 0x0400079B RID: 1947
		public global::Angle2 angle;

		// Token: 0x0400079C RID: 1948
		public float fraction;

		// Token: 0x0400079D RID: 1949
		public float timeScale;
	}
}
