using System;
using UnityEngine;

// Token: 0x02000400 RID: 1024
public struct Matrix4x4Decomp
{
	// Token: 0x06002467 RID: 9319 RVA: 0x000866C4 File Offset: 0x000848C4
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

	// Token: 0x1700087D RID: 2173
	// (get) Token: 0x06002468 RID: 9320 RVA: 0x000867F4 File Offset: 0x000849F4
	// (set) Token: 0x06002469 RID: 9321 RVA: 0x00086924 File Offset: 0x00084B24
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

	// Token: 0x1700087E RID: 2174
	// (get) Token: 0x0600246A RID: 9322 RVA: 0x00086A54 File Offset: 0x00084C54
	// (set) Token: 0x0600246B RID: 9323 RVA: 0x00086A68 File Offset: 0x00084C68
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

	// Token: 0x1700087F RID: 2175
	// (get) Token: 0x0600246C RID: 9324 RVA: 0x00086AC0 File Offset: 0x00084CC0
	// (set) Token: 0x0600246D RID: 9325 RVA: 0x00086B04 File Offset: 0x00084D04
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

	// Token: 0x17000880 RID: 2176
	// (get) Token: 0x0600246E RID: 9326 RVA: 0x00086B48 File Offset: 0x00084D48
	// (set) Token: 0x0600246F RID: 9327 RVA: 0x00086B58 File Offset: 0x00084D58
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

	// Token: 0x040010CE RID: 4302
	public Vector3 r;

	// Token: 0x040010CF RID: 4303
	public Vector3 u;

	// Token: 0x040010D0 RID: 4304
	public Vector3 f;

	// Token: 0x040010D1 RID: 4305
	public Vector3 t;

	// Token: 0x040010D2 RID: 4306
	public Vector4 s;
}
