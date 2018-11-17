using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020007A0 RID: 1952
[AddComponentMenu("Daikon Forge/User Interface/Input Manager")]
[Serializable]
public class dfInputManager : MonoBehaviour
{
	// Token: 0x17000C8A RID: 3210
	// (get) Token: 0x060041B4 RID: 16820 RVA: 0x000F1578 File Offset: 0x000EF778
	public static global::dfControl ControlUnderMouse
	{
		get
		{
			return global::dfInputManager.controlUnderMouse;
		}
	}

	// Token: 0x17000C8B RID: 3211
	// (get) Token: 0x060041B5 RID: 16821 RVA: 0x000F1580 File Offset: 0x000EF780
	// (set) Token: 0x060041B6 RID: 16822 RVA: 0x000F1588 File Offset: 0x000EF788
	public Camera RenderCamera
	{
		get
		{
			return this.renderCamera;
		}
		set
		{
			this.renderCamera = value;
		}
	}

	// Token: 0x17000C8C RID: 3212
	// (get) Token: 0x060041B7 RID: 16823 RVA: 0x000F1594 File Offset: 0x000EF794
	// (set) Token: 0x060041B8 RID: 16824 RVA: 0x000F159C File Offset: 0x000EF79C
	public bool UseTouch
	{
		get
		{
			return this.useTouch;
		}
		set
		{
			this.useTouch = value;
		}
	}

	// Token: 0x17000C8D RID: 3213
	// (get) Token: 0x060041B9 RID: 16825 RVA: 0x000F15A8 File Offset: 0x000EF7A8
	// (set) Token: 0x060041BA RID: 16826 RVA: 0x000F15B0 File Offset: 0x000EF7B0
	public int TouchClickRadius
	{
		get
		{
			return this.touchClickRadius;
		}
		set
		{
			this.touchClickRadius = Mathf.Max(0, value);
		}
	}

	// Token: 0x17000C8E RID: 3214
	// (get) Token: 0x060041BB RID: 16827 RVA: 0x000F15C0 File Offset: 0x000EF7C0
	// (set) Token: 0x060041BC RID: 16828 RVA: 0x000F15C8 File Offset: 0x000EF7C8
	public bool UseJoystick
	{
		get
		{
			return this.useJoystick;
		}
		set
		{
			this.useJoystick = value;
		}
	}

	// Token: 0x17000C8F RID: 3215
	// (get) Token: 0x060041BD RID: 16829 RVA: 0x000F15D4 File Offset: 0x000EF7D4
	// (set) Token: 0x060041BE RID: 16830 RVA: 0x000F15DC File Offset: 0x000EF7DC
	public KeyCode JoystickClickButton
	{
		get
		{
			return this.joystickClickButton;
		}
		set
		{
			this.joystickClickButton = value;
		}
	}

	// Token: 0x17000C90 RID: 3216
	// (get) Token: 0x060041BF RID: 16831 RVA: 0x000F15E8 File Offset: 0x000EF7E8
	// (set) Token: 0x060041C0 RID: 16832 RVA: 0x000F15F0 File Offset: 0x000EF7F0
	public string HorizontalAxis
	{
		get
		{
			return this.horizontalAxis;
		}
		set
		{
			this.horizontalAxis = value;
		}
	}

	// Token: 0x17000C91 RID: 3217
	// (get) Token: 0x060041C1 RID: 16833 RVA: 0x000F15FC File Offset: 0x000EF7FC
	// (set) Token: 0x060041C2 RID: 16834 RVA: 0x000F1604 File Offset: 0x000EF804
	public string VerticalAxis
	{
		get
		{
			return this.verticalAxis;
		}
		set
		{
			this.verticalAxis = value;
		}
	}

	// Token: 0x17000C92 RID: 3218
	// (get) Token: 0x060041C3 RID: 16835 RVA: 0x000F1610 File Offset: 0x000EF810
	// (set) Token: 0x060041C4 RID: 16836 RVA: 0x000F1618 File Offset: 0x000EF818
	public global::IInputAdapter Adapter
	{
		get
		{
			return this.adapter;
		}
		set
		{
			this.adapter = (value ?? new global::dfInputManager.DefaultInput());
		}
	}

	// Token: 0x17000C93 RID: 3219
	// (get) Token: 0x060041C5 RID: 16837 RVA: 0x000F1630 File Offset: 0x000EF830
	// (set) Token: 0x060041C6 RID: 16838 RVA: 0x000F1638 File Offset: 0x000EF838
	public bool RetainFocus
	{
		get
		{
			return this.retainFocus;
		}
		set
		{
			this.retainFocus = value;
		}
	}

	// Token: 0x060041C7 RID: 16839 RVA: 0x000F1644 File Offset: 0x000EF844
	public void Awake()
	{
	}

	// Token: 0x060041C8 RID: 16840 RVA: 0x000F1648 File Offset: 0x000EF848
	public void Start()
	{
	}

	// Token: 0x060041C9 RID: 16841 RVA: 0x000F164C File Offset: 0x000EF84C
	public void OnEnable()
	{
		this.mouseHandler = new global::dfInputManager.MouseInputManager();
		if (this.adapter == null)
		{
			Component component = (from c in base.GetComponents(typeof(MonoBehaviour))
			where typeof(global::IInputAdapter).IsAssignableFrom(c.GetType())
			select c).FirstOrDefault<Component>();
			this.adapter = (((global::IInputAdapter)component) ?? new global::dfInputManager.DefaultInput());
		}
	}

	// Token: 0x060041CA RID: 16842 RVA: 0x000F16C0 File Offset: 0x000EF8C0
	public void Update()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		global::dfControl activeControl = global::dfGUIManager.ActiveControl;
		this.processMouseInput();
		if (activeControl == null)
		{
			return;
		}
		if (this.processKeyboard())
		{
			return;
		}
		if (this.useJoystick)
		{
			for (int i = 0; i < global::dfInputManager.wasd.Length; i++)
			{
				if (Input.GetKey(global::dfInputManager.wasd[i]) || Input.GetKeyDown(global::dfInputManager.wasd[i]) || Input.GetKeyUp(global::dfInputManager.wasd[i]))
				{
					return;
				}
			}
			this.processJoystick();
		}
	}

	// Token: 0x060041CB RID: 16843 RVA: 0x000F175C File Offset: 0x000EF95C
	public void OnGUI()
	{
		Event current = Event.current;
		if (current == null)
		{
			return;
		}
		if (current.isKey && current.keyCode != null)
		{
			this.processKeyEvent(current.type, current.keyCode, current.modifiers);
			return;
		}
	}

	// Token: 0x060041CC RID: 16844 RVA: 0x000F17A8 File Offset: 0x000EF9A8
	private void processJoystick()
	{
		try
		{
			global::dfControl activeControl = global::dfGUIManager.ActiveControl;
			if (!(activeControl == null) && activeControl.transform.IsChildOf(base.transform))
			{
				float axis = this.adapter.GetAxis(this.horizontalAxis);
				float axis2 = this.adapter.GetAxis(this.verticalAxis);
				if (Mathf.Abs(axis) < 0.5f && Mathf.Abs(axis2) <= 0.5f)
				{
					this.lastAxisCheck = Time.deltaTime - this.axisPollingInterval;
				}
				if (Time.realtimeSinceStartup - this.lastAxisCheck > this.axisPollingInterval)
				{
					if (Mathf.Abs(axis) >= 0.5f)
					{
						this.lastAxisCheck = Time.realtimeSinceStartup;
						KeyCode key = (axis <= 0f) ? 276 : 275;
						activeControl.OnKeyDown(new global::dfKeyEventArgs(activeControl, key, false, false, false));
					}
					if (Mathf.Abs(axis2) >= 0.5f)
					{
						this.lastAxisCheck = Time.realtimeSinceStartup;
						KeyCode key2 = (axis2 <= 0f) ? 274 : 273;
						activeControl.OnKeyDown(new global::dfKeyEventArgs(activeControl, key2, false, false, false));
					}
				}
				if (this.joystickClickButton != null)
				{
					bool keyDown = this.adapter.GetKeyDown(this.joystickClickButton);
					if (keyDown)
					{
						Vector3 center = activeControl.GetCenter();
						Camera camera = activeControl.GetCamera();
						Ray ray = camera.ScreenPointToRay(camera.WorldToScreenPoint(center));
						global::dfMouseEventArgs args = new global::dfMouseEventArgs(activeControl, global::dfMouseButtons.Left, 0, ray, center, 0f);
						activeControl.OnMouseDown(args);
						this.buttonDownTarget = activeControl;
					}
					bool keyUp = this.adapter.GetKeyUp(this.joystickClickButton);
					if (keyUp)
					{
						if (this.buttonDownTarget == activeControl)
						{
							activeControl.DoClick();
						}
						Vector3 center2 = activeControl.GetCenter();
						Camera camera2 = activeControl.GetCamera();
						Ray ray2 = camera2.ScreenPointToRay(camera2.WorldToScreenPoint(center2));
						global::dfMouseEventArgs args2 = new global::dfMouseEventArgs(activeControl, global::dfMouseButtons.Left, 0, ray2, center2, 0f);
						activeControl.OnMouseUp(args2);
						this.buttonDownTarget = null;
					}
				}
				for (KeyCode keyCode = 350; keyCode <= 369; keyCode++)
				{
					bool keyDown2 = this.adapter.GetKeyDown(keyCode);
					if (keyDown2)
					{
						activeControl.OnKeyDown(new global::dfKeyEventArgs(activeControl, keyCode, false, false, false));
					}
				}
			}
		}
		catch (UnityException ex)
		{
			Debug.LogError(ex.ToString(), this);
			this.useJoystick = false;
		}
	}

	// Token: 0x060041CD RID: 16845 RVA: 0x000F1A48 File Offset: 0x000EFC48
	private void processKeyEvent(EventType eventType, KeyCode keyCode, EventModifiers modifiers)
	{
		global::dfControl activeControl = global::dfGUIManager.ActiveControl;
		if (activeControl == null || !activeControl.IsEnabled || !activeControl.transform.IsChildOf(base.transform))
		{
			return;
		}
		bool control = (modifiers & 2) == 2;
		bool flag = (modifiers & 1) == 1;
		bool alt = (modifiers & 4) == 4;
		global::dfKeyEventArgs dfKeyEventArgs = new global::dfKeyEventArgs(activeControl, keyCode, control, flag, alt);
		if (keyCode >= 32 && keyCode <= 122)
		{
			char c = keyCode;
			dfKeyEventArgs.Character = ((!flag) ? char.ToLower(c) : char.ToUpper(c));
		}
		if (eventType == 4)
		{
			activeControl.OnKeyDown(dfKeyEventArgs);
		}
		else if (eventType == 5)
		{
			activeControl.OnKeyUp(dfKeyEventArgs);
		}
		if (dfKeyEventArgs.Used || eventType == 5)
		{
			return;
		}
	}

	// Token: 0x060041CE RID: 16846 RVA: 0x000F1B18 File Offset: 0x000EFD18
	private bool processKeyboard()
	{
		global::dfControl activeControl = global::dfGUIManager.ActiveControl;
		if (activeControl == null || string.IsNullOrEmpty(Input.inputString) || !activeControl.transform.IsChildOf(base.transform))
		{
			return false;
		}
		foreach (char c in Input.inputString)
		{
			if (c != '\b' && c != '\n')
			{
				KeyCode key = c;
				activeControl.OnKeyPress(new global::dfKeyEventArgs(activeControl, key, false, false, false)
				{
					Character = c
				});
			}
		}
		return true;
	}

	// Token: 0x060041CF RID: 16847 RVA: 0x000F1BB8 File Offset: 0x000EFDB8
	private void processMouseInput()
	{
		Vector2 mousePosition = this.adapter.GetMousePosition();
		Ray ray = this.renderCamera.ScreenPointToRay(mousePosition);
		float num = this.renderCamera.farClipPlane - this.renderCamera.nearClipPlane;
		RaycastHit[] array = Physics.RaycastAll(ray, num, this.renderCamera.cullingMask);
		Array.Sort<RaycastHit>(array, new Comparison<RaycastHit>(global::dfInputManager.raycastHitSorter));
		global::dfInputManager.controlUnderMouse = this.clipCast(array);
		this.mouseHandler.ProcessInput(this.adapter, ray, global::dfInputManager.controlUnderMouse, this.retainFocus);
	}

	// Token: 0x060041D0 RID: 16848 RVA: 0x000F1C4C File Offset: 0x000EFE4C
	internal static int raycastHitSorter(RaycastHit lhs, RaycastHit rhs)
	{
		return lhs.distance.CompareTo(rhs.distance);
	}

	// Token: 0x060041D1 RID: 16849 RVA: 0x000F1C70 File Offset: 0x000EFE70
	internal global::dfControl clipCast(RaycastHit[] hits)
	{
		if (hits == null || hits.Length == 0)
		{
			return null;
		}
		global::dfControl dfControl = null;
		global::dfControl modalControl = global::dfGUIManager.GetModalControl();
		for (int i = hits.Length - 1; i >= 0; i--)
		{
			RaycastHit hit = hits[i];
			global::dfControl component = hit.transform.GetComponent<global::dfControl>();
			bool flag = component == null || (modalControl != null && !component.transform.IsChildOf(modalControl.transform)) || !component.enabled || global::dfInputManager.combinedOpacity(component) <= 0.01f || !component.IsEnabled || !component.IsVisible || !component.transform.IsChildOf(base.transform);
			if (!flag)
			{
				if (global::dfInputManager.isInsideClippingRegion(hit, component) && (dfControl == null || component.RenderOrder > dfControl.RenderOrder))
				{
					dfControl = component;
				}
			}
		}
		return dfControl;
	}

	// Token: 0x060041D2 RID: 16850 RVA: 0x000F1D80 File Offset: 0x000EFF80
	internal static bool isInsideClippingRegion(RaycastHit hit, global::dfControl control)
	{
		Vector3 point = hit.point;
		while (control != null)
		{
			Plane[] array = (!control.ClipChildren) ? null : control.GetClippingPlanes();
			if (array != null && array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (!array[i].GetSide(point))
					{
						return false;
					}
				}
			}
			control = control.Parent;
		}
		return true;
	}

	// Token: 0x060041D3 RID: 16851 RVA: 0x000F1E00 File Offset: 0x000F0000
	private static float combinedOpacity(global::dfControl control)
	{
		float num = 1f;
		while (control != null)
		{
			num *= control.Opacity;
			control = control.Parent;
		}
		return num;
	}

	// Token: 0x04002275 RID: 8821
	private static KeyCode[] wasd = new KeyCode[]
	{
		119,
		97,
		115,
		100,
		276,
		273,
		275,
		274
	};

	// Token: 0x04002276 RID: 8822
	private static global::dfControl controlUnderMouse = null;

	// Token: 0x04002277 RID: 8823
	[SerializeField]
	protected Camera renderCamera;

	// Token: 0x04002278 RID: 8824
	[SerializeField]
	protected bool useTouch = true;

	// Token: 0x04002279 RID: 8825
	[SerializeField]
	protected bool useJoystick;

	// Token: 0x0400227A RID: 8826
	[SerializeField]
	protected KeyCode joystickClickButton = 351;

	// Token: 0x0400227B RID: 8827
	[SerializeField]
	protected string horizontalAxis = "Horizontal";

	// Token: 0x0400227C RID: 8828
	[SerializeField]
	protected string verticalAxis = "Vertical";

	// Token: 0x0400227D RID: 8829
	[SerializeField]
	protected float axisPollingInterval = 0.15f;

	// Token: 0x0400227E RID: 8830
	[SerializeField]
	protected bool retainFocus;

	// Token: 0x0400227F RID: 8831
	[SerializeField]
	protected int touchClickRadius = 20;

	// Token: 0x04002280 RID: 8832
	private global::dfControl buttonDownTarget;

	// Token: 0x04002281 RID: 8833
	private global::dfInputManager.MouseInputManager mouseHandler;

	// Token: 0x04002282 RID: 8834
	private global::IInputAdapter adapter;

	// Token: 0x04002283 RID: 8835
	private float lastAxisCheck;

	// Token: 0x020007A1 RID: 1953
	private class TouchInputManager
	{
		// Token: 0x060041D5 RID: 16853 RVA: 0x000F1E50 File Offset: 0x000F0050
		private TouchInputManager()
		{
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x000F1E70 File Offset: 0x000F0070
		public TouchInputManager(global::dfInputManager manager)
		{
			this.manager = manager;
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x000F1E98 File Offset: 0x000F0098
		internal void Process(Transform transform, Camera renderCamera, global::dfList<Touch> touches, bool retainFocus)
		{
			for (int i = 0; i < touches.Count; i++)
			{
				Touch touch = touches[i];
				Ray ray = renderCamera.ScreenPointToRay(touch.position);
				float num = renderCamera.farClipPlane - renderCamera.nearClipPlane;
				RaycastHit[] hits = Physics.RaycastAll(ray, num, renderCamera.cullingMask);
				global::dfInputManager.controlUnderMouse = this.clipCast(transform, hits);
				if (global::dfInputManager.controlUnderMouse == null && touch.phase == null)
				{
					this.untracked.Add(touch.fingerId);
				}
				else if (this.untracked.Contains(touch.fingerId))
				{
					if (touch.phase == 3)
					{
						this.untracked.Remove(touch.fingerId);
					}
				}
				else
				{
					global::dfInputManager.TouchInputManager.TouchRaycast info = new global::dfInputManager.TouchInputManager.TouchRaycast(global::dfInputManager.controlUnderMouse, touch, ray);
					global::dfInputManager.TouchInputManager.ControlTouchTracker controlTouchTracker = this.tracked.FirstOrDefault((global::dfInputManager.TouchInputManager.ControlTouchTracker x) => x.IsTrackingFinger(info.FingerID));
					if (controlTouchTracker != null)
					{
						controlTouchTracker.Process(info);
					}
					else
					{
						bool flag = false;
						for (int j = 0; j < this.tracked.Count; j++)
						{
							if (this.tracked[j].Process(info))
							{
								flag = true;
								break;
							}
						}
						if (!flag && global::dfInputManager.controlUnderMouse != null)
						{
							if (!this.tracked.Any((global::dfInputManager.TouchInputManager.ControlTouchTracker x) => x.control == global::dfInputManager.controlUnderMouse))
							{
								if (global::dfInputManager.controlUnderMouse == null)
								{
									Debug.Log("Tracking touch with no control: " + touch.fingerId);
								}
								global::dfInputManager.TouchInputManager.ControlTouchTracker controlTouchTracker2 = new global::dfInputManager.TouchInputManager.ControlTouchTracker(this.manager, global::dfInputManager.controlUnderMouse);
								this.tracked.Add(controlTouchTracker2);
								controlTouchTracker2.Process(info);
							}
						}
					}
				}
			}
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x000F20A8 File Offset: 0x000F02A8
		private global::dfControl clipCast(Transform transform, RaycastHit[] hits)
		{
			if (hits == null || hits.Length == 0)
			{
				return null;
			}
			global::dfControl dfControl = null;
			global::dfControl modalControl = global::dfGUIManager.GetModalControl();
			for (int i = hits.Length - 1; i >= 0; i--)
			{
				RaycastHit hit = hits[i];
				global::dfControl component = hit.transform.GetComponent<global::dfControl>();
				bool flag = component == null || (modalControl != null && !component.transform.IsChildOf(modalControl.transform)) || !component.enabled || component.Opacity < 0.01f || !component.IsEnabled || !component.IsVisible || !component.transform.IsChildOf(transform);
				if (!flag)
				{
					if (this.isInsideClippingRegion(hit, component) && (dfControl == null || component.RenderOrder > dfControl.RenderOrder))
					{
						dfControl = component;
					}
				}
			}
			return dfControl;
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x000F21B4 File Offset: 0x000F03B4
		private bool isInsideClippingRegion(RaycastHit hit, global::dfControl control)
		{
			Vector3 point = hit.point;
			while (control != null)
			{
				Plane[] array = (!control.ClipChildren) ? null : control.GetClippingPlanes();
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!array[i].GetSide(point))
						{
							return false;
						}
					}
				}
				control = control.Parent;
			}
			return true;
		}

		// Token: 0x04002285 RID: 8837
		private List<global::dfInputManager.TouchInputManager.ControlTouchTracker> tracked = new List<global::dfInputManager.TouchInputManager.ControlTouchTracker>();

		// Token: 0x04002286 RID: 8838
		private List<int> untracked = new List<int>();

		// Token: 0x04002287 RID: 8839
		private global::dfInputManager manager;

		// Token: 0x020007A2 RID: 1954
		private class ControlTouchTracker
		{
			// Token: 0x060041DB RID: 16859 RVA: 0x000F2248 File Offset: 0x000F0448
			public ControlTouchTracker(global::dfInputManager manager, global::dfControl control)
			{
				this.manager = manager;
				this.control = control;
			}

			// Token: 0x17000C94 RID: 3220
			// (get) Token: 0x060041DC RID: 16860 RVA: 0x000F2280 File Offset: 0x000F0480
			public bool IsDragging
			{
				get
				{
					return this.dragState == global::dfDragDropState.Dragging;
				}
			}

			// Token: 0x17000C95 RID: 3221
			// (get) Token: 0x060041DD RID: 16861 RVA: 0x000F228C File Offset: 0x000F048C
			public int TouchCount
			{
				get
				{
					return this.touches.Count;
				}
			}

			// Token: 0x060041DE RID: 16862 RVA: 0x000F229C File Offset: 0x000F049C
			public bool IsTrackingFinger(int fingerID)
			{
				return this.touches.ContainsKey(fingerID);
			}

			// Token: 0x060041DF RID: 16863 RVA: 0x000F22AC File Offset: 0x000F04AC
			public bool Process(global::dfInputManager.TouchInputManager.TouchRaycast info)
			{
				if (this.IsDragging)
				{
					if (!this.capture.Contains(info.FingerID))
					{
						return false;
					}
					if (info.Phase == 2)
					{
						return true;
					}
					if (info.Phase == 4)
					{
						this.control.OnDragEnd(new global::dfDragEventArgs(this.control, global::dfDragDropState.Cancelled, this.dragData, info.ray, info.position));
						this.dragState = global::dfDragDropState.None;
						this.touches.Clear();
						this.capture.Clear();
						return true;
					}
					if (info.Phase != 3)
					{
						return true;
					}
					if (info.control == null || info.control == this.control)
					{
						this.control.OnDragEnd(new global::dfDragEventArgs(this.control, global::dfDragDropState.CancelledNoTarget, this.dragData, info.ray, info.position));
						this.dragState = global::dfDragDropState.None;
						this.touches.Clear();
						this.capture.Clear();
						return true;
					}
					global::dfDragEventArgs dfDragEventArgs = new global::dfDragEventArgs(info.control, global::dfDragDropState.Dragging, this.dragData, info.ray, info.position);
					info.control.OnDragDrop(dfDragEventArgs);
					if (!dfDragEventArgs.Used || dfDragEventArgs.State != global::dfDragDropState.Dropped)
					{
						dfDragEventArgs.State = global::dfDragDropState.Cancelled;
					}
					global::dfDragEventArgs dfDragEventArgs2 = new global::dfDragEventArgs(this.control, dfDragEventArgs.State, this.dragData, info.ray, info.position);
					dfDragEventArgs2.Target = info.control;
					this.control.OnDragEnd(dfDragEventArgs2);
					this.dragState = global::dfDragDropState.None;
					this.touches.Clear();
					this.capture.Clear();
					return true;
				}
				else if (!this.touches.ContainsKey(info.FingerID))
				{
					if (info.control != this.control)
					{
						return false;
					}
					this.touches[info.FingerID] = info;
					if (this.touches.Count == 1)
					{
						this.control.OnMouseEnter(info);
						if (info.Phase == null)
						{
							this.capture.Add(info.FingerID);
							this.control.OnMouseDown(info);
						}
						return true;
					}
					if (info.Phase == null)
					{
						this.control.OnMouseUp(info);
						this.control.OnMouseLeave(info);
						List<Touch> activeTouches = this.getActiveTouches();
						global::dfTouchEventArgs args = new global::dfTouchEventArgs(this.control, activeTouches, info.ray);
						this.control.OnMultiTouch(args);
					}
					return true;
				}
				else
				{
					if (info.Phase == 4 || info.Phase == 3)
					{
						global::dfInputManager.TouchInputManager.TouchRaycast touch = this.touches[info.FingerID];
						this.touches.Remove(info.FingerID);
						if (this.touches.Count == 0)
						{
							if (this.capture.Contains(info.FingerID))
							{
								if (this.canFireClickEvent(info, touch) && info.control == this.control)
								{
									if (info.touch.tapCount > 1)
									{
										this.control.OnDoubleClick(info);
									}
									else
									{
										this.control.OnClick(info);
									}
								}
								this.control.OnMouseUp(info);
							}
							this.control.OnMouseLeave(info);
							this.capture.Remove(info.FingerID);
							return true;
						}
						this.capture.Remove(info.FingerID);
						if (this.touches.Count == 1)
						{
							global::dfTouchEventArgs args2 = this.touches.Values.First<global::dfInputManager.TouchInputManager.TouchRaycast>();
							this.control.OnMouseEnter(args2);
							this.control.OnMouseDown(args2);
							return true;
						}
					}
					if (this.touches.Count > 1)
					{
						List<Touch> activeTouches2 = this.getActiveTouches();
						global::dfTouchEventArgs args3 = new global::dfTouchEventArgs(this.control, activeTouches2, info.ray);
						this.control.OnMultiTouch(args3);
						return true;
					}
					if (!this.IsDragging && info.Phase == 2)
					{
						if (info.control == this.control)
						{
							this.control.OnMouseHover(info);
							return true;
						}
						return false;
					}
					else
					{
						bool flag = this.capture.Contains(info.FingerID) && this.dragState == global::dfDragDropState.None && info.Phase == 1;
						if (flag)
						{
							global::dfDragEventArgs dfDragEventArgs3 = info;
							this.control.OnDragStart(dfDragEventArgs3);
							if (dfDragEventArgs3.State == global::dfDragDropState.Dragging && dfDragEventArgs3.Used)
							{
								this.dragState = global::dfDragDropState.Dragging;
								this.dragData = dfDragEventArgs3.Data;
								return true;
							}
							this.dragState = global::dfDragDropState.Denied;
						}
						if (info.control != this.control && !this.capture.Contains(info.FingerID))
						{
							this.control.OnMouseLeave(info);
							this.touches.Remove(info.FingerID);
							return true;
						}
						this.control.OnMouseMove(info);
						return true;
					}
				}
			}

			// Token: 0x060041E0 RID: 16864 RVA: 0x000F27F4 File Offset: 0x000F09F4
			private bool canFireClickEvent(global::dfInputManager.TouchInputManager.TouchRaycast info, global::dfInputManager.TouchInputManager.TouchRaycast touch)
			{
				if (this.manager.TouchClickRadius <= 0)
				{
					return true;
				}
				float num = Vector2.Distance(info.position, touch.position);
				return num < (float)this.manager.TouchClickRadius;
			}

			// Token: 0x060041E1 RID: 16865 RVA: 0x000F2838 File Offset: 0x000F0A38
			private List<Touch> getActiveTouches()
			{
				global::dfInputManager.TouchInputManager.ControlTouchTracker.<getActiveTouches>c__AnonStorey56 <getActiveTouches>c__AnonStorey = new global::dfInputManager.TouchInputManager.ControlTouchTracker.<getActiveTouches>c__AnonStorey56();
				Touch[] source = Input.touches;
				<getActiveTouches>c__AnonStorey.result = (from x in this.touches
				select x.Value.touch).ToList<Touch>();
				int i;
				for (i = 0; i < <getActiveTouches>c__AnonStorey.result.Count; i++)
				{
					<getActiveTouches>c__AnonStorey.result[i] = source.First((Touch x) => x.fingerId == <getActiveTouches>c__AnonStorey.result[i].fingerId);
				}
				return <getActiveTouches>c__AnonStorey.result;
			}

			// Token: 0x04002289 RID: 8841
			public global::dfControl control;

			// Token: 0x0400228A RID: 8842
			public Dictionary<int, global::dfInputManager.TouchInputManager.TouchRaycast> touches = new Dictionary<int, global::dfInputManager.TouchInputManager.TouchRaycast>();

			// Token: 0x0400228B RID: 8843
			public List<int> capture = new List<int>();

			// Token: 0x0400228C RID: 8844
			private global::dfInputManager manager;

			// Token: 0x0400228D RID: 8845
			private global::dfDragDropState dragState;

			// Token: 0x0400228E RID: 8846
			private object dragData;
		}

		// Token: 0x020007A5 RID: 1957
		private class TouchRaycast
		{
			// Token: 0x060041E6 RID: 16870 RVA: 0x000F2940 File Offset: 0x000F0B40
			public TouchRaycast(global::dfControl control, Touch touch, Ray ray)
			{
				this.control = control;
				this.touch = touch;
				this.ray = ray;
				this.position = touch.position;
			}

			// Token: 0x17000C96 RID: 3222
			// (get) Token: 0x060041E7 RID: 16871 RVA: 0x000F2978 File Offset: 0x000F0B78
			public int FingerID
			{
				get
				{
					return this.touch.fingerId;
				}
			}

			// Token: 0x17000C97 RID: 3223
			// (get) Token: 0x060041E8 RID: 16872 RVA: 0x000F2988 File Offset: 0x000F0B88
			public TouchPhase Phase
			{
				get
				{
					return this.touch.phase;
				}
			}

			// Token: 0x060041E9 RID: 16873 RVA: 0x000F2998 File Offset: 0x000F0B98
			public static implicit operator global::dfTouchEventArgs(global::dfInputManager.TouchInputManager.TouchRaycast touch)
			{
				return new global::dfTouchEventArgs(touch.control, touch.touch, touch.ray);
			}

			// Token: 0x060041EA RID: 16874 RVA: 0x000F29C0 File Offset: 0x000F0BC0
			public static implicit operator global::dfDragEventArgs(global::dfInputManager.TouchInputManager.TouchRaycast touch)
			{
				return new global::dfDragEventArgs(touch.control, global::dfDragDropState.None, null, touch.ray, touch.position);
			}

			// Token: 0x04002293 RID: 8851
			public global::dfControl control;

			// Token: 0x04002294 RID: 8852
			public Touch touch;

			// Token: 0x04002295 RID: 8853
			public Ray ray;

			// Token: 0x04002296 RID: 8854
			public Vector2 position;
		}
	}

	// Token: 0x020007A7 RID: 1959
	private class MouseInputManager
	{
		// Token: 0x060041EE RID: 16878 RVA: 0x000F2A38 File Offset: 0x000F0C38
		public void ProcessInput(global::IInputAdapter adapter, Ray ray, global::dfControl control, bool retainFocusSetting)
		{
			Vector2 mousePosition = adapter.GetMousePosition();
			this.buttonsDown = global::dfMouseButtons.None;
			this.buttonsReleased = global::dfMouseButtons.None;
			this.buttonsPressed = global::dfMouseButtons.None;
			global::dfInputManager.MouseInputManager.getMouseButtonInfo(adapter, ref this.buttonsDown, ref this.buttonsReleased, ref this.buttonsPressed);
			float num = adapter.GetAxis("Mouse ScrollWheel");
			if (!Mathf.Approximately(num, 0f))
			{
				num = Mathf.Sign(num) * Mathf.Max(1f, Mathf.Abs(num));
			}
			this.mouseMoveDelta = mousePosition - this.lastPosition;
			this.lastPosition = mousePosition;
			if (this.dragState == global::dfDragDropState.Dragging)
			{
				if (this.buttonsReleased != global::dfMouseButtons.None)
				{
					if (control != null && control != this.activeControl)
					{
						global::dfDragEventArgs dfDragEventArgs = new global::dfDragEventArgs(control, global::dfDragDropState.Dragging, this.dragData, ray, mousePosition);
						control.OnDragDrop(dfDragEventArgs);
						if (!dfDragEventArgs.Used || dfDragEventArgs.State == global::dfDragDropState.Dragging)
						{
							dfDragEventArgs.State = global::dfDragDropState.Cancelled;
						}
						dfDragEventArgs = new global::dfDragEventArgs(this.activeControl, dfDragEventArgs.State, dfDragEventArgs.Data, ray, mousePosition);
						dfDragEventArgs.Target = control;
						this.activeControl.OnDragEnd(dfDragEventArgs);
					}
					else
					{
						global::dfDragDropState state = (!(control == null)) ? global::dfDragDropState.Cancelled : global::dfDragDropState.CancelledNoTarget;
						global::dfDragEventArgs args = new global::dfDragEventArgs(this.activeControl, state, this.dragData, ray, mousePosition);
						this.activeControl.OnDragEnd(args);
					}
					this.dragState = global::dfDragDropState.None;
					this.lastDragControl = null;
					this.activeControl = null;
					this.lastClickTime = 0f;
					this.lastHoverTime = 0f;
					this.lastPosition = mousePosition;
					return;
				}
				if (control == this.activeControl)
				{
					return;
				}
				if (control != this.lastDragControl)
				{
					if (this.lastDragControl != null)
					{
						global::dfDragEventArgs args2 = new global::dfDragEventArgs(this.lastDragControl, this.dragState, this.dragData, ray, mousePosition);
						this.lastDragControl.OnDragLeave(args2);
					}
					if (control != null)
					{
						global::dfDragEventArgs args3 = new global::dfDragEventArgs(control, this.dragState, this.dragData, ray, mousePosition);
						control.OnDragEnter(args3);
					}
					this.lastDragControl = control;
					return;
				}
				if (control != null && Vector2.Distance(mousePosition, this.lastPosition) > 1f)
				{
					global::dfDragEventArgs args4 = new global::dfDragEventArgs(control, this.dragState, this.dragData, ray, mousePosition);
					control.OnDragOver(args4);
				}
				return;
			}
			else if (this.buttonsReleased != global::dfMouseButtons.None)
			{
				this.lastHoverTime = Time.realtimeSinceStartup + 0.25f;
				if (this.activeControl == null)
				{
					this.setActive(control, mousePosition, ray);
					return;
				}
				if (this.activeControl == control && this.buttonsDown == global::dfMouseButtons.None)
				{
					if (Time.realtimeSinceStartup - this.lastClickTime < 0.25f)
					{
						this.lastClickTime = 0f;
						this.activeControl.OnDoubleClick(new global::dfMouseEventArgs(this.activeControl, this.buttonsReleased, 1, ray, mousePosition, num));
					}
					else
					{
						this.lastClickTime = Time.realtimeSinceStartup;
						this.activeControl.OnClick(new global::dfMouseEventArgs(this.activeControl, this.buttonsReleased, 1, ray, mousePosition, num));
					}
				}
				this.activeControl.OnMouseUp(new global::dfMouseEventArgs(this.activeControl, this.buttonsReleased, 0, ray, mousePosition, num));
				if (this.buttonsDown == global::dfMouseButtons.None && this.activeControl != control)
				{
					this.setActive(null, mousePosition, ray);
				}
				return;
			}
			else
			{
				if (this.buttonsPressed != global::dfMouseButtons.None)
				{
					this.lastHoverTime = Time.realtimeSinceStartup + 0.25f;
					if (this.activeControl != null)
					{
						this.activeControl.OnMouseDown(new global::dfMouseEventArgs(this.activeControl, this.buttonsPressed, 0, ray, mousePosition, num));
					}
					else
					{
						this.setActive(control, mousePosition, ray);
						if (control != null)
						{
							control.OnMouseDown(new global::dfMouseEventArgs(control, this.buttonsPressed, 0, ray, mousePosition, num));
						}
						else if (!retainFocusSetting)
						{
							global::dfControl dfControl = global::dfGUIManager.ActiveControl;
							if (dfControl != null)
							{
								dfControl.Unfocus();
							}
						}
					}
					return;
				}
				if (this.activeControl != null && this.activeControl == control && this.mouseMoveDelta.magnitude == 0f && Time.realtimeSinceStartup - this.lastHoverTime > 0.1f)
				{
					this.activeControl.OnMouseHover(new global::dfMouseEventArgs(this.activeControl, this.buttonsDown, 0, ray, mousePosition, num));
					this.lastHoverTime = Time.realtimeSinceStartup;
				}
				if (this.buttonsDown == global::dfMouseButtons.None)
				{
					if (num != 0f && control != null)
					{
						this.setActive(control, mousePosition, ray);
						control.OnMouseWheel(new global::dfMouseEventArgs(control, this.buttonsDown, 0, ray, mousePosition, num));
						return;
					}
					this.setActive(control, mousePosition, ray);
				}
				else if (this.activeControl != null)
				{
					if (!(control != null) || control.RenderOrder > this.activeControl.RenderOrder)
					{
					}
					if (this.mouseMoveDelta.magnitude >= 2f && (this.buttonsDown & (global::dfMouseButtons.Left | global::dfMouseButtons.Right)) != global::dfMouseButtons.None && this.dragState != global::dfDragDropState.Denied)
					{
						global::dfDragEventArgs dfDragEventArgs2 = new global::dfDragEventArgs(this.activeControl)
						{
							Position = mousePosition
						};
						this.activeControl.OnDragStart(dfDragEventArgs2);
						if (dfDragEventArgs2.State == global::dfDragDropState.Dragging)
						{
							this.dragState = global::dfDragDropState.Dragging;
							this.dragData = dfDragEventArgs2.Data;
							return;
						}
						this.dragState = global::dfDragDropState.Denied;
					}
				}
				if (this.activeControl != null && this.mouseMoveDelta.magnitude >= 1f)
				{
					global::dfMouseEventArgs args5 = new global::dfMouseEventArgs(this.activeControl, this.buttonsDown, 0, ray, mousePosition, num)
					{
						MoveDelta = this.mouseMoveDelta
					};
					this.activeControl.OnMouseMove(args5);
				}
				return;
			}
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000F3024 File Offset: 0x000F1224
		private static void getMouseButtonInfo(global::IInputAdapter adapter, ref global::dfMouseButtons buttonsDown, ref global::dfMouseButtons buttonsReleased, ref global::dfMouseButtons buttonsPressed)
		{
			for (int i = 0; i < 3; i++)
			{
				if (adapter.GetMouseButton(i))
				{
					buttonsDown |= (global::dfMouseButtons)(1 << i);
				}
				if (adapter.GetMouseButtonUp(i))
				{
					buttonsReleased |= (global::dfMouseButtons)(1 << i);
				}
				if (adapter.GetMouseButtonDown(i))
				{
					buttonsPressed |= (global::dfMouseButtons)(1 << i);
				}
			}
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000F3088 File Offset: 0x000F1288
		private void setActive(global::dfControl control, Vector2 position, Ray ray)
		{
			if (this.activeControl != null && this.activeControl != control)
			{
				this.activeControl.OnMouseLeave(new global::dfMouseEventArgs(this.activeControl)
				{
					Position = position,
					Ray = ray
				});
			}
			if (control != null && control != this.activeControl)
			{
				this.lastClickTime = 0f;
				this.lastHoverTime = Time.realtimeSinceStartup + 0.25f;
				control.OnMouseEnter(new global::dfMouseEventArgs(control)
				{
					Position = position,
					Ray = ray
				});
			}
			this.activeControl = control;
			this.lastPosition = position;
			this.dragState = global::dfDragDropState.None;
		}

		// Token: 0x04002298 RID: 8856
		private const string scrollAxisName = "Mouse ScrollWheel";

		// Token: 0x04002299 RID: 8857
		private const float DOUBLECLICK_TIME = 0.25f;

		// Token: 0x0400229A RID: 8858
		private const int DRAG_START_DELTA = 2;

		// Token: 0x0400229B RID: 8859
		private const float HOVER_NOTIFICATION_FREQUENCY = 0.1f;

		// Token: 0x0400229C RID: 8860
		private const float HOVER_NOTIFICATION_BEGIN = 0.25f;

		// Token: 0x0400229D RID: 8861
		private global::dfControl activeControl;

		// Token: 0x0400229E RID: 8862
		private Vector2 lastPosition = Vector2.one * -2.14748365E+09f;

		// Token: 0x0400229F RID: 8863
		private Vector2 mouseMoveDelta = Vector2.zero;

		// Token: 0x040022A0 RID: 8864
		private float lastClickTime;

		// Token: 0x040022A1 RID: 8865
		private float lastHoverTime;

		// Token: 0x040022A2 RID: 8866
		private global::dfDragDropState dragState;

		// Token: 0x040022A3 RID: 8867
		private object dragData;

		// Token: 0x040022A4 RID: 8868
		private global::dfControl lastDragControl;

		// Token: 0x040022A5 RID: 8869
		private global::dfMouseButtons buttonsDown;

		// Token: 0x040022A6 RID: 8870
		private global::dfMouseButtons buttonsReleased;

		// Token: 0x040022A7 RID: 8871
		private global::dfMouseButtons buttonsPressed;
	}

	// Token: 0x020007A8 RID: 1960
	private class DefaultInput : global::IInputAdapter
	{
		// Token: 0x060041F2 RID: 16882 RVA: 0x000F3150 File Offset: 0x000F1350
		public bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000F3158 File Offset: 0x000F1358
		public bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUp(key);
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000F3160 File Offset: 0x000F1360
		public float GetAxis(string axisName)
		{
			return Input.GetAxis(axisName);
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000F3168 File Offset: 0x000F1368
		public Vector2 GetMousePosition()
		{
			return Input.mousePosition;
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000F3174 File Offset: 0x000F1374
		public bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000F317C File Offset: 0x000F137C
		public bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x000F3184 File Offset: 0x000F1384
		public bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}
	}
}
