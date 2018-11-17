using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200086C RID: 2156
[Serializable]
public class BMFont
{
	// Token: 0x17000E18 RID: 3608
	// (get) Token: 0x06004A16 RID: 18966 RVA: 0x0011D874 File Offset: 0x0011BA74
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0 || this.LegacyCheck();
		}
	}

	// Token: 0x17000E19 RID: 3609
	// (get) Token: 0x06004A17 RID: 18967 RVA: 0x0011D890 File Offset: 0x0011BA90
	// (set) Token: 0x06004A18 RID: 18968 RVA: 0x0011D898 File Offset: 0x0011BA98
	public int charSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			this.mSize = value;
		}
	}

	// Token: 0x17000E1A RID: 3610
	// (get) Token: 0x06004A19 RID: 18969 RVA: 0x0011D8A4 File Offset: 0x0011BAA4
	// (set) Token: 0x06004A1A RID: 18970 RVA: 0x0011D8AC File Offset: 0x0011BAAC
	public int baseOffset
	{
		get
		{
			return this.mBase;
		}
		set
		{
			this.mBase = value;
		}
	}

	// Token: 0x17000E1B RID: 3611
	// (get) Token: 0x06004A1B RID: 18971 RVA: 0x0011D8B8 File Offset: 0x0011BAB8
	// (set) Token: 0x06004A1C RID: 18972 RVA: 0x0011D8C0 File Offset: 0x0011BAC0
	public int texWidth
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			this.mWidth = value;
		}
	}

	// Token: 0x17000E1C RID: 3612
	// (get) Token: 0x06004A1D RID: 18973 RVA: 0x0011D8CC File Offset: 0x0011BACC
	// (set) Token: 0x06004A1E RID: 18974 RVA: 0x0011D8D4 File Offset: 0x0011BAD4
	public int texHeight
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			this.mHeight = value;
		}
	}

	// Token: 0x17000E1D RID: 3613
	// (get) Token: 0x06004A1F RID: 18975 RVA: 0x0011D8E0 File Offset: 0x0011BAE0
	public int glyphCount
	{
		get
		{
			return (!this.isValid) ? 0 : this.mSaved.Count;
		}
	}

	// Token: 0x17000E1E RID: 3614
	// (get) Token: 0x06004A20 RID: 18976 RVA: 0x0011D900 File Offset: 0x0011BB00
	// (set) Token: 0x06004A21 RID: 18977 RVA: 0x0011D908 File Offset: 0x0011BB08
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			this.mSpriteName = value;
		}
	}

	// Token: 0x17000E1F RID: 3615
	// (get) Token: 0x06004A22 RID: 18978 RVA: 0x0011D914 File Offset: 0x0011BB14
	public List<global::BMSymbol> symbols
	{
		get
		{
			return this.mSymbols;
		}
	}

	// Token: 0x06004A23 RID: 18979 RVA: 0x0011D91C File Offset: 0x0011BB1C
	public bool LegacyCheck()
	{
		if (this.mGlyphs != null && this.mGlyphs.Length > 0)
		{
			int i = 0;
			int num = this.mGlyphs.Length;
			while (i < num)
			{
				global::BMGlyph bmglyph = this.mGlyphs[i];
				if (bmglyph != null)
				{
					bmglyph.index = i;
					this.mSaved.Add(bmglyph);
					while (++i < num)
					{
						if (bmglyph != null)
						{
							bmglyph.index = i;
							this.mSaved.Add(bmglyph);
						}
					}
					this.mGlyphs = null;
					return true;
				}
				i++;
			}
			this.mGlyphs = null;
			return false;
		}
		return false;
	}

	// Token: 0x06004A24 RID: 18980 RVA: 0x0011D9BC File Offset: 0x0011BBBC
	private int GetArraySize(int index)
	{
		if (index < 256)
		{
			return 256;
		}
		if (index < 65536)
		{
			return 65536;
		}
		if (index < 262144)
		{
			return 262144;
		}
		return 0;
	}

	// Token: 0x06004A25 RID: 18981 RVA: 0x0011DA00 File Offset: 0x0011BC00
	private static Dictionary<int, global::BMGlyph> CreateGlyphDictionary()
	{
		return new Dictionary<int, global::BMGlyph>();
	}

	// Token: 0x06004A26 RID: 18982 RVA: 0x0011DA08 File Offset: 0x0011BC08
	private static Dictionary<int, global::BMGlyph> CreateGlyphDictionary(int cap)
	{
		return new Dictionary<int, global::BMGlyph>(cap);
	}

	// Token: 0x06004A27 RID: 18983 RVA: 0x0011DA10 File Offset: 0x0011BC10
	public global::BMFont.GetOrCreateGlyphResult GetOrCreateGlyph(int index, out global::BMGlyph glyph)
	{
		if (!this.mDictMade)
		{
			this.mDictMade = true;
			this.mDictAny = true;
			int count = this.mSaved.Count;
			if (count == 0 && this.LegacyCheck())
			{
				count = this.mSaved.Count;
			}
			if (count > 0)
			{
				this.mDict = global::BMFont.CreateGlyphDictionary(count + 1);
				for (int i = count - 1; i >= 0; i--)
				{
					global::BMGlyph bmglyph = this.mSaved[i];
					this.mDict.Add(bmglyph.index, bmglyph);
					if (bmglyph.index == index)
					{
						glyph = bmglyph;
						while (--i >= 0)
						{
							bmglyph = this.mSaved[i];
							this.mDict.Add(bmglyph.index, bmglyph);
						}
						return global::BMFont.GetOrCreateGlyphResult.Found;
					}
				}
			}
			else
			{
				this.mDict = global::BMFont.CreateGlyphDictionary();
			}
		}
		else if (this.mDictAny)
		{
			if (this.mDict.TryGetValue(index, out glyph))
			{
				return global::BMFont.GetOrCreateGlyphResult.Found;
			}
		}
		else
		{
			this.mDict = global::BMFont.CreateGlyphDictionary();
			this.mDictAny = true;
		}
		glyph = new global::BMGlyph
		{
			index = index
		};
		this.mDict.Add(index, glyph);
		return global::BMFont.GetOrCreateGlyphResult.Created;
	}

	// Token: 0x06004A28 RID: 18984 RVA: 0x0011DB54 File Offset: 0x0011BD54
	public bool GetGlyph(int index, out global::BMGlyph glyph)
	{
		if (!this.mDictMade)
		{
			this.mDictMade = true;
			int count = this.mSaved.Count;
			if (count == 0 && this.LegacyCheck())
			{
				count = this.mSaved.Count;
			}
			this.mDictAny = (count > 0);
			if (this.mDictAny)
			{
				this.mDict = global::BMFont.CreateGlyphDictionary(count);
				for (int i = count - 1; i >= 0; i--)
				{
					global::BMGlyph bmglyph = this.mSaved[i];
					this.mDict.Add(bmglyph.index, bmglyph);
					if (bmglyph.index == index)
					{
						glyph = bmglyph;
						while (--i >= 0)
						{
							bmglyph = this.mSaved[i];
							this.mDict.Add(bmglyph.index, bmglyph);
						}
						return true;
					}
				}
			}
		}
		else if (this.mDictAny)
		{
			return this.mDict.TryGetValue(index, out glyph);
		}
		glyph = null;
		return false;
	}

	// Token: 0x06004A29 RID: 18985 RVA: 0x0011DC54 File Offset: 0x0011BE54
	public bool ContainsGlyph(int index)
	{
		if (!this.mDictMade)
		{
			this.mDictMade = true;
			int count = this.mSaved.Count;
			if (count == 0 && this.LegacyCheck())
			{
				count = this.mSaved.Count;
			}
			this.mDictAny = (count > 0);
			if (this.mDictAny)
			{
				this.mDict = global::BMFont.CreateGlyphDictionary(count);
				for (int i = count - 1; i >= 0; i--)
				{
					global::BMGlyph bmglyph = this.mSaved[i];
					this.mDict.Add(bmglyph.index, bmglyph);
					if (bmglyph.index == index)
					{
						while (--i >= 0)
						{
							bmglyph = this.mSaved[i];
							this.mDict.Add(bmglyph.index, bmglyph);
						}
						return true;
					}
				}
			}
		}
		else if (this.mDictAny && this.mDict.ContainsKey(index))
		{
			return true;
		}
		return false;
	}

	// Token: 0x06004A2A RID: 18986 RVA: 0x0011DD54 File Offset: 0x0011BF54
	public global::BMSymbol GetSymbol(string sequence, bool createIfMissing)
	{
		int i = 0;
		int count = this.mSymbols.Count;
		while (i < count)
		{
			global::BMSymbol bmsymbol = this.mSymbols[i];
			if (bmsymbol.sequence == sequence)
			{
				return bmsymbol;
			}
			i++;
		}
		if (createIfMissing)
		{
			global::BMSymbol bmsymbol2 = new global::BMSymbol();
			bmsymbol2.sequence = sequence;
			this.mSymbols.Add(bmsymbol2);
			return bmsymbol2;
		}
		return null;
	}

	// Token: 0x06004A2B RID: 18987 RVA: 0x0011DDC4 File Offset: 0x0011BFC4
	public bool MatchSymbol(string text, int offset, int textLength, out global::BMSymbol symbol)
	{
		int count = this.mSymbols.Count;
		if (count > 0)
		{
			textLength -= offset;
			if (textLength > 0)
			{
				for (int i = 0; i < count; i++)
				{
					global::BMSymbol bmsymbol = this.mSymbols[i];
					int length = bmsymbol.sequence.Length;
					if (length != 0 && textLength >= length)
					{
						if (string.Compare(bmsymbol.sequence, 0, text, offset, length) == 0)
						{
							symbol = bmsymbol;
							if (length < textLength && ++i < count)
							{
								int num = length;
								do
								{
									bmsymbol = this.mSymbols[i];
									length = bmsymbol.sequence.Length;
									if (textLength >= length && length > num)
									{
										if (string.Compare(bmsymbol.sequence, 0, text, offset, length) == 0)
										{
											num = length;
											symbol = bmsymbol;
										}
									}
								}
								while (++i < count);
							}
							return true;
						}
					}
				}
			}
		}
		symbol = null;
		return false;
	}

	// Token: 0x06004A2C RID: 18988 RVA: 0x0011DEB4 File Offset: 0x0011C0B4
	public void Clear()
	{
		this.mGlyphs = null;
		this.mDict = null;
		this.mDictAny = (this.mDictMade = false);
		this.mSaved.Clear();
	}

	// Token: 0x06004A2D RID: 18989 RVA: 0x0011DEEC File Offset: 0x0011C0EC
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (this.isValid)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				global::BMGlyph bmglyph = this.mSaved[i];
				if (bmglyph != null)
				{
					bmglyph.Trim(xMin, yMin, xMax, yMax);
				}
				i++;
			}
		}
	}

	// Token: 0x04002861 RID: 10337
	[HideInInspector]
	[SerializeField]
	private global::BMGlyph[] mGlyphs;

	// Token: 0x04002862 RID: 10338
	[HideInInspector]
	[SerializeField]
	private int mSize;

	// Token: 0x04002863 RID: 10339
	[HideInInspector]
	[SerializeField]
	private int mBase;

	// Token: 0x04002864 RID: 10340
	[HideInInspector]
	[SerializeField]
	private int mWidth;

	// Token: 0x04002865 RID: 10341
	[HideInInspector]
	[SerializeField]
	private int mHeight;

	// Token: 0x04002866 RID: 10342
	[SerializeField]
	[HideInInspector]
	private string mSpriteName;

	// Token: 0x04002867 RID: 10343
	[HideInInspector]
	[SerializeField]
	private List<global::BMGlyph> mSaved = new List<global::BMGlyph>();

	// Token: 0x04002868 RID: 10344
	[HideInInspector]
	[SerializeField]
	private List<global::BMSymbol> mSymbols = new List<global::BMSymbol>();

	// Token: 0x04002869 RID: 10345
	[NonSerialized]
	private Dictionary<int, global::BMGlyph> mDict;

	// Token: 0x0400286A RID: 10346
	[NonSerialized]
	private bool mDictMade;

	// Token: 0x0400286B RID: 10347
	[NonSerialized]
	private bool mDictAny;

	// Token: 0x0200086D RID: 2157
	public enum GetOrCreateGlyphResult : sbyte
	{
		// Token: 0x0400286D RID: 10349
		Found = -1,
		// Token: 0x0400286E RID: 10350
		Failed,
		// Token: 0x0400286F RID: 10351
		Created
	}
}
