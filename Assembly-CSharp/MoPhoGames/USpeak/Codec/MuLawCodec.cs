using System;
using MoPhoGames.USpeak.Core.Utils;

namespace MoPhoGames.USpeak.Codec
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	public class MuLawCodec : ICodec
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x0001556C File Offset: 0x0001376C
		public byte[] Encode(short[] data, global::BandMode mode)
		{
			return MuLawCodec.MuLawEncoder.MuLawEncode(data);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00015574 File Offset: 0x00013774
		public short[] Decode(byte[] data, global::BandMode mode)
		{
			return MuLawCodec.MuLawDecoder.MuLawDecode(data);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001557C File Offset: 0x0001377C
		public int GetSampleSize(int recordingFrequency)
		{
			return 0;
		}

		// Token: 0x020000C7 RID: 199
		private class MuLawEncoder
		{
			// Token: 0x0600044C RID: 1100 RVA: 0x00015588 File Offset: 0x00013788
			static MuLawEncoder()
			{
				for (int i = -32768; i <= 32767; i++)
				{
					MuLawCodec.MuLawEncoder.pcmToMuLawMap[i & 65535] = MuLawCodec.MuLawEncoder.encode(i);
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x0600044D RID: 1101 RVA: 0x000155D4 File Offset: 0x000137D4
			// (set) Token: 0x0600044E RID: 1102 RVA: 0x000155E8 File Offset: 0x000137E8
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

			// Token: 0x0600044F RID: 1103 RVA: 0x00015628 File Offset: 0x00013828
			public static byte MuLawEncode(int pcm)
			{
				return MuLawCodec.MuLawEncoder.pcmToMuLawMap[pcm & 65535];
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x00015638 File Offset: 0x00013838
			public static byte MuLawEncode(short pcm)
			{
				return MuLawCodec.MuLawEncoder.pcmToMuLawMap[(int)pcm & 65535];
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x00015648 File Offset: 0x00013848
			public static byte[] MuLawEncode(int[] pcm)
			{
				int num = pcm.Length;
				byte[] @byte = MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.GetByte(num);
				for (int i = 0; i < num; i++)
				{
					@byte[i] = MuLawCodec.MuLawEncoder.MuLawEncode(pcm[i]);
				}
				return @byte;
			}

			// Token: 0x06000452 RID: 1106 RVA: 0x00015680 File Offset: 0x00013880
			public static byte[] MuLawEncode(short[] pcm)
			{
				int num = pcm.Length;
				byte[] @byte = MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.GetByte(num);
				for (int i = 0; i < num; i++)
				{
					@byte[i] = MuLawCodec.MuLawEncoder.MuLawEncode(pcm[i]);
				}
				return @byte;
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x000156B8 File Offset: 0x000138B8
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

			// Token: 0x040003C0 RID: 960
			public const int BIAS = 132;

			// Token: 0x040003C1 RID: 961
			public const int MAX = 32635;

			// Token: 0x040003C2 RID: 962
			private static byte[] pcmToMuLawMap = new byte[65536];
		}

		// Token: 0x020000C8 RID: 200
		private class MuLawDecoder
		{
			// Token: 0x06000455 RID: 1109 RVA: 0x00015734 File Offset: 0x00013934
			static MuLawDecoder()
			{
				for (byte b = 0; b < 255; b += 1)
				{
					MuLawCodec.MuLawDecoder.muLawToPcmMap[(int)b] = MuLawCodec.MuLawDecoder.Decode(b);
				}
			}

			// Token: 0x06000456 RID: 1110 RVA: 0x00015774 File Offset: 0x00013974
			public static short[] MuLawDecode(byte[] data)
			{
				int num = data.Length;
				short[] @short = MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.GetShort(num);
				for (int i = 0; i < num; i++)
				{
					@short[i] = MuLawCodec.MuLawDecoder.muLawToPcmMap[(int)data[i]];
				}
				return @short;
			}

			// Token: 0x06000457 RID: 1111 RVA: 0x000157AC File Offset: 0x000139AC
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

			// Token: 0x040003C3 RID: 963
			private static readonly short[] muLawToPcmMap = new short[256];
		}
	}
}
