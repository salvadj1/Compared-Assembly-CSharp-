using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x02000801 RID: 2049
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Sprite (Basic)")]
public class UISprite : UIWidget
{
	// Token: 0x0600499B RID: 18843 RVA: 0x00138D70 File Offset: 0x00136F70
	public UISprite() : base(UIWidget.WidgetFlags.CustomPivotOffset | UIWidget.WidgetFlags.CustomMaterialGet)
	{
	}

	// Token: 0x0600499C RID: 18844 RVA: 0x00138D84 File Offset: 0x00136F84
	protected UISprite(UIWidget.WidgetFlags additionalFlags) : base(UIWidget.WidgetFlags.CustomPivotOffset | UIWidget.WidgetFlags.CustomMaterialGet | additionalFlags)
	{
	}

	// Token: 0x17000E6A RID: 3690
	// (get) Token: 0x0600499D RID: 18845 RVA: 0x00138D9C File Offset: 0x00136F9C
	public Rect outerUV
	{
		get
		{
			this.UpdateUVs(false);
			return this.mOuterUV;
		}
	}

	// Token: 0x17000E6B RID: 3691
	// (get) Token: 0x0600499E RID: 18846 RVA: 0x00138DAC File Offset: 0x00136FAC
	// (set) Token: 0x0600499F RID: 18847 RVA: 0x00138DB4 File Offset: 0x00136FB4
	public UIAtlas atlas
	{
		get
		{
			return this.mAtlas;
		}
		set
		{
			if (this.mAtlas != value)
			{
				this.mAtlas = value;
				this.mSpriteSet = false;
				this.mSprite = null;
				this.material = ((!(this.mAtlas != null)) ? null : ((UIMaterial)this.mAtlas.spriteMaterial));
				if (string.IsNullOrEmpty(this.mSpriteName) && this.mAtlas != null && this.mAtlas.spriteList.Count > 0)
				{
					this.sprite = this.mAtlas.spriteList[0];
					this.mSpriteName = this.mSprite.name;
				}
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					string spriteName = this.mSpriteName;
					this.mSpriteName = string.Empty;
					this.spriteName = spriteName;
					base.ChangedAuto();
					this.UpdateUVs(true);
				}
			}
		}
	}

	// Token: 0x17000E6C RID: 3692
	// (get) Token: 0x060049A0 RID: 18848 RVA: 0x00138EAC File Offset: 0x001370AC
	// (set) Token: 0x060049A1 RID: 18849 RVA: 0x00138EB4 File Offset: 0x001370B4
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (string.IsNullOrEmpty(this.mSpriteName))
				{
					return;
				}
				this.mSpriteName = string.Empty;
				this.mSprite = null;
				base.ChangedAuto();
			}
			else if (this.mSpriteName != value)
			{
				this.mSpriteName = value;
				this.mSprite = null;
				base.ChangedAuto();
				if (this.mSprite != null)
				{
					this.UpdateUVs(true);
				}
			}
		}
	}

	// Token: 0x17000E6D RID: 3693
	// (get) Token: 0x060049A2 RID: 18850 RVA: 0x00138F34 File Offset: 0x00137134
	// (set) Token: 0x060049A3 RID: 18851 RVA: 0x00139008 File Offset: 0x00137208
	public UIAtlas.Sprite sprite
	{
		get
		{
			if (!this.mSpriteSet)
			{
				this.mSprite = null;
			}
			if (this.mSprite == null && this.mAtlas != null)
			{
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					this.sprite = this.mAtlas.GetSprite(this.mSpriteName);
				}
				if (this.mSprite == null && this.mAtlas.spriteList.Count > 0)
				{
					this.sprite = this.mAtlas.spriteList[0];
					this.mSpriteName = this.mSprite.name;
				}
				if (this.mSprite != null)
				{
					this.material = (UIMaterial)this.mAtlas.spriteMaterial;
				}
			}
			return this.mSprite;
		}
		set
		{
			this.mSprite = value;
			this.mSpriteSet = true;
			this.material = ((this.mSprite == null || !(this.mAtlas != null)) ? null : ((UIMaterial)this.mAtlas.spriteMaterial));
		}
	}

	// Token: 0x17000E6E RID: 3694
	// (get) Token: 0x060049A4 RID: 18852 RVA: 0x0013905C File Offset: 0x0013725C
	public new Vector2 pivotOffset
	{
		get
		{
			Vector2 zero = Vector2.zero;
			if (this.sprite != null)
			{
				UIWidget.Pivot pivot = base.pivot;
				if (pivot == UIWidget.Pivot.Top || pivot == UIWidget.Pivot.Center || pivot == UIWidget.Pivot.Bottom)
				{
					zero.x = (-1f - this.mSprite.paddingRight + this.mSprite.paddingLeft) * 0.5f;
				}
				else if (pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.BottomRight)
				{
					zero.x = -1f - this.mSprite.paddingRight;
				}
				else
				{
					zero.x = this.mSprite.paddingLeft;
				}
				if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.Center || pivot == UIWidget.Pivot.Right)
				{
					zero.y = (1f + this.mSprite.paddingBottom - this.mSprite.paddingTop) * 0.5f;
				}
				else if (pivot == UIWidget.Pivot.BottomLeft || pivot == UIWidget.Pivot.Bottom || pivot == UIWidget.Pivot.BottomRight)
				{
					zero.y = 1f + this.mSprite.paddingBottom;
				}
				else
				{
					zero.y = -this.mSprite.paddingTop;
				}
			}
			return zero;
		}
	}

	// Token: 0x17000E6F RID: 3695
	// (get) Token: 0x060049A5 RID: 18853 RVA: 0x00139194 File Offset: 0x00137394
	// (set) Token: 0x060049A6 RID: 18854 RVA: 0x00139200 File Offset: 0x00137400
	public new UIMaterial material
	{
		get
		{
			UIMaterial uimaterial = base.baseMaterial;
			if (uimaterial == null)
			{
				uimaterial = ((!(this.mAtlas != null)) ? null : ((UIMaterial)this.mAtlas.spriteMaterial));
				this.mSprite = null;
				base.baseMaterial = uimaterial;
				if (uimaterial != null)
				{
					this.UpdateUVs(true);
				}
			}
			return uimaterial;
		}
		set
		{
			base.material = value;
		}
	}

	// Token: 0x17000E70 RID: 3696
	// (get) Token: 0x060049A7 RID: 18855 RVA: 0x0013920C File Offset: 0x0013740C
	protected override UIMaterial customMaterial
	{
		get
		{
			return this.material;
		}
	}

	// Token: 0x17000E71 RID: 3697
	// (get) Token: 0x060049A8 RID: 18856 RVA: 0x00139214 File Offset: 0x00137414
	public Vector4 border
	{
		get
		{
			if ((byte)(this.widgetFlags & UIWidget.WidgetFlags.CustomBorder) == 16)
			{
				return this.customBorder;
			}
			return Vector4.zero;
		}
	}

	// Token: 0x17000E72 RID: 3698
	// (get) Token: 0x060049A9 RID: 18857 RVA: 0x00139234 File Offset: 0x00137434
	protected virtual Vector4 customBorder
	{
		get
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x060049AA RID: 18858 RVA: 0x0013923C File Offset: 0x0013743C
	public virtual void UpdateUVs(bool force)
	{
		if (this.sprite != null && (force || this.mOuter != this.mSprite.outer))
		{
			Texture mainTexture = base.mainTexture;
			if (mainTexture != null)
			{
				this.mOuter = this.mSprite.outer;
				this.mOuterUV = this.mOuter;
				if (this.mAtlas.coordinates == UIAtlas.Coordinates.Pixels)
				{
					this.mOuterUV = NGUIMath.ConvertToTexCoords(this.mOuterUV, mainTexture.width, mainTexture.height);
				}
				base.ChangedAuto();
			}
		}
	}

	// Token: 0x060049AB RID: 18859 RVA: 0x001392D8 File Offset: 0x001374D8
	public override void MakePixelPerfect()
	{
		if (this.sprite == null)
		{
			return;
		}
		Texture mainTexture = base.mainTexture;
		Vector3 localScale = base.cachedTransform.localScale;
		if (mainTexture != null)
		{
			Rect rect = NGUIMath.ConvertToPixels(this.outerUV, mainTexture.width, mainTexture.height, true);
			float pixelSize = this.atlas.pixelSize;
			localScale.x = (float)Mathf.RoundToInt(rect.width * pixelSize);
			localScale.y = (float)Mathf.RoundToInt(rect.height * pixelSize);
			localScale.z = 1f;
			base.cachedTransform.localScale = localScale;
		}
		int num = Mathf.RoundToInt(localScale.x * (1f + this.mSprite.paddingLeft + this.mSprite.paddingRight));
		int num2 = Mathf.RoundToInt(localScale.y * (1f + this.mSprite.paddingTop + this.mSprite.paddingBottom));
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		if (num % 2 == 1 && (base.pivot == UIWidget.Pivot.Top || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Bottom))
		{
			localPosition.x = Mathf.Floor(localPosition.x) + 0.5f;
		}
		else
		{
			localPosition.x = Mathf.Round(localPosition.x);
		}
		if (num2 % 2 == 1 && (base.pivot == UIWidget.Pivot.Left || base.pivot == UIWidget.Pivot.Center || base.pivot == UIWidget.Pivot.Right))
		{
			localPosition.y = Mathf.Ceil(localPosition.y) - 0.5f;
		}
		else
		{
			localPosition.y = Mathf.Round(localPosition.y);
		}
		base.cachedTransform.localPosition = localPosition;
	}

	// Token: 0x060049AC RID: 18860 RVA: 0x001394C0 File Offset: 0x001376C0
	protected override void OnStart()
	{
		if (this.mAtlas != null)
		{
			this.UpdateUVs(true);
		}
	}

	// Token: 0x060049AD RID: 18861 RVA: 0x001394DC File Offset: 0x001376DC
	public override bool OnUpdate()
	{
		if (this.mLastName != this.mSpriteName)
		{
			this.mSprite = null;
			base.ChangedAuto();
			this.mLastName = this.mSpriteName;
			this.UpdateUVs(false);
			return true;
		}
		this.UpdateUVs(false);
		return false;
	}

	// Token: 0x060049AE RID: 18862 RVA: 0x0013952C File Offset: 0x0013772C
	public override void OnFill(MeshBuffer m)
	{
		m.FastQuad(this.mOuterUV, base.color);
	}

	// Token: 0x060049AF RID: 18863 RVA: 0x00139544 File Offset: 0x00137744
	protected override void GetCustomVector2s(int start, int end, UIWidget.WidgetFlags[] flags, Vector2[] v)
	{
		for (int i = start; i < end; i++)
		{
			if (flags[i] == UIWidget.WidgetFlags.CustomPivotOffset)
			{
				v[i] = this.pivotOffset;
			}
			else
			{
				base.GetCustomVector2s(i, i + 1, flags, v);
			}
		}
	}

	// Token: 0x04002998 RID: 10648
	private const UIWidget.WidgetFlags kRequiredFlags = UIWidget.WidgetFlags.CustomPivotOffset | UIWidget.WidgetFlags.CustomMaterialGet;

	// Token: 0x04002999 RID: 10649
	[HideInInspector]
	[SerializeField]
	private UIAtlas mAtlas;

	// Token: 0x0400299A RID: 10650
	[SerializeField]
	[HideInInspector]
	private string mSpriteName;

	// Token: 0x0400299B RID: 10651
	protected UIAtlas.Sprite mSprite;

	// Token: 0x0400299C RID: 10652
	protected Rect mOuter;

	// Token: 0x0400299D RID: 10653
	protected Rect mOuterUV;

	// Token: 0x0400299E RID: 10654
	private bool mSpriteSet;

	// Token: 0x0400299F RID: 10655
	private string mLastName = string.Empty;
}
