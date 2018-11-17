using System;
using UnityEngine;

// Token: 0x02000739 RID: 1849
public struct StructureComponentKey : IEquatable<global::StructureComponentKey>
{
	// Token: 0x06003D68 RID: 15720 RVA: 0x000DC000 File Offset: 0x000DA200
	private StructureComponentKey(int iX, int iY, int iZ)
	{
		this.hashCode = ((iX << 8 | (iX >> 8 & 16777215)) ^ (iY << 16 | (iY >> 16 & 65535)) ^ (iZ << 24 | (iZ >> 24 & 255)) ^ iX * iY * iZ);
		this.iX = iX;
		this.iY = iY;
		this.iZ = iZ;
	}

	// Token: 0x06003D69 RID: 15721 RVA: 0x000DC05C File Offset: 0x000DA25C
	public StructureComponentKey(float x, float y, float z)
	{
		this = new global::StructureComponentKey(global::StructureComponentKey.ROUND(x, 0.4f), global::StructureComponentKey.ROUND(y, 0.25f), global::StructureComponentKey.ROUND(z, 0.4f));
	}

	// Token: 0x06003D6A RID: 15722 RVA: 0x000DC088 File Offset: 0x000DA288
	public StructureComponentKey(Vector3 v)
	{
		this = new global::StructureComponentKey(v.x, v.y, v.z);
	}

	// Token: 0x17000BAA RID: 2986
	// (get) Token: 0x06003D6B RID: 15723 RVA: 0x000DC0A8 File Offset: 0x000DA2A8
	public float x
	{
		get
		{
			return (float)this.iX * 2.5f;
		}
	}

	// Token: 0x17000BAB RID: 2987
	// (get) Token: 0x06003D6C RID: 15724 RVA: 0x000DC0B8 File Offset: 0x000DA2B8
	public float y
	{
		get
		{
			return (float)this.iY * 4f;
		}
	}

	// Token: 0x17000BAC RID: 2988
	// (get) Token: 0x06003D6D RID: 15725 RVA: 0x000DC0C8 File Offset: 0x000DA2C8
	public float z
	{
		get
		{
			return (float)this.iZ * 2.5f;
		}
	}

	// Token: 0x17000BAD RID: 2989
	// (get) Token: 0x06003D6E RID: 15726 RVA: 0x000DC0D8 File Offset: 0x000DA2D8
	public Vector3 vector
	{
		get
		{
			Vector3 result;
			result.x = (float)this.iX * 2.5f;
			result.y = (float)this.iY * 4f;
			result.z = (float)this.iZ * 2.5f;
			return result;
		}
	}

	// Token: 0x06003D6F RID: 15727 RVA: 0x000DC124 File Offset: 0x000DA324
	public static int ROUND(float v, float inverseStepSize)
	{
		if (v < 0f)
		{
			return -Mathf.RoundToInt(v * -inverseStepSize);
		}
		if (v > 0f)
		{
			return Mathf.RoundToInt(v * inverseStepSize);
		}
		return 0;
	}

	// Token: 0x06003D70 RID: 15728 RVA: 0x000DC154 File Offset: 0x000DA354
	public override int GetHashCode()
	{
		return this.hashCode;
	}

	// Token: 0x06003D71 RID: 15729 RVA: 0x000DC15C File Offset: 0x000DA35C
	public override bool Equals(object obj)
	{
		if (!(obj is global::StructureComponentKey))
		{
			return false;
		}
		global::StructureComponentKey structureComponentKey = (global::StructureComponentKey)obj;
		return structureComponentKey.iX == this.iX && structureComponentKey.iZ == this.iZ && structureComponentKey.iY == this.iY;
	}

	// Token: 0x06003D72 RID: 15730 RVA: 0x000DC1B4 File Offset: 0x000DA3B4
	public bool Equals(global::StructureComponentKey other)
	{
		return this.iX == other.iX && other.iZ == this.iZ && other.iY == this.iY;
	}

	// Token: 0x06003D73 RID: 15731 RVA: 0x000DC1F8 File Offset: 0x000DA3F8
	public override string ToString()
	{
		return string.Format("[{0},{1},{2}]", this.iX, this.iY, this.iZ);
	}

	// Token: 0x06003D74 RID: 15732 RVA: 0x000DC228 File Offset: 0x000DA428
	public static bool operator ==(global::StructureComponentKey l, global::StructureComponentKey r)
	{
		return l.hashCode == r.hashCode && l.iX == r.iX && l.iY == r.iY && l.iZ == r.iZ;
	}

	// Token: 0x06003D75 RID: 15733 RVA: 0x000DC284 File Offset: 0x000DA484
	public static bool operator !=(global::StructureComponentKey l, global::StructureComponentKey r)
	{
		return l.hashCode != r.hashCode || l.iX != r.iX || l.iY != r.iY || l.iZ != r.iZ;
	}

	// Token: 0x06003D76 RID: 15734 RVA: 0x000DC2E0 File Offset: 0x000DA4E0
	public static explicit operator global::StructureComponentKey(Vector3 v)
	{
		return new global::StructureComponentKey(global::StructureComponentKey.ROUND(v.x, 0.4f), global::StructureComponentKey.ROUND(v.y, 0.25f), global::StructureComponentKey.ROUND(v.z, 0.4f));
	}

	// Token: 0x06003D77 RID: 15735 RVA: 0x000DC328 File Offset: 0x000DA528
	public static implicit operator Vector3(global::StructureComponentKey key)
	{
		Vector3 result;
		result.x = (float)key.iX * 2.5f;
		result.y = (float)key.iY * 4f;
		result.z = (float)key.iZ * 2.5f;
		return result;
	}

	// Token: 0x04001F6F RID: 8047
	private const float kStepX = 2.5f;

	// Token: 0x04001F70 RID: 8048
	private const float kStepY = 4f;

	// Token: 0x04001F71 RID: 8049
	private const float kStepZ = 2.5f;

	// Token: 0x04001F72 RID: 8050
	private const float kInverseStepX = 0.4f;

	// Token: 0x04001F73 RID: 8051
	private const float kInverseStepY = 0.25f;

	// Token: 0x04001F74 RID: 8052
	private const float kInverseStepZ = 0.4f;

	// Token: 0x04001F75 RID: 8053
	public readonly int iX;

	// Token: 0x04001F76 RID: 8054
	public readonly int iY;

	// Token: 0x04001F77 RID: 8055
	public readonly int iZ;

	// Token: 0x04001F78 RID: 8056
	public readonly int hashCode;
}
