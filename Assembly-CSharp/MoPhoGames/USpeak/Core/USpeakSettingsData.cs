using System;

namespace MoPhoGames.USpeak.Core
{
	// Token: 0x020000C0 RID: 192
	public class USpeakSettingsData
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x00015410 File Offset: 0x00013610
		public USpeakSettingsData()
		{
			this.bandMode = BandMode.Narrow;
			this.Codec = 0;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00015428 File Offset: 0x00013628
		public USpeakSettingsData(byte src)
		{
			if ((src & 1) == 1)
			{
				this.bandMode = BandMode.Narrow;
			}
			else if ((src & 2) == 2)
			{
				this.bandMode = BandMode.Wide;
			}
			else
			{
				this.bandMode = BandMode.UltraWide;
			}
			this.Codec = src >> 2;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00015478 File Offset: 0x00013678
		public byte ToByte()
		{
			byte b = 0;
			if (this.bandMode == BandMode.Narrow)
			{
				b |= 1;
			}
			else if (this.bandMode == BandMode.Wide)
			{
				b |= 2;
			}
			return b | (byte)(this.Codec << 2);
		}

		// Token: 0x040003A0 RID: 928
		public BandMode bandMode;

		// Token: 0x040003A1 RID: 929
		public int Codec;
	}
}
