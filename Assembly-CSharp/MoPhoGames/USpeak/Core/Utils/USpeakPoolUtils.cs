using System;
using System.Collections.Generic;

namespace MoPhoGames.USpeak.Core.Utils
{
	// Token: 0x020000BF RID: 191
	public class USpeakPoolUtils
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x000152CC File Offset: 0x000134CC
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

		// Token: 0x0600040D RID: 1037 RVA: 0x00015328 File Offset: 0x00013528
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

		// Token: 0x0600040E RID: 1038 RVA: 0x00015384 File Offset: 0x00013584
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

		// Token: 0x0600040F RID: 1039 RVA: 0x000153E0 File Offset: 0x000135E0
		public static void Return(float[] d)
		{
			USpeakPoolUtils.FloatPool.Add(d);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000153F0 File Offset: 0x000135F0
		public static void Return(byte[] d)
		{
			USpeakPoolUtils.BytePool.Add(d);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00015400 File Offset: 0x00013600
		public static void Return(short[] d)
		{
			USpeakPoolUtils.ShortPool.Add(d);
		}

		// Token: 0x0400039D RID: 925
		private static List<byte[]> BytePool = new List<byte[]>();

		// Token: 0x0400039E RID: 926
		private static List<short[]> ShortPool = new List<short[]>();

		// Token: 0x0400039F RID: 927
		private static List<float[]> FloatPool = new List<float[]>();
	}
}
