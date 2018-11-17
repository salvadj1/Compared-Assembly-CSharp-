using System;

namespace MoPhoGames.USpeak.Codec
{
	// Token: 0x020000C5 RID: 197
	public interface ICodec
	{
		// Token: 0x06000444 RID: 1092
		byte[] Encode(short[] data, global::BandMode bandMode);

		// Token: 0x06000445 RID: 1093
		short[] Decode(byte[] data, global::BandMode bandMode);

		// Token: 0x06000446 RID: 1094
		int GetSampleSize(int recordingFrequency);
	}
}
