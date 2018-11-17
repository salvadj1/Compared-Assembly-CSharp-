using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000764 RID: 1892
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Texture Atlas")]
[Serializable]
public class dfAtlas : MonoBehaviour
{
	// Token: 0x17000BD3 RID: 3027
	// (get) Token: 0x06003E87 RID: 16007 RVA: 0x000E4120 File Offset: 0x000E2320
	public Texture2D Texture
	{
		get
		{
			return (!(this.replacementAtlas != null)) ? (this.material.mainTexture as Texture2D) : this.replacementAtlas.Texture;
		}
	}

	// Token: 0x17000BD4 RID: 3028
	// (get) Token: 0x06003E88 RID: 16008 RVA: 0x000E4154 File Offset: 0x000E2354
	public int Count
	{
		get
		{
			return (!(this.replacementAtlas != null)) ? this.items.Count : this.replacementAtlas.Count;
		}
	}

	// Token: 0x17000BD5 RID: 3029
	// (get) Token: 0x06003E89 RID: 16009 RVA: 0x000E4190 File Offset: 0x000E2390
	public List<global::dfAtlas.ItemInfo> Items
	{
		get
		{
			return (!(this.replacementAtlas != null)) ? this.items : this.replacementAtlas.Items;
		}
	}

	// Token: 0x17000BD6 RID: 3030
	// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000E41BC File Offset: 0x000E23BC
	// (set) Token: 0x06003E8B RID: 16011 RVA: 0x000E41E8 File Offset: 0x000E23E8
	public Material Material
	{
		get
		{
			return (!(this.replacementAtlas != null)) ? this.material : this.replacementAtlas.Material;
		}
		set
		{
			if (this.replacementAtlas != null)
			{
				this.replacementAtlas.Material = value;
			}
			else
			{
				this.material = value;
			}
		}
	}

	// Token: 0x17000BD7 RID: 3031
	// (get) Token: 0x06003E8C RID: 16012 RVA: 0x000E4214 File Offset: 0x000E2414
	// (set) Token: 0x06003E8D RID: 16013 RVA: 0x000E421C File Offset: 0x000E241C
	public global::dfAtlas Replacement
	{
		get
		{
			return this.replacementAtlas;
		}
		set
		{
			this.replacementAtlas = value;
		}
	}

	// Token: 0x17000BD8 RID: 3032
	public global::dfAtlas.ItemInfo this[string key]
	{
		get
		{
			if (this.replacementAtlas != null)
			{
				return this.replacementAtlas[key];
			}
			if (string.IsNullOrEmpty(key))
			{
				return null;
			}
			if (this.map.Count == 0)
			{
				this.RebuildIndexes();
			}
			global::dfAtlas.ItemInfo result = null;
			if (this.map.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}
	}

	// Token: 0x06003E8F RID: 16015 RVA: 0x000E4290 File Offset: 0x000E2490
	internal static bool Equals(global::dfAtlas lhs, global::dfAtlas rhs)
	{
		return object.ReferenceEquals(lhs, rhs) || (!(lhs == null) && !(rhs == null) && lhs.material == rhs.material);
	}

	// Token: 0x06003E90 RID: 16016 RVA: 0x000E42CC File Offset: 0x000E24CC
	public void AddItem(global::dfAtlas.ItemInfo item)
	{
		this.items.Add(item);
		this.RebuildIndexes();
	}

	// Token: 0x06003E91 RID: 16017 RVA: 0x000E42E0 File Offset: 0x000E24E0
	public void AddItems(IEnumerable<global::dfAtlas.ItemInfo> items)
	{
		this.items.AddRange(items);
		this.RebuildIndexes();
	}

	// Token: 0x06003E92 RID: 16018 RVA: 0x000E42F4 File Offset: 0x000E24F4
	public void Remove(string name)
	{
		for (int i = this.items.Count - 1; i >= 0; i--)
		{
			if (this.items[i].name == name)
			{
				this.items.RemoveAt(i);
			}
		}
		this.RebuildIndexes();
	}

	// Token: 0x06003E93 RID: 16019 RVA: 0x000E4350 File Offset: 0x000E2550
	public void RebuildIndexes()
	{
		if (this.map == null)
		{
			this.map = new Dictionary<string, global::dfAtlas.ItemInfo>();
		}
		else
		{
			this.map.Clear();
		}
		for (int i = 0; i < this.items.Count; i++)
		{
			global::dfAtlas.ItemInfo itemInfo = this.items[i];
			this.map[itemInfo.name] = itemInfo;
		}
	}

	// Token: 0x040020F0 RID: 8432
	[SerializeField]
	protected Material material;

	// Token: 0x040020F1 RID: 8433
	[SerializeField]
	protected List<global::dfAtlas.ItemInfo> items = new List<global::dfAtlas.ItemInfo>();

	// Token: 0x040020F2 RID: 8434
	private Dictionary<string, global::dfAtlas.ItemInfo> map = new Dictionary<string, global::dfAtlas.ItemInfo>();

	// Token: 0x040020F3 RID: 8435
	private global::dfAtlas replacementAtlas;

	// Token: 0x02000765 RID: 1893
	[Serializable]
	public class ItemInfo : IComparable<global::dfAtlas.ItemInfo>, IEquatable<global::dfAtlas.ItemInfo>
	{
		// Token: 0x06003E95 RID: 16021 RVA: 0x000E43EC File Offset: 0x000E25EC
		public int CompareTo(global::dfAtlas.ItemInfo other)
		{
			return this.name.CompareTo(other.name);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000E4400 File Offset: 0x000E2600
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000E4410 File Offset: 0x000E2610
		public override bool Equals(object obj)
		{
			return obj is global::dfAtlas.ItemInfo && this.name.Equals(((global::dfAtlas.ItemInfo)obj).name);
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x000E4438 File Offset: 0x000E2638
		public bool Equals(global::dfAtlas.ItemInfo other)
		{
			return this.name.Equals(other.name);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000E444C File Offset: 0x000E264C
		public static bool operator ==(global::dfAtlas.ItemInfo lhs, global::dfAtlas.ItemInfo rhs)
		{
			return object.ReferenceEquals(lhs, rhs) || (lhs != null && rhs != null && lhs.name.Equals(rhs.name));
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000E447C File Offset: 0x000E267C
		public static bool operator !=(global::dfAtlas.ItemInfo lhs, global::dfAtlas.ItemInfo rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040020F4 RID: 8436
		public string name;

		// Token: 0x040020F5 RID: 8437
		public Rect region;

		// Token: 0x040020F6 RID: 8438
		public RectOffset border = new RectOffset();

		// Token: 0x040020F7 RID: 8439
		public bool rotated;

		// Token: 0x040020F8 RID: 8440
		public Vector2 sizeInPixels = Vector2.zero;

		// Token: 0x040020F9 RID: 8441
		[SerializeField]
		public string textureGUID = string.Empty;

		// Token: 0x040020FA RID: 8442
		public bool deleted;

		// Token: 0x040020FB RID: 8443
		[SerializeField]
		public Texture2D texture;
	}
}
