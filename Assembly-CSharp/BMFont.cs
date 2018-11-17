using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000787 RID: 1927
[Serializable]
public class BMFont
{
	// Token: 0x17000D88 RID: 3464
	// (get) Token: 0x060045A9 RID: 17833 RVA: 0x00113EF4 File Offset: 0x001120F4
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0 || this.LegacyCheck();
		}
	}

	// Token: 0x17000D89 RID: 3465
	// (get) Token: 0x060045AA RID: 17834 RVA: 0x00113F10 File Offset: 0x00112110
	// (set) Token: 0x060045AB RID: 17835 RVA: 0x00113F18 File Offset: 0x00112118
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

	// Token: 0x17000D8A RID: 3466
	// (get) Token: 0x060045AC RID: 17836 RVA: 0x00113F24 File Offset: 0x00112124
	// (set) Token: 0x060045AD RID: 17837 RVA: 0x00113F2C File Offset: 0x0011212C
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

	// Token: 0x17000D8B RID: 3467
	// (get) Token: 0x060045AE RID: 17838 RVA: 0x00113F38 File Offset: 0x00112138
	// (set) Token: 0x060045AF RID: 17839 RVA: 0x00113F40 File Offset: 0x00112140
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

	// Token: 0x17000D8C RID: 3468
	// (get) Token: 0x060045B0 RID: 17840 RVA: 0x00113F4C File Offset: 0x0011214C
	// (set) Token: 0x060045B1 RID: 17841 RVA: 0x00113F54 File Offset: 0x00112154
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

	// Token: 0x17000D8D RID: 3469
	// (get) Token: 0x060045B2 RID: 17842 RVA: 0x00113F60 File Offset: 0x00112160
	public int glyphCount
	{
		get
		{
			return (!this.isValid) ? 0 : this.mSaved.Count;
		}
	}

	// Token: 0x17000D8E RID: 3470
	// (get) Token: 0x060045B3 RID: 17843 RVA: 0x00113F80 File Offset: 0x00112180
	// (set) Token: 0x060045B4 RID: 17844 RVA: 0x00113F88 File Offset: 0x00112188
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

	// Token: 0x17000D8F RID: 3471
	// (get) Token: 0x060045B5 RID: 17845 RVA: 0x00113F94 File Offset: 0x00112194
	public List<BMSymbol> symbols
	{
		get
		{
			return this.mSymbols;
		}
	}

	// Token: 0x060045B6 RID: 17846 RVA: 0x00113F9C File Offset: 0x0011219C
	public bool LegacyCheck()
	{
		if (this.mGlyphs != null && this.mGlyphs.Length > 0)
		{
			int i = 0;
			int num = this.mGlyphs.Length;
			while (i < num)
			{
				BMGlyph bmglyph = this.mGlyphs[i];
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

	// Token: 0x060045B7 RID: 17847 RVA: 0x0011403C File Offset: 0x0011223C
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

	// Token: 0x060045B8 RID: 17848 RVA: 0x00114080 File Offset: 0x00112280
	private static Dictionary<int, BMGlyph> CreateGlyphDictionary()
	{
		return new Dictionary<int, BMGlyph>();
	}

	// Token: 0x060045B9 RID: 17849 RVA: 0x00114088 File Offset: 0x00112288
	private static Dictionary<int, BMGlyph> CreateGlyphDictionary(int cap)
	{
		return new Dictionary<int, BMGlyph>(cap);
	}

	// Token: 0x060045BA RID: 17850 RVA: 0x00114090 File Offset: 0x00112290
	public BMFont.GetOrCreateGlyphResult GetOrCreateGlyph(int index, out BMGlyph glyph)
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
				this.mDict = BMFont.CreateGlyphDictionary(count + 1);
				for (int i = count - 1; i >= 0; i--)
				{
					BMGlyph bmglyph = this.mSaved[i];
					this.mDict.Add(bmglyph.index, bmglyph);
					if (bmglyph.index == index)
					{
						glyph = bmglyph;
						while (--i >= 0)
						{
							bmglyph = this.mSaved[i];
							this.mDict.Add(bmglyph.index, bmglyph);
						}
						return BMFont.GetOrCreateGlyphResult.Found;
					}
				}
			}
			else
			{
				this.mDict = BMFont.CreateGlyphDictionary();
			}
		}
		else if (this.mDictAny)
		{
			if (this.mDict.TryGetValue(index, out glyph))
			{
				return BMFont.GetOrCreateGlyphResult.Found;
			}
		}
		else
		{
			this.mDict = BMFont.CreateGlyphDictionary();
			this.mDictAny = true;
		}
		glyph = new BMGlyph
		{
			index = index
		};
		this.mDict.Add(index, glyph);
		return BMFont.GetOrCreateGlyphResult.Created;
	}

	// Token: 0x060045BB RID: 17851 RVA: 0x001141D4 File Offset: 0x001123D4
	public bool GetGlyph(int index, out BMGlyph glyph)
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
				this.mDict = BMFont.CreateGlyphDictionary(count);
				for (int i = count - 1; i >= 0; i--)
				{
					BMGlyph bmglyph = this.mSaved[i];
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

	// Token: 0x060045BC RID: 17852 RVA: 0x001142D4 File Offset: 0x001124D4
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
				this.mDict = BMFont.CreateGlyphDictionary(count);
				for (int i = count - 1; i >= 0; i--)
				{
					BMGlyph bmglyph = this.mSaved[i];
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

	// Token: 0x060045BD RID: 17853 RVA: 0x001143D4 File Offset: 0x001125D4
	public BMSymbol GetSymbol(string sequence, bool createIfMissing)
	{
		int i = 0;
		int count = this.mSymbols.Count;
		while (i < count)
		{
			BMSymbol bmsymbol = this.mSymbols[i];
			if (bmsymbol.sequence == sequence)
			{
				return bmsymbol;
			}
			i++;
		}
		if (createIfMissing)
		{
			BMSymbol bmsymbol2 = new BMSymbol();
			bmsymbol2.sequence = sequence;
			this.mSymbols.Add(bmsymbol2);
			return bmsymbol2;
		}
		return null;
	}

	// Token: 0x060045BE RID: 17854 RVA: 0x00114444 File Offset: 0x00112644
	public bool MatchSymbol(string text, int offset, int textLength, out BMSymbol symbol)
	{
		int count = this.mSymbols.Count;
		if (count > 0)
		{
			textLength -= offset;
			if (textLength > 0)
			{
				for (int i = 0; i < count; i++)
				{
					BMSymbol bmsymbol = this.mSymbols[i];
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

	// Token: 0x060045BF RID: 17855 RVA: 0x00114534 File Offset: 0x00112734
	public void Clear()
	{
		this.mGlyphs = null;
		this.mDict = null;
		this.mDictAny = (this.mDictMade = false);
		this.mSaved.Clear();
	}

	// Token: 0x060045C0 RID: 17856 RVA: 0x0011456C File Offset: 0x0011276C
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (this.isValid)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph = this.mSaved[i];
				if (bmglyph != null)
				{
					bmglyph.Trim(xMin, yMin, xMax, yMax);
				}
				i++;
			}
		}
	}

	// Token: 0x0400262A RID: 9770
	[SerializeField]
	[HideInInspector]
	private BMGlyph[] mGlyphs;

	// Token: 0x0400262B RID: 9771
	[HideInInspector]
	[SerializeField]
	private int mSize;

	// Token: 0x0400262C RID: 9772
	[SerializeField]
	[HideInInspector]
	private int mBase;

	// Token: 0x0400262D RID: 9773
	[SerializeField]
	[HideInInspector]
	private int mWidth;

	// Token: 0x0400262E RID: 9774
	[HideInInspector]
	[SerializeField]
	private int mHeight;

	// Token: 0x0400262F RID: 9775
	[SerializeField]
	[HideInInspector]
	private string mSpriteName;

	// Token: 0x04002630 RID: 9776
	[HideInInspector]
	[SerializeField]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x04002631 RID: 9777
	[SerializeField]
	[HideInInspector]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	// Token: 0x04002632 RID: 9778
	[NonSerialized]
	private Dictionary<int, BMGlyph> mDict;

	// Token: 0x04002633 RID: 9779
	[NonSerialized]
	private bool mDictMade;

	// Token: 0x04002634 RID: 9780
	[NonSerialized]
	private bool mDictAny;

	// Token: 0x02000788 RID: 1928
	public enum GetOrCreateGlyphResult : sbyte
	{
		// Token: 0x04002636 RID: 9782
		Found = -1,
		// Token: 0x04002637 RID: 9783
		Failed,
		// Token: 0x04002638 RID: 9784
		Created
	}
}
