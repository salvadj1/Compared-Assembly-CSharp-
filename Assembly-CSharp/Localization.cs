using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200078E RID: 1934
[AddComponentMenu("NGUI/Internal/Localization")]
public class Localization : MonoBehaviour
{
	// Token: 0x17000D92 RID: 3474
	// (get) Token: 0x060045D1 RID: 17873 RVA: 0x00114A10 File Offset: 0x00112C10
	public static Localization instance
	{
		get
		{
			if (Localization.mInst == null)
			{
				Localization.mInst = (Object.FindObjectOfType(typeof(Localization)) as Localization);
				if (Localization.mInst == null)
				{
					GameObject gameObject = new GameObject("_Localization");
					Object.DontDestroyOnLoad(gameObject);
					Localization.mInst = gameObject.AddComponent<Localization>();
				}
			}
			return Localization.mInst;
		}
	}

	// Token: 0x17000D93 RID: 3475
	// (get) Token: 0x060045D2 RID: 17874 RVA: 0x00114A78 File Offset: 0x00112C78
	// (set) Token: 0x060045D3 RID: 17875 RVA: 0x00114B04 File Offset: 0x00112D04
	public string currentLanguage
	{
		get
		{
			if (string.IsNullOrEmpty(this.mLanguage))
			{
				this.currentLanguage = PlayerPrefs.GetString("Language");
				if (string.IsNullOrEmpty(this.mLanguage))
				{
					this.currentLanguage = this.startingLanguage;
					if (string.IsNullOrEmpty(this.mLanguage) && this.languages != null && this.languages.Length > 0)
					{
						this.currentLanguage = this.languages[0].name;
					}
				}
			}
			return this.mLanguage;
		}
		set
		{
			if (this.mLanguage != value)
			{
				this.startingLanguage = value;
				if (!string.IsNullOrEmpty(value))
				{
					if (this.languages != null)
					{
						int i = 0;
						int num = this.languages.Length;
						while (i < num)
						{
							TextAsset textAsset = this.languages[i];
							if (textAsset != null && textAsset.name == value)
							{
								this.Load(textAsset);
								return;
							}
							i++;
						}
					}
					TextAsset textAsset2 = Resources.Load(value, typeof(TextAsset)) as TextAsset;
					if (textAsset2 != null)
					{
						this.Load(textAsset2);
						return;
					}
				}
				this.mDictionary.Clear();
				PlayerPrefs.DeleteKey("Language");
			}
		}
	}

	// Token: 0x060045D4 RID: 17876 RVA: 0x00114BC8 File Offset: 0x00112DC8
	private void Awake()
	{
		if (Localization.mInst == null)
		{
			Localization.mInst = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060045D5 RID: 17877 RVA: 0x00114BFC File Offset: 0x00112DFC
	private void Start()
	{
		if (!string.IsNullOrEmpty(this.startingLanguage))
		{
			this.currentLanguage = this.startingLanguage;
		}
	}

	// Token: 0x060045D6 RID: 17878 RVA: 0x00114C1C File Offset: 0x00112E1C
	private void OnEnable()
	{
		if (Localization.mInst == null)
		{
			Localization.mInst = this;
		}
	}

	// Token: 0x060045D7 RID: 17879 RVA: 0x00114C34 File Offset: 0x00112E34
	private void OnDestroy()
	{
		if (Localization.mInst == this)
		{
			Localization.mInst = null;
		}
	}

	// Token: 0x060045D8 RID: 17880 RVA: 0x00114C4C File Offset: 0x00112E4C
	private void Load(TextAsset asset)
	{
		this.mLanguage = asset.name;
		PlayerPrefs.SetString("Language", this.mLanguage);
		ByteReader byteReader = new ByteReader(asset);
		this.mDictionary = byteReader.ReadDictionary();
		UIRoot.Broadcast("OnLocalize", this);
	}

	// Token: 0x060045D9 RID: 17881 RVA: 0x00114C94 File Offset: 0x00112E94
	public string Get(string key)
	{
		string text;
		return (!this.mDictionary.TryGetValue(key, out text)) ? key : text;
	}

	// Token: 0x04002650 RID: 9808
	private static Localization mInst;

	// Token: 0x04002651 RID: 9809
	public string startingLanguage;

	// Token: 0x04002652 RID: 9810
	public TextAsset[] languages;

	// Token: 0x04002653 RID: 9811
	private Dictionary<string, string> mDictionary = new Dictionary<string, string>();

	// Token: 0x04002654 RID: 9812
	private string mLanguage;
}
