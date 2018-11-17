using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007C0 RID: 1984
[AddComponentMenu("NGUI/UI/Atlas")]
public class UIAtlas : MonoBehaviour
{
	// Token: 0x17000DC8 RID: 3528
	// (get) Token: 0x06004771 RID: 18289 RVA: 0x0011F920 File Offset: 0x0011DB20
	// (set) Token: 0x06004772 RID: 18290 RVA: 0x0011F94C File Offset: 0x0011DB4C
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

	// Token: 0x17000DC9 RID: 3529
	// (get) Token: 0x06004773 RID: 18291 RVA: 0x0011F9AC File Offset: 0x0011DBAC
	// (set) Token: 0x06004774 RID: 18292 RVA: 0x0011F9D8 File Offset: 0x0011DBD8
	public List<UIAtlas.Sprite> spriteList
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

	// Token: 0x17000DCA RID: 3530
	// (get) Token: 0x06004775 RID: 18293 RVA: 0x0011FA04 File Offset: 0x0011DC04
	public Texture texture
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((!(this.material != null)) ? null : this.material.mainTexture) : this.mReplacement.texture;
		}
	}

	// Token: 0x17000DCB RID: 3531
	// (get) Token: 0x06004776 RID: 18294 RVA: 0x0011FA54 File Offset: 0x0011DC54
	// (set) Token: 0x06004777 RID: 18295 RVA: 0x0011FA80 File Offset: 0x0011DC80
	public UIAtlas.Coordinates coordinates
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
					UIAtlas.Sprite sprite = this.sprites[i];
					if (this.mCoordinates == UIAtlas.Coordinates.TexCoords)
					{
						sprite.outer = NGUIMath.ConvertToTexCoords(sprite.outer, mainTexture.width, mainTexture.height);
						sprite.inner = NGUIMath.ConvertToTexCoords(sprite.inner, mainTexture.width, mainTexture.height);
					}
					else
					{
						sprite.outer = NGUIMath.ConvertToPixels(sprite.outer, mainTexture.width, mainTexture.height, true);
						sprite.inner = NGUIMath.ConvertToPixels(sprite.inner, mainTexture.width, mainTexture.height, true);
					}
					i++;
				}
			}
		}
	}

	// Token: 0x17000DCC RID: 3532
	// (get) Token: 0x06004778 RID: 18296 RVA: 0x0011FBB4 File Offset: 0x0011DDB4
	// (set) Token: 0x06004779 RID: 18297 RVA: 0x0011FBE0 File Offset: 0x0011DDE0
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

	// Token: 0x17000DCD RID: 3533
	// (get) Token: 0x0600477A RID: 18298 RVA: 0x0011FC3C File Offset: 0x0011DE3C
	// (set) Token: 0x0600477B RID: 18299 RVA: 0x0011FC44 File Offset: 0x0011DE44
	public UIAtlas replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIAtlas uiatlas = value;
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

	// Token: 0x0600477C RID: 18300 RVA: 0x0011FCBC File Offset: 0x0011DEBC
	public UIAtlas.Sprite GetSprite(string name)
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
				UIAtlas.Sprite sprite = this.sprites[i];
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

	// Token: 0x0600477D RID: 18301 RVA: 0x0011FD50 File Offset: 0x0011DF50
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
			UIAtlas.Sprite sprite = this.sprites[i];
			if (sprite != null && !string.IsNullOrEmpty(sprite.name))
			{
				list.Add(sprite.name);
			}
			i++;
		}
		list.Sort();
		return list;
	}

	// Token: 0x0600477E RID: 18302 RVA: 0x0011FDD4 File Offset: 0x0011DFD4
	private bool References(UIAtlas atlas)
	{
		return !(atlas == null) && (atlas == this || (this.mReplacement != null && this.mReplacement.References(atlas)));
	}

	// Token: 0x0600477F RID: 18303 RVA: 0x0011FE20 File Offset: 0x0011E020
	public static bool CheckIfRelated(UIAtlas a, UIAtlas b)
	{
		return !(a == null) && !(b == null) && (a == b || a.References(b) || b.References(a));
	}

	// Token: 0x06004780 RID: 18304 RVA: 0x0011FE6C File Offset: 0x0011E06C
	public void MarkAsDirty()
	{
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UISprite uisprite = array[i];
			if (UIAtlas.CheckIfRelated(this, uisprite.atlas))
			{
				UIAtlas atlas = uisprite.atlas;
				uisprite.atlas = null;
				uisprite.atlas = atlas;
			}
			i++;
		}
		UIFont[] array2 = Resources.FindObjectsOfTypeAll(typeof(UIFont)) as UIFont[];
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			UIFont uifont = array2[j];
			if (UIAtlas.CheckIfRelated(this, uifont.atlas))
			{
				UIAtlas atlas2 = uifont.atlas;
				uifont.atlas = null;
				uifont.atlas = atlas2;
			}
			j++;
		}
		UILabel[] array3 = NGUITools.FindActive<UILabel>();
		int k = 0;
		int num3 = array3.Length;
		while (k < num3)
		{
			UILabel uilabel = array3[k];
			if (uilabel.font != null && UIAtlas.CheckIfRelated(this, uilabel.font.atlas))
			{
				UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			k++;
		}
	}

	// Token: 0x0400277D RID: 10109
	[SerializeField]
	[HideInInspector]
	private Material material;

	// Token: 0x0400277E RID: 10110
	[SerializeField]
	[HideInInspector]
	private List<UIAtlas.Sprite> sprites = new List<UIAtlas.Sprite>();

	// Token: 0x0400277F RID: 10111
	[SerializeField]
	[HideInInspector]
	private UIAtlas.Coordinates mCoordinates;

	// Token: 0x04002780 RID: 10112
	[SerializeField]
	[HideInInspector]
	private float mPixelSize = 1f;

	// Token: 0x04002781 RID: 10113
	[HideInInspector]
	[SerializeField]
	private UIAtlas mReplacement;

	// Token: 0x020007C1 RID: 1985
	[Serializable]
	public class Sprite
	{
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06004782 RID: 18306 RVA: 0x0011FFF4 File Offset: 0x0011E1F4
		public bool hasPadding
		{
			get
			{
				return this.paddingLeft != 0f || this.paddingRight != 0f || this.paddingTop != 0f || this.paddingBottom != 0f;
			}
		}

		// Token: 0x04002782 RID: 10114
		public string name = "Unity Bug";

		// Token: 0x04002783 RID: 10115
		public Rect outer = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04002784 RID: 10116
		public Rect inner = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04002785 RID: 10117
		public float paddingLeft;

		// Token: 0x04002786 RID: 10118
		public float paddingRight;

		// Token: 0x04002787 RID: 10119
		public float paddingTop;

		// Token: 0x04002788 RID: 10120
		public float paddingBottom;
	}

	// Token: 0x020007C2 RID: 1986
	public enum Coordinates
	{
		// Token: 0x0400278A RID: 10122
		Pixels,
		// Token: 0x0400278B RID: 10123
		TexCoords
	}
}
