using System;
using UnityEngine;

// Token: 0x020006B8 RID: 1720
[Serializable]
public class LootSpawnListReference
{
	// Token: 0x06003A8B RID: 14987 RVA: 0x000CD8B4 File Offset: 0x000CBAB4
	public LootSpawnListReference()
	{
		this.name = string.Empty;
	}

	// Token: 0x17000B66 RID: 2918
	// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000CD8C8 File Offset: 0x000CBAC8
	// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000CD900 File Offset: 0x000CBB00
	public global::LootSpawnList list
	{
		get
		{
			if (!this.once)
			{
				this.once = true;
				this._list = global::DatablockDictionary.GetLootSpawnListByName(this.name ?? string.Empty);
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

	// Token: 0x06003A8E RID: 14990 RVA: 0x000CD934 File Offset: 0x000CBB34
	public static explicit operator global::LootSpawnList(global::LootSpawnListReference reference)
	{
		if (object.ReferenceEquals(reference, null))
		{
			return null;
		}
		return reference.list;
	}

	// Token: 0x06003A8F RID: 14991 RVA: 0x000CD94C File Offset: 0x000CBB4C
	public static bool operator true(global::LootSpawnListReference reference)
	{
		return !object.ReferenceEquals(reference, null) && reference.list;
	}

	// Token: 0x06003A90 RID: 14992 RVA: 0x000CD968 File Offset: 0x000CBB68
	public static bool operator false(global::LootSpawnListReference reference)
	{
		return object.ReferenceEquals(reference, null) || !reference.list;
	}

	// Token: 0x04001CC6 RID: 7366
	[SerializeField]
	private string name;

	// Token: 0x04001CC7 RID: 7367
	[NonSerialized]
	private bool once;

	// Token: 0x04001CC8 RID: 7368
	[NonSerialized]
	private global::LootSpawnList _list;
}
