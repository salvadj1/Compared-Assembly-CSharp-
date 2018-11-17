using System;
using System.Collections.Generic;
using Facepunch;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x0200040B RID: 1035
public class RPOS : MonoBehaviour
{
	// Token: 0x060025E4 RID: 9700 RVA: 0x00092354 File Offset: 0x00090554
	public RPOS()
	{
		this._onContextMenuVisible_ = new ContextClientWorkingCallback(this.OnContextMenuVisible);
	}

	// Token: 0x170008D6 RID: 2262
	// (get) Token: 0x060025E5 RID: 9701 RVA: 0x00092370 File Offset: 0x00090570
	private bool forceHideUseHoverText
	{
		get
		{
			return this.forceHideUseHoverTextCaseContextMenu || this.RPOSOn || this.forceHideUseHoverTextCaseLimitFlags;
		}
	}

	// Token: 0x060025E6 RID: 9702 RVA: 0x00092394 File Offset: 0x00090594
	private void CheckUseHoverTextEnabled()
	{
		this.SetHoverTextState(!this.forceHideUseHoverText && this.queuedUseHoverText, this.useHoverText);
	}

	// Token: 0x060025E7 RID: 9703 RVA: 0x000923C4 File Offset: 0x000905C4
	private void UseHoverTextInitialize()
	{
		if (this._useHoverPanel)
		{
			this.pointUseHoverOrigin = this._useHoverPanel.transform.localPosition;
			this.UpdateUseHoverTextPlane();
		}
		this.CheckUseHoverTextEnabled();
	}

	// Token: 0x060025E8 RID: 9704 RVA: 0x00092404 File Offset: 0x00090604
	private void UpdateUseHoverTextPlane()
	{
		this.pointUseHoverPlane = new Plane(-this._useHoverPanel.transform.forward, this._useHoverPanel.transform.position);
	}

	// Token: 0x060025E9 RID: 9705 RVA: 0x00092444 File Offset: 0x00090644
	private void UseHoverTextThink(Camera sceneCamera)
	{
		this.useHoverTextScreenPoint = null;
		if (!this.forceHideUseHoverText && this.queuedUseHoverText)
		{
			if (!(this.lastUseHoverText as MonoBehaviour))
			{
				this.lastUseHoverControllable = null;
			}
			if (this.lastUseHoverControllable)
			{
				if (!this._useHoverLabel)
				{
					return;
				}
				string a;
				if (this.useHoverTextUpdatable)
				{
					a = (this.lastUseHoverUpdatingText.ContextTextUpdate(this.lastUseHoverControllable, this.useHoverText) ?? string.Empty);
				}
				else
				{
					a = this.lastUseHoverText.ContextText(this.lastUseHoverControllable);
				}
				if (a != this.useHoverText)
				{
					this.useHoverText = a;
					this.SetHoverTextState(true, this.useHoverText);
				}
			}
			else
			{
				this.useHoverTextPanelVisible = false;
				if (this.lastUseHoverText != null)
				{
					RPOS.UseHoverTextClear();
				}
			}
			if (this.useHoverTextPanelVisible)
			{
				if (this.useHoverTextPoint)
				{
					Vector3 worldPoint;
					if (this.lastUseHoverPointText.ContextTextPoint(out worldPoint))
					{
						this.UseHoverTextMove(sceneCamera, worldPoint);
					}
					else
					{
						this.UseHoverTextMoveRevert();
					}
				}
				this._useHoverPanel.ManualPanelUpdate();
			}
		}
	}

	// Token: 0x060025EA RID: 9706 RVA: 0x00092584 File Offset: 0x00090784
	private void UseHoverTextMove(Camera sceneCamera, Vector3 worldPoint)
	{
		this.useHoverTextScreenPoint = new Vector3?(sceneCamera.WorldToScreenPoint(worldPoint));
	}

	// Token: 0x060025EB RID: 9707 RVA: 0x00092598 File Offset: 0x00090798
	private void UseHoverTextMoveRevert()
	{
		if (this._useHoverPanel)
		{
			this.useHoverTextScreenPoint = null;
			this._useHoverPanel.transform.localPosition = this.pointUseHoverOrigin;
		}
	}

	// Token: 0x060025EC RID: 9708 RVA: 0x000925DC File Offset: 0x000907DC
	private void UseHoverTextPostThink(Camera panelCamera)
	{
		if (this._useHoverPanel)
		{
			this.UseHoverTextScreen(panelCamera);
		}
	}

	// Token: 0x060025ED RID: 9709 RVA: 0x000925F8 File Offset: 0x000907F8
	private void UseHoverTextScreen(Camera panelCamera)
	{
		if (this.useHoverTextScreenPoint != null)
		{
			Vector3 value = this.useHoverTextScreenPoint.Value;
			this.useHoverTextScreenPoint = null;
			Vector2 vector = this.useHoverLabelBounds.min + value;
			Vector2 vector2 = this.useHoverLabelBounds.max + value;
			if (vector != vector2)
			{
				if (vector.x < 0f)
				{
					if (vector2.x < (float)Screen.width)
					{
						value.x -= vector.x;
					}
				}
				else if (vector2.x > (float)Screen.width)
				{
					value.x -= vector2.x - (float)Screen.width;
				}
				if (vector.y < 0f)
				{
					if (vector2.y < (float)Screen.height)
					{
						value.y -= vector.y;
					}
				}
				else if (vector2.y > (float)Screen.height)
				{
					value.y -= vector2.y - (float)Screen.height;
				}
			}
			Ray ray = panelCamera.ScreenPointToRay(value);
			float num;
			if (this.pointUseHoverPlane.Raycast(ray, ref num))
			{
				this._useHoverPanel.transform.position = ray.GetPoint(num);
				this._useHoverPanel.ManualPanelUpdate();
			}
		}
	}

	// Token: 0x060025EE RID: 9710 RVA: 0x00092784 File Offset: 0x00090984
	private void SetHoverTextState(bool enable, string text)
	{
		if (!this._useHoverLabel)
		{
			return;
		}
		if (enable && !string.IsNullOrEmpty(text))
		{
			bool flag = false;
			this._useHoverLabel.enabled = true;
			if (this._useHoverLabel.text != text)
			{
				this._useHoverLabel.text = text;
				flag = true;
			}
			if (this._useHoverPanel)
			{
				this.useHoverTextPanelVisible = this.lastUseHoverControllable;
				if (flag || !this._useHoverPanel.enabled)
				{
					this._useHoverPanel.enabled = true;
					this._useHoverPanel.ManualPanelUpdate();
					if (flag)
					{
						this.useHoverLabelBounds = NGUIMath.CalculateRelativeWidgetBounds(this._useHoverPanel.transform, this._useHoverLabel.transform);
					}
				}
			}
		}
		else
		{
			this.useHoverTextPanelVisible = false;
			if (this._useHoverPanel)
			{
				this._useHoverPanel.enabled = false;
			}
			else
			{
				this._useHoverLabel.enabled = false;
			}
		}
	}

	// Token: 0x060025EF RID: 9711 RVA: 0x00092894 File Offset: 0x00090A94
	public static void UseHoverTextClear()
	{
		RPOS.g_RPOS.useHoverText = string.Empty;
		RPOS.g_RPOS.queuedUseHoverText = false;
		RPOS.g_RPOS.lastUseHoverControllable = null;
		RPOS.g_RPOS.lastUseHoverText = null;
		RPOS.g_RPOS.lastUseHoverUpdatingText = null;
		RPOS.g_RPOS.lastUseHoverPointText = null;
		RPOS.g_RPOS.useHoverTextUpdatable = false;
		RPOS.g_RPOS.useHoverTextPoint = false;
		RPOS.g_RPOS.CheckUseHoverTextEnabled();
	}

	// Token: 0x060025F0 RID: 9712 RVA: 0x00092908 File Offset: 0x00090B08
	public static void UseHoverTextSet(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			RPOS.UseHoverTextClear();
		}
		else
		{
			RPOS.g_RPOS.queuedUseHoverText = true;
			RPOS.g_RPOS.useHoverText = text;
			RPOS.g_RPOS.lastUseHoverText = null;
			RPOS.g_RPOS.lastUseHoverControllable = null;
			RPOS.g_RPOS.lastUseHoverUpdatingText = null;
			RPOS.g_RPOS.lastUseHoverPointText = null;
			RPOS.g_RPOS.useHoverTextUpdatable = false;
			RPOS.g_RPOS.useHoverTextPoint = false;
			RPOS.g_RPOS.UseHoverTextMoveRevert();
			RPOS.g_RPOS.CheckUseHoverTextEnabled();
		}
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x00092998 File Offset: 0x00090B98
	public static void UseHoverTextSet(Controllable localPlayerControllable, IContextRequestableText text)
	{
		if (text == null)
		{
			RPOS.UseHoverTextClear();
		}
		else if (RPOS.g_RPOS.lastUseHoverText != text)
		{
			RPOS.g_RPOS.lastUseHoverText = text;
			RPOS.g_RPOS.lastUseHoverUpdatingText = (text as IContextRequestableUpdatingText);
			RPOS.g_RPOS.useHoverTextUpdatable = (RPOS.g_RPOS.lastUseHoverUpdatingText != null);
			RPOS.g_RPOS.lastUseHoverPointText = (text as IContextRequestablePointText);
			RPOS.g_RPOS.useHoverTextPoint = (RPOS.g_RPOS.lastUseHoverPointText != null);
			if (!RPOS.g_RPOS.useHoverTextPoint)
			{
				RPOS.g_RPOS.UseHoverTextMoveRevert();
			}
			RPOS.g_RPOS.lastUseHoverControllable = localPlayerControllable;
			RPOS.g_RPOS.useHoverText = text.ContextText(localPlayerControllable);
			RPOS.g_RPOS.queuedUseHoverText = true;
			RPOS.g_RPOS.CheckUseHoverTextEnabled();
		}
	}

	// Token: 0x060025F2 RID: 9714 RVA: 0x00092A70 File Offset: 0x00090C70
	internal static void BeforeSceneRender_Internal(Camera sceneCamera)
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.SceneUpdate(sceneCamera);
		}
	}

	// Token: 0x060025F3 RID: 9715 RVA: 0x00092A8C File Offset: 0x00090C8C
	internal static void BeforeRPOSRender_Internal(UICamera uicamera)
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.UIUpdate(uicamera);
		}
	}

	// Token: 0x060025F4 RID: 9716 RVA: 0x00092AA8 File Offset: 0x00090CA8
	public static RPOSWindow GetWindowByName(string name)
	{
		if (!RPOS.g_RPOS)
		{
			return null;
		}
		foreach (RPOSWindow rposwindow in RPOS.g_windows.allWindows)
		{
			if (rposwindow && rposwindow.title == name)
			{
				RPOSWindow.EnsureAwake(rposwindow);
				return rposwindow;
			}
		}
		Debug.Log("GetWindowByName returning null");
		return null;
	}

	// Token: 0x060025F5 RID: 9717 RVA: 0x00092B50 File Offset: 0x00090D50
	public static TRPOSWindow GetWindowByName<TRPOSWindow>(string name) where TRPOSWindow : RPOSWindow
	{
		if (!RPOS.g_RPOS)
		{
			return (TRPOSWindow)((object)null);
		}
		foreach (RPOSWindow rposwindow in RPOS.g_windows.allWindows)
		{
			if (rposwindow && rposwindow is TRPOSWindow && rposwindow.title == name)
			{
				RPOSWindow.EnsureAwake(rposwindow);
				return (TRPOSWindow)((object)rposwindow);
			}
		}
		return (TRPOSWindow)((object)null);
	}

	// Token: 0x060025F6 RID: 9718 RVA: 0x00092C08 File Offset: 0x00090E08
	public static IEnumerable<RPOSWindow> GetBumperWindowList()
	{
		RPOS rpos = RPOS.g_RPOS;
		if (!rpos)
		{
			Object[] array = Object.FindObjectsOfType(typeof(RPOS));
			if (array.Length <= 0)
			{
				return new RPOSWindow[0];
			}
			rpos = (RPOS)array[0];
		}
		return rpos.windowList;
	}

	// Token: 0x060025F7 RID: 9719 RVA: 0x00092C5C File Offset: 0x00090E5C
	public static bool BringToFront(RPOSWindow window)
	{
		window.EnsureAwake<RPOSWindow>();
		RPOS.g_windows.front = window;
		return RPOS.g_windows.lastPropertySetSuccess;
	}

	// Token: 0x060025F8 RID: 9720 RVA: 0x00092C70 File Offset: 0x00090E70
	public static bool MoveUp(RPOSWindow window)
	{
		return RPOS.g_windows.MoveUp(window.EnsureAwake<RPOSWindow>());
	}

	// Token: 0x060025F9 RID: 9721 RVA: 0x00092C80 File Offset: 0x00090E80
	public static bool SendToBack(RPOSWindow window)
	{
		window.EnsureAwake<RPOSWindow>();
		RPOS.g_windows.back = window;
		return RPOS.g_windows.lastPropertySetSuccess;
	}

	// Token: 0x060025FA RID: 9722 RVA: 0x00092C94 File Offset: 0x00090E94
	public static bool MoveDown(RPOSWindow window)
	{
		return RPOS.g_windows.MoveDown(window.EnsureAwake<RPOSWindow>());
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x00092CA4 File Offset: 0x00090EA4
	public static void CloseWindowByName(string name)
	{
		using (TempList<RPOSWindow> allWindows = RPOS.AllWindows)
		{
			foreach (RPOSWindow rposwindow in allWindows)
			{
				if (rposwindow && rposwindow.title == name)
				{
					rposwindow.ExternalClose();
				}
			}
		}
	}

	// Token: 0x060025FC RID: 9724 RVA: 0x00092D50 File Offset: 0x00090F50
	private static void InitWindow(RPOSWindow window)
	{
		if (window)
		{
			window.RPOSReady();
			window.CheckDisplay();
		}
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x00092D6C File Offset: 0x00090F6C
	internal static void RegisterWindow(RPOSWindow window)
	{
		if (window.zzz__index == -1)
		{
			window.zzz__index = RPOS.g_windows.allWindows.Count;
			RPOS.g_windows.allWindows.Add(window);
			if (RPOS.g_RPOS && !RPOS.g_RPOS.awaking)
			{
				RPOS.InitWindow(window);
			}
			RPOS.g_windows.orderChanged = true;
		}
	}

	// Token: 0x060025FE RID: 9726 RVA: 0x00092DCC File Offset: 0x00090FCC
	internal static void UnregisterWindow(RPOSWindow window)
	{
		while (window.zzz__index > -1)
		{
			bool flag;
			try
			{
				flag = (RPOS.g_windows.allWindows[window.zzz__index] == window);
			}
			catch (IndexOutOfRangeException)
			{
				flag = false;
			}
			if (flag)
			{
				RPOS.g_windows.allWindows.RemoveAt(window.zzz__index);
				int i = window.zzz__index;
				int count = RPOS.g_windows.allWindows.Count;
				while (i < count)
				{
					RPOS.g_windows.allWindows[i].zzz__index = i;
					i++;
				}
				RPOS.g_windows.orderChanged = true;
				break;
			}
			int num = RPOS.g_windows.allWindows.IndexOf(window);
			Debug.LogWarning(string.Format("Some how list maintanance failed, stored index was {0} but index of returned {1}", window.zzz__index, num), window);
			window.zzz__index = num;
		}
	}

	// Token: 0x170008D7 RID: 2263
	// (get) Token: 0x060025FF RID: 9727 RVA: 0x00092EB0 File Offset: 0x000910B0
	public static bool IsOpen
	{
		get
		{
			return RPOS.g_RPOS && RPOS.g_RPOS.RPOSOn && !RPOS.g_RPOS.awaking;
		}
	}

	// Token: 0x170008D8 RID: 2264
	// (get) Token: 0x06002600 RID: 9728 RVA: 0x00092EEC File Offset: 0x000910EC
	public static bool IsClosed
	{
		get
		{
			return !RPOS.IsOpen;
		}
	}

	// Token: 0x170008D9 RID: 2265
	// (get) Token: 0x06002601 RID: 9729 RVA: 0x00092EF8 File Offset: 0x000910F8
	public static TempList<RPOSWindow> AllWindows
	{
		get
		{
			return TempList<RPOSWindow>.New(RPOS.g_windows.allWindows);
		}
	}

	// Token: 0x170008DA RID: 2266
	// (get) Token: 0x06002602 RID: 9730 RVA: 0x00092F04 File Offset: 0x00091104
	public static TempList<RPOSWindow> AllOpenWindows
	{
		get
		{
			TempList<RPOSWindow> tempList = TempList<RPOSWindow>.New();
			foreach (RPOSWindow rposwindow in RPOS.g_windows.allWindows)
			{
				if (rposwindow && rposwindow.open)
				{
					tempList.Add(rposwindow);
				}
			}
			return tempList;
		}
	}

	// Token: 0x170008DB RID: 2267
	// (get) Token: 0x06002603 RID: 9731 RVA: 0x00092F88 File Offset: 0x00091188
	public static TempList<RPOSWindow> AllClosedWindows
	{
		get
		{
			TempList<RPOSWindow> tempList = TempList<RPOSWindow>.New();
			foreach (RPOSWindow rposwindow in RPOS.g_windows.allWindows)
			{
				if (rposwindow && rposwindow.closed)
				{
					tempList.Add(rposwindow);
				}
			}
			return tempList;
		}
	}

	// Token: 0x170008DC RID: 2268
	// (get) Token: 0x06002604 RID: 9732 RVA: 0x0009300C File Offset: 0x0009120C
	public static TempList<RPOSWindow> AllShowingWindows
	{
		get
		{
			TempList<RPOSWindow> tempList = TempList<RPOSWindow>.New();
			foreach (RPOSWindow rposwindow in RPOS.g_windows.allWindows)
			{
				if (rposwindow && rposwindow.showing)
				{
					tempList.Add(rposwindow);
				}
			}
			return tempList;
		}
	}

	// Token: 0x170008DD RID: 2269
	// (get) Token: 0x06002605 RID: 9733 RVA: 0x00093090 File Offset: 0x00091290
	public static TempList<RPOSWindow> AllHidingWindows
	{
		get
		{
			TempList<RPOSWindow> tempList = TempList<RPOSWindow>.New();
			foreach (RPOSWindow rposwindow in RPOS.g_windows.allWindows)
			{
				if (rposwindow && !rposwindow.showing)
				{
					tempList.Add(rposwindow);
				}
			}
			return tempList;
		}
	}

	// Token: 0x170008DE RID: 2270
	// (get) Token: 0x06002606 RID: 9734 RVA: 0x00093114 File Offset: 0x00091314
	public static int WindowCount
	{
		get
		{
			return RPOS.g_windows.allWindows.Count;
		}
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x00093120 File Offset: 0x00091320
	internal static bool GetWindowAbove(RPOSWindow window, out RPOSWindow fill)
	{
		if (!window)
		{
			throw new ArgumentNullException("window");
		}
		int order = window.order;
		if (order + 1 == RPOS.WindowCount)
		{
			fill = null;
			return false;
		}
		fill = RPOS.g_windows.allWindows[order + 1];
		return true;
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x0009316C File Offset: 0x0009136C
	internal static RPOSWindow GetWindowAbove(RPOSWindow window)
	{
		RPOSWindow rposwindow;
		return (!RPOS.GetWindowAbove(window, out rposwindow)) ? null : rposwindow;
	}

	// Token: 0x06002609 RID: 9737 RVA: 0x00093190 File Offset: 0x00091390
	internal static bool GetWindowBelow(RPOSWindow window, out RPOSWindow fill)
	{
		if (!window)
		{
			throw new ArgumentNullException("window");
		}
		int order = window.order;
		if (order == 0)
		{
			fill = null;
			return false;
		}
		fill = RPOS.g_windows.allWindows[order - 1];
		return true;
	}

	// Token: 0x0600260A RID: 9738 RVA: 0x000931D8 File Offset: 0x000913D8
	internal static RPOSWindow GetWindowBelow(RPOSWindow window)
	{
		RPOSWindow rposwindow;
		return (!RPOS.GetWindowAbove(window, out rposwindow)) ? null : rposwindow;
	}

	// Token: 0x0600260B RID: 9739 RVA: 0x000931FC File Offset: 0x000913FC
	private void Awake()
	{
		this.actionPanel.enabled = false;
		RPOS.g_RPOS = this;
		try
		{
			this.awaking = true;
			this._bumper.Populate();
			this.unlocker = LockCursorManager.CreateCursorUnlockNode(false, 64, "RPOS UNLOCKER");
			this.SetRPOSModeNoChecks(false);
			UIEventListener uieventListener = UIEventListener.Get(this._closeButton);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new UIEventListener.VoidDelegate(this.OnCloseButtonClicked));
			UIEventListener uieventListener3 = UIEventListener.Get(this._optionsButton);
			UIEventListener uieventListener4 = uieventListener3;
			uieventListener4.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener4.onClick, new UIEventListener.VoidDelegate(this.OnOptionsButtonClicked));
			TweenColor component = this.fadeSprite.GetComponent<TweenColor>();
			component.eventReceiver = base.gameObject;
			component.callWhenFinished = "FadeFinished";
			if (this._onContextMenuVisible_ != null)
			{
				Context.OnClientWorking += this._onContextMenuVisible_;
			}
			this.UseHoverTextInitialize();
		}
		finally
		{
			this.awaking = false;
		}
		using (TempList<RPOSWindow> tempList = TempList<RPOSWindow>.New(RPOS.g_windows.allWindows))
		{
			foreach (RPOSWindow window in tempList)
			{
				RPOS.InitWindow(window);
			}
		}
	}

	// Token: 0x0600260C RID: 9740 RVA: 0x00093394 File Offset: 0x00091594
	private void OnDestroy()
	{
		if (this.unlocker != null)
		{
			this.unlocker.Dispose();
			this.unlocker = null;
		}
		if (this._onContextMenuVisible_ != null)
		{
			Context.OnClientWorking -= this._onContextMenuVisible_;
		}
	}

	// Token: 0x0600260D RID: 9741 RVA: 0x000933CC File Offset: 0x000915CC
	public void OnCloseButtonClicked(GameObject go)
	{
		this.SetRPOSMode(false);
	}

	// Token: 0x0600260E RID: 9742 RVA: 0x000933D8 File Offset: 0x000915D8
	public void OnOptionsButtonClicked(GameObject go)
	{
		RPOS.OpenOptions();
	}

	// Token: 0x0600260F RID: 9743 RVA: 0x000933E0 File Offset: 0x000915E0
	[Obsolete("Use var player = RPOS.ObservedPlayer", true)]
	public Controllable GetObservedPlayer()
	{
		return this.observedPlayer;
	}

	// Token: 0x06002610 RID: 9744 RVA: 0x000933E8 File Offset: 0x000915E8
	public static void SetPlaqueActive(string plaqueName, bool on)
	{
		RPOS.g_RPOS._plaqueManager.SetPlaqueActive(plaqueName, on);
	}

	// Token: 0x06002611 RID: 9745 RVA: 0x000933FC File Offset: 0x000915FC
	public static RPOSItemRightClickMenu GetRightClickMenu()
	{
		return RPOS.g_RPOS.rightClickMenu;
	}

	// Token: 0x06002612 RID: 9746 RVA: 0x00093408 File Offset: 0x00091608
	[Obsolete("Use RPOS.ObservedPlayer = player")]
	public void SetObservedPlayer(Controllable player)
	{
		this.observedPlayer = player;
		RPOSWindow windowByName = RPOS.GetWindowByName("Inventory");
		if (windowByName)
		{
			RPOSInvCellManager componentInChildren = windowByName.GetComponentInChildren<RPOSInvCellManager>();
			componentInChildren.SetInventory(player.GetComponent<Inventory>(), false);
		}
		PlayerInventory component = player.GetComponent<PlayerInventory>();
		this._belt.CellIndexStart = 30;
		this._belt.SetInventory(component, false);
		RPOSWindow windowByName2 = RPOS.GetWindowByName("Armor");
		RPOSInvCellManager componentInChildren2 = windowByName2.GetComponentInChildren<RPOSInvCellManager>();
		componentInChildren2.CellIndexStart = 36;
		componentInChildren2.SetInventory(component, false);
		this.SetRPOSMode(false);
		RPOS.InjuryUpdate();
	}

	// Token: 0x06002613 RID: 9747 RVA: 0x00093498 File Offset: 0x00091698
	private void OnContextMenuVisible(bool visible)
	{
		this.forceHideUseHoverTextCaseContextMenu = visible;
		this.CheckUseHoverTextEnabled();
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x000934A8 File Offset: 0x000916A8
	[Obsolete("Use RPOS.Toggle()")]
	public void DoToggle()
	{
		this.SetRPOSMode(!this.RPOSOn);
	}

	// Token: 0x06002615 RID: 9749 RVA: 0x000934BC File Offset: 0x000916BC
	[Obsolete("Use RPOS.Hide()")]
	public void DoHide()
	{
		if (this.RPOSOn)
		{
			this.DoToggle();
		}
	}

	// Token: 0x06002616 RID: 9750 RVA: 0x000934D0 File Offset: 0x000916D0
	[Obsolete("Use RPOS.Show()")]
	public void DoShow()
	{
		if (!this.RPOSOn)
		{
			this.DoToggle();
		}
	}

	// Token: 0x06002617 RID: 9751 RVA: 0x000934E4 File Offset: 0x000916E4
	public static void ChangeRPOSMode(bool enable)
	{
		RPOS.g_RPOS.SetRPOSMode(enable);
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x000934F4 File Offset: 0x000916F4
	private void SetRPOSMode(bool enable)
	{
		if (enable != this.RPOSOn)
		{
			if (this.forceOff && enable)
			{
				return;
			}
			this.SetRPOSModeNoChecks(enable);
		}
	}

	// Token: 0x06002619 RID: 9753 RVA: 0x0009351C File Offset: 0x0009171C
	private void SetRPOSModeNoChecks(bool enable)
	{
		if (!this.rposModeLock)
		{
			try
			{
				this.rposModeLock = true;
				if (!this.observedPlayer)
				{
					enable = false;
				}
				bool flag = this.RPOSOn != enable;
				this.RPOSOn = enable;
				using (TempList<RPOSWindow> tempList = TempList<RPOSWindow>.New(RPOS.g_windows.allWindows))
				{
					if (enable)
					{
						foreach (RPOSWindow rposwindow in tempList)
						{
							if (rposwindow)
							{
								rposwindow.RPOSOn();
							}
						}
					}
					foreach (RPOSWindow rposwindow2 in tempList)
					{
						if (rposwindow2)
						{
							rposwindow2.CheckDisplay();
						}
					}
					if (!enable)
					{
						foreach (RPOSWindow rposwindow3 in tempList)
						{
							if (rposwindow3)
							{
								rposwindow3.RPOSOff();
							}
						}
						this._clickedItemCell = null;
						GUIHeldItem guiheldItem = GUIHeldItem.Get();
						if (guiheldItem)
						{
							guiheldItem.ClearHeldItem();
						}
					}
				}
				this._bumper.GetComponent<UIPanel>().enabled = enable;
				UIPanel.Find(this._closeButton.transform).enabled = enable;
				if (this.RPOSOn)
				{
					this.unlocker.On = true;
				}
				else
				{
					this.unlocker.TryLock();
				}
				if (flag)
				{
					this.CheckUseHoverTextEnabled();
				}
			}
			finally
			{
				this.rposModeLock = false;
			}
			ItemToolTip.SetToolTip(null, null);
			return;
		}
		if (enable != this.RPOSOn)
		{
			throw new InvalidOperationException((!enable) ? "You cannot turn OFF RPOS while its being turned ON-- check callstack" : "You cannot turn ON RPOS while its being turned OFF-- check callstack");
		}
	}

	// Token: 0x0600261A RID: 9754 RVA: 0x00093790 File Offset: 0x00091990
	[Obsolete("Use RPOS.Item_CellReset()")]
	public void ItemCellReset()
	{
		if (this._clickedItemCell)
		{
			GUIHeldItem.Get().ClearHeldItem(this._clickedItemCell);
			this._clickedItemCell._displayInventory.MarkSlotDirty((int)this._clickedItemCell._mySlot);
		}
		else
		{
			GUIHeldItem.Get().ClearHeldItem();
		}
		this._clickedItemCell = null;
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x000937F0 File Offset: 0x000919F0
	[Obsolete("Use RPOS.Item_CellDragBegin()")]
	public void ItemCellDragBegin(RPOSInventoryCell cell)
	{
		this.ItemCellReset();
		this.ItemCellClicked(cell);
	}

	// Token: 0x0600261C RID: 9756 RVA: 0x00093800 File Offset: 0x00091A00
	[Obsolete("Use RPOS.Item_CellDragEnd()")]
	public void ItemCellDragEnd(RPOSInventoryCell begin, RPOSInventoryCell end)
	{
		if (end)
		{
			GUIHeldItem.Get().ClearHeldItem(end);
		}
		this.ItemCellReset();
		if (begin != end && end && begin)
		{
			this._clickedItemCell = begin;
			this.ItemCellClicked(end);
		}
	}

	// Token: 0x0600261D RID: 9757 RVA: 0x0009385C File Offset: 0x00091A5C
	[Obsolete("Use RPOS.Item_CellDrop()")]
	public void ItemCellDrop(RPOSInventoryCell cell)
	{
		if (this._clickedItemCell != null)
		{
			this.ItemCellClicked(cell);
		}
	}

	// Token: 0x0600261E RID: 9758 RVA: 0x00093878 File Offset: 0x00091A78
	public static void TossItem(byte slot)
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.DoTossItem(slot);
		}
	}

	// Token: 0x0600261F RID: 9759 RVA: 0x00093894 File Offset: 0x00091A94
	private void DoTossItem(byte slot)
	{
		InventoryHolder component = RPOS.ObservedPlayer.GetComponent<InventoryHolder>();
		if (component)
		{
			component.TossItem((int)slot);
		}
		GUIHeldItem.Get().ClearHeldItem();
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x000938CC File Offset: 0x00091ACC
	public static void ItemCellAltClicked(RPOSInventoryCell cell)
	{
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x000938D0 File Offset: 0x00091AD0
	[Obsolete("Use RPOS.Item_CellClicked()")]
	public void ItemCellClicked(RPOSInventoryCell cell)
	{
		bool flag = false;
		byte b = 0;
		Inventory inventory = null;
		IInventoryItem inventoryItem = null;
		IInventoryItem inventoryItem2 = null;
		if (this._clickedItemCell != null)
		{
			inventory = this._clickedItemCell._displayInventory;
			b = this._clickedItemCell._mySlot;
			inventory.GetItem((int)b, out inventoryItem);
		}
		Inventory displayInventory = cell._displayInventory;
		byte mySlot = cell._mySlot;
		displayInventory.GetItem((int)mySlot, out inventoryItem2);
		if (inventoryItem == null && inventoryItem2 == null)
		{
			Debug.Log("wtf");
		}
		if (inventoryItem == null && inventoryItem2 != null)
		{
			this._clickedItemCell = cell;
			inventoryItem = cell._myDisplayItem;
			flag = true;
		}
		else if (inventoryItem != null && inventoryItem2 != null)
		{
			bool shift = Event.current.shift;
			NetEntityID fromInvID = NetEntityID.Get(inventory);
			NetEntityID toInvID = NetEntityID.Get(displayInventory);
			if (shift)
			{
				Inventory.ItemCombinePredicted(fromInvID, toInvID, (int)b, (int)mySlot);
			}
			else
			{
				Inventory.ItemMergePredicted(fromInvID, toInvID, (int)b, (int)mySlot);
			}
			inventoryItem = null;
			this._clickedItemCell = null;
		}
		else if (inventoryItem != null && inventoryItem2 == null)
		{
			NetEntityID toInvID2 = NetEntityID.Get(displayInventory);
			Inventory.ItemMovePredicted(NetEntityID.Get(inventory), toInvID2, (int)b, (int)mySlot);
			this._clickedItemCell = null;
			inventoryItem = null;
			flag = true;
		}
		if (inventoryItem != GUIHeldItem.CurrentItem())
		{
			if (inventoryItem != null)
			{
				if (!flag || !GUIHeldItem.Get().SetHeldItem(cell))
				{
					GUIHeldItem.Get().SetHeldItem(inventoryItem);
				}
			}
			else if (flag && cell)
			{
				GUIHeldItem.Get().ClearHeldItem(cell);
			}
			else
			{
				GUIHeldItem.Get().ClearHeldItem();
			}
		}
	}

	// Token: 0x06002622 RID: 9762 RVA: 0x00093A7C File Offset: 0x00091C7C
	public static void Item_CellReset()
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.ItemCellReset();
		}
	}

	// Token: 0x06002623 RID: 9763 RVA: 0x00093A98 File Offset: 0x00091C98
	public static void Item_CellDrop(RPOSInventoryCell cell)
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.ItemCellDrop(cell);
		}
	}

	// Token: 0x06002624 RID: 9764 RVA: 0x00093AB4 File Offset: 0x00091CB4
	public static void Item_CellDragEnd(RPOSInventoryCell begin, RPOSInventoryCell end)
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.ItemCellDragEnd(begin, end);
		}
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x00093AD4 File Offset: 0x00091CD4
	public static void Item_CellDragBegin(RPOSInventoryCell begin)
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.ItemCellDragBegin(begin);
		}
	}

	// Token: 0x06002626 RID: 9766 RVA: 0x00093AF0 File Offset: 0x00091CF0
	public static bool Item_IsClickedCell(RPOSInventoryCell cell)
	{
		return RPOS.g_RPOS && RPOS.g_RPOS._clickedItemCell && RPOS.g_RPOS._clickedItemCell == cell;
	}

	// Token: 0x170008DF RID: 2271
	// (get) Token: 0x06002627 RID: 9767 RVA: 0x00093B34 File Offset: 0x00091D34
	// (set) Token: 0x06002628 RID: 9768 RVA: 0x00093B58 File Offset: 0x00091D58
	public static Controllable ObservedPlayer
	{
		get
		{
			return (!RPOS.g_RPOS) ? null : RPOS.g_RPOS.observedPlayer;
		}
		set
		{
			if (RPOS.g_RPOS)
			{
				RPOS.g_RPOS.SetObservedPlayer(value);
			}
		}
	}

	// Token: 0x06002629 RID: 9769 RVA: 0x00093B74 File Offset: 0x00091D74
	public static bool GetObservedPlayerComponent<TComponent>(out TComponent component) where TComponent : Component
	{
		if (RPOS.g_RPOS)
		{
			Controllable controllable = RPOS.g_RPOS.observedPlayer;
			if (controllable)
			{
				component = controllable.GetComponent<TComponent>();
				return component;
			}
		}
		component = (TComponent)((object)null);
		return false;
	}

	// Token: 0x0600262A RID: 9770 RVA: 0x00093BD0 File Offset: 0x00091DD0
	public static bool IsObservedPlayer(Controllable controllable)
	{
		return RPOS.g_RPOS && controllable && RPOS.g_RPOS.observedPlayer == controllable;
	}

	// Token: 0x0600262B RID: 9771 RVA: 0x00093C00 File Offset: 0x00091E00
	public static void HealthUpdate(float value)
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.UpdateHealth(value);
		}
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x00093C1C File Offset: 0x00091E1C
	public static void Toggle()
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.DoToggle();
		}
	}

	// Token: 0x0600262D RID: 9773 RVA: 0x00093C38 File Offset: 0x00091E38
	public static void Hide()
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.DoHide();
		}
	}

	// Token: 0x0600262E RID: 9774 RVA: 0x00093C54 File Offset: 0x00091E54
	public static void SetEquipmentDirty()
	{
		if (RPOS.g_RPOS)
		{
			RPOS.g_RPOS.EquipmentDirty();
		}
	}

	// Token: 0x170008E0 RID: 2272
	// (get) Token: 0x0600262F RID: 9775 RVA: 0x00093C70 File Offset: 0x00091E70
	public static bool Exists
	{
		get
		{
			return RPOS.g_RPOS;
		}
	}

	// Token: 0x06002630 RID: 9776 RVA: 0x00093C7C File Offset: 0x00091E7C
	public static void OpenInfoWindow(ItemDataBlock itemdb)
	{
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x00093C80 File Offset: 0x00091E80
	public static bool FocusListedWindow(string name)
	{
		if (!RPOS.g_RPOS)
		{
			return false;
		}
		if (RPOS.g_RPOS.forceHideInventory)
		{
			return false;
		}
		bool result = false;
		foreach (RPOSWindow rposwindow in RPOS.g_RPOS.windowList)
		{
			if (rposwindow && rposwindow.title == name)
			{
				if (!RPOS.g_RPOS.RPOSOn)
				{
					RPOS.g_RPOS.SetRPOSMode(true);
					if (!RPOS.g_RPOS.RPOSOn)
					{
						return false;
					}
				}
				rposwindow.zzz___INTERNAL_FOCUS();
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06002632 RID: 9778 RVA: 0x00093D60 File Offset: 0x00091F60
	public static bool FocusInventory()
	{
		return RPOS.FocusListedWindow("Inventory");
	}

	// Token: 0x06002633 RID: 9779 RVA: 0x00093D6C File Offset: 0x00091F6C
	public static bool FocusArmor()
	{
		return RPOS.FocusListedWindow("Armor");
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x00093D78 File Offset: 0x00091F78
	public static void OpenLootWindow(LootableObject lootObj)
	{
		if (RPOS.g_RPOS)
		{
			RPOS.CloseWindowByName("Crafting");
			Vector3 localPosition = RPOS.g_RPOS.LootPanelPrefab.transform.localPosition;
			GameObject prefab;
			if (lootObj.lootWindowOverride)
			{
				prefab = lootObj.lootWindowOverride.gameObject;
			}
			else
			{
				prefab = RPOS.g_RPOS.LootPanelPrefab;
			}
			GameObject gameObject = NGUITools.AddChild(RPOS.g_RPOS.bottomCenterAnchor, prefab);
			gameObject.GetComponent<RPOSLootWindow>().SetLootable(lootObj, true);
			gameObject.transform.localPosition = localPosition;
			RPOS.BringToFront(gameObject.GetComponent<RPOSWindow>());
			RPOS.g_RPOS.SetRPOSMode(true);
		}
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x00093E24 File Offset: 0x00092024
	public static void OpenWorkbenchWindow(WorkBench workbenchObj)
	{
		if (RPOS.g_RPOS)
		{
			GameObject gameObject = NGUITools.AddChild(RPOS.g_RPOS.windowAnchor, RPOS.g_RPOS.WorkbenchPanelPrefab);
			gameObject.GetComponent<RPOSWorkbenchWindow>().SetWorkbench(workbenchObj);
			RPOS.BringToFront(gameObject.GetComponent<RPOSWindow>());
			RPOS.g_RPOS.SetRPOSMode(true);
		}
	}

	// Token: 0x06002636 RID: 9782 RVA: 0x00093E80 File Offset: 0x00092080
	public static void CloseWorkbenchWindow()
	{
		RPOS.CloseWindowByName("Workbench");
	}

	// Token: 0x06002637 RID: 9783 RVA: 0x00093E8C File Offset: 0x0009208C
	public static void CloseLootWindow()
	{
		foreach (RPOSWindow rposwindow in RPOS.g_windows.allWindows)
		{
			if (rposwindow && rposwindow is RPOSLootWindow)
			{
				((RPOSLootWindow)rposwindow).LootClosed();
				break;
			}
		}
	}

	// Token: 0x06002638 RID: 9784 RVA: 0x00093F10 File Offset: 0x00092110
	[Obsolete("Use RPOS.SetEquipmentDirty()")]
	public void EquipmentDirty()
	{
		RPOSArmorWindow windowByName = RPOS.GetWindowByName<RPOSArmorWindow>("Armor");
		windowByName.ForceUpdate();
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x00093F30 File Offset: 0x00092130
	[Obsolete("Use RPOS.HealthUpdate(amount)")]
	public void UpdateHealth(float amount)
	{
		this.healthLabel.text = amount.ToString("N0");
		this._healthProgress.sliderValue = Mathf.Clamp01(amount / 100f);
		UIFilledSprite component = this._healthProgress.foreground.GetComponent<UIFilledSprite>();
		if (amount > 75f)
		{
			component.color = Color.green;
		}
		else if (amount > 40f)
		{
			component.color = Color.yellow;
		}
		else
		{
			component.color = Color.red;
		}
	}

	// Token: 0x0600263A RID: 9786 RVA: 0x00093FC0 File Offset: 0x000921C0
	public static void SetCurrentFade(Color col)
	{
		RPOS.g_RPOS.fadeSprite.color = col;
		TweenColor component = RPOS.g_RPOS.fadeSprite.GetComponent<TweenColor>();
		component.from = col;
		component.to = col;
		component.isFullscreen = true;
		RPOS.g_RPOS.fadeSprite.enabled = true;
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x00094014 File Offset: 0x00092214
	public static void DoFadeNow(float duration, Color col)
	{
		RPOS.g_RPOS.DoFade(duration, col);
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x00094024 File Offset: 0x00092224
	public static void DoFade(float delay, float duration, Color col)
	{
		if (delay <= 0f)
		{
			RPOS.DoFadeNow(duration, col);
		}
		else
		{
			RPOS.g_RPOS.nextFadeColor = col;
			RPOS.g_RPOS.nextFadeDuration = duration;
			RPOS.g_RPOS.Invoke("Internal_DoFade", delay);
		}
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x00094064 File Offset: 0x00092264
	public void Internal_DoFade()
	{
		this.DoFade(this.nextFadeDuration, this.nextFadeColor);
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x00094078 File Offset: 0x00092278
	public void DoFade(float duration, Color col)
	{
		this.fadeSprite.enabled = true;
		TweenColor.Begin(this.fadeSprite.gameObject, duration, col);
	}

	// Token: 0x0600263F RID: 9791 RVA: 0x0009409C File Offset: 0x0009229C
	public static void ClearFade()
	{
		RPOS.g_RPOS.fadeSprite.enabled = false;
		RPOS.g_RPOS.CancelInvoke("DoFade");
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x000940C0 File Offset: 0x000922C0
	public void FadeFinished()
	{
		if (this.fadeSprite.color.a == 0f)
		{
			this.fadeSprite.enabled = false;
		}
	}

	// Token: 0x06002641 RID: 9793 RVA: 0x000940F8 File Offset: 0x000922F8
	private void SceneUpdate(Camera camera)
	{
		this.UseHoverTextThink(camera);
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x00094104 File Offset: 0x00092304
	private void UIUpdate(UICamera camera)
	{
		this.UseHoverTextPostThink(camera.cachedCamera);
	}

	// Token: 0x06002643 RID: 9795 RVA: 0x00094114 File Offset: 0x00092314
	private void Update()
	{
		HUDIndicator.Step();
		RPOSLimitFlags rposlimitFlags = this.currentLimitFlags;
		PlayerClient localPlayer = PlayerClient.GetLocalPlayer();
		if (localPlayer)
		{
			Controllable controllable = localPlayer.controllable;
			Controllable masterControllable;
			if (controllable && (masterControllable = controllable.masterControllable))
			{
				this.currentLimitFlags = masterControllable.rposLimitFlags;
			}
			else
			{
				this.currentLimitFlags = (RPOSLimitFlags.KeepOff | RPOSLimitFlags.HideInventory | RPOSLimitFlags.HideContext | RPOSLimitFlags.HideSprites);
			}
		}
		else
		{
			this.currentLimitFlags = (RPOSLimitFlags.KeepOff | RPOSLimitFlags.HideInventory | RPOSLimitFlags.HideContext | RPOSLimitFlags.HideSprites);
		}
		if (rposlimitFlags != this.currentLimitFlags)
		{
			RPOSLimitFlags rposlimitFlags2 = rposlimitFlags ^ this.currentLimitFlags;
			if ((rposlimitFlags2 & RPOSLimitFlags.HideContext) == RPOSLimitFlags.HideContext)
			{
				this.forceHideUseHoverTextCaseLimitFlags = ((this.currentLimitFlags & RPOSLimitFlags.HideContext) == RPOSLimitFlags.HideContext);
				this.CheckUseHoverTextEnabled();
			}
			if ((rposlimitFlags2 & RPOSLimitFlags.HideSprites) == RPOSLimitFlags.HideSprites)
			{
				this.forceHideSprites = ((this.currentLimitFlags & RPOSLimitFlags.HideSprites) == RPOSLimitFlags.HideSprites);
			}
			if ((rposlimitFlags2 & RPOSLimitFlags.HideInventory) == RPOSLimitFlags.HideInventory)
			{
				this.LimitInventory((this.currentLimitFlags & RPOSLimitFlags.HideInventory) == RPOSLimitFlags.HideInventory);
			}
			if ((rposlimitFlags2 & RPOSLimitFlags.KeepOff) == RPOSLimitFlags.KeepOff)
			{
				if ((this.currentLimitFlags & RPOSLimitFlags.KeepOff) == RPOSLimitFlags.KeepOff)
				{
					if (this.RPOSOn)
					{
						this.SetRPOSMode(false);
					}
					this.forceOff = true;
				}
				else
				{
					this.forceOff = false;
				}
			}
		}
		int width = Screen.width;
		int height = Screen.height;
		if (RPOS.g_windows.orderChanged || height != this.lastScreenHeight || width != this.lastScreenWidth)
		{
			RPOS.g_windows.ProcessDepth(this.windowAnchor.transform);
			this.lastScreenHeight = height;
			this.lastScreenWidth = width;
		}
		if (RPOS.g_RPOS.observedPlayer)
		{
			RPOS.SetPlaqueActive("PlaqueWorkbench1", RPOS.g_RPOS.observedPlayer.GetComponent<CraftingInventory>().AtWorkBench());
		}
	}

	// Token: 0x06002644 RID: 9796 RVA: 0x000942B8 File Offset: 0x000924B8
	private void LimitInventory(bool limit)
	{
		this.forceHideInventory = limit;
		using (TempList<RPOSWindow> allWindows = RPOS.AllWindows)
		{
			bool bumpersEnabled = !limit;
			foreach (RPOSWindow rposwindow in allWindows)
			{
				if (rposwindow && rposwindow.isInventoryRelated)
				{
					rposwindow.bumpersEnabled = bumpersEnabled;
				}
			}
			foreach (RPOSWindow rposwindow2 in allWindows)
			{
				if (rposwindow2)
				{
					rposwindow2.inventoryHide = limit;
				}
			}
		}
		if (this._belt)
		{
			UIPanel component = this._belt.GetComponent<UIPanel>();
			component.enabled = !limit;
		}
	}

	// Token: 0x06002645 RID: 9797 RVA: 0x000943F4 File Offset: 0x000925F4
	public static void MetabolismUpdate()
	{
		RPOS.g_RPOS.DoMetabolismUpdate();
	}

	// Token: 0x06002646 RID: 9798 RVA: 0x00094400 File Offset: 0x00092600
	public static void InjuryUpdate()
	{
		RPOS.g_RPOS.DoInjuryUpdate();
	}

	// Token: 0x06002647 RID: 9799 RVA: 0x0009440C File Offset: 0x0009260C
	private void DoInjuryUpdate()
	{
		FallDamage component = RPOS.ObservedPlayer.GetComponent<FallDamage>();
		this._plaqueManager.SetPlaqueActive("PlaqueInjury", component.GetLegInjury() > 0f);
	}

	// Token: 0x06002648 RID: 9800 RVA: 0x00094444 File Offset: 0x00092644
	private void ClearInjury()
	{
		this._plaqueManager.SetPlaqueActive("PlaqueInjury", false);
	}

	// Token: 0x06002649 RID: 9801 RVA: 0x00094458 File Offset: 0x00092658
	private void DoMetabolismUpdate()
	{
		Metabolism component = RPOS.ObservedPlayer.GetComponent<Metabolism>();
		this.calorieLabel.text = component.GetCalorieLevel().ToString("N0");
		this.radLabel.text = component.GetRadLevel().ToString("N0");
		this._foodProgress.sliderValue = Mathf.Clamp01(component.GetCalorieLevel() / 3000f);
		this._plaqueManager.SetPlaqueActive("PlaqueHunger", component.GetCalorieLevel() < 500f);
		this._plaqueManager.SetPlaqueActive("PlaqueCold", component.IsCold());
		this._plaqueManager.SetPlaqueActive("PlaqueWarm", component.IsWarm());
		this._plaqueManager.SetPlaqueActive("PlaqueRadiation", component.HasRadiationPoisoning());
		this._plaqueManager.SetPlaqueActive("PlaquePoison", component.IsPoisoned());
		if (component.GetCalorieLevel() < 500f)
		{
			this.calorieLabel.color = Color.red;
		}
		else
		{
			this.calorieLabel.color = Color.white;
		}
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x00094574 File Offset: 0x00092774
	public static void SetActionProgress(bool show, string label, float progress)
	{
		if (show)
		{
			if (!string.IsNullOrEmpty(label))
			{
				RPOS.g_RPOS.actionLabel.text = label;
				RPOS.g_RPOS.actionLabel.enabled = true;
			}
			else
			{
				RPOS.g_RPOS.actionLabel.enabled = false;
			}
			RPOS.g_RPOS.actionProgress.sliderValue = progress;
			RPOS.g_RPOS.actionPanel.enabled = true;
		}
		else
		{
			RPOS.g_RPOS.actionPanel.enabled = false;
		}
	}

	// Token: 0x0600264B RID: 9803 RVA: 0x000945FC File Offset: 0x000927FC
	public static int GetIndex2D(int x, int y, int width)
	{
		return x + y * width;
	}

	// Token: 0x0600264C RID: 9804 RVA: 0x00094604 File Offset: 0x00092804
	[Obsolete("Avoid using this", true)]
	public static RPOS Get()
	{
		return RPOS.g_RPOS;
	}

	// Token: 0x170008E1 RID: 2273
	// (get) Token: 0x0600264D RID: 9805 RVA: 0x0009460C File Offset: 0x0009280C
	public static bool hideSprites
	{
		get
		{
			return RPOS.g_RPOS && (RPOS.g_RPOS.RPOSOn || RPOS.g_RPOS.forceHideSprites);
		}
	}

	// Token: 0x0600264E RID: 9806 RVA: 0x00094648 File Offset: 0x00092848
	public static void OpenOptions()
	{
	}

	// Token: 0x0600264F RID: 9807 RVA: 0x0009464C File Offset: 0x0009284C
	public static void CloseOptions()
	{
	}

	// Token: 0x06002650 RID: 9808 RVA: 0x00094650 File Offset: 0x00092850
	public static void ToggleOptions()
	{
	}

	// Token: 0x06002651 RID: 9809 RVA: 0x00094654 File Offset: 0x00092854
	public static void LocalInventoryModified()
	{
		RPOSWindow windowByName = RPOS.GetWindowByName("Crafting");
		RPOSCraftWindow component = windowByName.GetComponent<RPOSCraftWindow>();
		component.LocalInventoryModified();
		RPOS.SetPlaqueActive("PlaqueCrafting", RPOS.g_RPOS.observedPlayer.GetComponent<CraftingInventory>().isCrafting);
	}

	// Token: 0x0400127C RID: 4732
	public const RPOSLimitFlags kNoControllableLimitFlags = RPOSLimitFlags.KeepOff | RPOSLimitFlags.HideInventory | RPOSLimitFlags.HideContext | RPOSLimitFlags.HideSprites;

	// Token: 0x0400127D RID: 4733
	[NonSerialized]
	private readonly ContextClientWorkingCallback _onContextMenuVisible_;

	// Token: 0x0400127E RID: 4734
	[SerializeField]
	private UILabel _useHoverLabel;

	// Token: 0x0400127F RID: 4735
	[SerializeField]
	private UIPanel _useHoverPanel;

	// Token: 0x04001280 RID: 4736
	private Controllable lastUseHoverControllable;

	// Token: 0x04001281 RID: 4737
	private IContextRequestableText lastUseHoverText;

	// Token: 0x04001282 RID: 4738
	private IContextRequestableUpdatingText lastUseHoverUpdatingText;

	// Token: 0x04001283 RID: 4739
	private IContextRequestablePointText lastUseHoverPointText;

	// Token: 0x04001284 RID: 4740
	private Vector3 pointUseHoverOrigin;

	// Token: 0x04001285 RID: 4741
	private Plane pointUseHoverPlane;

	// Token: 0x04001286 RID: 4742
	private AABBox useHoverLabelBounds;

	// Token: 0x04001287 RID: 4743
	private string useHoverText;

	// Token: 0x04001288 RID: 4744
	private bool forceHideUseHoverTextCaseContextMenu;

	// Token: 0x04001289 RID: 4745
	private bool forceHideUseHoverTextCaseLimitFlags;

	// Token: 0x0400128A RID: 4746
	private bool queuedUseHoverText;

	// Token: 0x0400128B RID: 4747
	private bool useHoverTextUpdatable;

	// Token: 0x0400128C RID: 4748
	private bool useHoverTextPoint;

	// Token: 0x0400128D RID: 4749
	private bool useHoverTextPanelVisible;

	// Token: 0x0400128E RID: 4750
	private Vector3? useHoverTextScreenPoint;

	// Token: 0x0400128F RID: 4751
	public static RPOS g_RPOS;

	// Token: 0x04001290 RID: 4752
	public List<RPOSWindow> windowList;

	// Token: 0x04001291 RID: 4753
	public RPOSBumper _bumper;

	// Token: 0x04001292 RID: 4754
	public GameObject _closeButton;

	// Token: 0x04001293 RID: 4755
	public GameObject _optionsButton;

	// Token: 0x04001294 RID: 4756
	public RPOSInvCellManager _belt;

	// Token: 0x04001295 RID: 4757
	[NonSerialized]
	public RPOSInventoryCell _clickedItemCell;

	// Token: 0x04001296 RID: 4758
	private bool RPOSOn;

	// Token: 0x04001297 RID: 4759
	private bool forceOff;

	// Token: 0x04001298 RID: 4760
	private bool forceHideSprites;

	// Token: 0x04001299 RID: 4761
	private bool forceHideInventory;

	// Token: 0x0400129A RID: 4762
	private Controllable observedPlayer;

	// Token: 0x0400129B RID: 4763
	private UnlockCursorNode unlocker;

	// Token: 0x0400129C RID: 4764
	public GameObject windowAnchor;

	// Token: 0x0400129D RID: 4765
	public GameObject bottomCenterAnchor;

	// Token: 0x0400129E RID: 4766
	public GameObject LootPanelPrefab;

	// Token: 0x0400129F RID: 4767
	public GameObject WorkbenchPanelPrefab;

	// Token: 0x040012A0 RID: 4768
	public GameObject InfoPanelPrefab;

	// Token: 0x040012A1 RID: 4769
	public UISlider _healthProgress;

	// Token: 0x040012A2 RID: 4770
	public UISlider _foodProgress;

	// Token: 0x040012A3 RID: 4771
	public UILabel healthLabel;

	// Token: 0x040012A4 RID: 4772
	public UISprite fadeSprite;

	// Token: 0x040012A5 RID: 4773
	public UILabel calorieLabel;

	// Token: 0x040012A6 RID: 4774
	public UILabel radLabel;

	// Token: 0x040012A7 RID: 4775
	public UISprite radSprite;

	// Token: 0x040012A8 RID: 4776
	public UIPanel actionPanel;

	// Token: 0x040012A9 RID: 4777
	public UILabel actionLabel;

	// Token: 0x040012AA RID: 4778
	public UISlider actionProgress;

	// Token: 0x040012AB RID: 4779
	public RPOSItemRightClickMenu rightClickMenu;

	// Token: 0x040012AC RID: 4780
	public RPOSPlaqueManager _plaqueManager;

	// Token: 0x040012AD RID: 4781
	public UIPanel[] keepTop;

	// Token: 0x040012AE RID: 4782
	public UIPanel[] keepBottom;

	// Token: 0x040012AF RID: 4783
	[HideInInspector]
	public Color nextFadeColor;

	// Token: 0x040012B0 RID: 4784
	[HideInInspector]
	public float nextFadeDuration;

	// Token: 0x040012B1 RID: 4785
	private bool awaking;

	// Token: 0x040012B2 RID: 4786
	private bool rposModeLock;

	// Token: 0x040012B3 RID: 4787
	private RPOSLimitFlags currentLimitFlags;

	// Token: 0x040012B4 RID: 4788
	private int lastScreenWidth;

	// Token: 0x040012B5 RID: 4789
	private int lastScreenHeight;

	// Token: 0x0200040C RID: 1036
	private static class g_windows
	{
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x000946B0 File Offset: 0x000928B0
		// (set) Token: 0x06002654 RID: 9812 RVA: 0x000946E4 File Offset: 0x000928E4
		public static RPOSWindow front
		{
			get
			{
				int count = RPOS.g_windows.allWindows.Count;
				return (count != 0) ? RPOS.g_windows.allWindows[count - 1] : null;
			}
			set
			{
				RPOS.g_windows.lastPropertySetSuccess = false;
				if (!value)
				{
					throw new ArgumentNullException();
				}
				if (value.zzz__index == -1)
				{
					throw new InvalidOperationException("The window was not awake");
				}
				int count = RPOS.g_windows.allWindows.Count;
				if (count == 0)
				{
					throw new InvalidOperationException("There definitely should have been a window in the list unless you passed a prefab here or didnt ensure awake.");
				}
				if (count == 1 || RPOS.g_windows.allWindows[count - 1] == value)
				{
					return;
				}
				for (int i = value.zzz__index; i < count - 1; i++)
				{
					RPOSWindow rposwindow = RPOS.g_windows.allWindows[i + 1];
					RPOS.g_windows.allWindows[i] = rposwindow;
					rposwindow.zzz__index = i;
				}
				RPOS.g_windows.allWindows[count - 1] = value;
				value.zzz__index = count - 1;
				RPOS.g_windows.orderChanged = true;
				RPOS.g_windows.lastPropertySetSuccess = true;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x000947B8 File Offset: 0x000929B8
		// (set) Token: 0x06002656 RID: 9814 RVA: 0x000947E8 File Offset: 0x000929E8
		public static RPOSWindow back
		{
			get
			{
				return (RPOS.g_windows.allWindows.Count != 0) ? RPOS.g_windows.allWindows[0] : null;
			}
			set
			{
				RPOS.g_windows.lastPropertySetSuccess = false;
				if (!value)
				{
					throw new ArgumentNullException();
				}
				if (value.zzz__index == -1)
				{
					throw new InvalidOperationException("The window was not awake");
				}
				int count = RPOS.g_windows.allWindows.Count;
				if (count == 0)
				{
					throw new InvalidOperationException("There definitely should have been a window in the list unless you passed a prefab here or didnt ensure awake.");
				}
				if (count == 1 || RPOS.g_windows.allWindows[0] == value)
				{
					return;
				}
				for (int i = value.zzz__index; i > 0; i--)
				{
					RPOSWindow rposwindow = RPOS.g_windows.allWindows[i - 1];
					RPOS.g_windows.allWindows[i] = rposwindow;
					rposwindow.zzz__index = i;
				}
				RPOS.g_windows.allWindows[0] = value;
				value.zzz__index = 0;
				RPOS.g_windows.orderChanged = true;
				RPOS.g_windows.lastPropertySetSuccess = true;
			}
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000948B4 File Offset: 0x00092AB4
		public static bool MoveUp(RPOSWindow window)
		{
			if (!window)
			{
				throw new ArgumentNullException();
			}
			if (window.zzz__index == -1)
			{
				throw new InvalidOperationException("The window was not awake");
			}
			int count = RPOS.g_windows.allWindows.Count;
			if (count == 0)
			{
				throw new InvalidOperationException("There definitely should have been a window in the list unless you passed a prefab here or didnt ensure awake.");
			}
			if (count == 1 || RPOS.g_windows.allWindows[count - 1] == window)
			{
				return false;
			}
			RPOS.g_windows.allWindows.Reverse(window.zzz__index, 2);
			RPOS.g_windows.allWindows[window.zzz__index].zzz__index = window.zzz__index;
			window.zzz__index++;
			RPOS.g_windows.orderChanged = true;
			return true;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00094968 File Offset: 0x00092B68
		public static bool MoveDown(RPOSWindow window)
		{
			if (!window)
			{
				throw new ArgumentNullException();
			}
			if (window.zzz__index == -1)
			{
				throw new InvalidOperationException("The window was not awake");
			}
			int count = RPOS.g_windows.allWindows.Count;
			if (count == 0)
			{
				throw new InvalidOperationException("There definitely should have been a window in the list unless you passed a prefab here or didnt ensure awake.");
			}
			if (count == 1 || RPOS.g_windows.allWindows[0] == window)
			{
				return false;
			}
			RPOS.g_windows.allWindows.Reverse(window.zzz__index - 1, 2);
			RPOS.g_windows.allWindows[window.zzz__index].zzz__index = window.zzz__index;
			window.zzz__index--;
			RPOS.g_windows.orderChanged = true;
			return true;
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x00094A1C File Offset: 0x00092C1C
		private static void ProcessTransform(Transform transform, ref float z)
		{
			AABBox aabbox = NGUIMath.CalculateRelativeWidgetBounds(transform);
			Vector3 localPosition = transform.localPosition;
			localPosition.z = -(z + aabbox.max.z);
			z += aabbox.size.z;
			transform.localPosition = localPosition;
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00094A6C File Offset: 0x00092C6C
		private static void ProcessTransform(ref Matrix4x4 toRoot, RPOSWindow window, ref float z, out Bounds bounds)
		{
			RPOS.g_windows.ProcessTransform(window.transform, ref z);
			Vector4 windowDimensions = window.windowDimensions;
			Matrix4x4 localToWorldMatrix = window.transform.localToWorldMatrix;
			bounds..ctor(toRoot.MultiplyPoint3x4(localToWorldMatrix.MultiplyPoint3x4(new Vector3(windowDimensions.x, windowDimensions.y, 0f))), Vector3.zero);
			bounds.Encapsulate(toRoot.MultiplyPoint3x4(localToWorldMatrix.MultiplyPoint3x4(new Vector3(windowDimensions.x, windowDimensions.y + windowDimensions.w, 0f))));
			bounds.Encapsulate(toRoot.MultiplyPoint3x4(localToWorldMatrix.MultiplyPoint3x4(new Vector3(windowDimensions.x + windowDimensions.z, windowDimensions.y, 0f))));
			bounds.Encapsulate(toRoot.MultiplyPoint3x4(localToWorldMatrix.MultiplyPoint3x4(new Vector3(windowDimensions.x + windowDimensions.z, windowDimensions.y + windowDimensions.w, 0f))));
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x00094B6C File Offset: 0x00092D6C
		public static void ProcessDepth(Transform uiRoot)
		{
			RPOS.g_windows.orderChanged = false;
			RPOS.g_windows.lastZ = 0f;
			UIPanel[] array = (!RPOS.g_RPOS) ? null : RPOS.g_RPOS.keepBottom;
			if (array != null)
			{
				for (int i = array.Length - 1; i >= 0; i--)
				{
					if (array[i])
					{
						RPOS.g_windows.ProcessTransform(array[i].transform, ref RPOS.g_windows.lastZ);
					}
				}
			}
			RPOS.g_windows.WindowRect[] array2 = new RPOS.g_windows.WindowRect[RPOS.g_windows.allWindows.Count];
			RPOS.g_windows.WindowRect a = default(RPOS.g_windows.WindowRect);
			Matrix4x4 worldToLocalMatrix = uiRoot.worldToLocalMatrix;
			int num = 0;
			foreach (RPOSWindow rposwindow in RPOS.g_windows.allWindows)
			{
				if (rposwindow)
				{
					Bounds bounds;
					RPOS.g_windows.ProcessTransform(ref worldToLocalMatrix, rposwindow, ref RPOS.g_windows.lastZ, out bounds);
					RPOS.g_windows.WindowRect windowRect = new RPOS.g_windows.WindowRect(bounds);
					if (a.empty)
					{
						a = windowRect;
					}
					else
					{
						a = new RPOS.g_windows.WindowRect(a, windowRect);
					}
					array2[num++] = windowRect;
				}
				else
				{
					array2[num++] = default(RPOS.g_windows.WindowRect);
				}
			}
			array = ((!RPOS.g_RPOS) ? null : RPOS.g_RPOS.keepTop);
			if (array != null)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j])
					{
						RPOS.g_windows.ProcessTransform(array[j].transform, ref RPOS.g_windows.lastZ);
					}
				}
			}
		}

		// Token: 0x040012B6 RID: 4790
		public static List<RPOSWindow> allWindows = new List<RPOSWindow>();

		// Token: 0x040012B7 RID: 4791
		public static bool orderChanged = false;

		// Token: 0x040012B8 RID: 4792
		public static bool lastPropertySetSuccess = false;

		// Token: 0x040012B9 RID: 4793
		public static float lastZ;

		// Token: 0x0200040D RID: 1037
		private struct WindowRect
		{
			// Token: 0x0600265C RID: 9820 RVA: 0x00094D38 File Offset: 0x00092F38
			public WindowRect(RPOS.g_windows.WindowRect a, RPOS.g_windows.WindowRect b)
			{
				if (a.x < b.x)
				{
					this.x = a.x;
					int num = b.x + (int)b.width - a.x;
					if (num < (int)a.width)
					{
						this.width = a.width;
					}
					else
					{
						this.width = (ushort)num;
					}
				}
				else
				{
					this.x = b.x;
					int num = a.x + (int)a.width - b.x;
					if (num < (int)b.width)
					{
						this.width = b.width;
					}
					else
					{
						this.width = (ushort)num;
					}
				}
				if (a.y < b.y)
				{
					this.y = a.y;
					int num = b.y + (int)b.height - a.y;
					if (num < (int)a.height)
					{
						this.height = a.height;
					}
					else
					{
						this.height = (ushort)num;
					}
				}
				else
				{
					this.y = b.y;
					int num = a.y + (int)a.height - b.y;
					if (num < (int)b.height)
					{
						this.height = b.height;
					}
					else
					{
						this.height = (ushort)num;
					}
				}
			}

			// Token: 0x0600265D RID: 9821 RVA: 0x00094EA8 File Offset: 0x000930A8
			public WindowRect(int x, int y, int width, int height)
			{
				if (width < 0)
				{
					this.x = x + width;
					this.width = (ushort)(-(ushort)width);
				}
				else
				{
					this.x = x;
					this.width = (ushort)width;
				}
				if (height < 0)
				{
					this.y = y + height;
					this.height = (ushort)(-(ushort)height);
				}
				else
				{
					this.y = y;
					this.height = (ushort)height;
				}
			}

			// Token: 0x0600265E RID: 9822 RVA: 0x00094F14 File Offset: 0x00093114
			public WindowRect(int x, int y, ushort width, ushort height)
			{
				this.x = x;
				this.y = y;
				this.width = width;
				this.height = height;
			}

			// Token: 0x0600265F RID: 9823 RVA: 0x00094F34 File Offset: 0x00093134
			public WindowRect(Bounds bounds)
			{
				Vector2 vector = bounds.center;
				Vector2 vector2 = bounds.extents;
				if (vector2.x < 0f)
				{
					this.x = Mathf.FloorToInt(vector.x + vector2.x);
					this.width = (ushort)Mathf.CeilToInt(vector.x - vector2.x - (float)this.x);
				}
				else
				{
					this.x = Mathf.FloorToInt(vector.x - vector2.x);
					this.width = (ushort)Mathf.CeilToInt(vector.x + vector2.x - (float)this.x);
				}
				if (vector2.y < 0f)
				{
					this.y = Mathf.FloorToInt(vector.y + vector2.y);
					this.height = (ushort)Mathf.CeilToInt(vector.y - vector2.y - (float)this.y);
				}
				else
				{
					this.y = Mathf.FloorToInt(vector.y - vector2.y);
					this.height = (ushort)Mathf.CeilToInt(vector.y + vector2.y - (float)this.y);
				}
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x06002660 RID: 9824 RVA: 0x0009507C File Offset: 0x0009327C
			public bool empty
			{
				get
				{
					return this.width == 0 || this.height == 0;
				}
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x06002661 RID: 9825 RVA: 0x00095098 File Offset: 0x00093298
			public int left
			{
				get
				{
					return this.x;
				}
			}

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x06002662 RID: 9826 RVA: 0x000950A0 File Offset: 0x000932A0
			public int right
			{
				get
				{
					return this.x + (int)this.width;
				}
			}

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x06002663 RID: 9827 RVA: 0x000950B0 File Offset: 0x000932B0
			public int top
			{
				get
				{
					return this.y;
				}
			}

			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x06002664 RID: 9828 RVA: 0x000950B8 File Offset: 0x000932B8
			public int bottom
			{
				get
				{
					return this.y + (int)this.height;
				}
			}

			// Token: 0x170008E9 RID: 2281
			// (get) Token: 0x06002665 RID: 9829 RVA: 0x000950C8 File Offset: 0x000932C8
			public int center
			{
				get
				{
					return this.x + (int)(this.width / 2);
				}
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06002666 RID: 9830 RVA: 0x000950DC File Offset: 0x000932DC
			public int middle
			{
				get
				{
					return this.y + (int)(this.height / 2);
				}
			}

			// Token: 0x06002667 RID: 9831 RVA: 0x000950F0 File Offset: 0x000932F0
			public bool Contains(RPOS.g_windows.WindowRect other)
			{
				return ((this.x >= other.x) ? (this.x == other.x && other.width < this.width) : (other.x + (int)other.width - this.x <= (int)this.width)) && ((this.y >= other.y) ? (this.y == other.y && other.height < this.height) : (other.y + (int)other.height - this.y <= (int)this.height));
			}

			// Token: 0x06002668 RID: 9832 RVA: 0x000951C0 File Offset: 0x000933C0
			public bool ContainsOrEquals(RPOS.g_windows.WindowRect other)
			{
				return ((other.x != this.x) ? (this.x < other.x && other.x + (int)other.width - this.x <= (int)this.width) : (other.width <= this.width)) && ((other.y != this.y) ? (this.y < other.y && other.y + (int)other.height - this.y <= (int)this.height) : (other.height <= this.height));
			}

			// Token: 0x06002669 RID: 9833 RVA: 0x00095294 File Offset: 0x00093494
			public bool Equals(RPOS.g_windows.WindowRect other)
			{
				return this.width == other.width && this.x == other.x && this.y == other.y && this.height == other.height;
			}

			// Token: 0x0600266A RID: 9834 RVA: 0x000952EC File Offset: 0x000934EC
			public bool Overlaps(RPOS.g_windows.WindowRect other)
			{
				return ((other.x >= this.x) ? (this.x - other.x + (int)this.width > 0) : (other.x + (int)other.width > this.x)) && ((other.y >= this.y) ? (this.y - other.y + (int)this.height > 0) : (other.y + (int)other.height > this.y));
			}

			// Token: 0x0600266B RID: 9835 RVA: 0x00095390 File Offset: 0x00093590
			public bool OverlapsOrTouches(RPOS.g_windows.WindowRect other)
			{
				return (other.x == this.x || ((other.x >= this.x) ? (this.x - other.x + (int)this.width >= 0) : (other.x + (int)other.width >= this.x))) && (other.y == this.y || ((other.y >= this.y) ? (this.y - other.y + (int)this.height >= 0) : (other.y + (int)other.height >= this.y)));
			}

			// Token: 0x0600266C RID: 9836 RVA: 0x00095464 File Offset: 0x00093664
			public bool OverlapsOrOutside(RPOS.g_windows.WindowRect other)
			{
				return other.x < this.x || other.y < this.y || this.x - other.x + (int)other.width > (int)this.width || this.y - other.y + (int)this.height > (int)this.height;
			}

			// Token: 0x0600266D RID: 9837 RVA: 0x000954D8 File Offset: 0x000936D8
			public bool OverlapsTouchesOrOutside(RPOS.g_windows.WindowRect other)
			{
				return other.x <= this.x || other.y <= this.y || this.x - other.x + (int)other.width >= (int)this.width || this.y - other.y + (int)this.height >= (int)this.height;
			}

			// Token: 0x0600266E RID: 9838 RVA: 0x00095550 File Offset: 0x00093750
			public override string ToString()
			{
				return string.Format("{{x:{0},y:{1},width:{2},height:{3}}}", new object[]
				{
					this.x,
					this.y,
					this.width,
					this.height
				});
			}

			// Token: 0x040012BA RID: 4794
			public int x;

			// Token: 0x040012BB RID: 4795
			public int y;

			// Token: 0x040012BC RID: 4796
			public ushort width;

			// Token: 0x040012BD RID: 4797
			public ushort height;
		}
	}
}
