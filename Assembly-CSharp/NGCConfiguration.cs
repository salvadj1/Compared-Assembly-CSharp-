using System;
using System.Collections.Generic;
using System.Globalization;
using Facepunch;
using Facepunch.Build;
using Facepunch.Hash;
using UnityEngine;

// Token: 0x02000395 RID: 917
[UniqueBundleScriptableObject]
public class NGCConfiguration : ScriptableObject
{
	// Token: 0x06001DA2 RID: 7586 RVA: 0x0006D944 File Offset: 0x0006BB44
	public static global::NGCConfiguration Load()
	{
		return Facepunch.Bundling.Load<global::NGCConfiguration>("content/network/NGCConf");
	}

	// Token: 0x06001DA3 RID: 7587 RVA: 0x0006D950 File Offset: 0x0006BB50
	public void Install()
	{
		foreach (global::NGCConfiguration.PrefabEntry prefabEntry in this.entries)
		{
			if (prefabEntry != null && prefabEntry.ReadyToRegister)
			{
				global::NGC.Prefab.Register.Add(prefabEntry.Path, prefabEntry.HashCode, ";" + prefabEntry.Name);
			}
		}
	}

	// Token: 0x06001DA4 RID: 7588 RVA: 0x0006D9B0 File Offset: 0x0006BBB0
	protected void OnEnable()
	{
		if (this.entries == null)
		{
			this.entries = new global::NGCConfiguration.PrefabEntry[0];
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
				Array.Resize<global::NGCConfiguration.PrefabEntry>(ref this.entries, num);
				Debug.LogWarning("The entries of the ngcconfiguration were altered!", this);
			}
		}
	}

	// Token: 0x04000F18 RID: 3864
	private const string bundledPath = "content/network/NGCConf";

	// Token: 0x04000F19 RID: 3865
	[SerializeField]
	private global::NGCConfiguration.PrefabEntry[] entries;

	// Token: 0x02000396 RID: 918
	[Serializable]
	public sealed class PrefabEntry
	{
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0006DAEC File Offset: 0x0006BCEC
		public int HashCode
		{
			get
			{
				return (!this.calculatedHashCode) ? (this._hashCode = global::NGCConfiguration.PrefabEntry.hash(this.guidText)) : this._hashCode;
			}
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0006DB24 File Offset: 0x0006BD24
		public override int GetHashCode()
		{
			return (!this.calculatedHashCode) ? (this._hashCode = global::NGCConfiguration.PrefabEntry.hash(this.guidText)) : this._hashCode;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0006DB5C File Offset: 0x0006BD5C
		public override string ToString()
		{
			return string.Format("[PrefabEntry: Name=\"{1}\", HashCode={0:X}, Path=\"{2}\"]", this.HashCode, this.Name, this.Path);
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0006DB8C File Offset: 0x0006BD8C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0006DB94 File Offset: 0x0006BD94
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0006DB9C File Offset: 0x0006BD9C
		public bool ReadyToRegister
		{
			get
			{
				return !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Path) && this.HashCode != 0;
			}
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0006DBD8 File Offset: 0x0006BDD8
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
			using (IEnumerator<int> enumerator = global::NGCConfiguration.PrefabEntry.ParseInts(guid))
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
			global::NGCConfiguration.PrefabEntry.hashwork.guid[0] = num;
			global::NGCConfiguration.PrefabEntry.hashwork.guid[1] = num6;
			global::NGCConfiguration.PrefabEntry.hashwork.guid[2] = num5;
			global::NGCConfiguration.PrefabEntry.hashwork.guid[3] = num3;
			global::NGCConfiguration.PrefabEntry.hashwork.guid[4] = num4;
			global::NGCConfiguration.PrefabEntry.hashwork.guid[5] = num2;
			return Facepunch.Hash.MurmurHash2.SINT(global::NGCConfiguration.PrefabEntry.hashwork.guid, global::NGCConfiguration.PrefabEntry.hashwork.guid.Length, 2260766486u);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0006DCFC File Offset: 0x0006BEFC
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

		// Token: 0x04000F1A RID: 3866
		private const uint peSeed = 2260766486u;

		// Token: 0x04000F1B RID: 3867
		[SerializeField]
		private string name = "!unnamed";

		// Token: 0x04000F1C RID: 3868
		[SerializeField]
		private string path = string.Empty;

		// Token: 0x04000F1D RID: 3869
		[SerializeField]
		private string guidText = string.Empty;

		// Token: 0x04000F1E RID: 3870
		[NonSerialized]
		private bool calculatedHashCode;

		// Token: 0x04000F1F RID: 3871
		[NonSerialized]
		private int _hashCode;

		// Token: 0x02000397 RID: 919
		private static class hashwork
		{
			// Token: 0x04000F20 RID: 3872
			public static readonly int[] guid = new int[6];
		}
	}
}
