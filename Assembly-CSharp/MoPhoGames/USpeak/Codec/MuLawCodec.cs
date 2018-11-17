using System;
using MoPhoGames.USpeak.Core.Utils;

namespace MoPhoGames.USpeak.Codec
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public class MuLawCodec : ICodec
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x00013D7C File Offset: 0x00011F7C
		public byte[] Encode(short[] data, BandMode mode)
		{
			return MuLawCodec.MuLawEncoder.MuLawEncode(data);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00013D84 File Offset: 0x00011F84
		public short[] Decode(byte[] data, BandMode mode)
		{
			return MuLawCodec.MuLawDecoder.MuLawDecode(data);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00013D8C File Offset: 0x00011F8C
		public int GetSampleSize(int recordingFrequency)
		{
			return 0;
		}

		// Token: 0x020000B4 RID: 180
		private class MuLawEncoder
		{
			// Token: 0x060003D4 RID: 980 RVA: 0x00013D98 File Offset: 0x00011F98
			static MuLawEncoder()
			{
				for (int i = -32768; i <= 32767; i++)
				{
					MuLawCodec.MuLawEncoder.pcmToMuLawMap[i & 65535] = MuLawCodec.MuLawEncoder.encode(i);
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060003D5 RID: 981 RVA: 0x00013DE4 File Offset: 0x00011FE4
			// (set) Token: 0x060003D6 RID: 982 RVA: 0x00013DF8 File Offset: 0x00011FF8
			public static bool ZeroTrap
			{
				get
				{
					return MuLawCodec.MuLawEncoder.pcmToMuLawMap[33000] != 0;
				}
				set
				{
					byte b = (!value) ? 0 : 2;
					for (int i = 32768; i <= 33924; i++)
					{
						MuLawCodec.MuLawEncoder.pcmToMuLawMap[i] = b;
					}
				}
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x00013E38 File Offset: 0x00012038
			public static byte MuLawEncode(int pcm)
			{
				return MuLawCodec.MuLawEncoder.pcmToMuLawMap[pcm & 65535];
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x00013E48 File Offset: 0x00012048
			public static byte MuLawEncode(short pcm)
			{
				return MuLawCodec.MuLawEncoder.pcmToMuLawMap[(int)pcm & 65535];
			}

			// Token: 0x060003D9 RID: 985 RVA: 0x00013E58 File Offset: 0x00012058
			public static byte[] MuLawEncode(int[] pcm)
			{
				int num = pcm.Length;
				byte[] @byte = USpeakPoolUtils.GetByte(num);
				for (int i = 0; i < num; i++)
				{
					@byte[i] = MuLawCodec.MuLawEncoder.MuLawEncode(pcm[i]);
				}
				return @byte;
			}

			// Token: 0x060003DA RID: 986 RVA: 0x00013E90 File Offset: 0x00012090
			public static byte[] MuLawEncode(short[] pcm)
			{
				int num = pcm.Length;
				byte[] @byte = USpeakPoolUtils.GetByte(num);
				for (int i = 0; i < num; i++)
				{
					@byte[i] = MuLawCodec.MuLawEncoder.MuLawEncode(pcm[i]);
				}
				return @byte;
			}

			// Token: 0x060003DB RID: 987 RVA: 0x00013EC8 File Offset: 0x000120C8
			private static byte encode(int pcm)
			{
				int num = (pcm & 32768) >> 8;
				if (num != 0)
				{
					pcm = -pcm;
				}
				if (pcm > 32635)
				{
					pcm = 32635;
				}
				pcm += 132;
				int num2 = 7;
				int num3 = 16384;
				while ((pcm & num3) == 0)
				{
					num2--;
					num3 >>= 1;
				}
				int num4 = pcm >> num2 + 3 & 15;
				byte b = (byte)(num | num2 << 4 | num4);
				return ~b;
			}

			// Token: 0x04000355 RID: 853
			public const int BIAS = 132;

			// Token: 0x04000356 RID: 854
			public const int MAX = 32635;

			// Token: 0x04000357 RID: 855
			private static byte[] pcmToMuLawMap = new byte[65536];
		}

		// Token: 0x020000B5 RID: 181
		private class MuLawDecoder
		{
			// Token: 0x060003DD RID: 989 RVA: 0x00013F44 File Offset: 0x00012144
			static MuLawDecoder()
			{
				for (byte b = 0; b < 255; b += 1)
				{
					MuLawCodec.MuLawDecoder.muLawToPcmMap[(int)b] = MuLawCodec.MuLawDecoder.Decode(b);
				}
			}

			// Token: 0x060003DE RID: 990 RVA: 0x00013F84 File Offset: 0x00012184
			public static short[] MuLawDecode(byte[] data)
			{
				int num = data.Length;
				short[] @short = USpeakPoolUtils.GetShort(num);
				for (int i = 0; i < num; i++)
				{
					@short[i] = MuLawCodec.MuLawDecoder.muLawToPcmMap[(int)data[i]];
				}
				return @short;
			}

			// Token: 0x060003DF RID: 991 RVA: 0x00013FBC File Offset: 0x000121BC
			private static short Decode(byte mulaw)
			{
				mulaw = ~mulaw;
				int num = (int)(mulaw & 128);
				int num2 = (mulaw & 112) >> 4;
				int num3 = (int)(mulaw & 15);
				num3 |= 16;
				num3 <<= 1;
				num3++;
				num3 <<= num2 + 2;
				num3 -= 132;
				return (short)((num != 0) ? (-(short)num3) : num3);
			}

			// Token: 0x04000358 RID: 856
			private static readonly short[] muLawToPcmMap = new short[256];
		}
	}
}
