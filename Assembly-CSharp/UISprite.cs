using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020008F3 RID: 2291
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Sprite (Basic)")]
public class UISprite : global::UIWidget
{
	// Token: 0x06004E4A RID: 20042 RVA: 0x00142CD4 File Offset: 0x00140ED4
	public UISprite() : base(global::UIWidget.WidgetFlags.CustomPivotOffset | global::UIWidget.WidgetFlags.CustomMaterialGet)
	{
	}

	// Token: 0x06004E4B RID: 20043 RVA: 0x00142CE8 File Offset: 0x00140EE8
	protected UISprite(global::UIWidget.WidgetFlags additionalFlags) : base(global::UIWidget.WidgetFlags.CustomPivotOffset | global::UIWidget.WidgetFlags.CustomMaterialGet | additionalFlags)
	{
	}

	// Token: 0x17000F04 RID: 3844
	// (get) Token: 0x06004E4C RID: 20044 RVA: 0x00142D00 File Offset: 0x00140F00
	public Rect outerUV
	{
		get
		{
			this.UpdateUVs(false);
			return this.mOuterUV;
		}
	}

	// Token: 0x17000F05 RID: 3845
	// (get) Token: 0x06004E4D RID: 20045 RVA: 0x00142D10 File Offset: 0x00140F10
	// (set) Token: 0x06004E4E RID: 20046 RVA: 0x00142D18 File Offset: 0x00140F18
	public global::UIAtlas atlas
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
				this.material = ((!(this.mAtlas != null)) ? null : ((global::UIMaterial)this.mAtlas.spriteMaterial));
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

	// Token: 0x17000F06 RID: 3846
	// (get) Token: 0x06004E4F RID: 20047 RVA: 0x00142E10 File Offset: 0x00141010
	// (set) Token: 0x06004E50 RID: 20048 RVA: 0x00142E18 File Offset: 0x00141018
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

	// Token: 0x17000F07 RID: 3847
	// (get) Token: 0x06004E51 RID: 20049 RVA: 0x00142E98 File Offset: 0x00141098
	// (set) Token: 0x06004E52 RID: 20050 RVA: 0x00142F6C File Offset: 0x0014116C
	public global::UIAtlas.Sprite sprite
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
					this.material = (global::UIMaterial)this.mAtlas.spriteMaterial;
				}
			}
			return this.mSprite;
		}
		set
		{
			this.mSprite = value;
			this.mSpriteSet = true;
			this.material = ((this.mSprite == null || !(this.mAtlas != null)) ? null : ((global::UIMaterial)this.mAtlas.spriteMaterial));
		}
	}

	// Token: 0x17000F08 RID: 3848
	// (get) Token: 0x06004E53 RID: 20051 RVA: 0x00142FC0 File Offset: 0x001411C0
	public new Vector2 pivotOffset
	{
		get
		{
			Vector2 zero = Vector2.zero;
			if (this.sprite != null)
			{
				global::UIWidget.Pivot pivot = base.pivot;
				if (pivot == global::UIWidget.Pivot.Top || pivot == global::UIWidget.Pivot.Center || pivot == global::UIWidget.Pivot.Bottom)
				{
					zero.x = (-1f - this.mSprite.paddingRight + this.mSprite.paddingLeft) * 0.5f;
				}
				else if (pivot == global::UIWidget.Pivot.TopRight || pivot == global::UIWidget.Pivot.Right || pivot == global::UIWidget.Pivot.BottomRight)
				{
					zero.x = -1f - this.mSprite.paddingRight;
				}
				else
				{
					zero.x = this.mSprite.paddingLeft;
				}
				if (pivot == global::UIWidget.Pivot.Left || pivot == global::UIWidget.Pivot.Center || pivot == global::UIWidget.Pivot.Right)
				{
					zero.y = (1f + this.mSprite.paddingBottom - this.mSprite.paddingTop) * 0.5f;
				}
				else if (pivot == global::UIWidget.Pivot.BottomLeft || pivot == global::UIWidget.Pivot.Bottom || pivot == global::UIWidget.Pivot.BottomRight)
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

	// Token: 0x17000F09 RID: 3849
	// (get) Token: 0x06004E54 RID: 20052 RVA: 0x001430F8 File Offset: 0x001412F8
	// (set) Token: 0x06004E55 RID: 20053 RVA: 0x00143164 File Offset: 0x00141364
	public new global::UIMaterial material
	{
		get
		{
			global::UIMaterial uimaterial = base.baseMaterial;
			if (uimaterial == null)
			{
				uimaterial = ((!(this.mAtlas != null)) ? null : ((global::UIMaterial)this.mAtlas.spriteMaterial));
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

	// Token: 0x17000F0A RID: 3850
	// (get) Token: 0x06004E56 RID: 20054 RVA: 0x00143170 File Offset: 0x00141370
	protected override global::UIMaterial customMaterial
	{
		get
		{
			return this.material;
		}
	}

	// Token: 0x17000F0B RID: 3851
	// (get) Token: 0x06004E57 RID: 20055 RVA: 0x00143178 File Offset: 0x00141378
	public Vector4 border
	{
		get
		{
			if ((byte)(this.widgetFlags & global::UIWidget.WidgetFlags.CustomBorder) == 16)
			{
				return this.customBorder;
			}
			return Vector4.zero;
		}
	}

	// Token: 0x17000F0C RID: 3852
	// (get) Token: 0x06004E58 RID: 20056 RVA: 0x00143198 File Offset: 0x00141398
	protected virtual Vector4 customBorder
	{
		get
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x06004E59 RID: 20057 RVA: 0x001431A0 File Offset: 0x001413A0
	public virtual void UpdateUVs(bool force)
	{
		if (this.sprite != null && (force || this.mOuter != this.mSprite.outer))
		{
			Texture mainTexture = base.mainTexture;
			if (mainTexture != null)
			{
				this.mOuter = this.mSprite.outer;
				this.mOuterUV = this.mOuter;
				if (this.mAtlas.coordinates == global::UIAtlas.Coordinates.Pixels)
				{
					this.mOuterUV = global::NGUIMath.ConvertToTexCoords(this.mOuterUV, mainTexture.width, mainTexture.height);
				}
				base.ChangedAuto();
			}
		}
	}

	// Token: 0x06004E5A RID: 20058 RVA: 0x0014323C File Offset: 0x0014143C
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
			Rect rect = global::NGUIMath.ConvertToPixels(this.outerUV, mainTexture.width, mainTexture.height, true);
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
		if (num % 2 == 1 && (base.pivot == global::UIWidget.Pivot.Top || base.pivot == global::UIWidget.Pivot.Center || base.pivot == global::UIWidget.Pivot.Bottom))
		{
			localPosition.x = Mathf.Floor(localPosition.x) + 0.5f;
		}
		else
		{
			localPosition.x = Mathf.Round(localPosition.x);
		}
		if (num2 % 2 == 1 && (base.pivot == global::UIWidget.Pivot.Left || base.pivot == global::UIWidget.Pivot.Center || base.pivot == global::UIWidget.Pivot.Right))
		{
			localPosition.y = Mathf.Ceil(localPosition.y) - 0.5f;
		}
		else
		{
			localPosition.y = Mathf.Round(localPosition.y);
		}
		base.cachedTransform.localPosition = localPosition;
	}

	// Token: 0x06004E5B RID: 20059 RVA: 0x00143424 File Offset: 0x00141624
	protected override void OnStart()
	{
		if (this.mAtlas != null)
		{
			this.UpdateUVs(true);
		}
	}

	// Token: 0x06004E5C RID: 20060 RVA: 0x00143440 File Offset: 0x00141640
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

	// Token: 0x06004E5D RID: 20061 RVA: 0x00143490 File Offset: 0x00141690
	public override void OnFill(NGUI.Meshing.MeshBuffer m)
	{
		m.FastQuad(this.mOuterUV, base.color);
	}

	// Token: 0x06004E5E RID: 20062 RVA: 0x001434A8 File Offset: 0x001416A8
	protected override void GetCustomVector2s(int start, int end, global::UIWidget.WidgetFlags[] flags, Vector2[] v)
	{
		for (int i = start; i < end; i++)
		{
			if (flags[i] == global::UIWidget.WidgetFlags.CustomPivotOffset)
			{
				v[i] = this.pivotOffset;
			}
			else
			{
				base.GetCustomVector2s(i, i + 1, flags, v);
			}
		}
	}

	// Token: 0x04002BE6 RID: 11238
	private const global::UIWidget.WidgetFlags kRequiredFlags = global::UIWidget.WidgetFlags.CustomPivotOffset | global::UIWidget.WidgetFlags.CustomMaterialGet;

	// Token: 0x04002BE7 RID: 11239
	[HideInInspector]
	[SerializeField]
	private global::UIAtlas mAtlas;

	// Token: 0x04002BE8 RID: 11240
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x04002BE9 RID: 11241
	protected global::UIAtlas.Sprite mSprite;

	// Token: 0x04002BEA RID: 11242
	protected Rect mOuter;

	// Token: 0x04002BEB RID: 11243
	protected Rect mOuterUV;

	// Token: 0x04002BEC RID: 11244
	private bool mSpriteSet;

	// Token: 0x04002BED RID: 11245
	private string mLastName = string.Empty;
}
