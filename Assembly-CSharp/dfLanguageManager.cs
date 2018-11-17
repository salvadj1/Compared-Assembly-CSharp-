using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020006DB RID: 1755
public class dfLanguageManager : MonoBehaviour
{
	// Token: 0x17000C34 RID: 3124
	// (get) Token: 0x06003E3D RID: 15933 RVA: 0x000EBBA0 File Offset: 0x000E9DA0
	public dfLanguageCode CurrentLanguage
	{
		get
		{
			return this.currentLanguage;
		}
	}

	// Token: 0x17000C35 RID: 3125
	// (get) Token: 0x06003E3E RID: 15934 RVA: 0x000EBBA8 File Offset: 0x000E9DA8
	// (set) Token: 0x06003E3F RID: 15935 RVA: 0x000EBBB0 File Offset: 0x000E9DB0
	public TextAsset DataFile
	{
		get
		{
			return this.dataFile;
		}
		set
		{
			if (value != this.dataFile)
			{
				this.dataFile = value;
				this.LoadLanguage(this.currentLanguage);
			}
		}
	}

	// Token: 0x06003E40 RID: 15936 RVA: 0x000EBBE4 File Offset: 0x000E9DE4
	public void Start()
	{
		dfLanguageCode language = this.currentLanguage;
		if (this.currentLanguage == dfLanguageCode.None)
		{
			language = this.SystemLanguageToLanguageCode(Application.systemLanguage);
		}
		this.LoadLanguage(language);
	}

	// Token: 0x06003E41 RID: 15937 RVA: 0x000EBC18 File Offset: 0x000E9E18
	public void LoadLanguage(dfLanguageCode language)
	{
		this.currentLanguage = language;
		this.strings.Clear();
		if (this.dataFile != null)
		{
			this.parseDataFile();
		}
		dfControl[] componentsInChildren = base.GetComponentsInChildren<dfControl>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Localize();
		}
	}

	// Token: 0x06003E42 RID: 15938 RVA: 0x000EBC74 File Offset: 0x000E9E74
	public string GetValue(string key)
	{
		string empty = string.Empty;
		if (this.strings.TryGetValue(key, out empty))
		{
			return empty;
		}
		return key;
	}

	// Token: 0x06003E43 RID: 15939 RVA: 0x000EBCA0 File Offset: 0x000E9EA0
	private void parseDataFile()
	{
		string text = this.dataFile.text.Replace("\r\n", "\n").Trim();
		List<string> list = new List<string>();
		int i = this.parseLine(text, list, 0);
		int num = list.IndexOf(this.currentLanguage.ToString());
		if (num < 0)
		{
			return;
		}
		List<string> list2 = new List<string>();
		while (i < text.Length)
		{
			i = this.parseLine(text, list2, i);
			if (list2.Count != 0)
			{
				string key = list2[0];
				string value = (num >= list2.Count) ? string.Empty : list2[num];
				this.strings[key] = value;
			}
		}
	}

	// Token: 0x06003E44 RID: 15940 RVA: 0x000EBD6C File Offset: 0x000E9F6C
	private int parseLine(string data, List<string> values, int index)
	{
		values.Clear();
		bool flag = false;
		StringBuilder stringBuilder = new StringBuilder(256);
		while (index < data.Length)
		{
			char c = data[index];
			if (c == '"')
			{
				if (!flag)
				{
					flag = true;
				}
				else if (index + 1 < data.Length && data[index + 1] == c)
				{
					index++;
					stringBuilder.Append(c);
				}
				else
				{
					flag = false;
				}
			}
			else if (c == ',')
			{
				if (flag)
				{
					stringBuilder.Append(c);
				}
				else
				{
					values.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
			}
			else if (c == '\n')
			{
				if (!flag)
				{
					index++;
					break;
				}
				stringBuilder.Append(c);
			}
			else
			{
				stringBuilder.Append(c);
			}
			index++;
		}
		if (stringBuilder.Length > 0)
		{
			values.Add(stringBuilder.ToString());
		}
		return index;
	}

	// Token: 0x06003E45 RID: 15941 RVA: 0x000EBE74 File Offset: 0x000EA074
	private dfLanguageCode SystemLanguageToLanguageCode(SystemLanguage language)
	{
		switch (language)
		{
		case 0:
			return dfLanguageCode.AF;
		case 1:
			return dfLanguageCode.AR;
		case 2:
			return dfLanguageCode.EU;
		case 3:
			return dfLanguageCode.BE;
		case 4:
			return dfLanguageCode.BG;
		case 5:
			return dfLanguageCode.CA;
		case 6:
			return dfLanguageCode.ZH;
		case 7:
			return dfLanguageCode.CS;
		case 8:
			return dfLanguageCode.DA;
		case 9:
			return dfLanguageCode.NL;
		case 10:
			return dfLanguageCode.EN;
		case 11:
			return dfLanguageCode.ES;
		case 12:
			return dfLanguageCode.FO;
		case 13:
			return dfLanguageCode.FI;
		case 14:
			return dfLanguageCode.FR;
		case 15:
			return dfLanguageCode.DE;
		case 16:
			return dfLanguageCode.EL;
		case 17:
			return dfLanguageCode.HE;
		case 18:
			return dfLanguageCode.HU;
		case 19:
			return dfLanguageCode.IS;
		case 20:
			return dfLanguageCode.ID;
		case 21:
			return dfLanguageCode.IT;
		case 22:
			return dfLanguageCode.JA;
		case 23:
			return dfLanguageCode.KO;
		case 24:
			return dfLanguageCode.LV;
		case 25:
			return dfLanguageCode.LT;
		case 26:
			return dfLanguageCode.NO;
		case 27:
			return dfLanguageCode.PL;
		case 28:
			return dfLanguageCode.PT;
		case 29:
			return dfLanguageCode.RO;
		case 30:
			return dfLanguageCode.RU;
		case 31:
			return dfLanguageCode.SH;
		case 32:
			return dfLanguageCode.SK;
		case 33:
			return dfLanguageCode.SL;
		case 34:
			return dfLanguageCode.ES;
		case 35:
			return dfLanguageCode.SV;
		case 36:
			return dfLanguageCode.TH;
		case 37:
			return dfLanguageCode.TR;
		case 38:
			return dfLanguageCode.UK;
		case 39:
			return dfLanguageCode.VI;
		case 40:
			return dfLanguageCode.EN;
		default:
			throw new ArgumentException("Unknown system language: " + language);
		}
	}

	// Token: 0x04002171 RID: 8561
	[SerializeField]
	private dfLanguageCode currentLanguage;

	// Token: 0x04002172 RID: 8562
	[SerializeField]
	private TextAsset dataFile;

	// Token: 0x04002173 RID: 8563
	private Dictionary<string, string> strings = new Dictionary<string, string>();
}
