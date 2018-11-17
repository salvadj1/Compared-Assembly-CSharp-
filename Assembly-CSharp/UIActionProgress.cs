using System;
using UnityEngine;

// Token: 0x02000427 RID: 1063
public class UIActionProgress : MonoBehaviour
{
	// Token: 0x17000909 RID: 2313
	// (get) Token: 0x0600276F RID: 10095 RVA: 0x00099C60 File Offset: 0x00097E60
	public UILabel label
	{
		get
		{
			return this._label;
		}
	}

	// Token: 0x1700090A RID: 2314
	// (get) Token: 0x06002770 RID: 10096 RVA: 0x00099C68 File Offset: 0x00097E68
	public UISlider slider
	{
		get
		{
			return this._slider;
		}
	}

	// Token: 0x1700090B RID: 2315
	// (get) Token: 0x06002771 RID: 10097 RVA: 0x00099C70 File Offset: 0x00097E70
	// (set) Token: 0x06002772 RID: 10098 RVA: 0x00099C80 File Offset: 0x00097E80
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

	// Token: 0x1700090C RID: 2316
	// (get) Token: 0x06002773 RID: 10099 RVA: 0x00099C90 File Offset: 0x00097E90
	// (set) Token: 0x06002774 RID: 10100 RVA: 0x00099CA0 File Offset: 0x00097EA0
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

	// Token: 0x06002775 RID: 10101 RVA: 0x00099CB0 File Offset: 0x00097EB0
	private void Awake()
	{
		this.sliderSprites = this._slider.GetComponentsInChildren<UISprite>();
	}

	// Token: 0x06002776 RID: 10102 RVA: 0x00099CC4 File Offset: 0x00097EC4
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
			foreach (UISprite uisprite in this.sliderSprites)
			{
				if (uisprite)
				{
					uisprite.enabled = yes;
				}
			}
		}
	}

	// Token: 0x06002777 RID: 10103 RVA: 0x00099D48 File Offset: 0x00097F48
	private void OnEnable()
	{
		this.SetEnabled(true);
	}

	// Token: 0x06002778 RID: 10104 RVA: 0x00099D54 File Offset: 0x00097F54
	private void OnDisable()
	{
		this.SetEnabled(false);
	}

	// Token: 0x0400136D RID: 4973
	[SerializeField]
	private UILabel _label;

	// Token: 0x0400136E RID: 4974
	[SerializeField]
	private UISlider _slider;

	// Token: 0x0400136F RID: 4975
	private UISprite[] sliderSprites;
}
