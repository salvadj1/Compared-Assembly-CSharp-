using System;
using UnityEngine;

// Token: 0x020005F8 RID: 1528
[Serializable]
public class LootSpawnListReference
{
	// Token: 0x060036B3 RID: 14003 RVA: 0x000C5384 File Offset: 0x000C3584
	public LootSpawnListReference()
	{
		this.name = string.Empty;
	}

	// Token: 0x17000AEC RID: 2796
	// (get) Token: 0x060036B4 RID: 14004 RVA: 0x000C5398 File Offset: 0x000C3598
	// (set) Token: 0x060036B5 RID: 14005 RVA: 0x000C53D0 File Offset: 0x000C35D0
	public LootSpawnList list
	{
		get
		{
			if (!this.once)
			{
				this.once = true;
				this._list = DatablockDictionary.GetLootSpawnListByName(this.name ?? string.Empty);
			}
			return this._list;
		}
		set
		{
			this.name = ((!value) ? string.Empty : value.name);
			this._list = value;
			this.once = true;
		}
	}

	// Token: 0x060036B6 RID: 14006 RVA: 0x000C5404 File Offset: 0x000C3604
	public static explicit operator LootSpawnList(LootSpawnListReference reference)
	{
		if (object.ReferenceEquals(reference, null))
		{
			return null;
		}
		return reference.list;
	}

	// Token: 0x060036B7 RID: 14007 RVA: 0x000C541C File Offset: 0x000C361C
	public static bool operator true(LootSpawnListReference reference)
	{
		return !object.ReferenceEquals(reference, null) && reference.list;
	}

	// Token: 0x060036B8 RID: 14008 RVA: 0x000C5438 File Offset: 0x000C3638
	public static bool operator false(LootSpawnListReference reference)
	{
		return object.ReferenceEquals(reference, null) || !reference.list;
	}

	// Token: 0x04001AE0 RID: 6880
	[SerializeField]
	private string name;

	// Token: 0x04001AE1 RID: 6881
	[NonSerialized]
	private bool once;

	// Token: 0x04001AE2 RID: 6882
	[NonSerialized]
	private LootSpawnList _list;
}
