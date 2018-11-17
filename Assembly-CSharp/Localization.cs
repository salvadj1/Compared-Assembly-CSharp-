using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000873 RID: 2163
[AddComponentMenu("NGUI/Internal/Localization")]
public class Localization : MonoBehaviour
{
	// Token: 0x17000E22 RID: 3618
	// (get) Token: 0x06004A3E RID: 19006 RVA: 0x0011E390 File Offset: 0x0011C590
	public static global::Localization instance
	{
		get
		{
			if (global::Localization.mInst == null)
			{
				global::Localization.mInst = (Object.FindObjectOfType(typeof(global::Localization)) as global::Localization);
				if (global::Localization.mInst == null)
				{
					GameObject gameObject = new GameObject("_Localization");
					Object.DontDestroyOnLoad(gameObject);
					global::Localization.mInst = gameObject.AddComponent<global::Localization>();
				}
			}
			return global::Localization.mInst;
		}
	}

	// Token: 0x17000E23 RID: 3619
	// (get) Token: 0x06004A3F RID: 19007 RVA: 0x0011E3F8 File Offset: 0x0011C5F8
	// (set) Token: 0x06004A40 RID: 19008 RVA: 0x0011E484 File Offset: 0x0011C684
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
					TextAsset textAsset2 = UnityEngine.Resources.Load(value, typeof(TextAsset)) as TextAsset;
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

	// Token: 0x06004A41 RID: 19009 RVA: 0x0011E548 File Offset: 0x0011C748
	private void Awake()
	{
		if (global::Localization.mInst == null)
		{
			global::Localization.mInst = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06004A42 RID: 19010 RVA: 0x0011E57C File Offset: 0x0011C77C
	private void Start()
	{
		if (!string.IsNullOrEmpty(this.startingLanguage))
		{
			this.currentLanguage = this.startingLanguage;
		}
	}

	// Token: 0x06004A43 RID: 19011 RVA: 0x0011E59C File Offset: 0x0011C79C
	private void OnEnable()
	{
		if (global::Localization.mInst == null)
		{
			global::Localization.mInst = this;
		}
	}

	// Token: 0x06004A44 RID: 19012 RVA: 0x0011E5B4 File Offset: 0x0011C7B4
	private void OnDestroy()
	{
		if (global::Localization.mInst == this)
		{
			global::Localization.mInst = null;
		}
	}

	// Token: 0x06004A45 RID: 19013 RVA: 0x0011E5CC File Offset: 0x0011C7CC
	private void Load(TextAsset asset)
	{
		this.mLanguage = asset.name;
		PlayerPrefs.SetString("Language", this.mLanguage);
		global::ByteReader byteReader = new global::ByteReader(asset);
		this.mDictionary = byteReader.ReadDictionary();
		global::UIRoot.Broadcast("OnLocalize", this);
	}

	// Token: 0x06004A46 RID: 19014 RVA: 0x0011E614 File Offset: 0x0011C814
	public string Get(string key)
	{
		string text;
		return (!this.mDictionary.TryGetValue(key, out text)) ? key : text;
	}

	// Token: 0x04002887 RID: 10375
	private static global::Localization mInst;

	// Token: 0x04002888 RID: 10376
	public string startingLanguage;

	// Token: 0x04002889 RID: 10377
	public TextAsset[] languages;

	// Token: 0x0400288A RID: 10378
	private Dictionary<string, string> mDictionary = new Dictionary<string, string>();

	// Token: 0x0400288B RID: 10379
	private string mLanguage;
}
