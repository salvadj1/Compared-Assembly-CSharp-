using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200069E RID: 1694
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Texture Atlas")]
[Serializable]
public class dfAtlas : MonoBehaviour
{
	// Token: 0x17000B4F RID: 2895
	// (get) Token: 0x06003A89 RID: 14985 RVA: 0x000DB690 File Offset: 0x000D9890
	public Texture2D Texture
	{
		get
		{
			return (!(this.replacementAtlas != null)) ? (this.material.mainTexture as Texture2D) : this.replacementAtlas.Texture;
		}
	}

	// Token: 0x17000B50 RID: 2896
	// (get) Token: 0x06003A8A RID: 14986 RVA: 0x000DB6C4 File Offset: 0x000D98C4
	public int Count
	{
		get
		{
			return (!(this.replacementAtlas != null)) ? this.items.Count : this.replacementAtlas.Count;
		}
	}

	// Token: 0x17000B51 RID: 2897
	// (get) Token: 0x06003A8B RID: 14987 RVA: 0x000DB700 File Offset: 0x000D9900
	public List<dfAtlas.ItemInfo> Items
	{
		get
		{
			return (!(this.replacementAtlas != null)) ? this.items : this.replacementAtlas.Items;
		}
	}

	// Token: 0x17000B52 RID: 2898
	// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000DB72C File Offset: 0x000D992C
	// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000DB758 File Offset: 0x000D9958
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

	// Token: 0x17000B53 RID: 2899
	// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000DB784 File Offset: 0x000D9984
	// (set) Token: 0x06003A8F RID: 14991 RVA: 0x000DB78C File Offset: 0x000D998C
	public dfAtlas Replacement
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

	// Token: 0x17000B54 RID: 2900
	public dfAtlas.ItemInfo this[string key]
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
			dfAtlas.ItemInfo result = null;
			if (this.map.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}
	}

	// Token: 0x06003A91 RID: 14993 RVA: 0x000DB800 File Offset: 0x000D9A00
	internal static bool Equals(dfAtlas lhs, dfAtlas rhs)
	{
		return object.ReferenceEquals(lhs, rhs) || (!(lhs == null) && !(rhs == null) && lhs.material == rhs.material);
	}

	// Token: 0x06003A92 RID: 14994 RVA: 0x000DB83C File Offset: 0x000D9A3C
	public void AddItem(dfAtlas.ItemInfo item)
	{
		this.items.Add(item);
		this.RebuildIndexes();
	}

	// Token: 0x06003A93 RID: 14995 RVA: 0x000DB850 File Offset: 0x000D9A50
	public void AddItems(IEnumerable<dfAtlas.ItemInfo> items)
	{
		this.items.AddRange(items);
		this.RebuildIndexes();
	}

	// Token: 0x06003A94 RID: 14996 RVA: 0x000DB864 File Offset: 0x000D9A64
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

	// Token: 0x06003A95 RID: 14997 RVA: 0x000DB8C0 File Offset: 0x000D9AC0
	public void RebuildIndexes()
	{
		if (this.map == null)
		{
			this.map = new Dictionary<string, dfAtlas.ItemInfo>();
		}
		else
		{
			this.map.Clear();
		}
		for (int i = 0; i < this.items.Count; i++)
		{
			dfAtlas.ItemInfo itemInfo = this.items[i];
			this.map[itemInfo.name] = itemInfo;
		}
	}

	// Token: 0x04001EF4 RID: 7924
	[SerializeField]
	protected Material material;

	// Token: 0x04001EF5 RID: 7925
	[SerializeField]
	protected List<dfAtlas.ItemInfo> items = new List<dfAtlas.ItemInfo>();

	// Token: 0x04001EF6 RID: 7926
	private Dictionary<string, dfAtlas.ItemInfo> map = new Dictionary<string, dfAtlas.ItemInfo>();

	// Token: 0x04001EF7 RID: 7927
	private dfAtlas replacementAtlas;

	// Token: 0x0200069F RID: 1695
	[Serializable]
	public class ItemInfo : IComparable<dfAtlas.ItemInfo>, IEquatable<dfAtlas.ItemInfo>
	{
		// Token: 0x06003A97 RID: 14999 RVA: 0x000DB95C File Offset: 0x000D9B5C
		public int CompareTo(dfAtlas.ItemInfo other)
		{
			return this.name.CompareTo(other.name);
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000DB970 File Offset: 0x000D9B70
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000DB980 File Offset: 0x000D9B80
		public override bool Equals(object obj)
		{
			return obj is dfAtlas.ItemInfo && this.name.Equals(((dfAtlas.ItemInfo)obj).name);
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000DB9A8 File Offset: 0x000D9BA8
		public bool Equals(dfAtlas.ItemInfo other)
		{
			return this.name.Equals(other.name);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000DB9BC File Offset: 0x000D9BBC
		public static bool operator ==(dfAtlas.ItemInfo lhs, dfAtlas.ItemInfo rhs)
		{
			return object.ReferenceEquals(lhs, rhs) || (lhs != null && rhs != null && lhs.name.Equals(rhs.name));
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000DB9EC File Offset: 0x000D9BEC
		public static bool operator !=(dfAtlas.ItemInfo lhs, dfAtlas.ItemInfo rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04001EF8 RID: 7928
		public string name;

		// Token: 0x04001EF9 RID: 7929
		public Rect region;

		// Token: 0x04001EFA RID: 7930
		public RectOffset border = new RectOffset();

		// Token: 0x04001EFB RID: 7931
		public bool rotated;

		// Token: 0x04001EFC RID: 7932
		public Vector2 sizeInPixels = Vector2.zero;

		// Token: 0x04001EFD RID: 7933
		[SerializeField]
		public string textureGUID = string.Empty;

		// Token: 0x04001EFE RID: 7934
		public bool deleted;

		// Token: 0x04001EFF RID: 7935
		[SerializeField]
		public Texture2D texture;
	}
}
