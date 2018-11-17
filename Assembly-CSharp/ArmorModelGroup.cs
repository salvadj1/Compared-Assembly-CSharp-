using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000509 RID: 1289
public sealed class ArmorModelGroup : ScriptableObject, IEnumerable, IEnumerable<ArmorModel>
{
	// Token: 0x06002BA7 RID: 11175 RVA: 0x000AE880 File Offset: 0x000ACA80
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)this.collection).GetEnumerator();
	}

	// Token: 0x170009A5 RID: 2469
	public ArmorModel this[ArmorModelSlot slot]
	{
		get
		{
			return this.collection[slot];
		}
	}

	// Token: 0x06002BA9 RID: 11177 RVA: 0x000AE8A0 File Offset: 0x000ACAA0
	public T GetArmorModel<T>() where T : ArmorModel, new()
	{
		return this.collection.GetArmorModel<T>();
	}

	// Token: 0x06002BAA RID: 11178 RVA: 0x000AE8B0 File Offset: 0x000ACAB0
	public IEnumerator<ArmorModel> GetEnumerator()
	{
		return this.collection.GetEnumerator();
	}

	// Token: 0x170009A6 RID: 2470
	// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000AE8C4 File Offset: 0x000ACAC4
	public ArmorModelMemberMap armorModelMemberMap
	{
		get
		{
			return this.collection.ToMemberMap();
		}
	}

	// Token: 0x040017DF RID: 6111
	[SerializeField]
	private ArmorModelCollection collection;
}
