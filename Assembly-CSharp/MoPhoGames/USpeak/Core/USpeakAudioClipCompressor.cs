using System;
using System.Collections.Generic;
using System.IO;
using MoPhoGames.USpeak.Codec;
using MoPhoGames.USpeak.Core.Utils;
using UnityEngine;

namespace MoPhoGames.USpeak.Core
{
	// Token: 0x020000BB RID: 187
	public class USpeakAudioClipCompressor : MonoBehaviour
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x00014F60 File Offset: 0x00013160
		public static byte[] CompressAudioData(float[] samples, int channels, out int sample_count, BandMode mode, ICodec Codec, float gain = 1f)
		{
			USpeakAudioClipCompressor.data.Clear();
			sample_count = 0;
			short[] d = USpeakAudioClipConverter.AudioDataToShorts(samples, channels, gain);
			byte[] array = Codec.Encode(d, mode);
			USpeakPoolUtils.Return(d);
			USpeakAudioClipCompressor.data.AddRange(array);
			USpeakPoolUtils.Return(array);
			return USpeakAudioClipCompressor.data.ToArray();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00014FB0 File Offset: 0x000131B0
		public static float[] DecompressAudio(byte[] data, int samples, int channels, bool threeD, BandMode mode, ICodec Codec, float gain)
		{
			int frequency = 4000;
			if (mode == BandMode.Narrow)
			{
				frequency = 8000;
			}
			else if (mode == BandMode.Wide)
			{
				frequency = 16000;
			}
			short[] array = Codec.Decode(data, mode);
			USpeakAudioClipCompressor.tmp.Clear();
			USpeakAudioClipCompressor.tmp.AddRange(array);
			USpeakPoolUtils.Return(array);
			return USpeakAudioClipConverter.ShortsToAudioData(USpeakAudioClipCompressor.tmp.ToArray(), channels, frequency, threeD, gain);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00015020 File Offset: 0x00013220
		private static void CopyStream(Stream input, Stream output)
		{
			byte[] @byte = USpeakPoolUtils.GetByte(32768);
			for (;;)
			{
				int num = input.Read(@byte, 0, @byte.Length);
				if (num <= 0)
				{
					break;
				}
				output.Write(@byte, 0, num);
			}
			USpeakPoolUtils.Return(@byte);
		}

		// Token: 0x04000395 RID: 917
		private static List<byte> data = new List<byte>();

		// Token: 0x04000396 RID: 918
		private static List<short> tmp = new List<short>();
	}
}
