using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008F4 RID: 2292
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
[RequireComponent(typeof(global::UISprite))]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x17000F0D RID: 3853
	// (get) Token: 0x06004E60 RID: 20064 RVA: 0x0014352C File Offset: 0x0014172C
	// (set) Token: 0x06004E61 RID: 20065 RVA: 0x00143534 File Offset: 0x00141734
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

	// Token: 0x17000F0E RID: 3854
	// (get) Token: 0x06004E62 RID: 20066 RVA: 0x00143540 File Offset: 0x00141740
	// (set) Token: 0x06004E63 RID: 20067 RVA: 0x00143548 File Offset: 0x00141748
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

	// Token: 0x06004E64 RID: 20068 RVA: 0x00143568 File Offset: 0x00141768
	private void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x06004E65 RID: 20069 RVA: 0x00143570 File Offset: 0x00141770
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

	// Token: 0x06004E66 RID: 20070 RVA: 0x00143654 File Offset: 0x00141854
	private void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<global::UISprite>();
		}
		this.mSpriteNames.Clear();
		if (this.mSprite != null && this.mSprite.atlas != null)
		{
			List<global::UIAtlas.Sprite> spriteList = this.mSprite.atlas.spriteList;
			int i = 0;
			int count = spriteList.Count;
			while (i < count)
			{
				global::UIAtlas.Sprite sprite = spriteList[i];
				if (string.IsNullOrEmpty(this.mPrefix) || sprite.name.StartsWith(this.mPrefix))
				{
					this.mSpriteNames.Add(sprite.name);
				}
				i++;
			}
			this.mSpriteNames.Sort();
		}
	}

	// Token: 0x04002BEE RID: 11246
	[HideInInspector]
	[SerializeField]
	private int mFPS = 30;

	// Token: 0x04002BEF RID: 11247
	[HideInInspector]
	[SerializeField]
	private string mPrefix = string.Empty;

	// Token: 0x04002BF0 RID: 11248
	private global::UISprite mSprite;

	// Token: 0x04002BF1 RID: 11249
	private float mDelta;

	// Token: 0x04002BF2 RID: 11250
	private int mIndex;

	// Token: 0x04002BF3 RID: 11251
	private List<string> mSpriteNames = new List<string>();
}
