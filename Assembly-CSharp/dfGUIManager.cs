using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000797 RID: 1943
[RequireComponent(typeof(global::dfInputManager))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/GUI Manager")]
[Serializable]
public class dfGUIManager : MonoBehaviour
{
	// Token: 0x14000049 RID: 73
	// (add) Token: 0x06004136 RID: 16694 RVA: 0x000EED18 File Offset: 0x000ECF18
	// (remove) Token: 0x06004137 RID: 16695 RVA: 0x000EED30 File Offset: 0x000ECF30
	public static event global::dfGUIManager.RenderCallback BeforeRender;

	// Token: 0x1400004A RID: 74
	// (add) Token: 0x06004138 RID: 16696 RVA: 0x000EED48 File Offset: 0x000ECF48
	// (remove) Token: 0x06004139 RID: 16697 RVA: 0x000EED60 File Offset: 0x000ECF60
	public static event global::dfGUIManager.RenderCallback AfterRender;

	// Token: 0x17000C79 RID: 3193
	// (get) Token: 0x0600413A RID: 16698 RVA: 0x000EED78 File Offset: 0x000ECF78
	// (set) Token: 0x0600413B RID: 16699 RVA: 0x000EED80 File Offset: 0x000ECF80
	public int TotalDrawCalls { get; private set; }

	// Token: 0x17000C7A RID: 3194
	// (get) Token: 0x0600413C RID: 16700 RVA: 0x000EED8C File Offset: 0x000ECF8C
	// (set) Token: 0x0600413D RID: 16701 RVA: 0x000EED94 File Offset: 0x000ECF94
	public int TotalTriangles { get; private set; }

	// Token: 0x17000C7B RID: 3195
	// (get) Token: 0x0600413E RID: 16702 RVA: 0x000EEDA0 File Offset: 0x000ECFA0
	// (set) Token: 0x0600413F RID: 16703 RVA: 0x000EEDA8 File Offset: 0x000ECFA8
	public int ControlsRendered { get; private set; }

	// Token: 0x17000C7C RID: 3196
	// (get) Token: 0x06004140 RID: 16704 RVA: 0x000EEDB4 File Offset: 0x000ECFB4
	// (set) Token: 0x06004141 RID: 16705 RVA: 0x000EEDBC File Offset: 0x000ECFBC
	public int FramesRendered { get; private set; }

	// Token: 0x17000C7D RID: 3197
	// (get) Token: 0x06004142 RID: 16706 RVA: 0x000EEDC8 File Offset: 0x000ECFC8
	public static global::dfControl ActiveControl
	{
		get
		{
			return global::dfGUIManager.activeControl;
		}
	}

	// Token: 0x17000C7E RID: 3198
	// (get) Token: 0x06004143 RID: 16707 RVA: 0x000EEDD0 File Offset: 0x000ECFD0
	// (set) Token: 0x06004144 RID: 16708 RVA: 0x000EEDD8 File Offset: 0x000ECFD8
	public float UIScale
	{
		get
		{
			return this.uiScale;
		}
		set
		{
			if (!Mathf.Approximately(value, this.uiScale))
			{
				this.uiScale = value;
				this.onResolutionChanged();
			}
		}
	}

	// Token: 0x17000C7F RID: 3199
	// (get) Token: 0x06004145 RID: 16709 RVA: 0x000EEDF8 File Offset: 0x000ECFF8
	// (set) Token: 0x06004146 RID: 16710 RVA: 0x000EEE00 File Offset: 0x000ED000
	public Vector2 UIOffset
	{
		get
		{
			return this.uiOffset;
		}
		set
		{
			if (!object.Equals(this.uiOffset, value))
			{
				this.uiOffset = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C80 RID: 3200
	// (get) Token: 0x06004147 RID: 16711 RVA: 0x000EEE38 File Offset: 0x000ED038
	// (set) Token: 0x06004148 RID: 16712 RVA: 0x000EEE40 File Offset: 0x000ED040
	public Camera RenderCamera
	{
		get
		{
			return this.renderCamera;
		}
		set
		{
			if (!object.ReferenceEquals(this.renderCamera, value))
			{
				this.renderCamera = value;
				this.Invalidate();
				if (value != null && value.gameObject.GetComponent<global::dfGUICamera>() == null)
				{
					value.gameObject.AddComponent<global::dfGUICamera>();
				}
				if (this.inputManager != null)
				{
					this.inputManager.RenderCamera = value;
				}
			}
		}
	}

	// Token: 0x17000C81 RID: 3201
	// (get) Token: 0x06004149 RID: 16713 RVA: 0x000EEEB8 File Offset: 0x000ED0B8
	// (set) Token: 0x0600414A RID: 16714 RVA: 0x000EEEC0 File Offset: 0x000ED0C0
	public bool MergeMaterials
	{
		get
		{
			return this.mergeMaterials;
		}
		set
		{
			if (value != this.mergeMaterials)
			{
				this.mergeMaterials = value;
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x17000C82 RID: 3202
	// (get) Token: 0x0600414B RID: 16715 RVA: 0x000EEEDC File Offset: 0x000ED0DC
	// (set) Token: 0x0600414C RID: 16716 RVA: 0x000EEEE4 File Offset: 0x000ED0E4
	public bool GenerateNormals
	{
		get
		{
			return this.generateNormals;
		}
		set
		{
			if (value != this.generateNormals)
			{
				this.generateNormals = value;
				if (this.renderMesh != null)
				{
					this.renderMesh[0].Clear();
					this.renderMesh[1].Clear();
				}
				global::dfRenderData.FlushObjectPool();
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x17000C83 RID: 3203
	// (get) Token: 0x0600414D RID: 16717 RVA: 0x000EEF34 File Offset: 0x000ED134
	// (set) Token: 0x0600414E RID: 16718 RVA: 0x000EEF3C File Offset: 0x000ED13C
	public bool PixelPerfectMode
	{
		get
		{
			return this.pixelPerfectMode;
		}
		set
		{
			if (value != this.pixelPerfectMode)
			{
				this.pixelPerfectMode = value;
				this.onResolutionChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C84 RID: 3204
	// (get) Token: 0x0600414F RID: 16719 RVA: 0x000EEF60 File Offset: 0x000ED160
	// (set) Token: 0x06004150 RID: 16720 RVA: 0x000EEF68 File Offset: 0x000ED168
	public global::dfAtlas DefaultAtlas
	{
		get
		{
			return this.atlas;
		}
		set
		{
			if (!global::dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x17000C85 RID: 3205
	// (get) Token: 0x06004151 RID: 16721 RVA: 0x000EEF88 File Offset: 0x000ED188
	// (set) Token: 0x06004152 RID: 16722 RVA: 0x000EEF90 File Offset: 0x000ED190
	public global::dfFont DefaultFont
	{
		get
		{
			return this.defaultFont;
		}
		set
		{
			if (value != this.defaultFont)
			{
				this.defaultFont = value;
				this.invalidateAllControls();
			}
		}
	}

	// Token: 0x17000C86 RID: 3206
	// (get) Token: 0x06004153 RID: 16723 RVA: 0x000EEFB0 File Offset: 0x000ED1B0
	// (set) Token: 0x06004154 RID: 16724 RVA: 0x000EEFB8 File Offset: 0x000ED1B8
	public int FixedWidth
	{
		get
		{
			return this.fixedWidth;
		}
		set
		{
			if (value != this.fixedWidth)
			{
				this.fixedWidth = value;
				this.onResolutionChanged();
			}
		}
	}

	// Token: 0x17000C87 RID: 3207
	// (get) Token: 0x06004155 RID: 16725 RVA: 0x000EEFD4 File Offset: 0x000ED1D4
	// (set) Token: 0x06004156 RID: 16726 RVA: 0x000EEFDC File Offset: 0x000ED1DC
	public int FixedHeight
	{
		get
		{
			return this.fixedHeight;
		}
		set
		{
			if (value != this.fixedHeight)
			{
				int oldSize = this.fixedHeight;
				this.fixedHeight = value;
				this.onResolutionChanged(oldSize, value);
			}
		}
	}

	// Token: 0x17000C88 RID: 3208
	// (get) Token: 0x06004157 RID: 16727 RVA: 0x000EF00C File Offset: 0x000ED20C
	// (set) Token: 0x06004158 RID: 16728 RVA: 0x000EF014 File Offset: 0x000ED214
	public bool ConsumeMouseEvents
	{
		get
		{
			return this.consumeMouseEvents;
		}
		set
		{
			this.consumeMouseEvents = value;
		}
	}

	// Token: 0x17000C89 RID: 3209
	// (get) Token: 0x06004159 RID: 16729 RVA: 0x000EF020 File Offset: 0x000ED220
	// (set) Token: 0x0600415A RID: 16730 RVA: 0x000EF028 File Offset: 0x000ED228
	public bool OverrideCamera
	{
		get
		{
			return this.overrideCamera;
		}
		set
		{
			this.overrideCamera = value;
		}
	}

	// Token: 0x0600415B RID: 16731 RVA: 0x000EF034 File Offset: 0x000ED234
	public void OnGUI()
	{
		if (this.overrideCamera || !this.consumeMouseEvents || !Application.isPlaying || this.occluders == null)
		{
			return;
		}
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.y = (float)Screen.height - mousePosition.y;
		if (global::dfGUIManager.modalControlStack.Count > 0)
		{
			GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), GUIContent.none, GUIStyle.none);
		}
		for (int i = 0; i < this.occluders.Count; i++)
		{
			if (this.occluders[i].Contains(mousePosition))
			{
				GUI.Box(this.occluders[i], GUIContent.none, GUIStyle.none);
				break;
			}
		}
		if (Event.current.isMouse && Input.touchCount > 0)
		{
			int touchCount = Input.touchCount;
			for (int j = 0; j < touchCount; j++)
			{
				Touch touch = Input.GetTouch(j);
				if (touch.phase == null)
				{
					Vector2 touchPosition = touch.position;
					touchPosition.y = (float)Screen.height - touchPosition.y;
					if (this.occluders.Any((Rect x) => x.Contains(touchPosition)))
					{
						Event.current.Use();
						break;
					}
				}
			}
		}
	}

	// Token: 0x0600415C RID: 16732 RVA: 0x000EF1C0 File Offset: 0x000ED3C0
	public virtual void OnDestroy()
	{
		if (this.meshRenderer != null)
		{
			this.renderFilter.sharedMesh = null;
			Object.DestroyImmediate(this.renderMesh[0]);
			Object.DestroyImmediate(this.renderMesh[1]);
			this.renderMesh = null;
		}
	}

	// Token: 0x0600415D RID: 16733 RVA: 0x000EF20C File Offset: 0x000ED40C
	public virtual void Awake()
	{
		global::dfRenderData.FlushObjectPool();
	}

	// Token: 0x0600415E RID: 16734 RVA: 0x000EF214 File Offset: 0x000ED414
	public virtual void OnEnable()
	{
		this.FramesRendered = 0;
		this.TotalDrawCalls = 0;
		this.TotalTriangles = 0;
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = true;
		}
		if (Application.isPlaying)
		{
			this.onResolutionChanged();
		}
	}

	// Token: 0x0600415F RID: 16735 RVA: 0x000EF264 File Offset: 0x000ED464
	public virtual void OnDisable()
	{
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = false;
		}
	}

	// Token: 0x06004160 RID: 16736 RVA: 0x000EF284 File Offset: 0x000ED484
	public virtual void Start()
	{
		Camera[] array = Object.FindObjectsOfType(typeof(Camera)) as Camera[];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].eventMask &= ~(1 << base.gameObject.layer);
		}
		this.inputManager = (base.GetComponent<global::dfInputManager>() ?? base.gameObject.AddComponent<global::dfInputManager>());
		this.inputManager.RenderCamera = this.RenderCamera;
		this.FramesRendered = 0;
		this.invalidateAllControls();
		this.updateRenderOrder(null);
		if (this.meshRenderer != null)
		{
			this.meshRenderer.enabled = true;
		}
	}

	// Token: 0x06004161 RID: 16737 RVA: 0x000EF340 File Offset: 0x000ED540
	public virtual void Update()
	{
		if (this.renderCamera == null || !base.enabled)
		{
			if (this.meshRenderer != null)
			{
				this.meshRenderer.enabled = false;
			}
			return;
		}
		if (this.renderMesh == null || this.renderMesh.Length == 0)
		{
			this.initialize();
			if (Application.isEditor && !Application.isPlaying)
			{
				this.Render();
			}
		}
		if (this.cachedChildCount != base.transform.childCount)
		{
			this.cachedChildCount = base.transform.childCount;
			this.Invalidate();
		}
		Vector2 screenSize = this.GetScreenSize();
		if ((screenSize - this.cachedScreenSize).sqrMagnitude > 1.401298E-45f)
		{
			this.onResolutionChanged(this.cachedScreenSize, screenSize);
			this.cachedScreenSize = screenSize;
		}
	}

	// Token: 0x06004162 RID: 16738 RVA: 0x000EF428 File Offset: 0x000ED628
	public virtual void LateUpdate()
	{
		if (this.renderMesh == null || this.renderMesh.Length == 0)
		{
			this.initialize();
		}
		if (!Application.isPlaying)
		{
			BoxCollider boxCollider = base.collider as BoxCollider;
			if (boxCollider != null)
			{
				Vector2 vector = this.GetScreenSize() * this.PixelsToUnits();
				boxCollider.center = Vector3.zero;
				boxCollider.size = vector;
			}
		}
		if (this.isDirty)
		{
			this.isDirty = false;
			this.Render();
		}
	}

	// Token: 0x06004163 RID: 16739 RVA: 0x000EF4B8 File Offset: 0x000ED6B8
	public global::dfControl HitTest(Vector2 screenPosition)
	{
		Ray ray = this.renderCamera.ScreenPointToRay(screenPosition);
		float num = this.renderCamera.farClipPlane - this.renderCamera.nearClipPlane;
		RaycastHit[] array = Physics.RaycastAll(ray, num, this.renderCamera.cullingMask);
		Array.Sort<RaycastHit>(array, new Comparison<RaycastHit>(global::dfInputManager.raycastHitSorter));
		return this.inputManager.clipCast(array);
	}

	// Token: 0x06004164 RID: 16740 RVA: 0x000EF520 File Offset: 0x000ED720
	public Vector2 WorldPointToGUI(Vector3 worldPoint)
	{
		Vector2 screenSize = this.GetScreenSize();
		Camera main = Camera.main;
		Vector3 vector = Camera.main.WorldToScreenPoint(worldPoint);
		vector.x = screenSize.x * (vector.x / main.pixelWidth);
		vector.y = screenSize.y * (vector.y / main.pixelHeight);
		return this.ScreenToGui(vector);
	}

	// Token: 0x06004165 RID: 16741 RVA: 0x000EF58C File Offset: 0x000ED78C
	public float PixelsToUnits()
	{
		float num = 2f / (float)this.FixedHeight;
		return num * this.UIScale;
	}

	// Token: 0x06004166 RID: 16742 RVA: 0x000EF5B0 File Offset: 0x000ED7B0
	public virtual Plane[] GetClippingPlanes()
	{
		Vector3[] array = this.GetCorners();
		Vector3 vector = base.transform.TransformDirection(Vector3.right);
		Vector3 vector2 = base.transform.TransformDirection(Vector3.left);
		Vector3 vector3 = base.transform.TransformDirection(Vector3.up);
		Vector3 vector4 = base.transform.TransformDirection(Vector3.down);
		return new Plane[]
		{
			new Plane(vector, array[0]),
			new Plane(vector2, array[1]),
			new Plane(vector3, array[2]),
			new Plane(vector4, array[0])
		};
	}

	// Token: 0x06004167 RID: 16743 RVA: 0x000EF688 File Offset: 0x000ED888
	public Vector3[] GetCorners()
	{
		float num = this.PixelsToUnits();
		Vector2 vector = this.GetScreenSize() * num;
		float x = vector.x;
		float y = vector.y;
		Vector3 vector2;
		vector2..ctor(-x * 0.5f, y * 0.5f);
		Vector3 vector3 = vector2 + new Vector3(x, 0f);
		Vector3 vector4 = vector2 + new Vector3(0f, -y);
		Vector3 vector5 = vector3 + new Vector3(0f, -y);
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		this.corners[0] = localToWorldMatrix.MultiplyPoint(vector2);
		this.corners[1] = localToWorldMatrix.MultiplyPoint(vector3);
		this.corners[2] = localToWorldMatrix.MultiplyPoint(vector5);
		this.corners[3] = localToWorldMatrix.MultiplyPoint(vector4);
		return this.corners;
	}

	// Token: 0x06004168 RID: 16744 RVA: 0x000EF788 File Offset: 0x000ED988
	public Vector2 GetScreenSize()
	{
		Camera camera = this.RenderCamera;
		bool flag = Application.isPlaying && camera != null;
		if (flag)
		{
			float num = (!this.PixelPerfectMode) ? (camera.pixelHeight / (float)this.fixedHeight * this.uiScale) : 1f;
			return (new Vector2(camera.pixelWidth, camera.pixelHeight) / num).CeilToInt();
		}
		return new Vector2((float)this.FixedWidth, (float)this.FixedHeight);
	}

	// Token: 0x06004169 RID: 16745 RVA: 0x000EF814 File Offset: 0x000EDA14
	public T AddControl<T>() where T : global::dfControl
	{
		return (T)((object)this.AddControl(typeof(T)));
	}

	// Token: 0x0600416A RID: 16746 RVA: 0x000EF82C File Offset: 0x000EDA2C
	public global::dfControl AddControl(Type type)
	{
		if (!typeof(global::dfControl).IsAssignableFrom(type))
		{
			throw new InvalidCastException();
		}
		global::dfControl dfControl = new GameObject(type.Name, new Type[]
		{
			type
		})
		{
			transform = 
			{
				parent = base.transform
			},
			layer = base.gameObject.layer
		}.GetComponent(type) as global::dfControl;
		dfControl.ZOrder = this.getMaxZOrder() + 1;
		return dfControl;
	}

	// Token: 0x0600416B RID: 16747 RVA: 0x000EF8A8 File Offset: 0x000EDAA8
	private int getMaxZOrder()
	{
		int num = -1;
		using (global::dfList<global::dfControl> topLevelControls = this.getTopLevelControls())
		{
			for (int i = 0; i < topLevelControls.Count; i++)
			{
				num = Mathf.Max(num, topLevelControls[i].ZOrder);
			}
		}
		return num;
	}

	// Token: 0x0600416C RID: 16748 RVA: 0x000EF918 File Offset: 0x000EDB18
	public global::dfRenderData GetDrawCallBuffer(int drawCallNumber)
	{
		return this.drawCallBuffers[drawCallNumber];
	}

	// Token: 0x0600416D RID: 16749 RVA: 0x000EF928 File Offset: 0x000EDB28
	public static global::dfControl GetModalControl()
	{
		return (global::dfGUIManager.modalControlStack.Count <= 0) ? null : global::dfGUIManager.modalControlStack.Peek().control;
	}

	// Token: 0x0600416E RID: 16750 RVA: 0x000EF960 File Offset: 0x000EDB60
	public Vector2 ScreenToGui(Vector2 position)
	{
		position.y = this.GetScreenSize().y - position.y;
		return position;
	}

	// Token: 0x0600416F RID: 16751 RVA: 0x000EF98C File Offset: 0x000EDB8C
	public static void PushModal(global::dfControl control, global::dfGUIManager.ModalPoppedCallback callback = null)
	{
		if (control == null)
		{
			throw new NullReferenceException("Cannot call PushModal() with a null reference");
		}
		global::dfGUIManager.modalControlStack.Push(new global::dfGUIManager.ModalControlReference
		{
			control = control,
			callback = callback
		});
	}

	// Token: 0x06004170 RID: 16752 RVA: 0x000EF9D4 File Offset: 0x000EDBD4
	public static void PopModal()
	{
		if (global::dfGUIManager.modalControlStack.Count == 0)
		{
			throw new InvalidOperationException("Modal stack is empty");
		}
		global::dfGUIManager.ModalControlReference modalControlReference = global::dfGUIManager.modalControlStack.Pop();
		if (modalControlReference.callback != null)
		{
			modalControlReference.callback(modalControlReference.control);
		}
	}

	// Token: 0x06004171 RID: 16753 RVA: 0x000EFA28 File Offset: 0x000EDC28
	public static void SetFocus(global::dfControl control)
	{
		if (global::dfGUIManager.activeControl == control)
		{
			return;
		}
		global::dfControl dfControl = global::dfGUIManager.activeControl;
		global::dfGUIManager.activeControl = control;
		global::dfFocusEventArgs args = new global::dfFocusEventArgs(control, dfControl);
		global::dfList<global::dfControl> prevFocusChain = global::dfList<global::dfControl>.Obtain();
		if (dfControl != null)
		{
			global::dfControl dfControl2 = dfControl;
			while (dfControl2 != null)
			{
				prevFocusChain.Add(dfControl2);
				dfControl2 = dfControl2.Parent;
			}
		}
		global::dfList<global::dfControl> newFocusChain = global::dfList<global::dfControl>.Obtain();
		if (control != null)
		{
			global::dfControl dfControl3 = control;
			while (dfControl3 != null)
			{
				newFocusChain.Add(dfControl3);
				dfControl3 = dfControl3.Parent;
			}
		}
		if (dfControl != null)
		{
			prevFocusChain.ForEach(delegate(global::dfControl c)
			{
				if (!newFocusChain.Contains(c))
				{
					c.OnLeaveFocus(args);
				}
			});
			dfControl.OnLostFocus(args);
		}
		if (control != null)
		{
			newFocusChain.ForEach(delegate(global::dfControl c)
			{
				if (!prevFocusChain.Contains(c))
				{
					c.OnEnterFocus(args);
				}
			});
			control.OnGotFocus(args);
		}
		newFocusChain.Release();
		prevFocusChain.Release();
	}

	// Token: 0x06004172 RID: 16754 RVA: 0x000EFB54 File Offset: 0x000EDD54
	public static bool HasFocus(global::dfControl control)
	{
		return !(control == null) && global::dfGUIManager.activeControl == control;
	}

	// Token: 0x06004173 RID: 16755 RVA: 0x000EFB70 File Offset: 0x000EDD70
	public static bool ContainsFocus(global::dfControl control)
	{
		return global::dfGUIManager.activeControl == control || (!(global::dfGUIManager.activeControl == null) && !(control == null) && global::dfGUIManager.activeControl.transform.IsChildOf(control.transform));
	}

	// Token: 0x06004174 RID: 16756 RVA: 0x000EFBC4 File Offset: 0x000EDDC4
	public void BringToFront(global::dfControl control)
	{
		if (control.Parent != null)
		{
			control = control.GetRootContainer();
		}
		using (global::dfList<global::dfControl> topLevelControls = this.getTopLevelControls())
		{
			int zorder = 0;
			for (int i = 0; i < topLevelControls.Count; i++)
			{
				global::dfControl dfControl = topLevelControls[i];
				if (dfControl != control)
				{
					dfControl.ZOrder = zorder++;
				}
			}
			control.ZOrder = zorder;
			this.Invalidate();
		}
	}

	// Token: 0x06004175 RID: 16757 RVA: 0x000EFC64 File Offset: 0x000EDE64
	public void SendToBack(global::dfControl control)
	{
		if (control.Parent != null)
		{
			control = control.GetRootContainer();
		}
		using (global::dfList<global::dfControl> topLevelControls = this.getTopLevelControls())
		{
			int num = 1;
			for (int i = 0; i < topLevelControls.Count; i++)
			{
				global::dfControl dfControl = topLevelControls[i];
				if (dfControl != control)
				{
					dfControl.ZOrder = num++;
				}
			}
			control.ZOrder = 0;
			this.Invalidate();
		}
	}

	// Token: 0x06004176 RID: 16758 RVA: 0x000EFD04 File Offset: 0x000EDF04
	public void Invalidate()
	{
		if (this.isDirty)
		{
			return;
		}
		this.isDirty = true;
		this.updateRenderSettings();
	}

	// Token: 0x06004177 RID: 16759 RVA: 0x000EFD20 File Offset: 0x000EDF20
	public static void RefreshAll(bool force = false)
	{
		global::dfGUIManager[] array = Object.FindObjectsOfType(typeof(global::dfGUIManager)) as global::dfGUIManager[];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].invalidateAllControls();
			if (force || !Application.isPlaying)
			{
				array[i].Render();
			}
		}
	}

	// Token: 0x06004178 RID: 16760 RVA: 0x000EFD78 File Offset: 0x000EDF78
	public void Render()
	{
		this.FramesRendered++;
		if (global::dfGUIManager.BeforeRender != null)
		{
			global::dfGUIManager.BeforeRender(this);
		}
		try
		{
			this.updateRenderSettings();
			this.ControlsRendered = 0;
			this.occluders.Clear();
			this.TotalDrawCalls = 0;
			this.TotalTriangles = 0;
			if (this.RenderCamera == null || !base.enabled)
			{
				if (this.meshRenderer != null)
				{
					this.meshRenderer.enabled = false;
				}
			}
			else
			{
				if (this.meshRenderer != null && !this.meshRenderer.enabled)
				{
					this.meshRenderer.enabled = true;
				}
				if (this.renderMesh == null || this.renderMesh.Length == 0)
				{
					Debug.LogError("GUI Manager not initialized before Render() called");
				}
				else
				{
					this.resetDrawCalls();
					global::dfRenderData dfRenderData = null;
					this.clipStack.Clear();
					this.clipStack.Push(global::dfGUIManager.ClipRegion.Obtain());
					uint start_VALUE = dfChecksumUtil.START_VALUE;
					using (global::dfList<global::dfControl> topLevelControls = this.getTopLevelControls())
					{
						this.updateRenderOrder(topLevelControls);
						for (int i = 0; i < topLevelControls.Count; i++)
						{
							global::dfControl control = topLevelControls[i];
							this.renderControl(ref dfRenderData, control, start_VALUE, 1f);
						}
					}
					this.drawCallBuffers.RemoveAll((global::dfRenderData x) => x.Vertices.Count == 0);
					this.drawCallCount = this.drawCallBuffers.Count;
					this.TotalDrawCalls = this.drawCallCount;
					if (this.drawCallBuffers.Count == 0)
					{
						if (this.renderFilter.sharedMesh != null)
						{
							this.renderFilter.sharedMesh.Clear();
						}
					}
					else
					{
						this.meshRenderer.sharedMaterials = this.gatherMaterials();
						global::dfRenderData dfRenderData2 = this.compileMasterBuffer();
						this.TotalTriangles = dfRenderData2.Triangles.Count / 3;
						Mesh mesh = this.getRenderMesh();
						this.renderFilter.sharedMesh = mesh;
						Mesh mesh2 = mesh;
						mesh2.Clear();
						mesh2.vertices = dfRenderData2.Vertices.Items;
						mesh2.uv = dfRenderData2.UV.Items;
						mesh2.colors32 = dfRenderData2.Colors.Items;
						if (this.generateNormals && dfRenderData2.Normals.Items.Length == dfRenderData2.Vertices.Items.Length)
						{
							mesh2.normals = dfRenderData2.Normals.Items;
							mesh2.tangents = dfRenderData2.Tangents.Items;
						}
						mesh2.subMeshCount = this.submeshes.Count;
						for (int j = 0; j < this.submeshes.Count; j++)
						{
							int num = this.submeshes[j];
							int num2 = dfRenderData2.Triangles.Count - num;
							if (j < this.submeshes.Count - 1)
							{
								num2 = this.submeshes[j + 1] - num;
							}
							int[] array = new int[num2];
							dfRenderData2.Triangles.CopyTo(num, array, 0, num2);
							mesh2.SetTriangles(array, j);
						}
						if (this.clipStack.Count != 1)
						{
							Debug.LogError("Clip stack not properly maintained");
						}
						this.clipStack.Pop().Release();
						this.clipStack.Clear();
					}
				}
			}
		}
		catch (global::dfAbortRenderingException)
		{
			this.isDirty = true;
		}
		finally
		{
			if (global::dfGUIManager.AfterRender != null)
			{
				global::dfGUIManager.AfterRender(this);
			}
		}
	}

	// Token: 0x06004179 RID: 16761 RVA: 0x000F017C File Offset: 0x000EE37C
	private global::dfList<global::dfControl> getTopLevelControls()
	{
		int childCount = base.transform.childCount;
		global::dfList<global::dfControl> dfList = global::dfList<global::dfControl>.Obtain(childCount);
		for (int i = 0; i < childCount; i++)
		{
			global::dfControl component = base.transform.GetChild(i).GetComponent<global::dfControl>();
			if (component != null)
			{
				dfList.Add(component);
			}
		}
		dfList.Sort();
		return dfList;
	}

	// Token: 0x0600417A RID: 16762 RVA: 0x000F01DC File Offset: 0x000EE3DC
	private void updateRenderSettings()
	{
		Camera camera = this.RenderCamera;
		if (camera == null)
		{
			return;
		}
		if (!this.overrideCamera)
		{
			this.updateRenderCamera(camera);
		}
		if (base.transform.hasChanged)
		{
			Vector3 localScale = base.transform.localScale;
			bool flag = localScale.x < float.Epsilon || !Mathf.Approximately(localScale.x, localScale.y) || !Mathf.Approximately(localScale.x, localScale.z);
			if (flag)
			{
				localScale.y = (localScale.z = (localScale.x = Mathf.Max(localScale.x, 0.001f)));
				base.transform.localScale = localScale;
			}
		}
		if (!this.overrideCamera)
		{
			if (Application.isPlaying && this.PixelPerfectMode)
			{
				float num = camera.pixelHeight / (float)this.fixedHeight;
				camera.orthographicSize = num;
				camera.fieldOfView = 60f * num;
			}
			else
			{
				camera.orthographicSize = 1f;
				camera.fieldOfView = 60f;
			}
		}
		camera.transparencySortMode = 2;
		if (this.cachedScreenSize.sqrMagnitude <= 1.401298E-45f)
		{
			this.cachedScreenSize = new Vector2((float)this.FixedWidth, (float)this.FixedHeight);
		}
		base.transform.hasChanged = false;
	}

	// Token: 0x0600417B RID: 16763 RVA: 0x000F0350 File Offset: 0x000EE550
	private void updateRenderCamera(Camera camera)
	{
		if (Application.isPlaying && camera.targetTexture != null)
		{
			camera.clearFlags = 2;
			camera.backgroundColor = Color.clear;
		}
		else
		{
			camera.clearFlags = 3;
		}
		Vector3 vector = (!Application.isPlaying) ? Vector3.zero : (-this.uiOffset * this.PixelsToUnits());
		if (camera.isOrthoGraphic)
		{
			camera.nearClipPlane = Mathf.Min(camera.nearClipPlane, -1f);
			camera.farClipPlane = Mathf.Max(camera.farClipPlane, 1f);
		}
		else
		{
			float num = camera.fieldOfView * 0.0174532924f;
			Vector3[] array = this.GetCorners();
			float num2 = Vector3.Distance(array[3], array[0]);
			float num3 = num2 / (2f * Mathf.Tan(num / 2f));
			Vector3 vector2 = base.transform.TransformDirection(Vector3.back) * num3;
			camera.farClipPlane = Mathf.Max(num3 * 2f, camera.farClipPlane);
			vector += vector2;
		}
		if (Application.isPlaying && this.needHalfPixelOffset())
		{
			float pixelHeight = camera.pixelHeight;
			float num4 = 2f / pixelHeight * (pixelHeight / (float)this.FixedHeight);
			Vector3 vector3;
			vector3..ctor(num4 * 0.5f, num4 * -0.5f, 0f);
			vector += vector3;
		}
		if (!this.overrideCamera)
		{
			if (Vector3.SqrMagnitude(camera.transform.localPosition - vector) > 1.401298E-45f)
			{
				camera.transform.localPosition = vector;
			}
			camera.transform.hasChanged = false;
		}
	}

	// Token: 0x0600417C RID: 16764 RVA: 0x000F0520 File Offset: 0x000EE720
	private global::dfRenderData compileMasterBuffer()
	{
		this.submeshes.Clear();
		global::dfGUIManager.masterBuffer.Clear();
		for (int i = 0; i < this.drawCallCount; i++)
		{
			this.submeshes.Add(global::dfGUIManager.masterBuffer.Triangles.Count);
			global::dfRenderData dfRenderData = this.drawCallBuffers[i];
			if (this.generateNormals && dfRenderData.Normals.Count == 0)
			{
				this.generateNormalsAndTangents(dfRenderData);
			}
			global::dfGUIManager.masterBuffer.Merge(dfRenderData, false);
		}
		global::dfGUIManager.masterBuffer.ApplyTransform(base.transform.worldToLocalMatrix);
		return global::dfGUIManager.masterBuffer;
	}

	// Token: 0x0600417D RID: 16765 RVA: 0x000F05C8 File Offset: 0x000EE7C8
	private void generateNormalsAndTangents(global::dfRenderData buffer)
	{
		Vector3 normalized = buffer.Transform.MultiplyVector(Vector3.back).normalized;
		Vector4 item = buffer.Transform.MultiplyVector(Vector3.right).normalized;
		item.w = -1f;
		for (int i = 0; i < buffer.Vertices.Count; i++)
		{
			buffer.Normals.Add(normalized);
			buffer.Tangents.Add(item);
		}
	}

	// Token: 0x0600417E RID: 16766 RVA: 0x000F0658 File Offset: 0x000EE858
	private bool needHalfPixelOffset()
	{
		if (this.applyHalfPixelOffset != null)
		{
			return this.applyHalfPixelOffset.Value;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = this.pixelPerfectMode && (platform == 2 || platform == 5 || platform == 7) && SystemInfo.graphicsShaderLevel < 40;
		this.applyHalfPixelOffset = new bool?(Application.isEditor || flag);
		return flag;
	}

	// Token: 0x0600417F RID: 16767 RVA: 0x000F06D0 File Offset: 0x000EE8D0
	private Material[] gatherMaterials()
	{
		int num = this.renderQueueBase;
		global::dfGUIManager.MaterialCache.Reset();
		int num2 = this.drawCallBuffers.Matching((global::dfRenderData buff) => buff != null && buff.Material != null);
		int num3 = 0;
		Material[] array = new Material[num2];
		for (int i = 0; i < this.drawCallBuffers.Count; i++)
		{
			if (!(this.drawCallBuffers[i].Material == null))
			{
				Material material = global::dfGUIManager.MaterialCache.Lookup(this.drawCallBuffers[i].Material);
				material.renderQueue = num++;
				array[num3++] = material;
			}
		}
		return array;
	}

	// Token: 0x06004180 RID: 16768 RVA: 0x000F078C File Offset: 0x000EE98C
	private void resetDrawCalls()
	{
		this.drawCallCount = 0;
		for (int i = 0; i < this.drawCallBuffers.Count; i++)
		{
			this.drawCallBuffers[i].Release();
		}
		this.drawCallBuffers.Clear();
	}

	// Token: 0x06004181 RID: 16769 RVA: 0x000F07D8 File Offset: 0x000EE9D8
	private global::dfRenderData getDrawCallBuffer(Material material)
	{
		global::dfRenderData dfRenderData;
		if (this.MergeMaterials && material != null)
		{
			dfRenderData = this.findDrawCallBufferByMaterial(material);
			if (dfRenderData != null)
			{
				return dfRenderData;
			}
		}
		dfRenderData = global::dfRenderData.Obtain();
		dfRenderData.Material = material;
		this.drawCallBuffers.Add(dfRenderData);
		this.drawCallCount++;
		return dfRenderData;
	}

	// Token: 0x06004182 RID: 16770 RVA: 0x000F0838 File Offset: 0x000EEA38
	private global::dfRenderData findDrawCallBufferByMaterial(Material material)
	{
		for (int i = 0; i < this.drawCallCount; i++)
		{
			if (this.drawCallBuffers[i].Material == material)
			{
				return this.drawCallBuffers[i];
			}
		}
		return null;
	}

	// Token: 0x06004183 RID: 16771 RVA: 0x000F0888 File Offset: 0x000EEA88
	private Mesh getRenderMesh()
	{
		this.activeRenderMesh = ((this.activeRenderMesh != 1) ? 1 : 0);
		return this.renderMesh[this.activeRenderMesh];
	}

	// Token: 0x06004184 RID: 16772 RVA: 0x000F08BC File Offset: 0x000EEABC
	private void renderControl(ref global::dfRenderData buffer, global::dfControl control, uint checksum, float opacity)
	{
		if (!control.GetIsVisibleRaw())
		{
			return;
		}
		float opacity2 = opacity * control.Opacity;
		if (opacity <= 0.005f)
		{
			return;
		}
		global::dfGUIManager.ClipRegion clipRegion = this.clipStack.Peek();
		checksum = dfChecksumUtil.Calculate(checksum, control.Version);
		Bounds bounds = control.GetBounds();
		bool flag = false;
		if (!(control is global::IDFMultiRender))
		{
			global::dfRenderData dfRenderData = control.Render();
			if (dfRenderData == null)
			{
				return;
			}
			if (this.processRenderData(ref buffer, dfRenderData, bounds, checksum, clipRegion))
			{
				flag = true;
			}
		}
		else
		{
			global::dfList<global::dfRenderData> dfList = ((global::IDFMultiRender)control).RenderMultiple();
			if (dfList != null)
			{
				for (int i = 0; i < dfList.Count; i++)
				{
					global::dfRenderData controlData = dfList[i];
					if (this.processRenderData(ref buffer, controlData, bounds, checksum, clipRegion))
					{
						flag = true;
					}
				}
			}
		}
		if (flag)
		{
			this.ControlsRendered++;
			this.occluders.Add(this.getControlOccluder(control));
		}
		if (control.ClipChildren)
		{
			clipRegion = global::dfGUIManager.ClipRegion.Obtain(clipRegion, control);
			this.clipStack.Push(clipRegion);
		}
		for (int j = 0; j < control.Controls.Count; j++)
		{
			global::dfControl control2 = control.Controls[j];
			this.renderControl(ref buffer, control2, checksum, opacity2);
		}
		if (control.ClipChildren)
		{
			this.clipStack.Pop().Release();
		}
	}

	// Token: 0x06004185 RID: 16773 RVA: 0x000F0A2C File Offset: 0x000EEC2C
	private Rect getControlOccluder(global::dfControl control)
	{
		Rect screenRect = control.GetScreenRect();
		Vector2 vector;
		vector..ctor(screenRect.width * control.HotZoneScale.x, screenRect.height * control.HotZoneScale.y);
		Vector2 vector2 = new Vector2(vector.x - screenRect.width, vector.y - screenRect.height) * 0.5f;
		return new Rect(screenRect.x - vector2.x, screenRect.y - vector2.y, vector.x, vector.y);
	}

	// Token: 0x06004186 RID: 16774 RVA: 0x000F0AD4 File Offset: 0x000EECD4
	private bool processRenderData(ref global::dfRenderData buffer, global::dfRenderData controlData, Bounds bounds, uint checksum, global::dfGUIManager.ClipRegion clipInfo)
	{
		bool flag = buffer == null || (controlData.IsValid() && (!object.Equals(buffer.Shader, controlData.Shader) || (controlData.Material != null && !controlData.Material.Equals(buffer.Material))));
		if (flag && controlData.IsValid())
		{
			buffer = this.getDrawCallBuffer(controlData.Material);
		}
		return controlData != null && controlData.IsValid() && clipInfo.PerformClipping(buffer, bounds, checksum, controlData);
	}

	// Token: 0x06004187 RID: 16775 RVA: 0x000F0B84 File Offset: 0x000EED84
	private void initialize()
	{
		if (this.renderCamera == null)
		{
			Debug.LogError("No camera is assigned to the GUIManager");
			return;
		}
		this.meshRenderer = base.GetComponent<MeshRenderer>();
		this.meshRenderer.hideFlags = 2;
		this.renderFilter = base.GetComponent<MeshFilter>();
		this.renderFilter.hideFlags = 2;
		this.renderMesh = new Mesh[]
		{
			new Mesh
			{
				hideFlags = 4
			},
			new Mesh
			{
				hideFlags = 4
			}
		};
		this.renderMesh[0].MarkDynamic();
		this.renderMesh[1].MarkDynamic();
		if (this.fixedWidth < 0)
		{
			this.fixedWidth = Mathf.RoundToInt((float)this.fixedHeight * 1.33333f);
			base.GetComponentsInChildren<global::dfControl>().ToList<global::dfControl>().ForEach(delegate(global::dfControl x)
			{
				x.ResetLayout(false, false);
			});
		}
	}

	// Token: 0x06004188 RID: 16776 RVA: 0x000F0C78 File Offset: 0x000EEE78
	private global::dfGUICamera findCameraComponent()
	{
		if (this.guiCamera != null)
		{
			return this.guiCamera;
		}
		if (this.renderCamera == null)
		{
			return null;
		}
		this.guiCamera = this.renderCamera.GetComponent<global::dfGUICamera>();
		if (this.guiCamera == null)
		{
			this.guiCamera = this.renderCamera.gameObject.AddComponent<global::dfGUICamera>();
			this.guiCamera.transform.position = base.transform.position;
		}
		return this.guiCamera;
	}

	// Token: 0x06004189 RID: 16777 RVA: 0x000F0D0C File Offset: 0x000EEF0C
	private void onResolutionChanged()
	{
		int currentSize = (!Application.isPlaying) ? this.FixedHeight : ((int)this.renderCamera.pixelHeight);
		this.onResolutionChanged(this.FixedHeight, currentSize);
	}

	// Token: 0x0600418A RID: 16778 RVA: 0x000F0D48 File Offset: 0x000EEF48
	private void onResolutionChanged(int oldSize, int currentSize)
	{
		float aspect = this.RenderCamera.aspect;
		float num = (float)oldSize * aspect;
		float num2 = (float)currentSize * aspect;
		Vector2 oldSize2;
		oldSize2..ctor(num, (float)oldSize);
		Vector2 currentSize2;
		currentSize2..ctor(num2, (float)currentSize);
		this.onResolutionChanged(oldSize2, currentSize2);
	}

	// Token: 0x0600418B RID: 16779 RVA: 0x000F0D88 File Offset: 0x000EEF88
	private void onResolutionChanged(Vector2 oldSize, Vector2 currentSize)
	{
		this.cachedScreenSize = currentSize;
		this.applyHalfPixelOffset = null;
		float aspect = this.RenderCamera.aspect;
		float num = oldSize.y * aspect;
		float num2 = currentSize.y * aspect;
		Vector2 previousResolution;
		previousResolution..ctor(num, oldSize.y);
		Vector2 currentResolution;
		currentResolution..ctor(num2, currentSize.y);
		global::dfControl[] componentsInChildren = base.GetComponentsInChildren<global::dfControl>();
		Array.Sort<global::dfControl>(componentsInChildren, new Comparison<global::dfControl>(this.renderSortFunc));
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (this.pixelPerfectMode && componentsInChildren[i].Parent == null)
			{
				componentsInChildren[i].MakePixelPerfect(true);
			}
			componentsInChildren[i].OnResolutionChanged(previousResolution, currentResolution);
		}
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].PerformLayout();
		}
		int num3 = 0;
		while (num3 < componentsInChildren.Length && this.pixelPerfectMode)
		{
			if (componentsInChildren[num3].Parent == null)
			{
				componentsInChildren[num3].MakePixelPerfect(true);
			}
			num3++;
		}
	}

	// Token: 0x0600418C RID: 16780 RVA: 0x000F0EC0 File Offset: 0x000EF0C0
	private void invalidateAllControls()
	{
		global::dfControl[] componentsInChildren = base.GetComponentsInChildren<global::dfControl>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Invalidate();
		}
		this.updateRenderOrder(null);
	}

	// Token: 0x0600418D RID: 16781 RVA: 0x000F0EF8 File Offset: 0x000EF0F8
	private int renderSortFunc(global::dfControl lhs, global::dfControl rhs)
	{
		return lhs.RenderOrder.CompareTo(rhs.RenderOrder);
	}

	// Token: 0x0600418E RID: 16782 RVA: 0x000F0F1C File Offset: 0x000EF11C
	private void updateRenderOrder(global::dfList<global::dfControl> list = null)
	{
		global::dfList<global::dfControl> dfList = (list == null) ? this.getTopLevelControls() : list;
		dfList.Sort();
		int num = 0;
		for (int i = 0; i < dfList.Count; i++)
		{
			global::dfControl dfControl = dfList[i];
			if (dfControl.Parent == null)
			{
				dfControl.setRenderOrder(ref num);
			}
		}
	}

	// Token: 0x0400223F RID: 8767
	[SerializeField]
	protected float uiScale = 1f;

	// Token: 0x04002240 RID: 8768
	[SerializeField]
	protected global::dfInputManager inputManager;

	// Token: 0x04002241 RID: 8769
	[SerializeField]
	protected int fixedWidth = -1;

	// Token: 0x04002242 RID: 8770
	[SerializeField]
	protected int fixedHeight = 600;

	// Token: 0x04002243 RID: 8771
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x04002244 RID: 8772
	[SerializeField]
	protected global::dfFont defaultFont;

	// Token: 0x04002245 RID: 8773
	[SerializeField]
	protected bool mergeMaterials;

	// Token: 0x04002246 RID: 8774
	[SerializeField]
	protected bool pixelPerfectMode = true;

	// Token: 0x04002247 RID: 8775
	[SerializeField]
	protected Camera renderCamera;

	// Token: 0x04002248 RID: 8776
	[SerializeField]
	protected bool generateNormals;

	// Token: 0x04002249 RID: 8777
	[SerializeField]
	protected bool consumeMouseEvents = true;

	// Token: 0x0400224A RID: 8778
	[SerializeField]
	protected bool overrideCamera;

	// Token: 0x0400224B RID: 8779
	[SerializeField]
	protected int renderQueueBase = 3000;

	// Token: 0x0400224C RID: 8780
	private static global::dfControl activeControl = null;

	// Token: 0x0400224D RID: 8781
	private static Stack<global::dfGUIManager.ModalControlReference> modalControlStack = new Stack<global::dfGUIManager.ModalControlReference>();

	// Token: 0x0400224E RID: 8782
	private global::dfGUICamera guiCamera;

	// Token: 0x0400224F RID: 8783
	private Mesh[] renderMesh;

	// Token: 0x04002250 RID: 8784
	private MeshFilter renderFilter;

	// Token: 0x04002251 RID: 8785
	private MeshRenderer meshRenderer;

	// Token: 0x04002252 RID: 8786
	private int activeRenderMesh;

	// Token: 0x04002253 RID: 8787
	private int cachedChildCount;

	// Token: 0x04002254 RID: 8788
	private bool isDirty;

	// Token: 0x04002255 RID: 8789
	private Vector2 cachedScreenSize;

	// Token: 0x04002256 RID: 8790
	private Vector3[] corners = new Vector3[4];

	// Token: 0x04002257 RID: 8791
	private global::dfList<Rect> occluders = new global::dfList<Rect>(256);

	// Token: 0x04002258 RID: 8792
	private Stack<global::dfGUIManager.ClipRegion> clipStack = new Stack<global::dfGUIManager.ClipRegion>();

	// Token: 0x04002259 RID: 8793
	private static global::dfRenderData masterBuffer = new global::dfRenderData(4096);

	// Token: 0x0400225A RID: 8794
	private global::dfList<global::dfRenderData> drawCallBuffers = new global::dfList<global::dfRenderData>();

	// Token: 0x0400225B RID: 8795
	private List<int> submeshes = new List<int>();

	// Token: 0x0400225C RID: 8796
	private int drawCallCount;

	// Token: 0x0400225D RID: 8797
	private Vector2 uiOffset = Vector2.zero;

	// Token: 0x0400225E RID: 8798
	private bool? applyHalfPixelOffset;

	// Token: 0x02000798 RID: 1944
	private class ClipRegion
	{
		// Token: 0x06004192 RID: 16786 RVA: 0x000F0FB0 File Offset: 0x000EF1B0
		private ClipRegion()
		{
			this.planes = new global::dfList<Plane>();
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x000F0FD0 File Offset: 0x000EF1D0
		public static global::dfGUIManager.ClipRegion Obtain()
		{
			return (global::dfGUIManager.ClipRegion.pool.Count <= 0) ? new global::dfGUIManager.ClipRegion() : global::dfGUIManager.ClipRegion.pool.Dequeue();
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x000F1004 File Offset: 0x000EF204
		public static global::dfGUIManager.ClipRegion Obtain(global::dfGUIManager.ClipRegion parent, global::dfControl control)
		{
			global::dfGUIManager.ClipRegion clipRegion = (global::dfGUIManager.ClipRegion.pool.Count <= 0) ? new global::dfGUIManager.ClipRegion() : global::dfGUIManager.ClipRegion.pool.Dequeue();
			clipRegion.planes.AddRange(control.GetClippingPlanes());
			if (parent != null)
			{
				clipRegion.planes.AddRange(parent.planes);
			}
			return clipRegion;
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x000F1060 File Offset: 0x000EF260
		public void Release()
		{
			this.planes.Clear();
			global::dfGUIManager.ClipRegion.pool.Enqueue(this);
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000F1078 File Offset: 0x000EF278
		public bool PerformClipping(global::dfRenderData dest, Bounds bounds, uint checksum, global::dfRenderData controlData)
		{
			if (controlData.Checksum == checksum)
			{
				if (controlData.Intersection == global::dfIntersectionType.Inside)
				{
					dest.Merge(controlData, true);
					return true;
				}
				if (controlData.Intersection == global::dfIntersectionType.None)
				{
					return false;
				}
			}
			bool result = false;
			global::dfIntersectionType dfIntersectionType;
			using (global::dfList<Plane> dfList = this.TestIntersection(bounds, out dfIntersectionType))
			{
				if (dfIntersectionType == global::dfIntersectionType.Inside)
				{
					dest.Merge(controlData, true);
					result = true;
				}
				else if (dfIntersectionType == global::dfIntersectionType.Intersecting)
				{
					this.clipToPlanes(dfList, controlData, dest, checksum);
					result = true;
				}
				controlData.Checksum = checksum;
				controlData.Intersection = dfIntersectionType;
			}
			return result;
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000F1130 File Offset: 0x000EF330
		public global::dfList<Plane> TestIntersection(Bounds bounds, out global::dfIntersectionType type)
		{
			if (this.planes == null || this.planes.Count == 0)
			{
				type = global::dfIntersectionType.Inside;
				return null;
			}
			global::dfList<Plane> dfList = global::dfList<Plane>.Obtain(this.planes.Count);
			Vector3 center = bounds.center;
			Vector3 extents = bounds.extents;
			bool flag = false;
			for (int i = 0; i < this.planes.Count; i++)
			{
				Plane item = this.planes[i];
				Vector3 normal = item.normal;
				float distance = item.distance;
				float num = extents.x * Mathf.Abs(normal.x) + extents.y * Mathf.Abs(normal.y) + extents.z * Mathf.Abs(normal.z);
				float num2 = Vector3.Dot(normal, center) + distance;
				if (Mathf.Abs(num2) <= num)
				{
					flag = true;
					dfList.Add(item);
				}
				else if (num2 < -num)
				{
					type = global::dfIntersectionType.None;
					dfList.Release();
					return null;
				}
			}
			if (flag)
			{
				type = global::dfIntersectionType.Intersecting;
				return dfList;
			}
			type = global::dfIntersectionType.Inside;
			dfList.Release();
			return null;
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000F1258 File Offset: 0x000EF458
		public void clipToPlanes(global::dfList<Plane> planes, global::dfRenderData data, global::dfRenderData dest, uint controlChecksum)
		{
			if (data == null || data.Vertices.Count == 0)
			{
				return;
			}
			if (planes == null || planes.Count == 0)
			{
				dest.Merge(data, true);
				return;
			}
			global::dfClippingUtil.Clip(planes, data, dest);
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000F12A0 File Offset: 0x000EF4A0
		private static int sortClipPlanes(Plane lhs, Plane rhs)
		{
			return lhs.distance.CompareTo(rhs.distance);
		}

		// Token: 0x04002268 RID: 8808
		private static Queue<global::dfGUIManager.ClipRegion> pool = new Queue<global::dfGUIManager.ClipRegion>();

		// Token: 0x04002269 RID: 8809
		private global::dfList<Plane> planes;
	}

	// Token: 0x02000799 RID: 1945
	private struct ModalControlReference
	{
		// Token: 0x0400226A RID: 8810
		public global::dfControl control;

		// Token: 0x0400226B RID: 8811
		public global::dfGUIManager.ModalPoppedCallback callback;
	}

	// Token: 0x0200079A RID: 1946
	private class MaterialCache
	{
		// Token: 0x0600419D RID: 16797 RVA: 0x000F12D8 File Offset: 0x000EF4D8
		public static Material Lookup(Material BaseMaterial)
		{
			if (BaseMaterial == null)
			{
				Debug.LogError("Cache lookup on null material");
				return null;
			}
			global::dfGUIManager.MaterialCache.Cache cache = null;
			if (global::dfGUIManager.MaterialCache.cache.TryGetValue(BaseMaterial, out cache))
			{
				return cache.Obtain();
			}
			global::dfGUIManager.MaterialCache.Cache cache2 = new global::dfGUIManager.MaterialCache.Cache(BaseMaterial);
			global::dfGUIManager.MaterialCache.cache[BaseMaterial] = cache2;
			cache = cache2;
			return cache.Obtain();
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000F1334 File Offset: 0x000EF534
		public static void Reset()
		{
			global::dfGUIManager.MaterialCache.Cache.ResetAll();
		}

		// Token: 0x0400226C RID: 8812
		private static Dictionary<Material, global::dfGUIManager.MaterialCache.Cache> cache = new Dictionary<Material, global::dfGUIManager.MaterialCache.Cache>();

		// Token: 0x0200079B RID: 1947
		private class Cache
		{
			// Token: 0x0600419F RID: 16799 RVA: 0x000F133C File Offset: 0x000EF53C
			private Cache()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060041A0 RID: 16800 RVA: 0x000F1358 File Offset: 0x000EF558
			public Cache(Material BaseMaterial)
			{
				this.baseMaterial = BaseMaterial;
				this.instances.Add(BaseMaterial);
				global::dfGUIManager.MaterialCache.Cache.cacheInstances.Add(this);
			}

			// Token: 0x060041A2 RID: 16802 RVA: 0x000F1398 File Offset: 0x000EF598
			public static void ResetAll()
			{
				for (int i = 0; i < global::dfGUIManager.MaterialCache.Cache.cacheInstances.Count; i++)
				{
					global::dfGUIManager.MaterialCache.Cache.cacheInstances[i].Reset();
				}
			}

			// Token: 0x060041A3 RID: 16803 RVA: 0x000F13D0 File Offset: 0x000EF5D0
			public Material Obtain()
			{
				if (this.currentIndex < this.instances.Count)
				{
					return this.instances[this.currentIndex++];
				}
				this.currentIndex++;
				Material material = new Material(this.baseMaterial)
				{
					hideFlags = 4,
					name = string.Format("{0} (Copy {1})", this.baseMaterial.name, this.currentIndex)
				};
				this.instances.Add(material);
				return material;
			}

			// Token: 0x060041A4 RID: 16804 RVA: 0x000F1468 File Offset: 0x000EF668
			public void Reset()
			{
				this.currentIndex = 0;
			}

			// Token: 0x0400226D RID: 8813
			private static List<global::dfGUIManager.MaterialCache.Cache> cacheInstances = new List<global::dfGUIManager.MaterialCache.Cache>();

			// Token: 0x0400226E RID: 8814
			private Material baseMaterial;

			// Token: 0x0400226F RID: 8815
			private List<Material> instances = new List<Material>(10);

			// Token: 0x04002270 RID: 8816
			private int currentIndex;
		}
	}

	// Token: 0x0200079C RID: 1948
	// (Invoke) Token: 0x060041A6 RID: 16806
	[global::dfEventCategory("Modal Dialog")]
	public delegate void ModalPoppedCallback(global::dfControl control);

	// Token: 0x0200079D RID: 1949
	// (Invoke) Token: 0x060041AA RID: 16810
	[global::dfEventCategory("Global Callbacks")]
	public delegate void RenderCallback(global::dfGUIManager manager);
}
