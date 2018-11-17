using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020007AD RID: 1965
public class dfLanguageManager : MonoBehaviour
{
	// Token: 0x17000CB8 RID: 3256
	// (get) Token: 0x06004259 RID: 16985 RVA: 0x000F47A4 File Offset: 0x000F29A4
	public global::dfLanguageCode CurrentLanguage
	{
		get
		{
			return this.currentLanguage;
		}
	}

	// Token: 0x17000CB9 RID: 3257
	// (get) Token: 0x0600425A RID: 16986 RVA: 0x000F47AC File Offset: 0x000F29AC
	// (set) Token: 0x0600425B RID: 16987 RVA: 0x000F47B4 File Offset: 0x000F29B4
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

	// Token: 0x0600425C RID: 16988 RVA: 0x000F47E8 File Offset: 0x000F29E8
	public void Start()
	{
		global::dfLanguageCode language = this.currentLanguage;
		if (this.currentLanguage == global::dfLanguageCode.None)
		{
			language = this.SystemLanguageToLanguageCode(Application.systemLanguage);
		}
		this.LoadLanguage(language);
	}

	// Token: 0x0600425D RID: 16989 RVA: 0x000F481C File Offset: 0x000F2A1C
	public void LoadLanguage(global::dfLanguageCode language)
	{
		this.currentLanguage = language;
		this.strings.Clear();
		if (this.dataFile != null)
		{
			this.parseDataFile();
		}
		global::dfControl[] componentsInChildren = base.GetComponentsInChildren<global::dfControl>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Localize();
		}
	}

	// Token: 0x0600425E RID: 16990 RVA: 0x000F4878 File Offset: 0x000F2A78
	public string GetValue(string key)
	{
		string empty = string.Empty;
		if (this.strings.TryGetValue(key, out empty))
		{
			return empty;
		}
		return key;
	}

	// Token: 0x0600425F RID: 16991 RVA: 0x000F48A4 File Offset: 0x000F2AA4
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

	// Token: 0x06004260 RID: 16992 RVA: 0x000F4970 File Offset: 0x000F2B70
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

	// Token: 0x06004261 RID: 16993 RVA: 0x000F4A78 File Offset: 0x000F2C78
	private global::dfLanguageCode SystemLanguageToLanguageCode(SystemLanguage language)
	{
		switch (language)
		{
		case 0:
			return global::dfLanguageCode.AF;
		case 1:
			return global::dfLanguageCode.AR;
		case 2:
			return global::dfLanguageCode.EU;
		case 3:
			return global::dfLanguageCode.BE;
		case 4:
			return global::dfLanguageCode.BG;
		case 5:
			return global::dfLanguageCode.CA;
		case 6:
			return global::dfLanguageCode.ZH;
		case 7:
			return global::dfLanguageCode.CS;
		case 8:
			return global::dfLanguageCode.DA;
		case 9:
			return global::dfLanguageCode.NL;
		case 10:
			return global::dfLanguageCode.EN;
		case 11:
			return global::dfLanguageCode.ES;
		case 12:
			return global::dfLanguageCode.FO;
		case 13:
			return global::dfLanguageCode.FI;
		case 14:
			return global::dfLanguageCode.FR;
		case 15:
			return global::dfLanguageCode.DE;
		case 16:
			return global::dfLanguageCode.EL;
		case 17:
			return global::dfLanguageCode.HE;
		case 18:
			return global::dfLanguageCode.HU;
		case 19:
			return global::dfLanguageCode.IS;
		case 20:
			return global::dfLanguageCode.ID;
		case 21:
			return global::dfLanguageCode.IT;
		case 22:
			return global::dfLanguageCode.JA;
		case 23:
			return global::dfLanguageCode.KO;
		case 24:
			return global::dfLanguageCode.LV;
		case 25:
			return global::dfLanguageCode.LT;
		case 26:
			return global::dfLanguageCode.NO;
		case 27:
			return global::dfLanguageCode.PL;
		case 28:
			return global::dfLanguageCode.PT;
		case 29:
			return global::dfLanguageCode.RO;
		case 30:
			return global::dfLanguageCode.RU;
		case 31:
			return global::dfLanguageCode.SH;
		case 32:
			return global::dfLanguageCode.SK;
		case 33:
			return global::dfLanguageCode.SL;
		case 34:
			return global::dfLanguageCode.ES;
		case 35:
			return global::dfLanguageCode.SV;
		case 36:
			return global::dfLanguageCode.TH;
		case 37:
			return global::dfLanguageCode.TR;
		case 38:
			return global::dfLanguageCode.UK;
		case 39:
			return global::dfLanguageCode.VI;
		case 40:
			return global::dfLanguageCode.EN;
		default:
			throw new ArgumentException("Unknown system language: " + language);
		}
	}

	// Token: 0x0400237A RID: 9082
	[SerializeField]
	private global::dfLanguageCode currentLanguage;

	// Token: 0x0400237B RID: 9083
	[SerializeField]
	private TextAsset dataFile;

	// Token: 0x0400237C RID: 9084
	private Dictionary<string, string> strings = new Dictionary<string, string>();
}
