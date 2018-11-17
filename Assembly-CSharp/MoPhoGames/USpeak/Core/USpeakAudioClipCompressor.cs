using System;
using System.Collections.Generic;
using System.IO;
using MoPhoGames.USpeak.Codec;
using MoPhoGames.USpeak.Core.Utils;
using UnityEngine;

namespace MoPhoGames.USpeak.Core
{
	// Token: 0x020000CF RID: 207
	public class USpeakAudioClipCompressor : MonoBehaviour
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00016928 File Offset: 0x00014B28
		public static byte[] CompressAudioData(float[] samples, int channels, out int sample_count, global::BandMode mode, MoPhoGames.USpeak.Codec.ICodec Codec, float gain = 1f)
		{
			USpeakAudioClipCompressor.data.Clear();
			sample_count = 0;
			short[] d = USpeakAudioClipConverter.AudioDataToShorts(samples, channels, gain);
			byte[] array = Codec.Encode(d, mode);
			MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.Return(d);
			USpeakAudioClipCompressor.data.AddRange(array);
			MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.Return(array);
			return USpeakAudioClipCompressor.data.ToArray();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00016978 File Offset: 0x00014B78
		public static float[] DecompressAudio(byte[] data, int samples, int channels, bool threeD, global::BandMode mode, MoPhoGames.USpeak.Codec.ICodec Codec, float gain)
		{
			int frequency = 4000;
			if (mode == global::BandMode.Narrow)
			{
				frequency = 8000;
			}
			else if (mode == global::BandMode.Wide)
			{
				frequency = 16000;
			}
			short[] array = Codec.Decode(data, mode);
			USpeakAudioClipCompressor.tmp.Clear();
			USpeakAudioClipCompressor.tmp.AddRange(array);
			MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.Return(array);
			return USpeakAudioClipConverter.ShortsToAudioData(USpeakAudioClipCompressor.tmp.ToArray(), channels, frequency, threeD, gain);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000169E8 File Offset: 0x00014BE8
		private static void CopyStream(Stream input, Stream output)
		{
			byte[] @byte = MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.GetByte(32768);
			for (;;)
			{
				int num = input.Read(@byte, 0, @byte.Length);
				if (num <= 0)
				{
					break;
				}
				output.Write(@byte, 0, num);
			}
			MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.Return(@byte);
		}

		// Token: 0x04000404 RID: 1028
		private static List<byte> data = new List<byte>();

		// Token: 0x04000405 RID: 1029
		private static List<short> tmp = new List<short>();
	}
}
