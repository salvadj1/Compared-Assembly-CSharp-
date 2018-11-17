using System;
using System.Collections;
using System.Collections.Generic;
using NGUI.MessageUtil;
using NGUIHack;
using UnityEngine;

// Token: 0x020008B4 RID: 2228
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Camera")]
public class UICamera : MonoBehaviour
{
	// Token: 0x06004C1A RID: 19482 RVA: 0x00129F44 File Offset: 0x00128144
	public static bool PopupPanel(global::UIPanel panel)
	{
		if (global::UICamera.popupPanel == panel)
		{
			return false;
		}
		if (global::UICamera.popupPanel)
		{
			global::UICamera.popupPanel.gameObject.NGUIMessage("PopupEnd");
			global::UICamera.popupPanel = null;
		}
		if (panel)
		{
			global::UICamera.popupPanel = panel;
			global::UICamera.popupPanel.gameObject.NGUIMessage("PopupStart");
		}
		return true;
	}

	// Token: 0x06004C1B RID: 19483 RVA: 0x00129FB4 File Offset: 0x001281B4
	public static bool UnPopupPanel(global::UIPanel panel)
	{
		if (global::UICamera.popupPanel == panel && panel)
		{
			global::UICamera.popupPanel.gameObject.NGUIMessage("PopupEnd");
			global::UICamera.popupPanel = null;
			return true;
		}
		return false;
	}

	// Token: 0x17000E61 RID: 3681
	// (get) Token: 0x06004C1C RID: 19484 RVA: 0x00129FFC File Offset: 0x001281FC
	public static global::UICamera.CursorSampler Cursor
	{
		get
		{
			return global::UICamera.LateLoadCursor.Sampler;
		}
	}

	// Token: 0x17000E62 RID: 3682
	// (get) Token: 0x06004C1D RID: 19485 RVA: 0x0012A004 File Offset: 0x00128204
	public static bool IsPressing
	{
		get
		{
			return global::UICamera.Cursor.Buttons.LeftValue.Held && global::UICamera.Cursor.Buttons.LeftValue.Pressed;
		}
	}

	// Token: 0x06004C1E RID: 19486 RVA: 0x0012A03C File Offset: 0x0012823C
	private void OnEvent(NGUIHack.Event @event, EventType type)
	{
		Camera camera = global::UICamera.currentCamera;
		try
		{
			global::UICamera.currentCamera = this.cachedCamera;
			switch (type)
			{
			case 0:
			case 1:
			case 2:
			case 3:
				if ((this.mouseMode & global::UIInputMode.UseEvents) == global::UIInputMode.UseEvents)
				{
					this.OnMouseEvent(@event, type);
				}
				break;
			case 4:
			case 5:
				if ((this.keyboardMode & global::UIInputMode.UseEvents) == global::UIInputMode.UseEvents)
				{
					this.OnKeyboardEvent(@event, type);
				}
				break;
			case 6:
				if ((this.scrollWheelMode & global::UIInputMode.UseEvents) == global::UIInputMode.UseEvents)
				{
					this.OnScrollWheelEvent(@event, type);
				}
				break;
			}
		}
		finally
		{
			global::UICamera.currentCamera = camera;
		}
	}

	// Token: 0x06004C1F RID: 19487 RVA: 0x0012A0FC File Offset: 0x001282FC
	private void OnMouseEvent(NGUIHack.Event @event, EventType type)
	{
		if (this.OnEventShared(@event, type))
		{
			return;
		}
		global::UICamera.Cursor.MouseEvent(@event, type);
	}

	// Token: 0x06004C20 RID: 19488 RVA: 0x0012A118 File Offset: 0x00128318
	private void OnScrollWheelEvent(NGUIHack.Event @event, EventType type)
	{
		if (global::UICamera.mHover != null)
		{
			Vector2 delta = @event.delta;
			bool flag = false;
			bool flag2 = false;
			if (delta.y != 0f)
			{
				global::UICamera.SwallowScroll = false;
				global::UICamera.mHover.Scroll(delta.y);
				flag2 = !global::UICamera.SwallowScroll;
			}
			if (delta.x != 0f)
			{
				global::UICamera.SwallowScroll = false;
				global::UICamera.mHover.ScrollX(delta.x);
				flag = !global::UICamera.SwallowScroll;
			}
			if (flag2 || flag)
			{
				global::UIPanel uipanel = global::UIPanel.Find(global::UICamera.mHover.transform);
				if (uipanel)
				{
					if (flag2)
					{
						uipanel.gameObject.NGUIMessage("OnHoverScroll", delta.y);
					}
					if (flag)
					{
						uipanel.gameObject.NGUIMessage("OnHoverScrollX", delta.x);
					}
				}
			}
			@event.Use();
		}
	}

	// Token: 0x06004C21 RID: 19489 RVA: 0x0012A20C File Offset: 0x0012840C
	private void OnSubmitEvent(NGUIHack.Event @event, EventType type)
	{
	}

	// Token: 0x06004C22 RID: 19490 RVA: 0x0012A210 File Offset: 0x00128410
	private void OnCancelEvent(NGUIHack.Event @event, EventType type)
	{
		if (type == 4)
		{
			global::UICamera.mSel.SendMessage("OnKey", 27, 1);
			@event.Use();
		}
	}

	// Token: 0x06004C23 RID: 19491 RVA: 0x0012A244 File Offset: 0x00128444
	private void OnDirectionEvent(NGUIHack.Event @event, int x, int y, EventType type)
	{
		bool flag = false;
		if (type == 4)
		{
			if (x != 0)
			{
				global::UICamera.mSel.SendMessage("OnKey", (x >= 0) ? 275 : 276, 1);
				flag = true;
			}
			if (y != 0)
			{
				global::UICamera.mSel.SendMessage("OnKey", (y >= 0) ? 273 : 274, 1);
				flag = true;
			}
		}
		if (flag)
		{
			@event.Use();
		}
	}

	// Token: 0x06004C24 RID: 19492 RVA: 0x0012A2D0 File Offset: 0x001284D0
	private void OnKeyboardEvent(NGUIHack.Event @event, EventType type)
	{
		if (this.OnEventShared(@event, type))
		{
			return;
		}
		char character = @event.character;
		KeyCode keyCode = @event.keyCode;
		bool flag = global::UICamera.mSelInput;
		if (flag)
		{
			global::UICamera.mSelInput.OnEvent(this, @event, type);
		}
		if (global::UICamera.mSel != null)
		{
			KeyCode keyCode2 = keyCode;
			if (keyCode2 != 9)
			{
				if (keyCode2 != 127)
				{
					if (type == 4 && character != '\0')
					{
						global::UICamera.mSel.Input(character.ToString());
					}
					if (keyCode == this.submitKey0 || keyCode == this.submitKey1)
					{
						if (!flag || @event.type == type)
						{
							this.OnSubmitEvent(@event, type);
						}
					}
					else if (keyCode == this.cancelKey0 || keyCode == this.cancelKey1)
					{
						if (!flag || @event.type == type)
						{
							this.OnCancelEvent(@event, type);
						}
					}
					else if (global::UICamera.inputHasFocus)
					{
						if (!flag || @event.type == type)
						{
							if (keyCode == 273)
							{
								this.OnDirectionEvent(@event, 0, 1, type);
							}
							else if (keyCode == 274)
							{
								this.OnDirectionEvent(@event, 0, -1, type);
							}
							else if (keyCode == 276)
							{
								this.OnDirectionEvent(@event, -1, 0, type);
							}
							else if (keyCode == 275)
							{
								this.OnDirectionEvent(@event, 1, 0, type);
							}
						}
					}
					else if (!flag || @event.type == type)
					{
						if (keyCode == 273 || keyCode == 119)
						{
							this.OnDirectionEvent(@event, 0, 1, type);
						}
						else if (keyCode == 274 || keyCode == 115)
						{
							this.OnDirectionEvent(@event, 0, -1, type);
						}
						else if (keyCode == 276 || keyCode == 97)
						{
							this.OnDirectionEvent(@event, -1, 0, type);
						}
						else if (keyCode == 275 || keyCode == 100)
						{
							this.OnDirectionEvent(@event, 1, 0, type);
						}
					}
				}
				else if (type == 4)
				{
					global::UICamera.mSel.Input("\b");
				}
			}
			else if (type == 4)
			{
				global::UICamera.mSel.Key(9);
			}
		}
	}

	// Token: 0x06004C25 RID: 19493 RVA: 0x0012A51C File Offset: 0x0012871C
	private bool OnEventShared(NGUIHack.Event @event, EventType type)
	{
		return false;
	}

	// Token: 0x06004C26 RID: 19494 RVA: 0x0012A520 File Offset: 0x00128720
	private static void IssueEvent(NGUIHack.Event @event, EventType type)
	{
		int button = @event.button;
		KeyCode keyCode = @event.keyCode;
		global::UICamera uicamera = null;
		switch (type)
		{
		case 0:
			if (button != 0 && global::UICamera.mMouseCamera.TryGetValue(0, out uicamera) && uicamera)
			{
				uicamera.OnEvent(@event, type);
				if (@event.type != null)
				{
					global::UICamera.mMouseCamera[button] = uicamera;
					return;
				}
			}
			break;
		case 1:
			if (global::UICamera.mMouseCamera.TryGetValue(button, out uicamera))
			{
				if (uicamera)
				{
					uicamera.OnEvent(@event, type);
					if (@event.type == 1)
					{
						@event.Use();
					}
				}
				else
				{
					@event.Use();
				}
				global::UICamera.mMouseCamera.Remove(button);
			}
			return;
		case 3:
			if (global::UICamera.mMouseCamera.TryGetValue(0, out uicamera))
			{
				if (uicamera)
				{
					uicamera.OnEvent(@event, type);
				}
				else
				{
					@event.Use();
				}
			}
			return;
		case 5:
			if (global::UICamera.mKeyCamera.TryGetValue(keyCode, out uicamera))
			{
				if (uicamera)
				{
					uicamera.OnEvent(@event, type);
					if (@event.type == 5)
					{
						@event.Use();
					}
				}
				else
				{
					@event.Use();
				}
				global::UICamera.mKeyCamera.Remove(keyCode);
			}
			return;
		}
		for (int i = 0; i < global::UICamera.mListCount; i++)
		{
			global::UICamera uicamera2 = global::UICamera.mList[(int)global::UICamera.mListSort[i]];
			if (!(uicamera2 == uicamera))
			{
				if (uicamera2.usesAnyEvents)
				{
					uicamera2.OnEvent(@event, type);
					EventType type2 = @event.type;
					if (type2 != type)
					{
						if (type != null)
						{
							if (type == 4)
							{
								global::UICamera.mKeyCamera[keyCode] = uicamera2;
							}
						}
						else
						{
							global::UICamera.mMouseCamera[button] = uicamera2;
						}
						return;
					}
				}
			}
		}
	}

	// Token: 0x06004C27 RID: 19495 RVA: 0x0012A720 File Offset: 0x00128920
	public static void HandleEvent(NGUIHack.Event @event, EventType type)
	{
		switch (type)
		{
		case 0:
			using (new global::UICamera.Mouse.Button.ButtonPressEventHandler(@event))
			{
				global::UICamera.IssueEvent(@event, 0);
			}
			return;
		case 1:
			using (new global::UICamera.Mouse.Button.ButtonReleaseEventHandler(@event))
			{
				global::UICamera.IssueEvent(@event, 1);
			}
			return;
		case 2:
			if (!global::UICamera.Mouse.Button.AllowMove)
			{
				return;
			}
			break;
		case 3:
			if (!global::UICamera.Mouse.Button.AllowDrag)
			{
				return;
			}
			break;
		case 7:
		case 8:
		case 11:
		case 12:
			return;
		}
		global::UICamera.IssueEvent(@event, type);
		if (type == 2 && @event.type == 12)
		{
			Debug.LogWarning("Mouse move was used.");
		}
	}

	// Token: 0x06004C28 RID: 19496 RVA: 0x0012A830 File Offset: 0x00128A30
	public static void Render()
	{
		for (int i = 0; i < global::UICamera.mListCount; i++)
		{
			if (global::UICamera.mList[i] && global::UICamera.mList[i].enabled && global::UICamera.mList[i].camera && !global::UICamera.mList[i].camera.enabled)
			{
				global::UICamera.mList[i].camera.Render();
			}
		}
	}

	// Token: 0x06004C29 RID: 19497 RVA: 0x0012A8B4 File Offset: 0x00128AB4
	public global::UITextPosition RaycastText(Vector3 inPos, global::UILabel label)
	{
		if (!base.enabled || !base.camera.enabled || !base.camera.pixelRect.Contains(inPos) || !label)
		{
			Debug.Log("No Sir");
			return default(global::UITextPosition);
		}
		Ray ray = base.camera.ScreenPointToRay(inPos);
		Vector3 forward = label.transform.forward;
		if (Vector3.Dot(ray.direction, forward) <= 0f)
		{
			Debug.Log("Bad Dir");
			return default(global::UITextPosition);
		}
		Plane plane;
		plane..ctor(forward, label.transform.position);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			Debug.Log("Paralell");
			return default(global::UITextPosition);
		}
		Vector3 point = ray.GetPoint(num);
		Vector3[] points = new Vector3[]
		{
			label.transform.InverseTransformPoint(point)
		};
		global::UITextPosition[] array = new global::UITextPosition[]
		{
			default(global::UITextPosition)
		};
		if (label.CalculateTextPosition(1, points, array) == 0)
		{
			Debug.Log("Zero");
		}
		return array[0];
	}

	// Token: 0x17000E63 RID: 3683
	// (get) Token: 0x06004C2A RID: 19498 RVA: 0x0012AA04 File Offset: 0x00128C04
	public bool usesAnyEvents
	{
		get
		{
			return ((this.mouseMode | this.keyboardMode | this.scrollWheelMode) & global::UIInputMode.UseEvents) == global::UIInputMode.UseEvents;
		}
	}

	// Token: 0x17000E64 RID: 3684
	// (get) Token: 0x06004C2B RID: 19499 RVA: 0x0012AA20 File Offset: 0x00128C20
	[Obsolete("Use UICamera.currentCamera instead")]
	public static Camera lastCamera
	{
		get
		{
			return global::UICamera.currentCamera;
		}
	}

	// Token: 0x17000E65 RID: 3685
	// (get) Token: 0x06004C2C RID: 19500 RVA: 0x0012AA28 File Offset: 0x00128C28
	[Obsolete("Use UICamera.currentTouchID instead")]
	public static int lastTouchID
	{
		get
		{
			return global::UICamera.currentTouchID;
		}
	}

	// Token: 0x17000E66 RID: 3686
	// (get) Token: 0x06004C2D RID: 19501 RVA: 0x0012AA30 File Offset: 0x00128C30
	private bool handlesEvents
	{
		get
		{
			return global::UICamera.eventHandler == this;
		}
	}

	// Token: 0x17000E67 RID: 3687
	// (get) Token: 0x06004C2E RID: 19502 RVA: 0x0012AA40 File Offset: 0x00128C40
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.camera;
			}
			return this.mCam;
		}
	}

	// Token: 0x17000E68 RID: 3688
	// (get) Token: 0x06004C2F RID: 19503 RVA: 0x0012AA68 File Offset: 0x00128C68
	public static GameObject hoveredObject
	{
		get
		{
			return global::UICamera.mHover;
		}
	}

	// Token: 0x17000E69 RID: 3689
	// (get) Token: 0x06004C30 RID: 19504 RVA: 0x0012AA70 File Offset: 0x00128C70
	// (set) Token: 0x06004C31 RID: 19505 RVA: 0x0012AA78 File Offset: 0x00128C78
	public static GameObject selectedObject
	{
		get
		{
			return global::UICamera.mSel;
		}
		set
		{
			if (!global::UICamera.SetSelectedObject(value))
			{
				throw new InvalidOperationException("Do not set selectedObject within a OnSelect message.");
			}
		}
	}

	// Token: 0x06004C32 RID: 19506 RVA: 0x0012AA90 File Offset: 0x00128C90
	public static bool SetSelectedObject(GameObject value)
	{
		if (global::UICamera.mSel != value)
		{
			if (global::UICamera.inSelectionCallback)
			{
				return false;
			}
			global::UIInput uiinput = (!value) ? null : value.GetComponent<global::UIInput>();
			if (global::UICamera.mSelInput != uiinput)
			{
				if (global::UICamera.mSelInput)
				{
					global::UICamera.mSelInput.LoseFocus();
				}
				global::UICamera.mSelInput = uiinput;
				if (uiinput && global::UICamera.mPressInput != uiinput)
				{
					uiinput.GainFocus();
				}
			}
			if (global::UICamera.mSel != null)
			{
				global::UICamera uicamera = global::UICamera.FindCameraForLayer(global::UICamera.mSel.layer);
				if (uicamera != null)
				{
					Camera camera = global::UICamera.currentCamera;
					try
					{
						global::UICamera.currentCamera = uicamera.mCam;
						global::UICamera.inSelectionCallback = true;
						global::UICamera.mSel.Select(false);
						if (uicamera.useController || uicamera.useKeyboard)
						{
							global::UICamera.Highlight(global::UICamera.mSel, false);
						}
					}
					finally
					{
						global::UICamera.currentCamera = camera;
						global::UICamera.inSelectionCallback = false;
					}
				}
			}
			global::UICamera.mSel = value;
			if (global::UICamera.mSel != null)
			{
				global::UICamera uicamera2 = global::UICamera.FindCameraForLayer(global::UICamera.mSel.layer);
				if (uicamera2 != null)
				{
					global::UICamera.currentCamera = uicamera2.mCam;
					if (uicamera2.useController || uicamera2.useKeyboard)
					{
						global::UICamera.Highlight(global::UICamera.mSel, true);
					}
					global::UICamera.mSel.Select(true);
				}
			}
		}
		return true;
	}

	// Token: 0x06004C33 RID: 19507 RVA: 0x0012AC28 File Offset: 0x00128E28
	private void OnApplicationQuit()
	{
		global::UICamera.mHighlighted.Clear();
	}

	// Token: 0x17000E6A RID: 3690
	// (get) Token: 0x06004C34 RID: 19508 RVA: 0x0012AC34 File Offset: 0x00128E34
	public static Camera mainCamera
	{
		get
		{
			global::UICamera eventHandler = global::UICamera.eventHandler;
			return (!(eventHandler != null)) ? null : eventHandler.cachedCamera;
		}
	}

	// Token: 0x17000E6B RID: 3691
	// (get) Token: 0x06004C35 RID: 19509 RVA: 0x0012AC60 File Offset: 0x00128E60
	public static global::UICamera eventHandler
	{
		get
		{
			return global::UICamera.mList[(int)global::UICamera.mListSort[0]];
		}
	}

	// Token: 0x06004C36 RID: 19510 RVA: 0x0012AC70 File Offset: 0x00128E70
	private static int CompareFunc(global::UICamera a, global::UICamera b)
	{
		return b.cachedCamera.depth.CompareTo(a.cachedCamera.depth);
	}

	// Token: 0x06004C37 RID: 19511 RVA: 0x0012AC9C File Offset: 0x00128E9C
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

	// Token: 0x06004C38 RID: 19512 RVA: 0x0012AD4C File Offset: 0x00128F4C
	private static bool Raycast(Vector3 inPos, ref global::UIHotSpot.Hit hit, out global::UICamera cam)
	{
		if (!Screen.lockCursor)
		{
			for (int i = 0; i < global::UICamera.mListCount; i++)
			{
				cam = global::UICamera.mList[(int)global::UICamera.mListSort[i]];
				if (cam.enabled && cam.gameObject.activeInHierarchy)
				{
					global::UICamera.currentCamera = cam.cachedCamera;
					Vector3 vector = global::UICamera.currentCamera.ScreenToViewportPoint(inPos);
					if (vector.x >= -1f && vector.x <= 1f && vector.y >= -1f && vector.y <= 1f)
					{
						global::UICamera.RaycastCheckWork raycastCheckWork;
						raycastCheckWork.ray = global::UICamera.currentCamera.ScreenPointToRay(inPos);
						raycastCheckWork.mask = (global::UICamera.currentCamera.cullingMask & cam.eventReceiverMask);
						raycastCheckWork.dist = ((cam.rangeDistance <= 0f) ? (global::UICamera.currentCamera.farClipPlane - global::UICamera.currentCamera.nearClipPlane) : cam.rangeDistance);
						if (!cam.onlyHotSpots)
						{
							bool flag = Physics.Raycast(raycastCheckWork.ray, ref raycastCheckWork.hit, raycastCheckWork.dist, raycastCheckWork.mask) && raycastCheckWork.Check();
							if (flag)
							{
								global::UIHotSpot.Hit hit2;
								if (global::UIHotSpot.Raycast(raycastCheckWork.ray, out hit2, raycastCheckWork.dist) && hit2.distance <= raycastCheckWork.hit.distance)
								{
									hit = hit2;
								}
								else
								{
									global::UIHotSpot.ConvertRaycastHit(ref raycastCheckWork.ray, ref raycastCheckWork.hit, out hit);
								}
								return true;
							}
						}
						if (global::UIHotSpot.Raycast(raycastCheckWork.ray, out hit, raycastCheckWork.dist))
						{
							return true;
						}
					}
				}
			}
		}
		cam = null;
		return false;
	}

	// Token: 0x06004C39 RID: 19513 RVA: 0x0012AF2C File Offset: 0x0012912C
	private static bool Raycast(global::UICamera cam, Vector3 inPos, ref RaycastHit hit)
	{
		if (Screen.lockCursor)
		{
			return false;
		}
		if (!cam.enabled || !cam.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (!cam.cachedCamera.pixelRect.Contains(inPos))
		{
			return false;
		}
		global::UICamera.RaycastCheckWork raycastCheckWork;
		raycastCheckWork.ray = cam.cachedCamera.ScreenPointToRay(inPos);
		raycastCheckWork.mask = (global::UICamera.currentCamera.cullingMask & cam.eventReceiverMask);
		raycastCheckWork.dist = ((cam.rangeDistance <= 0f) ? (global::UICamera.currentCamera.farClipPlane - global::UICamera.currentCamera.nearClipPlane) : cam.rangeDistance);
		bool result = Physics.Raycast(raycastCheckWork.ray, ref raycastCheckWork.hit, raycastCheckWork.dist, raycastCheckWork.mask) && raycastCheckWork.Check();
		hit = raycastCheckWork.hit;
		return result;
	}

	// Token: 0x06004C3A RID: 19514 RVA: 0x0012B024 File Offset: 0x00129224
	public static global::UICamera FindCameraForLayer(int layer)
	{
		return global::UICamera.mList[layer];
	}

	// Token: 0x06004C3B RID: 19515 RVA: 0x0012B030 File Offset: 0x00129230
	private static int GetDirection(KeyCode up, KeyCode down)
	{
		bool keyDown = Input.GetKeyDown(up);
		bool keyDown2 = Input.GetKeyDown(down);
		if (keyDown == keyDown2)
		{
			return 0;
		}
		if (keyDown)
		{
			return (!Input.GetKey(down)) ? 1 : 0;
		}
		return (!Input.GetKey(up)) ? -1 : 0;
	}

	// Token: 0x06004C3C RID: 19516 RVA: 0x0012B080 File Offset: 0x00129280
	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		bool flag = Input.GetKeyDown(up0) | Input.GetKeyDown(up1);
		bool flag2 = Input.GetKeyDown(down0) | Input.GetKeyDown(down1);
		if (flag == flag2)
		{
			return 0;
		}
		if (flag)
		{
			return (!Input.GetKey(down0) && !Input.GetKey(down1)) ? 1 : 0;
		}
		return (!Input.GetKey(up0) && !Input.GetKey(up1)) ? -1 : 0;
	}

	// Token: 0x06004C3D RID: 19517 RVA: 0x0012B0F4 File Offset: 0x001292F4
	private static int GetDirection(string axis)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (global::UICamera.mNextEvent < realtimeSinceStartup)
		{
			float axis2 = Input.GetAxis(axis);
			if (axis2 > 0.75f)
			{
				global::UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return 1;
			}
			if (axis2 < -0.75f)
			{
				global::UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x06004C3E RID: 19518 RVA: 0x0012B14C File Offset: 0x0012934C
	public static bool IsHighlighted(GameObject go)
	{
		int i = global::UICamera.mHighlighted.Count;
		while (i > 0)
		{
			global::UICamera.Highlighted highlighted = global::UICamera.mHighlighted[--i];
			if (highlighted.go == go)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004C3F RID: 19519 RVA: 0x0012B194 File Offset: 0x00129394
	private static void Highlight(GameObject go, bool highlighted)
	{
		if (go != null)
		{
			int i = global::UICamera.mHighlighted.Count;
			while (i > 0)
			{
				global::UICamera.Highlighted highlighted2 = global::UICamera.mHighlighted[--i];
				if (highlighted2 == null || highlighted2.go == null)
				{
					global::UICamera.mHighlighted.RemoveAt(i);
				}
				else if (highlighted2.go == go)
				{
					if (highlighted)
					{
						highlighted2.counter++;
					}
					else if (--highlighted2.counter < 1)
					{
						global::UICamera.mHighlighted.Remove(highlighted2);
						go.Hover(false);
					}
					return;
				}
			}
			if (highlighted)
			{
				global::UICamera.Highlighted highlighted3 = new global::UICamera.Highlighted();
				highlighted3.go = go;
				highlighted3.counter = 1;
				global::UICamera.mHighlighted.Add(highlighted3);
				go.Hover(true);
			}
		}
	}

	// Token: 0x06004C40 RID: 19520 RVA: 0x0012B27C File Offset: 0x0012947C
	private void Awake()
	{
		if (Application.platform == 11 || Application.platform == 8)
		{
			this.useMouse = false;
			this.useTouch = true;
			this.useKeyboard = false;
			this.useController = false;
		}
		else if (Application.platform == 9 || Application.platform == 10)
		{
			this.useMouse = false;
			this.useTouch = false;
			this.useKeyboard = false;
			this.useController = true;
		}
		else if (Application.platform == 7 || Application.platform == null)
		{
			this.mIsEditor = true;
		}
		this.AddToList();
		if (this.eventReceiverMask == -1)
		{
			this.eventReceiverMask = base.camera.cullingMask;
		}
		if (this.usesAnyEvents && Application.isPlaying)
		{
			global::UIUnityEvents.CameraCreated(this);
		}
	}

	// Token: 0x06004C41 RID: 19521 RVA: 0x0012B360 File Offset: 0x00129560
	private void OnDestroy()
	{
		this.RemoveFromList();
	}

	// Token: 0x06004C42 RID: 19522 RVA: 0x0012B368 File Offset: 0x00129568
	private void AddToList()
	{
		int layer = base.gameObject.layer;
		if (layer != this.lastBoundLayerIndex)
		{
			bool flag;
			if (this.lastBoundLayerIndex != -1 && global::UICamera.mList[this.lastBoundLayerIndex] == this)
			{
				global::UICamera.mList[this.lastBoundLayerIndex] = null;
				for (int i = 0; i < global::UICamera.mListCount; i++)
				{
					if ((int)global::UICamera.mListSort[i] == this.lastBoundLayerIndex)
					{
						global::UICamera.mListSort[i] = (byte)layer;
					}
				}
				flag = false;
			}
			else
			{
				global::UICamera.mListSort[global::UICamera.mListCount++] = (byte)layer;
				flag = true;
			}
			global::UICamera.mList[layer] = this;
			this.lastBoundLayerIndex = layer;
			if (flag)
			{
				Array.Sort<byte>(global::UICamera.mListSort, 0, global::UICamera.mListCount, global::UICamera.sorter);
			}
		}
	}

	// Token: 0x06004C43 RID: 19523 RVA: 0x0012B438 File Offset: 0x00129638
	private void RemoveFromList()
	{
		if (this.lastBoundLayerIndex != -1)
		{
			global::UICamera.mList[this.lastBoundLayerIndex] = null;
			int num = 0;
			for (int i = 0; i < global::UICamera.mListCount; i++)
			{
				if ((int)global::UICamera.mListSort[i] != this.lastBoundLayerIndex)
				{
					global::UICamera.mListSort[num++] = global::UICamera.mListSort[i];
				}
			}
			global::UICamera.mListCount = num;
			this.lastBoundLayerIndex = -1;
		}
	}

	// Token: 0x06004C44 RID: 19524 RVA: 0x0012B4A8 File Offset: 0x001296A8
	private void Update()
	{
		if (!Application.isPlaying || !this.handlesEvents)
		{
			return;
		}
		if (global::UICamera.mSel != null)
		{
			this.ProcessOthers();
		}
		else
		{
			global::UICamera.inputHasFocus = false;
		}
		if (this.useMouse && global::UICamera.mHover != null)
		{
			if ((this.mouseMode & global::UIInputMode.UseInput) == global::UIInputMode.UseInput)
			{
				float axis = Input.GetAxis(this.scrollAxisName);
				if (axis != 0f)
				{
					global::UICamera.mHover.Scroll(axis);
				}
			}
			if (this.mTooltipTime != 0f && this.mTooltipTime < Time.realtimeSinceStartup)
			{
				this.mTooltip = global::UICamera.mHover;
				this.ShowTooltip(true);
			}
		}
	}

	// Token: 0x06004C45 RID: 19525 RVA: 0x0012B56C File Offset: 0x0012976C
	private void ProcessOthers()
	{
		int num = 0;
		int num2 = 0;
		if (this.useController)
		{
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				num += global::UICamera.GetDirection(this.verticalAxisName);
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				num2 += global::UICamera.GetDirection(this.horizontalAxisName);
			}
		}
		if (num != 0)
		{
			global::UICamera.mSel.SendMessage("OnKey", (num <= 0) ? 274 : 273, 1);
		}
		if (num2 != 0)
		{
			global::UICamera.mSel.SendMessage("OnKey", (num2 <= 0) ? 276 : 275, 1);
		}
	}

	// Token: 0x06004C46 RID: 19526 RVA: 0x0012B628 File Offset: 0x00129828
	internal bool SetKeyboardFocus(global::UIInput input)
	{
		return global::UICamera.mSelInput == input || (!global::UICamera.mSelInput && input && global::UICamera.SetSelectedObject(input.gameObject));
	}

	// Token: 0x06004C47 RID: 19527 RVA: 0x0012B670 File Offset: 0x00129870
	public void ShowTooltip(bool val)
	{
		this.mTooltipTime = 0f;
		if (this.mTooltip != null)
		{
			this.mTooltip.Tooltip(val);
		}
		if (!val)
		{
			this.mTooltip = null;
		}
	}

	// Token: 0x040029F5 RID: 10741
	private const int kMouseButton0Flag = 1;

	// Token: 0x040029F6 RID: 10742
	private const int kMouseButton1Flag = 2;

	// Token: 0x040029F7 RID: 10743
	private const int kMouseButton2Flag = 4;

	// Token: 0x040029F8 RID: 10744
	private const int kMouseButton3Flag = 8;

	// Token: 0x040029F9 RID: 10745
	private const int kMouseButton4Flag = 16;

	// Token: 0x040029FA RID: 10746
	private const int kMouseButtonCount = 3;

	// Token: 0x040029FB RID: 10747
	private static global::UIPanel popupPanel;

	// Token: 0x040029FC RID: 10748
	public static global::UICamera.BackwardsCompatabilitySupport currentTouch;

	// Token: 0x040029FD RID: 10749
	public static bool SwallowScroll;

	// Token: 0x040029FE RID: 10750
	public bool useMouse = true;

	// Token: 0x040029FF RID: 10751
	public bool useTouch = true;

	// Token: 0x04002A00 RID: 10752
	public bool allowMultiTouch = true;

	// Token: 0x04002A01 RID: 10753
	public bool useKeyboard = true;

	// Token: 0x04002A02 RID: 10754
	public bool useController = true;

	// Token: 0x04002A03 RID: 10755
	public LayerMask eventReceiverMask = -1;

	// Token: 0x04002A04 RID: 10756
	public float tooltipDelay = 1f;

	// Token: 0x04002A05 RID: 10757
	public bool stickyTooltip = true;

	// Token: 0x04002A06 RID: 10758
	public float mouseClickThreshold = 10f;

	// Token: 0x04002A07 RID: 10759
	public float touchClickThreshold = 40f;

	// Token: 0x04002A08 RID: 10760
	public float rangeDistance = -1f;

	// Token: 0x04002A09 RID: 10761
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x04002A0A RID: 10762
	public string verticalAxisName = "Vertical";

	// Token: 0x04002A0B RID: 10763
	public string horizontalAxisName = "Horizontal";

	// Token: 0x04002A0C RID: 10764
	public KeyCode submitKey0 = 13;

	// Token: 0x04002A0D RID: 10765
	public KeyCode submitKey1 = 330;

	// Token: 0x04002A0E RID: 10766
	public KeyCode cancelKey0 = 27;

	// Token: 0x04002A0F RID: 10767
	public KeyCode cancelKey1 = 331;

	// Token: 0x04002A10 RID: 10768
	public global::UIInputMode mouseMode = global::UIInputMode.UseEvents;

	// Token: 0x04002A11 RID: 10769
	public global::UIInputMode keyboardMode = global::UIInputMode.UseInputAndEvents;

	// Token: 0x04002A12 RID: 10770
	public global::UIInputMode scrollWheelMode = global::UIInputMode.UseEvents;

	// Token: 0x04002A13 RID: 10771
	public bool onlyHotSpots;

	// Token: 0x04002A14 RID: 10772
	public static Vector2 lastTouchPosition = Vector2.zero;

	// Token: 0x04002A15 RID: 10773
	public static Vector2 lastMousePosition = Vector2.zero;

	// Token: 0x04002A16 RID: 10774
	public static global::UIHotSpot.Hit lastHit;

	// Token: 0x04002A17 RID: 10775
	public static Camera currentCamera = null;

	// Token: 0x04002A18 RID: 10776
	public static int currentTouchID = -1;

	// Token: 0x04002A19 RID: 10777
	public static bool inputHasFocus = false;

	// Token: 0x04002A1A RID: 10778
	public static GameObject fallThrough;

	// Token: 0x04002A1B RID: 10779
	private static global::UICamera[] mList = new global::UICamera[32];

	// Token: 0x04002A1C RID: 10780
	private static byte[] mListSort = new byte[32];

	// Token: 0x04002A1D RID: 10781
	private static int mListCount = 0;

	// Token: 0x04002A1E RID: 10782
	private static Dictionary<int, global::UICamera> mMouseCamera = new Dictionary<int, global::UICamera>();

	// Token: 0x04002A1F RID: 10783
	private static Dictionary<KeyCode, global::UICamera> mKeyCamera = new Dictionary<KeyCode, global::UICamera>();

	// Token: 0x04002A20 RID: 10784
	private static List<global::UICamera.Highlighted> mHighlighted = new List<global::UICamera.Highlighted>();

	// Token: 0x04002A21 RID: 10785
	private static GameObject mSel = null;

	// Token: 0x04002A22 RID: 10786
	private static global::UIInput mSelInput = null;

	// Token: 0x04002A23 RID: 10787
	private static global::UIInput mPressInput = null;

	// Token: 0x04002A24 RID: 10788
	private static GameObject mHover;

	// Token: 0x04002A25 RID: 10789
	private static float mNextEvent = 0f;

	// Token: 0x04002A26 RID: 10790
	private GameObject mTooltip;

	// Token: 0x04002A27 RID: 10791
	private Camera mCam;

	// Token: 0x04002A28 RID: 10792
	private LayerMask mLayerMask;

	// Token: 0x04002A29 RID: 10793
	private float mTooltipTime;

	// Token: 0x04002A2A RID: 10794
	private bool mIsEditor;

	// Token: 0x04002A2B RID: 10795
	private int lastBoundLayerIndex = -1;

	// Token: 0x04002A2C RID: 10796
	private static bool inSelectionCallback;

	// Token: 0x04002A2D RID: 10797
	private static readonly global::UICamera.CamSorter sorter = new global::UICamera.CamSorter();

	// Token: 0x020008B5 RID: 2229
	public static class Mouse
	{
		// Token: 0x020008B6 RID: 2230
		public static class Button
		{
			// Token: 0x17000E6C RID: 3692
			// (get) Token: 0x06004C49 RID: 19529 RVA: 0x0012B6B8 File Offset: 0x001298B8
			internal static global::UICamera.Mouse.Button.Flags NewlyPressed
			{
				get
				{
					return global::UICamera.Mouse.Button.pressed;
				}
			}

			// Token: 0x17000E6D RID: 3693
			// (get) Token: 0x06004C4A RID: 19530 RVA: 0x0012B6C0 File Offset: 0x001298C0
			internal static global::UICamera.Mouse.Button.Flags NewlyReleased
			{
				get
				{
					return global::UICamera.Mouse.Button.released;
				}
			}

			// Token: 0x17000E6E RID: 3694
			// (get) Token: 0x06004C4B RID: 19531 RVA: 0x0012B6C8 File Offset: 0x001298C8
			internal static global::UICamera.Mouse.Button.Flags Held
			{
				get
				{
					return global::UICamera.Mouse.Button.held;
				}
			}

			// Token: 0x17000E6F RID: 3695
			// (get) Token: 0x06004C4C RID: 19532 RVA: 0x0012B6D0 File Offset: 0x001298D0
			internal static bool AnyNewlyPressed
			{
				get
				{
					return global::UICamera.Mouse.Button.pressed != (global::UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x17000E70 RID: 3696
			// (get) Token: 0x06004C4D RID: 19533 RVA: 0x0012B6E0 File Offset: 0x001298E0
			internal static bool AnyNewlyReleased
			{
				get
				{
					return global::UICamera.Mouse.Button.released != (global::UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x17000E71 RID: 3697
			// (get) Token: 0x06004C4E RID: 19534 RVA: 0x0012B6F0 File Offset: 0x001298F0
			internal static bool AnyNewlyPressedOrReleased
			{
				get
				{
					return (global::UICamera.Mouse.Button.pressed | global::UICamera.Mouse.Button.released) != (global::UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x17000E72 RID: 3698
			// (get) Token: 0x06004C4F RID: 19535 RVA: 0x0012B704 File Offset: 0x00129904
			internal static bool AnyNewlyPressedThatCancelTooltips
			{
				get
				{
					return (global::UICamera.Mouse.Button.pressed & (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle)) != (global::UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x06004C50 RID: 19536 RVA: 0x0012B714 File Offset: 0x00129914
			internal static bool IsNewlyPressed(global::UICamera.Mouse.Button.Flags flag)
			{
				return (global::UICamera.Mouse.Button.pressed & flag) == flag;
			}

			// Token: 0x06004C51 RID: 19537 RVA: 0x0012B720 File Offset: 0x00129920
			internal static bool IsNewlyReleased(global::UICamera.Mouse.Button.Flags flag)
			{
				return (global::UICamera.Mouse.Button.released & flag) == flag;
			}

			// Token: 0x17000E73 RID: 3699
			// (get) Token: 0x06004C52 RID: 19538 RVA: 0x0012B72C File Offset: 0x0012992C
			public static bool AllowDrag
			{
				get
				{
					return global::UICamera.Mouse.Button.held != (global::UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x17000E74 RID: 3700
			// (get) Token: 0x06004C53 RID: 19539 RVA: 0x0012B73C File Offset: 0x0012993C
			public static bool AllowMove
			{
				get
				{
					return (global::UICamera.Mouse.Button.held | global::UICamera.Mouse.Button.released | global::UICamera.Mouse.Button.pressed) == (global::UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x06004C54 RID: 19540 RVA: 0x0012B754 File Offset: 0x00129954
			public static global::UICamera.Mouse.Button.Flags Index(int index)
			{
				if (index < 0 || index >= 3)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (global::UICamera.Mouse.Button.Flags)(1 << index);
			}

			// Token: 0x04002A2E RID: 10798
			private const global::UICamera.Mouse.Button.Flags kCancelsTooltips = global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle;

			// Token: 0x04002A2F RID: 10799
			public const global::UICamera.Mouse.Button.Flags Left = global::UICamera.Mouse.Button.Flags.Left;

			// Token: 0x04002A30 RID: 10800
			public const global::UICamera.Mouse.Button.Flags Right = global::UICamera.Mouse.Button.Flags.Right;

			// Token: 0x04002A31 RID: 10801
			public const global::UICamera.Mouse.Button.Flags Middle = global::UICamera.Mouse.Button.Flags.Middle;

			// Token: 0x04002A32 RID: 10802
			public const global::UICamera.Mouse.Button.Flags Mouse0 = global::UICamera.Mouse.Button.Flags.Left;

			// Token: 0x04002A33 RID: 10803
			public const global::UICamera.Mouse.Button.Flags Mouse1 = global::UICamera.Mouse.Button.Flags.Right;

			// Token: 0x04002A34 RID: 10804
			public const global::UICamera.Mouse.Button.Flags Mouse2 = global::UICamera.Mouse.Button.Flags.Middle;

			// Token: 0x04002A35 RID: 10805
			public const global::UICamera.Mouse.Button.Flags None = (global::UICamera.Mouse.Button.Flags)0;

			// Token: 0x04002A36 RID: 10806
			public const global::UICamera.Mouse.Button.Flags All = global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle;

			// Token: 0x04002A37 RID: 10807
			public const int Count = 3;

			// Token: 0x04002A38 RID: 10808
			private static global::UICamera.Mouse.Button.Flags pressed;

			// Token: 0x04002A39 RID: 10809
			private static global::UICamera.Mouse.Button.Flags released;

			// Token: 0x04002A3A RID: 10810
			private static global::UICamera.Mouse.Button.Flags held;

			// Token: 0x04002A3B RID: 10811
			private static int indexPressed = -1;

			// Token: 0x04002A3C RID: 10812
			private static int indexReleased = -1;

			// Token: 0x020008B7 RID: 2231
			public struct ButtonPressEventHandler : IDisposable
			{
				// Token: 0x06004C55 RID: 19541 RVA: 0x0012B778 File Offset: 0x00129978
				public ButtonPressEventHandler(NGUIHack.Event @event)
				{
					this.@event = @event;
					global::UICamera.Mouse.Button.pressed = global::UICamera.Mouse.Button.Index(@event.button);
					global::UICamera.Mouse.Button.indexPressed = @event.button;
				}

				// Token: 0x06004C56 RID: 19542 RVA: 0x0012B7A8 File Offset: 0x001299A8
				public void Dispose()
				{
					if (global::UICamera.Mouse.Button.indexPressed != -1)
					{
						if (this.@event.type == 12)
						{
							global::UICamera.Mouse.Button.held |= global::UICamera.Mouse.Button.pressed;
						}
						global::UICamera.Mouse.Button.indexPressed = -1;
						global::UICamera.Mouse.Button.pressed = (global::UICamera.Mouse.Button.Flags)0;
					}
				}

				// Token: 0x04002A3D RID: 10813
				private NGUIHack.Event @event;
			}

			// Token: 0x020008B8 RID: 2232
			public struct ButtonReleaseEventHandler : IDisposable
			{
				// Token: 0x06004C57 RID: 19543 RVA: 0x0012B7E4 File Offset: 0x001299E4
				public ButtonReleaseEventHandler(NGUIHack.Event @event)
				{
					this.@event = @event;
					global::UICamera.Mouse.Button.released = global::UICamera.Mouse.Button.Index(@event.button);
					global::UICamera.Mouse.Button.indexReleased = @event.button;
				}

				// Token: 0x06004C58 RID: 19544 RVA: 0x0012B814 File Offset: 0x00129A14
				public void Dispose()
				{
					if (global::UICamera.Mouse.Button.indexReleased != -1)
					{
						if (this.@event.type == 12)
						{
							global::UICamera.Mouse.Button.held &= ~global::UICamera.Mouse.Button.released;
						}
						global::UICamera.Mouse.Button.indexReleased = -1;
						global::UICamera.Mouse.Button.released = (global::UICamera.Mouse.Button.Flags)0;
					}
				}

				// Token: 0x04002A3E RID: 10814
				private NGUIHack.Event @event;
			}

			// Token: 0x020008B9 RID: 2233
			[Flags]
			public enum Flags
			{
				// Token: 0x04002A40 RID: 10816
				Left = 1,
				// Token: 0x04002A41 RID: 10817
				Right = 2,
				// Token: 0x04002A42 RID: 10818
				Middle = 4
			}

			// Token: 0x020008BA RID: 2234
			public struct Pair<T>
			{
				// Token: 0x06004C59 RID: 19545 RVA: 0x0012B85C File Offset: 0x00129A5C
				public Pair(global::UICamera.Mouse.Button.Flags Button, T Value)
				{
					this.Button = Button;
					this.Value = Value;
				}

				// Token: 0x06004C5A RID: 19546 RVA: 0x0012B86C File Offset: 0x00129A6C
				public Pair(global::UICamera.Mouse.Button.Flags Button, ref T Value)
				{
					this.Button = Button;
					this.Value = Value;
				}

				// Token: 0x06004C5B RID: 19547 RVA: 0x0012B884 File Offset: 0x00129A84
				public Pair(global::UICamera.Mouse.Button.Flags Button)
				{
					this = new global::UICamera.Mouse.Button.Pair<T>(Button, default(T));
				}

				// Token: 0x04002A43 RID: 10819
				public readonly global::UICamera.Mouse.Button.Flags Button;

				// Token: 0x04002A44 RID: 10820
				public readonly T Value;
			}

			// Token: 0x020008BB RID: 2235
			public struct ValCollection<T> : IEnumerable, IEnumerable<global::UICamera.Mouse.Button.Pair<T>>
			{
				// Token: 0x06004C5C RID: 19548 RVA: 0x0012B8A4 File Offset: 0x00129AA4
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x17000E75 RID: 3701
				public T this[global::UICamera.Mouse.Button.Flags button]
				{
					get
					{
						switch (button)
						{
						case global::UICamera.Mouse.Button.Flags.Left:
							return this.LeftValue;
						case global::UICamera.Mouse.Button.Flags.Right:
							return this.RightValue;
						case global::UICamera.Mouse.Button.Flags.Middle:
							return this.MiddleValue;
						}
						throw new ArgumentOutOfRangeException("button", "button should not be None or a Combination of multiple buttons");
					}
					set
					{
						switch (button)
						{
						case global::UICamera.Mouse.Button.Flags.Left:
							this.LeftValue = value;
							return;
						case global::UICamera.Mouse.Button.Flags.Right:
							this.RightValue = value;
							return;
						case global::UICamera.Mouse.Button.Flags.Middle:
							this.MiddleValue = value;
							return;
						}
						throw new ArgumentOutOfRangeException("button", "button should not be None or a Combination of multiple buttons");
					}
				}

				// Token: 0x17000E76 RID: 3702
				public T this[int i]
				{
					get
					{
						return this[global::UICamera.Mouse.Button.Flags.Left];
					}
					set
					{
						this[global::UICamera.Mouse.Button.Flags.Left] = value;
					}
				}

				// Token: 0x17000E77 RID: 3703
				public IEnumerable<global::UICamera.Mouse.Button.Pair<T>> this[global::UICamera.Mouse.Button.PressState state]
				{
					get
					{
						foreach (global::UICamera.Mouse.Button.Flags flag in state)
						{
							yield return new global::UICamera.Mouse.Button.Pair<T>(flag, this[flag]);
						}
						yield break;
					}
				}

				// Token: 0x06004C62 RID: 19554 RVA: 0x0012B9AC File Offset: 0x00129BAC
				public IEnumerator<global::UICamera.Mouse.Button.Pair<T>> GetEnumerator()
				{
					yield return new global::UICamera.Mouse.Button.Pair<T>(global::UICamera.Mouse.Button.Flags.Left, this.LeftValue);
					yield return new global::UICamera.Mouse.Button.Pair<T>(global::UICamera.Mouse.Button.Flags.Right, this.RightValue);
					yield return new global::UICamera.Mouse.Button.Pair<T>(global::UICamera.Mouse.Button.Flags.Middle, this.MiddleValue);
					yield break;
				}

				// Token: 0x04002A45 RID: 10821
				public T LeftValue;

				// Token: 0x04002A46 RID: 10822
				public T RightValue;

				// Token: 0x04002A47 RID: 10823
				public T MiddleValue;
			}

			// Token: 0x020008BE RID: 2238
			public struct RefCollection<T> : IEnumerable, IEnumerable<global::UICamera.Mouse.Button.Pair<T>>
			{
				// Token: 0x06004C71 RID: 19569 RVA: 0x0012BC6C File Offset: 0x00129E6C
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x17000E7C RID: 3708
				public T this[global::UICamera.Mouse.Button.Flags button]
				{
					get
					{
						switch (button)
						{
						case global::UICamera.Mouse.Button.Flags.Left:
							return this.LeftValue;
						case global::UICamera.Mouse.Button.Flags.Right:
							return this.RightValue;
						case global::UICamera.Mouse.Button.Flags.Middle:
							return this.MiddleValue;
						}
						throw new ArgumentOutOfRangeException("button", "button should not be None or a Combination of multiple buttons");
					}
					set
					{
						switch (button)
						{
						case global::UICamera.Mouse.Button.Flags.Left:
							this.LeftValue = value;
							return;
						case global::UICamera.Mouse.Button.Flags.Right:
							this.RightValue = value;
							return;
						case global::UICamera.Mouse.Button.Flags.Middle:
							this.MiddleValue = value;
							return;
						}
						throw new ArgumentOutOfRangeException("button", "button should not be None or a Combination of multiple buttons");
					}
				}

				// Token: 0x17000E7D RID: 3709
				public T this[int i]
				{
					get
					{
						return this[global::UICamera.Mouse.Button.Flags.Left];
					}
					set
					{
						this[global::UICamera.Mouse.Button.Flags.Left] = value;
					}
				}

				// Token: 0x17000E7E RID: 3710
				public IEnumerable<global::UICamera.Mouse.Button.Pair<T>> this[global::UICamera.Mouse.Button.PressState state]
				{
					get
					{
						foreach (global::UICamera.Mouse.Button.Flags flag in state)
						{
							yield return new global::UICamera.Mouse.Button.Pair<T>(flag, this[flag]);
						}
						yield break;
					}
				}

				// Token: 0x06004C77 RID: 19575 RVA: 0x0012BD74 File Offset: 0x00129F74
				public IEnumerator<global::UICamera.Mouse.Button.Pair<T>> GetEnumerator()
				{
					yield return new global::UICamera.Mouse.Button.Pair<T>(global::UICamera.Mouse.Button.Flags.Left, this.LeftValue);
					yield return new global::UICamera.Mouse.Button.Pair<T>(global::UICamera.Mouse.Button.Flags.Right, this.RightValue);
					yield return new global::UICamera.Mouse.Button.Pair<T>(global::UICamera.Mouse.Button.Flags.Middle, this.MiddleValue);
					yield break;
				}

				// Token: 0x04002A52 RID: 10834
				public T LeftValue;

				// Token: 0x04002A53 RID: 10835
				public T RightValue;

				// Token: 0x04002A54 RID: 10836
				public T MiddleValue;
			}

			// Token: 0x020008C1 RID: 2241
			public struct PressState : IEnumerable, IEnumerable<global::UICamera.Mouse.Button.Flags>
			{
				// Token: 0x06004C86 RID: 19590 RVA: 0x0012C034 File Offset: 0x0012A234
				IEnumerator<global::UICamera.Mouse.Button.Flags> IEnumerable<global::UICamera.Mouse.Button.Flags>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06004C87 RID: 19591 RVA: 0x0012C03C File Offset: 0x0012A23C
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x17000E83 RID: 3715
				// (get) Token: 0x06004C88 RID: 19592 RVA: 0x0012C044 File Offset: 0x0012A244
				// (set) Token: 0x06004C89 RID: 19593 RVA: 0x0012C054 File Offset: 0x0012A254
				public bool LeftPressed
				{
					get
					{
						return (this.Pressed & global::UICamera.Mouse.Button.Flags.Left) == global::UICamera.Mouse.Button.Flags.Left;
					}
					set
					{
						if (value)
						{
							this.Pressed |= global::UICamera.Mouse.Button.Flags.Left;
						}
						else
						{
							this.Pressed &= ~global::UICamera.Mouse.Button.Flags.Left;
						}
					}
				}

				// Token: 0x17000E84 RID: 3716
				// (get) Token: 0x06004C8A RID: 19594 RVA: 0x0012C08C File Offset: 0x0012A28C
				// (set) Token: 0x06004C8B RID: 19595 RVA: 0x0012C09C File Offset: 0x0012A29C
				public bool RightPressed
				{
					get
					{
						return (this.Pressed & global::UICamera.Mouse.Button.Flags.Right) == global::UICamera.Mouse.Button.Flags.Right;
					}
					set
					{
						if (value)
						{
							this.Pressed |= global::UICamera.Mouse.Button.Flags.Right;
						}
						else
						{
							this.Pressed &= ~global::UICamera.Mouse.Button.Flags.Right;
						}
					}
				}

				// Token: 0x17000E85 RID: 3717
				// (get) Token: 0x06004C8C RID: 19596 RVA: 0x0012C0D4 File Offset: 0x0012A2D4
				// (set) Token: 0x06004C8D RID: 19597 RVA: 0x0012C0E4 File Offset: 0x0012A2E4
				public bool MiddlePressed
				{
					get
					{
						return (this.Pressed & global::UICamera.Mouse.Button.Flags.Middle) == global::UICamera.Mouse.Button.Flags.Middle;
					}
					set
					{
						if (value)
						{
							this.Pressed |= global::UICamera.Mouse.Button.Flags.Middle;
						}
						else
						{
							this.Pressed &= ~global::UICamera.Mouse.Button.Flags.Middle;
						}
					}
				}

				// Token: 0x17000E86 RID: 3718
				// (get) Token: 0x06004C8E RID: 19598 RVA: 0x0012C11C File Offset: 0x0012A31C
				// (set) Token: 0x06004C8F RID: 19599 RVA: 0x0012C128 File Offset: 0x0012A328
				public global::UICamera.Mouse.Button.Flags Released
				{
					get
					{
						return ~this.Pressed & (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle);
					}
					set
					{
						this.Pressed = (~value & (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle));
					}
				}

				// Token: 0x17000E87 RID: 3719
				// (get) Token: 0x06004C90 RID: 19600 RVA: 0x0012C134 File Offset: 0x0012A334
				public bool AnyPressed
				{
					get
					{
						return (this.Pressed & (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle)) != (global::UICamera.Mouse.Button.Flags)0;
					}
				}

				// Token: 0x17000E88 RID: 3720
				// (get) Token: 0x06004C91 RID: 19601 RVA: 0x0012C144 File Offset: 0x0012A344
				public bool AllPressed
				{
					get
					{
						return (this.Pressed & (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle)) == (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle);
					}
				}

				// Token: 0x17000E89 RID: 3721
				// (get) Token: 0x06004C92 RID: 19602 RVA: 0x0012C154 File Offset: 0x0012A354
				public bool NonePressed
				{
					get
					{
						return (this.Pressed & (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle)) == (global::UICamera.Mouse.Button.Flags)0;
					}
				}

				// Token: 0x17000E8A RID: 3722
				// (get) Token: 0x06004C93 RID: 19603 RVA: 0x0012C164 File Offset: 0x0012A364
				public int PressedCount
				{
					get
					{
						int num = 0;
						uint num2 = (uint)(this.Pressed & (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle));
						while (num2 != 0u)
						{
							num2 &= num2 - 1u;
							num++;
						}
						return num;
					}
				}

				// Token: 0x17000E8B RID: 3723
				// (get) Token: 0x06004C94 RID: 19604 RVA: 0x0012C194 File Offset: 0x0012A394
				// (set) Token: 0x06004C95 RID: 19605 RVA: 0x0012C1A0 File Offset: 0x0012A3A0
				public bool LeftReleased
				{
					get
					{
						return !this.LeftPressed;
					}
					set
					{
						this.LeftPressed = !value;
					}
				}

				// Token: 0x17000E8C RID: 3724
				// (get) Token: 0x06004C96 RID: 19606 RVA: 0x0012C1AC File Offset: 0x0012A3AC
				// (set) Token: 0x06004C97 RID: 19607 RVA: 0x0012C1B8 File Offset: 0x0012A3B8
				public bool RightReleased
				{
					get
					{
						return !this.RightPressed;
					}
					set
					{
						this.RightPressed = !value;
					}
				}

				// Token: 0x17000E8D RID: 3725
				// (get) Token: 0x06004C98 RID: 19608 RVA: 0x0012C1C4 File Offset: 0x0012A3C4
				// (set) Token: 0x06004C99 RID: 19609 RVA: 0x0012C1D0 File Offset: 0x0012A3D0
				public bool MiddleReleased
				{
					get
					{
						return !this.MiddlePressed;
					}
					set
					{
						this.MiddlePressed = !value;
					}
				}

				// Token: 0x17000E8E RID: 3726
				// (get) Token: 0x06004C9A RID: 19610 RVA: 0x0012C1DC File Offset: 0x0012A3DC
				public bool AnyReleased
				{
					get
					{
						return !this.AllPressed;
					}
				}

				// Token: 0x17000E8F RID: 3727
				// (get) Token: 0x06004C9B RID: 19611 RVA: 0x0012C1E8 File Offset: 0x0012A3E8
				public bool AllReleased
				{
					get
					{
						return !this.AnyPressed;
					}
				}

				// Token: 0x17000E90 RID: 3728
				// (get) Token: 0x06004C9C RID: 19612 RVA: 0x0012C1F4 File Offset: 0x0012A3F4
				public bool NoneReleased
				{
					get
					{
						return !this.AllPressed;
					}
				}

				// Token: 0x06004C9D RID: 19613 RVA: 0x0012C200 File Offset: 0x0012A400
				public void Clear()
				{
					this.Pressed &= ~(global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle);
				}

				// Token: 0x17000E91 RID: 3729
				public bool this[int index]
				{
					get
					{
						global::UICamera.Mouse.Button.Flags flags = global::UICamera.Mouse.Button.Index(index);
						return (this.Pressed & flags) == flags;
					}
					set
					{
						global::UICamera.Mouse.Button.Flags flags = global::UICamera.Mouse.Button.Index(index);
						if (value)
						{
							this.Pressed |= flags;
						}
						else
						{
							this.Pressed &= ~flags;
						}
					}
				}

				// Token: 0x06004CA0 RID: 19616 RVA: 0x0012C270 File Offset: 0x0012A470
				public global::UICamera.Mouse.Button.PressState.Enumerator GetEnumerator()
				{
					return global::UICamera.Mouse.Button.PressState.Enumerator.Enumerate(this.Pressed);
				}

				// Token: 0x06004CA1 RID: 19617 RVA: 0x0012C280 File Offset: 0x0012A480
				public static implicit operator global::UICamera.Mouse.Button.Flags(global::UICamera.Mouse.Button.PressState state)
				{
					return state.Pressed & (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle);
				}

				// Token: 0x06004CA2 RID: 19618 RVA: 0x0012C28C File Offset: 0x0012A48C
				public static implicit operator global::UICamera.Mouse.Button.PressState(global::UICamera.Mouse.Button.Flags buttons)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (buttons & (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle));
					return result;
				}

				// Token: 0x06004CA3 RID: 19619 RVA: 0x0012C2A4 File Offset: 0x0012A4A4
				public static bool operator true(global::UICamera.Mouse.Button.PressState state)
				{
					return state.AnyPressed;
				}

				// Token: 0x06004CA4 RID: 19620 RVA: 0x0012C2B0 File Offset: 0x0012A4B0
				public static bool operator false(global::UICamera.Mouse.Button.PressState state)
				{
					return state.NonePressed;
				}

				// Token: 0x06004CA5 RID: 19621 RVA: 0x0012C2BC File Offset: 0x0012A4BC
				public static global::UICamera.Mouse.Button.PressState operator -(global::UICamera.Mouse.Button.PressState s)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = s.Released;
					return result;
				}

				// Token: 0x06004CA6 RID: 19622 RVA: 0x0012C2D8 File Offset: 0x0012A4D8
				public static global::UICamera.Mouse.Button.PressState operator +(global::UICamera.Mouse.Button.PressState l, global::UICamera.Mouse.Button.PressState r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed | r.Pressed);
					return result;
				}

				// Token: 0x06004CA7 RID: 19623 RVA: 0x0012C2FC File Offset: 0x0012A4FC
				public static global::UICamera.Mouse.Button.PressState operator +(global::UICamera.Mouse.Button.PressState l, global::UICamera.Mouse.Button.Flags r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed | r);
					return result;
				}

				// Token: 0x06004CA8 RID: 19624 RVA: 0x0012C31C File Offset: 0x0012A51C
				public static global::UICamera.Mouse.Button.PressState operator +(global::UICamera.Mouse.Button.Flags l, global::UICamera.Mouse.Button.PressState r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l | r.Pressed);
					return result;
				}

				// Token: 0x06004CA9 RID: 19625 RVA: 0x0012C33C File Offset: 0x0012A53C
				public static global::UICamera.Mouse.Button.PressState operator -(global::UICamera.Mouse.Button.PressState l, global::UICamera.Mouse.Button.PressState r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed & ~r.Pressed);
					return result;
				}

				// Token: 0x06004CAA RID: 19626 RVA: 0x0012C364 File Offset: 0x0012A564
				public static global::UICamera.Mouse.Button.PressState operator -(global::UICamera.Mouse.Button.PressState l, global::UICamera.Mouse.Button.Flags r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed & ~r);
					return result;
				}

				// Token: 0x06004CAB RID: 19627 RVA: 0x0012C384 File Offset: 0x0012A584
				public static global::UICamera.Mouse.Button.PressState operator -(global::UICamera.Mouse.Button.Flags l, global::UICamera.Mouse.Button.PressState r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l & ~r.Pressed);
					return result;
				}

				// Token: 0x06004CAC RID: 19628 RVA: 0x0012C3A4 File Offset: 0x0012A5A4
				public static global::UICamera.Mouse.Button.PressState operator *(global::UICamera.Mouse.Button.PressState l, global::UICamera.Mouse.Button.PressState r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed & r.Pressed);
					return result;
				}

				// Token: 0x06004CAD RID: 19629 RVA: 0x0012C3C8 File Offset: 0x0012A5C8
				public static global::UICamera.Mouse.Button.PressState operator *(global::UICamera.Mouse.Button.PressState l, global::UICamera.Mouse.Button.Flags r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed & r);
					return result;
				}

				// Token: 0x06004CAE RID: 19630 RVA: 0x0012C3E8 File Offset: 0x0012A5E8
				public static global::UICamera.Mouse.Button.PressState operator *(global::UICamera.Mouse.Button.Flags l, global::UICamera.Mouse.Button.PressState r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l & r.Pressed);
					return result;
				}

				// Token: 0x06004CAF RID: 19631 RVA: 0x0012C408 File Offset: 0x0012A608
				public static global::UICamera.Mouse.Button.PressState operator /(global::UICamera.Mouse.Button.PressState l, global::UICamera.Mouse.Button.PressState r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed ^ r.Pressed);
					return result;
				}

				// Token: 0x06004CB0 RID: 19632 RVA: 0x0012C42C File Offset: 0x0012A62C
				public static global::UICamera.Mouse.Button.PressState operator /(global::UICamera.Mouse.Button.PressState l, global::UICamera.Mouse.Button.Flags r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed ^ r);
					return result;
				}

				// Token: 0x06004CB1 RID: 19633 RVA: 0x0012C44C File Offset: 0x0012A64C
				public static global::UICamera.Mouse.Button.PressState operator /(global::UICamera.Mouse.Button.Flags l, global::UICamera.Mouse.Button.PressState r)
				{
					global::UICamera.Mouse.Button.PressState result;
					result.Pressed = (l ^ r.Pressed);
					return result;
				}

				// Token: 0x04002A5F RID: 10847
				public global::UICamera.Mouse.Button.Flags Pressed;

				// Token: 0x020008C2 RID: 2242
				public class Enumerator : IDisposable, IEnumerator, IEnumerator<global::UICamera.Mouse.Button.Flags>
				{
					// Token: 0x06004CB2 RID: 19634 RVA: 0x0012C46C File Offset: 0x0012A66C
					private Enumerator()
					{
					}

					// Token: 0x06004CB3 RID: 19635 RVA: 0x0012C474 File Offset: 0x0012A674
					static Enumerator()
					{
						for (global::UICamera.Mouse.Button.Flags flags = (global::UICamera.Mouse.Button.Flags)0; flags <= (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle); flags++)
						{
							int num = 0;
							uint num2 = (uint)flags;
							while (num2 != 0u)
							{
								num2 &= num2 - 1u;
								num++;
							}
							global::UICamera.Mouse.Button.Flags[] array = new global::UICamera.Mouse.Button.Flags[num];
							int num3 = 0;
							int num4 = 0;
							while (num4 < 3 && num3 < num)
							{
								if ((flags & (global::UICamera.Mouse.Button.Flags)(1 << num4)) == (global::UICamera.Mouse.Button.Flags)(1 << num4))
								{
									array[num3++] = (global::UICamera.Mouse.Button.Flags)(1 << num4);
								}
								num4++;
							}
							global::UICamera.Mouse.Button.PressState.Enumerator.combos[(int)flags] = array;
						}
					}

					// Token: 0x17000E92 RID: 3730
					// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x0012C50C File Offset: 0x0012A70C
					object IEnumerator.Current
					{
						get
						{
							return this.flags[this.pos];
						}
					}

					// Token: 0x06004CB5 RID: 19637 RVA: 0x0012C520 File Offset: 0x0012A720
					public static global::UICamera.Mouse.Button.PressState.Enumerator Enumerate(global::UICamera.Mouse.Button.Flags flags)
					{
						global::UICamera.Mouse.Button.PressState.Enumerator enumerator;
						if (global::UICamera.Mouse.Button.PressState.Enumerator.dumpCount == 0u)
						{
							enumerator = new global::UICamera.Mouse.Button.PressState.Enumerator();
						}
						else
						{
							enumerator = global::UICamera.Mouse.Button.PressState.Enumerator.dump;
							global::UICamera.Mouse.Button.PressState.Enumerator.dump = enumerator.nextDump;
							global::UICamera.Mouse.Button.PressState.Enumerator.dumpCount -= 1u;
							enumerator.nextDump = null;
						}
						enumerator.pos = -1;
						enumerator.value = flags;
						enumerator.inDump = false;
						enumerator.flags = global::UICamera.Mouse.Button.PressState.Enumerator.combos[(int)flags];
						return enumerator;
					}

					// Token: 0x17000E93 RID: 3731
					// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x0012C58C File Offset: 0x0012A78C
					public global::UICamera.Mouse.Button.Flags Current
					{
						get
						{
							return this.flags[this.pos];
						}
					}

					// Token: 0x06004CB7 RID: 19639 RVA: 0x0012C59C File Offset: 0x0012A79C
					public bool MoveNext()
					{
						return ++this.pos < this.flags.Length;
					}

					// Token: 0x06004CB8 RID: 19640 RVA: 0x0012C5C4 File Offset: 0x0012A7C4
					public void Reset()
					{
						this.pos = -1;
					}

					// Token: 0x06004CB9 RID: 19641 RVA: 0x0012C5D0 File Offset: 0x0012A7D0
					public void Dispose()
					{
						if (!this.inDump)
						{
							this.nextDump = global::UICamera.Mouse.Button.PressState.Enumerator.dump;
							this.inDump = true;
							global::UICamera.Mouse.Button.PressState.Enumerator.dump = this;
							global::UICamera.Mouse.Button.PressState.Enumerator.dumpCount += 1u;
						}
					}

					// Token: 0x04002A60 RID: 10848
					private static readonly global::UICamera.Mouse.Button.Flags[][] combos = new global::UICamera.Mouse.Button.Flags[8][];

					// Token: 0x04002A61 RID: 10849
					private global::UICamera.Mouse.Button.Flags[] flags;

					// Token: 0x04002A62 RID: 10850
					private global::UICamera.Mouse.Button.Flags value;

					// Token: 0x04002A63 RID: 10851
					private int pos;

					// Token: 0x04002A64 RID: 10852
					private global::UICamera.Mouse.Button.PressState.Enumerator nextDump;

					// Token: 0x04002A65 RID: 10853
					private bool inDump;

					// Token: 0x04002A66 RID: 10854
					private static global::UICamera.Mouse.Button.PressState.Enumerator dump;

					// Token: 0x04002A67 RID: 10855
					private static uint dumpCount;
				}
			}

			// Token: 0x020008C3 RID: 2243
			public sealed class Sampler
			{
				// Token: 0x06004CBA RID: 19642 RVA: 0x0012C604 File Offset: 0x0012A804
				public Sampler(global::UICamera.Mouse.Button.Flags Button, global::UICamera.CursorSampler Cursor)
				{
					this.Button = Button;
					this.Cursor = Cursor;
				}

				// Token: 0x04002A68 RID: 10856
				public readonly global::UICamera.Mouse.Button.Flags Button;

				// Token: 0x04002A69 RID: 10857
				public readonly global::UICamera.CursorSampler Cursor;

				// Token: 0x04002A6A RID: 10858
				public GameObject Pressed;

				// Token: 0x04002A6B RID: 10859
				public global::UIHotSpot.Hit Hit;

				// Token: 0x04002A6C RID: 10860
				public Vector2 Point;

				// Token: 0x04002A6D RID: 10861
				public Vector2 TotalDelta;

				// Token: 0x04002A6E RID: 10862
				public ulong ClickCount;

				// Token: 0x04002A6F RID: 10863
				public ulong DragClickNumber;

				// Token: 0x04002A70 RID: 10864
				public float PressTime;

				// Token: 0x04002A71 RID: 10865
				public float ReleaseTime;

				// Token: 0x04002A72 RID: 10866
				public global::UICamera.ClickNotification ClickNotification;

				// Token: 0x04002A73 RID: 10867
				public bool PressedNow;

				// Token: 0x04002A74 RID: 10868
				public bool Held;

				// Token: 0x04002A75 RID: 10869
				public bool ReleasedNow;

				// Token: 0x04002A76 RID: 10870
				public bool DidHit;

				// Token: 0x04002A77 RID: 10871
				public bool Once;

				// Token: 0x04002A78 RID: 10872
				public bool DragClick;
			}
		}

		// Token: 0x020008C4 RID: 2244
		public struct State
		{
			// Token: 0x04002A79 RID: 10873
			public Vector2 Point;

			// Token: 0x04002A7A RID: 10874
			public Vector2 Delta;

			// Token: 0x04002A7B RID: 10875
			public Vector2 Scroll;

			// Token: 0x04002A7C RID: 10876
			public global::UICamera.Mouse.Button.PressState Buttons;
		}
	}

	// Token: 0x020008C5 RID: 2245
	public sealed class CursorSampler
	{
		// Token: 0x06004CBB RID: 19643 RVA: 0x0012C61C File Offset: 0x0012A81C
		public CursorSampler()
		{
			this.Buttons.LeftValue = new global::UICamera.Mouse.Button.Sampler(global::UICamera.Mouse.Button.Flags.Left, this);
			this.Buttons.RightValue = new global::UICamera.Mouse.Button.Sampler(global::UICamera.Mouse.Button.Flags.Right, this);
			this.Buttons.MiddleValue = new global::UICamera.Mouse.Button.Sampler(global::UICamera.Mouse.Button.Flags.Middle, this);
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06004CBC RID: 19644 RVA: 0x0012C670 File Offset: 0x0012A870
		public Vector2 Point
		{
			get
			{
				return this.Current.Mouse.Point;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06004CBD RID: 19645 RVA: 0x0012C684 File Offset: 0x0012A884
		public Vector2 FrameDelta
		{
			get
			{
				return this.Current.Mouse.Delta;
			}
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0012C698 File Offset: 0x0012A898
		private static void ExitDragHover(GameObject lander, GameObject drop, global::DropNotificationFlags flags)
		{
			if ((flags & global::DropNotificationFlags.ReverseHover) == global::DropNotificationFlags.ReverseHover)
			{
				if ((flags & global::DropNotificationFlags.DragHover) == global::DropNotificationFlags.DragHover)
				{
					drop.NGUIMessage("OnDragHoverExit", lander);
				}
				if ((flags & global::DropNotificationFlags.LandHover) == global::DropNotificationFlags.LandHover)
				{
					lander.NGUIMessage("OnLandHoverExit", drop);
				}
			}
			else
			{
				if ((flags & global::DropNotificationFlags.LandHover) == global::DropNotificationFlags.LandHover)
				{
					lander.NGUIMessage("OnLandHoverExit", drop);
				}
				if ((flags & global::DropNotificationFlags.DragHover) == global::DropNotificationFlags.DragHover)
				{
					drop.NGUIMessage("OnDragHoverExit", lander);
				}
			}
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0012C730 File Offset: 0x0012A930
		private static void EnterDragHover(GameObject lander, GameObject drop, global::DropNotificationFlags flags)
		{
			if ((flags & global::DropNotificationFlags.ReverseHover) == global::DropNotificationFlags.ReverseHover)
			{
				if ((flags & global::DropNotificationFlags.LandHover) == global::DropNotificationFlags.LandHover)
				{
					lander.NGUIMessage("OnLandHoverEnter", drop);
				}
				if ((flags & global::DropNotificationFlags.DragHover) == global::DropNotificationFlags.DragHover)
				{
					drop.NGUIMessage("OnDragHoverEnter", lander);
				}
			}
			else
			{
				if ((flags & global::DropNotificationFlags.DragHover) == global::DropNotificationFlags.DragHover)
				{
					drop.NGUIMessage("OnDragHoverEnter", lander);
				}
				if ((flags & global::DropNotificationFlags.LandHover) == global::DropNotificationFlags.LandHover)
				{
					lander.NGUIMessage("OnLandHoverEnter", drop);
				}
			}
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x0012C7C8 File Offset: 0x0012A9C8
		private void CheckDragHover(bool HasCurrent, GameObject Current, GameObject Pressed)
		{
			if (HasCurrent)
			{
				if (this.DragHover == Current)
				{
					return;
				}
				if (this.DragHover && this.DragHover != Pressed)
				{
					global::UICamera.CursorSampler.ExitDragHover(Pressed, this.DragHover, this.LastHoverDropNotification);
				}
				this.DragHover = Current;
				if (Current != Pressed)
				{
					this.LastHoverDropNotification = this.DropNotification;
					global::UICamera.CursorSampler.EnterDragHover(Pressed, this.DragHover, this.LastHoverDropNotification);
				}
			}
			else
			{
				this.ClearDragHover(Pressed);
			}
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x0012C860 File Offset: 0x0012AA60
		private void ClearDragHover(GameObject Pressed)
		{
			if (this.DragHover)
			{
				if (this.DragHover != Pressed)
				{
					global::UICamera.CursorSampler.ExitDragHover(Pressed, this.DragHover, this.LastHoverDropNotification);
				}
				this.DragHover = null;
			}
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0012C8A8 File Offset: 0x0012AAA8
		internal void MouseEvent(NGUIHack.Event @event, EventType type)
		{
			global::UICamera.CursorSampler.Sample current;
			current.Mouse.Scroll = default(Vector2);
			current.Mouse.Buttons.Pressed = (global::UICamera.Mouse.Button.Held | global::UICamera.Mouse.Button.NewlyPressed);
			current.Mouse.Point = @event.mousePosition;
			if (this.Current.Valid)
			{
				current.IsFirst = false;
				if (this.Current.Mouse.Point.x != current.Mouse.Point.x)
				{
					current.Mouse.Delta.x = current.Mouse.Point.x - this.Current.Mouse.Point.x;
					if (this.Current.Mouse.Point.y != current.Mouse.Point.y)
					{
						current.Mouse.Delta.y = current.Mouse.Point.y - this.Current.Mouse.Point.y;
					}
					else
					{
						current.Mouse.Delta.y = 0f;
					}
					current.DidMove = true;
				}
				else if (this.Current.Mouse.Point.y != current.Mouse.Point.y)
				{
					current.Mouse.Delta.x = 0f;
					current.Mouse.Delta.y = current.Mouse.Point.y - this.Current.Mouse.Point.y;
					current.DidMove = true;
				}
				else
				{
					current.DidMove = false;
					current.Mouse.Delta.x = (current.Mouse.Delta.y = 0f);
				}
			}
			else
			{
				current.IsFirst = true;
				current.DidMove = false;
				current.Mouse.Delta.x = (current.Mouse.Delta.y = 0f);
			}
			current.Hit = global::UIHotSpot.Hit.invalid;
			if (current.DidHit = global::UICamera.Raycast(current.Mouse.Point, ref current.Hit, out current.UICamera))
			{
				global::UICamera.lastHit = current.Hit;
				current.Under = current.Hit.gameObject;
				current.HasUnder = true;
			}
			else if (global::UICamera.fallThrough)
			{
				current.Under = global::UICamera.fallThrough;
				current.HasUnder = true;
				current.UICamera = global::UICamera.FindCameraForLayer(global::UICamera.fallThrough.layer);
				if (!current.UICamera)
				{
					current.UICamera = ((current.IsFirst || !this.Current.UICamera) ? global::UICamera.mList[(int)global::UICamera.mListSort[0]] : this.Current.UICamera);
				}
			}
			else
			{
				current.Under = null;
				current.HasUnder = false;
				current.UICamera = ((current.IsFirst || !this.Current.UICamera) ? global::UICamera.mList[(int)global::UICamera.mListSort[0]] : this.Current.UICamera);
			}
			current.UnderChange = (current.IsFirst || ((!current.HasUnder) ? this.Current.HasUnder : (!this.Current.HasUnder || this.Current.Under != current.Under)));
			current.HoverChange = (current.UnderChange && current.Under != global::UICamera.mHover);
			current.ButtonChange = global::UICamera.Mouse.Button.AnyNewlyPressedOrReleased;
			bool flag = false;
			if (current.ButtonChange && global::UICamera.Mouse.Button.AnyNewlyPressedThatCancelTooltips)
			{
				current.UICamera.mTooltipTime = 0f;
			}
			else
			{
				if (current.DidMove && (current.HoverChange || !current.UICamera.stickyTooltip))
				{
					if (current.UICamera.mTooltipTime != 0f)
					{
						current.UICamera.mTooltipTime = Time.realtimeSinceStartup + current.UICamera.tooltipDelay;
					}
					else if (current.UICamera.mTooltip != null)
					{
						flag = true;
						current.UICamera.ShowTooltip(false);
					}
				}
				if (current.HoverChange && global::UICamera.mHover)
				{
					if (current.UICamera.mTooltip != null)
					{
						current.UICamera.ShowTooltip(false);
					}
					global::UICamera.Highlight(global::UICamera.mHover, false);
					global::UICamera.mHover = null;
				}
			}
			current.Time = Time.realtimeSinceStartup;
			current.ButtonsPressed = global::UICamera.Mouse.Button.NewlyPressed;
			current.ButtonsReleased = global::UICamera.Mouse.Button.NewlyReleased;
			if (!flag && current.ButtonsPressed != (global::UICamera.Mouse.Button.Flags)0 && current.UICamera.mTooltip)
			{
				current.UICamera.ShowTooltip(false);
				flag = true;
			}
			for (global::UICamera.Mouse.Button.Flags flags = global::UICamera.Mouse.Button.Flags.Left; flags < (global::UICamera.Mouse.Button.Flags.Left | global::UICamera.Mouse.Button.Flags.Right | global::UICamera.Mouse.Button.Flags.Middle); flags <<= 1)
			{
				global::UICamera.Mouse.Button.Sampler sampler = this.Buttons[flags];
				try
				{
					this.CurrentButton = sampler;
					sampler.PressedNow = (sampler.ReleasedNow = false);
					if ((current.ButtonsPressed & flags) == flags)
					{
						if (sampler.Once)
						{
							float releaseTime = sampler.ReleaseTime;
						}
						else
						{
							float num = sampler.ReleaseTime = current.Time - 120f;
							sampler.Once = true;
						}
						sampler.PressTime = current.Time;
						sampler.Pressed = current.Under;
						sampler.DidHit = current.DidHit;
						sampler.PressedNow = true;
						sampler.Hit = current.Hit;
						sampler.ReleasedNow = false;
						sampler.Held = true;
						sampler.Point = current.Mouse.Point;
						sampler.TotalDelta.x = (sampler.TotalDelta.y = 0f);
						sampler.ClickNotification = global::UICamera.ClickNotification.Always;
						if (flags == global::UICamera.Mouse.Button.Flags.Left)
						{
							this.Dragging = false;
							this.DropNotification = global::DropNotificationFlags.DragDrop;
							sampler.DragClick = false;
							sampler.DragClickNumber = 0UL;
						}
						else if (this.Dragging)
						{
							sampler.DragClick = true;
							sampler.DragClickNumber = this.Buttons.LeftValue.ClickCount;
						}
						else
						{
							sampler.DragClick = false;
							sampler.DragClickNumber = 0UL;
						}
						if (current.DidHit)
						{
							if (flags == global::UICamera.Mouse.Button.Flags.Left)
							{
								global::UICamera.mPressInput = current.Under.GetComponent<global::UIInput>();
								if (global::UICamera.mSelInput)
								{
									if (global::UICamera.mPressInput)
									{
										if (global::UICamera.mSelInput == global::UICamera.mPressInput)
										{
											global::UICamera.mSelInput.OnEvent(current.UICamera, @event, type);
										}
										else
										{
											global::UICamera.mSelInput.LoseFocus();
											global::UICamera.mSelInput = null;
											global::UICamera.mPressInput.GainFocus();
											global::UICamera.mPressInput.OnEvent(current.UICamera, @event, type);
										}
									}
									else
									{
										global::UICamera.mSelInput.LoseFocus();
										global::UICamera.mSelInput = null;
									}
								}
								else if (global::UICamera.mPressInput)
								{
									global::UICamera.mPressInput.GainFocus();
									global::UICamera.mPressInput.OnEvent(current.UICamera, @event, type);
								}
								if (global::UICamera.mSel && global::UICamera.mSel != current.Under)
								{
									if (!flag && current.UICamera.mTooltip)
									{
										current.UICamera.ShowTooltip(false);
									}
									global::UICamera.SetSelectedObject(null);
								}
								this.Panel = global::UIPanel.FindRoot(current.Under.transform);
								if (this.Panel)
								{
									if (this.Panel != global::UICamera.popupPanel && global::UICamera.popupPanel)
									{
										global::UICamera.PopupPanel(null);
									}
									current.Under.Press(true);
									this.Panel.gameObject.NGUIMessage("OnChildPress", true);
								}
								else
								{
									if (global::UICamera.popupPanel)
									{
										global::UICamera.PopupPanel(null);
									}
									current.Under.Press(true);
								}
								this.PressDropNotification = this.DropNotification;
							}
							else
							{
								if (global::UICamera.mSelInput)
								{
									global::UICamera.mSelInput.OnEvent(current.UICamera, @event, type);
								}
								if (!sampler.DragClick)
								{
									if (flags == global::UICamera.Mouse.Button.Flags.Right)
									{
										global::UIPanel uipanel = global::UIPanel.FindRoot(current.Under.transform);
										if (global::UICamera.popupPanel && global::UICamera.popupPanel != uipanel)
										{
											global::UICamera.PopupPanel(null);
										}
										current.Under.AltPress(true);
									}
									else if (flags == global::UICamera.Mouse.Button.Flags.Middle)
									{
										current.Under.MidPress(true);
									}
								}
							}
							@event.Use();
						}
						else if (flags == global::UICamera.Mouse.Button.Flags.Left)
						{
							if (global::UICamera.popupPanel)
							{
								global::UICamera.PopupPanel(null);
							}
							global::UICamera.mPressInput = null;
							if (global::UICamera.mSelInput)
							{
								global::UICamera.mSelInput.LoseFocus();
								global::UICamera.mSelInput = null;
							}
							if (global::UICamera.mSel)
							{
								if (!flag && current.UICamera.mTooltip)
								{
									current.UICamera.ShowTooltip(false);
								}
								global::UICamera.SetSelectedObject(null);
							}
						}
					}
					else if (sampler.Held && sampler.DidHit)
					{
						if (type == 3 && flags == global::UICamera.Mouse.Button.Flags.Left)
						{
							if (global::UICamera.mPressInput)
							{
								global::UICamera.mPressInput.OnEvent(current.UICamera, @event, type);
							}
							@event.Use();
						}
						if (current.DidMove)
						{
							if (!flag && current.UICamera.mTooltip)
							{
								current.UICamera.ShowTooltip(false);
							}
							global::UICamera.Mouse.Button.Sampler sampler2 = sampler;
							sampler2.TotalDelta.x = sampler2.TotalDelta.x + current.Mouse.Delta.x;
							global::UICamera.Mouse.Button.Sampler sampler3 = sampler;
							sampler3.TotalDelta.y = sampler3.TotalDelta.y + current.Mouse.Delta.y;
							bool flag2 = sampler.ClickNotification == global::UICamera.ClickNotification.None;
							if (flags == global::UICamera.Mouse.Button.Flags.Left && !sampler.DragClick && (this.PressDropNotification & (global::DropNotificationFlags.DragDrop | global::DropNotificationFlags.DragLand | global::DropNotificationFlags.AltDrop | global::DropNotificationFlags.AltLand | global::DropNotificationFlags.MidDrop | global::DropNotificationFlags.MidLand)) != (global::DropNotificationFlags)0)
							{
								if (!this.Dragging)
								{
									sampler.Pressed.DragState(true);
									this.Dragging = true;
								}
								sampler.Pressed.Drag(current.Mouse.Delta);
								this.CheckDragHover(current.DidHit, current.Under, sampler.Pressed);
							}
							if (flag2)
							{
								sampler.ClickNotification = global::UICamera.ClickNotification.None;
							}
							else if (sampler.ClickNotification == global::UICamera.ClickNotification.BasedOnDelta)
							{
								float num2;
								if (flags == global::UICamera.Mouse.Button.Flags.Left)
								{
									num2 = current.UICamera.mouseClickThreshold;
								}
								else
								{
									num2 = (float)Screen.height * 0.1f;
									if (num2 < current.UICamera.touchClickThreshold)
									{
										num2 = current.UICamera.touchClickThreshold;
									}
								}
								if (sampler.TotalDelta.x * sampler.TotalDelta.x + sampler.TotalDelta.y * sampler.TotalDelta.y > num2 * num2)
								{
									sampler.ClickNotification = global::UICamera.ClickNotification.None;
								}
							}
						}
					}
				}
				finally
				{
					this.CurrentButton = null;
				}
			}
			for (global::UICamera.Mouse.Button.Flags flags2 = global::UICamera.Mouse.Button.Flags.Middle; flags2 != (global::UICamera.Mouse.Button.Flags)0; flags2 >>= 1)
			{
				global::UICamera.Mouse.Button.Sampler sampler4 = this.Buttons[flags2];
				try
				{
					this.CurrentButton = sampler4;
					if ((current.ButtonsReleased & flags2) == flags2)
					{
						sampler4.ReleasedNow = true;
						if (sampler4.DidHit)
						{
							if (flags2 == global::UICamera.Mouse.Button.Flags.Left)
							{
								if ((type == 1 || type == 5) && global::UICamera.mPressInput && sampler4.Pressed == global::UICamera.mPressInput.gameObject)
								{
									global::UICamera.mPressInput.OnEvent(current.UICamera, @event, type);
									global::UICamera.mSelInput = global::UICamera.mPressInput;
								}
								global::UICamera.mPressInput = null;
								if (current.HasUnder)
								{
									if (sampler4.Pressed == current.Under)
									{
										if (this.Dragging && (this.PressDropNotification & (global::DropNotificationFlags.DragDrop | global::DropNotificationFlags.DragLand | global::DropNotificationFlags.AltDrop | global::DropNotificationFlags.AltLand | global::DropNotificationFlags.MidDrop | global::DropNotificationFlags.MidLand)) != (global::DropNotificationFlags)0)
										{
											this.ClearDragHover(sampler4.Pressed);
											if (!sampler4.DragClick)
											{
												sampler4.Pressed.DragState(false);
											}
										}
										if (this.Panel)
										{
											this.Panel.gameObject.NGUIMessage("OnChildPress", false);
										}
										sampler4.Pressed.Press(false);
										if (sampler4.Pressed == global::UICamera.mHover)
										{
											sampler4.Pressed.Hover(true);
										}
										if (sampler4.Pressed != global::UICamera.mSel)
										{
											global::UICamera.mSel = sampler4.Pressed;
											sampler4.Pressed.Select(true);
										}
										else
										{
											global::UICamera.mSel = sampler4.Pressed;
										}
										if (!sampler4.DragClick && sampler4.ClickNotification != global::UICamera.ClickNotification.None)
										{
											if (this.Panel)
											{
												this.Panel.gameObject.NGUIMessage("OnChildClick", sampler4.Pressed);
											}
											if (sampler4.ClickNotification != global::UICamera.ClickNotification.None)
											{
												sampler4.Pressed.Click();
												if (sampler4.ClickNotification != global::UICamera.ClickNotification.None && sampler4.ReleaseTime + 0.25f > current.Time)
												{
													sampler4.Pressed.DoubleClick();
												}
											}
										}
										else if (this.Panel)
										{
											this.Panel.gameObject.NGUIMessage("OnChildClickCanceled", sampler4.Pressed);
										}
									}
									else
									{
										if (this.Dragging && !sampler4.DragClick && (this.PressDropNotification & (global::DropNotificationFlags.DragDrop | global::DropNotificationFlags.DragLand | global::DropNotificationFlags.AltDrop | global::DropNotificationFlags.AltLand | global::DropNotificationFlags.MidDrop | global::DropNotificationFlags.MidLand)) != (global::DropNotificationFlags)0)
										{
											global::DropNotification.DropMessage(ref this.DropNotification, global::DragEventKind.Drag, sampler4.Pressed, current.Under);
											this.ClearDragHover(sampler4.Pressed);
											sampler4.Pressed.DragState(false);
										}
										if (this.Panel)
										{
											this.Panel.gameObject.NGUIMessage("OnChildPress", false);
										}
										sampler4.Pressed.Press(false);
										if (sampler4.Pressed == global::UICamera.mHover)
										{
											sampler4.Pressed.Hover(true);
										}
									}
								}
								else if (this.Dragging)
								{
									this.ClearDragHover(sampler4.Pressed);
									if (!sampler4.DragClick)
									{
										global::DropNotification.DropMessage(ref this.DropNotification, global::DragEventKind.Drag, sampler4.Pressed, current.Under);
										sampler4.Pressed.DragState(false);
									}
									if (this.Panel)
									{
										this.Panel.gameObject.NGUIMessage("OnChildPress", false);
									}
									sampler4.Pressed.Press(false);
									if (sampler4.Pressed == global::UICamera.mHover)
									{
										sampler4.Pressed.Hover(true);
									}
									this.Dragging = false;
								}
							}
							else if (sampler4.DragClick)
							{
								if (!this.Buttons.LeftValue.DragClick && this.Buttons.LeftValue.ClickCount == sampler4.DragClickNumber)
								{
									bool flag3;
									if (flags2 == global::UICamera.Mouse.Button.Flags.Right)
									{
										flag3 = global::DropNotification.DropMessage(ref this.DropNotification, global::DragEventKind.Alt, this.Buttons.LeftValue.Pressed, sampler4.Pressed);
									}
									else
									{
										flag3 = (flags2 == global::UICamera.Mouse.Button.Flags.Middle && global::DropNotification.DropMessage(ref this.DropNotification, global::DragEventKind.Mid, this.Buttons.LeftValue.Pressed, sampler4.Pressed));
									}
									if (flag3)
									{
										this.Buttons.LeftValue.DragClick = true;
										this.ClearDragHover(this.Buttons.LeftValue.Pressed);
										sampler4.Pressed.DragState(false);
									}
								}
							}
							else if (flags2 == global::UICamera.Mouse.Button.Flags.Right)
							{
								sampler4.Pressed.AltPress(false);
								if (current.HasUnder && sampler4.Pressed == current.Under && sampler4.ClickNotification != global::UICamera.ClickNotification.None)
								{
									sampler4.Pressed.AltClick();
									if (sampler4.ClickNotification != global::UICamera.ClickNotification.None && sampler4.ReleaseTime + 0.25f > current.Time)
									{
										sampler4.Pressed.AltDoubleClick();
									}
								}
							}
							else if (flags2 == global::UICamera.Mouse.Button.Flags.Middle)
							{
								sampler4.Pressed.MidPress(false);
								if (current.HasUnder && sampler4.Pressed == current.Under && sampler4.ClickNotification != global::UICamera.ClickNotification.None)
								{
									sampler4.Pressed.MidClick();
									if (sampler4.ClickNotification != global::UICamera.ClickNotification.None && sampler4.ReleaseTime + 0.25f > current.Time)
									{
										sampler4.Pressed.MidDoubleClick();
									}
								}
							}
						}
						sampler4.ReleasedNow = true;
						sampler4.ClickNotification = global::UICamera.ClickNotification.None;
						sampler4.ReleaseTime = current.Time;
						sampler4.Held = false;
						sampler4.ClickCount += 1UL;
						sampler4.DragClick = false;
						sampler4.DragClickNumber = 0UL;
						if (flags2 == global::UICamera.Mouse.Button.Flags.Left)
						{
							this.Dragging = false;
							this.Panel = null;
						}
						if (@event.type == 1 || @event.type == 5)
						{
							@event.Use();
						}
					}
				}
				finally
				{
					this.CurrentButton = null;
				}
			}
			global::UICamera.lastMousePosition = ((!current.IsFirst) ? this.Current.Mouse.Point : current.Mouse.Point);
			if (current.HasUnder && (current.Mouse.Buttons.NonePressed || (this.Dragging && (this.DropNotification & global::DropNotificationFlags.RegularHover) == global::DropNotificationFlags.RegularHover)) && global::UICamera.mHover != current.Under)
			{
				current.UICamera.mTooltipTime = current.Time + current.UICamera.tooltipDelay;
				global::UICamera.mHover = current.Under;
				global::UICamera.Highlight(global::UICamera.mHover, true);
			}
			current.Valid = true;
			this.Last = this.Current;
			this.Current = current;
		}

		// Token: 0x04002A7D RID: 10877
		private const float kDoubleClickLimit = 0.25f;

		// Token: 0x04002A7E RID: 10878
		public global::UICamera.Mouse.Button.ValCollection<global::UICamera.Mouse.Button.Sampler> Buttons;

		// Token: 0x04002A7F RID: 10879
		public global::DropNotificationFlags DropNotification;

		// Token: 0x04002A80 RID: 10880
		public bool Dragging;

		// Token: 0x04002A81 RID: 10881
		public global::UICamera.CursorSampler.Sample Current;

		// Token: 0x04002A82 RID: 10882
		public global::UICamera.CursorSampler.Sample Last;

		// Token: 0x04002A83 RID: 10883
		public float LastClickTime = float.MaxValue;

		// Token: 0x04002A84 RID: 10884
		public bool IsFirst;

		// Token: 0x04002A85 RID: 10885
		public bool IsLast;

		// Token: 0x04002A86 RID: 10886
		public bool IsCurrent;

		// Token: 0x04002A87 RID: 10887
		public global::UICamera.Mouse.Button.Sampler CurrentButton;

		// Token: 0x04002A88 RID: 10888
		private global::DropNotificationFlags LastHoverDropNotification;

		// Token: 0x04002A89 RID: 10889
		private global::DropNotificationFlags PressDropNotification;

		// Token: 0x04002A8A RID: 10890
		private GameObject DragHover;

		// Token: 0x04002A8B RID: 10891
		private global::UIPanel Panel;

		// Token: 0x020008C6 RID: 2246
		public struct Sample
		{
			// Token: 0x17000E96 RID: 3734
			// (get) Token: 0x06004CC3 RID: 19651 RVA: 0x0012DC08 File Offset: 0x0012BE08
			public Camera Camera
			{
				get
				{
					return (!this.UICamera) ? null : this.UICamera.cachedCamera;
				}
			}

			// Token: 0x06004CC4 RID: 19652 RVA: 0x0012DC2C File Offset: 0x0012BE2C
			public static bool operator true(global::UICamera.CursorSampler.Sample sample)
			{
				return sample.Valid;
			}

			// Token: 0x06004CC5 RID: 19653 RVA: 0x0012DC38 File Offset: 0x0012BE38
			public static bool operator false(global::UICamera.CursorSampler.Sample sample)
			{
				return !sample.Valid;
			}

			// Token: 0x04002A8C RID: 10892
			public GameObject Under;

			// Token: 0x04002A8D RID: 10893
			public global::UICamera UICamera;

			// Token: 0x04002A8E RID: 10894
			public global::UICamera.Mouse.State Mouse;

			// Token: 0x04002A8F RID: 10895
			public global::UIHotSpot.Hit Hit;

			// Token: 0x04002A90 RID: 10896
			public float Time;

			// Token: 0x04002A91 RID: 10897
			public bool DidHit;

			// Token: 0x04002A92 RID: 10898
			public bool HasUnder;

			// Token: 0x04002A93 RID: 10899
			public bool Valid;

			// Token: 0x04002A94 RID: 10900
			public bool DidMove;

			// Token: 0x04002A95 RID: 10901
			public bool IsFirst;

			// Token: 0x04002A96 RID: 10902
			public bool ButtonChange;

			// Token: 0x04002A97 RID: 10903
			public bool UnderChange;

			// Token: 0x04002A98 RID: 10904
			public bool HoverChange;

			// Token: 0x04002A99 RID: 10905
			public global::UICamera.Mouse.Button.Flags ButtonsPressed;

			// Token: 0x04002A9A RID: 10906
			public global::UICamera.Mouse.Button.Flags ButtonsReleased;
		}
	}

	// Token: 0x020008C7 RID: 2247
	private static class LateLoadCursor
	{
		// Token: 0x04002A9B RID: 10907
		public static readonly global::UICamera.CursorSampler Sampler = new global::UICamera.CursorSampler();
	}

	// Token: 0x020008C8 RID: 2248
	public struct BackwardsCompatabilitySupport
	{
		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x0012DC50 File Offset: 0x0012BE50
		// (set) Token: 0x06004CC8 RID: 19656 RVA: 0x0012DC68 File Offset: 0x0012BE68
		public global::UICamera.ClickNotification clickNotification
		{
			get
			{
				return global::UICamera.Cursor.Buttons.LeftValue.ClickNotification;
			}
			set
			{
				global::UICamera.Cursor.Buttons.LeftValue.ClickNotification = value;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06004CC9 RID: 19657 RVA: 0x0012DC80 File Offset: 0x0012BE80
		public Vector2 pos
		{
			get
			{
				return (global::UICamera.Cursor.CurrentButton != null) ? (global::UICamera.Cursor.CurrentButton.Point + global::UICamera.Cursor.CurrentButton.TotalDelta) : global::UICamera.Cursor.Current.Mouse.Point;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x0012DCD8 File Offset: 0x0012BED8
		public Vector2 delta
		{
			get
			{
				return global::UICamera.Cursor.FrameDelta;
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x0012DCE4 File Offset: 0x0012BEE4
		public Vector2 totalDelta
		{
			get
			{
				return global::UICamera.Cursor.Buttons.LeftValue.TotalDelta;
			}
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x0012DCFC File Offset: 0x0012BEFC
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x0012DD00 File Offset: 0x0012BF00
		public override int GetHashCode()
		{
			return -1;
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x0012DD04 File Offset: 0x0012BF04
		public override string ToString()
		{
			return string.Format("[BackwardsCompatabilitySupport: clickNotification={0}, pos={1}, delta={2}, totalDelta={3}]", new object[]
			{
				this.clickNotification,
				this.pos,
				this.delta,
				this.totalDelta
			});
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x0012DD5C File Offset: 0x0012BF5C
		public static bool operator ==(global::UICamera.BackwardsCompatabilitySupport b, bool? s)
		{
			return global::UICamera.Cursor.Current.Valid == (s != null);
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x0012DD78 File Offset: 0x0012BF78
		public static bool operator !=(global::UICamera.BackwardsCompatabilitySupport b, bool? s)
		{
			return global::UICamera.Cursor.Current.Valid != (s != null);
		}
	}

	// Token: 0x020008C9 RID: 2249
	public enum ClickNotification
	{
		// Token: 0x04002A9D RID: 10909
		None,
		// Token: 0x04002A9E RID: 10910
		Always,
		// Token: 0x04002A9F RID: 10911
		BasedOnDelta
	}

	// Token: 0x020008CA RID: 2250
	private class Highlighted
	{
		// Token: 0x04002AA0 RID: 10912
		public GameObject go;

		// Token: 0x04002AA1 RID: 10913
		public int counter;
	}

	// Token: 0x020008CB RID: 2251
	private struct RaycastCheckWork
	{
		// Token: 0x06004CD2 RID: 19666 RVA: 0x0012DDA0 File Offset: 0x0012BFA0
		public bool Check()
		{
			global::UIPanel uipanel = global::UIPanel.Find(this.hit.collider.transform, false);
			if (!uipanel)
			{
				return true;
			}
			if (uipanel.enabled && (uipanel.clipping == global::UIDrawCall.Clipping.None || global::UICamera.CheckRayEnterClippingRect(this.ray, uipanel.transform, uipanel.clipRange)))
			{
				return true;
			}
			Collider collider = this.hit.collider;
			bool result;
			try
			{
				collider.enabled = false;
				if (Physics.Raycast(this.ray, ref this.hit, this.dist, this.mask))
				{
					result = this.Check();
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				collider.enabled = true;
			}
			return result;
		}

		// Token: 0x04002AA2 RID: 10914
		public Ray ray;

		// Token: 0x04002AA3 RID: 10915
		public RaycastHit hit;

		// Token: 0x04002AA4 RID: 10916
		public float dist;

		// Token: 0x04002AA5 RID: 10917
		public int mask;
	}

	// Token: 0x020008CC RID: 2252
	private class CamSorter : Comparer<byte>
	{
		// Token: 0x06004CD4 RID: 19668 RVA: 0x0012DE80 File Offset: 0x0012C080
		public override int Compare(byte a, byte b)
		{
			return global::UICamera.mList[(int)b].cachedCamera.depth.CompareTo(global::UICamera.mList[(int)a].cachedCamera.depth);
		}
	}
}
