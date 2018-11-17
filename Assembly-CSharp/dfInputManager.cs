using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020006D1 RID: 1745
[AddComponentMenu("Daikon Forge/User Interface/Input Manager")]
[Serializable]
public class dfInputManager : MonoBehaviour
{
	// Token: 0x17000C06 RID: 3078
	// (get) Token: 0x06003D9D RID: 15773 RVA: 0x000E89D4 File Offset: 0x000E6BD4
	public static dfControl ControlUnderMouse
	{
		get
		{
			return dfInputManager.controlUnderMouse;
		}
	}

	// Token: 0x17000C07 RID: 3079
	// (get) Token: 0x06003D9E RID: 15774 RVA: 0x000E89DC File Offset: 0x000E6BDC
	// (set) Token: 0x06003D9F RID: 15775 RVA: 0x000E89E4 File Offset: 0x000E6BE4
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

	// Token: 0x17000C08 RID: 3080
	// (get) Token: 0x06003DA0 RID: 15776 RVA: 0x000E89F0 File Offset: 0x000E6BF0
	// (set) Token: 0x06003DA1 RID: 15777 RVA: 0x000E89F8 File Offset: 0x000E6BF8
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

	// Token: 0x17000C09 RID: 3081
	// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x000E8A04 File Offset: 0x000E6C04
	// (set) Token: 0x06003DA3 RID: 15779 RVA: 0x000E8A0C File Offset: 0x000E6C0C
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

	// Token: 0x17000C0A RID: 3082
	// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x000E8A1C File Offset: 0x000E6C1C
	// (set) Token: 0x06003DA5 RID: 15781 RVA: 0x000E8A24 File Offset: 0x000E6C24
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

	// Token: 0x17000C0B RID: 3083
	// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x000E8A30 File Offset: 0x000E6C30
	// (set) Token: 0x06003DA7 RID: 15783 RVA: 0x000E8A38 File Offset: 0x000E6C38
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

	// Token: 0x17000C0C RID: 3084
	// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x000E8A44 File Offset: 0x000E6C44
	// (set) Token: 0x06003DA9 RID: 15785 RVA: 0x000E8A4C File Offset: 0x000E6C4C
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

	// Token: 0x17000C0D RID: 3085
	// (get) Token: 0x06003DAA RID: 15786 RVA: 0x000E8A58 File Offset: 0x000E6C58
	// (set) Token: 0x06003DAB RID: 15787 RVA: 0x000E8A60 File Offset: 0x000E6C60
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

	// Token: 0x17000C0E RID: 3086
	// (get) Token: 0x06003DAC RID: 15788 RVA: 0x000E8A6C File Offset: 0x000E6C6C
	// (set) Token: 0x06003DAD RID: 15789 RVA: 0x000E8A74 File Offset: 0x000E6C74
	public IInputAdapter Adapter
	{
		get
		{
			return this.adapter;
		}
		set
		{
			this.adapter = (value ?? new dfInputManager.DefaultInput());
		}
	}

	// Token: 0x17000C0F RID: 3087
	// (get) Token: 0x06003DAE RID: 15790 RVA: 0x000E8A8C File Offset: 0x000E6C8C
	// (set) Token: 0x06003DAF RID: 15791 RVA: 0x000E8A94 File Offset: 0x000E6C94
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

	// Token: 0x06003DB0 RID: 15792 RVA: 0x000E8AA0 File Offset: 0x000E6CA0
	public void Awake()
	{
	}

	// Token: 0x06003DB1 RID: 15793 RVA: 0x000E8AA4 File Offset: 0x000E6CA4
	public void Start()
	{
	}

	// Token: 0x06003DB2 RID: 15794 RVA: 0x000E8AA8 File Offset: 0x000E6CA8
	public void OnEnable()
	{
		this.mouseHandler = new dfInputManager.MouseInputManager();
		if (this.adapter == null)
		{
			Component component = (from c in base.GetComponents(typeof(MonoBehaviour))
			where typeof(IInputAdapter).IsAssignableFrom(c.GetType())
			select c).FirstOrDefault<Component>();
			this.adapter = (((IInputAdapter)component) ?? new dfInputManager.DefaultInput());
		}
	}

	// Token: 0x06003DB3 RID: 15795 RVA: 0x000E8B1C File Offset: 0x000E6D1C
	public void Update()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		dfControl activeControl = dfGUIManager.ActiveControl;
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
			for (int i = 0; i < dfInputManager.wasd.Length; i++)
			{
				if (Input.GetKey(dfInputManager.wasd[i]) || Input.GetKeyDown(dfInputManager.wasd[i]) || Input.GetKeyUp(dfInputManager.wasd[i]))
				{
					return;
				}
			}
			this.processJoystick();
		}
	}

	// Token: 0x06003DB4 RID: 15796 RVA: 0x000E8BB8 File Offset: 0x000E6DB8
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

	// Token: 0x06003DB5 RID: 15797 RVA: 0x000E8C04 File Offset: 0x000E6E04
	private void processJoystick()
	{
		try
		{
			dfControl activeControl = dfGUIManager.ActiveControl;
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
						activeControl.OnKeyDown(new dfKeyEventArgs(activeControl, key, false, false, false));
					}
					if (Mathf.Abs(axis2) >= 0.5f)
					{
						this.lastAxisCheck = Time.realtimeSinceStartup;
						KeyCode key2 = (axis2 <= 0f) ? 274 : 273;
						activeControl.OnKeyDown(new dfKeyEventArgs(activeControl, key2, false, false, false));
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
						dfMouseEventArgs args = new dfMouseEventArgs(activeControl, dfMouseButtons.Left, 0, ray, center, 0f);
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
						dfMouseEventArgs args2 = new dfMouseEventArgs(activeControl, dfMouseButtons.Left, 0, ray2, center2, 0f);
						activeControl.OnMouseUp(args2);
						this.buttonDownTarget = null;
					}
				}
				for (KeyCode keyCode = 350; keyCode <= 369; keyCode++)
				{
					bool keyDown2 = this.adapter.GetKeyDown(keyCode);
					if (keyDown2)
					{
						activeControl.OnKeyDown(new dfKeyEventArgs(activeControl, keyCode, false, false, false));
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

	// Token: 0x06003DB6 RID: 15798 RVA: 0x000E8EA4 File Offset: 0x000E70A4
	private void processKeyEvent(EventType eventType, KeyCode keyCode, EventModifiers modifiers)
	{
		dfControl activeControl = dfGUIManager.ActiveControl;
		if (activeControl == null || !activeControl.IsEnabled || !activeControl.transform.IsChildOf(base.transform))
		{
			return;
		}
		bool control = (modifiers & 2) == 2;
		bool flag = (modifiers & 1) == 1;
		bool alt = (modifiers & 4) == 4;
		dfKeyEventArgs dfKeyEventArgs = new dfKeyEventArgs(activeControl, keyCode, control, flag, alt);
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

	// Token: 0x06003DB7 RID: 15799 RVA: 0x000E8F74 File Offset: 0x000E7174
	private bool processKeyboard()
	{
		dfControl activeControl = dfGUIManager.ActiveControl;
		if (activeControl == null || string.IsNullOrEmpty(Input.inputString) || !activeControl.transform.IsChildOf(base.transform))
		{
			return false;
		}
		foreach (char c in Input.inputString)
		{
			if (c != '\b' && c != '\n')
			{
				KeyCode key = c;
				activeControl.OnKeyPress(new dfKeyEventArgs(activeControl, key, false, false, false)
				{
					Character = c
				});
			}
		}
		return true;
	}

	// Token: 0x06003DB8 RID: 15800 RVA: 0x000E9014 File Offset: 0x000E7214
	private void processMouseInput()
	{
		Vector2 mousePosition = this.adapter.GetMousePosition();
		Ray ray = this.renderCamera.ScreenPointToRay(mousePosition);
		float num = this.renderCamera.farClipPlane - this.renderCamera.nearClipPlane;
		RaycastHit[] array = Physics.RaycastAll(ray, num, this.renderCamera.cullingMask);
		Array.Sort<RaycastHit>(array, new Comparison<RaycastHit>(dfInputManager.raycastHitSorter));
		dfInputManager.controlUnderMouse = this.clipCast(array);
		this.mouseHandler.ProcessInput(this.adapter, ray, dfInputManager.controlUnderMouse, this.retainFocus);
	}

	// Token: 0x06003DB9 RID: 15801 RVA: 0x000E90A8 File Offset: 0x000E72A8
	internal static int raycastHitSorter(RaycastHit lhs, RaycastHit rhs)
	{
		return lhs.distance.CompareTo(rhs.distance);
	}

	// Token: 0x06003DBA RID: 15802 RVA: 0x000E90CC File Offset: 0x000E72CC
	internal dfControl clipCast(RaycastHit[] hits)
	{
		if (hits == null || hits.Length == 0)
		{
			return null;
		}
		dfControl dfControl = null;
		dfControl modalControl = dfGUIManager.GetModalControl();
		for (int i = hits.Length - 1; i >= 0; i--)
		{
			RaycastHit hit = hits[i];
			dfControl component = hit.transform.GetComponent<dfControl>();
			bool flag = component == null || (modalControl != null && !component.transform.IsChildOf(modalControl.transform)) || !component.enabled || dfInputManager.combinedOpacity(component) <= 0.01f || !component.IsEnabled || !component.IsVisible || !component.transform.IsChildOf(base.transform);
			if (!flag)
			{
				if (dfInputManager.isInsideClippingRegion(hit, component) && (dfControl == null || component.RenderOrder > dfControl.RenderOrder))
				{
					dfControl = component;
				}
			}
		}
		return dfControl;
	}

	// Token: 0x06003DBB RID: 15803 RVA: 0x000E91DC File Offset: 0x000E73DC
	internal static bool isInsideClippingRegion(RaycastHit hit, dfControl control)
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

	// Token: 0x06003DBC RID: 15804 RVA: 0x000E925C File Offset: 0x000E745C
	private static float combinedOpacity(dfControl control)
	{
		float num = 1f;
		while (control != null)
		{
			num *= control.Opacity;
			control = control.Parent;
		}
		return num;
	}

	// Token: 0x04002070 RID: 8304
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

	// Token: 0x04002071 RID: 8305
	private static dfControl controlUnderMouse = null;

	// Token: 0x04002072 RID: 8306
	[SerializeField]
	protected Camera renderCamera;

	// Token: 0x04002073 RID: 8307
	[SerializeField]
	protected bool useTouch = true;

	// Token: 0x04002074 RID: 8308
	[SerializeField]
	protected bool useJoystick;

	// Token: 0x04002075 RID: 8309
	[SerializeField]
	protected KeyCode joystickClickButton = 351;

	// Token: 0x04002076 RID: 8310
	[SerializeField]
	protected string horizontalAxis = "Horizontal";

	// Token: 0x04002077 RID: 8311
	[SerializeField]
	protected string verticalAxis = "Vertical";

	// Token: 0x04002078 RID: 8312
	[SerializeField]
	protected float axisPollingInterval = 0.15f;

	// Token: 0x04002079 RID: 8313
	[SerializeField]
	protected bool retainFocus;

	// Token: 0x0400207A RID: 8314
	[SerializeField]
	protected int touchClickRadius = 20;

	// Token: 0x0400207B RID: 8315
	private dfControl buttonDownTarget;

	// Token: 0x0400207C RID: 8316
	private dfInputManager.MouseInputManager mouseHandler;

	// Token: 0x0400207D RID: 8317
	private IInputAdapter adapter;

	// Token: 0x0400207E RID: 8318
	private float lastAxisCheck;

	// Token: 0x020006D2 RID: 1746
	private class TouchInputManager
	{
		// Token: 0x06003DBE RID: 15806 RVA: 0x000E92AC File Offset: 0x000E74AC
		private TouchInputManager()
		{
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x000E92CC File Offset: 0x000E74CC
		public TouchInputManager(dfInputManager manager)
		{
			this.manager = manager;
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000E92F4 File Offset: 0x000E74F4
		internal void Process(Transform transform, Camera renderCamera, dfList<Touch> touches, bool retainFocus)
		{
			for (int i = 0; i < touches.Count; i++)
			{
				Touch touch = touches[i];
				Ray ray = renderCamera.ScreenPointToRay(touch.position);
				float num = renderCamera.farClipPlane - renderCamera.nearClipPlane;
				RaycastHit[] hits = Physics.RaycastAll(ray, num, renderCamera.cullingMask);
				dfInputManager.controlUnderMouse = this.clipCast(transform, hits);
				if (dfInputManager.controlUnderMouse == null && touch.phase == null)
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
					dfInputManager.TouchInputManager.TouchRaycast info = new dfInputManager.TouchInputManager.TouchRaycast(dfInputManager.controlUnderMouse, touch, ray);
					dfInputManager.TouchInputManager.ControlTouchTracker controlTouchTracker = this.tracked.FirstOrDefault((dfInputManager.TouchInputManager.ControlTouchTracker x) => x.IsTrackingFinger(info.FingerID));
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
						if (!flag && dfInputManager.controlUnderMouse != null)
						{
							if (!this.tracked.Any((dfInputManager.TouchInputManager.ControlTouchTracker x) => x.control == dfInputManager.controlUnderMouse))
							{
								if (dfInputManager.controlUnderMouse == null)
								{
									Debug.Log("Tracking touch with no control: " + touch.fingerId);
								}
								dfInputManager.TouchInputManager.ControlTouchTracker controlTouchTracker2 = new dfInputManager.TouchInputManager.ControlTouchTracker(this.manager, dfInputManager.controlUnderMouse);
								this.tracked.Add(controlTouchTracker2);
								controlTouchTracker2.Process(info);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000E9504 File Offset: 0x000E7704
		private dfControl clipCast(Transform transform, RaycastHit[] hits)
		{
			if (hits == null || hits.Length == 0)
			{
				return null;
			}
			dfControl dfControl = null;
			dfControl modalControl = dfGUIManager.GetModalControl();
			for (int i = hits.Length - 1; i >= 0; i--)
			{
				RaycastHit hit = hits[i];
				dfControl component = hit.transform.GetComponent<dfControl>();
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

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000E9610 File Offset: 0x000E7810
		private bool isInsideClippingRegion(RaycastHit hit, dfControl control)
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

		// Token: 0x04002080 RID: 8320
		private List<dfInputManager.TouchInputManager.ControlTouchTracker> tracked = new List<dfInputManager.TouchInputManager.ControlTouchTracker>();

		// Token: 0x04002081 RID: 8321
		private List<int> untracked = new List<int>();

		// Token: 0x04002082 RID: 8322
		private dfInputManager manager;

		// Token: 0x020006D3 RID: 1747
		private class ControlTouchTracker
		{
			// Token: 0x06003DC4 RID: 15812 RVA: 0x000E96A4 File Offset: 0x000E78A4
			public ControlTouchTracker(dfInputManager manager, dfControl control)
			{
				this.manager = manager;
				this.control = control;
			}

			// Token: 0x17000C10 RID: 3088
			// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x000E96DC File Offset: 0x000E78DC
			public bool IsDragging
			{
				get
				{
					return this.dragState == dfDragDropState.Dragging;
				}
			}

			// Token: 0x17000C11 RID: 3089
			// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000E96E8 File Offset: 0x000E78E8
			public int TouchCount
			{
				get
				{
					return this.touches.Count;
				}
			}

			// Token: 0x06003DC7 RID: 15815 RVA: 0x000E96F8 File Offset: 0x000E78F8
			public bool IsTrackingFinger(int fingerID)
			{
				return this.touches.ContainsKey(fingerID);
			}

			// Token: 0x06003DC8 RID: 15816 RVA: 0x000E9708 File Offset: 0x000E7908
			public bool Process(dfInputManager.TouchInputManager.TouchRaycast info)
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
						this.control.OnDragEnd(new dfDragEventArgs(this.control, dfDragDropState.Cancelled, this.dragData, info.ray, info.position));
						this.dragState = dfDragDropState.None;
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
						this.control.OnDragEnd(new dfDragEventArgs(this.control, dfDragDropState.CancelledNoTarget, this.dragData, info.ray, info.position));
						this.dragState = dfDragDropState.None;
						this.touches.Clear();
						this.capture.Clear();
						return true;
					}
					dfDragEventArgs dfDragEventArgs = new dfDragEventArgs(info.control, dfDragDropState.Dragging, this.dragData, info.ray, info.position);
					info.control.OnDragDrop(dfDragEventArgs);
					if (!dfDragEventArgs.Used || dfDragEventArgs.State != dfDragDropState.Dropped)
					{
						dfDragEventArgs.State = dfDragDropState.Cancelled;
					}
					dfDragEventArgs dfDragEventArgs2 = new dfDragEventArgs(this.control, dfDragEventArgs.State, this.dragData, info.ray, info.position);
					dfDragEventArgs2.Target = info.control;
					this.control.OnDragEnd(dfDragEventArgs2);
					this.dragState = dfDragDropState.None;
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
						dfTouchEventArgs args = new dfTouchEventArgs(this.control, activeTouches, info.ray);
						this.control.OnMultiTouch(args);
					}
					return true;
				}
				else
				{
					if (info.Phase == 4 || info.Phase == 3)
					{
						dfInputManager.TouchInputManager.TouchRaycast touch = this.touches[info.FingerID];
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
							dfTouchEventArgs args2 = this.touches.Values.First<dfInputManager.TouchInputManager.TouchRaycast>();
							this.control.OnMouseEnter(args2);
							this.control.OnMouseDown(args2);
							return true;
						}
					}
					if (this.touches.Count > 1)
					{
						List<Touch> activeTouches2 = this.getActiveTouches();
						dfTouchEventArgs args3 = new dfTouchEventArgs(this.control, activeTouches2, info.ray);
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
						bool flag = this.capture.Contains(info.FingerID) && this.dragState == dfDragDropState.None && info.Phase == 1;
						if (flag)
						{
							dfDragEventArgs dfDragEventArgs3 = info;
							this.control.OnDragStart(dfDragEventArgs3);
							if (dfDragEventArgs3.State == dfDragDropState.Dragging && dfDragEventArgs3.Used)
							{
								this.dragState = dfDragDropState.Dragging;
								this.dragData = dfDragEventArgs3.Data;
								return true;
							}
							this.dragState = dfDragDropState.Denied;
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

			// Token: 0x06003DC9 RID: 15817 RVA: 0x000E9C50 File Offset: 0x000E7E50
			private bool canFireClickEvent(dfInputManager.TouchInputManager.TouchRaycast info, dfInputManager.TouchInputManager.TouchRaycast touch)
			{
				if (this.manager.TouchClickRadius <= 0)
				{
					return true;
				}
				float num = Vector2.Distance(info.position, touch.position);
				return num < (float)this.manager.TouchClickRadius;
			}

			// Token: 0x06003DCA RID: 15818 RVA: 0x000E9C94 File Offset: 0x000E7E94
			private List<Touch> getActiveTouches()
			{
				dfInputManager.TouchInputManager.ControlTouchTracker.<getActiveTouches>c__AnonStorey57 <getActiveTouches>c__AnonStorey = new dfInputManager.TouchInputManager.ControlTouchTracker.<getActiveTouches>c__AnonStorey57();
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

			// Token: 0x04002084 RID: 8324
			public dfControl control;

			// Token: 0x04002085 RID: 8325
			public Dictionary<int, dfInputManager.TouchInputManager.TouchRaycast> touches = new Dictionary<int, dfInputManager.TouchInputManager.TouchRaycast>();

			// Token: 0x04002086 RID: 8326
			public List<int> capture = new List<int>();

			// Token: 0x04002087 RID: 8327
			private dfInputManager manager;

			// Token: 0x04002088 RID: 8328
			private dfDragDropState dragState;

			// Token: 0x04002089 RID: 8329
			private object dragData;
		}

		// Token: 0x020006D4 RID: 1748
		private class TouchRaycast
		{
			// Token: 0x06003DCC RID: 15820 RVA: 0x000E9D58 File Offset: 0x000E7F58
			public TouchRaycast(dfControl control, Touch touch, Ray ray)
			{
				this.control = control;
				this.touch = touch;
				this.ray = ray;
				this.position = touch.position;
			}

			// Token: 0x17000C12 RID: 3090
			// (get) Token: 0x06003DCD RID: 15821 RVA: 0x000E9D90 File Offset: 0x000E7F90
			public int FingerID
			{
				get
				{
					return this.touch.fingerId;
				}
			}

			// Token: 0x17000C13 RID: 3091
			// (get) Token: 0x06003DCE RID: 15822 RVA: 0x000E9DA0 File Offset: 0x000E7FA0
			public TouchPhase Phase
			{
				get
				{
					return this.touch.phase;
				}
			}

			// Token: 0x06003DCF RID: 15823 RVA: 0x000E9DB0 File Offset: 0x000E7FB0
			public static implicit operator dfTouchEventArgs(dfInputManager.TouchInputManager.TouchRaycast touch)
			{
				return new dfTouchEventArgs(touch.control, touch.touch, touch.ray);
			}

			// Token: 0x06003DD0 RID: 15824 RVA: 0x000E9DD8 File Offset: 0x000E7FD8
			public static implicit operator dfDragEventArgs(dfInputManager.TouchInputManager.TouchRaycast touch)
			{
				return new dfDragEventArgs(touch.control, dfDragDropState.None, null, touch.ray, touch.position);
			}

			// Token: 0x0400208B RID: 8331
			public dfControl control;

			// Token: 0x0400208C RID: 8332
			public Touch touch;

			// Token: 0x0400208D RID: 8333
			public Ray ray;

			// Token: 0x0400208E RID: 8334
			public Vector2 position;
		}
	}

	// Token: 0x020006D5 RID: 1749
	private class MouseInputManager
	{
		// Token: 0x06003DD2 RID: 15826 RVA: 0x000E9E34 File Offset: 0x000E8034
		public void ProcessInput(IInputAdapter adapter, Ray ray, dfControl control, bool retainFocusSetting)
		{
			Vector2 mousePosition = adapter.GetMousePosition();
			this.buttonsDown = dfMouseButtons.None;
			this.buttonsReleased = dfMouseButtons.None;
			this.buttonsPressed = dfMouseButtons.None;
			dfInputManager.MouseInputManager.getMouseButtonInfo(adapter, ref this.buttonsDown, ref this.buttonsReleased, ref this.buttonsPressed);
			float num = adapter.GetAxis("Mouse ScrollWheel");
			if (!Mathf.Approximately(num, 0f))
			{
				num = Mathf.Sign(num) * Mathf.Max(1f, Mathf.Abs(num));
			}
			this.mouseMoveDelta = mousePosition - this.lastPosition;
			this.lastPosition = mousePosition;
			if (this.dragState == dfDragDropState.Dragging)
			{
				if (this.buttonsReleased != dfMouseButtons.None)
				{
					if (control != null && control != this.activeControl)
					{
						dfDragEventArgs dfDragEventArgs = new dfDragEventArgs(control, dfDragDropState.Dragging, this.dragData, ray, mousePosition);
						control.OnDragDrop(dfDragEventArgs);
						if (!dfDragEventArgs.Used || dfDragEventArgs.State == dfDragDropState.Dragging)
						{
							dfDragEventArgs.State = dfDragDropState.Cancelled;
						}
						dfDragEventArgs = new dfDragEventArgs(this.activeControl, dfDragEventArgs.State, dfDragEventArgs.Data, ray, mousePosition);
						dfDragEventArgs.Target = control;
						this.activeControl.OnDragEnd(dfDragEventArgs);
					}
					else
					{
						dfDragDropState state = (!(control == null)) ? dfDragDropState.Cancelled : dfDragDropState.CancelledNoTarget;
						dfDragEventArgs args = new dfDragEventArgs(this.activeControl, state, this.dragData, ray, mousePosition);
						this.activeControl.OnDragEnd(args);
					}
					this.dragState = dfDragDropState.None;
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
						dfDragEventArgs args2 = new dfDragEventArgs(this.lastDragControl, this.dragState, this.dragData, ray, mousePosition);
						this.lastDragControl.OnDragLeave(args2);
					}
					if (control != null)
					{
						dfDragEventArgs args3 = new dfDragEventArgs(control, this.dragState, this.dragData, ray, mousePosition);
						control.OnDragEnter(args3);
					}
					this.lastDragControl = control;
					return;
				}
				if (control != null && Vector2.Distance(mousePosition, this.lastPosition) > 1f)
				{
					dfDragEventArgs args4 = new dfDragEventArgs(control, this.dragState, this.dragData, ray, mousePosition);
					control.OnDragOver(args4);
				}
				return;
			}
			else if (this.buttonsReleased != dfMouseButtons.None)
			{
				this.lastHoverTime = Time.realtimeSinceStartup + 0.25f;
				if (this.activeControl == null)
				{
					this.setActive(control, mousePosition, ray);
					return;
				}
				if (this.activeControl == control && this.buttonsDown == dfMouseButtons.None)
				{
					if (Time.realtimeSinceStartup - this.lastClickTime < 0.25f)
					{
						this.lastClickTime = 0f;
						this.activeControl.OnDoubleClick(new dfMouseEventArgs(this.activeControl, this.buttonsReleased, 1, ray, mousePosition, num));
					}
					else
					{
						this.lastClickTime = Time.realtimeSinceStartup;
						this.activeControl.OnClick(new dfMouseEventArgs(this.activeControl, this.buttonsReleased, 1, ray, mousePosition, num));
					}
				}
				this.activeControl.OnMouseUp(new dfMouseEventArgs(this.activeControl, this.buttonsReleased, 0, ray, mousePosition, num));
				if (this.buttonsDown == dfMouseButtons.None && this.activeControl != control)
				{
					this.setActive(null, mousePosition, ray);
				}
				return;
			}
			else
			{
				if (this.buttonsPressed != dfMouseButtons.None)
				{
					this.lastHoverTime = Time.realtimeSinceStartup + 0.25f;
					if (this.activeControl != null)
					{
						this.activeControl.OnMouseDown(new dfMouseEventArgs(this.activeControl, this.buttonsPressed, 0, ray, mousePosition, num));
					}
					else
					{
						this.setActive(control, mousePosition, ray);
						if (control != null)
						{
							control.OnMouseDown(new dfMouseEventArgs(control, this.buttonsPressed, 0, ray, mousePosition, num));
						}
						else if (!retainFocusSetting)
						{
							dfControl dfControl = dfGUIManager.ActiveControl;
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
					this.activeControl.OnMouseHover(new dfMouseEventArgs(this.activeControl, this.buttonsDown, 0, ray, mousePosition, num));
					this.lastHoverTime = Time.realtimeSinceStartup;
				}
				if (this.buttonsDown == dfMouseButtons.None)
				{
					if (num != 0f && control != null)
					{
						this.setActive(control, mousePosition, ray);
						control.OnMouseWheel(new dfMouseEventArgs(control, this.buttonsDown, 0, ray, mousePosition, num));
						return;
					}
					this.setActive(control, mousePosition, ray);
				}
				else if (this.activeControl != null)
				{
					if (!(control != null) || control.RenderOrder > this.activeControl.RenderOrder)
					{
					}
					if (this.mouseMoveDelta.magnitude >= 2f && (this.buttonsDown & (dfMouseButtons.Left | dfMouseButtons.Right)) != dfMouseButtons.None && this.dragState != dfDragDropState.Denied)
					{
						dfDragEventArgs dfDragEventArgs2 = new dfDragEventArgs(this.activeControl)
						{
							Position = mousePosition
						};
						this.activeControl.OnDragStart(dfDragEventArgs2);
						if (dfDragEventArgs2.State == dfDragDropState.Dragging)
						{
							this.dragState = dfDragDropState.Dragging;
							this.dragData = dfDragEventArgs2.Data;
							return;
						}
						this.dragState = dfDragDropState.Denied;
					}
				}
				if (this.activeControl != null && this.mouseMoveDelta.magnitude >= 1f)
				{
					dfMouseEventArgs args5 = new dfMouseEventArgs(this.activeControl, this.buttonsDown, 0, ray, mousePosition, num)
					{
						MoveDelta = this.mouseMoveDelta
					};
					this.activeControl.OnMouseMove(args5);
				}
				return;
			}
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000EA420 File Offset: 0x000E8620
		private static void getMouseButtonInfo(IInputAdapter adapter, ref dfMouseButtons buttonsDown, ref dfMouseButtons buttonsReleased, ref dfMouseButtons buttonsPressed)
		{
			for (int i = 0; i < 3; i++)
			{
				if (adapter.GetMouseButton(i))
				{
					buttonsDown |= (dfMouseButtons)(1 << i);
				}
				if (adapter.GetMouseButtonUp(i))
				{
					buttonsReleased |= (dfMouseButtons)(1 << i);
				}
				if (adapter.GetMouseButtonDown(i))
				{
					buttonsPressed |= (dfMouseButtons)(1 << i);
				}
			}
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000EA484 File Offset: 0x000E8684
		private void setActive(dfControl control, Vector2 position, Ray ray)
		{
			if (this.activeControl != null && this.activeControl != control)
			{
				this.activeControl.OnMouseLeave(new dfMouseEventArgs(this.activeControl)
				{
					Position = position,
					Ray = ray
				});
			}
			if (control != null && control != this.activeControl)
			{
				this.lastClickTime = 0f;
				this.lastHoverTime = Time.realtimeSinceStartup + 0.25f;
				control.OnMouseEnter(new dfMouseEventArgs(control)
				{
					Position = position,
					Ray = ray
				});
			}
			this.activeControl = control;
			this.lastPosition = position;
			this.dragState = dfDragDropState.None;
		}

		// Token: 0x0400208F RID: 8335
		private const string scrollAxisName = "Mouse ScrollWheel";

		// Token: 0x04002090 RID: 8336
		private const float DOUBLECLICK_TIME = 0.25f;

		// Token: 0x04002091 RID: 8337
		private const int DRAG_START_DELTA = 2;

		// Token: 0x04002092 RID: 8338
		private const float HOVER_NOTIFICATION_FREQUENCY = 0.1f;

		// Token: 0x04002093 RID: 8339
		private const float HOVER_NOTIFICATION_BEGIN = 0.25f;

		// Token: 0x04002094 RID: 8340
		private dfControl activeControl;

		// Token: 0x04002095 RID: 8341
		private Vector2 lastPosition = Vector2.one * -2.14748365E+09f;

		// Token: 0x04002096 RID: 8342
		private Vector2 mouseMoveDelta = Vector2.zero;

		// Token: 0x04002097 RID: 8343
		private float lastClickTime;

		// Token: 0x04002098 RID: 8344
		private float lastHoverTime;

		// Token: 0x04002099 RID: 8345
		private dfDragDropState dragState;

		// Token: 0x0400209A RID: 8346
		private object dragData;

		// Token: 0x0400209B RID: 8347
		private dfControl lastDragControl;

		// Token: 0x0400209C RID: 8348
		private dfMouseButtons buttonsDown;

		// Token: 0x0400209D RID: 8349
		private dfMouseButtons buttonsReleased;

		// Token: 0x0400209E RID: 8350
		private dfMouseButtons buttonsPressed;
	}

	// Token: 0x020006D6 RID: 1750
	private class DefaultInput : IInputAdapter
	{
		// Token: 0x06003DD6 RID: 15830 RVA: 0x000EA54C File Offset: 0x000E874C
		public bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000EA554 File Offset: 0x000E8754
		public bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUp(key);
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000EA55C File Offset: 0x000E875C
		public float GetAxis(string axisName)
		{
			return Input.GetAxis(axisName);
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x000EA564 File Offset: 0x000E8764
		public Vector2 GetMousePosition()
		{
			return Input.mousePosition;
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000EA570 File Offset: 0x000E8770
		public bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000EA578 File Offset: 0x000E8778
		public bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x000EA580 File Offset: 0x000E8780
		public bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}
	}
}
