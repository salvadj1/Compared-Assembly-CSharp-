using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008AD RID: 2221
[AddComponentMenu("NGUI/UI/Atlas")]
public class UIAtlas : MonoBehaviour
{
	// Token: 0x17000E5A RID: 3674
	// (get) Token: 0x06004C00 RID: 19456 RVA: 0x00129344 File Offset: 0x00127544
	// (set) Token: 0x06004C01 RID: 19457 RVA: 0x00129370 File Offset: 0x00127570
	public Material spriteMaterial
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.material : this.mReplacement.spriteMaterial;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteMaterial = value;
			}
			else if (this.material == null)
			{
				this.material = value;
			}
			else
			{
				this.MarkAsDirty();
				this.material = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000E5B RID: 3675
	// (get) Token: 0x06004C02 RID: 19458 RVA: 0x001293D0 File Offset: 0x001275D0
	// (set) Token: 0x06004C03 RID: 19459 RVA: 0x001293FC File Offset: 0x001275FC
	public List<global::UIAtlas.Sprite> spriteList
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.sprites : this.mReplacement.spriteList;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteList = value;
			}
			else
			{
				this.sprites = value;
			}
		}
	}

	// Token: 0x17000E5C RID: 3676
	// (get) Token: 0x06004C04 RID: 19460 RVA: 0x00129428 File Offset: 0x00127628
	public Texture texture
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((!(this.material != null)) ? null : this.material.mainTexture) : this.mReplacement.texture;
		}
	}

	// Token: 0x17000E5D RID: 3677
	// (get) Token: 0x06004C05 RID: 19461 RVA: 0x00129478 File Offset: 0x00127678
	// (set) Token: 0x06004C06 RID: 19462 RVA: 0x001294A4 File Offset: 0x001276A4
	public global::UIAtlas.Coordinates coordinates
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mCoordinates : this.mReplacement.coordinates;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.coordinates = value;
			}
			else if (this.mCoordinates != value)
			{
				if (this.material == null || this.material.mainTexture == null)
				{
					Debug.LogError("Can't switch coordinates until the atlas material has a valid texture");
					return;
				}
				this.mCoordinates = value;
				Texture mainTexture = this.material.mainTexture;
				int i = 0;
				int count = this.sprites.Count;
				while (i < count)
				{
					global::UIAtlas.Sprite sprite = this.sprites[i];
					if (this.mCoordinates == global::UIAtlas.Coordinates.TexCoords)
					{
						sprite.outer = global::NGUIMath.ConvertToTexCoords(sprite.outer, mainTexture.width, mainTexture.height);
						sprite.inner = global::NGUIMath.ConvertToTexCoords(sprite.inner, mainTexture.width, mainTexture.height);
					}
					else
					{
						sprite.outer = global::NGUIMath.ConvertToPixels(sprite.outer, mainTexture.width, mainTexture.height, true);
						sprite.inner = global::NGUIMath.ConvertToPixels(sprite.inner, mainTexture.width, mainTexture.height, true);
					}
					i++;
				}
			}
		}
	}

	// Token: 0x17000E5E RID: 3678
	// (get) Token: 0x06004C07 RID: 19463 RVA: 0x001295D8 File Offset: 0x001277D8
	// (set) Token: 0x06004C08 RID: 19464 RVA: 0x00129604 File Offset: 0x00127804
	public float pixelSize
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mPixelSize : this.mReplacement.pixelSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.pixelSize = value;
			}
			else
			{
				float num = Mathf.Clamp(value, 0.25f, 4f);
				if (this.mPixelSize != num)
				{
					this.mPixelSize = num;
					this.MarkAsDirty();
				}
			}
		}
	}

	// Token: 0x17000E5F RID: 3679
	// (get) Token: 0x06004C09 RID: 19465 RVA: 0x00129660 File Offset: 0x00127860
	// (set) Token: 0x06004C0A RID: 19466 RVA: 0x00129668 File Offset: 0x00127868
	public global::UIAtlas replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			global::UIAtlas uiatlas = value;
			if (uiatlas == this)
			{
				uiatlas = null;
			}
			if (this.mReplacement != uiatlas)
			{
				if (uiatlas != null && uiatlas.replacement == this)
				{
					uiatlas.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsDirty();
				}
				this.mReplacement = uiatlas;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x06004C0B RID: 19467 RVA: 0x001296E0 File Offset: 0x001278E0
	public global::UIAtlas.Sprite GetSprite(string name)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetSprite(name);
		}
		if (!string.IsNullOrEmpty(name))
		{
			int i = 0;
			int count = this.sprites.Count;
			while (i < count)
			{
				global::UIAtlas.Sprite sprite = this.sprites[i];
				if (!string.IsNullOrEmpty(sprite.name) && name == sprite.name)
				{
					return sprite;
				}
				i++;
			}
		}
		else
		{
			Debug.LogWarning("Expected a valid name, found nothing");
		}
		return null;
	}

	// Token: 0x06004C0C RID: 19468 RVA: 0x00129774 File Offset: 0x00127974
	public List<string> GetListOfSprites()
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetListOfSprites();
		}
		List<string> list = new List<string>();
		int i = 0;
		int count = this.sprites.Count;
		while (i < count)
		{
			global::UIAtlas.Sprite sprite = this.sprites[i];
			if (sprite != null && !string.IsNullOrEmpty(sprite.name))
			{
				list.Add(sprite.name);
			}
			i++;
		}
		list.Sort();
		return list;
	}

	// Token: 0x06004C0D RID: 19469 RVA: 0x001297F8 File Offset: 0x001279F8
	private bool References(global::UIAtlas atlas)
	{
		return !(atlas == null) && (atlas == this || (this.mReplacement != null && this.mReplacement.References(atlas)));
	}

	// Token: 0x06004C0E RID: 19470 RVA: 0x00129844 File Offset: 0x00127A44
	public static bool CheckIfRelated(global::UIAtlas a, global::UIAtlas b)
	{
		return !(a == null) && !(b == null) && (a == b || a.References(b) || b.References(a));
	}

	// Token: 0x06004C0F RID: 19471 RVA: 0x00129890 File Offset: 0x00127A90
	public void MarkAsDirty()
	{
		global::UISprite[] array = global::NGUITools.FindActive<global::UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			global::UISprite uisprite = array[i];
			if (global::UIAtlas.CheckIfRelated(this, uisprite.atlas))
			{
				global::UIAtlas atlas = uisprite.atlas;
				uisprite.atlas = null;
				uisprite.atlas = atlas;
			}
			i++;
		}
		global::UIFont[] array2 = global::Resources.FindObjectsOfTypeAll(typeof(global::UIFont)) as global::UIFont[];
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			global::UIFont uifont = array2[j];
			if (global::UIAtlas.CheckIfRelated(this, uifont.atlas))
			{
				global::UIAtlas atlas2 = uifont.atlas;
				uifont.atlas = null;
				uifont.atlas = atlas2;
			}
			j++;
		}
		global::UILabel[] array3 = global::NGUITools.FindActive<global::UILabel>();
		int k = 0;
		int num3 = array3.Length;
		while (k < num3)
		{
			global::UILabel uilabel = array3[k];
			if (uilabel.font != null && global::UIAtlas.CheckIfRelated(this, uilabel.font.atlas))
			{
				global::UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			k++;
		}
	}

	// Token: 0x040029B7 RID: 10679
	[HideInInspector]
	[SerializeField]
	private Material material;

	// Token: 0x040029B8 RID: 10680
	[SerializeField]
	[HideInInspector]
	private List<global::UIAtlas.Sprite> sprites = new List<global::UIAtlas.Sprite>();

	// Token: 0x040029B9 RID: 10681
	[SerializeField]
	[HideInInspector]
	private global::UIAtlas.Coordinates mCoordinates;

	// Token: 0x040029BA RID: 10682
	[HideInInspector]
	[SerializeField]
	private float mPixelSize = 1f;

	// Token: 0x040029BB RID: 10683
	[HideInInspector]
	[SerializeField]
	private global::UIAtlas mReplacement;

	// Token: 0x020008AE RID: 2222
	[Serializable]
	public class Sprite
	{
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06004C11 RID: 19473 RVA: 0x00129A18 File Offset: 0x00127C18
		public bool hasPadding
		{
			get
			{
				return this.paddingLeft != 0f || this.paddingRight != 0f || this.paddingTop != 0f || this.paddingBottom != 0f;
			}
		}

		// Token: 0x040029BC RID: 10684
		public string name = "Unity Bug";

		// Token: 0x040029BD RID: 10685
		public Rect outer = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040029BE RID: 10686
		public Rect inner = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040029BF RID: 10687
		public float paddingLeft;

		// Token: 0x040029C0 RID: 10688
		public float paddingRight;

		// Token: 0x040029C1 RID: 10689
		public float paddingTop;

		// Token: 0x040029C2 RID: 10690
		public float paddingBottom;
	}

	// Token: 0x020008AF RID: 2223
	public enum Coordinates
	{
		// Token: 0x040029C4 RID: 10692
		Pixels,
		// Token: 0x040029C5 RID: 10693
		TexCoords
	}
}
