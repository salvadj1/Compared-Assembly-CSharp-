using System;
using System.Collections.Generic;
using Facepunch;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x020004C0 RID: 1216
public class RPOS : MonoBehaviour
{
	// Token: 0x0600296E RID: 10606 RVA: 0x00098218 File Offset: 0x00096418
	public RPOS()
	{
		this._onContextMenuVisible_ = new global::ContextClientWorkingCallback(this.OnContextMenuVisible);
	}

	// Token: 0x1700093C RID: 2364
	// (get) Token: 0x0600296F RID: 10607 RVA: 0x00098234 File Offset: 0x00096434
	private bool forceHideUseHoverText
	{
		get
		{
			return this.forceHideUseHoverTextCaseContextMenu || this.RPOSOn || this.forceHideUseHoverTextCaseLimitFlags;
		}
	}

	// Token: 0x06002970 RID: 10608 RVA: 0x00098258 File Offset: 0x00096458
	private void CheckUseHoverTextEnabled()
	{
		this.SetHoverTextState(!this.forceHideUseHoverText && this.queuedUseHoverText, this.useHoverText);
	}

	// Token: 0x06002971 RID: 10609 RVA: 0x00098288 File Offset: 0x00096488
	private void UseHoverTextInitialize()
	{
		if (this._useHoverPanel)
		{
			this.pointUseHoverOrigin = this._useHoverPanel.transform.localPosition;
			this.UpdateUseHoverTextPlane();
		}
		this.CheckUseHoverTextEnabled();
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x000982C8 File Offset: 0x000964C8
	private void UpdateUseHoverTextPlane()
	{
		this.pointUseHoverPlane = new Plane(-this._useHoverPanel.transform.forward, this._useHoverPanel.transform.position);
	}

	// Token: 0x06002973 RID: 10611 RVA: 0x00098308 File Offset: 0x00096508
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
					global::RPOS.UseHoverTextClear();
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

	// Token: 0x06002974 RID: 10612 RVA: 0x00098448 File Offset: 0x00096648
	private void UseHoverTextMove(Camera sceneCamera, Vector3 worldPoint)
	{
		this.useHoverTextScreenPoint = new Vector3?(sceneCamera.WorldToScreenPoint(worldPoint));
	}

	// Token: 0x06002975 RID: 10613 RVA: 0x0009845C File Offset: 0x0009665C
	private void UseHoverTextMoveRevert()
	{
		if (this._useHoverPanel)
		{
			this.useHoverTextScreenPoint = null;
			this._useHoverPanel.transform.localPosition = this.pointUseHoverOrigin;
		}
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x000984A0 File Offset: 0x000966A0
	private void UseHoverTextPostThink(Camera panelCamera)
	{
		if (this._useHoverPanel)
		{
			this.UseHoverTextScreen(panelCamera);
		}
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x000984BC File Offset: 0x000966BC
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

	// Token: 0x06002978 RID: 10616 RVA: 0x00098648 File Offset: 0x00096848
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
						this.useHoverLabelBounds = global::NGUIMath.CalculateRelativeWidgetBounds(this._useHoverPanel.transform, this._useHoverLabel.transform);
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

	// Token: 0x06002979 RID: 10617 RVA: 0x00098758 File Offset: 0x00096958
	public static void UseHoverTextClear()
	{
		global::RPOS.g_RPOS.useHoverText = string.Empty;
		global::RPOS.g_RPOS.queuedUseHoverText = false;
		global::RPOS.g_RPOS.lastUseHoverControllable = null;
		global::RPOS.g_RPOS.lastUseHoverText = null;
		global::RPOS.g_RPOS.lastUseHoverUpdatingText = null;
		global::RPOS.g_RPOS.lastUseHoverPointText = null;
		global::RPOS.g_RPOS.useHoverTextUpdatable = false;
		global::RPOS.g_RPOS.useHoverTextPoint = false;
		global::RPOS.g_RPOS.CheckUseHoverTextEnabled();
	}

	// Token: 0x0600297A RID: 10618 RVA: 0x000987CC File Offset: 0x000969CC
	public static void UseHoverTextSet(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			global::RPOS.UseHoverTextClear();
		}
		else
		{
			global::RPOS.g_RPOS.queuedUseHoverText = true;
			global::RPOS.g_RPOS.useHoverText = text;
			global::RPOS.g_RPOS.lastUseHoverText = null;
			global::RPOS.g_RPOS.lastUseHoverControllable = null;
			global::RPOS.g_RPOS.lastUseHoverUpdatingText = null;
			global::RPOS.g_RPOS.lastUseHoverPointText = null;
			global::RPOS.g_RPOS.useHoverTextUpdatable = false;
			global::RPOS.g_RPOS.useHoverTextPoint = false;
			global::RPOS.g_RPOS.UseHoverTextMoveRevert();
			global::RPOS.g_RPOS.CheckUseHoverTextEnabled();
		}
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x0009885C File Offset: 0x00096A5C
	public static void UseHoverTextSet(global::Controllable localPlayerControllable, global::IContextRequestableText text)
	{
		if (text == null)
		{
			global::RPOS.UseHoverTextClear();
		}
		else if (global::RPOS.g_RPOS.lastUseHoverText != text)
		{
			global::RPOS.g_RPOS.lastUseHoverText = text;
			global::RPOS.g_RPOS.lastUseHoverUpdatingText = (text as global::IContextRequestableUpdatingText);
			global::RPOS.g_RPOS.useHoverTextUpdatable = (global::RPOS.g_RPOS.lastUseHoverUpdatingText != null);
			global::RPOS.g_RPOS.lastUseHoverPointText = (text as global::IContextRequestablePointText);
			global::RPOS.g_RPOS.useHoverTextPoint = (global::RPOS.g_RPOS.lastUseHoverPointText != null);
			if (!global::RPOS.g_RPOS.useHoverTextPoint)
			{
				global::RPOS.g_RPOS.UseHoverTextMoveRevert();
			}
			global::RPOS.g_RPOS.lastUseHoverControllable = localPlayerControllable;
			global::RPOS.g_RPOS.useHoverText = text.ContextText(localPlayerControllable);
			global::RPOS.g_RPOS.queuedUseHoverText = true;
			global::RPOS.g_RPOS.CheckUseHoverTextEnabled();
		}
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x00098934 File Offset: 0x00096B34
	internal static void BeforeSceneRender_Internal(Camera sceneCamera)
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.SceneUpdate(sceneCamera);
		}
	}

	// Token: 0x0600297D RID: 10621 RVA: 0x00098950 File Offset: 0x00096B50
	internal static void BeforeRPOSRender_Internal(global::UICamera uicamera)
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.UIUpdate(uicamera);
		}
	}

	// Token: 0x0600297E RID: 10622 RVA: 0x0009896C File Offset: 0x00096B6C
	public static global::RPOSWindow GetWindowByName(string name)
	{
		if (!global::RPOS.g_RPOS)
		{
			return null;
		}
		foreach (global::RPOSWindow rposwindow in global::RPOS.g_windows.allWindows)
		{
			if (rposwindow && rposwindow.title == name)
			{
				global::RPOSWindow.EnsureAwake(rposwindow);
				return rposwindow;
			}
		}
		Debug.Log("GetWindowByName returning null");
		return null;
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x00098A14 File Offset: 0x00096C14
	public static TRPOSWindow GetWindowByName<TRPOSWindow>(string name) where TRPOSWindow : global::RPOSWindow
	{
		if (!global::RPOS.g_RPOS)
		{
			return (TRPOSWindow)((object)null);
		}
		foreach (global::RPOSWindow rposwindow in global::RPOS.g_windows.allWindows)
		{
			if (rposwindow && rposwindow is TRPOSWindow && rposwindow.title == name)
			{
				global::RPOSWindow.EnsureAwake(rposwindow);
				return (TRPOSWindow)((object)rposwindow);
			}
		}
		return (TRPOSWindow)((object)null);
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x00098ACC File Offset: 0x00096CCC
	public static IEnumerable<global::RPOSWindow> GetBumperWindowList()
	{
		global::RPOS rpos = global::RPOS.g_RPOS;
		if (!rpos)
		{
			Object[] array = Object.FindObjectsOfType(typeof(global::RPOS));
			if (array.Length <= 0)
			{
				return new global::RPOSWindow[0];
			}
			rpos = (global::RPOS)array[0];
		}
		return rpos.windowList;
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x00098B20 File Offset: 0x00096D20
	public static bool BringToFront(global::RPOSWindow window)
	{
		window.EnsureAwake<global::RPOSWindow>();
		global::RPOS.g_windows.front = window;
		return global::RPOS.g_windows.lastPropertySetSuccess;
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x00098B34 File Offset: 0x00096D34
	public static bool MoveUp(global::RPOSWindow window)
	{
		return global::RPOS.g_windows.MoveUp(window.EnsureAwake<global::RPOSWindow>());
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x00098B44 File Offset: 0x00096D44
	public static bool SendToBack(global::RPOSWindow window)
	{
		window.EnsureAwake<global::RPOSWindow>();
		global::RPOS.g_windows.back = window;
		return global::RPOS.g_windows.lastPropertySetSuccess;
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x00098B58 File Offset: 0x00096D58
	public static bool MoveDown(global::RPOSWindow window)
	{
		return global::RPOS.g_windows.MoveDown(window.EnsureAwake<global::RPOSWindow>());
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x00098B68 File Offset: 0x00096D68
	public static void CloseWindowByName(string name)
	{
		using (global::TempList<global::RPOSWindow> allWindows = global::RPOS.AllWindows)
		{
			foreach (global::RPOSWindow rposwindow in allWindows)
			{
				if (rposwindow && rposwindow.title == name)
				{
					rposwindow.ExternalClose();
				}
			}
		}
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x00098C14 File Offset: 0x00096E14
	private static void InitWindow(global::RPOSWindow window)
	{
		if (window)
		{
			window.RPOSReady();
			window.CheckDisplay();
		}
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x00098C30 File Offset: 0x00096E30
	internal static void RegisterWindow(global::RPOSWindow window)
	{
		if (window.zzz__index == -1)
		{
			window.zzz__index = global::RPOS.g_windows.allWindows.Count;
			global::RPOS.g_windows.allWindows.Add(window);
			if (global::RPOS.g_RPOS && !global::RPOS.g_RPOS.awaking)
			{
				global::RPOS.InitWindow(window);
			}
			global::RPOS.g_windows.orderChanged = true;
		}
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x00098C90 File Offset: 0x00096E90
	internal static void UnregisterWindow(global::RPOSWindow window)
	{
		while (window.zzz__index > -1)
		{
			bool flag;
			try
			{
				flag = (global::RPOS.g_windows.allWindows[window.zzz__index] == window);
			}
			catch (IndexOutOfRangeException)
			{
				flag = false;
			}
			if (flag)
			{
				global::RPOS.g_windows.allWindows.RemoveAt(window.zzz__index);
				int i = window.zzz__index;
				int count = global::RPOS.g_windows.allWindows.Count;
				while (i < count)
				{
					global::RPOS.g_windows.allWindows[i].zzz__index = i;
					i++;
				}
				global::RPOS.g_windows.orderChanged = true;
				break;
			}
			int num = global::RPOS.g_windows.allWindows.IndexOf(window);
			Debug.LogWarning(string.Format("Some how list maintanance failed, stored index was {0} but index of returned {1}", window.zzz__index, num), window);
			window.zzz__index = num;
		}
	}

	// Token: 0x1700093D RID: 2365
	// (get) Token: 0x06002989 RID: 10633 RVA: 0x00098D74 File Offset: 0x00096F74
	public static bool IsOpen
	{
		get
		{
			return global::RPOS.g_RPOS && global::RPOS.g_RPOS.RPOSOn && !global::RPOS.g_RPOS.awaking;
		}
	}

	// Token: 0x1700093E RID: 2366
	// (get) Token: 0x0600298A RID: 10634 RVA: 0x00098DB0 File Offset: 0x00096FB0
	public static bool IsClosed
	{
		get
		{
			return !global::RPOS.IsOpen;
		}
	}

	// Token: 0x1700093F RID: 2367
	// (get) Token: 0x0600298B RID: 10635 RVA: 0x00098DBC File Offset: 0x00096FBC
	public static global::TempList<global::RPOSWindow> AllWindows
	{
		get
		{
			return global::TempList<global::RPOSWindow>.New(global::RPOS.g_windows.allWindows);
		}
	}

	// Token: 0x17000940 RID: 2368
	// (get) Token: 0x0600298C RID: 10636 RVA: 0x00098DC8 File Offset: 0x00096FC8
	public static global::TempList<global::RPOSWindow> AllOpenWindows
	{
		get
		{
			global::TempList<global::RPOSWindow> tempList = global::TempList<global::RPOSWindow>.New();
			foreach (global::RPOSWindow rposwindow in global::RPOS.g_windows.allWindows)
			{
				if (rposwindow && rposwindow.open)
				{
					tempList.Add(rposwindow);
				}
			}
			return tempList;
		}
	}

	// Token: 0x17000941 RID: 2369
	// (get) Token: 0x0600298D RID: 10637 RVA: 0x00098E4C File Offset: 0x0009704C
	public static global::TempList<global::RPOSWindow> AllClosedWindows
	{
		get
		{
			global::TempList<global::RPOSWindow> tempList = global::TempList<global::RPOSWindow>.New();
			foreach (global::RPOSWindow rposwindow in global::RPOS.g_windows.allWindows)
			{
				if (rposwindow && rposwindow.closed)
				{
					tempList.Add(rposwindow);
				}
			}
			return tempList;
		}
	}

	// Token: 0x17000942 RID: 2370
	// (get) Token: 0x0600298E RID: 10638 RVA: 0x00098ED0 File Offset: 0x000970D0
	public static global::TempList<global::RPOSWindow> AllShowingWindows
	{
		get
		{
			global::TempList<global::RPOSWindow> tempList = global::TempList<global::RPOSWindow>.New();
			foreach (global::RPOSWindow rposwindow in global::RPOS.g_windows.allWindows)
			{
				if (rposwindow && rposwindow.showing)
				{
					tempList.Add(rposwindow);
				}
			}
			return tempList;
		}
	}

	// Token: 0x17000943 RID: 2371
	// (get) Token: 0x0600298F RID: 10639 RVA: 0x00098F54 File Offset: 0x00097154
	public static global::TempList<global::RPOSWindow> AllHidingWindows
	{
		get
		{
			global::TempList<global::RPOSWindow> tempList = global::TempList<global::RPOSWindow>.New();
			foreach (global::RPOSWindow rposwindow in global::RPOS.g_windows.allWindows)
			{
				if (rposwindow && !rposwindow.showing)
				{
					tempList.Add(rposwindow);
				}
			}
			return tempList;
		}
	}

	// Token: 0x17000944 RID: 2372
	// (get) Token: 0x06002990 RID: 10640 RVA: 0x00098FD8 File Offset: 0x000971D8
	public static int WindowCount
	{
		get
		{
			return global::RPOS.g_windows.allWindows.Count;
		}
	}

	// Token: 0x06002991 RID: 10641 RVA: 0x00098FE4 File Offset: 0x000971E4
	internal static bool GetWindowAbove(global::RPOSWindow window, out global::RPOSWindow fill)
	{
		if (!window)
		{
			throw new ArgumentNullException("window");
		}
		int order = window.order;
		if (order + 1 == global::RPOS.WindowCount)
		{
			fill = null;
			return false;
		}
		fill = global::RPOS.g_windows.allWindows[order + 1];
		return true;
	}

	// Token: 0x06002992 RID: 10642 RVA: 0x00099030 File Offset: 0x00097230
	internal static global::RPOSWindow GetWindowAbove(global::RPOSWindow window)
	{
		global::RPOSWindow rposwindow;
		return (!global::RPOS.GetWindowAbove(window, out rposwindow)) ? null : rposwindow;
	}

	// Token: 0x06002993 RID: 10643 RVA: 0x00099054 File Offset: 0x00097254
	internal static bool GetWindowBelow(global::RPOSWindow window, out global::RPOSWindow fill)
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
		fill = global::RPOS.g_windows.allWindows[order - 1];
		return true;
	}

	// Token: 0x06002994 RID: 10644 RVA: 0x0009909C File Offset: 0x0009729C
	internal static global::RPOSWindow GetWindowBelow(global::RPOSWindow window)
	{
		global::RPOSWindow rposwindow;
		return (!global::RPOS.GetWindowAbove(window, out rposwindow)) ? null : rposwindow;
	}

	// Token: 0x06002995 RID: 10645 RVA: 0x000990C0 File Offset: 0x000972C0
	private void Awake()
	{
		this.actionPanel.enabled = false;
		global::RPOS.g_RPOS = this;
		try
		{
			this.awaking = true;
			this._bumper.Populate();
			this.unlocker = LockCursorManager.CreateCursorUnlockNode(false, 64, "RPOS UNLOCKER");
			this.SetRPOSModeNoChecks(false);
			global::UIEventListener uieventListener = global::UIEventListener.Get(this._closeButton);
			global::UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new global::UIEventListener.VoidDelegate(this.OnCloseButtonClicked));
			global::UIEventListener uieventListener3 = global::UIEventListener.Get(this._optionsButton);
			global::UIEventListener uieventListener4 = uieventListener3;
			uieventListener4.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener4.onClick, new global::UIEventListener.VoidDelegate(this.OnOptionsButtonClicked));
			global::TweenColor component = this.fadeSprite.GetComponent<global::TweenColor>();
			component.eventReceiver = base.gameObject;
			component.callWhenFinished = "FadeFinished";
			if (this._onContextMenuVisible_ != null)
			{
				global::Context.OnClientWorking += this._onContextMenuVisible_;
			}
			this.UseHoverTextInitialize();
		}
		finally
		{
			this.awaking = false;
		}
		using (global::TempList<global::RPOSWindow> tempList = global::TempList<global::RPOSWindow>.New(global::RPOS.g_windows.allWindows))
		{
			foreach (global::RPOSWindow window in tempList)
			{
				global::RPOS.InitWindow(window);
			}
		}
	}

	// Token: 0x06002996 RID: 10646 RVA: 0x00099258 File Offset: 0x00097458
	private void OnDestroy()
	{
		if (this.unlocker != null)
		{
			this.unlocker.Dispose();
			this.unlocker = null;
		}
		if (this._onContextMenuVisible_ != null)
		{
			global::Context.OnClientWorking -= this._onContextMenuVisible_;
		}
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x00099290 File Offset: 0x00097490
	public void OnCloseButtonClicked(GameObject go)
	{
		this.SetRPOSMode(false);
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x0009929C File Offset: 0x0009749C
	public void OnOptionsButtonClicked(GameObject go)
	{
		global::RPOS.OpenOptions();
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x000992A4 File Offset: 0x000974A4
	[Obsolete("Use var player = RPOS.ObservedPlayer", true)]
	public global::Controllable GetObservedPlayer()
	{
		return this.observedPlayer;
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x000992AC File Offset: 0x000974AC
	public static void SetPlaqueActive(string plaqueName, bool on)
	{
		global::RPOS.g_RPOS._plaqueManager.SetPlaqueActive(plaqueName, on);
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x000992C0 File Offset: 0x000974C0
	public static global::RPOSItemRightClickMenu GetRightClickMenu()
	{
		return global::RPOS.g_RPOS.rightClickMenu;
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x000992CC File Offset: 0x000974CC
	[Obsolete("Use RPOS.ObservedPlayer = player")]
	public void SetObservedPlayer(global::Controllable player)
	{
		this.observedPlayer = player;
		global::RPOSWindow windowByName = global::RPOS.GetWindowByName("Inventory");
		if (windowByName)
		{
			global::RPOSInvCellManager componentInChildren = windowByName.GetComponentInChildren<global::RPOSInvCellManager>();
			componentInChildren.SetInventory(player.GetComponent<global::Inventory>(), false);
		}
		global::PlayerInventory component = player.GetComponent<global::PlayerInventory>();
		this._belt.CellIndexStart = 30;
		this._belt.SetInventory(component, false);
		global::RPOSWindow windowByName2 = global::RPOS.GetWindowByName("Armor");
		global::RPOSInvCellManager componentInChildren2 = windowByName2.GetComponentInChildren<global::RPOSInvCellManager>();
		componentInChildren2.CellIndexStart = 36;
		componentInChildren2.SetInventory(component, false);
		this.SetRPOSMode(false);
		global::RPOS.InjuryUpdate();
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x0009935C File Offset: 0x0009755C
	private void OnContextMenuVisible(bool visible)
	{
		this.forceHideUseHoverTextCaseContextMenu = visible;
		this.CheckUseHoverTextEnabled();
	}

	// Token: 0x0600299E RID: 10654 RVA: 0x0009936C File Offset: 0x0009756C
	[Obsolete("Use RPOS.Toggle()")]
	public void DoToggle()
	{
		this.SetRPOSMode(!this.RPOSOn);
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x00099380 File Offset: 0x00097580
	[Obsolete("Use RPOS.Hide()")]
	public void DoHide()
	{
		if (this.RPOSOn)
		{
			this.DoToggle();
		}
	}

	// Token: 0x060029A0 RID: 10656 RVA: 0x00099394 File Offset: 0x00097594
	[Obsolete("Use RPOS.Show()")]
	public void DoShow()
	{
		if (!this.RPOSOn)
		{
			this.DoToggle();
		}
	}

	// Token: 0x060029A1 RID: 10657 RVA: 0x000993A8 File Offset: 0x000975A8
	public static void ChangeRPOSMode(bool enable)
	{
		global::RPOS.g_RPOS.SetRPOSMode(enable);
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x000993B8 File Offset: 0x000975B8
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

	// Token: 0x060029A3 RID: 10659 RVA: 0x000993E0 File Offset: 0x000975E0
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
				using (global::TempList<global::RPOSWindow> tempList = global::TempList<global::RPOSWindow>.New(global::RPOS.g_windows.allWindows))
				{
					if (enable)
					{
						foreach (global::RPOSWindow rposwindow in tempList)
						{
							if (rposwindow)
							{
								rposwindow.RPOSOn();
							}
						}
					}
					foreach (global::RPOSWindow rposwindow2 in tempList)
					{
						if (rposwindow2)
						{
							rposwindow2.CheckDisplay();
						}
					}
					if (!enable)
					{
						foreach (global::RPOSWindow rposwindow3 in tempList)
						{
							if (rposwindow3)
							{
								rposwindow3.RPOSOff();
							}
						}
						this._clickedItemCell = null;
						global::GUIHeldItem guiheldItem = global::GUIHeldItem.Get();
						if (guiheldItem)
						{
							guiheldItem.ClearHeldItem();
						}
					}
				}
				this._bumper.GetComponent<global::UIPanel>().enabled = enable;
				global::UIPanel.Find(this._closeButton.transform).enabled = enable;
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
			global::ItemToolTip.SetToolTip(null, null);
			return;
		}
		if (enable != this.RPOSOn)
		{
			throw new InvalidOperationException((!enable) ? "You cannot turn OFF RPOS while its being turned ON-- check callstack" : "You cannot turn ON RPOS while its being turned OFF-- check callstack");
		}
	}

	// Token: 0x060029A4 RID: 10660 RVA: 0x00099654 File Offset: 0x00097854
	[Obsolete("Use RPOS.Item_CellReset()")]
	public void ItemCellReset()
	{
		if (this._clickedItemCell)
		{
			global::GUIHeldItem.Get().ClearHeldItem(this._clickedItemCell);
			this._clickedItemCell._displayInventory.MarkSlotDirty((int)this._clickedItemCell._mySlot);
		}
		else
		{
			global::GUIHeldItem.Get().ClearHeldItem();
		}
		this._clickedItemCell = null;
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x000996B4 File Offset: 0x000978B4
	[Obsolete("Use RPOS.Item_CellDragBegin()")]
	public void ItemCellDragBegin(global::RPOSInventoryCell cell)
	{
		this.ItemCellReset();
		this.ItemCellClicked(cell);
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x000996C4 File Offset: 0x000978C4
	[Obsolete("Use RPOS.Item_CellDragEnd()")]
	public void ItemCellDragEnd(global::RPOSInventoryCell begin, global::RPOSInventoryCell end)
	{
		if (end)
		{
			global::GUIHeldItem.Get().ClearHeldItem(end);
		}
		this.ItemCellReset();
		if (begin != end && end && begin)
		{
			this._clickedItemCell = begin;
			this.ItemCellClicked(end);
		}
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x00099720 File Offset: 0x00097920
	[Obsolete("Use RPOS.Item_CellDrop()")]
	public void ItemCellDrop(global::RPOSInventoryCell cell)
	{
		if (this._clickedItemCell != null)
		{
			this.ItemCellClicked(cell);
		}
	}

	// Token: 0x060029A8 RID: 10664 RVA: 0x0009973C File Offset: 0x0009793C
	public static void TossItem(byte slot)
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.DoTossItem(slot);
		}
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x00099758 File Offset: 0x00097958
	private void DoTossItem(byte slot)
	{
		global::InventoryHolder component = global::RPOS.ObservedPlayer.GetComponent<global::InventoryHolder>();
		if (component)
		{
			component.TossItem((int)slot);
		}
		global::GUIHeldItem.Get().ClearHeldItem();
	}

	// Token: 0x060029AA RID: 10666 RVA: 0x00099790 File Offset: 0x00097990
	public static void ItemCellAltClicked(global::RPOSInventoryCell cell)
	{
	}

	// Token: 0x060029AB RID: 10667 RVA: 0x00099794 File Offset: 0x00097994
	[Obsolete("Use RPOS.Item_CellClicked()")]
	public void ItemCellClicked(global::RPOSInventoryCell cell)
	{
		bool flag = false;
		byte b = 0;
		global::Inventory inventory = null;
		global::IInventoryItem inventoryItem = null;
		global::IInventoryItem inventoryItem2 = null;
		if (this._clickedItemCell != null)
		{
			inventory = this._clickedItemCell._displayInventory;
			b = this._clickedItemCell._mySlot;
			inventory.GetItem((int)b, out inventoryItem);
		}
		global::Inventory displayInventory = cell._displayInventory;
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
			global::NetEntityID fromInvID = global::NetEntityID.Get(inventory);
			global::NetEntityID toInvID = global::NetEntityID.Get(displayInventory);
			if (shift)
			{
				global::Inventory.ItemCombinePredicted(fromInvID, toInvID, (int)b, (int)mySlot);
			}
			else
			{
				global::Inventory.ItemMergePredicted(fromInvID, toInvID, (int)b, (int)mySlot);
			}
			inventoryItem = null;
			this._clickedItemCell = null;
		}
		else if (inventoryItem != null && inventoryItem2 == null)
		{
			global::NetEntityID toInvID2 = global::NetEntityID.Get(displayInventory);
			global::Inventory.ItemMovePredicted(global::NetEntityID.Get(inventory), toInvID2, (int)b, (int)mySlot);
			this._clickedItemCell = null;
			inventoryItem = null;
			flag = true;
		}
		if (inventoryItem != global::GUIHeldItem.CurrentItem())
		{
			if (inventoryItem != null)
			{
				if (!flag || !global::GUIHeldItem.Get().SetHeldItem(cell))
				{
					global::GUIHeldItem.Get().SetHeldItem(inventoryItem);
				}
			}
			else if (flag && cell)
			{
				global::GUIHeldItem.Get().ClearHeldItem(cell);
			}
			else
			{
				global::GUIHeldItem.Get().ClearHeldItem();
			}
		}
	}

	// Token: 0x060029AC RID: 10668 RVA: 0x00099940 File Offset: 0x00097B40
	public static void Item_CellReset()
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.ItemCellReset();
		}
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x0009995C File Offset: 0x00097B5C
	public static void Item_CellDrop(global::RPOSInventoryCell cell)
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.ItemCellDrop(cell);
		}
	}

	// Token: 0x060029AE RID: 10670 RVA: 0x00099978 File Offset: 0x00097B78
	public static void Item_CellDragEnd(global::RPOSInventoryCell begin, global::RPOSInventoryCell end)
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.ItemCellDragEnd(begin, end);
		}
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x00099998 File Offset: 0x00097B98
	public static void Item_CellDragBegin(global::RPOSInventoryCell begin)
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.ItemCellDragBegin(begin);
		}
	}

	// Token: 0x060029B0 RID: 10672 RVA: 0x000999B4 File Offset: 0x00097BB4
	public static bool Item_IsClickedCell(global::RPOSInventoryCell cell)
	{
		return global::RPOS.g_RPOS && global::RPOS.g_RPOS._clickedItemCell && global::RPOS.g_RPOS._clickedItemCell == cell;
	}

	// Token: 0x17000945 RID: 2373
	// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000999F8 File Offset: 0x00097BF8
	// (set) Token: 0x060029B2 RID: 10674 RVA: 0x00099A1C File Offset: 0x00097C1C
	public static global::Controllable ObservedPlayer
	{
		get
		{
			return (!global::RPOS.g_RPOS) ? null : global::RPOS.g_RPOS.observedPlayer;
		}
		set
		{
			if (global::RPOS.g_RPOS)
			{
				global::RPOS.g_RPOS.SetObservedPlayer(value);
			}
		}
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x00099A38 File Offset: 0x00097C38
	public static bool GetObservedPlayerComponent<TComponent>(out TComponent component) where TComponent : Component
	{
		if (global::RPOS.g_RPOS)
		{
			global::Controllable controllable = global::RPOS.g_RPOS.observedPlayer;
			if (controllable)
			{
				component = controllable.GetComponent<TComponent>();
				return component;
			}
		}
		component = (TComponent)((object)null);
		return false;
	}

	// Token: 0x060029B4 RID: 10676 RVA: 0x00099A94 File Offset: 0x00097C94
	public static bool IsObservedPlayer(global::Controllable controllable)
	{
		return global::RPOS.g_RPOS && controllable && global::RPOS.g_RPOS.observedPlayer == controllable;
	}

	// Token: 0x060029B5 RID: 10677 RVA: 0x00099AC4 File Offset: 0x00097CC4
	public static void HealthUpdate(float value)
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.UpdateHealth(value);
		}
	}

	// Token: 0x060029B6 RID: 10678 RVA: 0x00099AE0 File Offset: 0x00097CE0
	public static void Toggle()
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.DoToggle();
		}
	}

	// Token: 0x060029B7 RID: 10679 RVA: 0x00099AFC File Offset: 0x00097CFC
	public static void Hide()
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.DoHide();
		}
	}

	// Token: 0x060029B8 RID: 10680 RVA: 0x00099B18 File Offset: 0x00097D18
	public static void SetEquipmentDirty()
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.g_RPOS.EquipmentDirty();
		}
	}

	// Token: 0x17000946 RID: 2374
	// (get) Token: 0x060029B9 RID: 10681 RVA: 0x00099B34 File Offset: 0x00097D34
	public static bool Exists
	{
		get
		{
			return global::RPOS.g_RPOS;
		}
	}

	// Token: 0x060029BA RID: 10682 RVA: 0x00099B40 File Offset: 0x00097D40
	public static void OpenInfoWindow(global::ItemDataBlock itemdb)
	{
	}

	// Token: 0x060029BB RID: 10683 RVA: 0x00099B44 File Offset: 0x00097D44
	public static bool FocusListedWindow(string name)
	{
		if (!global::RPOS.g_RPOS)
		{
			return false;
		}
		if (global::RPOS.g_RPOS.forceHideInventory)
		{
			return false;
		}
		bool result = false;
		foreach (global::RPOSWindow rposwindow in global::RPOS.g_RPOS.windowList)
		{
			if (rposwindow && rposwindow.title == name)
			{
				if (!global::RPOS.g_RPOS.RPOSOn)
				{
					global::RPOS.g_RPOS.SetRPOSMode(true);
					if (!global::RPOS.g_RPOS.RPOSOn)
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

	// Token: 0x060029BC RID: 10684 RVA: 0x00099C24 File Offset: 0x00097E24
	public static bool FocusInventory()
	{
		return global::RPOS.FocusListedWindow("Inventory");
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x00099C30 File Offset: 0x00097E30
	public static bool FocusArmor()
	{
		return global::RPOS.FocusListedWindow("Armor");
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x00099C3C File Offset: 0x00097E3C
	public static void OpenLootWindow(global::LootableObject lootObj)
	{
		if (global::RPOS.g_RPOS)
		{
			global::RPOS.CloseWindowByName("Crafting");
			Vector3 localPosition = global::RPOS.g_RPOS.LootPanelPrefab.transform.localPosition;
			GameObject prefab;
			if (lootObj.lootWindowOverride)
			{
				prefab = lootObj.lootWindowOverride.gameObject;
			}
			else
			{
				prefab = global::RPOS.g_RPOS.LootPanelPrefab;
			}
			GameObject gameObject = global::NGUITools.AddChild(global::RPOS.g_RPOS.bottomCenterAnchor, prefab);
			gameObject.GetComponent<global::RPOSLootWindow>().SetLootable(lootObj, true);
			gameObject.transform.localPosition = localPosition;
			global::RPOS.BringToFront(gameObject.GetComponent<global::RPOSWindow>());
			global::RPOS.g_RPOS.SetRPOSMode(true);
		}
	}

	// Token: 0x060029BF RID: 10687 RVA: 0x00099CE8 File Offset: 0x00097EE8
	public static void OpenWorkbenchWindow(global::WorkBench workbenchObj)
	{
		if (global::RPOS.g_RPOS)
		{
			GameObject gameObject = global::NGUITools.AddChild(global::RPOS.g_RPOS.windowAnchor, global::RPOS.g_RPOS.WorkbenchPanelPrefab);
			gameObject.GetComponent<global::RPOSWorkbenchWindow>().SetWorkbench(workbenchObj);
			global::RPOS.BringToFront(gameObject.GetComponent<global::RPOSWindow>());
			global::RPOS.g_RPOS.SetRPOSMode(true);
		}
	}

	// Token: 0x060029C0 RID: 10688 RVA: 0x00099D44 File Offset: 0x00097F44
	public static void CloseWorkbenchWindow()
	{
		global::RPOS.CloseWindowByName("Workbench");
	}

	// Token: 0x060029C1 RID: 10689 RVA: 0x00099D50 File Offset: 0x00097F50
	public static void CloseLootWindow()
	{
		foreach (global::RPOSWindow rposwindow in global::RPOS.g_windows.allWindows)
		{
			if (rposwindow && rposwindow is global::RPOSLootWindow)
			{
				((global::RPOSLootWindow)rposwindow).LootClosed();
				break;
			}
		}
	}

	// Token: 0x060029C2 RID: 10690 RVA: 0x00099DD4 File Offset: 0x00097FD4
	[Obsolete("Use RPOS.SetEquipmentDirty()")]
	public void EquipmentDirty()
	{
		global::RPOSArmorWindow windowByName = global::RPOS.GetWindowByName<global::RPOSArmorWindow>("Armor");
		windowByName.ForceUpdate();
	}

	// Token: 0x060029C3 RID: 10691 RVA: 0x00099DF4 File Offset: 0x00097FF4
	[Obsolete("Use RPOS.HealthUpdate(amount)")]
	public void UpdateHealth(float amount)
	{
		this.healthLabel.text = amount.ToString("N0");
		this._healthProgress.sliderValue = Mathf.Clamp01(amount / 100f);
		global::UIFilledSprite component = this._healthProgress.foreground.GetComponent<global::UIFilledSprite>();
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

	// Token: 0x060029C4 RID: 10692 RVA: 0x00099E84 File Offset: 0x00098084
	public static void SetCurrentFade(Color col)
	{
		global::RPOS.g_RPOS.fadeSprite.color = col;
		global::TweenColor component = global::RPOS.g_RPOS.fadeSprite.GetComponent<global::TweenColor>();
		component.from = col;
		component.to = col;
		component.isFullscreen = true;
		global::RPOS.g_RPOS.fadeSprite.enabled = true;
	}

	// Token: 0x060029C5 RID: 10693 RVA: 0x00099ED8 File Offset: 0x000980D8
	public static void DoFadeNow(float duration, Color col)
	{
		global::RPOS.g_RPOS.DoFade(duration, col);
	}

	// Token: 0x060029C6 RID: 10694 RVA: 0x00099EE8 File Offset: 0x000980E8
	public static void DoFade(float delay, float duration, Color col)
	{
		if (delay <= 0f)
		{
			global::RPOS.DoFadeNow(duration, col);
		}
		else
		{
			global::RPOS.g_RPOS.nextFadeColor = col;
			global::RPOS.g_RPOS.nextFadeDuration = duration;
			global::RPOS.g_RPOS.Invoke("Internal_DoFade", delay);
		}
	}

	// Token: 0x060029C7 RID: 10695 RVA: 0x00099F28 File Offset: 0x00098128
	public void Internal_DoFade()
	{
		this.DoFade(this.nextFadeDuration, this.nextFadeColor);
	}

	// Token: 0x060029C8 RID: 10696 RVA: 0x00099F3C File Offset: 0x0009813C
	public void DoFade(float duration, Color col)
	{
		this.fadeSprite.enabled = true;
		global::TweenColor.Begin(this.fadeSprite.gameObject, duration, col);
	}

	// Token: 0x060029C9 RID: 10697 RVA: 0x00099F60 File Offset: 0x00098160
	public static void ClearFade()
	{
		global::RPOS.g_RPOS.fadeSprite.enabled = false;
		global::RPOS.g_RPOS.CancelInvoke("DoFade");
	}

	// Token: 0x060029CA RID: 10698 RVA: 0x00099F84 File Offset: 0x00098184
	public void FadeFinished()
	{
		if (this.fadeSprite.color.a == 0f)
		{
			this.fadeSprite.enabled = false;
		}
	}

	// Token: 0x060029CB RID: 10699 RVA: 0x00099FBC File Offset: 0x000981BC
	private void SceneUpdate(Camera camera)
	{
		this.UseHoverTextThink(camera);
	}

	// Token: 0x060029CC RID: 10700 RVA: 0x00099FC8 File Offset: 0x000981C8
	private void UIUpdate(global::UICamera camera)
	{
		this.UseHoverTextPostThink(camera.cachedCamera);
	}

	// Token: 0x060029CD RID: 10701 RVA: 0x00099FD8 File Offset: 0x000981D8
	private void Update()
	{
		global::HUDIndicator.Step();
		global::RPOSLimitFlags rposlimitFlags = this.currentLimitFlags;
		global::PlayerClient localPlayer = global::PlayerClient.GetLocalPlayer();
		if (localPlayer)
		{
			global::Controllable controllable = localPlayer.controllable;
			global::Controllable masterControllable;
			if (controllable && (masterControllable = controllable.masterControllable))
			{
				this.currentLimitFlags = masterControllable.rposLimitFlags;
			}
			else
			{
				this.currentLimitFlags = (global::RPOSLimitFlags.KeepOff | global::RPOSLimitFlags.HideInventory | global::RPOSLimitFlags.HideContext | global::RPOSLimitFlags.HideSprites);
			}
		}
		else
		{
			this.currentLimitFlags = (global::RPOSLimitFlags.KeepOff | global::RPOSLimitFlags.HideInventory | global::RPOSLimitFlags.HideContext | global::RPOSLimitFlags.HideSprites);
		}
		if (rposlimitFlags != this.currentLimitFlags)
		{
			global::RPOSLimitFlags rposlimitFlags2 = rposlimitFlags ^ this.currentLimitFlags;
			if ((rposlimitFlags2 & global::RPOSLimitFlags.HideContext) == global::RPOSLimitFlags.HideContext)
			{
				this.forceHideUseHoverTextCaseLimitFlags = ((this.currentLimitFlags & global::RPOSLimitFlags.HideContext) == global::RPOSLimitFlags.HideContext);
				this.CheckUseHoverTextEnabled();
			}
			if ((rposlimitFlags2 & global::RPOSLimitFlags.HideSprites) == global::RPOSLimitFlags.HideSprites)
			{
				this.forceHideSprites = ((this.currentLimitFlags & global::RPOSLimitFlags.HideSprites) == global::RPOSLimitFlags.HideSprites);
			}
			if ((rposlimitFlags2 & global::RPOSLimitFlags.HideInventory) == global::RPOSLimitFlags.HideInventory)
			{
				this.LimitInventory((this.currentLimitFlags & global::RPOSLimitFlags.HideInventory) == global::RPOSLimitFlags.HideInventory);
			}
			if ((rposlimitFlags2 & global::RPOSLimitFlags.KeepOff) == global::RPOSLimitFlags.KeepOff)
			{
				if ((this.currentLimitFlags & global::RPOSLimitFlags.KeepOff) == global::RPOSLimitFlags.KeepOff)
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
		if (global::RPOS.g_windows.orderChanged || height != this.lastScreenHeight || width != this.lastScreenWidth)
		{
			global::RPOS.g_windows.ProcessDepth(this.windowAnchor.transform);
			this.lastScreenHeight = height;
			this.lastScreenWidth = width;
		}
		if (global::RPOS.g_RPOS.observedPlayer)
		{
			global::RPOS.SetPlaqueActive("PlaqueWorkbench1", global::RPOS.g_RPOS.observedPlayer.GetComponent<global::CraftingInventory>().AtWorkBench());
		}
	}

	// Token: 0x060029CE RID: 10702 RVA: 0x0009A17C File Offset: 0x0009837C
	private void LimitInventory(bool limit)
	{
		this.forceHideInventory = limit;
		using (global::TempList<global::RPOSWindow> allWindows = global::RPOS.AllWindows)
		{
			bool bumpersEnabled = !limit;
			foreach (global::RPOSWindow rposwindow in allWindows)
			{
				if (rposwindow && rposwindow.isInventoryRelated)
				{
					rposwindow.bumpersEnabled = bumpersEnabled;
				}
			}
			foreach (global::RPOSWindow rposwindow2 in allWindows)
			{
				if (rposwindow2)
				{
					rposwindow2.inventoryHide = limit;
				}
			}
		}
		if (this._belt)
		{
			global::UIPanel component = this._belt.GetComponent<global::UIPanel>();
			component.enabled = !limit;
		}
	}

	// Token: 0x060029CF RID: 10703 RVA: 0x0009A2B8 File Offset: 0x000984B8
	public static void MetabolismUpdate()
	{
		global::RPOS.g_RPOS.DoMetabolismUpdate();
	}

	// Token: 0x060029D0 RID: 10704 RVA: 0x0009A2C4 File Offset: 0x000984C4
	public static void InjuryUpdate()
	{
		global::RPOS.g_RPOS.DoInjuryUpdate();
	}

	// Token: 0x060029D1 RID: 10705 RVA: 0x0009A2D0 File Offset: 0x000984D0
	private void DoInjuryUpdate()
	{
		global::FallDamage component = global::RPOS.ObservedPlayer.GetComponent<global::FallDamage>();
		this._plaqueManager.SetPlaqueActive("PlaqueInjury", component.GetLegInjury() > 0f);
	}

	// Token: 0x060029D2 RID: 10706 RVA: 0x0009A308 File Offset: 0x00098508
	private void ClearInjury()
	{
		this._plaqueManager.SetPlaqueActive("PlaqueInjury", false);
	}

	// Token: 0x060029D3 RID: 10707 RVA: 0x0009A31C File Offset: 0x0009851C
	private void DoMetabolismUpdate()
	{
		global::Metabolism component = global::RPOS.ObservedPlayer.GetComponent<global::Metabolism>();
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

	// Token: 0x060029D4 RID: 10708 RVA: 0x0009A438 File Offset: 0x00098638
	public static void SetActionProgress(bool show, string label, float progress)
	{
		if (show)
		{
			if (!string.IsNullOrEmpty(label))
			{
				global::RPOS.g_RPOS.actionLabel.text = label;
				global::RPOS.g_RPOS.actionLabel.enabled = true;
			}
			else
			{
				global::RPOS.g_RPOS.actionLabel.enabled = false;
			}
			global::RPOS.g_RPOS.actionProgress.sliderValue = progress;
			global::RPOS.g_RPOS.actionPanel.enabled = true;
		}
		else
		{
			global::RPOS.g_RPOS.actionPanel.enabled = false;
		}
	}

	// Token: 0x060029D5 RID: 10709 RVA: 0x0009A4C0 File Offset: 0x000986C0
	public static int GetIndex2D(int x, int y, int width)
	{
		return x + y * width;
	}

	// Token: 0x060029D6 RID: 10710 RVA: 0x0009A4C8 File Offset: 0x000986C8
	[Obsolete("Avoid using this", true)]
	public static global::RPOS Get()
	{
		return global::RPOS.g_RPOS;
	}

	// Token: 0x17000947 RID: 2375
	// (get) Token: 0x060029D7 RID: 10711 RVA: 0x0009A4D0 File Offset: 0x000986D0
	public static bool hideSprites
	{
		get
		{
			return global::RPOS.g_RPOS && (global::RPOS.g_RPOS.RPOSOn || global::RPOS.g_RPOS.forceHideSprites);
		}
	}

	// Token: 0x060029D8 RID: 10712 RVA: 0x0009A50C File Offset: 0x0009870C
	public static void OpenOptions()
	{
	}

	// Token: 0x060029D9 RID: 10713 RVA: 0x0009A510 File Offset: 0x00098710
	public static void CloseOptions()
	{
	}

	// Token: 0x060029DA RID: 10714 RVA: 0x0009A514 File Offset: 0x00098714
	public static void ToggleOptions()
	{
	}

	// Token: 0x060029DB RID: 10715 RVA: 0x0009A518 File Offset: 0x00098718
	public static void LocalInventoryModified()
	{
		global::RPOSWindow windowByName = global::RPOS.GetWindowByName("Crafting");
		global::RPOSCraftWindow component = windowByName.GetComponent<global::RPOSCraftWindow>();
		component.LocalInventoryModified();
		global::RPOS.SetPlaqueActive("PlaqueCrafting", global::RPOS.g_RPOS.observedPlayer.GetComponent<global::CraftingInventory>().isCrafting);
	}

	// Token: 0x040013FC RID: 5116
	public const global::RPOSLimitFlags kNoControllableLimitFlags = global::RPOSLimitFlags.KeepOff | global::RPOSLimitFlags.HideInventory | global::RPOSLimitFlags.HideContext | global::RPOSLimitFlags.HideSprites;

	// Token: 0x040013FD RID: 5117
	[NonSerialized]
	private readonly global::ContextClientWorkingCallback _onContextMenuVisible_;

	// Token: 0x040013FE RID: 5118
	[SerializeField]
	private global::UILabel _useHoverLabel;

	// Token: 0x040013FF RID: 5119
	[SerializeField]
	private global::UIPanel _useHoverPanel;

	// Token: 0x04001400 RID: 5120
	private global::Controllable lastUseHoverControllable;

	// Token: 0x04001401 RID: 5121
	private global::IContextRequestableText lastUseHoverText;

	// Token: 0x04001402 RID: 5122
	private global::IContextRequestableUpdatingText lastUseHoverUpdatingText;

	// Token: 0x04001403 RID: 5123
	private global::IContextRequestablePointText lastUseHoverPointText;

	// Token: 0x04001404 RID: 5124
	private Vector3 pointUseHoverOrigin;

	// Token: 0x04001405 RID: 5125
	private Plane pointUseHoverPlane;

	// Token: 0x04001406 RID: 5126
	private global::AABBox useHoverLabelBounds;

	// Token: 0x04001407 RID: 5127
	private string useHoverText;

	// Token: 0x04001408 RID: 5128
	private bool forceHideUseHoverTextCaseContextMenu;

	// Token: 0x04001409 RID: 5129
	private bool forceHideUseHoverTextCaseLimitFlags;

	// Token: 0x0400140A RID: 5130
	private bool queuedUseHoverText;

	// Token: 0x0400140B RID: 5131
	private bool useHoverTextUpdatable;

	// Token: 0x0400140C RID: 5132
	private bool useHoverTextPoint;

	// Token: 0x0400140D RID: 5133
	private bool useHoverTextPanelVisible;

	// Token: 0x0400140E RID: 5134
	private Vector3? useHoverTextScreenPoint;

	// Token: 0x0400140F RID: 5135
	public static global::RPOS g_RPOS;

	// Token: 0x04001410 RID: 5136
	public List<global::RPOSWindow> windowList;

	// Token: 0x04001411 RID: 5137
	public global::RPOSBumper _bumper;

	// Token: 0x04001412 RID: 5138
	public GameObject _closeButton;

	// Token: 0x04001413 RID: 5139
	public GameObject _optionsButton;

	// Token: 0x04001414 RID: 5140
	public global::RPOSInvCellManager _belt;

	// Token: 0x04001415 RID: 5141
	[NonSerialized]
	public global::RPOSInventoryCell _clickedItemCell;

	// Token: 0x04001416 RID: 5142
	private bool RPOSOn;

	// Token: 0x04001417 RID: 5143
	private bool forceOff;

	// Token: 0x04001418 RID: 5144
	private bool forceHideSprites;

	// Token: 0x04001419 RID: 5145
	private bool forceHideInventory;

	// Token: 0x0400141A RID: 5146
	private global::Controllable observedPlayer;

	// Token: 0x0400141B RID: 5147
	private UnlockCursorNode unlocker;

	// Token: 0x0400141C RID: 5148
	public GameObject windowAnchor;

	// Token: 0x0400141D RID: 5149
	public GameObject bottomCenterAnchor;

	// Token: 0x0400141E RID: 5150
	public GameObject LootPanelPrefab;

	// Token: 0x0400141F RID: 5151
	public GameObject WorkbenchPanelPrefab;

	// Token: 0x04001420 RID: 5152
	public GameObject InfoPanelPrefab;

	// Token: 0x04001421 RID: 5153
	public global::UISlider _healthProgress;

	// Token: 0x04001422 RID: 5154
	public global::UISlider _foodProgress;

	// Token: 0x04001423 RID: 5155
	public global::UILabel healthLabel;

	// Token: 0x04001424 RID: 5156
	public global::UISprite fadeSprite;

	// Token: 0x04001425 RID: 5157
	public global::UILabel calorieLabel;

	// Token: 0x04001426 RID: 5158
	public global::UILabel radLabel;

	// Token: 0x04001427 RID: 5159
	public global::UISprite radSprite;

	// Token: 0x04001428 RID: 5160
	public global::UIPanel actionPanel;

	// Token: 0x04001429 RID: 5161
	public global::UILabel actionLabel;

	// Token: 0x0400142A RID: 5162
	public global::UISlider actionProgress;

	// Token: 0x0400142B RID: 5163
	public global::RPOSItemRightClickMenu rightClickMenu;

	// Token: 0x0400142C RID: 5164
	public global::RPOSPlaqueManager _plaqueManager;

	// Token: 0x0400142D RID: 5165
	public global::UIPanel[] keepTop;

	// Token: 0x0400142E RID: 5166
	public global::UIPanel[] keepBottom;

	// Token: 0x0400142F RID: 5167
	[HideInInspector]
	public Color nextFadeColor;

	// Token: 0x04001430 RID: 5168
	[HideInInspector]
	public float nextFadeDuration;

	// Token: 0x04001431 RID: 5169
	private bool awaking;

	// Token: 0x04001432 RID: 5170
	private bool rposModeLock;

	// Token: 0x04001433 RID: 5171
	private global::RPOSLimitFlags currentLimitFlags;

	// Token: 0x04001434 RID: 5172
	private int lastScreenWidth;

	// Token: 0x04001435 RID: 5173
	private int lastScreenHeight;

	// Token: 0x020004C1 RID: 1217
	private static class g_windows
	{
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x0009A574 File Offset: 0x00098774
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x0009A5A8 File Offset: 0x000987A8
		public static global::RPOSWindow front
		{
			get
			{
				int count = global::RPOS.g_windows.allWindows.Count;
				return (count != 0) ? global::RPOS.g_windows.allWindows[count - 1] : null;
			}
			set
			{
				global::RPOS.g_windows.lastPropertySetSuccess = false;
				if (!value)
				{
					throw new ArgumentNullException();
				}
				if (value.zzz__index == -1)
				{
					throw new InvalidOperationException("The window was not awake");
				}
				int count = global::RPOS.g_windows.allWindows.Count;
				if (count == 0)
				{
					throw new InvalidOperationException("There definitely should have been a window in the list unless you passed a prefab here or didnt ensure awake.");
				}
				if (count == 1 || global::RPOS.g_windows.allWindows[count - 1] == value)
				{
					return;
				}
				for (int i = value.zzz__index; i < count - 1; i++)
				{
					global::RPOSWindow rposwindow = global::RPOS.g_windows.allWindows[i + 1];
					global::RPOS.g_windows.allWindows[i] = rposwindow;
					rposwindow.zzz__index = i;
				}
				global::RPOS.g_windows.allWindows[count - 1] = value;
				value.zzz__index = count - 1;
				global::RPOS.g_windows.orderChanged = true;
				global::RPOS.g_windows.lastPropertySetSuccess = true;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x0009A67C File Offset: 0x0009887C
		// (set) Token: 0x060029E0 RID: 10720 RVA: 0x0009A6AC File Offset: 0x000988AC
		public static global::RPOSWindow back
		{
			get
			{
				return (global::RPOS.g_windows.allWindows.Count != 0) ? global::RPOS.g_windows.allWindows[0] : null;
			}
			set
			{
				global::RPOS.g_windows.lastPropertySetSuccess = false;
				if (!value)
				{
					throw new ArgumentNullException();
				}
				if (value.zzz__index == -1)
				{
					throw new InvalidOperationException("The window was not awake");
				}
				int count = global::RPOS.g_windows.allWindows.Count;
				if (count == 0)
				{
					throw new InvalidOperationException("There definitely should have been a window in the list unless you passed a prefab here or didnt ensure awake.");
				}
				if (count == 1 || global::RPOS.g_windows.allWindows[0] == value)
				{
					return;
				}
				for (int i = value.zzz__index; i > 0; i--)
				{
					global::RPOSWindow rposwindow = global::RPOS.g_windows.allWindows[i - 1];
					global::RPOS.g_windows.allWindows[i] = rposwindow;
					rposwindow.zzz__index = i;
				}
				global::RPOS.g_windows.allWindows[0] = value;
				value.zzz__index = 0;
				global::RPOS.g_windows.orderChanged = true;
				global::RPOS.g_windows.lastPropertySetSuccess = true;
			}
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x0009A778 File Offset: 0x00098978
		public static bool MoveUp(global::RPOSWindow window)
		{
			if (!window)
			{
				throw new ArgumentNullException();
			}
			if (window.zzz__index == -1)
			{
				throw new InvalidOperationException("The window was not awake");
			}
			int count = global::RPOS.g_windows.allWindows.Count;
			if (count == 0)
			{
				throw new InvalidOperationException("There definitely should have been a window in the list unless you passed a prefab here or didnt ensure awake.");
			}
			if (count == 1 || global::RPOS.g_windows.allWindows[count - 1] == window)
			{
				return false;
			}
			global::RPOS.g_windows.allWindows.Reverse(window.zzz__index, 2);
			global::RPOS.g_windows.allWindows[window.zzz__index].zzz__index = window.zzz__index;
			window.zzz__index++;
			global::RPOS.g_windows.orderChanged = true;
			return true;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x0009A82C File Offset: 0x00098A2C
		public static bool MoveDown(global::RPOSWindow window)
		{
			if (!window)
			{
				throw new ArgumentNullException();
			}
			if (window.zzz__index == -1)
			{
				throw new InvalidOperationException("The window was not awake");
			}
			int count = global::RPOS.g_windows.allWindows.Count;
			if (count == 0)
			{
				throw new InvalidOperationException("There definitely should have been a window in the list unless you passed a prefab here or didnt ensure awake.");
			}
			if (count == 1 || global::RPOS.g_windows.allWindows[0] == window)
			{
				return false;
			}
			global::RPOS.g_windows.allWindows.Reverse(window.zzz__index - 1, 2);
			global::RPOS.g_windows.allWindows[window.zzz__index].zzz__index = window.zzz__index;
			window.zzz__index--;
			global::RPOS.g_windows.orderChanged = true;
			return true;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x0009A8E0 File Offset: 0x00098AE0
		private static void ProcessTransform(Transform transform, ref float z)
		{
			global::AABBox aabbox = global::NGUIMath.CalculateRelativeWidgetBounds(transform);
			Vector3 localPosition = transform.localPosition;
			localPosition.z = -(z + aabbox.max.z);
			z += aabbox.size.z;
			transform.localPosition = localPosition;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x0009A930 File Offset: 0x00098B30
		private static void ProcessTransform(ref Matrix4x4 toRoot, global::RPOSWindow window, ref float z, out Bounds bounds)
		{
			global::RPOS.g_windows.ProcessTransform(window.transform, ref z);
			Vector4 windowDimensions = window.windowDimensions;
			Matrix4x4 localToWorldMatrix = window.transform.localToWorldMatrix;
			bounds..ctor(toRoot.MultiplyPoint3x4(localToWorldMatrix.MultiplyPoint3x4(new Vector3(windowDimensions.x, windowDimensions.y, 0f))), Vector3.zero);
			bounds.Encapsulate(toRoot.MultiplyPoint3x4(localToWorldMatrix.MultiplyPoint3x4(new Vector3(windowDimensions.x, windowDimensions.y + windowDimensions.w, 0f))));
			bounds.Encapsulate(toRoot.MultiplyPoint3x4(localToWorldMatrix.MultiplyPoint3x4(new Vector3(windowDimensions.x + windowDimensions.z, windowDimensions.y, 0f))));
			bounds.Encapsulate(toRoot.MultiplyPoint3x4(localToWorldMatrix.MultiplyPoint3x4(new Vector3(windowDimensions.x + windowDimensions.z, windowDimensions.y + windowDimensions.w, 0f))));
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x0009AA30 File Offset: 0x00098C30
		public static void ProcessDepth(Transform uiRoot)
		{
			global::RPOS.g_windows.orderChanged = false;
			global::RPOS.g_windows.lastZ = 0f;
			global::UIPanel[] array = (!global::RPOS.g_RPOS) ? null : global::RPOS.g_RPOS.keepBottom;
			if (array != null)
			{
				for (int i = array.Length - 1; i >= 0; i--)
				{
					if (array[i])
					{
						global::RPOS.g_windows.ProcessTransform(array[i].transform, ref global::RPOS.g_windows.lastZ);
					}
				}
			}
			global::RPOS.g_windows.WindowRect[] array2 = new global::RPOS.g_windows.WindowRect[global::RPOS.g_windows.allWindows.Count];
			global::RPOS.g_windows.WindowRect a = default(global::RPOS.g_windows.WindowRect);
			Matrix4x4 worldToLocalMatrix = uiRoot.worldToLocalMatrix;
			int num = 0;
			foreach (global::RPOSWindow rposwindow in global::RPOS.g_windows.allWindows)
			{
				if (rposwindow)
				{
					Bounds bounds;
					global::RPOS.g_windows.ProcessTransform(ref worldToLocalMatrix, rposwindow, ref global::RPOS.g_windows.lastZ, out bounds);
					global::RPOS.g_windows.WindowRect windowRect = new global::RPOS.g_windows.WindowRect(bounds);
					if (a.empty)
					{
						a = windowRect;
					}
					else
					{
						a = new global::RPOS.g_windows.WindowRect(a, windowRect);
					}
					array2[num++] = windowRect;
				}
				else
				{
					array2[num++] = default(global::RPOS.g_windows.WindowRect);
				}
			}
			array = ((!global::RPOS.g_RPOS) ? null : global::RPOS.g_RPOS.keepTop);
			if (array != null)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j])
					{
						global::RPOS.g_windows.ProcessTransform(array[j].transform, ref global::RPOS.g_windows.lastZ);
					}
				}
			}
		}

		// Token: 0x04001436 RID: 5174
		public static List<global::RPOSWindow> allWindows = new List<global::RPOSWindow>();

		// Token: 0x04001437 RID: 5175
		public static bool orderChanged = false;

		// Token: 0x04001438 RID: 5176
		public static bool lastPropertySetSuccess = false;

		// Token: 0x04001439 RID: 5177
		public static float lastZ;

		// Token: 0x020004C2 RID: 1218
		private struct WindowRect
		{
			// Token: 0x060029E6 RID: 10726 RVA: 0x0009ABFC File Offset: 0x00098DFC
			public WindowRect(global::RPOS.g_windows.WindowRect a, global::RPOS.g_windows.WindowRect b)
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

			// Token: 0x060029E7 RID: 10727 RVA: 0x0009AD6C File Offset: 0x00098F6C
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

			// Token: 0x060029E8 RID: 10728 RVA: 0x0009ADD8 File Offset: 0x00098FD8
			public WindowRect(int x, int y, ushort width, ushort height)
			{
				this.x = x;
				this.y = y;
				this.width = width;
				this.height = height;
			}

			// Token: 0x060029E9 RID: 10729 RVA: 0x0009ADF8 File Offset: 0x00098FF8
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

			// Token: 0x1700094A RID: 2378
			// (get) Token: 0x060029EA RID: 10730 RVA: 0x0009AF40 File Offset: 0x00099140
			public bool empty
			{
				get
				{
					return this.width == 0 || this.height == 0;
				}
			}

			// Token: 0x1700094B RID: 2379
			// (get) Token: 0x060029EB RID: 10731 RVA: 0x0009AF5C File Offset: 0x0009915C
			public int left
			{
				get
				{
					return this.x;
				}
			}

			// Token: 0x1700094C RID: 2380
			// (get) Token: 0x060029EC RID: 10732 RVA: 0x0009AF64 File Offset: 0x00099164
			public int right
			{
				get
				{
					return this.x + (int)this.width;
				}
			}

			// Token: 0x1700094D RID: 2381
			// (get) Token: 0x060029ED RID: 10733 RVA: 0x0009AF74 File Offset: 0x00099174
			public int top
			{
				get
				{
					return this.y;
				}
			}

			// Token: 0x1700094E RID: 2382
			// (get) Token: 0x060029EE RID: 10734 RVA: 0x0009AF7C File Offset: 0x0009917C
			public int bottom
			{
				get
				{
					return this.y + (int)this.height;
				}
			}

			// Token: 0x1700094F RID: 2383
			// (get) Token: 0x060029EF RID: 10735 RVA: 0x0009AF8C File Offset: 0x0009918C
			public int center
			{
				get
				{
					return this.x + (int)(this.width / 2);
				}
			}

			// Token: 0x17000950 RID: 2384
			// (get) Token: 0x060029F0 RID: 10736 RVA: 0x0009AFA0 File Offset: 0x000991A0
			public int middle
			{
				get
				{
					return this.y + (int)(this.height / 2);
				}
			}

			// Token: 0x060029F1 RID: 10737 RVA: 0x0009AFB4 File Offset: 0x000991B4
			public bool Contains(global::RPOS.g_windows.WindowRect other)
			{
				return ((this.x >= other.x) ? (this.x == other.x && other.width < this.width) : (other.x + (int)other.width - this.x <= (int)this.width)) && ((this.y >= other.y) ? (this.y == other.y && other.height < this.height) : (other.y + (int)other.height - this.y <= (int)this.height));
			}

			// Token: 0x060029F2 RID: 10738 RVA: 0x0009B084 File Offset: 0x00099284
			public bool ContainsOrEquals(global::RPOS.g_windows.WindowRect other)
			{
				return ((other.x != this.x) ? (this.x < other.x && other.x + (int)other.width - this.x <= (int)this.width) : (other.width <= this.width)) && ((other.y != this.y) ? (this.y < other.y && other.y + (int)other.height - this.y <= (int)this.height) : (other.height <= this.height));
			}

			// Token: 0x060029F3 RID: 10739 RVA: 0x0009B158 File Offset: 0x00099358
			public bool Equals(global::RPOS.g_windows.WindowRect other)
			{
				return this.width == other.width && this.x == other.x && this.y == other.y && this.height == other.height;
			}

			// Token: 0x060029F4 RID: 10740 RVA: 0x0009B1B0 File Offset: 0x000993B0
			public bool Overlaps(global::RPOS.g_windows.WindowRect other)
			{
				return ((other.x >= this.x) ? (this.x - other.x + (int)this.width > 0) : (other.x + (int)other.width > this.x)) && ((other.y >= this.y) ? (this.y - other.y + (int)this.height > 0) : (other.y + (int)other.height > this.y));
			}

			// Token: 0x060029F5 RID: 10741 RVA: 0x0009B254 File Offset: 0x00099454
			public bool OverlapsOrTouches(global::RPOS.g_windows.WindowRect other)
			{
				return (other.x == this.x || ((other.x >= this.x) ? (this.x - other.x + (int)this.width >= 0) : (other.x + (int)other.width >= this.x))) && (other.y == this.y || ((other.y >= this.y) ? (this.y - other.y + (int)this.height >= 0) : (other.y + (int)other.height >= this.y)));
			}

			// Token: 0x060029F6 RID: 10742 RVA: 0x0009B328 File Offset: 0x00099528
			public bool OverlapsOrOutside(global::RPOS.g_windows.WindowRect other)
			{
				return other.x < this.x || other.y < this.y || this.x - other.x + (int)other.width > (int)this.width || this.y - other.y + (int)this.height > (int)this.height;
			}

			// Token: 0x060029F7 RID: 10743 RVA: 0x0009B39C File Offset: 0x0009959C
			public bool OverlapsTouchesOrOutside(global::RPOS.g_windows.WindowRect other)
			{
				return other.x <= this.x || other.y <= this.y || this.x - other.x + (int)other.width >= (int)this.width || this.y - other.y + (int)this.height >= (int)this.height;
			}

			// Token: 0x060029F8 RID: 10744 RVA: 0x0009B414 File Offset: 0x00099614
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

			// Token: 0x0400143A RID: 5178
			public int x;

			// Token: 0x0400143B RID: 5179
			public int y;

			// Token: 0x0400143C RID: 5180
			public ushort width;

			// Token: 0x0400143D RID: 5181
			public ushort height;
		}
	}
}
