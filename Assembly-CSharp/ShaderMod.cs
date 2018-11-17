using System;
using UnityEngine;

// Token: 0x02000408 RID: 1032
public class ShaderMod : ScriptableObject
{
	// Token: 0x17000881 RID: 2177
	public global::ShaderMod.DICT this[global::ShaderMod.Replacement replacement]
	{
		get
		{
			switch (replacement)
			{
			case global::ShaderMod.Replacement.Include:
				return this.replaceIncludes;
			case global::ShaderMod.Replacement.Queue:
				return this.replaceQueues;
			case global::ShaderMod.Replacement.Define:
				return this.macroDefines;
			default:
				return null;
			}
		}
	}

	// Token: 0x06002475 RID: 9333 RVA: 0x00086C28 File Offset: 0x00084E28
	public bool Replace(global::ShaderMod.Replacement replacement, string incoming, ref string outgoing)
	{
		global::ShaderMod.DICT dict = this[replacement];
		return dict != null && dict.Replace(replacement, incoming, ref outgoing);
	}

	// Token: 0x040010E4 RID: 4324
	public global::ShaderMod.DICT replaceIncludes;

	// Token: 0x040010E5 RID: 4325
	public global::ShaderMod.DICT replaceQueues;

	// Token: 0x040010E6 RID: 4326
	public global::ShaderMod.DICT macroDefines;

	// Token: 0x040010E7 RID: 4327
	public string[] preIncludes;

	// Token: 0x040010E8 RID: 4328
	public string[] postIncludes;

	// Token: 0x02000409 RID: 1033
	[Serializable]
	public class KV
	{
		// Token: 0x06002476 RID: 9334 RVA: 0x00086C50 File Offset: 0x00084E50
		public KV()
		{
			this.key = string.Empty;
			this.value = string.Empty;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00086C70 File Offset: 0x00084E70
		public KV(string key, string value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00086C88 File Offset: 0x00084E88
		public override int GetHashCode()
		{
			return (this.key != null) ? this.key.GetHashCode() : 0;
		}

		// Token: 0x040010E9 RID: 4329
		public string key;

		// Token: 0x040010EA RID: 4330
		public string value;
	}

	// Token: 0x0200040A RID: 1034
	public static class QueueCompare
	{
		// Token: 0x0600247A RID: 9338 RVA: 0x00086CC0 File Offset: 0x00084EC0
		public static int ToInt32(string queue)
		{
			if (queue == null || queue.Length == 0)
			{
				return 2000;
			}
			int num = queue.IndexOfAny(global::ShaderMod.QueueCompare.signChars);
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

		// Token: 0x0600247B RID: 9339 RVA: 0x00086DF0 File Offset: 0x00084FF0
		public static bool Equals(string queue1, string queue2)
		{
			return global::ShaderMod.QueueCompare.ToInt32(queue1) == global::ShaderMod.QueueCompare.ToInt32(queue2);
		}

		// Token: 0x040010EB RID: 4331
		public const int kBackground = 1000;

		// Token: 0x040010EC RID: 4332
		public const int kGeometry = 2000;

		// Token: 0x040010ED RID: 4333
		public const int kAlphaTest = 2450;

		// Token: 0x040010EE RID: 4334
		public const int kTransparent = 3000;

		// Token: 0x040010EF RID: 4335
		public const int kOverlay = 4000;

		// Token: 0x040010F0 RID: 4336
		public const int kDefault = 2000;

		// Token: 0x040010F1 RID: 4337
		private static readonly char[] signChars = new char[]
		{
			'-',
			'+'
		};
	}

	// Token: 0x0200040B RID: 1035
	[Serializable]
	public class DICT
	{
		// Token: 0x17000882 RID: 2178
		public string this[string key]
		{
			get
			{
				foreach (global::ShaderMod.KV kv in this.keyValues)
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
							Array.Resize<global::ShaderMod.KV>(ref this.keyValues, this.keyValues.Length - 1);
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
				Array.Resize<global::ShaderMod.KV>(ref this.keyValues, this.keyValues.Length + 1);
				this.keyValues[this.keyValues.Length - 1] = new global::ShaderMod.KV(key, value);
			}
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00086F20 File Offset: 0x00085120
		public bool Replace(global::ShaderMod.Replacement replacement, string incoming, ref string outgoing)
		{
			if (this.keyValues != null)
			{
				if (replacement != global::ShaderMod.Replacement.Queue)
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
						if (global::ShaderMod.QueueCompare.Equals(this.keyValues[j].key, incoming))
						{
							outgoing = this.keyValues[j].value;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x040010F3 RID: 4339
		public global::ShaderMod.KV[] keyValues;
	}

	// Token: 0x0200040C RID: 1036
	public enum Replacement
	{
		// Token: 0x040010F5 RID: 4341
		Include,
		// Token: 0x040010F6 RID: 4342
		Queue,
		// Token: 0x040010F7 RID: 4343
		Define
	}
}
