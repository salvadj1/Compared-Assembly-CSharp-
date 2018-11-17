using System;
using UnityEngine;

// Token: 0x020004DD RID: 1245
public class UIActionProgress : MonoBehaviour
{
	// Token: 0x17000971 RID: 2417
	// (get) Token: 0x06002AFF RID: 11007 RVA: 0x0009FBE0 File Offset: 0x0009DDE0
	public global::UILabel label
	{
		get
		{
			return this._label;
		}
	}

	// Token: 0x17000972 RID: 2418
	// (get) Token: 0x06002B00 RID: 11008 RVA: 0x0009FBE8 File Offset: 0x0009DDE8
	public global::UISlider slider
	{
		get
		{
			return this._slider;
		}
	}

	// Token: 0x17000973 RID: 2419
	// (get) Token: 0x06002B01 RID: 11009 RVA: 0x0009FBF0 File Offset: 0x0009DDF0
	// (set) Token: 0x06002B02 RID: 11010 RVA: 0x0009FC00 File Offset: 0x0009DE00
	public string text
	{
		get
		{
			return this.label.text;
		}
		set
		{
			this.label.text = value;
		}
	}

	// Token: 0x17000974 RID: 2420
	// (get) Token: 0x06002B03 RID: 11011 RVA: 0x0009FC10 File Offset: 0x0009DE10
	// (set) Token: 0x06002B04 RID: 11012 RVA: 0x0009FC20 File Offset: 0x0009DE20
	public float progress
	{
		get
		{
			return this.slider.sliderValue;
		}
		set
		{
			this.slider.sliderValue = value;
		}
	}

	// Token: 0x06002B05 RID: 11013 RVA: 0x0009FC30 File Offset: 0x0009DE30
	private void Awake()
	{
		this.sliderSprites = this._slider.GetComponentsInChildren<global::UISprite>();
	}

	// Token: 0x06002B06 RID: 11014 RVA: 0x0009FC44 File Offset: 0x0009DE44
	private void SetEnabled(bool yes)
	{
		if (this._slider)
		{
			this._slider.enabled = yes;
		}
		if (this._label)
		{
			this._label.enabled = yes;
		}
		if (this.sliderSprites != null)
		{
			foreach (global::UISprite uisprite in this.sliderSprites)
			{
				if (uisprite)
				{
					uisprite.enabled = yes;
				}
			}
		}
	}

	// Token: 0x06002B07 RID: 11015 RVA: 0x0009FCC8 File Offset: 0x0009DEC8
	private void OnEnable()
	{
		this.SetEnabled(true);
	}

	// Token: 0x06002B08 RID: 11016 RVA: 0x0009FCD4 File Offset: 0x0009DED4
	private void OnDisable()
	{
		this.SetEnabled(false);
	}

	// Token: 0x040014F0 RID: 5360
	[SerializeField]
	private global::UILabel _label;

	// Token: 0x040014F1 RID: 5361
	[SerializeField]
	private global::UISlider _slider;

	// Token: 0x040014F2 RID: 5362
	private global::UISprite[] sliderSprites;
}
