using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005C6 RID: 1478
public sealed class ArmorModelGroup : ScriptableObject, IEnumerable, IEnumerable<global::ArmorModel>
{
	// Token: 0x06002F67 RID: 12135 RVA: 0x000B691C File Offset: 0x000B4B1C
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)this.collection).GetEnumerator();
	}

	// Token: 0x17000A19 RID: 2585
	public global::ArmorModel this[global::ArmorModelSlot slot]
	{
		get
		{
			return this.collection[slot];
		}
	}

	// Token: 0x06002F69 RID: 12137 RVA: 0x000B693C File Offset: 0x000B4B3C
	public T GetArmorModel<T>() where T : global::ArmorModel, new()
	{
		return this.collection.GetArmorModel<T>();
	}

	// Token: 0x06002F6A RID: 12138 RVA: 0x000B694C File Offset: 0x000B4B4C
	public IEnumerator<global::ArmorModel> GetEnumerator()
	{
		return this.collection.GetEnumerator();
	}

	// Token: 0x17000A1A RID: 2586
	// (get) Token: 0x06002F6B RID: 12139 RVA: 0x000B6960 File Offset: 0x000B4B60
	public global::ArmorModelMemberMap armorModelMemberMap
	{
		get
		{
			return this.collection.ToMemberMap();
		}
	}

	// Token: 0x040019AB RID: 6571
	[SerializeField]
	private global::ArmorModelCollection collection;
}
