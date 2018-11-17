using System;
using MoPhoGames.USpeak.Core.Utils;
using UnityEngine;

namespace MoPhoGames.USpeak.Core
{
	// Token: 0x020000BC RID: 188
	public class USpeakAudioClipConverter
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0001506C File Offset: 0x0001326C
		public static short[] AudioDataToShorts(float[] samples, int channels, float gain = 1f)
		{
			short[] @short = USpeakPoolUtils.GetShort(samples.Length * channels);
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

		// Token: 0x06000405 RID: 1029 RVA: 0x000150D8 File Offset: 0x000132D8
		public static float[] ShortsToAudioData(short[] data, int channels, int frequency, bool threedimensional, float gain)
		{
			float[] @float = USpeakPoolUtils.GetFloat(data.Length);
			for (int i = 0; i < @float.Length; i++)
			{
				int num = (int)data[i];
				@float[i] = (float)num / 3267f * gain;
			}
			return @float;
		}
	}
}
