using System;
using UnityEngine;

// Token: 0x020004FF RID: 1279
public abstract class ArmorModel<TArmorModel> : ArmorModel where TArmorModel : ArmorModel<TArmorModel>, new()
{
	// Token: 0x06002B4E RID: 11086 RVA: 0x000ADB0C File Offset: 0x000ABD0C
	internal ArmorModel(ArmorModelSlot slot) : base(slot)
	{
	}

	// Token: 0x17000992 RID: 2450
	// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000ADB18 File Offset: 0x000ABD18
	public new TArmorModel censoredModel
	{
		get
		{
			return this.censored;
		}
	}

	// Token: 0x17000993 RID: 2451
	// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000ADB20 File Offset: 0x000ABD20
	public new bool hasCensoredModel
	{
		get
		{
			return this.censored;
		}
	}

	// Token: 0x17000994 RID: 2452
	// (get) Token: 0x06002B51 RID: 11089 RVA: 0x000ADB34 File Offset: 0x000ABD34
	protected sealed override ArmorModel _censored
	{
		get
		{
			return this.censored;
		}
	}

	// Token: 0x040017C6 RID: 6086
	[SerializeField]
	protected TArmorModel censored;
}
