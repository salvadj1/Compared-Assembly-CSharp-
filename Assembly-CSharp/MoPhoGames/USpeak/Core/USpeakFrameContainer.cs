using System;

namespace MoPhoGames.USpeak.Core
{
	// Token: 0x020000BE RID: 190
	public struct USpeakFrameContainer
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x000151F4 File Offset: 0x000133F4
		public void LoadFrom(byte[] source)
		{
			int num = BitConverter.ToInt32(source, 0);
			this.Samples = BitConverter.ToUInt16(source, 4);
			this.encodedData = new byte[num];
			Array.Copy(source, 6, this.encodedData, 0, num);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00015234 File Offset: 0x00013434
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

		// Token: 0x0400039B RID: 923
		public ushort Samples;

		// Token: 0x0400039C RID: 924
		public byte[] encodedData;
	}
}
