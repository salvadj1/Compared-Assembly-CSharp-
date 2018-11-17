using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000802 RID: 2050
[AddComponentMenu("NGUI/UI/Sprite Animation")]
[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x17000E73 RID: 3699
	// (get) Token: 0x060049B1 RID: 18865 RVA: 0x001395C8 File Offset: 0x001377C8
	// (set) Token: 0x060049B2 RID: 18866 RVA: 0x001395D0 File Offset: 0x001377D0
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x17000E74 RID: 3700
	// (get) Token: 0x060049B3 RID: 18867 RVA: 0x001395DC File Offset: 0x001377DC
	// (set) Token: 0x060049B4 RID: 18868 RVA: 0x001395E4 File Offset: 0x001377E4
	public string namePrefix
	{
		get
		{
			return this.mPrefix;
		}
		set
		{
			if (this.mPrefix != value)
			{
				this.mPrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x060049B5 RID: 18869 RVA: 0x00139604 File Offset: 0x00137804
	private void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x060049B6 RID: 18870 RVA: 0x0013960C File Offset: 0x0013780C
	private void Update()
	{
		if (this.mSpriteNames.Count > 1 && Application.isPlaying)
		{
			this.mDelta += Time.deltaTime;
			float num = ((float)this.mFPS <= 0f) ? 0f : (1f / (float)this.mFPS);
			if (num < this.mDelta)
			{
				this.mDelta = ((num <= 0f) ? 0f : (this.mDelta - num));
				if (++this.mIndex >= this.mSpriteNames.Count)
				{
					this.mIndex = 0;
				}
				this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x060049B7 RID: 18871 RVA: 0x001396F0 File Offset: 0x001378F0
	private void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<UISprite>();
		}
		this.mSpriteNames.Clear();
		if (this.mSprite != null && this.mSprite.atlas != null)
		{
			List<UIAtlas.Sprite> spriteList = this.mSprite.atlas.spriteList;
			int i = 0;
			int count = spriteList.Count;
			while (i < count)
			{
				UIAtlas.Sprite sprite = spriteList[i];
				if (string.IsNullOrEmpty(this.mPrefix) || sprite.name.StartsWith(this.mPrefix))
				{
					this.mSpriteNames.Add(sprite.name);
				}
				i++;
			}
			this.mSpriteNames.Sort();
		}
	}

	// Token: 0x040029A0 RID: 10656
	[SerializeField]
	[HideInInspector]
	private int mFPS = 30;

	// Token: 0x040029A1 RID: 10657
	[HideInInspector]
	[SerializeField]
	private string mPrefix = string.Empty;

	// Token: 0x040029A2 RID: 10658
	private UISprite mSprite;

	// Token: 0x040029A3 RID: 10659
	private float mDelta;

	// Token: 0x040029A4 RID: 10660
	private int mIndex;

	// Token: 0x040029A5 RID: 10661
	private List<string> mSpriteNames = new List<string>();
}
