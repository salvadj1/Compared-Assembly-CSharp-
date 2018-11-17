using System;
using System.Collections.Generic;

namespace MoPhoGames.USpeak.Core.Utils
{
	// Token: 0x020000D3 RID: 211
	public class USpeakPoolUtils
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x00016C94 File Offset: 0x00014E94
		public static float[] GetFloat(int length)
		{
			for (int i = 0; i < USpeakPoolUtils.FloatPool.Count; i++)
			{
				if (USpeakPoolUtils.FloatPool[i].Length == length)
				{
					float[] result = USpeakPoolUtils.FloatPool[i];
					USpeakPoolUtils.FloatPool.RemoveAt(i);
					return result;
				}
			}
			return new float[length];
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00016CF0 File Offset: 0x00014EF0
		public static short[] GetShort(int length)
		{
			for (int i = 0; i < USpeakPoolUtils.ShortPool.Count; i++)
			{
				if (USpeakPoolUtils.ShortPool[i].Length == length)
				{
					short[] result = USpeakPoolUtils.ShortPool[i];
					USpeakPoolUtils.ShortPool.RemoveAt(i);
					return result;
				}
			}
			return new short[length];
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00016D4C File Offset: 0x00014F4C
		public static byte[] GetByte(int length)
		{
			for (int i = 0; i < USpeakPoolUtils.BytePool.Count; i++)
			{
				if (USpeakPoolUtils.BytePool[i].Length == length)
				{
					byte[] result = USpeakPoolUtils.BytePool[i];
					USpeakPoolUtils.BytePool.RemoveAt(i);
					return result;
				}
			}
			return new byte[length];
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public static void Return(float[] d)
		{
			USpeakPoolUtils.FloatPool.Add(d);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00016DB8 File Offset: 0x00014FB8
		public static void Return(byte[] d)
		{
			USpeakPoolUtils.BytePool.Add(d);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00016DC8 File Offset: 0x00014FC8
		public static void Return(short[] d)
		{
			USpeakPoolUtils.ShortPool.Add(d);
		}

		// Token: 0x0400040C RID: 1036
		private static List<byte[]> BytePool = new List<byte[]>();

		// Token: 0x0400040D RID: 1037
		private static List<short[]> ShortPool = new List<short[]>();

		// Token: 0x0400040E RID: 1038
		private static List<float[]> FloatPool = new List<float[]>();
	}
}
