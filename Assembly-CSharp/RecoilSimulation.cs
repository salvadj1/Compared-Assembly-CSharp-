using System;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class RecoilSimulation : IDLocalCharacter
{
	// Token: 0x06000A21 RID: 2593 RVA: 0x00028714 File Offset: 0x00026914
	private void LateUpdate()
	{
		Angle2 angles;
		if (this.ExtractRecoil(out angles))
		{
			base.ApplyAdditiveEyeAngles(angles);
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00028738 File Offset: 0x00026938
	private bool ExtractRecoil(out Angle2 offset)
	{
		offset = default(Angle2);
		if (this.recoilImpulses != null)
		{
			int count = this.recoilImpulses.Count;
			if (count > 0)
			{
				float deltaTime = Time.deltaTime;
				RecoilSimulation.Recoil[] buffer = this.recoilImpulses.Buffer;
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

	// Token: 0x06000A23 RID: 2595 RVA: 0x00028824 File Offset: 0x00026A24
	public void AddRecoil(float duration, float pitch, float yaw)
	{
		Angle2 angle = default(Angle2);
		angle.pitch = pitch;
		angle.yaw = yaw;
		this.AddRecoil(duration, ref angle);
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00028854 File Offset: 0x00026A54
	public void AddRecoil(float duration, float pitch)
	{
		Angle2 angle = default(Angle2);
		angle.pitch = pitch;
		this.AddRecoil(duration, ref angle);
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0002887C File Offset: 0x00026A7C
	public void AddRecoil(float duration, Angle2 angle)
	{
		this.AddRecoil(duration, ref angle);
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00028888 File Offset: 0x00026A88
	public void AddRecoil(float duration, ref Angle2 angle2)
	{
		if (duration > 0f && (angle2.pitch != 0f || angle2.yaw != 0f))
		{
			if (this.recoilImpulses == null)
			{
				this.recoilImpulses = new GrabBag<RecoilSimulation.Recoil>(4);
				Debug.Log("Created GrabBag<Recoil>", this);
			}
			if (this.recoilImpulses.Add(new RecoilSimulation.Recoil(ref angle2, duration)) == 0)
			{
				base.enabled = true;
			}
		}
	}

	// Token: 0x0400068B RID: 1675
	[NonSerialized]
	private GrabBag<RecoilSimulation.Recoil> recoilImpulses;

	// Token: 0x0200014D RID: 333
	private struct Recoil
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x00028900 File Offset: 0x00026B00
		public Recoil(ref Angle2 angle, float duration)
		{
			this.angle = angle;
			this.timeScale = 1f / duration;
			this.fraction = 0f;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00028934 File Offset: 0x00026B34
		public bool Extract(ref Angle2 sum, float deltaTime)
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

		// Token: 0x0400068C RID: 1676
		public Angle2 angle;

		// Token: 0x0400068D RID: 1677
		public float fraction;

		// Token: 0x0400068E RID: 1678
		public float timeScale;
	}
}
