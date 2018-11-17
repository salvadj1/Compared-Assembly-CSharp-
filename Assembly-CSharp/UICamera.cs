using System;
using System.Collections;
using System.Collections.Generic;
using NGUI.MessageUtil;
using NGUIHack;
using UnityEngine;

// Token: 0x020007C7 RID: 1991
[AddComponentMenu("NGUI/UI/Camera")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class UICamera : MonoBehaviour
{
	// Token: 0x0600478B RID: 18315 RVA: 0x00120520 File Offset: 0x0011E720
	public static bool PopupPanel(UIPanel panel)
	{
		if (UICamera.popupPanel == panel)
		{
			return false;
		}
		if (UICamera.popupPanel)
		{
			UICamera.popupPanel.gameObject.NGUIMessage("PopupEnd");
			UICamera.popupPanel = null;
		}
		if (panel)
		{
			UICamera.popupPanel = panel;
			UICamera.popupPanel.gameObject.NGUIMessage("PopupStart");
		}
		return true;
	}

	// Token: 0x0600478C RID: 18316 RVA: 0x00120590 File Offset: 0x0011E790
	public static bool UnPopupPanel(UIPanel panel)
	{
		if (UICamera.popupPanel == panel && panel)
		{
			UICamera.popupPanel.gameObject.NGUIMessage("PopupEnd");
			UICamera.popupPanel = null;
			return true;
		}
		return false;
	}

	// Token: 0x17000DCF RID: 3535
	// (get) Token: 0x0600478D RID: 18317 RVA: 0x001205D8 File Offset: 0x0011E7D8
	public static UICamera.CursorSampler Cursor
	{
		get
		{
			return UICamera.LateLoadCursor.Sampler;
		}
	}

	// Token: 0x17000DD0 RID: 3536
	// (get) Token: 0x0600478E RID: 18318 RVA: 0x001205E0 File Offset: 0x0011E7E0
	public static bool IsPressing
	{
		get
		{
			return UICamera.Cursor.Buttons.LeftValue.Held && UICamera.Cursor.Buttons.LeftValue.Pressed;
		}
	}

	// Token: 0x0600478F RID: 18319 RVA: 0x00120618 File Offset: 0x0011E818
	private void OnEvent(Event @event, EventType type)
	{
		Camera camera = UICamera.currentCamera;
		try
		{
			UICamera.currentCamera = this.cachedCamera;
			switch (type)
			{
			case 0:
			case 1:
			case 2:
			case 3:
				if ((this.mouseMode & UIInputMode.UseEvents) == UIInputMode.UseEvents)
				{
					this.OnMouseEvent(@event, type);
				}
				break;
			case 4:
			case 5:
				if ((this.keyboardMode & UIInputMode.UseEvents) == UIInputMode.UseEvents)
				{
					this.OnKeyboardEvent(@event, type);
				}
				break;
			case 6:
				if ((this.scrollWheelMode & UIInputMode.UseEvents) == UIInputMode.UseEvents)
				{
					this.OnScrollWheelEvent(@event, type);
				}
				break;
			}
		}
		finally
		{
			UICamera.currentCamera = camera;
		}
	}

	// Token: 0x06004790 RID: 18320 RVA: 0x001206D8 File Offset: 0x0011E8D8
	private void OnMouseEvent(Event @event, EventType type)
	{
		if (this.OnEventShared(@event, type))
		{
			return;
		}
		UICamera.Cursor.MouseEvent(@event, type);
	}

	// Token: 0x06004791 RID: 18321 RVA: 0x001206F4 File Offset: 0x0011E8F4
	private void OnScrollWheelEvent(Event @event, EventType type)
	{
		if (UICamera.mHover != null)
		{
			Vector2 delta = @event.delta;
			bool flag = false;
			bool flag2 = false;
			if (delta.y != 0f)
			{
				UICamera.SwallowScroll = false;
				UICamera.mHover.Scroll(delta.y);
				flag2 = !UICamera.SwallowScroll;
			}
			if (delta.x != 0f)
			{
				UICamera.SwallowScroll = false;
				UICamera.mHover.ScrollX(delta.x);
				flag = !UICamera.SwallowScroll;
			}
			if (flag2 || flag)
			{
				UIPanel uipanel = UIPanel.Find(UICamera.mHover.transform);
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

	// Token: 0x06004792 RID: 18322 RVA: 0x001207E8 File Offset: 0x0011E9E8
	private void OnSubmitEvent(Event @event, EventType type)
	{
	}

	// Token: 0x06004793 RID: 18323 RVA: 0x001207EC File Offset: 0x0011E9EC
	private void OnCancelEvent(Event @event, EventType type)
	{
		if (type == 4)
		{
			UICamera.mSel.SendMessage("OnKey", 27, 1);
			@event.Use();
		}
	}

	// Token: 0x06004794 RID: 18324 RVA: 0x00120820 File Offset: 0x0011EA20
	private void OnDirectionEvent(Event @event, int x, int y, EventType type)
	{
		bool flag = false;
		if (type == 4)
		{
			if (x != 0)
			{
				UICamera.mSel.SendMessage("OnKey", (x >= 0) ? 275 : 276, 1);
				flag = true;
			}
			if (y != 0)
			{
				UICamera.mSel.SendMessage("OnKey", (y >= 0) ? 273 : 274, 1);
				flag = true;
			}
		}
		if (flag)
		{
			@event.Use();
		}
	}

	// Token: 0x06004795 RID: 18325 RVA: 0x001208AC File Offset: 0x0011EAAC
	private void OnKeyboardEvent(Event @event, EventType type)
	{
		if (this.OnEventShared(@event, type))
		{
			return;
		}
		char character = @event.character;
		KeyCode keyCode = @event.keyCode;
		bool flag = UICamera.mSelInput;
		if (flag)
		{
			UICamera.mSelInput.OnEvent(this, @event, type);
		}
		if (UICamera.mSel != null)
		{
			KeyCode keyCode2 = keyCode;
			if (keyCode2 != 9)
			{
				if (keyCode2 != 127)
				{
					if (type == 4 && character != '\0')
					{
						UICamera.mSel.Input(character.ToString());
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
					else if (UICamera.inputHasFocus)
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
					UICamera.mSel.Input("\b");
				}
			}
			else if (type == 4)
			{
				UICamera.mSel.Key(9);
			}
		}
	}

	// Token: 0x06004796 RID: 18326 RVA: 0x00120AF8 File Offset: 0x0011ECF8
	private bool OnEventShared(Event @event, EventType type)
	{
		return false;
	}

	// Token: 0x06004797 RID: 18327 RVA: 0x00120AFC File Offset: 0x0011ECFC
	private static void IssueEvent(Event @event, EventType type)
	{
		int button = @event.button;
		KeyCode keyCode = @event.keyCode;
		UICamera uicamera = null;
		switch (type)
		{
		case 0:
			if (button != 0 && UICamera.mMouseCamera.TryGetValue(0, out uicamera) && uicamera)
			{
				uicamera.OnEvent(@event, type);
				if (@event.type != null)
				{
					UICamera.mMouseCamera[button] = uicamera;
					return;
				}
			}
			break;
		case 1:
			if (UICamera.mMouseCamera.TryGetValue(button, out uicamera))
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
				UICamera.mMouseCamera.Remove(button);
			}
			return;
		case 3:
			if (UICamera.mMouseCamera.TryGetValue(0, out uicamera))
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
			if (UICamera.mKeyCamera.TryGetValue(keyCode, out uicamera))
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
				UICamera.mKeyCamera.Remove(keyCode);
			}
			return;
		}
		for (int i = 0; i < UICamera.mListCount; i++)
		{
			UICamera uicamera2 = UICamera.mList[(int)UICamera.mListSort[i]];
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
								UICamera.mKeyCamera[keyCode] = uicamera2;
							}
						}
						else
						{
							UICamera.mMouseCamera[button] = uicamera2;
						}
						return;
					}
				}
			}
		}
	}

	// Token: 0x06004798 RID: 18328 RVA: 0x00120CFC File Offset: 0x0011EEFC
	public static void HandleEvent(Event @event, EventType type)
	{
		switch (type)
		{
		case 0:
			using (new UICamera.Mouse.Button.ButtonPressEventHandler(@event))
			{
				UICamera.IssueEvent(@event, 0);
			}
			return;
		case 1:
			using (new UICamera.Mouse.Button.ButtonReleaseEventHandler(@event))
			{
				UICamera.IssueEvent(@event, 1);
			}
			return;
		case 2:
			if (!UICamera.Mouse.Button.AllowMove)
			{
				return;
			}
			break;
		case 3:
			if (!UICamera.Mouse.Button.AllowDrag)
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
		UICamera.IssueEvent(@event, type);
		if (type == 2 && @event.type == 12)
		{
			Debug.LogWarning("Mouse move was used.");
		}
	}

	// Token: 0x06004799 RID: 18329 RVA: 0x00120E0C File Offset: 0x0011F00C
	public static void Render()
	{
		for (int i = 0; i < UICamera.mListCount; i++)
		{
			if (UICamera.mList[i] && UICamera.mList[i].enabled && UICamera.mList[i].camera && !UICamera.mList[i].camera.enabled)
			{
				UICamera.mList[i].camera.Render();
			}
		}
	}

	// Token: 0x0600479A RID: 18330 RVA: 0x00120E90 File Offset: 0x0011F090
	public UITextPosition RaycastText(Vector3 inPos, UILabel label)
	{
		if (!base.enabled || !base.camera.enabled || !base.camera.pixelRect.Contains(inPos) || !label)
		{
			Debug.Log("No Sir");
			return default(UITextPosition);
		}
		Ray ray = base.camera.ScreenPointToRay(inPos);
		Vector3 forward = label.transform.forward;
		if (Vector3.Dot(ray.direction, forward) <= 0f)
		{
			Debug.Log("Bad Dir");
			return default(UITextPosition);
		}
		Plane plane;
		plane..ctor(forward, label.transform.position);
		float num;
		if (!plane.Raycast(ray, ref num))
		{
			Debug.Log("Paralell");
			return default(UITextPosition);
		}
		Vector3 point = ray.GetPoint(num);
		Vector3[] points = new Vector3[]
		{
			label.transform.InverseTransformPoint(point)
		};
		UITextPosition[] array = new UITextPosition[]
		{
			default(UITextPosition)
		};
		if (label.CalculateTextPosition(1, points, array) == 0)
		{
			Debug.Log("Zero");
		}
		return array[0];
	}

	// Token: 0x17000DD1 RID: 3537
	// (get) Token: 0x0600479B RID: 18331 RVA: 0x00120FE0 File Offset: 0x0011F1E0
	public bool usesAnyEvents
	{
		get
		{
			return ((this.mouseMode | this.keyboardMode | this.scrollWheelMode) & UIInputMode.UseEvents) == UIInputMode.UseEvents;
		}
	}

	// Token: 0x17000DD2 RID: 3538
	// (get) Token: 0x0600479C RID: 18332 RVA: 0x00120FFC File Offset: 0x0011F1FC
	[Obsolete("Use UICamera.currentCamera instead")]
	public static Camera lastCamera
	{
		get
		{
			return UICamera.currentCamera;
		}
	}

	// Token: 0x17000DD3 RID: 3539
	// (get) Token: 0x0600479D RID: 18333 RVA: 0x00121004 File Offset: 0x0011F204
	[Obsolete("Use UICamera.currentTouchID instead")]
	public static int lastTouchID
	{
		get
		{
			return UICamera.currentTouchID;
		}
	}

	// Token: 0x17000DD4 RID: 3540
	// (get) Token: 0x0600479E RID: 18334 RVA: 0x0012100C File Offset: 0x0011F20C
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x17000DD5 RID: 3541
	// (get) Token: 0x0600479F RID: 18335 RVA: 0x0012101C File Offset: 0x0011F21C
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

	// Token: 0x17000DD6 RID: 3542
	// (get) Token: 0x060047A0 RID: 18336 RVA: 0x00121044 File Offset: 0x0011F244
	public static GameObject hoveredObject
	{
		get
		{
			return UICamera.mHover;
		}
	}

	// Token: 0x17000DD7 RID: 3543
	// (get) Token: 0x060047A1 RID: 18337 RVA: 0x0012104C File Offset: 0x0011F24C
	// (set) Token: 0x060047A2 RID: 18338 RVA: 0x00121054 File Offset: 0x0011F254
	public static GameObject selectedObject
	{
		get
		{
			return UICamera.mSel;
		}
		set
		{
			if (!UICamera.SetSelectedObject(value))
			{
				throw new InvalidOperationException("Do not set selectedObject within a OnSelect message.");
			}
		}
	}

	// Token: 0x060047A3 RID: 18339 RVA: 0x0012106C File Offset: 0x0011F26C
	public static bool SetSelectedObject(GameObject value)
	{
		if (UICamera.mSel != value)
		{
			if (UICamera.inSelectionCallback)
			{
				return false;
			}
			UIInput uiinput = (!value) ? null : value.GetComponent<UIInput>();
			if (UICamera.mSelInput != uiinput)
			{
				if (UICamera.mSelInput)
				{
					UICamera.mSelInput.LoseFocus();
				}
				UICamera.mSelInput = uiinput;
				if (uiinput && UICamera.mPressInput != uiinput)
				{
					uiinput.GainFocus();
				}
			}
			if (UICamera.mSel != null)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(UICamera.mSel.layer);
				if (uicamera != null)
				{
					Camera camera = UICamera.currentCamera;
					try
					{
						UICamera.currentCamera = uicamera.mCam;
						UICamera.inSelectionCallback = true;
						UICamera.mSel.Select(false);
						if (uicamera.useController || uicamera.useKeyboard)
						{
							UICamera.Highlight(UICamera.mSel, false);
						}
					}
					finally
					{
						UICamera.currentCamera = camera;
						UICamera.inSelectionCallback = false;
					}
				}
			}
			UICamera.mSel = value;
			if (UICamera.mSel != null)
			{
				UICamera uicamera2 = UICamera.FindCameraForLayer(UICamera.mSel.layer);
				if (uicamera2 != null)
				{
					UICamera.currentCamera = uicamera2.mCam;
					if (uicamera2.useController || uicamera2.useKeyboard)
					{
						UICamera.Highlight(UICamera.mSel, true);
					}
					UICamera.mSel.Select(true);
				}
			}
		}
		return true;
	}

	// Token: 0x060047A4 RID: 18340 RVA: 0x00121204 File Offset: 0x0011F404
	private void OnApplicationQuit()
	{
		UICamera.mHighlighted.Clear();
	}

	// Token: 0x17000DD8 RID: 3544
	// (get) Token: 0x060047A5 RID: 18341 RVA: 0x00121210 File Offset: 0x0011F410
	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			return (!(eventHandler != null)) ? null : eventHandler.cachedCamera;
		}
	}

	// Token: 0x17000DD9 RID: 3545
	// (get) Token: 0x060047A6 RID: 18342 RVA: 0x0012123C File Offset: 0x0011F43C
	public static UICamera eventHandler
	{
		get
		{
			return UICamera.mList[(int)UICamera.mListSort[0]];
		}
	}

	// Token: 0x060047A7 RID: 18343 RVA: 0x0012124C File Offset: 0x0011F44C
	private static int CompareFunc(UICamera a, UICamera b)
	{
		return b.cachedCamera.depth.CompareTo(a.cachedCamera.depth);
	}

	// Token: 0x060047A8 RID: 18344 RVA: 0x00121278 File Offset: 0x0011F478
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

	// Token: 0x060047A9 RID: 18345 RVA: 0x00121328 File Offset: 0x0011F528
	private static bool Raycast(Vector3 inPos, ref UIHotSpot.Hit hit, out UICamera cam)
	{
		if (!Screen.lockCursor)
		{
			for (int i = 0; i < UICamera.mListCount; i++)
			{
				cam = UICamera.mList[(int)UICamera.mListSort[i]];
				if (cam.enabled && cam.gameObject.activeInHierarchy)
				{
					UICamera.currentCamera = cam.cachedCamera;
					Vector3 vector = UICamera.currentCamera.ScreenToViewportPoint(inPos);
					if (vector.x >= -1f && vector.x <= 1f && vector.y >= -1f && vector.y <= 1f)
					{
						UICamera.RaycastCheckWork raycastCheckWork;
						raycastCheckWork.ray = UICamera.currentCamera.ScreenPointToRay(inPos);
						raycastCheckWork.mask = (UICamera.currentCamera.cullingMask & cam.eventReceiverMask);
						raycastCheckWork.dist = ((cam.rangeDistance <= 0f) ? (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane) : cam.rangeDistance);
						if (!cam.onlyHotSpots)
						{
							bool flag = Physics.Raycast(raycastCheckWork.ray, ref raycastCheckWork.hit, raycastCheckWork.dist, raycastCheckWork.mask) && raycastCheckWork.Check();
							if (flag)
							{
								UIHotSpot.Hit hit2;
								if (UIHotSpot.Raycast(raycastCheckWork.ray, out hit2, raycastCheckWork.dist) && hit2.distance <= raycastCheckWork.hit.distance)
								{
									hit = hit2;
								}
								else
								{
									UIHotSpot.ConvertRaycastHit(ref raycastCheckWork.ray, ref raycastCheckWork.hit, out hit);
								}
								return true;
							}
						}
						if (UIHotSpot.Raycast(raycastCheckWork.ray, out hit, raycastCheckWork.dist))
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

	// Token: 0x060047AA RID: 18346 RVA: 0x00121508 File Offset: 0x0011F708
	private static bool Raycast(UICamera cam, Vector3 inPos, ref RaycastHit hit)
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
		UICamera.RaycastCheckWork raycastCheckWork;
		raycastCheckWork.ray = cam.cachedCamera.ScreenPointToRay(inPos);
		raycastCheckWork.mask = (UICamera.currentCamera.cullingMask & cam.eventReceiverMask);
		raycastCheckWork.dist = ((cam.rangeDistance <= 0f) ? (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane) : cam.rangeDistance);
		bool result = Physics.Raycast(raycastCheckWork.ray, ref raycastCheckWork.hit, raycastCheckWork.dist, raycastCheckWork.mask) && raycastCheckWork.Check();
		hit = raycastCheckWork.hit;
		return result;
	}

	// Token: 0x060047AB RID: 18347 RVA: 0x00121600 File Offset: 0x0011F800
	public static UICamera FindCameraForLayer(int layer)
	{
		return UICamera.mList[layer];
	}

	// Token: 0x060047AC RID: 18348 RVA: 0x0012160C File Offset: 0x0011F80C
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

	// Token: 0x060047AD RID: 18349 RVA: 0x0012165C File Offset: 0x0011F85C
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

	// Token: 0x060047AE RID: 18350 RVA: 0x001216D0 File Offset: 0x0011F8D0
	private static int GetDirection(string axis)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (UICamera.mNextEvent < realtimeSinceStartup)
		{
			float axis2 = Input.GetAxis(axis);
			if (axis2 > 0.75f)
			{
				UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return 1;
			}
			if (axis2 < -0.75f)
			{
				UICamera.mNextEvent = realtimeSinceStartup + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x060047AF RID: 18351 RVA: 0x00121728 File Offset: 0x0011F928
	public static bool IsHighlighted(GameObject go)
	{
		int i = UICamera.mHighlighted.Count;
		while (i > 0)
		{
			UICamera.Highlighted highlighted = UICamera.mHighlighted[--i];
			if (highlighted.go == go)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060047B0 RID: 18352 RVA: 0x00121770 File Offset: 0x0011F970
	private static void Highlight(GameObject go, bool highlighted)
	{
		if (go != null)
		{
			int i = UICamera.mHighlighted.Count;
			while (i > 0)
			{
				UICamera.Highlighted highlighted2 = UICamera.mHighlighted[--i];
				if (highlighted2 == null || highlighted2.go == null)
				{
					UICamera.mHighlighted.RemoveAt(i);
				}
				else if (highlighted2.go == go)
				{
					if (highlighted)
					{
						highlighted2.counter++;
					}
					else if (--highlighted2.counter < 1)
					{
						UICamera.mHighlighted.Remove(highlighted2);
						go.Hover(false);
					}
					return;
				}
			}
			if (highlighted)
			{
				UICamera.Highlighted highlighted3 = new UICamera.Highlighted();
				highlighted3.go = go;
				highlighted3.counter = 1;
				UICamera.mHighlighted.Add(highlighted3);
				go.Hover(true);
			}
		}
	}

	// Token: 0x060047B1 RID: 18353 RVA: 0x00121858 File Offset: 0x0011FA58
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
			UIUnityEvents.CameraCreated(this);
		}
	}

	// Token: 0x060047B2 RID: 18354 RVA: 0x0012193C File Offset: 0x0011FB3C
	private void OnDestroy()
	{
		this.RemoveFromList();
	}

	// Token: 0x060047B3 RID: 18355 RVA: 0x00121944 File Offset: 0x0011FB44
	private void AddToList()
	{
		int layer = base.gameObject.layer;
		if (layer != this.lastBoundLayerIndex)
		{
			bool flag;
			if (this.lastBoundLayerIndex != -1 && UICamera.mList[this.lastBoundLayerIndex] == this)
			{
				UICamera.mList[this.lastBoundLayerIndex] = null;
				for (int i = 0; i < UICamera.mListCount; i++)
				{
					if ((int)UICamera.mListSort[i] == this.lastBoundLayerIndex)
					{
						UICamera.mListSort[i] = (byte)layer;
					}
				}
				flag = false;
			}
			else
			{
				UICamera.mListSort[UICamera.mListCount++] = (byte)layer;
				flag = true;
			}
			UICamera.mList[layer] = this;
			this.lastBoundLayerIndex = layer;
			if (flag)
			{
				Array.Sort<byte>(UICamera.mListSort, 0, UICamera.mListCount, UICamera.sorter);
			}
		}
	}

	// Token: 0x060047B4 RID: 18356 RVA: 0x00121A14 File Offset: 0x0011FC14
	private void RemoveFromList()
	{
		if (this.lastBoundLayerIndex != -1)
		{
			UICamera.mList[this.lastBoundLayerIndex] = null;
			int num = 0;
			for (int i = 0; i < UICamera.mListCount; i++)
			{
				if ((int)UICamera.mListSort[i] != this.lastBoundLayerIndex)
				{
					UICamera.mListSort[num++] = UICamera.mListSort[i];
				}
			}
			UICamera.mListCount = num;
			this.lastBoundLayerIndex = -1;
		}
	}

	// Token: 0x060047B5 RID: 18357 RVA: 0x00121A84 File Offset: 0x0011FC84
	private void Update()
	{
		if (!Application.isPlaying || !this.handlesEvents)
		{
			return;
		}
		if (UICamera.mSel != null)
		{
			this.ProcessOthers();
		}
		else
		{
			UICamera.inputHasFocus = false;
		}
		if (this.useMouse && UICamera.mHover != null)
		{
			if ((this.mouseMode & UIInputMode.UseInput) == UIInputMode.UseInput)
			{
				float axis = Input.GetAxis(this.scrollAxisName);
				if (axis != 0f)
				{
					UICamera.mHover.Scroll(axis);
				}
			}
			if (this.mTooltipTime != 0f && this.mTooltipTime < Time.realtimeSinceStartup)
			{
				this.mTooltip = UICamera.mHover;
				this.ShowTooltip(true);
			}
		}
	}

	// Token: 0x060047B6 RID: 18358 RVA: 0x00121B48 File Offset: 0x0011FD48
	private void ProcessOthers()
	{
		int num = 0;
		int num2 = 0;
		if (this.useController)
		{
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				num += UICamera.GetDirection(this.verticalAxisName);
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				num2 += UICamera.GetDirection(this.horizontalAxisName);
			}
		}
		if (num != 0)
		{
			UICamera.mSel.SendMessage("OnKey", (num <= 0) ? 274 : 273, 1);
		}
		if (num2 != 0)
		{
			UICamera.mSel.SendMessage("OnKey", (num2 <= 0) ? 276 : 275, 1);
		}
	}

	// Token: 0x060047B7 RID: 18359 RVA: 0x00121C04 File Offset: 0x0011FE04
	internal bool SetKeyboardFocus(UIInput input)
	{
		return UICamera.mSelInput == input || (!UICamera.mSelInput && input && UICamera.SetSelectedObject(input.gameObject));
	}

	// Token: 0x060047B8 RID: 18360 RVA: 0x00121C4C File Offset: 0x0011FE4C
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

	// Token: 0x040027BB RID: 10171
	private const int kMouseButton0Flag = 1;

	// Token: 0x040027BC RID: 10172
	private const int kMouseButton1Flag = 2;

	// Token: 0x040027BD RID: 10173
	private const int kMouseButton2Flag = 4;

	// Token: 0x040027BE RID: 10174
	private const int kMouseButton3Flag = 8;

	// Token: 0x040027BF RID: 10175
	private const int kMouseButton4Flag = 16;

	// Token: 0x040027C0 RID: 10176
	private const int kMouseButtonCount = 3;

	// Token: 0x040027C1 RID: 10177
	private static UIPanel popupPanel;

	// Token: 0x040027C2 RID: 10178
	public static UICamera.BackwardsCompatabilitySupport currentTouch;

	// Token: 0x040027C3 RID: 10179
	public static bool SwallowScroll;

	// Token: 0x040027C4 RID: 10180
	public bool useMouse = true;

	// Token: 0x040027C5 RID: 10181
	public bool useTouch = true;

	// Token: 0x040027C6 RID: 10182
	public bool allowMultiTouch = true;

	// Token: 0x040027C7 RID: 10183
	public bool useKeyboard = true;

	// Token: 0x040027C8 RID: 10184
	public bool useController = true;

	// Token: 0x040027C9 RID: 10185
	public LayerMask eventReceiverMask = -1;

	// Token: 0x040027CA RID: 10186
	public float tooltipDelay = 1f;

	// Token: 0x040027CB RID: 10187
	public bool stickyTooltip = true;

	// Token: 0x040027CC RID: 10188
	public float mouseClickThreshold = 10f;

	// Token: 0x040027CD RID: 10189
	public float touchClickThreshold = 40f;

	// Token: 0x040027CE RID: 10190
	public float rangeDistance = -1f;

	// Token: 0x040027CF RID: 10191
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x040027D0 RID: 10192
	public string verticalAxisName = "Vertical";

	// Token: 0x040027D1 RID: 10193
	public string horizontalAxisName = "Horizontal";

	// Token: 0x040027D2 RID: 10194
	public KeyCode submitKey0 = 13;

	// Token: 0x040027D3 RID: 10195
	public KeyCode submitKey1 = 330;

	// Token: 0x040027D4 RID: 10196
	public KeyCode cancelKey0 = 27;

	// Token: 0x040027D5 RID: 10197
	public KeyCode cancelKey1 = 331;

	// Token: 0x040027D6 RID: 10198
	public UIInputMode mouseMode = UIInputMode.UseEvents;

	// Token: 0x040027D7 RID: 10199
	public UIInputMode keyboardMode = UIInputMode.UseInputAndEvents;

	// Token: 0x040027D8 RID: 10200
	public UIInputMode scrollWheelMode = UIInputMode.UseEvents;

	// Token: 0x040027D9 RID: 10201
	public bool onlyHotSpots;

	// Token: 0x040027DA RID: 10202
	public static Vector2 lastTouchPosition = Vector2.zero;

	// Token: 0x040027DB RID: 10203
	public static Vector2 lastMousePosition = Vector2.zero;

	// Token: 0x040027DC RID: 10204
	public static UIHotSpot.Hit lastHit;

	// Token: 0x040027DD RID: 10205
	public static Camera currentCamera = null;

	// Token: 0x040027DE RID: 10206
	public static int currentTouchID = -1;

	// Token: 0x040027DF RID: 10207
	public static bool inputHasFocus = false;

	// Token: 0x040027E0 RID: 10208
	public static GameObject fallThrough;

	// Token: 0x040027E1 RID: 10209
	private static UICamera[] mList = new UICamera[32];

	// Token: 0x040027E2 RID: 10210
	private static byte[] mListSort = new byte[32];

	// Token: 0x040027E3 RID: 10211
	private static int mListCount = 0;

	// Token: 0x040027E4 RID: 10212
	private static Dictionary<int, UICamera> mMouseCamera = new Dictionary<int, UICamera>();

	// Token: 0x040027E5 RID: 10213
	private static Dictionary<KeyCode, UICamera> mKeyCamera = new Dictionary<KeyCode, UICamera>();

	// Token: 0x040027E6 RID: 10214
	private static List<UICamera.Highlighted> mHighlighted = new List<UICamera.Highlighted>();

	// Token: 0x040027E7 RID: 10215
	private static GameObject mSel = null;

	// Token: 0x040027E8 RID: 10216
	private static UIInput mSelInput = null;

	// Token: 0x040027E9 RID: 10217
	private static UIInput mPressInput = null;

	// Token: 0x040027EA RID: 10218
	private static GameObject mHover;

	// Token: 0x040027EB RID: 10219
	private static float mNextEvent = 0f;

	// Token: 0x040027EC RID: 10220
	private GameObject mTooltip;

	// Token: 0x040027ED RID: 10221
	private Camera mCam;

	// Token: 0x040027EE RID: 10222
	private LayerMask mLayerMask;

	// Token: 0x040027EF RID: 10223
	private float mTooltipTime;

	// Token: 0x040027F0 RID: 10224
	private bool mIsEditor;

	// Token: 0x040027F1 RID: 10225
	private int lastBoundLayerIndex = -1;

	// Token: 0x040027F2 RID: 10226
	private static bool inSelectionCallback;

	// Token: 0x040027F3 RID: 10227
	private static readonly UICamera.CamSorter sorter = new UICamera.CamSorter();

	// Token: 0x020007C8 RID: 1992
	public static class Mouse
	{
		// Token: 0x020007C9 RID: 1993
		public static class Button
		{
			// Token: 0x17000DDA RID: 3546
			// (get) Token: 0x060047BA RID: 18362 RVA: 0x00121C94 File Offset: 0x0011FE94
			internal static UICamera.Mouse.Button.Flags NewlyPressed
			{
				get
				{
					return UICamera.Mouse.Button.pressed;
				}
			}

			// Token: 0x17000DDB RID: 3547
			// (get) Token: 0x060047BB RID: 18363 RVA: 0x00121C9C File Offset: 0x0011FE9C
			internal static UICamera.Mouse.Button.Flags NewlyReleased
			{
				get
				{
					return UICamera.Mouse.Button.released;
				}
			}

			// Token: 0x17000DDC RID: 3548
			// (get) Token: 0x060047BC RID: 18364 RVA: 0x00121CA4 File Offset: 0x0011FEA4
			internal static UICamera.Mouse.Button.Flags Held
			{
				get
				{
					return UICamera.Mouse.Button.held;
				}
			}

			// Token: 0x17000DDD RID: 3549
			// (get) Token: 0x060047BD RID: 18365 RVA: 0x00121CAC File Offset: 0x0011FEAC
			internal static bool AnyNewlyPressed
			{
				get
				{
					return UICamera.Mouse.Button.pressed != (UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x17000DDE RID: 3550
			// (get) Token: 0x060047BE RID: 18366 RVA: 0x00121CBC File Offset: 0x0011FEBC
			internal static bool AnyNewlyReleased
			{
				get
				{
					return UICamera.Mouse.Button.released != (UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x17000DDF RID: 3551
			// (get) Token: 0x060047BF RID: 18367 RVA: 0x00121CCC File Offset: 0x0011FECC
			internal static bool AnyNewlyPressedOrReleased
			{
				get
				{
					return (UICamera.Mouse.Button.pressed | UICamera.Mouse.Button.released) != (UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x17000DE0 RID: 3552
			// (get) Token: 0x060047C0 RID: 18368 RVA: 0x00121CE0 File Offset: 0x0011FEE0
			internal static bool AnyNewlyPressedThatCancelTooltips
			{
				get
				{
					return (UICamera.Mouse.Button.pressed & (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle)) != (UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x060047C1 RID: 18369 RVA: 0x00121CF0 File Offset: 0x0011FEF0
			internal static bool IsNewlyPressed(UICamera.Mouse.Button.Flags flag)
			{
				return (UICamera.Mouse.Button.pressed & flag) == flag;
			}

			// Token: 0x060047C2 RID: 18370 RVA: 0x00121CFC File Offset: 0x0011FEFC
			internal static bool IsNewlyReleased(UICamera.Mouse.Button.Flags flag)
			{
				return (UICamera.Mouse.Button.released & flag) == flag;
			}

			// Token: 0x17000DE1 RID: 3553
			// (get) Token: 0x060047C3 RID: 18371 RVA: 0x00121D08 File Offset: 0x0011FF08
			public static bool AllowDrag
			{
				get
				{
					return UICamera.Mouse.Button.held != (UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x17000DE2 RID: 3554
			// (get) Token: 0x060047C4 RID: 18372 RVA: 0x00121D18 File Offset: 0x0011FF18
			public static bool AllowMove
			{
				get
				{
					return (UICamera.Mouse.Button.held | UICamera.Mouse.Button.released | UICamera.Mouse.Button.pressed) == (UICamera.Mouse.Button.Flags)0;
				}
			}

			// Token: 0x060047C5 RID: 18373 RVA: 0x00121D30 File Offset: 0x0011FF30
			public static UICamera.Mouse.Button.Flags Index(int index)
			{
				if (index < 0 || index >= 3)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (UICamera.Mouse.Button.Flags)(1 << index);
			}

			// Token: 0x040027F4 RID: 10228
			private const UICamera.Mouse.Button.Flags kCancelsTooltips = UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle;

			// Token: 0x040027F5 RID: 10229
			public const UICamera.Mouse.Button.Flags Left = UICamera.Mouse.Button.Flags.Left;

			// Token: 0x040027F6 RID: 10230
			public const UICamera.Mouse.Button.Flags Right = UICamera.Mouse.Button.Flags.Right;

			// Token: 0x040027F7 RID: 10231
			public const UICamera.Mouse.Button.Flags Middle = UICamera.Mouse.Button.Flags.Middle;

			// Token: 0x040027F8 RID: 10232
			public const UICamera.Mouse.Button.Flags Mouse0 = UICamera.Mouse.Button.Flags.Left;

			// Token: 0x040027F9 RID: 10233
			public const UICamera.Mouse.Button.Flags Mouse1 = UICamera.Mouse.Button.Flags.Right;

			// Token: 0x040027FA RID: 10234
			public const UICamera.Mouse.Button.Flags Mouse2 = UICamera.Mouse.Button.Flags.Middle;

			// Token: 0x040027FB RID: 10235
			public const UICamera.Mouse.Button.Flags None = (UICamera.Mouse.Button.Flags)0;

			// Token: 0x040027FC RID: 10236
			public const UICamera.Mouse.Button.Flags All = UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle;

			// Token: 0x040027FD RID: 10237
			public const int Count = 3;

			// Token: 0x040027FE RID: 10238
			private static UICamera.Mouse.Button.Flags pressed;

			// Token: 0x040027FF RID: 10239
			private static UICamera.Mouse.Button.Flags released;

			// Token: 0x04002800 RID: 10240
			private static UICamera.Mouse.Button.Flags held;

			// Token: 0x04002801 RID: 10241
			private static int indexPressed = -1;

			// Token: 0x04002802 RID: 10242
			private static int indexReleased = -1;

			// Token: 0x020007CA RID: 1994
			public struct ButtonPressEventHandler : IDisposable
			{
				// Token: 0x060047C6 RID: 18374 RVA: 0x00121D54 File Offset: 0x0011FF54
				public ButtonPressEventHandler(Event @event)
				{
					this.@event = @event;
					UICamera.Mouse.Button.pressed = UICamera.Mouse.Button.Index(@event.button);
					UICamera.Mouse.Button.indexPressed = @event.button;
				}

				// Token: 0x060047C7 RID: 18375 RVA: 0x00121D84 File Offset: 0x0011FF84
				public void Dispose()
				{
					if (UICamera.Mouse.Button.indexPressed != -1)
					{
						if (this.@event.type == 12)
						{
							UICamera.Mouse.Button.held |= UICamera.Mouse.Button.pressed;
						}
						UICamera.Mouse.Button.indexPressed = -1;
						UICamera.Mouse.Button.pressed = (UICamera.Mouse.Button.Flags)0;
					}
				}

				// Token: 0x04002803 RID: 10243
				private Event @event;
			}

			// Token: 0x020007CB RID: 1995
			public struct ButtonReleaseEventHandler : IDisposable
			{
				// Token: 0x060047C8 RID: 18376 RVA: 0x00121DC0 File Offset: 0x0011FFC0
				public ButtonReleaseEventHandler(Event @event)
				{
					this.@event = @event;
					UICamera.Mouse.Button.released = UICamera.Mouse.Button.Index(@event.button);
					UICamera.Mouse.Button.indexReleased = @event.button;
				}

				// Token: 0x060047C9 RID: 18377 RVA: 0x00121DF0 File Offset: 0x0011FFF0
				public void Dispose()
				{
					if (UICamera.Mouse.Button.indexReleased != -1)
					{
						if (this.@event.type == 12)
						{
							UICamera.Mouse.Button.held &= ~UICamera.Mouse.Button.released;
						}
						UICamera.Mouse.Button.indexReleased = -1;
						UICamera.Mouse.Button.released = (UICamera.Mouse.Button.Flags)0;
					}
				}

				// Token: 0x04002804 RID: 10244
				private Event @event;
			}

			// Token: 0x020007CC RID: 1996
			[Flags]
			public enum Flags
			{
				// Token: 0x04002806 RID: 10246
				Left = 1,
				// Token: 0x04002807 RID: 10247
				Right = 2,
				// Token: 0x04002808 RID: 10248
				Middle = 4
			}

			// Token: 0x020007CD RID: 1997
			public struct Pair<T>
			{
				// Token: 0x060047CA RID: 18378 RVA: 0x00121E38 File Offset: 0x00120038
				public Pair(UICamera.Mouse.Button.Flags Button, T Value)
				{
					this.Button = Button;
					this.Value = Value;
				}

				// Token: 0x060047CB RID: 18379 RVA: 0x00121E48 File Offset: 0x00120048
				public Pair(UICamera.Mouse.Button.Flags Button, ref T Value)
				{
					this.Button = Button;
					this.Value = Value;
				}

				// Token: 0x060047CC RID: 18380 RVA: 0x00121E60 File Offset: 0x00120060
				public Pair(UICamera.Mouse.Button.Flags Button)
				{
					this = new UICamera.Mouse.Button.Pair<T>(Button, default(T));
				}

				// Token: 0x04002809 RID: 10249
				public readonly UICamera.Mouse.Button.Flags Button;

				// Token: 0x0400280A RID: 10250
				public readonly T Value;
			}

			// Token: 0x020007CE RID: 1998
			public struct ValCollection<T> : IEnumerable, IEnumerable<UICamera.Mouse.Button.Pair<T>>
			{
				// Token: 0x060047CD RID: 18381 RVA: 0x00121E80 File Offset: 0x00120080
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x17000DE3 RID: 3555
				public T this[UICamera.Mouse.Button.Flags button]
				{
					get
					{
						switch (button)
						{
						case UICamera.Mouse.Button.Flags.Left:
							return this.LeftValue;
						case UICamera.Mouse.Button.Flags.Right:
							return this.RightValue;
						case UICamera.Mouse.Button.Flags.Middle:
							return this.MiddleValue;
						}
						throw new ArgumentOutOfRangeException("button", "button should not be None or a Combination of multiple buttons");
					}
					set
					{
						switch (button)
						{
						case UICamera.Mouse.Button.Flags.Left:
							this.LeftValue = value;
							return;
						case UICamera.Mouse.Button.Flags.Right:
							this.RightValue = value;
							return;
						case UICamera.Mouse.Button.Flags.Middle:
							this.MiddleValue = value;
							return;
						}
						throw new ArgumentOutOfRangeException("button", "button should not be None or a Combination of multiple buttons");
					}
				}

				// Token: 0x17000DE4 RID: 3556
				public T this[int i]
				{
					get
					{
						return this[UICamera.Mouse.Button.Flags.Left];
					}
					set
					{
						this[UICamera.Mouse.Button.Flags.Left] = value;
					}
				}

				// Token: 0x17000DE5 RID: 3557
				public IEnumerable<UICamera.Mouse.Button.Pair<T>> this[UICamera.Mouse.Button.PressState state]
				{
					get
					{
						foreach (UICamera.Mouse.Button.Flags flag in state)
						{
							yield return new UICamera.Mouse.Button.Pair<T>(flag, this[flag]);
						}
						yield break;
					}
				}

				// Token: 0x060047D3 RID: 18387 RVA: 0x00121F88 File Offset: 0x00120188
				public IEnumerator<UICamera.Mouse.Button.Pair<T>> GetEnumerator()
				{
					yield return new UICamera.Mouse.Button.Pair<T>(UICamera.Mouse.Button.Flags.Left, this.LeftValue);
					yield return new UICamera.Mouse.Button.Pair<T>(UICamera.Mouse.Button.Flags.Right, this.RightValue);
					yield return new UICamera.Mouse.Button.Pair<T>(UICamera.Mouse.Button.Flags.Middle, this.MiddleValue);
					yield break;
				}

				// Token: 0x0400280B RID: 10251
				public T LeftValue;

				// Token: 0x0400280C RID: 10252
				public T RightValue;

				// Token: 0x0400280D RID: 10253
				public T MiddleValue;
			}

			// Token: 0x020007CF RID: 1999
			public struct RefCollection<T> : IEnumerable, IEnumerable<UICamera.Mouse.Button.Pair<T>>
			{
				// Token: 0x060047D4 RID: 18388 RVA: 0x00121FA8 File Offset: 0x001201A8
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x17000DE6 RID: 3558
				public T this[UICamera.Mouse.Button.Flags button]
				{
					get
					{
						switch (button)
						{
						case UICamera.Mouse.Button.Flags.Left:
							return this.LeftValue;
						case UICamera.Mouse.Button.Flags.Right:
							return this.RightValue;
						case UICamera.Mouse.Button.Flags.Middle:
							return this.MiddleValue;
						}
						throw new ArgumentOutOfRangeException("button", "button should not be None or a Combination of multiple buttons");
					}
					set
					{
						switch (button)
						{
						case UICamera.Mouse.Button.Flags.Left:
							this.LeftValue = value;
							return;
						case UICamera.Mouse.Button.Flags.Right:
							this.RightValue = value;
							return;
						case UICamera.Mouse.Button.Flags.Middle:
							this.MiddleValue = value;
							return;
						}
						throw new ArgumentOutOfRangeException("button", "button should not be None or a Combination of multiple buttons");
					}
				}

				// Token: 0x17000DE7 RID: 3559
				public T this[int i]
				{
					get
					{
						return this[UICamera.Mouse.Button.Flags.Left];
					}
					set
					{
						this[UICamera.Mouse.Button.Flags.Left] = value;
					}
				}

				// Token: 0x17000DE8 RID: 3560
				public IEnumerable<UICamera.Mouse.Button.Pair<T>> this[UICamera.Mouse.Button.PressState state]
				{
					get
					{
						foreach (UICamera.Mouse.Button.Flags flag in state)
						{
							yield return new UICamera.Mouse.Button.Pair<T>(flag, this[flag]);
						}
						yield break;
					}
				}

				// Token: 0x060047DA RID: 18394 RVA: 0x001220B0 File Offset: 0x001202B0
				public IEnumerator<UICamera.Mouse.Button.Pair<T>> GetEnumerator()
				{
					yield return new UICamera.Mouse.Button.Pair<T>(UICamera.Mouse.Button.Flags.Left, this.LeftValue);
					yield return new UICamera.Mouse.Button.Pair<T>(UICamera.Mouse.Button.Flags.Right, this.RightValue);
					yield return new UICamera.Mouse.Button.Pair<T>(UICamera.Mouse.Button.Flags.Middle, this.MiddleValue);
					yield break;
				}

				// Token: 0x0400280E RID: 10254
				public T LeftValue;

				// Token: 0x0400280F RID: 10255
				public T RightValue;

				// Token: 0x04002810 RID: 10256
				public T MiddleValue;
			}

			// Token: 0x020007D0 RID: 2000
			public struct PressState : IEnumerable, IEnumerable<UICamera.Mouse.Button.Flags>
			{
				// Token: 0x060047DB RID: 18395 RVA: 0x001220D0 File Offset: 0x001202D0
				IEnumerator<UICamera.Mouse.Button.Flags> IEnumerable<UICamera.Mouse.Button.Flags>.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060047DC RID: 18396 RVA: 0x001220D8 File Offset: 0x001202D8
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x17000DE9 RID: 3561
				// (get) Token: 0x060047DD RID: 18397 RVA: 0x001220E0 File Offset: 0x001202E0
				// (set) Token: 0x060047DE RID: 18398 RVA: 0x001220F0 File Offset: 0x001202F0
				public bool LeftPressed
				{
					get
					{
						return (this.Pressed & UICamera.Mouse.Button.Flags.Left) == UICamera.Mouse.Button.Flags.Left;
					}
					set
					{
						if (value)
						{
							this.Pressed |= UICamera.Mouse.Button.Flags.Left;
						}
						else
						{
							this.Pressed &= ~UICamera.Mouse.Button.Flags.Left;
						}
					}
				}

				// Token: 0x17000DEA RID: 3562
				// (get) Token: 0x060047DF RID: 18399 RVA: 0x00122128 File Offset: 0x00120328
				// (set) Token: 0x060047E0 RID: 18400 RVA: 0x00122138 File Offset: 0x00120338
				public bool RightPressed
				{
					get
					{
						return (this.Pressed & UICamera.Mouse.Button.Flags.Right) == UICamera.Mouse.Button.Flags.Right;
					}
					set
					{
						if (value)
						{
							this.Pressed |= UICamera.Mouse.Button.Flags.Right;
						}
						else
						{
							this.Pressed &= ~UICamera.Mouse.Button.Flags.Right;
						}
					}
				}

				// Token: 0x17000DEB RID: 3563
				// (get) Token: 0x060047E1 RID: 18401 RVA: 0x00122170 File Offset: 0x00120370
				// (set) Token: 0x060047E2 RID: 18402 RVA: 0x00122180 File Offset: 0x00120380
				public bool MiddlePressed
				{
					get
					{
						return (this.Pressed & UICamera.Mouse.Button.Flags.Middle) == UICamera.Mouse.Button.Flags.Middle;
					}
					set
					{
						if (value)
						{
							this.Pressed |= UICamera.Mouse.Button.Flags.Middle;
						}
						else
						{
							this.Pressed &= ~UICamera.Mouse.Button.Flags.Middle;
						}
					}
				}

				// Token: 0x17000DEC RID: 3564
				// (get) Token: 0x060047E3 RID: 18403 RVA: 0x001221B8 File Offset: 0x001203B8
				// (set) Token: 0x060047E4 RID: 18404 RVA: 0x001221C4 File Offset: 0x001203C4
				public UICamera.Mouse.Button.Flags Released
				{
					get
					{
						return ~this.Pressed & (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle);
					}
					set
					{
						this.Pressed = (~value & (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle));
					}
				}

				// Token: 0x17000DED RID: 3565
				// (get) Token: 0x060047E5 RID: 18405 RVA: 0x001221D0 File Offset: 0x001203D0
				public bool AnyPressed
				{
					get
					{
						return (this.Pressed & (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle)) != (UICamera.Mouse.Button.Flags)0;
					}
				}

				// Token: 0x17000DEE RID: 3566
				// (get) Token: 0x060047E6 RID: 18406 RVA: 0x001221E0 File Offset: 0x001203E0
				public bool AllPressed
				{
					get
					{
						return (this.Pressed & (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle)) == (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle);
					}
				}

				// Token: 0x17000DEF RID: 3567
				// (get) Token: 0x060047E7 RID: 18407 RVA: 0x001221F0 File Offset: 0x001203F0
				public bool NonePressed
				{
					get
					{
						return (this.Pressed & (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle)) == (UICamera.Mouse.Button.Flags)0;
					}
				}

				// Token: 0x17000DF0 RID: 3568
				// (get) Token: 0x060047E8 RID: 18408 RVA: 0x00122200 File Offset: 0x00120400
				public int PressedCount
				{
					get
					{
						int num = 0;
						uint num2 = (uint)(this.Pressed & (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle));
						while (num2 != 0u)
						{
							num2 &= num2 - 1u;
							num++;
						}
						return num;
					}
				}

				// Token: 0x17000DF1 RID: 3569
				// (get) Token: 0x060047E9 RID: 18409 RVA: 0x00122230 File Offset: 0x00120430
				// (set) Token: 0x060047EA RID: 18410 RVA: 0x0012223C File Offset: 0x0012043C
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

				// Token: 0x17000DF2 RID: 3570
				// (get) Token: 0x060047EB RID: 18411 RVA: 0x00122248 File Offset: 0x00120448
				// (set) Token: 0x060047EC RID: 18412 RVA: 0x00122254 File Offset: 0x00120454
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

				// Token: 0x17000DF3 RID: 3571
				// (get) Token: 0x060047ED RID: 18413 RVA: 0x00122260 File Offset: 0x00120460
				// (set) Token: 0x060047EE RID: 18414 RVA: 0x0012226C File Offset: 0x0012046C
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

				// Token: 0x17000DF4 RID: 3572
				// (get) Token: 0x060047EF RID: 18415 RVA: 0x00122278 File Offset: 0x00120478
				public bool AnyReleased
				{
					get
					{
						return !this.AllPressed;
					}
				}

				// Token: 0x17000DF5 RID: 3573
				// (get) Token: 0x060047F0 RID: 18416 RVA: 0x00122284 File Offset: 0x00120484
				public bool AllReleased
				{
					get
					{
						return !this.AnyPressed;
					}
				}

				// Token: 0x17000DF6 RID: 3574
				// (get) Token: 0x060047F1 RID: 18417 RVA: 0x00122290 File Offset: 0x00120490
				public bool NoneReleased
				{
					get
					{
						return !this.AllPressed;
					}
				}

				// Token: 0x060047F2 RID: 18418 RVA: 0x0012229C File Offset: 0x0012049C
				public void Clear()
				{
					this.Pressed &= ~(UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle);
				}

				// Token: 0x17000DF7 RID: 3575
				public bool this[int index]
				{
					get
					{
						UICamera.Mouse.Button.Flags flags = UICamera.Mouse.Button.Index(index);
						return (this.Pressed & flags) == flags;
					}
					set
					{
						UICamera.Mouse.Button.Flags flags = UICamera.Mouse.Button.Index(index);
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

				// Token: 0x060047F5 RID: 18421 RVA: 0x0012230C File Offset: 0x0012050C
				public UICamera.Mouse.Button.PressState.Enumerator GetEnumerator()
				{
					return UICamera.Mouse.Button.PressState.Enumerator.Enumerate(this.Pressed);
				}

				// Token: 0x060047F6 RID: 18422 RVA: 0x0012231C File Offset: 0x0012051C
				public static implicit operator UICamera.Mouse.Button.Flags(UICamera.Mouse.Button.PressState state)
				{
					return state.Pressed & (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle);
				}

				// Token: 0x060047F7 RID: 18423 RVA: 0x00122328 File Offset: 0x00120528
				public static implicit operator UICamera.Mouse.Button.PressState(UICamera.Mouse.Button.Flags buttons)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (buttons & (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle));
					return result;
				}

				// Token: 0x060047F8 RID: 18424 RVA: 0x00122340 File Offset: 0x00120540
				public static bool operator true(UICamera.Mouse.Button.PressState state)
				{
					return state.AnyPressed;
				}

				// Token: 0x060047F9 RID: 18425 RVA: 0x0012234C File Offset: 0x0012054C
				public static bool operator false(UICamera.Mouse.Button.PressState state)
				{
					return state.NonePressed;
				}

				// Token: 0x060047FA RID: 18426 RVA: 0x00122358 File Offset: 0x00120558
				public static UICamera.Mouse.Button.PressState operator -(UICamera.Mouse.Button.PressState s)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = s.Released;
					return result;
				}

				// Token: 0x060047FB RID: 18427 RVA: 0x00122374 File Offset: 0x00120574
				public static UICamera.Mouse.Button.PressState operator +(UICamera.Mouse.Button.PressState l, UICamera.Mouse.Button.PressState r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed | r.Pressed);
					return result;
				}

				// Token: 0x060047FC RID: 18428 RVA: 0x00122398 File Offset: 0x00120598
				public static UICamera.Mouse.Button.PressState operator +(UICamera.Mouse.Button.PressState l, UICamera.Mouse.Button.Flags r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed | r);
					return result;
				}

				// Token: 0x060047FD RID: 18429 RVA: 0x001223B8 File Offset: 0x001205B8
				public static UICamera.Mouse.Button.PressState operator +(UICamera.Mouse.Button.Flags l, UICamera.Mouse.Button.PressState r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l | r.Pressed);
					return result;
				}

				// Token: 0x060047FE RID: 18430 RVA: 0x001223D8 File Offset: 0x001205D8
				public static UICamera.Mouse.Button.PressState operator -(UICamera.Mouse.Button.PressState l, UICamera.Mouse.Button.PressState r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed & ~r.Pressed);
					return result;
				}

				// Token: 0x060047FF RID: 18431 RVA: 0x00122400 File Offset: 0x00120600
				public static UICamera.Mouse.Button.PressState operator -(UICamera.Mouse.Button.PressState l, UICamera.Mouse.Button.Flags r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed & ~r);
					return result;
				}

				// Token: 0x06004800 RID: 18432 RVA: 0x00122420 File Offset: 0x00120620
				public static UICamera.Mouse.Button.PressState operator -(UICamera.Mouse.Button.Flags l, UICamera.Mouse.Button.PressState r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l & ~r.Pressed);
					return result;
				}

				// Token: 0x06004801 RID: 18433 RVA: 0x00122440 File Offset: 0x00120640
				public static UICamera.Mouse.Button.PressState operator *(UICamera.Mouse.Button.PressState l, UICamera.Mouse.Button.PressState r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed & r.Pressed);
					return result;
				}

				// Token: 0x06004802 RID: 18434 RVA: 0x00122464 File Offset: 0x00120664
				public static UICamera.Mouse.Button.PressState operator *(UICamera.Mouse.Button.PressState l, UICamera.Mouse.Button.Flags r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed & r);
					return result;
				}

				// Token: 0x06004803 RID: 18435 RVA: 0x00122484 File Offset: 0x00120684
				public static UICamera.Mouse.Button.PressState operator *(UICamera.Mouse.Button.Flags l, UICamera.Mouse.Button.PressState r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l & r.Pressed);
					return result;
				}

				// Token: 0x06004804 RID: 18436 RVA: 0x001224A4 File Offset: 0x001206A4
				public static UICamera.Mouse.Button.PressState operator /(UICamera.Mouse.Button.PressState l, UICamera.Mouse.Button.PressState r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed ^ r.Pressed);
					return result;
				}

				// Token: 0x06004805 RID: 18437 RVA: 0x001224C8 File Offset: 0x001206C8
				public static UICamera.Mouse.Button.PressState operator /(UICamera.Mouse.Button.PressState l, UICamera.Mouse.Button.Flags r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l.Pressed ^ r);
					return result;
				}

				// Token: 0x06004806 RID: 18438 RVA: 0x001224E8 File Offset: 0x001206E8
				public static UICamera.Mouse.Button.PressState operator /(UICamera.Mouse.Button.Flags l, UICamera.Mouse.Button.PressState r)
				{
					UICamera.Mouse.Button.PressState result;
					result.Pressed = (l ^ r.Pressed);
					return result;
				}

				// Token: 0x04002811 RID: 10257
				public UICamera.Mouse.Button.Flags Pressed;

				// Token: 0x020007D1 RID: 2001
				public class Enumerator : IDisposable, IEnumerator, IEnumerator<UICamera.Mouse.Button.Flags>
				{
					// Token: 0x06004807 RID: 18439 RVA: 0x00122508 File Offset: 0x00120708
					private Enumerator()
					{
					}

					// Token: 0x06004808 RID: 18440 RVA: 0x00122510 File Offset: 0x00120710
					static Enumerator()
					{
						for (UICamera.Mouse.Button.Flags flags = (UICamera.Mouse.Button.Flags)0; flags <= (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle); flags++)
						{
							int num = 0;
							uint num2 = (uint)flags;
							while (num2 != 0u)
							{
								num2 &= num2 - 1u;
								num++;
							}
							UICamera.Mouse.Button.Flags[] array = new UICamera.Mouse.Button.Flags[num];
							int num3 = 0;
							int num4 = 0;
							while (num4 < 3 && num3 < num)
							{
								if ((flags & (UICamera.Mouse.Button.Flags)(1 << num4)) == (UICamera.Mouse.Button.Flags)(1 << num4))
								{
									array[num3++] = (UICamera.Mouse.Button.Flags)(1 << num4);
								}
								num4++;
							}
							UICamera.Mouse.Button.PressState.Enumerator.combos[(int)flags] = array;
						}
					}

					// Token: 0x17000DF8 RID: 3576
					// (get) Token: 0x06004809 RID: 18441 RVA: 0x001225A8 File Offset: 0x001207A8
					object IEnumerator.Current
					{
						get
						{
							return this.flags[this.pos];
						}
					}

					// Token: 0x0600480A RID: 18442 RVA: 0x001225BC File Offset: 0x001207BC
					public static UICamera.Mouse.Button.PressState.Enumerator Enumerate(UICamera.Mouse.Button.Flags flags)
					{
						UICamera.Mouse.Button.PressState.Enumerator enumerator;
						if (UICamera.Mouse.Button.PressState.Enumerator.dumpCount == 0u)
						{
							enumerator = new UICamera.Mouse.Button.PressState.Enumerator();
						}
						else
						{
							enumerator = UICamera.Mouse.Button.PressState.Enumerator.dump;
							UICamera.Mouse.Button.PressState.Enumerator.dump = enumerator.nextDump;
							UICamera.Mouse.Button.PressState.Enumerator.dumpCount -= 1u;
							enumerator.nextDump = null;
						}
						enumerator.pos = -1;
						enumerator.value = flags;
						enumerator.inDump = false;
						enumerator.flags = UICamera.Mouse.Button.PressState.Enumerator.combos[(int)flags];
						return enumerator;
					}

					// Token: 0x17000DF9 RID: 3577
					// (get) Token: 0x0600480B RID: 18443 RVA: 0x00122628 File Offset: 0x00120828
					public UICamera.Mouse.Button.Flags Current
					{
						get
						{
							return this.flags[this.pos];
						}
					}

					// Token: 0x0600480C RID: 18444 RVA: 0x00122638 File Offset: 0x00120838
					public bool MoveNext()
					{
						return ++this.pos < this.flags.Length;
					}

					// Token: 0x0600480D RID: 18445 RVA: 0x00122660 File Offset: 0x00120860
					public void Reset()
					{
						this.pos = -1;
					}

					// Token: 0x0600480E RID: 18446 RVA: 0x0012266C File Offset: 0x0012086C
					public void Dispose()
					{
						if (!this.inDump)
						{
							this.nextDump = UICamera.Mouse.Button.PressState.Enumerator.dump;
							this.inDump = true;
							UICamera.Mouse.Button.PressState.Enumerator.dump = this;
							UICamera.Mouse.Button.PressState.Enumerator.dumpCount += 1u;
						}
					}

					// Token: 0x04002812 RID: 10258
					private static readonly UICamera.Mouse.Button.Flags[][] combos = new UICamera.Mouse.Button.Flags[8][];

					// Token: 0x04002813 RID: 10259
					private UICamera.Mouse.Button.Flags[] flags;

					// Token: 0x04002814 RID: 10260
					private UICamera.Mouse.Button.Flags value;

					// Token: 0x04002815 RID: 10261
					private int pos;

					// Token: 0x04002816 RID: 10262
					private UICamera.Mouse.Button.PressState.Enumerator nextDump;

					// Token: 0x04002817 RID: 10263
					private bool inDump;

					// Token: 0x04002818 RID: 10264
					private static UICamera.Mouse.Button.PressState.Enumerator dump;

					// Token: 0x04002819 RID: 10265
					private static uint dumpCount;
				}
			}

			// Token: 0x020007D2 RID: 2002
			public sealed class Sampler
			{
				// Token: 0x0600480F RID: 18447 RVA: 0x001226A0 File Offset: 0x001208A0
				public Sampler(UICamera.Mouse.Button.Flags Button, UICamera.CursorSampler Cursor)
				{
					this.Button = Button;
					this.Cursor = Cursor;
				}

				// Token: 0x0400281A RID: 10266
				public readonly UICamera.Mouse.Button.Flags Button;

				// Token: 0x0400281B RID: 10267
				public readonly UICamera.CursorSampler Cursor;

				// Token: 0x0400281C RID: 10268
				public GameObject Pressed;

				// Token: 0x0400281D RID: 10269
				public UIHotSpot.Hit Hit;

				// Token: 0x0400281E RID: 10270
				public Vector2 Point;

				// Token: 0x0400281F RID: 10271
				public Vector2 TotalDelta;

				// Token: 0x04002820 RID: 10272
				public ulong ClickCount;

				// Token: 0x04002821 RID: 10273
				public ulong DragClickNumber;

				// Token: 0x04002822 RID: 10274
				public float PressTime;

				// Token: 0x04002823 RID: 10275
				public float ReleaseTime;

				// Token: 0x04002824 RID: 10276
				public UICamera.ClickNotification ClickNotification;

				// Token: 0x04002825 RID: 10277
				public bool PressedNow;

				// Token: 0x04002826 RID: 10278
				public bool Held;

				// Token: 0x04002827 RID: 10279
				public bool ReleasedNow;

				// Token: 0x04002828 RID: 10280
				public bool DidHit;

				// Token: 0x04002829 RID: 10281
				public bool Once;

				// Token: 0x0400282A RID: 10282
				public bool DragClick;
			}
		}

		// Token: 0x020007D3 RID: 2003
		public struct State
		{
			// Token: 0x0400282B RID: 10283
			public Vector2 Point;

			// Token: 0x0400282C RID: 10284
			public Vector2 Delta;

			// Token: 0x0400282D RID: 10285
			public Vector2 Scroll;

			// Token: 0x0400282E RID: 10286
			public UICamera.Mouse.Button.PressState Buttons;
		}
	}

	// Token: 0x020007D4 RID: 2004
	public sealed class CursorSampler
	{
		// Token: 0x06004810 RID: 18448 RVA: 0x001226B8 File Offset: 0x001208B8
		public CursorSampler()
		{
			this.Buttons.LeftValue = new UICamera.Mouse.Button.Sampler(UICamera.Mouse.Button.Flags.Left, this);
			this.Buttons.RightValue = new UICamera.Mouse.Button.Sampler(UICamera.Mouse.Button.Flags.Right, this);
			this.Buttons.MiddleValue = new UICamera.Mouse.Button.Sampler(UICamera.Mouse.Button.Flags.Middle, this);
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06004811 RID: 18449 RVA: 0x0012270C File Offset: 0x0012090C
		public Vector2 Point
		{
			get
			{
				return this.Current.Mouse.Point;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x00122720 File Offset: 0x00120920
		public Vector2 FrameDelta
		{
			get
			{
				return this.Current.Mouse.Delta;
			}
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x00122734 File Offset: 0x00120934
		private static void ExitDragHover(GameObject lander, GameObject drop, DropNotificationFlags flags)
		{
			if ((flags & DropNotificationFlags.ReverseHover) == DropNotificationFlags.ReverseHover)
			{
				if ((flags & DropNotificationFlags.DragHover) == DropNotificationFlags.DragHover)
				{
					drop.NGUIMessage("OnDragHoverExit", lander);
				}
				if ((flags & DropNotificationFlags.LandHover) == DropNotificationFlags.LandHover)
				{
					lander.NGUIMessage("OnLandHoverExit", drop);
				}
			}
			else
			{
				if ((flags & DropNotificationFlags.LandHover) == DropNotificationFlags.LandHover)
				{
					lander.NGUIMessage("OnLandHoverExit", drop);
				}
				if ((flags & DropNotificationFlags.DragHover) == DropNotificationFlags.DragHover)
				{
					drop.NGUIMessage("OnDragHoverExit", lander);
				}
			}
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x001227CC File Offset: 0x001209CC
		private static void EnterDragHover(GameObject lander, GameObject drop, DropNotificationFlags flags)
		{
			if ((flags & DropNotificationFlags.ReverseHover) == DropNotificationFlags.ReverseHover)
			{
				if ((flags & DropNotificationFlags.LandHover) == DropNotificationFlags.LandHover)
				{
					lander.NGUIMessage("OnLandHoverEnter", drop);
				}
				if ((flags & DropNotificationFlags.DragHover) == DropNotificationFlags.DragHover)
				{
					drop.NGUIMessage("OnDragHoverEnter", lander);
				}
			}
			else
			{
				if ((flags & DropNotificationFlags.DragHover) == DropNotificationFlags.DragHover)
				{
					drop.NGUIMessage("OnDragHoverEnter", lander);
				}
				if ((flags & DropNotificationFlags.LandHover) == DropNotificationFlags.LandHover)
				{
					lander.NGUIMessage("OnLandHoverEnter", drop);
				}
			}
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x00122864 File Offset: 0x00120A64
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
					UICamera.CursorSampler.ExitDragHover(Pressed, this.DragHover, this.LastHoverDropNotification);
				}
				this.DragHover = Current;
				if (Current != Pressed)
				{
					this.LastHoverDropNotification = this.DropNotification;
					UICamera.CursorSampler.EnterDragHover(Pressed, this.DragHover, this.LastHoverDropNotification);
				}
			}
			else
			{
				this.ClearDragHover(Pressed);
			}
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x001228FC File Offset: 0x00120AFC
		private void ClearDragHover(GameObject Pressed)
		{
			if (this.DragHover)
			{
				if (this.DragHover != Pressed)
				{
					UICamera.CursorSampler.ExitDragHover(Pressed, this.DragHover, this.LastHoverDropNotification);
				}
				this.DragHover = null;
			}
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x00122944 File Offset: 0x00120B44
		internal void MouseEvent(Event @event, EventType type)
		{
			UICamera.CursorSampler.Sample current;
			current.Mouse.Scroll = default(Vector2);
			current.Mouse.Buttons.Pressed = (UICamera.Mouse.Button.Held | UICamera.Mouse.Button.NewlyPressed);
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
			current.Hit = UIHotSpot.Hit.invalid;
			if (current.DidHit = UICamera.Raycast(current.Mouse.Point, ref current.Hit, out current.UICamera))
			{
				UICamera.lastHit = current.Hit;
				current.Under = current.Hit.gameObject;
				current.HasUnder = true;
			}
			else if (UICamera.fallThrough)
			{
				current.Under = UICamera.fallThrough;
				current.HasUnder = true;
				current.UICamera = UICamera.FindCameraForLayer(UICamera.fallThrough.layer);
				if (!current.UICamera)
				{
					current.UICamera = ((current.IsFirst || !this.Current.UICamera) ? UICamera.mList[(int)UICamera.mListSort[0]] : this.Current.UICamera);
				}
			}
			else
			{
				current.Under = null;
				current.HasUnder = false;
				current.UICamera = ((current.IsFirst || !this.Current.UICamera) ? UICamera.mList[(int)UICamera.mListSort[0]] : this.Current.UICamera);
			}
			current.UnderChange = (current.IsFirst || ((!current.HasUnder) ? this.Current.HasUnder : (!this.Current.HasUnder || this.Current.Under != current.Under)));
			current.HoverChange = (current.UnderChange && current.Under != UICamera.mHover);
			current.ButtonChange = UICamera.Mouse.Button.AnyNewlyPressedOrReleased;
			bool flag = false;
			if (current.ButtonChange && UICamera.Mouse.Button.AnyNewlyPressedThatCancelTooltips)
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
				if (current.HoverChange && UICamera.mHover)
				{
					if (current.UICamera.mTooltip != null)
					{
						current.UICamera.ShowTooltip(false);
					}
					UICamera.Highlight(UICamera.mHover, false);
					UICamera.mHover = null;
				}
			}
			current.Time = Time.realtimeSinceStartup;
			current.ButtonsPressed = UICamera.Mouse.Button.NewlyPressed;
			current.ButtonsReleased = UICamera.Mouse.Button.NewlyReleased;
			if (!flag && current.ButtonsPressed != (UICamera.Mouse.Button.Flags)0 && current.UICamera.mTooltip)
			{
				current.UICamera.ShowTooltip(false);
				flag = true;
			}
			for (UICamera.Mouse.Button.Flags flags = UICamera.Mouse.Button.Flags.Left; flags < (UICamera.Mouse.Button.Flags.Left | UICamera.Mouse.Button.Flags.Right | UICamera.Mouse.Button.Flags.Middle); flags <<= 1)
			{
				UICamera.Mouse.Button.Sampler sampler = this.Buttons[flags];
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
						sampler.ClickNotification = UICamera.ClickNotification.Always;
						if (flags == UICamera.Mouse.Button.Flags.Left)
						{
							this.Dragging = false;
							this.DropNotification = DropNotificationFlags.DragDrop;
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
							if (flags == UICamera.Mouse.Button.Flags.Left)
							{
								UICamera.mPressInput = current.Under.GetComponent<UIInput>();
								if (UICamera.mSelInput)
								{
									if (UICamera.mPressInput)
									{
										if (UICamera.mSelInput == UICamera.mPressInput)
										{
											UICamera.mSelInput.OnEvent(current.UICamera, @event, type);
										}
										else
										{
											UICamera.mSelInput.LoseFocus();
											UICamera.mSelInput = null;
											UICamera.mPressInput.GainFocus();
											UICamera.mPressInput.OnEvent(current.UICamera, @event, type);
										}
									}
									else
									{
										UICamera.mSelInput.LoseFocus();
										UICamera.mSelInput = null;
									}
								}
								else if (UICamera.mPressInput)
								{
									UICamera.mPressInput.GainFocus();
									UICamera.mPressInput.OnEvent(current.UICamera, @event, type);
								}
								if (UICamera.mSel && UICamera.mSel != current.Under)
								{
									if (!flag && current.UICamera.mTooltip)
									{
										current.UICamera.ShowTooltip(false);
									}
									UICamera.SetSelectedObject(null);
								}
								this.Panel = UIPanel.FindRoot(current.Under.transform);
								if (this.Panel)
								{
									if (this.Panel != UICamera.popupPanel && UICamera.popupPanel)
									{
										UICamera.PopupPanel(null);
									}
									current.Under.Press(true);
									this.Panel.gameObject.NGUIMessage("OnChildPress", true);
								}
								else
								{
									if (UICamera.popupPanel)
									{
										UICamera.PopupPanel(null);
									}
									current.Under.Press(true);
								}
								this.PressDropNotification = this.DropNotification;
							}
							else
							{
								if (UICamera.mSelInput)
								{
									UICamera.mSelInput.OnEvent(current.UICamera, @event, type);
								}
								if (!sampler.DragClick)
								{
									if (flags == UICamera.Mouse.Button.Flags.Right)
									{
										UIPanel uipanel = UIPanel.FindRoot(current.Under.transform);
										if (UICamera.popupPanel && UICamera.popupPanel != uipanel)
										{
											UICamera.PopupPanel(null);
										}
										current.Under.AltPress(true);
									}
									else if (flags == UICamera.Mouse.Button.Flags.Middle)
									{
										current.Under.MidPress(true);
									}
								}
							}
							@event.Use();
						}
						else if (flags == UICamera.Mouse.Button.Flags.Left)
						{
							if (UICamera.popupPanel)
							{
								UICamera.PopupPanel(null);
							}
							UICamera.mPressInput = null;
							if (UICamera.mSelInput)
							{
								UICamera.mSelInput.LoseFocus();
								UICamera.mSelInput = null;
							}
							if (UICamera.mSel)
							{
								if (!flag && current.UICamera.mTooltip)
								{
									current.UICamera.ShowTooltip(false);
								}
								UICamera.SetSelectedObject(null);
							}
						}
					}
					else if (sampler.Held && sampler.DidHit)
					{
						if (type == 3 && flags == UICamera.Mouse.Button.Flags.Left)
						{
							if (UICamera.mPressInput)
							{
								UICamera.mPressInput.OnEvent(current.UICamera, @event, type);
							}
							@event.Use();
						}
						if (current.DidMove)
						{
							if (!flag && current.UICamera.mTooltip)
							{
								current.UICamera.ShowTooltip(false);
							}
							UICamera.Mouse.Button.Sampler sampler2 = sampler;
							sampler2.TotalDelta.x = sampler2.TotalDelta.x + current.Mouse.Delta.x;
							UICamera.Mouse.Button.Sampler sampler3 = sampler;
							sampler3.TotalDelta.y = sampler3.TotalDelta.y + current.Mouse.Delta.y;
							bool flag2 = sampler.ClickNotification == UICamera.ClickNotification.None;
							if (flags == UICamera.Mouse.Button.Flags.Left && !sampler.DragClick && (this.PressDropNotification & (DropNotificationFlags.DragDrop | DropNotificationFlags.DragLand | DropNotificationFlags.AltDrop | DropNotificationFlags.AltLand | DropNotificationFlags.MidDrop | DropNotificationFlags.MidLand)) != (DropNotificationFlags)0)
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
								sampler.ClickNotification = UICamera.ClickNotification.None;
							}
							else if (sampler.ClickNotification == UICamera.ClickNotification.BasedOnDelta)
							{
								float num2;
								if (flags == UICamera.Mouse.Button.Flags.Left)
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
									sampler.ClickNotification = UICamera.ClickNotification.None;
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
			for (UICamera.Mouse.Button.Flags flags2 = UICamera.Mouse.Button.Flags.Middle; flags2 != (UICamera.Mouse.Button.Flags)0; flags2 >>= 1)
			{
				UICamera.Mouse.Button.Sampler sampler4 = this.Buttons[flags2];
				try
				{
					this.CurrentButton = sampler4;
					if ((current.ButtonsReleased & flags2) == flags2)
					{
						sampler4.ReleasedNow = true;
						if (sampler4.DidHit)
						{
							if (flags2 == UICamera.Mouse.Button.Flags.Left)
							{
								if ((type == 1 || type == 5) && UICamera.mPressInput && sampler4.Pressed == UICamera.mPressInput.gameObject)
								{
									UICamera.mPressInput.OnEvent(current.UICamera, @event, type);
									UICamera.mSelInput = UICamera.mPressInput;
								}
								UICamera.mPressInput = null;
								if (current.HasUnder)
								{
									if (sampler4.Pressed == current.Under)
									{
										if (this.Dragging && (this.PressDropNotification & (DropNotificationFlags.DragDrop | DropNotificationFlags.DragLand | DropNotificationFlags.AltDrop | DropNotificationFlags.AltLand | DropNotificationFlags.MidDrop | DropNotificationFlags.MidLand)) != (DropNotificationFlags)0)
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
										if (sampler4.Pressed == UICamera.mHover)
										{
											sampler4.Pressed.Hover(true);
										}
										if (sampler4.Pressed != UICamera.mSel)
										{
											UICamera.mSel = sampler4.Pressed;
											sampler4.Pressed.Select(true);
										}
										else
										{
											UICamera.mSel = sampler4.Pressed;
										}
										if (!sampler4.DragClick && sampler4.ClickNotification != UICamera.ClickNotification.None)
										{
											if (this.Panel)
											{
												this.Panel.gameObject.NGUIMessage("OnChildClick", sampler4.Pressed);
											}
											if (sampler4.ClickNotification != UICamera.ClickNotification.None)
											{
												sampler4.Pressed.Click();
												if (sampler4.ClickNotification != UICamera.ClickNotification.None && sampler4.ReleaseTime + 0.25f > current.Time)
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
										if (this.Dragging && !sampler4.DragClick && (this.PressDropNotification & (DropNotificationFlags.DragDrop | DropNotificationFlags.DragLand | DropNotificationFlags.AltDrop | DropNotificationFlags.AltLand | DropNotificationFlags.MidDrop | DropNotificationFlags.MidLand)) != (DropNotificationFlags)0)
										{
											global::DropNotification.DropMessage(ref this.DropNotification, DragEventKind.Drag, sampler4.Pressed, current.Under);
											this.ClearDragHover(sampler4.Pressed);
											sampler4.Pressed.DragState(false);
										}
										if (this.Panel)
										{
											this.Panel.gameObject.NGUIMessage("OnChildPress", false);
										}
										sampler4.Pressed.Press(false);
										if (sampler4.Pressed == UICamera.mHover)
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
										global::DropNotification.DropMessage(ref this.DropNotification, DragEventKind.Drag, sampler4.Pressed, current.Under);
										sampler4.Pressed.DragState(false);
									}
									if (this.Panel)
									{
										this.Panel.gameObject.NGUIMessage("OnChildPress", false);
									}
									sampler4.Pressed.Press(false);
									if (sampler4.Pressed == UICamera.mHover)
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
									if (flags2 == UICamera.Mouse.Button.Flags.Right)
									{
										flag3 = global::DropNotification.DropMessage(ref this.DropNotification, DragEventKind.Alt, this.Buttons.LeftValue.Pressed, sampler4.Pressed);
									}
									else
									{
										flag3 = (flags2 == UICamera.Mouse.Button.Flags.Middle && global::DropNotification.DropMessage(ref this.DropNotification, DragEventKind.Mid, this.Buttons.LeftValue.Pressed, sampler4.Pressed));
									}
									if (flag3)
									{
										this.Buttons.LeftValue.DragClick = true;
										this.ClearDragHover(this.Buttons.LeftValue.Pressed);
										sampler4.Pressed.DragState(false);
									}
								}
							}
							else if (flags2 == UICamera.Mouse.Button.Flags.Right)
							{
								sampler4.Pressed.AltPress(false);
								if (current.HasUnder && sampler4.Pressed == current.Under && sampler4.ClickNotification != UICamera.ClickNotification.None)
								{
									sampler4.Pressed.AltClick();
									if (sampler4.ClickNotification != UICamera.ClickNotification.None && sampler4.ReleaseTime + 0.25f > current.Time)
									{
										sampler4.Pressed.AltDoubleClick();
									}
								}
							}
							else if (flags2 == UICamera.Mouse.Button.Flags.Middle)
							{
								sampler4.Pressed.MidPress(false);
								if (current.HasUnder && sampler4.Pressed == current.Under && sampler4.ClickNotification != UICamera.ClickNotification.None)
								{
									sampler4.Pressed.MidClick();
									if (sampler4.ClickNotification != UICamera.ClickNotification.None && sampler4.ReleaseTime + 0.25f > current.Time)
									{
										sampler4.Pressed.MidDoubleClick();
									}
								}
							}
						}
						sampler4.ReleasedNow = true;
						sampler4.ClickNotification = UICamera.ClickNotification.None;
						sampler4.ReleaseTime = current.Time;
						sampler4.Held = false;
						sampler4.ClickCount += 1UL;
						sampler4.DragClick = false;
						sampler4.DragClickNumber = 0UL;
						if (flags2 == UICamera.Mouse.Button.Flags.Left)
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
			UICamera.lastMousePosition = ((!current.IsFirst) ? this.Current.Mouse.Point : current.Mouse.Point);
			if (current.HasUnder && (current.Mouse.Buttons.NonePressed || (this.Dragging && (this.DropNotification & DropNotificationFlags.RegularHover) == DropNotificationFlags.RegularHover)) && UICamera.mHover != current.Under)
			{
				current.UICamera.mTooltipTime = current.Time + current.UICamera.tooltipDelay;
				UICamera.mHover = current.Under;
				UICamera.Highlight(UICamera.mHover, true);
			}
			current.Valid = true;
			this.Last = this.Current;
			this.Current = current;
		}

		// Token: 0x0400282F RID: 10287
		private const float kDoubleClickLimit = 0.25f;

		// Token: 0x04002830 RID: 10288
		public UICamera.Mouse.Button.ValCollection<UICamera.Mouse.Button.Sampler> Buttons;

		// Token: 0x04002831 RID: 10289
		public DropNotificationFlags DropNotification;

		// Token: 0x04002832 RID: 10290
		public bool Dragging;

		// Token: 0x04002833 RID: 10291
		public UICamera.CursorSampler.Sample Current;

		// Token: 0x04002834 RID: 10292
		public UICamera.CursorSampler.Sample Last;

		// Token: 0x04002835 RID: 10293
		public float LastClickTime = float.MaxValue;

		// Token: 0x04002836 RID: 10294
		public bool IsFirst;

		// Token: 0x04002837 RID: 10295
		public bool IsLast;

		// Token: 0x04002838 RID: 10296
		public bool IsCurrent;

		// Token: 0x04002839 RID: 10297
		public UICamera.Mouse.Button.Sampler CurrentButton;

		// Token: 0x0400283A RID: 10298
		private DropNotificationFlags LastHoverDropNotification;

		// Token: 0x0400283B RID: 10299
		private DropNotificationFlags PressDropNotification;

		// Token: 0x0400283C RID: 10300
		private GameObject DragHover;

		// Token: 0x0400283D RID: 10301
		private UIPanel Panel;

		// Token: 0x020007D5 RID: 2005
		public struct Sample
		{
			// Token: 0x17000DFC RID: 3580
			// (get) Token: 0x06004818 RID: 18456 RVA: 0x00123CA4 File Offset: 0x00121EA4
			public Camera Camera
			{
				get
				{
					return (!this.UICamera) ? null : this.UICamera.cachedCamera;
				}
			}

			// Token: 0x06004819 RID: 18457 RVA: 0x00123CC8 File Offset: 0x00121EC8
			public static bool operator true(UICamera.CursorSampler.Sample sample)
			{
				return sample.Valid;
			}

			// Token: 0x0600481A RID: 18458 RVA: 0x00123CD4 File Offset: 0x00121ED4
			public static bool operator false(UICamera.CursorSampler.Sample sample)
			{
				return !sample.Valid;
			}

			// Token: 0x0400283E RID: 10302
			public GameObject Under;

			// Token: 0x0400283F RID: 10303
			public UICamera UICamera;

			// Token: 0x04002840 RID: 10304
			public UICamera.Mouse.State Mouse;

			// Token: 0x04002841 RID: 10305
			public UIHotSpot.Hit Hit;

			// Token: 0x04002842 RID: 10306
			public float Time;

			// Token: 0x04002843 RID: 10307
			public bool DidHit;

			// Token: 0x04002844 RID: 10308
			public bool HasUnder;

			// Token: 0x04002845 RID: 10309
			public bool Valid;

			// Token: 0x04002846 RID: 10310
			public bool DidMove;

			// Token: 0x04002847 RID: 10311
			public bool IsFirst;

			// Token: 0x04002848 RID: 10312
			public bool ButtonChange;

			// Token: 0x04002849 RID: 10313
			public bool UnderChange;

			// Token: 0x0400284A RID: 10314
			public bool HoverChange;

			// Token: 0x0400284B RID: 10315
			public UICamera.Mouse.Button.Flags ButtonsPressed;

			// Token: 0x0400284C RID: 10316
			public UICamera.Mouse.Button.Flags ButtonsReleased;
		}
	}

	// Token: 0x020007D6 RID: 2006
	private static class LateLoadCursor
	{
		// Token: 0x0400284D RID: 10317
		public static readonly UICamera.CursorSampler Sampler = new UICamera.CursorSampler();
	}

	// Token: 0x020007D7 RID: 2007
	public struct BackwardsCompatabilitySupport
	{
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x00123CEC File Offset: 0x00121EEC
		// (set) Token: 0x0600481D RID: 18461 RVA: 0x00123D04 File Offset: 0x00121F04
		public UICamera.ClickNotification clickNotification
		{
			get
			{
				return UICamera.Cursor.Buttons.LeftValue.ClickNotification;
			}
			set
			{
				UICamera.Cursor.Buttons.LeftValue.ClickNotification = value;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x00123D1C File Offset: 0x00121F1C
		public Vector2 pos
		{
			get
			{
				return (UICamera.Cursor.CurrentButton != null) ? (UICamera.Cursor.CurrentButton.Point + UICamera.Cursor.CurrentButton.TotalDelta) : UICamera.Cursor.Current.Mouse.Point;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x00123D74 File Offset: 0x00121F74
		public Vector2 delta
		{
			get
			{
				return UICamera.Cursor.FrameDelta;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x00123D80 File Offset: 0x00121F80
		public Vector2 totalDelta
		{
			get
			{
				return UICamera.Cursor.Buttons.LeftValue.TotalDelta;
			}
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x00123D98 File Offset: 0x00121F98
		public override bool Equals(object obj)
		{
			return false;
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x00123D9C File Offset: 0x00121F9C
		public override int GetHashCode()
		{
			return -1;
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x00123DA0 File Offset: 0x00121FA0
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

		// Token: 0x06004824 RID: 18468 RVA: 0x00123DF8 File Offset: 0x00121FF8
		public static bool operator ==(UICamera.BackwardsCompatabilitySupport b, bool? s)
		{
			return UICamera.Cursor.Current.Valid == (s != null);
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x00123E14 File Offset: 0x00122014
		public static bool operator !=(UICamera.BackwardsCompatabilitySupport b, bool? s)
		{
			return UICamera.Cursor.Current.Valid != (s != null);
		}
	}

	// Token: 0x020007D8 RID: 2008
	public enum ClickNotification
	{
		// Token: 0x0400284F RID: 10319
		None,
		// Token: 0x04002850 RID: 10320
		Always,
		// Token: 0x04002851 RID: 10321
		BasedOnDelta
	}

	// Token: 0x020007D9 RID: 2009
	private class Highlighted
	{
		// Token: 0x04002852 RID: 10322
		public GameObject go;

		// Token: 0x04002853 RID: 10323
		public int counter;
	}

	// Token: 0x020007DA RID: 2010
	private struct RaycastCheckWork
	{
		// Token: 0x06004827 RID: 18471 RVA: 0x00123E3C File Offset: 0x0012203C
		public bool Check()
		{
			UIPanel uipanel = UIPanel.Find(this.hit.collider.transform, false);
			if (!uipanel)
			{
				return true;
			}
			if (uipanel.enabled && (uipanel.clipping == UIDrawCall.Clipping.None || UICamera.CheckRayEnterClippingRect(this.ray, uipanel.transform, uipanel.clipRange)))
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

		// Token: 0x04002854 RID: 10324
		public Ray ray;

		// Token: 0x04002855 RID: 10325
		public RaycastHit hit;

		// Token: 0x04002856 RID: 10326
		public float dist;

		// Token: 0x04002857 RID: 10327
		public int mask;
	}

	// Token: 0x020007DB RID: 2011
	private class CamSorter : Comparer<byte>
	{
		// Token: 0x06004829 RID: 18473 RVA: 0x00123F1C File Offset: 0x0012211C
		public override int Compare(byte a, byte b)
		{
			return UICamera.mList[(int)b].cachedCamera.depth.CompareTo(UICamera.mList[(int)a].cachedCamera.depth);
		}
	}
}
