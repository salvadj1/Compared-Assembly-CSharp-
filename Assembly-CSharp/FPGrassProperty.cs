using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
[ExecuteInEditMode]
public class FPGrassProperty : ScriptableObject, global::IFPGrassAsset
{
	// Token: 0x17000078 RID: 120
	// (get) Token: 0x060002DA RID: 730 RVA: 0x0000EFB8 File Offset: 0x0000D1B8
	// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000EFAC File Offset: 0x0000D1AC
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

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x060002DC RID: 732 RVA: 0x0000EFCC File Offset: 0x0000D1CC
	// (set) Token: 0x060002DB RID: 731 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
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

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x060002DE RID: 734 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
	// (set) Token: 0x060002DD RID: 733 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
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

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
	// (set) Token: 0x060002DF RID: 735 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
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

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000F008 File Offset: 0x0000D208
	// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000EFFC File Offset: 0x0000D1FC
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

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000F01C File Offset: 0x0000D21C
	// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000F010 File Offset: 0x0000D210
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

	// Token: 0x040001FC RID: 508
	[SerializeField]
	private Color color1 = Color.white;

	// Token: 0x040001FD RID: 509
	[SerializeField]
	private Color color2 = Color.white;

	// Token: 0x040001FE RID: 510
	[SerializeField]
	private float minWidth = 1f;

	// Token: 0x040001FF RID: 511
	[SerializeField]
	private float maxWidth = 1f;

	// Token: 0x04000200 RID: 512
	[SerializeField]
	private float minHeight = 1f;

	// Token: 0x04000201 RID: 513
	[SerializeField]
	private float maxHeight = 1f;
}
