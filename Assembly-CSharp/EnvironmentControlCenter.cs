using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class EnvironmentControlCenter : NetBehaviour
{
	// Token: 0x060002BD RID: 701 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
	protected void Awake()
	{
		EnvironmentControlCenter.Singleton = this;
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000E5BC File Offset: 0x0000C7BC
	private void OnDestroy()
	{
		if (EnvironmentControlCenter.Singleton == this)
		{
			EnvironmentControlCenter.Singleton = null;
		}
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
	[RPC]
	private void CL_UpdateSkyState(BitStream stream)
	{
		env.daylength = stream.Read<float>(new object[0]);
		env.nightlength = stream.Read<float>(new object[0]);
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

	// Token: 0x060002C0 RID: 704 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
	public bool IsNight()
	{
		return this.sky && this.sky.IsNight;
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0000E710 File Offset: 0x0000C910
	public float GetTime()
	{
		if (this.sky == null)
		{
			return 0f;
		}
		return this.sky.Cycle.Hour;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000E73C File Offset: 0x0000C93C
	protected void Update()
	{
		if (this.sky == null)
		{
			this.sky = (TOD_Sky)Object.FindObjectOfType(typeof(TOD_Sky));
			if (this.sky == null)
			{
				return;
			}
		}
		float num = env.daylength * 60f;
		if (this.sky.IsNight)
		{
			num = env.nightlength * 60f;
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

	// Token: 0x040001B2 RID: 434
	public static EnvironmentControlCenter Singleton;

	// Token: 0x040001B3 RID: 435
	private TOD_Sky sky;
}
