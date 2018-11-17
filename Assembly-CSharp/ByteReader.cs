using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200078C RID: 1932
public class ByteReader
{
	// Token: 0x060045C6 RID: 17862 RVA: 0x001147A4 File Offset: 0x001129A4
	public ByteReader(byte[] bytes)
	{
		this.mBuffer = bytes;
	}

	// Token: 0x060045C7 RID: 17863 RVA: 0x001147B4 File Offset: 0x001129B4
	public ByteReader(TextAsset asset)
	{
		this.mBuffer = asset.bytes;
	}

	// Token: 0x17000D90 RID: 3472
	// (get) Token: 0x060045C8 RID: 17864 RVA: 0x001147C8 File Offset: 0x001129C8
	public bool canRead
	{
		get
		{
			return this.mBuffer != null && this.mOffset < this.mBuffer.Length;
		}
	}

	// Token: 0x060045C9 RID: 17865 RVA: 0x001147E8 File Offset: 0x001129E8
	private static string ReadLine(byte[] buffer, int start, int count)
	{
		return Encoding.UTF8.GetString(buffer, start, count);
	}

	// Token: 0x060045CA RID: 17866 RVA: 0x001147F8 File Offset: 0x001129F8
	public string ReadLine()
	{
		int num = this.mBuffer.Length;
		while (this.mOffset < num && this.mBuffer[this.mOffset] < 32)
		{
			this.mOffset++;
		}
		int i = this.mOffset;
		if (i < num)
		{
			while (i < num)
			{
				int num2 = (int)this.mBuffer[i++];
				if (num2 == 10 || num2 == 13)
				{
					IL_81:
					string result = ByteReader.ReadLine(this.mBuffer, this.mOffset, i - this.mOffset - 1);
					this.mOffset = i;
					return result;
				}
			}
			i++;
			goto IL_81;
		}
		this.mOffset = num;
		return null;
	}

	// Token: 0x060045CB RID: 17867 RVA: 0x001148B8 File Offset: 0x00112AB8
	public Dictionary<string, string> ReadDictionary()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		char[] separator = new char[]
		{
			'='
		};
		while (this.canRead)
		{
			string text = this.ReadLine();
			if (text == null)
			{
				break;
			}
			string[] array = text.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 2)
			{
				string key = array[0].Trim();
				string value = array[1].Trim();
				dictionary[key] = value;
			}
		}
		return dictionary;
	}

	// Token: 0x0400264A RID: 9802
	private byte[] mBuffer;

	// Token: 0x0400264B RID: 9803
	private int mOffset;
}
