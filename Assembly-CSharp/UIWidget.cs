using System;
using System.Collections.Generic;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020007AA RID: 1962
public abstract class UIWidget : MonoBehaviour
{
	// Token: 0x060046DF RID: 18143 RVA: 0x0011CDC8 File Offset: 0x0011AFC8
	protected UIWidget(UIWidget.WidgetFlags flags)
	{
		this.widgetFlags = flags;
	}

	// Token: 0x060046E1 RID: 18145 RVA: 0x0011CE58 File Offset: 0x0011B058
	public static void GlobalUpdate()
	{
		UIWidget.Global.WidgetUpdate();
	}

	// Token: 0x060046E2 RID: 18146 RVA: 0x0011CE60 File Offset: 0x0011B060
	public void MarkAsChangedForced()
	{
		this.MarkAsChanged();
		this.mForcedChanged = true;
	}

	// Token: 0x17000DA8 RID: 3496
	// (get) Token: 0x060046E3 RID: 18147 RVA: 0x0011CE70 File Offset: 0x0011B070
	// (set) Token: 0x060046E4 RID: 18148 RVA: 0x0011CE78 File Offset: 0x0011B078
	public bool alphaUnchecked
	{
		get
		{
			return this.mAlphaUnchecked;
		}
		set
		{
			if (value)
			{
				if (!this.mAlphaUnchecked)
				{
					this.mAlphaUnchecked = true;
					if (NGUITools.ZeroAlpha(this.mColor.a))
					{
						this.mChangedCall = true;
					}
				}
			}
			else if (this.mAlphaUnchecked)
			{
				this.mAlphaUnchecked = false;
				if (NGUITools.ZeroAlpha(this.mColor.a))
				{
					this.mChangedCall = true;
				}
			}
		}
	}

	// Token: 0x17000DA9 RID: 3497
	// (get) Token: 0x060046E5 RID: 18149 RVA: 0x0011CEEC File Offset: 0x0011B0EC
	public bool changesQueued
	{
		get
		{
			return this.mChangedCall || this.mForcedChanged;
		}
	}

	// Token: 0x060046E6 RID: 18150 RVA: 0x0011CF04 File Offset: 0x0011B104
	protected void ChangedAuto()
	{
		this.mChangedCall = true;
	}

	// Token: 0x17000DAA RID: 3498
	// (get) Token: 0x060046E7 RID: 18151 RVA: 0x0011CF10 File Offset: 0x0011B110
	private UIGeometry mGeom
	{
		get
		{
			UIGeometry result;
			if ((result = this.__mGeom) == null)
			{
				result = (this.__mGeom = new UIGeometry());
			}
			return result;
		}
	}

	// Token: 0x17000DAB RID: 3499
	// (get) Token: 0x060046E8 RID: 18152 RVA: 0x0011CF38 File Offset: 0x0011B138
	// (set) Token: 0x060046E9 RID: 18153 RVA: 0x0011CF40 File Offset: 0x0011B140
	public Color color
	{
		get
		{
			return this.mColor;
		}
		set
		{
			if (this.mColor != value)
			{
				this.mColor = value;
				this.mChangedCall = true;
			}
		}
	}

	// Token: 0x17000DAC RID: 3500
	// (get) Token: 0x060046EA RID: 18154 RVA: 0x0011CF64 File Offset: 0x0011B164
	// (set) Token: 0x060046EB RID: 18155 RVA: 0x0011CF74 File Offset: 0x0011B174
	public float alpha
	{
		get
		{
			return this.mColor.a;
		}
		set
		{
			Color color = this.mColor;
			color.a = value;
			this.color = color;
		}
	}

	// Token: 0x17000DAD RID: 3501
	// (get) Token: 0x060046EC RID: 18156 RVA: 0x0011CF98 File Offset: 0x0011B198
	// (set) Token: 0x060046ED RID: 18157 RVA: 0x0011CFA0 File Offset: 0x0011B1A0
	public UIWidget.Pivot pivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				this.mPivot = value;
				this.mChangedCall = true;
			}
		}
	}

	// Token: 0x17000DAE RID: 3502
	// (get) Token: 0x060046EE RID: 18158 RVA: 0x0011CFBC File Offset: 0x0011B1BC
	// (set) Token: 0x060046EF RID: 18159 RVA: 0x0011CFC4 File Offset: 0x0011B1C4
	public int depth
	{
		get
		{
			return this.mDepth;
		}
		set
		{
			if (this.mDepth != value)
			{
				this.mDepth = value;
				if (this.mPanel != null)
				{
					this.mPanel.MarkMaterialAsChanged(this.material, true);
				}
			}
		}
	}

	// Token: 0x17000DAF RID: 3503
	// (get) Token: 0x060046F0 RID: 18160 RVA: 0x0011D008 File Offset: 0x0011B208
	public Transform cachedTransform
	{
		get
		{
			if (!this.gotCachedTransform)
			{
				this.mTrans = base.transform;
				this.gotCachedTransform = true;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000DB0 RID: 3504
	// (get) Token: 0x060046F1 RID: 18161 RVA: 0x0011D03C File Offset: 0x0011B23C
	// (set) Token: 0x060046F2 RID: 18162 RVA: 0x0011D05C File Offset: 0x0011B25C
	public UIMaterial material
	{
		get
		{
			if ((byte)(this.widgetFlags & UIWidget.WidgetFlags.CustomMaterialGet) == 4)
			{
				return this.customMaterial;
			}
			return this.baseMaterial;
		}
		set
		{
			if ((byte)(this.widgetFlags & UIWidget.WidgetFlags.CustomMaterialSet) == 8)
			{
				this.customMaterial = value;
			}
			else
			{
				this.baseMaterial = value;
			}
		}
	}

	// Token: 0x17000DB1 RID: 3505
	// (get) Token: 0x060046F3 RID: 18163 RVA: 0x0011D08C File Offset: 0x0011B28C
	// (set) Token: 0x060046F4 RID: 18164 RVA: 0x0011D09C File Offset: 0x0011B29C
	protected UIMaterial baseMaterial
	{
		get
		{
			return (UIMaterial)this.mMat;
		}
		set
		{
			UIMaterial uimaterial = (UIMaterial)this.mMat;
			if (uimaterial != value)
			{
				if (uimaterial != null && this.mPanel != null)
				{
					this.mPanel.RemoveWidget(this);
				}
				this.mPanel = null;
				this.mMat = (Material)value;
				this.mTex = null;
				if (this.mMat != null)
				{
					this.CreatePanel();
				}
			}
		}
	}

	// Token: 0x060046F5 RID: 18165 RVA: 0x0011D11C File Offset: 0x0011B31C
	public void ForceReloadMaterial()
	{
		if (this.mMat)
		{
			if (this.mPanel)
			{
				this.mPanel.RemoveWidget(this);
			}
			this.mPanel = null;
			this.mTex = null;
			if (this.mMat)
			{
				this.CreatePanel();
			}
		}
	}

	// Token: 0x17000DB2 RID: 3506
	// (get) Token: 0x060046F6 RID: 18166 RVA: 0x0011D17C File Offset: 0x0011B37C
	// (set) Token: 0x060046F7 RID: 18167 RVA: 0x0011D184 File Offset: 0x0011B384
	protected virtual UIMaterial customMaterial
	{
		get
		{
			throw new NotSupportedException();
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	// Token: 0x17000DB3 RID: 3507
	// (get) Token: 0x060046F8 RID: 18168 RVA: 0x0011D18C File Offset: 0x0011B38C
	public Texture mainTexture
	{
		get
		{
			if (!this.mTex)
			{
				UIMaterial material = this.material;
				if (material != null)
				{
					this.mTex = material.mainTexture;
				}
			}
			return this.mTex;
		}
	}

	// Token: 0x17000DB4 RID: 3508
	// (get) Token: 0x060046F9 RID: 18169 RVA: 0x0011D1D0 File Offset: 0x0011B3D0
	// (set) Token: 0x060046FA RID: 18170 RVA: 0x0011D1F0 File Offset: 0x0011B3F0
	public UIPanel panel
	{
		get
		{
			if (!this.mPanel)
			{
				this.CreatePanel();
			}
			return this.mPanel;
		}
		set
		{
			this.mPanel = value;
		}
	}

	// Token: 0x17000DB5 RID: 3509
	// (get) Token: 0x060046FB RID: 18171 RVA: 0x0011D1FC File Offset: 0x0011B3FC
	// (set) Token: 0x060046FC RID: 18172 RVA: 0x0011D204 File Offset: 0x0011B404
	public int visibleFlag
	{
		get
		{
			return this.mVisibleFlag;
		}
		set
		{
			this.mVisibleFlag = value;
		}
	}

	// Token: 0x060046FD RID: 18173 RVA: 0x0011D210 File Offset: 0x0011B410
	public static int CompareFunc(UIWidget left, UIWidget right)
	{
		if (left.mDepth > right.mDepth)
		{
			return 1;
		}
		if (left.mDepth < right.mDepth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060046FE RID: 18174 RVA: 0x0011D23C File Offset: 0x0011B43C
	public virtual void MarkAsChanged()
	{
		this.mChangedCall = true;
		if (this.mPanel != null && base.enabled && base.gameObject.activeInHierarchy && !Application.isPlaying && this.material != null)
		{
			this.mPanel.AddWidget(this);
			this.CheckLayer();
		}
	}

	// Token: 0x060046FF RID: 18175 RVA: 0x0011D2AC File Offset: 0x0011B4AC
	private void CreatePanel()
	{
		if (!this.mPanel && base.enabled && base.gameObject.activeInHierarchy && this.material != null)
		{
			this.mPanel = UIPanel.Find(this.cachedTransform);
			if (this.mPanel != null)
			{
				this.CheckLayer();
				this.mPanel.AddWidget(this);
				this.mChangedCall = true;
			}
		}
	}

	// Token: 0x06004700 RID: 18176 RVA: 0x0011D330 File Offset: 0x0011B530
	private void CheckLayer()
	{
		if (this.mPanel != null && this.mPanel.gameObject.layer != base.gameObject.layer)
		{
			Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = this.mPanel.gameObject.layer;
		}
	}

	// Token: 0x06004701 RID: 18177 RVA: 0x0011D394 File Offset: 0x0011B594
	private void CheckParent()
	{
		if (this.mPanel != null)
		{
			bool flag = true;
			Transform parent = this.cachedTransform.parent;
			while (parent != null)
			{
				if (parent == this.mPanel.cachedTransform)
				{
					break;
				}
				if (!this.mPanel.WatchesTransform(parent))
				{
					flag = false;
					break;
				}
				parent = parent.parent;
			}
			if (!flag)
			{
				if ((byte)(this.widgetFlags & UIWidget.WidgetFlags.KeepsMaterial) != 64)
				{
					this.material = null;
				}
				this.mPanel = null;
				this.CreatePanel();
			}
		}
	}

	// Token: 0x06004702 RID: 18178 RVA: 0x0011D438 File Offset: 0x0011B638
	protected void Awake()
	{
		this.mPlayMode = Application.isPlaying;
		UIWidget.Global.RegisterWidget(this);
	}

	// Token: 0x06004703 RID: 18179 RVA: 0x0011D44C File Offset: 0x0011B64C
	private void OnEnable()
	{
		UIWidget.Global.WidgetEnabled(this);
		this.mChangedCall = true;
		if ((byte)(this.widgetFlags & UIWidget.WidgetFlags.KeepsMaterial) != 64)
		{
			this.mMat = null;
			this.mTex = null;
		}
		if (this.mPanel != null && this.material != null)
		{
			this.mPanel.MarkMaterialAsChanged(this.material, false);
		}
	}

	// Token: 0x06004704 RID: 18180 RVA: 0x0011D4BC File Offset: 0x0011B6BC
	private void Start()
	{
		this.OnStart();
		this.CreatePanel();
	}

	// Token: 0x06004705 RID: 18181 RVA: 0x0011D4CC File Offset: 0x0011B6CC
	private void DefaultUpdate()
	{
		if (!this.mPanel)
		{
			this.CreatePanel();
		}
		Vector3 localScale = this.cachedTransform.localScale;
		if (localScale.z != 1f)
		{
			localScale.z = 1f;
			this.mTrans.localScale = localScale;
		}
	}

	// Token: 0x06004706 RID: 18182 RVA: 0x0011D524 File Offset: 0x0011B724
	private void OnDisable()
	{
		UIWidget.Global.WidgetDisabled(this);
		if ((byte)(this.widgetFlags & UIWidget.WidgetFlags.KeepsMaterial) != 64)
		{
			this.material = null;
		}
		else if (this.mPanel != null)
		{
			this.mPanel.RemoveWidget(this);
		}
		this.mPanel = null;
	}

	// Token: 0x06004707 RID: 18183 RVA: 0x0011D578 File Offset: 0x0011B778
	private void OnDestroy()
	{
		UIWidget.Global.UnregisterWidget(this);
		if (this.mPanel != null)
		{
			this.mPanel.RemoveWidget(this);
			this.mPanel = null;
		}
		this.__mGeom = null;
	}

	// Token: 0x06004708 RID: 18184 RVA: 0x0011D5AC File Offset: 0x0011B7AC
	public bool UpdateGeometry(ref Matrix4x4 worldToPanel, bool parentMoved, bool generateNormals)
	{
		if (!this.material)
		{
			return false;
		}
		UIGeometry mGeom = this.mGeom;
		if (this.OnUpdate() || this.mChangedCall || this.mForcedChanged)
		{
			this.mChangedCall = false;
			this.mForcedChanged = false;
			mGeom.Clear();
			if (this.mAlphaUnchecked || !NGUITools.ZeroAlpha(this.mColor.a))
			{
				this.OnFill(mGeom.meshBuffer);
			}
			if (mGeom.hasVertices)
			{
				Vector2 vector;
				Vector2 vector2;
				switch ((byte)(this.widgetFlags & (UIWidget.WidgetFlags.CustomPivotOffset | UIWidget.WidgetFlags.CustomRelativeSize)))
				{
				case 1:
					UIWidget.tempWidgetFlags[0] = UIWidget.WidgetFlags.CustomPivotOffset;
					this.GetCustomVector2s(0, 1, UIWidget.tempWidgetFlags, UIWidget.tempVector2s);
					vector = UIWidget.tempVector2s[0];
					vector2.x = (vector2.y = 1f);
					break;
				case 2:
					UIWidget.tempWidgetFlags[0] = UIWidget.WidgetFlags.CustomRelativeSize;
					this.GetCustomVector2s(0, 1, UIWidget.tempWidgetFlags, UIWidget.tempVector2s);
					vector2 = UIWidget.tempVector2s[0];
					vector = UIWidget.DefaultPivot(this.mPivot);
					break;
				case 3:
					UIWidget.tempWidgetFlags[0] = UIWidget.WidgetFlags.CustomPivotOffset;
					UIWidget.tempWidgetFlags[1] = UIWidget.WidgetFlags.CustomRelativeSize;
					this.GetCustomVector2s(0, 2, UIWidget.tempWidgetFlags, UIWidget.tempVector2s);
					vector = UIWidget.tempVector2s[0];
					vector2 = UIWidget.tempVector2s[1];
					break;
				default:
					vector = UIWidget.DefaultPivot(this.mPivot);
					vector2.x = (vector2.y = 1f);
					break;
				}
				Vector3 vector3;
				vector3.x = vector.x * vector2.x;
				vector3.y = vector.y * vector2.y;
				vector3.z = 0f;
				Matrix4x4 matrix4x = worldToPanel * this.cachedTransform.localToWorldMatrix;
				mGeom.Apply(ref vector3, ref matrix4x);
			}
			return true;
		}
		if (mGeom.hasVertices && parentMoved)
		{
			Matrix4x4 matrix4x2 = worldToPanel * this.cachedTransform.localToWorldMatrix;
			mGeom.Apply(ref matrix4x2);
		}
		return false;
	}

	// Token: 0x06004709 RID: 18185 RVA: 0x0011D7E8 File Offset: 0x0011B9E8
	public void WriteToBuffers(MeshBuffer m)
	{
		this.mGeom.WriteToBuffers(m);
	}

	// Token: 0x0600470A RID: 18186 RVA: 0x0011D7F8 File Offset: 0x0011B9F8
	public virtual void MakePixelPerfect()
	{
		Vector3 localScale = this.cachedTransform.localScale;
		int num = Mathf.RoundToInt(localScale.x);
		int num2 = Mathf.RoundToInt(localScale.y);
		localScale.x = (float)num;
		localScale.y = (float)num2;
		localScale.z = 1f;
		Vector3 localPosition = this.cachedTransform.localPosition;
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		if (num % 2 == 1 && (this.pivot == UIWidget.Pivot.Top || this.pivot == UIWidget.Pivot.Center || this.pivot == UIWidget.Pivot.Bottom))
		{
			localPosition.x = Mathf.Floor(localPosition.x) + 0.5f;
		}
		else
		{
			localPosition.x = Mathf.Round(localPosition.x);
		}
		if (num2 % 2 == 1 && (this.pivot == UIWidget.Pivot.Left || this.pivot == UIWidget.Pivot.Center || this.pivot == UIWidget.Pivot.Right))
		{
			localPosition.y = Mathf.Ceil(localPosition.y) - 0.5f;
		}
		else
		{
			localPosition.y = Mathf.Round(localPosition.y);
		}
		this.cachedTransform.localPosition = localPosition;
		this.cachedTransform.localScale = localScale;
	}

	// Token: 0x0600470B RID: 18187 RVA: 0x0011D940 File Offset: 0x0011BB40
	protected static Vector2 DefaultPivot(UIWidget.Pivot pivot)
	{
		Vector2 result;
		switch (pivot)
		{
		case UIWidget.Pivot.TopLeft:
			result.x = 0f;
			result.y = 0f;
			break;
		case UIWidget.Pivot.Top:
			result.y = -0.5f;
			result.x = -1f;
			break;
		case UIWidget.Pivot.TopRight:
			result.y = 0f;
			result.x = -1f;
			break;
		case UIWidget.Pivot.Left:
			result.x = 0f;
			result.y = 0.5f;
			break;
		case UIWidget.Pivot.Center:
			result.x = -0.5f;
			result.y = 0.5f;
			break;
		case UIWidget.Pivot.Right:
			result.x = -1f;
			result.y = 0.5f;
			break;
		case UIWidget.Pivot.BottomLeft:
			result.x = 0f;
			result.y = 1f;
			break;
		case UIWidget.Pivot.Bottom:
			result.x = -0.5f;
			result.y = 1f;
			break;
		case UIWidget.Pivot.BottomRight:
			result.x = -1f;
			result.y = 1f;
			break;
		default:
			throw new NotImplementedException();
		}
		return result;
	}

	// Token: 0x17000DB6 RID: 3510
	// (get) Token: 0x0600470C RID: 18188 RVA: 0x0011DA8C File Offset: 0x0011BC8C
	public Vector2 pivotOffset
	{
		get
		{
			if ((byte)(this.widgetFlags & UIWidget.WidgetFlags.CustomPivotOffset) == 1)
			{
				UIWidget.tempWidgetFlags[0] = UIWidget.WidgetFlags.CustomPivotOffset;
				this.GetCustomVector2s(0, 1, UIWidget.tempWidgetFlags, UIWidget.tempVector2s);
				return UIWidget.tempVector2s[0];
			}
			return UIWidget.DefaultPivot(this.mPivot);
		}
	}

	// Token: 0x17000DB7 RID: 3511
	// (get) Token: 0x0600470D RID: 18189 RVA: 0x0011DAE0 File Offset: 0x0011BCE0
	[Obsolete("Use 'relativeSize' instead")]
	public Vector2 visibleSize
	{
		get
		{
			return this.relativeSize;
		}
	}

	// Token: 0x0600470E RID: 18190 RVA: 0x0011DAE8 File Offset: 0x0011BCE8
	protected virtual void GetCustomVector2s(int start, int end, UIWidget.WidgetFlags[] flags, Vector2[] v)
	{
		throw new NotSupportedException("Only call base.GetCustomVector2s when its something not supported by your implementation, otherwise the custructor for your class is incorrect in its usage.");
	}

	// Token: 0x17000DB8 RID: 3512
	// (get) Token: 0x0600470F RID: 18191 RVA: 0x0011DAF4 File Offset: 0x0011BCF4
	public Vector2 relativeSize
	{
		get
		{
			if ((byte)(this.widgetFlags & UIWidget.WidgetFlags.CustomRelativeSize) == 2)
			{
				UIWidget.tempWidgetFlags[0] = UIWidget.WidgetFlags.CustomRelativeSize;
				this.GetCustomVector2s(0, 1, UIWidget.tempWidgetFlags, UIWidget.tempVector2s);
				return UIWidget.tempVector2s[0];
			}
			return Vector2.one;
		}
	}

	// Token: 0x17000DB9 RID: 3513
	// (get) Token: 0x06004710 RID: 18192 RVA: 0x0011DB40 File Offset: 0x0011BD40
	public bool keepMaterial
	{
		get
		{
			return (byte)(this.widgetFlags & UIWidget.WidgetFlags.KeepsMaterial) == 64;
		}
	}

	// Token: 0x06004711 RID: 18193 RVA: 0x0011DB50 File Offset: 0x0011BD50
	protected virtual void OnStart()
	{
	}

	// Token: 0x06004712 RID: 18194 RVA: 0x0011DB54 File Offset: 0x0011BD54
	public virtual bool OnUpdate()
	{
		return false;
	}

	// Token: 0x06004713 RID: 18195
	public abstract void OnFill(MeshBuffer m);

	// Token: 0x06004714 RID: 18196 RVA: 0x0011DB58 File Offset: 0x0011BD58
	public void GetPivotOffsetAndRelativeSize(out Vector2 pivotOffset, out Vector2 relativeSize)
	{
		switch ((byte)(this.widgetFlags & (UIWidget.WidgetFlags.CustomPivotOffset | UIWidget.WidgetFlags.CustomRelativeSize)))
		{
		case 1:
			UIWidget.tempWidgetFlags[0] = UIWidget.WidgetFlags.CustomPivotOffset;
			this.GetCustomVector2s(0, 1, UIWidget.tempWidgetFlags, UIWidget.tempVector2s);
			pivotOffset = UIWidget.tempVector2s[0];
			relativeSize.x = (relativeSize.y = 1f);
			break;
		case 2:
			UIWidget.tempWidgetFlags[0] = UIWidget.WidgetFlags.CustomRelativeSize;
			this.GetCustomVector2s(0, 1, UIWidget.tempWidgetFlags, UIWidget.tempVector2s);
			relativeSize = UIWidget.tempVector2s[0];
			pivotOffset = UIWidget.DefaultPivot(this.mPivot);
			break;
		case 3:
			UIWidget.tempWidgetFlags[0] = UIWidget.WidgetFlags.CustomPivotOffset;
			UIWidget.tempWidgetFlags[1] = UIWidget.WidgetFlags.CustomRelativeSize;
			this.GetCustomVector2s(0, 2, UIWidget.tempWidgetFlags, UIWidget.tempVector2s);
			pivotOffset = UIWidget.tempVector2s[0];
			relativeSize = UIWidget.tempVector2s[1];
			break;
		default:
			pivotOffset = UIWidget.DefaultPivot(this.mPivot);
			relativeSize.x = (relativeSize.y = 1f);
			break;
		}
	}

	// Token: 0x040026FC RID: 9980
	[HideInInspector]
	[SerializeField]
	private Material mMat;

	// Token: 0x040026FD RID: 9981
	[SerializeField]
	[HideInInspector]
	private Color mColor = Color.white;

	// Token: 0x040026FE RID: 9982
	[SerializeField]
	[HideInInspector]
	private UIWidget.Pivot mPivot = UIWidget.Pivot.Center;

	// Token: 0x040026FF RID: 9983
	[HideInInspector]
	[SerializeField]
	private int mDepth;

	// Token: 0x04002700 RID: 9984
	[HideInInspector]
	[SerializeField]
	private bool mAlphaUnchecked;

	// Token: 0x04002701 RID: 9985
	[NonSerialized]
	private bool mForcedChanged;

	// Token: 0x04002702 RID: 9986
	private Transform mTrans;

	// Token: 0x04002703 RID: 9987
	private Texture mTex;

	// Token: 0x04002704 RID: 9988
	private UIPanel mPanel;

	// Token: 0x04002705 RID: 9989
	private bool mChangedCall = true;

	// Token: 0x04002706 RID: 9990
	protected bool mPlayMode = true;

	// Token: 0x04002707 RID: 9991
	private bool gotCachedTransform;

	// Token: 0x04002708 RID: 9992
	[NonSerialized]
	protected readonly UIWidget.WidgetFlags widgetFlags;

	// Token: 0x04002709 RID: 9993
	private Vector3 mDiffPos;

	// Token: 0x0400270A RID: 9994
	private Quaternion mDiffRot;

	// Token: 0x0400270B RID: 9995
	private Vector3 mDiffScale;

	// Token: 0x0400270C RID: 9996
	private int mVisibleFlag = -1;

	// Token: 0x0400270D RID: 9997
	private int globalIndex = -1;

	// Token: 0x0400270E RID: 9998
	private UIGeometry __mGeom;

	// Token: 0x0400270F RID: 9999
	private static Vector2[] tempVector2s = new Vector2[]
	{
		default(Vector2),
		default(Vector2)
	};

	// Token: 0x04002710 RID: 10000
	private static UIWidget.WidgetFlags[] tempWidgetFlags = new UIWidget.WidgetFlags[2];

	// Token: 0x020007AB RID: 1963
	[Flags]
	protected enum WidgetFlags : byte
	{
		// Token: 0x04002712 RID: 10002
		CustomPivotOffset = 1,
		// Token: 0x04002713 RID: 10003
		CustomRelativeSize = 2,
		// Token: 0x04002714 RID: 10004
		CustomMaterialGet = 4,
		// Token: 0x04002715 RID: 10005
		CustomMaterialSet = 8,
		// Token: 0x04002716 RID: 10006
		CustomBorder = 16,
		// Token: 0x04002717 RID: 10007
		KeepsMaterial = 64,
		// Token: 0x04002718 RID: 10008
		Reserved = 128
	}

	// Token: 0x020007AC RID: 1964
	public enum Pivot
	{
		// Token: 0x0400271A RID: 10010
		TopLeft,
		// Token: 0x0400271B RID: 10011
		Top,
		// Token: 0x0400271C RID: 10012
		TopRight,
		// Token: 0x0400271D RID: 10013
		Left,
		// Token: 0x0400271E RID: 10014
		Center,
		// Token: 0x0400271F RID: 10015
		Right,
		// Token: 0x04002720 RID: 10016
		BottomLeft,
		// Token: 0x04002721 RID: 10017
		Bottom,
		// Token: 0x04002722 RID: 10018
		BottomRight
	}

	// Token: 0x020007AD RID: 1965
	private static class Global
	{
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06004715 RID: 18197 RVA: 0x0011DC94 File Offset: 0x0011BE94
		private static bool noGlobal
		{
			get
			{
				return !Application.isPlaying;
			}
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x0011DCA0 File Offset: 0x0011BEA0
		public static void RegisterWidget(UIWidget widget)
		{
			if (UIWidget.Global.noGlobal)
			{
				return;
			}
			UIGlobal.EnsureGlobal();
			if (widget.globalIndex == -1)
			{
				widget.globalIndex = UIWidget.Global.g.allWidgets.Count;
				UIWidget.Global.g.allWidgets.Add(widget);
			}
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x0011DCDC File Offset: 0x0011BEDC
		public static void UnregisterWidget(UIWidget widget)
		{
			if (UIWidget.Global.noGlobal)
			{
				return;
			}
			if (widget.globalIndex != -1)
			{
				UIWidget.Global.g.allWidgets.RemoveAt(widget.globalIndex);
				int i = widget.globalIndex;
				int count = UIWidget.Global.g.allWidgets.Count;
				while (i < count)
				{
					UIWidget.Global.g.allWidgets[i].globalIndex = i;
					i++;
				}
				widget.globalIndex = -1;
			}
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x0011DD4C File Offset: 0x0011BF4C
		public static void WidgetEnabled(UIWidget widget)
		{
			if (UIWidget.Global.noGlobal)
			{
				return;
			}
			UIWidget.Global.g.enabledWidgets.Add(widget);
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x0011DD68 File Offset: 0x0011BF68
		public static void WidgetDisabled(UIWidget widget)
		{
			if (UIWidget.Global.noGlobal)
			{
				return;
			}
			UIWidget.Global.g.enabledWidgets.Remove(widget);
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x0011DD84 File Offset: 0x0011BF84
		public static void WidgetUpdate()
		{
			if (UIWidget.Global.noGlobal)
			{
				return;
			}
			try
			{
				UIWidget.Global.g.enableWidgetsSwap.AddRange(UIWidget.Global.g.enabledWidgets);
				foreach (UIWidget uiwidget in UIWidget.Global.g.enableWidgetsSwap)
				{
					if (uiwidget && uiwidget.enabled)
					{
						uiwidget.DefaultUpdate();
					}
				}
			}
			finally
			{
				UIWidget.Global.g.enableWidgetsSwap.Clear();
			}
		}

		// Token: 0x020007AE RID: 1966
		public static class g
		{
			// Token: 0x0600471B RID: 18203 RVA: 0x0011DE40 File Offset: 0x0011C040
			static g()
			{
				UIGlobal.EnsureGlobal();
			}

			// Token: 0x04002723 RID: 10019
			public static List<UIWidget> allWidgets = new List<UIWidget>();

			// Token: 0x04002724 RID: 10020
			public static HashSet<UIWidget> enabledWidgets = new HashSet<UIWidget>();

			// Token: 0x04002725 RID: 10021
			public static List<UIWidget> enableWidgetsSwap = new List<UIWidget>();
		}
	}
}
