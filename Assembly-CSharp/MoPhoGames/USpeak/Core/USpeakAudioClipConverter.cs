using System;
using MoPhoGames.USpeak.Core.Utils;
using UnityEngine;

namespace MoPhoGames.USpeak.Core
{
	// Token: 0x020000D0 RID: 208
	public class USpeakAudioClipConverter
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x00016A34 File Offset: 0x00014C34
		public static short[] AudioDataToShorts(float[] samples, int channels, float gain = 1f)
		{
			short[] @short = MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.GetShort(samples.Length * channels);
			for (int i = 0; i < samples.Length; i++)
			{
				float num = samples[i] * gain;
				if (Mathf.Abs(num) > 1f)
				{
					if (num > 0f)
					{
						num = 1f;
					}
					else
					{
						num = -1f;
					}
				}
				float num2 = num * 3267f;
				@short[i] = (short)num2;
			}
			return @short;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00016AA0 File Offset: 0x00014CA0
		public static float[] ShortsToAudioData(short[] data, int channels, int frequency, bool threedimensional, float gain)
		{
			float[] @float = MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.GetFloat(data.Length);
			for (int i = 0; i < @float.Length; i++)
			{
				int num = (int)data[i];
				@float[i] = (float)num / 3267f * gain;
			}
			return @float;
		}
	}
}
