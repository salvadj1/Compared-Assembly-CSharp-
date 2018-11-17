using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020007F8 RID: 2040
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Panel")]
public class UIPanel : MonoBehaviour
{
	// Token: 0x06004923 RID: 18723 RVA: 0x0012C0EC File Offset: 0x0012A2EC
	public static void GlobalUpdate()
	{
		UIPanel.Global.PanelUpdate();
	}

	// Token: 0x17000E46 RID: 3654
	// (get) Token: 0x06004924 RID: 18724 RVA: 0x0012C0F4 File Offset: 0x0012A2F4
	// (set) Token: 0x06004925 RID: 18725 RVA: 0x0012C114 File Offset: 0x0012A314
	public UIPanel RootPanel
	{
		get
		{
			return (!this._rootPanel) ? this : this._rootPanel;
		}
		set
		{
			if (value == this)
			{
				this._rootPanel = null;
			}
			else
			{
				this._rootPanel = value;
			}
		}
	}

	// Token: 0x17000E47 RID: 3655
	// (get) Token: 0x06004926 RID: 18726 RVA: 0x0012C138 File Offset: 0x0012A338
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000E48 RID: 3656
	// (get) Token: 0x06004927 RID: 18727 RVA: 0x0012C160 File Offset: 0x0012A360
	public bool changedLastFrame
	{
		get
		{
			return this.mChangedLastFrame;
		}
	}

	// Token: 0x17000E49 RID: 3657
	// (get) Token: 0x06004928 RID: 18728 RVA: 0x0012C168 File Offset: 0x0012A368
	// (set) Token: 0x06004929 RID: 18729 RVA: 0x0012C170 File Offset: 0x0012A370
	public UIPanel.DebugInfo debugInfo
	{
		get
		{
			return this.mDebugInfo;
		}
		set
		{
			if (this.mDebugInfo != value)
			{
				this.mDebugInfo = value;
				UIDrawCall.Iterator iterator = (UIDrawCall.Iterator)this.mDrawCalls;
				HideFlags hideFlags = (this.mDebugInfo != UIPanel.DebugInfo.Geometry) ? 13 : 12;
				while (iterator.Has)
				{
					GameObject gameObject = iterator.Current.gameObject;
					iterator = iterator.Next;
					gameObject.SetActive(false);
					gameObject.hideFlags = hideFlags;
					gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x17000E4A RID: 3658
	// (get) Token: 0x0600492A RID: 18730 RVA: 0x0012C1F0 File Offset: 0x0012A3F0
	// (set) Token: 0x0600492B RID: 18731 RVA: 0x0012C1F8 File Offset: 0x0012A3F8
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mCheckVisibility = true;
				this.mClipping = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x17000E4B RID: 3659
	// (get) Token: 0x0600492C RID: 18732 RVA: 0x0012C228 File Offset: 0x0012A428
	// (set) Token: 0x0600492D RID: 18733 RVA: 0x0012C230 File Offset: 0x0012A430
	public Vector4 clipRange
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			if (this.mClipRange != value)
			{
				this.mCullTime = ((this.mCullTime != 0f) ? (Time.realtimeSinceStartup + 0.15f) : 0.001f);
				this.mCheckVisibility = true;
				this.mClipRange = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x17000E4C RID: 3660
	// (get) Token: 0x0600492E RID: 18734 RVA: 0x0012C290 File Offset: 0x0012A490
	// (set) Token: 0x0600492F RID: 18735 RVA: 0x0012C298 File Offset: 0x0012A498
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoftness;
		}
		set
		{
			if (this.mClipSoftness != value)
			{
				this.mClipSoftness = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x17000E4D RID: 3661
	// (get) Token: 0x06004930 RID: 18736 RVA: 0x0012C2B8 File Offset: 0x0012A4B8
	public List<UIWidget> widgets
	{
		get
		{
			return this.mWidgets;
		}
	}

	// Token: 0x17000E4E RID: 3662
	// (get) Token: 0x06004931 RID: 18737 RVA: 0x0012C2C0 File Offset: 0x0012A4C0
	public UIDrawCall.Iterator drawCalls
	{
		get
		{
			return (UIDrawCall.Iterator)this.mDrawCalls;
		}
	}

	// Token: 0x17000E4F RID: 3663
	// (get) Token: 0x06004932 RID: 18738 RVA: 0x0012C2D0 File Offset: 0x0012A4D0
	public int drawCallCount
	{
		get
		{
			return this.mDrawCallCount;
		}
	}

	// Token: 0x17000E50 RID: 3664
	// (get) Token: 0x06004933 RID: 18739 RVA: 0x0012C2D8 File Offset: 0x0012A4D8
	public bool manUp
	{
		get
		{
			return this.manualPanelUpdate;
		}
	}

	// Token: 0x06004934 RID: 18740 RVA: 0x0012C2E0 File Offset: 0x0012A4E0
	private UINode GetNode(Transform t)
	{
		UINode result = null;
		if (t != null && this.mChildren.Contains(t))
		{
			result = (UINode)this.mChildren[t];
		}
		return result;
	}

	// Token: 0x06004935 RID: 18741 RVA: 0x0012C320 File Offset: 0x0012A520
	private bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		this.UpdateTransformMatrix();
		a = this.mWorldToLocal.MultiplyPoint3x4(a);
		b = this.mWorldToLocal.MultiplyPoint3x4(b);
		c = this.mWorldToLocal.MultiplyPoint3x4(c);
		d = this.mWorldToLocal.MultiplyPoint3x4(d);
		UIPanel.mTemp[0] = a.x;
		UIPanel.mTemp[1] = b.x;
		UIPanel.mTemp[2] = c.x;
		UIPanel.mTemp[3] = d.x;
		float num = Mathf.Min(UIPanel.mTemp);
		float num2 = Mathf.Max(UIPanel.mTemp);
		UIPanel.mTemp[0] = a.y;
		UIPanel.mTemp[1] = b.y;
		UIPanel.mTemp[2] = c.y;
		UIPanel.mTemp[3] = d.y;
		float num3 = Mathf.Min(UIPanel.mTemp);
		float num4 = Mathf.Max(UIPanel.mTemp);
		return num2 >= this.mMin.x && num4 >= this.mMin.y && num <= this.mMax.x && num3 <= this.mMax.y;
	}

	// Token: 0x06004936 RID: 18742 RVA: 0x0012C458 File Offset: 0x0012A658
	public bool IsVisible(UIWidget w)
	{
		if (!w.enabled || !w.gameObject.activeInHierarchy || w.color.a < 0.001f)
		{
			return false;
		}
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			return true;
		}
		Vector2 relativeSize = w.relativeSize;
		Vector2 vector = Vector2.Scale(w.pivotOffset, relativeSize);
		Vector2 vector2 = vector;
		vector.x += relativeSize.x;
		vector.y -= relativeSize.y;
		Transform cachedTransform = w.cachedTransform;
		Vector3 a = cachedTransform.TransformPoint(vector);
		Vector3 b = cachedTransform.TransformPoint(new Vector2(vector.x, vector2.y));
		Vector3 c = cachedTransform.TransformPoint(new Vector2(vector2.x, vector.y));
		Vector3 d = cachedTransform.TransformPoint(vector2);
		return this.IsVisible(a, b, c, d);
	}

	// Token: 0x06004937 RID: 18743 RVA: 0x0012C55C File Offset: 0x0012A75C
	public void MarkMaterialAsChanged(UIMaterial mat, bool sort)
	{
		if (mat)
		{
			if (sort)
			{
				this.mDepthChanged = true;
			}
			if (this.mChanged.Add(mat))
			{
				this.mChangedLastFrame = true;
			}
		}
	}

	// Token: 0x06004938 RID: 18744 RVA: 0x0012C59C File Offset: 0x0012A79C
	public bool WatchesTransform(Transform t)
	{
		return t == this.cachedTransform || this.mChildren.Contains(t);
	}

	// Token: 0x06004939 RID: 18745 RVA: 0x0012C5CC File Offset: 0x0012A7CC
	private UINode AddTransform(Transform t)
	{
		UINode uinode = null;
		while (t != null && t != this.cachedTransform)
		{
			if (this.mChildren.Contains(t))
			{
				if (uinode == null)
				{
					uinode = (UINode)this.mChildren[t];
				}
				break;
			}
			UINode uinode2 = new UINode(t);
			if (uinode == null)
			{
				uinode = uinode2;
			}
			this.mChildren.Add(t, uinode2);
			t = t.parent;
		}
		return uinode;
	}

	// Token: 0x0600493A RID: 18746 RVA: 0x0012C654 File Offset: 0x0012A854
	private void RemoveTransform(Transform t)
	{
		if (t != null)
		{
			while (this.mChildren.Contains(t))
			{
				this.mChildren.Remove(t);
				t = t.parent;
				if (t == null || t == this.mTrans || t.childCount > 1)
				{
					break;
				}
			}
		}
	}

	// Token: 0x0600493B RID: 18747 RVA: 0x0012C6C8 File Offset: 0x0012A8C8
	public void AddWidget(UIWidget w)
	{
		if (w != null)
		{
			UINode uinode = this.AddTransform(w.cachedTransform);
			if (uinode != null)
			{
				uinode.widget = w;
				if (!this.mWidgets.Contains(w))
				{
					this.mWidgets.Add(w);
					if (!this.mChanged.Contains(w.material))
					{
						this.mChanged.Add(w.material);
						this.mChangedLastFrame = true;
					}
					this.mDepthChanged = true;
				}
			}
			else
			{
				Debug.LogError("Unable to find an appropriate UIRoot for " + NGUITools.GetHierarchy(w.gameObject) + "\nPlease make sure that there is at least one game object above this widget!", w.gameObject);
			}
		}
	}

	// Token: 0x0600493C RID: 18748 RVA: 0x0012C778 File Offset: 0x0012A978
	public void RemoveWidget(UIWidget w)
	{
		if (w != null)
		{
			UINode node = this.GetNode(w.cachedTransform);
			if (node != null)
			{
				if (node.visibleFlag == 1 && !this.mChanged.Contains(w.material))
				{
					this.mChanged.Add(w.material);
					this.mChangedLastFrame = true;
				}
				this.RemoveTransform(w.cachedTransform);
			}
			this.mWidgets.Remove(w);
		}
	}

	// Token: 0x0600493D RID: 18749 RVA: 0x0012C7F8 File Offset: 0x0012A9F8
	private UIDrawCall.Iterator GetDrawCall(UIMaterial mat, bool createIfMissing)
	{
		UIDrawCall.Iterator result = (UIDrawCall.Iterator)this.mDrawCalls;
		while (result.Has)
		{
			if (result.Current.material == mat)
			{
				return result;
			}
			result = result.Next;
		}
		UIDrawCall uidrawCall = null;
		if (createIfMissing)
		{
			uidrawCall = new GameObject("_UIDrawCall [" + mat.name + "]")
			{
				hideFlags = 4,
				layer = base.gameObject.layer
			}.AddComponent<UIDrawCall>();
			uidrawCall.material = mat;
			uidrawCall.LinkedList__Insert(ref this.mDrawCalls);
			this.mDrawCallCount++;
		}
		return (UIDrawCall.Iterator)uidrawCall;
	}

	// Token: 0x0600493E RID: 18750 RVA: 0x0012C8AC File Offset: 0x0012AAAC
	protected void Awake()
	{
		UIPanel.Global.RegisterPanel(this);
	}

	// Token: 0x0600493F RID: 18751 RVA: 0x0012C8B4 File Offset: 0x0012AAB4
	protected void Start()
	{
		this.mLayer = base.gameObject.layer;
		UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
		this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
	}

	// Token: 0x06004940 RID: 18752 RVA: 0x0012C908 File Offset: 0x0012AB08
	protected void OnEnable()
	{
		UIPanel.Global.PanelEnabled(this);
		if (this.mHotSpots != null)
		{
			foreach (UIHotSpot uihotSpot in this.mHotSpots)
			{
				uihotSpot.OnPanelEnable();
			}
		}
		int i = 0;
		int count = this.mWidgets.Count;
		while (i < count)
		{
			this.AddWidget(this.mWidgets[i]);
			i++;
		}
		this.mRebuildAll = true;
	}

	// Token: 0x06004941 RID: 18753 RVA: 0x0012C9B8 File Offset: 0x0012ABB8
	protected void OnDisable()
	{
		UIPanel.Global.PanelDisabled(this);
		if (this.mHotSpots != null)
		{
			foreach (UIHotSpot uihotSpot in this.mHotSpots)
			{
				uihotSpot.OnPanelDisable();
			}
		}
		UIDrawCall.Iterator iterator = (UIDrawCall.Iterator)this.mDrawCalls;
		while (iterator.Has)
		{
			UIDrawCall current = iterator.Current;
			iterator = iterator.Next;
			NGUITools.DestroyImmediate(current.gameObject);
		}
		this.mDrawCalls = null;
		this.mChanged.Clear();
		this.mChildren.Clear();
	}

	// Token: 0x06004942 RID: 18754 RVA: 0x0012CA84 File Offset: 0x0012AC84
	protected void OnDestroy()
	{
		UIPanel.Global.UnregisterPanel(this);
		if (this.mHotSpots != null)
		{
			HashSet<UIHotSpot> hashSet = this.mHotSpots;
			this.mHotSpots = null;
			foreach (UIHotSpot uihotSpot in hashSet)
			{
				uihotSpot.OnPanelDestroy();
			}
		}
	}

	// Token: 0x06004943 RID: 18755 RVA: 0x0012CB04 File Offset: 0x0012AD04
	private int GetChangeFlag(UINode start)
	{
		int num = start.changeFlag;
		if (num == -1)
		{
			Transform parent = start.trans.parent;
			while (this.mChildren.Contains(parent))
			{
				UINode uinode = (UINode)this.mChildren[parent];
				num = uinode.changeFlag;
				parent = parent.parent;
				if (num != -1)
				{
					IL_7D:
					int i = 0;
					int count = UIPanel.mHierarchy.Count;
					while (i < count)
					{
						UINode uinode2 = UIPanel.mHierarchy[i];
						uinode2.changeFlag = num;
						i++;
					}
					UIPanel.mHierarchy.Clear();
					return num;
				}
				UIPanel.mHierarchy.Add(uinode);
			}
			num = 0;
			goto IL_7D;
		}
		return num;
	}

	// Token: 0x06004944 RID: 18756 RVA: 0x0012CBD0 File Offset: 0x0012ADD0
	private void UpdateTransformMatrix()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup == 0f || this.mMatrixTime != realtimeSinceStartup)
		{
			this.mMatrixTime = realtimeSinceStartup;
			this.mWorldToLocal = this.cachedTransform.worldToLocalMatrix;
			if (this.mClipping != UIDrawCall.Clipping.None)
			{
				Vector2 vector;
				vector..ctor(this.mClipRange.z, this.mClipRange.w);
				if (vector.x == 0f)
				{
					vector.x = ((!(this.mCam == null)) ? this.mCam.pixelWidth : ((float)Screen.width));
				}
				if (vector.y == 0f)
				{
					vector.y = ((!(this.mCam == null)) ? this.mCam.pixelHeight : ((float)Screen.height));
				}
				vector *= 0.5f;
				this.mMin.x = this.mClipRange.x - vector.x;
				this.mMin.y = this.mClipRange.y - vector.y;
				this.mMax.x = this.mClipRange.x + vector.x;
				this.mMax.y = this.mClipRange.y + vector.y;
			}
		}
	}

	// Token: 0x06004945 RID: 18757 RVA: 0x0012CD3C File Offset: 0x0012AF3C
	private void UpdateTransforms()
	{
		this.mChangedLastFrame = false;
		bool flag = false;
		bool flag2 = Time.realtimeSinceStartup > this.mCullTime;
		if (!this.widgetsAreStatic || flag2 != this.mCulled)
		{
			int i = 0;
			int count = this.mChildren.Count;
			while (i < count)
			{
				UINode uinode = (UINode)this.mChildren[i];
				if (uinode.trans == null)
				{
					this.mRemoved.Add(uinode.trans);
				}
				else if (uinode.HasChanged())
				{
					uinode.changeFlag = 1;
					flag = true;
				}
				else
				{
					uinode.changeFlag = -1;
				}
				i++;
			}
			int j = 0;
			int count2 = this.mRemoved.Count;
			while (j < count2)
			{
				this.mChildren.Remove(this.mRemoved[j]);
				j++;
			}
			this.mRemoved.Clear();
		}
		if (!this.mCulled && flag2)
		{
			this.mCheckVisibility = true;
		}
		if (this.mCheckVisibility || flag || this.mRebuildAll)
		{
			int k = 0;
			int count3 = this.mChildren.Count;
			while (k < count3)
			{
				UINode uinode2 = (UINode)this.mChildren[k];
				if (uinode2.widget != null)
				{
					int num = 1;
					if (flag2 || flag)
					{
						if (uinode2.changeFlag == -1)
						{
							uinode2.changeFlag = this.GetChangeFlag(uinode2);
						}
						if (flag2)
						{
							num = ((!this.mCheckVisibility && uinode2.changeFlag != 1) ? uinode2.visibleFlag : ((!this.IsVisible(uinode2.widget)) ? 0 : 1));
						}
					}
					if (uinode2.visibleFlag != num)
					{
						uinode2.changeFlag = 1;
					}
					if (uinode2.changeFlag == 1 && (num == 1 || uinode2.visibleFlag != 0))
					{
						uinode2.visibleFlag = num;
						UIMaterial material = uinode2.widget.material;
						if (!this.mChanged.Contains(material))
						{
							this.mChanged.Add(material);
							this.mChangedLastFrame = true;
						}
					}
				}
				k++;
			}
		}
		this.mCulled = flag2;
		this.mCheckVisibility = false;
	}

	// Token: 0x06004946 RID: 18758 RVA: 0x0012CFAC File Offset: 0x0012B1AC
	private void UpdateWidgets()
	{
		int i = 0;
		int count = this.mChildren.Count;
		while (i < count)
		{
			UINode uinode = (UINode)this.mChildren[i];
			UIWidget widget = uinode.widget;
			if (uinode.visibleFlag == 1 && widget != null && widget.UpdateGeometry(ref this.mWorldToLocal, uinode.changeFlag == 1, this.generateNormals) && !this.mChanged.Contains(widget.material))
			{
				this.mChanged.Add(widget.material);
				this.mChangedLastFrame = true;
			}
			uinode.changeFlag = 0;
			i++;
		}
	}

	// Token: 0x06004947 RID: 18759 RVA: 0x0012D060 File Offset: 0x0012B260
	public void UpdateDrawcalls()
	{
		Vector4 zero = Vector4.zero;
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			zero..ctor(this.mClipRange.x, this.mClipRange.y, this.mClipRange.z * 0.5f, this.mClipRange.w * 0.5f);
		}
		if (zero.z == 0f)
		{
			zero.z = (float)Screen.width * 0.5f;
		}
		if (zero.w == 0f)
		{
			zero.w = (float)Screen.height * 0.5f;
		}
		RuntimePlatform platform = Application.platform;
		if (platform == 2 || platform == 5 || platform == 7)
		{
			zero.x -= 0.5f;
			zero.y += 0.5f;
		}
		Vector3 position = this.cachedTransform.position;
		Quaternion rotation = this.cachedTransform.rotation;
		Vector3 lossyScale = this.cachedTransform.lossyScale;
		UIDrawCall.Iterator iterator = (UIDrawCall.Iterator)this.mDrawCalls;
		while (iterator.Has)
		{
			UIDrawCall current = iterator.Current;
			iterator = iterator.Next;
			current.clipping = this.mClipping;
			current.clipRange = zero;
			current.clipSoftness = this.mClipSoftness;
			current.depthPass = this.depthPass;
			current.panelPropertyBlock = this.propertyBlock;
			Transform transform = current.transform;
			transform.position = position;
			transform.rotation = rotation;
			transform.localScale = lossyScale;
		}
	}

	// Token: 0x06004948 RID: 18760 RVA: 0x0012D1FC File Offset: 0x0012B3FC
	private void Fill(UIMaterial mat)
	{
		int i = this.mWidgets.Count;
		while (i > 0)
		{
			if (this.mWidgets[--i] == null)
			{
				this.mWidgets.RemoveAt(i);
			}
		}
		int j = 0;
		int count = this.mWidgets.Count;
		while (j < count)
		{
			UIWidget uiwidget = this.mWidgets[j];
			if (uiwidget.visibleFlag == 1 && uiwidget.material == mat)
			{
				UINode node = this.GetNode(uiwidget.cachedTransform);
				if (node != null)
				{
					uiwidget.WriteToBuffers(this.mCacheBuffer);
				}
				else
				{
					Debug.LogError("No transform found for " + NGUITools.GetHierarchy(uiwidget.gameObject), this);
				}
			}
			j++;
		}
		if (this.mCacheBuffer.vSize > 0)
		{
			UIDrawCall current = this.GetDrawCall(mat, true).Current;
			current.depthPass = this.depthPass;
			current.panelPropertyBlock = this.propertyBlock;
			current.Set(this.mCacheBuffer);
		}
		else
		{
			UIDrawCall.Iterator drawCall = this.GetDrawCall(mat, false);
			if (drawCall.Has)
			{
				this.Delete(ref drawCall);
			}
		}
		this.mCacheBuffer.Clear();
	}

	// Token: 0x06004949 RID: 18761 RVA: 0x0012D34C File Offset: 0x0012B54C
	private void Delete(ref UIDrawCall.Iterator iter)
	{
		if (iter.Has)
		{
			UIDrawCall current = iter.Current;
			if (object.ReferenceEquals(current, this.mDrawCalls))
			{
				this.mDrawCalls = iter.Next.Current;
			}
			iter = iter.Next;
			current.LinkedList__Remove();
			this.mDrawCallCount--;
			NGUITools.DestroyImmediate(current.gameObject);
		}
	}

	// Token: 0x0600494A RID: 18762 RVA: 0x0012D3BC File Offset: 0x0012B5BC
	private void PanelUpdate(bool letFill)
	{
		this.UpdateTransformMatrix();
		this.UpdateTransforms();
		if (this.mLayer != base.gameObject.layer)
		{
			this.mLayer = base.gameObject.layer;
			UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
			this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
			UIPanel.SetChildLayer(this.cachedTransform, this.mLayer);
			UIDrawCall.Iterator iterator = (UIDrawCall.Iterator)this.mDrawCalls;
			while (iterator.Has)
			{
				iterator.Current.gameObject.layer = this.mLayer;
				iterator = iterator.Next;
			}
		}
		this.UpdateWidgets();
		if (this.mDepthChanged)
		{
			this.mDepthChanged = false;
			this.mWidgets.Sort(new Comparison<UIWidget>(UIWidget.CompareFunc));
		}
		if (letFill)
		{
			this.FillUpdate();
		}
		else
		{
			this.UpdateDrawcalls();
		}
		this.mRebuildAll = false;
	}

	// Token: 0x0600494B RID: 18763 RVA: 0x0012D4CC File Offset: 0x0012B6CC
	public bool Contains(UIDrawCall drawcall)
	{
		UIDrawCall.Iterator iterator = (UIDrawCall.Iterator)this.mDrawCalls;
		while (iterator.Has)
		{
			if (object.ReferenceEquals(drawcall, iterator.Current))
			{
				return true;
			}
			iterator = iterator.Next;
		}
		return false;
	}

	// Token: 0x0600494C RID: 18764 RVA: 0x0012D514 File Offset: 0x0012B714
	private void FillUpdate()
	{
		foreach (UIMaterial mat in this.mChanged)
		{
			this.Fill(mat);
		}
		this.UpdateDrawcalls();
		this.mChanged.Clear();
	}

	// Token: 0x0600494D RID: 18765 RVA: 0x0012D58C File Offset: 0x0012B78C
	private void DefaultLateUpdate()
	{
		if (!this.manualPanelUpdate)
		{
			this.PanelUpdate(true);
		}
		else
		{
			this.FillUpdate();
		}
	}

	// Token: 0x0600494E RID: 18766 RVA: 0x0012D5AC File Offset: 0x0012B7AC
	public bool ManualPanelUpdate()
	{
		if (this.manualPanelUpdate && base.enabled)
		{
			this.PanelUpdate(false);
			return true;
		}
		return false;
	}

	// Token: 0x0600494F RID: 18767 RVA: 0x0012D5DC File Offset: 0x0012B7DC
	public void Refresh()
	{
		base.BroadcastMessage("Update", 1);
		this.DefaultLateUpdate();
	}

	// Token: 0x06004950 RID: 18768 RVA: 0x0012D5F0 File Offset: 0x0012B7F0
	private static bool CheckRayEnterClippingRect(Ray ray, Transform transform, Vector4 clipRange)
	{
		Plane plane;
		plane..ctor(transform.forward, transform.position);
		float num;
		if (plane.Raycast(ray, ref num))
		{
			Vector3 vector = transform.InverseTransformPoint(ray.GetPoint(num));
			clipRange.z = Mathf.Abs(clipRange.z);
			clipRange.w = Mathf.Abs(clipRange.w);
			Rect rect;
			rect..ctor(clipRange.x - clipRange.z / 2f, clipRange.y - clipRange.w / 2f, clipRange.z, clipRange.w);
			return rect.Contains(vector);
		}
		return false;
	}

	// Token: 0x06004951 RID: 18769 RVA: 0x0012D6A0 File Offset: 0x0012B8A0
	internal bool InsideClippingRect(Ray ray, int traceID)
	{
		if (this.clipping == UIDrawCall.Clipping.None)
		{
			return true;
		}
		if (traceID != this.traceID || ray.origin != this.lastRayTrace.origin || ray.direction != this.lastRayTrace.direction)
		{
			this.traceID = traceID;
			this.lastRayTrace = ray;
			this.lastRayTraceInside = UIPanel.CheckRayEnterClippingRect(ray, base.transform, this.clipRange);
		}
		return this.lastRayTraceInside;
	}

	// Token: 0x06004952 RID: 18770 RVA: 0x0012D72C File Offset: 0x0012B92C
	public Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		float num = this.clipRange.z * 0.5f;
		float num2 = this.clipRange.w * 0.5f;
		Vector2 minRect;
		minRect..ctor(min.x, min.y);
		Vector2 maxRect;
		maxRect..ctor(max.x, max.y);
		Vector2 minArea;
		minArea..ctor(this.clipRange.x - num, this.clipRange.y - num2);
		Vector2 maxArea;
		maxArea..ctor(this.clipRange.x + num, this.clipRange.y + num2);
		if (this.clipping == UIDrawCall.Clipping.SoftClip)
		{
			minArea.x += this.clipSoftness.x;
			minArea.y += this.clipSoftness.y;
			maxArea.x -= this.clipSoftness.x;
			maxArea.y -= this.clipSoftness.y;
		}
		return NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea);
	}

	// Token: 0x06004953 RID: 18771 RVA: 0x0012D874 File Offset: 0x0012BA74
	public bool ConstrainTargetToBounds(Transform target, ref AABBox targetBounds, bool immediate)
	{
		Vector3 vector = this.CalculateConstrainOffset(targetBounds.min, targetBounds.max);
		if (vector.magnitude > 0f)
		{
			if (immediate)
			{
				target.localPosition += vector;
				targetBounds.center += vector;
				SpringPosition component = target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(target.gameObject, target.localPosition + vector, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06004954 RID: 18772 RVA: 0x0012D928 File Offset: 0x0012BB28
	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		AABBox aabbox = NGUIMath.CalculateRelativeWidgetBounds(this.cachedTransform, target);
		return this.ConstrainTargetToBounds(target, ref aabbox, immediate);
	}

	// Token: 0x06004955 RID: 18773 RVA: 0x0012D94C File Offset: 0x0012BB4C
	private static void SetChildLayer(Transform t, int layer)
	{
		for (int i = 0; i < t.childCount; i++)
		{
			Transform child = t.GetChild(i);
			if (child.GetComponent<UIPanel>() == null)
			{
				child.gameObject.layer = layer;
				UIPanel.SetChildLayer(child, layer);
			}
		}
	}

	// Token: 0x06004956 RID: 18774 RVA: 0x0012D99C File Offset: 0x0012BB9C
	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		Transform transform = trans;
		UIPanel uipanel = null;
		while (uipanel == null && trans != null)
		{
			uipanel = trans.GetComponent<UIPanel>();
			if (uipanel != null)
			{
				break;
			}
			if (trans.parent == null)
			{
				break;
			}
			trans = trans.parent;
		}
		if (createIfMissing && uipanel == null && trans != transform)
		{
			uipanel = trans.gameObject.AddComponent<UIPanel>();
			UIPanel.SetChildLayer(uipanel.cachedTransform, uipanel.gameObject.layer);
		}
		return uipanel;
	}

	// Token: 0x06004957 RID: 18775 RVA: 0x0012DA44 File Offset: 0x0012BC44
	public static UIPanel Find(Transform trans)
	{
		return UIPanel.Find(trans, true);
	}

	// Token: 0x06004958 RID: 18776 RVA: 0x0012DA50 File Offset: 0x0012BC50
	public static UIPanel FindRoot(Transform trans)
	{
		UIPanel uipanel = UIPanel.Find(trans);
		return (!uipanel) ? null : uipanel.RootPanel;
	}

	// Token: 0x06004959 RID: 18777 RVA: 0x0012DA7C File Offset: 0x0012BC7C
	internal static void RegisterHotSpot(UIPanel panel, UIHotSpot hotSpot)
	{
		if (panel.mHotSpots == null)
		{
			panel.mHotSpots = new HashSet<UIHotSpot>();
		}
		if (panel.mHotSpots.Add(hotSpot))
		{
			if (panel.enabled)
			{
				hotSpot.OnPanelEnable();
			}
			else
			{
				hotSpot.OnPanelDisable();
			}
		}
	}

	// Token: 0x0600495A RID: 18778 RVA: 0x0012DACC File Offset: 0x0012BCCC
	internal static void UnregisterHotSpot(UIPanel panel, UIHotSpot hotSpot)
	{
		if (panel.mHotSpots == null || !panel.mHotSpots.Remove(hotSpot))
		{
			return;
		}
		if (panel.enabled)
		{
			hotSpot.OnPanelDisable();
		}
	}

	// Token: 0x17000E51 RID: 3665
	// (get) Token: 0x0600495B RID: 18779 RVA: 0x0012DB08 File Offset: 0x0012BD08
	// (set) Token: 0x0600495C RID: 18780 RVA: 0x0012DB10 File Offset: 0x0012BD10
	public UIPanelMaterialPropertyBlock propertyBlock
	{
		get
		{
			return this._propertyBlock;
		}
		set
		{
			this._propertyBlock = value;
		}
	}

	// Token: 0x04002950 RID: 10576
	[SerializeField]
	private UIPanel _rootPanel;

	// Token: 0x04002951 RID: 10577
	public bool showInPanelTool = true;

	// Token: 0x04002952 RID: 10578
	public bool generateNormals;

	// Token: 0x04002953 RID: 10579
	public bool depthPass;

	// Token: 0x04002954 RID: 10580
	public bool widgetsAreStatic;

	// Token: 0x04002955 RID: 10581
	[SerializeField]
	[HideInInspector]
	private UIPanel.DebugInfo mDebugInfo = UIPanel.DebugInfo.Gizmos;

	// Token: 0x04002956 RID: 10582
	[SerializeField]
	[HideInInspector]
	private UIDrawCall.Clipping mClipping;

	// Token: 0x04002957 RID: 10583
	[SerializeField]
	[HideInInspector]
	private Vector4 mClipRange = Vector4.zero;

	// Token: 0x04002958 RID: 10584
	[HideInInspector]
	[SerializeField]
	private Vector2 mClipSoftness = new Vector2(40f, 40f);

	// Token: 0x04002959 RID: 10585
	[HideInInspector]
	[SerializeField]
	private bool manualPanelUpdate;

	// Token: 0x0400295A RID: 10586
	private OrderedDictionary mChildren = new OrderedDictionary();

	// Token: 0x0400295B RID: 10587
	private List<UIWidget> mWidgets = new List<UIWidget>();

	// Token: 0x0400295C RID: 10588
	private HashSet<UIMaterial> mChanged = new HashSet<UIMaterial>();

	// Token: 0x0400295D RID: 10589
	private UIDrawCall mDrawCalls;

	// Token: 0x0400295E RID: 10590
	private int mDrawCallCount;

	// Token: 0x0400295F RID: 10591
	private MeshBuffer mCacheBuffer = new MeshBuffer();

	// Token: 0x04002960 RID: 10592
	private HashSet<UIHotSpot> mHotSpots;

	// Token: 0x04002961 RID: 10593
	private Transform mTrans;

	// Token: 0x04002962 RID: 10594
	private Camera mCam;

	// Token: 0x04002963 RID: 10595
	private int mLayer = -1;

	// Token: 0x04002964 RID: 10596
	private bool mDepthChanged;

	// Token: 0x04002965 RID: 10597
	private bool mRebuildAll;

	// Token: 0x04002966 RID: 10598
	private bool mChangedLastFrame;

	// Token: 0x04002967 RID: 10599
	private float mMatrixTime;

	// Token: 0x04002968 RID: 10600
	private Matrix4x4 mWorldToLocal = Matrix4x4.identity;

	// Token: 0x04002969 RID: 10601
	private static float[] mTemp = new float[4];

	// Token: 0x0400296A RID: 10602
	private Vector2 mMin = Vector2.zero;

	// Token: 0x0400296B RID: 10603
	private Vector2 mMax = Vector2.zero;

	// Token: 0x0400296C RID: 10604
	private List<Transform> mRemoved = new List<Transform>();

	// Token: 0x0400296D RID: 10605
	private bool mCheckVisibility;

	// Token: 0x0400296E RID: 10606
	private float mCullTime;

	// Token: 0x0400296F RID: 10607
	private bool mCulled;

	// Token: 0x04002970 RID: 10608
	private int globalIndex = -1;

	// Token: 0x04002971 RID: 10609
	private static List<UINode> mHierarchy = new List<UINode>();

	// Token: 0x04002972 RID: 10610
	private int traceID;

	// Token: 0x04002973 RID: 10611
	private Ray lastRayTrace;

	// Token: 0x04002974 RID: 10612
	private bool lastRayTraceInside;

	// Token: 0x04002975 RID: 10613
	[NonSerialized]
	private UIPanelMaterialPropertyBlock _propertyBlock;

	// Token: 0x020007F9 RID: 2041
	private static class Global
	{
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x0012DB1C File Offset: 0x0012BD1C
		public static bool noGlobal
		{
			get
			{
				return !Application.isPlaying;
			}
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x0012DB28 File Offset: 0x0012BD28
		public static void RegisterPanel(UIPanel panel)
		{
			if (UIPanel.Global.noGlobal)
			{
				return;
			}
			UIGlobal.EnsureGlobal();
			if (panel.globalIndex == -1)
			{
				panel.globalIndex = UIPanel.Global.g.allPanels.Count;
				UIPanel.Global.g.allPanels.Add(panel);
			}
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x0012DB64 File Offset: 0x0012BD64
		public static void UnregisterPanel(UIPanel panel)
		{
			if (UIPanel.Global.noGlobal)
			{
				return;
			}
			if (panel.globalIndex != -1)
			{
				UIPanel.Global.g.allPanels.RemoveAt(panel.globalIndex);
				int i = panel.globalIndex;
				int count = UIPanel.Global.g.allPanels.Count;
				while (i < count)
				{
					UIPanel.Global.g.allPanels[i].globalIndex = i;
					i++;
				}
				panel.globalIndex = -1;
			}
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x0012DBD4 File Offset: 0x0012BDD4
		public static void PanelEnabled(UIPanel panel)
		{
			if (UIPanel.Global.noGlobal)
			{
				return;
			}
			UIPanel.Global.g.allEnabled.Add(panel);
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x0012DBF0 File Offset: 0x0012BDF0
		public static void PanelDisabled(UIPanel panel)
		{
			if (UIPanel.Global.noGlobal)
			{
				return;
			}
			UIPanel.Global.g.allEnabled.Remove(panel);
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x0012DC0C File Offset: 0x0012BE0C
		public static void PanelUpdate()
		{
			if (UIPanel.Global.noGlobal)
			{
				return;
			}
			try
			{
				UIPanel.Global.g.allEnableSwap.AddRange(UIPanel.Global.g.allEnabled);
				foreach (UIPanel uipanel in UIPanel.Global.g.allEnableSwap)
				{
					if (uipanel && uipanel.enabled)
					{
						uipanel.DefaultLateUpdate();
					}
				}
			}
			finally
			{
				UIPanel.Global.g.allEnableSwap.Clear();
			}
		}

		// Token: 0x020007FA RID: 2042
		private static class g
		{
			// Token: 0x06004963 RID: 18787 RVA: 0x0012DCC8 File Offset: 0x0012BEC8
			static g()
			{
				UIGlobal.EnsureGlobal();
			}

			// Token: 0x04002976 RID: 10614
			public static HashSet<UIPanel> allEnabled = new HashSet<UIPanel>();

			// Token: 0x04002977 RID: 10615
			public static List<UIPanel> allEnableSwap = new List<UIPanel>();

			// Token: 0x04002978 RID: 10616
			public static List<UIPanel> allPanels = new List<UIPanel>();
		}
	}

	// Token: 0x020007FB RID: 2043
	public enum DebugInfo
	{
		// Token: 0x0400297A RID: 10618
		None,
		// Token: 0x0400297B RID: 10619
		Gizmos,
		// Token: 0x0400297C RID: 10620
		Geometry
	}
}
