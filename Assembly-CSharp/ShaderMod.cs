using System;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class ShaderMod : ScriptableObject
{
	// Token: 0x17000823 RID: 2083
	public ShaderMod.DICT this[ShaderMod.Replacement replacement]
	{
		get
		{
			switch (replacement)
			{
			case ShaderMod.Replacement.Include:
				return this.replaceIncludes;
			case ShaderMod.Replacement.Queue:
				return this.replaceQueues;
			case ShaderMod.Replacement.Define:
				return this.macroDefines;
			default:
				return null;
			}
		}
	}

	// Token: 0x06002113 RID: 8467 RVA: 0x0008182C File Offset: 0x0007FA2C
	public bool Replace(ShaderMod.Replacement replacement, string incoming, ref string outgoing)
	{
		ShaderMod.DICT dict = this[replacement];
		return dict != null && dict.Replace(replacement, incoming, ref outgoing);
	}

	// Token: 0x04000F7E RID: 3966
	public ShaderMod.DICT replaceIncludes;

	// Token: 0x04000F7F RID: 3967
	public ShaderMod.DICT replaceQueues;

	// Token: 0x04000F80 RID: 3968
	public ShaderMod.DICT macroDefines;

	// Token: 0x04000F81 RID: 3969
	public string[] preIncludes;

	// Token: 0x04000F82 RID: 3970
	public string[] postIncludes;

	// Token: 0x0200035C RID: 860
	[Serializable]
	public class KV
	{
		// Token: 0x06002114 RID: 8468 RVA: 0x00081854 File Offset: 0x0007FA54
		public KV()
		{
			this.key = string.Empty;
			this.value = string.Empty;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x00081874 File Offset: 0x0007FA74
		public KV(string key, string value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0008188C File Offset: 0x0007FA8C
		public override int GetHashCode()
		{
			return (this.key != null) ? this.key.GetHashCode() : 0;
		}

		// Token: 0x04000F83 RID: 3971
		public string key;

		// Token: 0x04000F84 RID: 3972
		public string value;
	}

	// Token: 0x0200035D RID: 861
	public static class QueueCompare
	{
		// Token: 0x06002118 RID: 8472 RVA: 0x000818C4 File Offset: 0x0007FAC4
		public static int ToInt32(string queue)
		{
			if (queue == null || queue.Length == 0)
			{
				return 2000;
			}
			int num = queue.IndexOfAny(ShaderMod.QueueCompare.signChars);
			int num2;
			if (num != -1)
			{
				queue = queue.Substring(0, num);
				num2 = int.Parse(queue.Substring(num));
			}
			else
			{
				num2 = 0;
			}
			string text = (queue = queue.Trim()).ToLowerInvariant();
			switch (text)
			{
			case "geometry":
				return 2000 + num2;
			case "alphatest":
				return 2450 + num2;
			case "transparent":
				return 3000 + num2;
			case "background":
				return 1000 + num2;
			case "overlay":
				return 4000 + num2;
			}
			return (!int.TryParse(queue, out num2)) ? 2000 : num2;
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000819F4 File Offset: 0x0007FBF4
		public static bool Equals(string queue1, string queue2)
		{
			return ShaderMod.QueueCompare.ToInt32(queue1) == ShaderMod.QueueCompare.ToInt32(queue2);
		}

		// Token: 0x04000F85 RID: 3973
		public const int kBackground = 1000;

		// Token: 0x04000F86 RID: 3974
		public const int kGeometry = 2000;

		// Token: 0x04000F87 RID: 3975
		public const int kAlphaTest = 2450;

		// Token: 0x04000F88 RID: 3976
		public const int kTransparent = 3000;

		// Token: 0x04000F89 RID: 3977
		public const int kOverlay = 4000;

		// Token: 0x04000F8A RID: 3978
		public const int kDefault = 2000;

		// Token: 0x04000F8B RID: 3979
		private static readonly char[] signChars = new char[]
		{
			'-',
			'+'
		};
	}

	// Token: 0x0200035E RID: 862
	[Serializable]
	public class DICT
	{
		// Token: 0x17000824 RID: 2084
		public string this[string key]
		{
			get
			{
				foreach (ShaderMod.KV kv in this.keyValues)
				{
					if (kv.key == key)
					{
						return kv.value;
					}
				}
				return null;
			}
			set
			{
				int num = -1;
				while (++num < this.keyValues.Length)
				{
					if (this.keyValues[num].key == key)
					{
						if (value == null)
						{
							this.keyValues[num] = this.keyValues[this.keyValues.Length - 1];
							Array.Resize<ShaderMod.KV>(ref this.keyValues, this.keyValues.Length - 1);
						}
						else
						{
							this.keyValues[num].value = value;
						}
					}
				}
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				Array.Resize<ShaderMod.KV>(ref this.keyValues, this.keyValues.Length + 1);
				this.keyValues[this.keyValues.Length - 1] = new ShaderMod.KV(key, value);
			}
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00081B24 File Offset: 0x0007FD24
		public bool Replace(ShaderMod.Replacement replacement, string incoming, ref string outgoing)
		{
			if (this.keyValues != null)
			{
				if (replacement != ShaderMod.Replacement.Queue)
				{
					for (int i = 0; i < this.keyValues.Length; i++)
					{
						if (string.Equals(this.keyValues[i].key, incoming, StringComparison.InvariantCultureIgnoreCase))
						{
							outgoing = this.keyValues[i].value;
							return true;
						}
					}
				}
				else
				{
					for (int j = 0; j < this.keyValues.Length; j++)
					{
						if (ShaderMod.QueueCompare.Equals(this.keyValues[j].key, incoming))
						{
							outgoing = this.keyValues[j].value;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04000F8D RID: 3981
		public ShaderMod.KV[] keyValues;
	}

	// Token: 0x0200035F RID: 863
	public enum Replacement
	{
		// Token: 0x04000F8F RID: 3983
		Include,
		// Token: 0x04000F90 RID: 3984
		Queue,
		// Token: 0x04000F91 RID: 3985
		Define
	}
}
