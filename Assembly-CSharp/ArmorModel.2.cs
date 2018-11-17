using System;
using UnityEngine;

// Token: 0x020005BC RID: 1468
public abstract class ArmorModel<TArmorModel> : global::ArmorModel where TArmorModel : global::ArmorModel<TArmorModel>, new()
{
	// Token: 0x06002F0E RID: 12046 RVA: 0x000B5BA8 File Offset: 0x000B3DA8
	internal ArmorModel(global::ArmorModelSlot slot) : base(slot)
	{
	}

	// Token: 0x17000A06 RID: 2566
	// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000B5BB4 File Offset: 0x000B3DB4
	public new TArmorModel censoredModel
	{
		get
		{
			return this.censored;
		}
	}

	// Token: 0x17000A07 RID: 2567
	// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000B5BBC File Offset: 0x000B3DBC
	public new bool hasCensoredModel
	{
		get
		{
			return this.censored;
		}
	}

	// Token: 0x17000A08 RID: 2568
	// (get) Token: 0x06002F11 RID: 12049 RVA: 0x000B5BD0 File Offset: 0x000B3DD0
	protected sealed override global::ArmorModel _censored
	{
		get
		{
			return this.censored;
		}
	}

	// Token: 0x04001992 RID: 6546
	[SerializeField]
	protected TArmorModel censored;
}
