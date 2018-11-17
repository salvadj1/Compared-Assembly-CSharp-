using System;
using UnityEngine;

// Token: 0x02000353 RID: 851
public struct Matrix4x4Decomp
{
	// Token: 0x06002105 RID: 8453 RVA: 0x000812C8 File Offset: 0x0007F4C8
	public Matrix4x4Decomp(Matrix4x4 v)
	{
		this.r.x = v.m00;
		this.r.y = v.m01;
		this.r.z = v.m02;
		this.s.x = v.m03;
		this.u.x = v.m10;
		this.u.y = v.m11;
		this.u.z = v.m12;
		this.s.y = v.m13;
		this.f.x = v.m20;
		this.f.y = v.m21;
		this.f.z = v.m22;
		this.s.z = v.m23;
		this.t.x = v.m30;
		this.t.y = v.m31;
		this.t.z = v.m32;
		this.s.w = v.m33;
	}

	// Token: 0x1700081F RID: 2079
	// (get) Token: 0x06002106 RID: 8454 RVA: 0x000813F8 File Offset: 0x0007F5F8
	// (set) Token: 0x06002107 RID: 8455 RVA: 0x00081528 File Offset: 0x0007F728
	public Matrix4x4 m
	{
		get
		{
			Matrix4x4 result;
			result.m00 = this.r.x;
			result.m01 = this.r.y;
			result.m02 = this.r.z;
			result.m03 = this.s.x;
			result.m10 = this.u.x;
			result.m11 = this.u.y;
			result.m12 = this.u.z;
			result.m13 = this.s.y;
			result.m20 = this.f.x;
			result.m21 = this.f.y;
			result.m22 = this.f.z;
			result.m23 = this.s.z;
			result.m30 = this.t.x;
			result.m31 = this.t.y;
			result.m32 = this.t.z;
			result.m33 = this.s.w;
			return result;
		}
		set
		{
			this.r.x = value.m00;
			this.r.y = value.m01;
			this.r.z = value.m02;
			this.s.x = value.m03;
			this.u.x = value.m10;
			this.u.y = value.m11;
			this.u.z = value.m12;
			this.s.y = value.m13;
			this.f.x = value.m20;
			this.f.y = value.m21;
			this.f.z = value.m22;
			this.s.z = value.m23;
			this.t.x = value.m30;
			this.t.y = value.m31;
			this.t.z = value.m32;
			this.s.w = value.m33;
		}
	}

	// Token: 0x17000820 RID: 2080
	// (get) Token: 0x06002108 RID: 8456 RVA: 0x00081658 File Offset: 0x0007F858
	// (set) Token: 0x06002109 RID: 8457 RVA: 0x0008166C File Offset: 0x0007F86C
	public Quaternion q
	{
		get
		{
			return Quaternion.LookRotation(this.f, this.u);
		}
		set
		{
			Quaternion quaternion = value * Quaternion.Inverse(this.q);
			this.r = quaternion * this.r;
			this.u = quaternion * this.u;
			this.f = quaternion * this.f;
		}
	}

	// Token: 0x17000821 RID: 2081
	// (get) Token: 0x0600210A RID: 8458 RVA: 0x000816C4 File Offset: 0x0007F8C4
	// (set) Token: 0x0600210B RID: 8459 RVA: 0x00081708 File Offset: 0x0007F908
	public Vector3 S
	{
		get
		{
			Vector3 result;
			result.x = this.s.x;
			result.y = this.s.y;
			result.z = this.s.z;
			return result;
		}
		set
		{
			this.s.x = value.x;
			this.s.y = value.y;
			this.s.z = value.z;
		}
	}

	// Token: 0x17000822 RID: 2082
	// (get) Token: 0x0600210C RID: 8460 RVA: 0x0008174C File Offset: 0x0007F94C
	// (set) Token: 0x0600210D RID: 8461 RVA: 0x0008175C File Offset: 0x0007F95C
	public float w
	{
		get
		{
			return this.s.w;
		}
		set
		{
			this.s.w = value;
		}
	}

	// Token: 0x04000F68 RID: 3944
	public Vector3 r;

	// Token: 0x04000F69 RID: 3945
	public Vector3 u;

	// Token: 0x04000F6A RID: 3946
	public Vector3 f;

	// Token: 0x04000F6B RID: 3947
	public Vector3 t;

	// Token: 0x04000F6C RID: 3948
	public Vector4 s;
}
