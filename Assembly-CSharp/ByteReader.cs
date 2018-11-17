using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000871 RID: 2161
public class ByteReader
{
	// Token: 0x06004A33 RID: 18995 RVA: 0x0011E124 File Offset: 0x0011C324
	public ByteReader(byte[] bytes)
	{
		this.mBuffer = bytes;
	}

	// Token: 0x06004A34 RID: 18996 RVA: 0x0011E134 File Offset: 0x0011C334
	public ByteReader(TextAsset asset)
	{
		this.mBuffer = asset.bytes;
	}

	// Token: 0x17000E20 RID: 3616
	// (get) Token: 0x06004A35 RID: 18997 RVA: 0x0011E148 File Offset: 0x0011C348
	public bool canRead
	{
		get
		{
			return this.mBuffer != null && this.mOffset < this.mBuffer.Length;
		}
	}

	// Token: 0x06004A36 RID: 18998 RVA: 0x0011E168 File Offset: 0x0011C368
	private static string ReadLine(byte[] buffer, int start, int count)
	{
		return Encoding.UTF8.GetString(buffer, start, count);
	}

	// Token: 0x06004A37 RID: 18999 RVA: 0x0011E178 File Offset: 0x0011C378
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
					string result = global::ByteReader.ReadLine(this.mBuffer, this.mOffset, i - this.mOffset - 1);
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

	// Token: 0x06004A38 RID: 19000 RVA: 0x0011E238 File Offset: 0x0011C438
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

	// Token: 0x04002881 RID: 10369
	private byte[] mBuffer;

	// Token: 0x04002882 RID: 10370
	private int mOffset;
}
