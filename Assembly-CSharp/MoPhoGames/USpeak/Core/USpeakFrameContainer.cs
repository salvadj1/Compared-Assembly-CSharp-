using System;

namespace MoPhoGames.USpeak.Core
{
	// Token: 0x020000D2 RID: 210
	public struct USpeakFrameContainer
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x00016BBC File Offset: 0x00014DBC
		public void LoadFrom(byte[] source)
		{
			int num = BitConverter.ToInt32(source, 0);
			this.Samples = BitConverter.ToUInt16(source, 4);
			this.encodedData = new byte[num];
			Array.Copy(source, 6, this.encodedData, 0, num);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00016BFC File Offset: 0x00014DFC
		public byte[] ToByteArray()
		{
			byte[] array = new byte[6 + this.encodedData.Length];
			byte[] bytes = BitConverter.GetBytes(this.encodedData.Length);
			bytes.CopyTo(array, 0);
			byte[] bytes2 = BitConverter.GetBytes(this.Samples);
			Array.Copy(bytes2, 0, array, 4, 2);
			for (int i = 0; i < this.encodedData.Length; i++)
			{
				array[i + 6] = this.encodedData[i];
			}
			return array;
		}

		// Token: 0x0400040A RID: 1034
		public ushort Samples;

		// Token: 0x0400040B RID: 1035
		public byte[] encodedData;
	}
}
