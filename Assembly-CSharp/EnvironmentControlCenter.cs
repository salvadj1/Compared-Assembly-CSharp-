using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class EnvironmentControlCenter : NetBehaviour
{
	// Token: 0x0600032F RID: 815 RVA: 0x0000FB5C File Offset: 0x0000DD5C
	protected void Awake()
	{
		global::EnvironmentControlCenter.Singleton = this;
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0000FB64 File Offset: 0x0000DD64
	private void OnDestroy()
	{
		if (global::EnvironmentControlCenter.Singleton == this)
		{
			global::EnvironmentControlCenter.Singleton = null;
		}
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0000FB7C File Offset: 0x0000DD7C
	[RPC]
	private void CL_UpdateSkyState(BitStream stream)
	{
		global::env.daylength = stream.Read<float>(new object[0]);
		global::env.nightlength = stream.Read<float>(new object[0]);
		this.sky.Cycle.MoonPhase = stream.Read<float>(new object[0]);
		this.sky.Components.Animation.CloudUV = stream.Read<Vector4>(new object[0]);
		this.sky.Cycle.Year = stream.Read<int>(new object[0]);
		this.sky.Cycle.Month = (int)stream.Read<byte>(new object[0]);
		this.sky.Cycle.Day = (int)stream.Read<byte>(new object[0]);
		float num = stream.Read<float>(new object[0]);
		if (Mathf.Abs(this.sky.Cycle.Hour - num) > 0.0166666675f && this.sky.Cycle.Hour > 0.05f)
		{
			this.sky.Cycle.Hour = num;
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0000FC98 File Offset: 0x0000DE98
	public bool IsNight()
	{
		return this.sky && this.sky.IsNight;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
	public float GetTime()
	{
		if (this.sky == null)
		{
			return 0f;
		}
		return this.sky.Cycle.Hour;
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0000FCE4 File Offset: 0x0000DEE4
	protected void Update()
	{
		if (this.sky == null)
		{
			this.sky = (global::TOD_Sky)Object.FindObjectOfType(typeof(global::TOD_Sky));
			if (this.sky == null)
			{
				return;
			}
		}
		float num = global::env.daylength * 60f;
		if (this.sky.IsNight)
		{
			num = global::env.nightlength * 60f;
		}
		float num2 = num / 24f;
		float num3 = Time.deltaTime / num2;
		float num4 = Time.deltaTime / (30f * num) * 2f;
		this.sky.Cycle.Hour += num3;
		this.sky.Cycle.MoonPhase += num4;
		if (this.sky.Cycle.MoonPhase < -1f)
		{
			this.sky.Cycle.MoonPhase += 2f;
		}
		else if (this.sky.Cycle.MoonPhase > 1f)
		{
			this.sky.Cycle.MoonPhase -= 2f;
		}
		if (this.sky.Cycle.Hour >= 24f)
		{
			this.sky.Cycle.Hour = 0f;
			int num5 = DateTime.DaysInMonth(this.sky.Cycle.Year, this.sky.Cycle.Month);
			if (++this.sky.Cycle.Day > num5)
			{
				this.sky.Cycle.Day = 1;
				if (++this.sky.Cycle.Month > 12)
				{
					this.sky.Cycle.Month = 1;
					this.sky.Cycle.Year++;
				}
			}
		}
	}

	// Token: 0x04000214 RID: 532
	public static global::EnvironmentControlCenter Singleton;

	// Token: 0x04000215 RID: 533
	private global::TOD_Sky sky;
}
