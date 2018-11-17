using System;

namespace MoPhoGames.USpeak.Core
{
	// Token: 0x020000D4 RID: 212
	public class USpeakSettingsData
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x00016DD8 File Offset: 0x00014FD8
		public USpeakSettingsData()
		{
			this.bandMode = global::BandMode.Narrow;
			this.Codec = 0;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00016DF0 File Offset: 0x00014FF0
		public USpeakSettingsData(byte src)
		{
			if ((src & 1) == 1)
			{
				this.bandMode = global::BandMode.Narrow;
			}
			else if ((src & 2) == 2)
			{
				this.bandMode = global::BandMode.Wide;
			}
			else
			{
				this.bandMode = global::BandMode.UltraWide;
			}
			this.Codec = src >> 2;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00016E40 File Offset: 0x00015040
		public byte ToByte()
		{
			byte b = 0;
			if (this.bandMode == global::BandMode.Narrow)
			{
				b |= 1;
			}
			else if (this.bandMode == global::BandMode.Wide)
			{
				b |= 2;
			}
			return b | (byte)(this.Codec << 2);
		}

		// Token: 0x0400040F RID: 1039
		public global::BandMode bandMode;

		// Token: 0x04000410 RID: 1040
		public int Codec;
	}
}
