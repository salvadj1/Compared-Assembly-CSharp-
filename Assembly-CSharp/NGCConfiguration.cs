using System;
using System.Collections.Generic;
using System.Globalization;
using Facepunch;
using Facepunch.Build;
using Facepunch.Hash;
using UnityEngine;

// Token: 0x020002EF RID: 751
[UniqueBundleScriptableObject]
public class NGCConfiguration : ScriptableObject
{
	// Token: 0x06001A6E RID: 6766 RVA: 0x00068FD0 File Offset: 0x000671D0
	public static NGCConfiguration Load()
	{
		return Bundling.Load<NGCConfiguration>("content/network/NGCConf");
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x00068FDC File Offset: 0x000671DC
	public void Install()
	{
		foreach (NGCConfiguration.PrefabEntry prefabEntry in this.entries)
		{
			if (prefabEntry != null && prefabEntry.ReadyToRegister)
			{
				NGC.Prefab.Register.Add(prefabEntry.Path, prefabEntry.HashCode, ";" + prefabEntry.Name);
			}
		}
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x0006903C File Offset: 0x0006723C
	protected void OnEnable()
	{
		if (this.entries == null)
		{
			this.entries = new NGCConfiguration.PrefabEntry[0];
		}
		else
		{
			HashSet<string> hashSet = new HashSet<string>();
			int num = 0;
			for (int i = 0; i < this.entries.Length; i++)
			{
				if (this.entries[i] != null)
				{
					if (!hashSet.Add(this.entries[i].Name))
					{
						Debug.LogWarning(string.Format("Removing duplicate ngc prefab named '{0}' (path:{1})", this.entries[i].Name, this.entries[i].Path));
					}
					else
					{
						if (string.IsNullOrEmpty(this.entries[i].Path))
						{
							Debug.LogWarning(string.Format("ngc prefab {0} has no path!", this.entries[i].Name), this);
						}
						this.entries[num++] = this.entries[i];
					}
				}
			}
			if (num < this.entries.Length)
			{
				Array.Resize<NGCConfiguration.PrefabEntry>(ref this.entries, num);
				Debug.LogWarning("The entries of the ngcconfiguration were altered!", this);
			}
		}
	}

	// Token: 0x04000DDD RID: 3549
	private const string bundledPath = "content/network/NGCConf";

	// Token: 0x04000DDE RID: 3550
	[SerializeField]
	private NGCConfiguration.PrefabEntry[] entries;

	// Token: 0x020002F0 RID: 752
	[Serializable]
	public sealed class PrefabEntry
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x00069178 File Offset: 0x00067378
		public int HashCode
		{
			get
			{
				return (!this.calculatedHashCode) ? (this._hashCode = NGCConfiguration.PrefabEntry.hash(this.guidText)) : this._hashCode;
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x000691B0 File Offset: 0x000673B0
		public override int GetHashCode()
		{
			return (!this.calculatedHashCode) ? (this._hashCode = NGCConfiguration.PrefabEntry.hash(this.guidText)) : this._hashCode;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000691E8 File Offset: 0x000673E8
		public override string ToString()
		{
			return string.Format("[PrefabEntry: Name=\"{1}\", HashCode={0:X}, Path=\"{2}\"]", this.HashCode, this.Name, this.Path);
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00069218 File Offset: 0x00067418
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x00069220 File Offset: 0x00067420
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00069228 File Offset: 0x00067428
		public bool ReadyToRegister
		{
			get
			{
				return !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Path) && this.HashCode != 0;
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00069264 File Offset: 0x00067464
		private static int hash(string guid)
		{
			if (string.IsNullOrEmpty(guid))
			{
				return 0;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			using (IEnumerator<int> enumerator = NGCConfiguration.PrefabEntry.ParseInts(guid))
			{
				if (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (enumerator.MoveNext())
					{
						num2 = enumerator.Current;
						if (enumerator.MoveNext())
						{
							num3 = enumerator.Current;
							if (enumerator.MoveNext())
							{
								num4 = enumerator.Current;
								if (enumerator.MoveNext())
								{
									num5 = enumerator.Current;
									if (enumerator.MoveNext())
									{
										num6 = enumerator.Current;
									}
								}
							}
						}
					}
				}
			}
			NGCConfiguration.PrefabEntry.hashwork.guid[0] = num;
			NGCConfiguration.PrefabEntry.hashwork.guid[1] = num6;
			NGCConfiguration.PrefabEntry.hashwork.guid[2] = num5;
			NGCConfiguration.PrefabEntry.hashwork.guid[3] = num3;
			NGCConfiguration.PrefabEntry.hashwork.guid[4] = num4;
			NGCConfiguration.PrefabEntry.hashwork.guid[5] = num2;
			return MurmurHash2.SINT(NGCConfiguration.PrefabEntry.hashwork.guid, NGCConfiguration.PrefabEntry.hashwork.guid.Length, 2260766486u);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00069388 File Offset: 0x00067588
		private static IEnumerator<int> ParseInts(string hex)
		{
			int start = hex.Length;
			while (start >= 8)
			{
				start -= 8;
				yield return int.Parse(hex.Substring(start, 8), NumberStyles.HexNumber);
			}
			if (start > 0)
			{
				yield return int.Parse(hex.Remove(start), NumberStyles.HexNumber);
			}
			yield break;
		}

		// Token: 0x04000DDF RID: 3551
		private const uint peSeed = 2260766486u;

		// Token: 0x04000DE0 RID: 3552
		[SerializeField]
		private string name = "!unnamed";

		// Token: 0x04000DE1 RID: 3553
		[SerializeField]
		private string path = string.Empty;

		// Token: 0x04000DE2 RID: 3554
		[SerializeField]
		private string guidText = string.Empty;

		// Token: 0x04000DE3 RID: 3555
		[NonSerialized]
		private bool calculatedHashCode;

		// Token: 0x04000DE4 RID: 3556
		[NonSerialized]
		private int _hashCode;

		// Token: 0x020002F1 RID: 753
		private static class hashwork
		{
			// Token: 0x04000DE5 RID: 3557
			public static readonly int[] guid = new int[6];
		}
	}
}
