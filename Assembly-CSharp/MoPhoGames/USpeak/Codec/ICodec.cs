using System;

namespace MoPhoGames.USpeak.Codec
{
	// Token: 0x020000B2 RID: 178
	public interface ICodec
	{
		// Token: 0x060003CC RID: 972
		byte[] Encode(short[] data, BandMode bandMode);

		// Token: 0x060003CD RID: 973
		short[] Decode(byte[] data, BandMode bandMode);

		// Token: 0x060003CE RID: 974
		int GetSampleSize(int recordingFrequency);
	}
}
