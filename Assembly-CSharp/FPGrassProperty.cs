using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
[ExecuteInEditMode]
public class FPGrassProperty : ScriptableObject, IFPGrassAsset
{
	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06000268 RID: 616 RVA: 0x0000DA10 File Offset: 0x0000BC10
	// (set) Token: 0x06000267 RID: 615 RVA: 0x0000DA04 File Offset: 0x0000BC04
	public Color Color1
	{
		get
		{
			return this.color1;
		}
		set
		{
			this.color1 = value;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x0600026A RID: 618 RVA: 0x0000DA24 File Offset: 0x0000BC24
	// (set) Token: 0x06000269 RID: 617 RVA: 0x0000DA18 File Offset: 0x0000BC18
	public Color Color2
	{
		get
		{
			return this.color2;
		}
		set
		{
			this.color2 = value;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x0600026C RID: 620 RVA: 0x0000DA38 File Offset: 0x0000BC38
	// (set) Token: 0x0600026B RID: 619 RVA: 0x0000DA2C File Offset: 0x0000BC2C
	public float MinHeight
	{
		get
		{
			return this.minHeight;
		}
		set
		{
			this.minHeight = value;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x0600026E RID: 622 RVA: 0x0000DA4C File Offset: 0x0000BC4C
	// (set) Token: 0x0600026D RID: 621 RVA: 0x0000DA40 File Offset: 0x0000BC40
	public float MaxHeight
	{
		get
		{
			return this.maxHeight;
		}
		set
		{
			this.maxHeight = value;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000270 RID: 624 RVA: 0x0000DA60 File Offset: 0x0000BC60
	// (set) Token: 0x0600026F RID: 623 RVA: 0x0000DA54 File Offset: 0x0000BC54
	public float MinWidth
	{
		get
		{
			return this.minWidth;
		}
		set
		{
			this.minWidth = value;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06000272 RID: 626 RVA: 0x0000DA74 File Offset: 0x0000BC74
	// (set) Token: 0x06000271 RID: 625 RVA: 0x0000DA68 File Offset: 0x0000BC68
	public float MaxWidth
	{
		get
		{
			return this.maxWidth;
		}
		set
		{
			this.maxWidth = value;
		}
	}

	// Token: 0x0400019A RID: 410
	[SerializeField]
	private Color color1 = Color.white;

	// Token: 0x0400019B RID: 411
	[SerializeField]
	private Color color2 = Color.white;

	// Token: 0x0400019C RID: 412
	[SerializeField]
	private float minWidth = 1f;

	// Token: 0x0400019D RID: 413
	[SerializeField]
	private float maxWidth = 1f;

	// Token: 0x0400019E RID: 414
	[SerializeField]
	private float minHeight = 1f;

	// Token: 0x0400019F RID: 415
	[SerializeField]
	private float maxHeight = 1f;
}
