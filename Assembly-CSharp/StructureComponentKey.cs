using System;
using UnityEngine;

// Token: 0x02000675 RID: 1653
public struct StructureComponentKey : IEquatable<StructureComponentKey>
{
	// Token: 0x06003974 RID: 14708 RVA: 0x000D3620 File Offset: 0x000D1820
	private StructureComponentKey(int iX, int iY, int iZ)
	{
		this.hashCode = ((iX << 8 | (iX >> 8 & 16777215)) ^ (iY << 16 | (iY >> 16 & 65535)) ^ (iZ << 24 | (iZ >> 24 & 255)) ^ iX * iY * iZ);
		this.iX = iX;
		this.iY = iY;
		this.iZ = iZ;
	}

	// Token: 0x06003975 RID: 14709 RVA: 0x000D367C File Offset: 0x000D187C
	public StructureComponentKey(float x, float y, float z)
	{
		this = new StructureComponentKey(StructureComponentKey.ROUND(x, 0.4f), StructureComponentKey.ROUND(y, 0.25f), StructureComponentKey.ROUND(z, 0.4f));
	}

	// Token: 0x06003976 RID: 14710 RVA: 0x000D36A8 File Offset: 0x000D18A8
	public StructureComponentKey(Vector3 v)
	{
		this = new StructureComponentKey(v.x, v.y, v.z);
	}

	// Token: 0x17000B28 RID: 2856
	// (get) Token: 0x06003977 RID: 14711 RVA: 0x000D36C8 File Offset: 0x000D18C8
	public float x
	{
		get
		{
			return (float)this.iX * 2.5f;
		}
	}

	// Token: 0x17000B29 RID: 2857
	// (get) Token: 0x06003978 RID: 14712 RVA: 0x000D36D8 File Offset: 0x000D18D8
	public float y
	{
		get
		{
			return (float)this.iY * 4f;
		}
	}

	// Token: 0x17000B2A RID: 2858
	// (get) Token: 0x06003979 RID: 14713 RVA: 0x000D36E8 File Offset: 0x000D18E8
	public float z
	{
		get
		{
			return (float)this.iZ * 2.5f;
		}
	}

	// Token: 0x17000B2B RID: 2859
	// (get) Token: 0x0600397A RID: 14714 RVA: 0x000D36F8 File Offset: 0x000D18F8
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

	// Token: 0x0600397B RID: 14715 RVA: 0x000D3744 File Offset: 0x000D1944
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

	// Token: 0x0600397C RID: 14716 RVA: 0x000D3774 File Offset: 0x000D1974
	public override int GetHashCode()
	{
		return this.hashCode;
	}

	// Token: 0x0600397D RID: 14717 RVA: 0x000D377C File Offset: 0x000D197C
	public override bool Equals(object obj)
	{
		if (!(obj is StructureComponentKey))
		{
			return false;
		}
		StructureComponentKey structureComponentKey = (StructureComponentKey)obj;
		return structureComponentKey.iX == this.iX && structureComponentKey.iZ == this.iZ && structureComponentKey.iY == this.iY;
	}

	// Token: 0x0600397E RID: 14718 RVA: 0x000D37D4 File Offset: 0x000D19D4
	public bool Equals(StructureComponentKey other)
	{
		return this.iX == other.iX && other.iZ == this.iZ && other.iY == this.iY;
	}

	// Token: 0x0600397F RID: 14719 RVA: 0x000D3818 File Offset: 0x000D1A18
	public override string ToString()
	{
		return string.Format("[{0},{1},{2}]", this.iX, this.iY, this.iZ);
	}

	// Token: 0x06003980 RID: 14720 RVA: 0x000D3848 File Offset: 0x000D1A48
	public static bool operator ==(StructureComponentKey l, StructureComponentKey r)
	{
		return l.hashCode == r.hashCode && l.iX == r.iX && l.iY == r.iY && l.iZ == r.iZ;
	}

	// Token: 0x06003981 RID: 14721 RVA: 0x000D38A4 File Offset: 0x000D1AA4
	public static bool operator !=(StructureComponentKey l, StructureComponentKey r)
	{
		return l.hashCode != r.hashCode || l.iX != r.iX || l.iY != r.iY || l.iZ != r.iZ;
	}

	// Token: 0x06003982 RID: 14722 RVA: 0x000D3900 File Offset: 0x000D1B00
	public static explicit operator StructureComponentKey(Vector3 v)
	{
		return new StructureComponentKey(StructureComponentKey.ROUND(v.x, 0.4f), StructureComponentKey.ROUND(v.y, 0.25f), StructureComponentKey.ROUND(v.z, 0.4f));
	}

	// Token: 0x06003983 RID: 14723 RVA: 0x000D3948 File Offset: 0x000D1B48
	public static implicit operator Vector3(StructureComponentKey key)
	{
		Vector3 result;
		result.x = (float)key.iX * 2.5f;
		result.y = (float)key.iY * 4f;
		result.z = (float)key.iZ * 2.5f;
		return result;
	}

	// Token: 0x04001D77 RID: 7543
	private const float kStepX = 2.5f;

	// Token: 0x04001D78 RID: 7544
	private const float kStepY = 4f;

	// Token: 0x04001D79 RID: 7545
	private const float kStepZ = 2.5f;

	// Token: 0x04001D7A RID: 7546
	private const float kInverseStepX = 0.4f;

	// Token: 0x04001D7B RID: 7547
	private const float kInverseStepY = 0.25f;

	// Token: 0x04001D7C RID: 7548
	private const float kInverseStepZ = 0.4f;

	// Token: 0x04001D7D RID: 7549
	public readonly int iX;

	// Token: 0x04001D7E RID: 7550
	public readonly int iY;

	// Token: 0x04001D7F RID: 7551
	public readonly int iZ;

	// Token: 0x04001D80 RID: 7552
	public readonly int hashCode;
}
