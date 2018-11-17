using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class RPOSWindow : MonoBehaviour
{
	// Token: 0x060026DB RID: 9947 RVA: 0x00097D74 File Offset: 0x00095F74
	public RPOSWindow()
	{
		this.buttonCallback = new UIEventListener.VoidDelegate(this.ButtonClickCallback);
	}

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x060026DC RID: 9948 RVA: 0x00097DC8 File Offset: 0x00095FC8
	// (remove) Token: 0x060026DD RID: 9949 RVA: 0x00097DD4 File Offset: 0x00095FD4
	public event RPOSWindowMessageHandler WillOpen
	{
		add
		{
			this.AddMessageHandler(RPOSWindowMessage.WillOpen, value);
		}
		remove
		{
			this.RemoveMessageHandler(RPOSWindowMessage.WillOpen, value);
		}
	}

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x060026DE RID: 9950 RVA: 0x00097DE0 File Offset: 0x00095FE0
	// (remove) Token: 0x060026DF RID: 9951 RVA: 0x00097DEC File Offset: 0x00095FEC
	public event RPOSWindowMessageHandler DidOpen
	{
		add
		{
			this.AddMessageHandler(RPOSWindowMessage.DidOpen, value);
		}
		remove
		{
			this.RemoveMessageHandler(RPOSWindowMessage.DidOpen, value);
		}
	}

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x060026E0 RID: 9952 RVA: 0x00097DF8 File Offset: 0x00095FF8
	// (remove) Token: 0x060026E1 RID: 9953 RVA: 0x00097E04 File Offset: 0x00096004
	public event RPOSWindowMessageHandler WillShow
	{
		add
		{
			this.AddMessageHandler(RPOSWindowMessage.WillShow, value);
		}
		remove
		{
			this.RemoveMessageHandler(RPOSWindowMessage.WillShow, value);
		}
	}

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x060026E2 RID: 9954 RVA: 0x00097E10 File Offset: 0x00096010
	// (remove) Token: 0x060026E3 RID: 9955 RVA: 0x00097E1C File Offset: 0x0009601C
	public event RPOSWindowMessageHandler DidShow
	{
		add
		{
			this.AddMessageHandler(RPOSWindowMessage.DidShow, value);
		}
		remove
		{
			this.RemoveMessageHandler(RPOSWindowMessage.DidShow, value);
		}
	}

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x060026E4 RID: 9956 RVA: 0x00097E28 File Offset: 0x00096028
	// (remove) Token: 0x060026E5 RID: 9957 RVA: 0x00097E34 File Offset: 0x00096034
	public event RPOSWindowMessageHandler WillHide
	{
		add
		{
			this.AddMessageHandler(RPOSWindowMessage.WillHide, value);
		}
		remove
		{
			this.RemoveMessageHandler(RPOSWindowMessage.WillHide, value);
		}
	}

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x060026E6 RID: 9958 RVA: 0x00097E40 File Offset: 0x00096040
	// (remove) Token: 0x060026E7 RID: 9959 RVA: 0x00097E4C File Offset: 0x0009604C
	public event RPOSWindowMessageHandler DidHide
	{
		add
		{
			this.AddMessageHandler(RPOSWindowMessage.DidHide, value);
		}
		remove
		{
			this.RemoveMessageHandler(RPOSWindowMessage.DidHide, value);
		}
	}

	// Token: 0x1400001E RID: 30
	// (add) Token: 0x060026E8 RID: 9960 RVA: 0x00097E58 File Offset: 0x00096058
	// (remove) Token: 0x060026E9 RID: 9961 RVA: 0x00097E64 File Offset: 0x00096064
	public event RPOSWindowMessageHandler WillClose
	{
		add
		{
			this.AddMessageHandler(RPOSWindowMessage.WillClose, value);
		}
		remove
		{
			this.RemoveMessageHandler(RPOSWindowMessage.WillClose, value);
		}
	}

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x060026EA RID: 9962 RVA: 0x00097E70 File Offset: 0x00096070
	// (remove) Token: 0x060026EB RID: 9963 RVA: 0x00097E7C File Offset: 0x0009607C
	public event RPOSWindowMessageHandler DidClose
	{
		add
		{
			this.AddMessageHandler(RPOSWindowMessage.DidClose, value);
		}
		remove
		{
			this.RemoveMessageHandler(RPOSWindowMessage.DidClose, value);
		}
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x00097E88 File Offset: 0x00096088
	private void FireEvent(RPOSWindowMessage message)
	{
		this.messageCenter.Fire(this, message);
	}

	// Token: 0x060026ED RID: 9965 RVA: 0x00097E98 File Offset: 0x00096098
	public bool AddMessageHandler(RPOSWindowMessage message, RPOSWindowMessageHandler handler)
	{
		return !this._destroyed && !this._lock_destroy && !this._destroyAfterAwake && (this._awake || this._lock_awake) && this.messageCenter.Add(message, handler);
	}

	// Token: 0x060026EE RID: 9966 RVA: 0x00097EEC File Offset: 0x000960EC
	public bool RemoveMessageHandler(RPOSWindowMessage message, RPOSWindowMessageHandler handler)
	{
		return (!this._awake || this._destroyed) && this.messageCenter.Remove(message, handler);
	}

	// Token: 0x060026EF RID: 9967 RVA: 0x00097F14 File Offset: 0x00096114
	[Obsolete("Use WindowAwake", true)]
	protected void Awake()
	{
		this._EnsureAwake();
	}

	// Token: 0x060026F0 RID: 9968 RVA: 0x00097F1C File Offset: 0x0009611C
	[Obsolete("Forwarder to SubTouch with SubTouchKind.Press if true else SubTouchKind.Release", true)]
	protected void OnChildPress(bool press)
	{
		this.SubTouch(UICamera.Cursor.Buttons.LeftValue.Pressed, (!press) ? RPOSWindow.SubTouchKind.Release : RPOSWindow.SubTouchKind.Press);
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x00097F48 File Offset: 0x00096148
	[Obsolete("Forwarder to SubTouch with SubTouchKind.Click", true)]
	protected void OnChildClick(GameObject go)
	{
		this.SubTouch(go, RPOSWindow.SubTouchKind.Click);
	}

	// Token: 0x060026F2 RID: 9970 RVA: 0x00097F54 File Offset: 0x00096154
	[Obsolete("Forwarder to SubTouch with SubTouchKind.ClickCancel", true)]
	protected void OnChildClickMissed(GameObject go)
	{
		this.SubTouch(go, RPOSWindow.SubTouchKind.ClickCancel);
	}

	// Token: 0x060026F3 RID: 9971 RVA: 0x00097F60 File Offset: 0x00096160
	[Obsolete("Use WindowDestroy", true)]
	protected void OnDestroy()
	{
		if (this._awake)
		{
			this._EnsureDestroy();
		}
	}

	// Token: 0x060026F4 RID: 9972 RVA: 0x00097F74 File Offset: 0x00096174
	private void _EnsureAwake()
	{
		if (!this._awake)
		{
			if (this._lock_awake)
			{
				Debug.LogWarning("Something tried to ensure this while it was being awoken in ensure awake", this);
				return;
			}
			if (this._destroyed)
			{
				Debug.LogWarning("This window was destroyed before it could be awoke", this);
			}
			else if (this._lock_destroy)
			{
				Debug.LogWarning("This window is in the process of being destroyed, please look at the call stack and avoid this", this);
			}
			else
			{
				try
				{
					this._lock_awake = true;
					this._myPanel = base.GetComponent<UIPanel>();
					this.panelsEnabled = false;
					if (this._closeButton)
					{
						this.closeButtonListener = UIEventListener.Get(this._closeButton.gameObject);
						if (this.closeButtonListener)
						{
							UIEventListener uieventListener = this.closeButtonListener;
							uieventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener.onClick, this.buttonCallback);
						}
					}
					this.WindowAwake();
				}
				catch (Exception arg)
				{
					Debug.LogError(string.Format("A exception was thrown during window awake ({0}, title={1}) and has probably broken something, exception is below\r\n{2}", this, this.TitleText, arg), this);
				}
				finally
				{
					this._awake = true;
					this._lock_awake = false;
					if (this._destroyAfterAwake)
					{
						Debug.LogWarning("Because of something trying to destroy this while we were awaking, destroy will occur now", this);
						try
						{
							this._lock_destroy = true;
							this.WindowDestroy();
						}
						catch (Exception arg2)
						{
							Debug.LogError(string.Format("A exception was thrown during window destroy following awake. ({0}, title={1}) and potentially screwed up stuff, exception is below\r\n{2}", this, this.TitleText, arg2), this);
						}
						finally
						{
							this._destroyed = true;
							this._lock_destroy = false;
						}
					}
					else
					{
						RPOS.RegisterWindow(this);
					}
				}
			}
		}
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x00098148 File Offset: 0x00096348
	private void _EnsureDestroy()
	{
		if (this._awake)
		{
			if (this._lock_destroy)
			{
				Debug.LogWarning("Something tried to destroy while this window was destroying", this);
			}
			else
			{
				try
				{
					this._lock_destroy = true;
					if (this.closeButtonListener)
					{
						UIEventListener uieventListener = this.closeButtonListener;
						uieventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uieventListener.onClick, this.buttonCallback);
					}
					if (!this._closed)
					{
						this._showWithRPOS = false;
						this._showWithoutRPOS = false;
						this.CheckDisplay();
						if (this._opened && !this._closed)
						{
							this.WindowClose();
						}
					}
					this.WindowDestroy();
				}
				catch (Exception arg)
				{
					Debug.LogError(string.Format("A exception was thrown during window destroy ({0}, title={1}) and potentially screwed up stuff, exception is below\r\n{2}", this, this.TitleText, arg), this);
				}
				finally
				{
					this._destroyed = true;
					this._lock_destroy = false;
					RPOS.UnregisterWindow(this);
				}
			}
		}
		else if (this._lock_awake)
		{
			Debug.LogWarning("This window was awakening.. the call to destroy will happen when its done. Look at call stack. Avoid this.", this);
			this._destroyAfterAwake = true;
		}
		else if (!this._lock_destroy)
		{
			this._lock_destroy = true;
			Debug.LogWarning("This window is being destroyed, and has never got it's Awake.", this);
		}
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x000982A8 File Offset: 0x000964A8
	[Obsolete("For use by RPOS only!")]
	internal void RPOSReady()
	{
		if (!this.neverAutoShow)
		{
			if (this.autoShowWithRPOS)
			{
				this._showWithRPOS = true;
			}
			if (this.autoShowWithoutRPOS)
			{
				this._showWithoutRPOS = true;
			}
		}
		if (RPOS.IsOpen)
		{
			Debug.Log("Was ready");
			this.RPOSOn();
		}
		this.CheckDisplay();
	}

	// Token: 0x060026F7 RID: 9975 RVA: 0x00098308 File Offset: 0x00096508
	[Obsolete("For use by RPOS only!")]
	internal void RPOSOn()
	{
		this.OnRPOSOpened();
	}

	// Token: 0x060026F8 RID: 9976 RVA: 0x00098310 File Offset: 0x00096510
	[Obsolete("For use by RPOS only!")]
	internal void RPOSOff()
	{
		this.OnRPOSClosed();
	}

	// Token: 0x060026F9 RID: 9977 RVA: 0x00098318 File Offset: 0x00096518
	[Obsolete("For use by RPOS only!")]
	internal bool CheckDisplay()
	{
		if (this._lock_show)
		{
			return false;
		}
		if (!this._showing)
		{
			if ((!this._showWithoutRPOS && (!this._showWithRPOS || !RPOS.IsOpen)) || (this._inventoryHide && this._isInventoryRelated))
			{
				return false;
			}
			this._showing = true;
			this.WindowShow();
		}
		else
		{
			if (!this._forceHide && (this._showWithoutRPOS || (this._showWithRPOS && RPOS.IsOpen)) && (!this._inventoryHide || !this._isInventoryRelated))
			{
				return false;
			}
			this._showing = false;
			this.WindowHide();
		}
		return true;
	}

	// Token: 0x060026FA RID: 9978 RVA: 0x000983E4 File Offset: 0x000965E4
	public static void EnsureAwake(RPOSWindow window)
	{
		window._EnsureAwake();
	}

	// Token: 0x060026FB RID: 9979 RVA: 0x000983EC File Offset: 0x000965EC
	private static bool GameObjectEqual(Object A, Object B)
	{
		if (!A)
		{
			return !B;
		}
		if (!B)
		{
			return false;
		}
		if (A is GameObject)
		{
			if (B is GameObject)
			{
				return A == B;
			}
			return B is Component && (GameObject)A == ((Component)B).gameObject;
		}
		else
		{
			if (!(A is Component))
			{
				return false;
			}
			if (B is GameObject)
			{
				return ((Component)A).gameObject == (GameObject)B;
			}
			return B is Component && ((Component)A).gameObject == ((Component)B).gameObject;
		}
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x000984BC File Offset: 0x000966BC
	private void ButtonClickCallback(GameObject button)
	{
		if (RPOSWindow.GameObjectEqual(button, this._closeButton))
		{
			this.CloseButtonClicked();
		}
		else if (this.bumpers != null)
		{
			int count = this.bumpers.Count;
			if (count > 0 && button)
			{
				UIButton component = button.GetComponent<UIButton>();
				if (component)
				{
					for (int i = 0; i < count; i++)
					{
						if (component == this.bumpers[i].button)
						{
							this.OnBumperClick(this.bumpers[i]);
							return;
						}
					}
				}
			}
		}
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x00098564 File Offset: 0x00096764
	private void HideOrClose(bool hideIsTrue)
	{
		if (hideIsTrue)
		{
			this.Hide();
		}
		else
		{
			this.WindowClose();
		}
	}

	// Token: 0x170008F0 RID: 2288
	// (set) Token: 0x060026FE RID: 9982 RVA: 0x00098580 File Offset: 0x00096780
	private bool panelsEnabled
	{
		set
		{
			if (this._myPanel)
			{
				this._myPanel.enabled = value;
			}
			if (this.childPanels != null)
			{
				foreach (UIPanel uipanel in this.childPanels)
				{
					if (uipanel)
					{
						uipanel.enabled = value;
					}
				}
			}
		}
	}

	// Token: 0x060026FF RID: 9983 RVA: 0x000985E8 File Offset: 0x000967E8
	private void WindowShow()
	{
		if (this._lock_show)
		{
			throw new InvalidOperationException("The window was already in the process of showing or hiding");
		}
		if (!this._opened)
		{
			this.WindowOpen();
		}
		try
		{
			this._lock_show = true;
			this.FireEvent(RPOSWindowMessage.WillShow);
			this.OnWindowShow();
			this.FireEvent(RPOSWindowMessage.DidShow);
		}
		finally
		{
			this._lock_show = false;
		}
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x00098660 File Offset: 0x00096860
	private void WindowHide()
	{
		if (this._lock_show)
		{
			throw new InvalidOperationException("The window was already in the process of showing or hiding");
		}
		try
		{
			this._lock_show = true;
			this.FireEvent(RPOSWindowMessage.WillHide);
			this.OnWindowHide();
			this.FireEvent(RPOSWindowMessage.DidHide);
		}
		finally
		{
			this._lock_show = false;
		}
	}

	// Token: 0x06002701 RID: 9985 RVA: 0x000986C8 File Offset: 0x000968C8
	private void WindowClose()
	{
		if (this._closed || this._lock_close)
		{
			return;
		}
		if (this._lock_open)
		{
			throw new InvalidOperationException("cannot close while opening -- check call stack.");
		}
		try
		{
			this._lock_close = true;
			this._forceHide = true;
			if (this._showing)
			{
				this.CheckDisplay();
			}
			if (this._opened)
			{
				this.FireEvent(RPOSWindowMessage.WillClose);
				this._closed = true;
				this.OnWindowClosed();
				this._opened = false;
				this.FireEvent(RPOSWindowMessage.DidClose);
			}
		}
		finally
		{
			this._lock_close = false;
		}
		if (this.destroyWithClose && !this._lock_destroy && !this._destroyed && !this._destroyAfterAwake)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06002702 RID: 9986 RVA: 0x000987B0 File Offset: 0x000969B0
	private void WindowOpen()
	{
		if (this._opened || this._lock_open)
		{
			return;
		}
		if (this._lock_close)
		{
			throw new InvalidOperationException("cannot open while closing -- check call stack.");
		}
		try
		{
			this._lock_open = true;
			bool closed = this._closed;
			this.FireEvent(RPOSWindowMessage.WillOpen);
			this._opened = true;
			this._closed = false;
			if (closed)
			{
				this.OnWindowReOpen();
			}
			else
			{
				this.OnWindowOpened();
			}
			this.FireEvent(RPOSWindowMessage.DidOpen);
		}
		finally
		{
			this._lock_open = false;
		}
		if (!this._lock_show)
		{
			this.CheckDisplay();
		}
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x00098868 File Offset: 0x00096A68
	internal void AddBumper(RPOSBumper.Instance inst)
	{
		inst.label.text = this.title;
		UIEventListener listener = inst.listener;
		if (listener)
		{
			UIEventListener uieventListener = listener;
			uieventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener.onClick, this.buttonCallback);
			if (this.bumpers == null)
			{
				this.bumpers = new List<RPOSBumper.Instance>();
			}
			this.bumpers.Add(inst);
		}
	}

	// Token: 0x06002704 RID: 9988 RVA: 0x000988D8 File Offset: 0x00096AD8
	internal void RemoveBumper(RPOSBumper.Instance inst)
	{
		if (this.bumpers != null && this.bumpers.Remove(inst))
		{
			UIEventListener listener = inst.listener;
			if (listener)
			{
				UIEventListener uieventListener = listener;
				uieventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uieventListener.onClick, this.buttonCallback);
			}
		}
	}

	// Token: 0x170008F1 RID: 2289
	// (get) Token: 0x06002705 RID: 9989 RVA: 0x00098930 File Offset: 0x00096B30
	// (set) Token: 0x06002706 RID: 9990 RVA: 0x00098938 File Offset: 0x00096B38
	internal bool inventoryHide
	{
		get
		{
			return this._inventoryHide;
		}
		set
		{
			if (this._inventoryHide != value)
			{
				this._inventoryHide = value;
				if (this._isInventoryRelated && this.ready)
				{
					this.CheckDisplay();
				}
			}
		}
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x00098978 File Offset: 0x00096B78
	internal void zzz___INTERNAL_FOCUS()
	{
		if (!this.showWithRPOS)
		{
			this.showWithRPOS = true;
		}
		this.BringToFront();
	}

	// Token: 0x170008F2 RID: 2290
	// (get) Token: 0x06002708 RID: 9992 RVA: 0x00098994 File Offset: 0x00096B94
	public Vector4 windowDimensions
	{
		get
		{
			return this._windowDimensions;
		}
	}

	// Token: 0x170008F3 RID: 2291
	// (get) Token: 0x06002709 RID: 9993 RVA: 0x0009899C File Offset: 0x00096B9C
	public UIWidget.Pivot shrinkPivot
	{
		get
		{
			return this._shrinkPivot;
		}
	}

	// Token: 0x170008F4 RID: 2292
	// (get) Token: 0x0600270A RID: 9994 RVA: 0x000989A4 File Offset: 0x00096BA4
	// (set) Token: 0x0600270B RID: 9995 RVA: 0x000989AC File Offset: 0x00096BAC
	public UILabel titleObj
	{
		get
		{
			return this._titleObj;
		}
		private set
		{
			this._titleObj = value;
		}
	}

	// Token: 0x170008F5 RID: 2293
	// (get) Token: 0x0600270C RID: 9996 RVA: 0x000989B8 File Offset: 0x00096BB8
	// (set) Token: 0x0600270D RID: 9997 RVA: 0x000989C0 File Offset: 0x00096BC0
	public UIButton closeButton
	{
		get
		{
			return this._closeButton;
		}
		private set
		{
			this._closeButton = value;
		}
	}

	// Token: 0x170008F6 RID: 2294
	// (get) Token: 0x0600270E RID: 9998 RVA: 0x000989CC File Offset: 0x00096BCC
	// (set) Token: 0x0600270F RID: 9999 RVA: 0x000989D4 File Offset: 0x00096BD4
	public UIPanel mainPanel
	{
		get
		{
			return this._myPanel;
		}
		private set
		{
			this._myPanel = value;
		}
	}

	// Token: 0x170008F7 RID: 2295
	// (get) Token: 0x06002710 RID: 10000 RVA: 0x000989E0 File Offset: 0x00096BE0
	// (set) Token: 0x06002711 RID: 10001 RVA: 0x000989E8 File Offset: 0x00096BE8
	public GameObject background
	{
		get
		{
			return this._background;
		}
		private set
		{
			this._background = value;
		}
	}

	// Token: 0x170008F8 RID: 2296
	// (get) Token: 0x06002712 RID: 10002 RVA: 0x000989F4 File Offset: 0x00096BF4
	// (set) Token: 0x06002713 RID: 10003 RVA: 0x000989FC File Offset: 0x00096BFC
	public GameObject dragger
	{
		get
		{
			return this._dragger;
		}
		private set
		{
			this._dragger = value;
		}
	}

	// Token: 0x170008F9 RID: 2297
	// (get) Token: 0x06002714 RID: 10004 RVA: 0x00098A08 File Offset: 0x00096C08
	public bool open
	{
		get
		{
			return this._opened && !this._closed;
		}
	}

	// Token: 0x170008FA RID: 2298
	// (get) Token: 0x06002715 RID: 10005 RVA: 0x00098A24 File Offset: 0x00096C24
	public bool closed
	{
		get
		{
			return this._closed && !this._opened;
		}
	}

	// Token: 0x170008FB RID: 2299
	// (get) Token: 0x06002716 RID: 10006 RVA: 0x00098A40 File Offset: 0x00096C40
	public bool showing
	{
		get
		{
			return this._showing;
		}
	}

	// Token: 0x170008FC RID: 2300
	// (get) Token: 0x06002717 RID: 10007 RVA: 0x00098A48 File Offset: 0x00096C48
	// (set) Token: 0x06002718 RID: 10008 RVA: 0x00098A60 File Offset: 0x00096C60
	public bool showWithRPOS
	{
		get
		{
			return !this._forceHide && this._showWithRPOS;
		}
		protected set
		{
			if (value != this._showWithRPOS)
			{
				this._showWithRPOS = value;
				this.CheckDisplay();
			}
		}
	}

	// Token: 0x170008FD RID: 2301
	// (get) Token: 0x06002719 RID: 10009 RVA: 0x00098A7C File Offset: 0x00096C7C
	public bool showingWithRPOS
	{
		get
		{
			return this._showing && RPOS.IsOpen;
		}
	}

	// Token: 0x170008FE RID: 2302
	// (get) Token: 0x0600271A RID: 10010 RVA: 0x00098A94 File Offset: 0x00096C94
	// (set) Token: 0x0600271B RID: 10011 RVA: 0x00098AAC File Offset: 0x00096CAC
	public bool showWithoutRPOS
	{
		get
		{
			return !this._forceHide && this._showWithoutRPOS;
		}
		protected set
		{
			if (value != this._showWithoutRPOS)
			{
				this._showWithoutRPOS = value;
				this.CheckDisplay();
			}
		}
	}

	// Token: 0x170008FF RID: 2303
	// (get) Token: 0x0600271C RID: 10012 RVA: 0x00098AC8 File Offset: 0x00096CC8
	public bool showingWithoutRPOS
	{
		get
		{
			return this._showing && !RPOS.IsOpen;
		}
	}

	// Token: 0x17000900 RID: 2304
	// (get) Token: 0x0600271D RID: 10013 RVA: 0x00098AE0 File Offset: 0x00096CE0
	// (set) Token: 0x0600271E RID: 10014 RVA: 0x00098AE8 File Offset: 0x00096CE8
	public string title
	{
		get
		{
			return this.TitleText;
		}
		set
		{
			if (value != null && !string.Equals(this.TitleText, value))
			{
				this.SetWindowTitle(value);
			}
		}
	}

	// Token: 0x17000901 RID: 2305
	// (get) Token: 0x0600271F RID: 10015 RVA: 0x00098B08 File Offset: 0x00096D08
	public bool ready
	{
		get
		{
			return this.zzz__index != -1;
		}
	}

	// Token: 0x17000902 RID: 2306
	// (get) Token: 0x06002720 RID: 10016 RVA: 0x00098B18 File Offset: 0x00096D18
	public int numBelow
	{
		get
		{
			return this.order;
		}
	}

	// Token: 0x17000903 RID: 2307
	// (get) Token: 0x06002721 RID: 10017 RVA: 0x00098B20 File Offset: 0x00096D20
	public int numAbove
	{
		get
		{
			return RPOS.WindowCount - (this.order + 1);
		}
	}

	// Token: 0x17000904 RID: 2308
	// (get) Token: 0x06002722 RID: 10018 RVA: 0x00098B30 File Offset: 0x00096D30
	public int order
	{
		get
		{
			if (this.zzz__index == -1)
			{
				throw new InvalidOperationException("this window is not yet ready. you should check .ready");
			}
			return this.zzz__index;
		}
	}

	// Token: 0x17000905 RID: 2309
	// (get) Token: 0x06002723 RID: 10019 RVA: 0x00098B50 File Offset: 0x00096D50
	// (set) Token: 0x06002724 RID: 10020 RVA: 0x00098B5C File Offset: 0x00096D5C
	public bool bumpersEnabled
	{
		get
		{
			return !this.bumpersDisabled;
		}
		set
		{
			this.bumpersDisabled = !value;
		}
	}

	// Token: 0x17000906 RID: 2310
	// (get) Token: 0x06002725 RID: 10021 RVA: 0x00098B68 File Offset: 0x00096D68
	public bool isInventoryRelated
	{
		get
		{
			return this._isInventoryRelated;
		}
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x00098B70 File Offset: 0x00096D70
	public bool BringToFront()
	{
		return RPOS.BringToFront(this);
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x00098B78 File Offset: 0x00096D78
	public bool SendToBack()
	{
		return RPOS.SendToBack(this);
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x00098B80 File Offset: 0x00096D80
	public bool MoveUp()
	{
		RPOSWindow.EnsureAwake(this);
		return RPOS.MoveUp(this);
	}

	// Token: 0x06002729 RID: 10025 RVA: 0x00098B90 File Offset: 0x00096D90
	public bool MoveDown()
	{
		RPOSWindow.EnsureAwake(this);
		return RPOS.MoveDown(this);
	}

	// Token: 0x0600272A RID: 10026 RVA: 0x00098BA0 File Offset: 0x00096DA0
	public bool IsAbove(RPOSWindow window)
	{
		return window.order < this.order;
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x00098BB0 File Offset: 0x00096DB0
	public bool IsBelow(RPOSWindow window)
	{
		return window.order > this.order;
	}

	// Token: 0x17000907 RID: 2311
	// (get) Token: 0x0600272C RID: 10028 RVA: 0x00098BC0 File Offset: 0x00096DC0
	public RPOSWindow prevWindow
	{
		get
		{
			return RPOS.GetWindowBelow(this);
		}
	}

	// Token: 0x17000908 RID: 2312
	// (get) Token: 0x0600272D RID: 10029 RVA: 0x00098BC8 File Offset: 0x00096DC8
	public RPOSWindow nextWindow
	{
		get
		{
			return RPOS.GetWindowAbove(this);
		}
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x00098BD0 File Offset: 0x00096DD0
	public void ExternalClose()
	{
		this.OnExternalClose();
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x00098BD8 File Offset: 0x00096DD8
	protected void Hide()
	{
		this.showWithRPOS = false;
		this.showWithoutRPOS = false;
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x00098BE8 File Offset: 0x00096DE8
	protected void SetWindowTitle(string title)
	{
		this.TitleText = title;
		this._titleObj.text = title.ToUpper();
		if (this.bumpers != null)
		{
			foreach (RPOSBumper.Instance instance in this.bumpers)
			{
				if (instance.label)
				{
					instance.label.text = title.ToUpper();
				}
			}
		}
	}

	// Token: 0x06002731 RID: 10033 RVA: 0x00098C8C File Offset: 0x00096E8C
	public void OnScroll(float delta)
	{
		Debug.Log("fuck you" + delta);
	}

	// Token: 0x06002732 RID: 10034 RVA: 0x00098CA4 File Offset: 0x00096EA4
	protected virtual void WindowAwake()
	{
		this.SetWindowTitle(this.TitleText);
	}

	// Token: 0x06002733 RID: 10035 RVA: 0x00098CB4 File Offset: 0x00096EB4
	protected virtual void WindowDestroy()
	{
	}

	// Token: 0x06002734 RID: 10036 RVA: 0x00098CB8 File Offset: 0x00096EB8
	protected virtual void OnWindowShow()
	{
		this.panelsEnabled = true;
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x00098CC4 File Offset: 0x00096EC4
	protected virtual void OnWindowHide()
	{
		this.panelsEnabled = false;
	}

	// Token: 0x06002736 RID: 10038 RVA: 0x00098CD0 File Offset: 0x00096ED0
	protected virtual void OnWindowOpened()
	{
		this.BringToFront();
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x00098CDC File Offset: 0x00096EDC
	protected virtual void OnWindowReOpen()
	{
		this.OnWindowOpened();
	}

	// Token: 0x06002738 RID: 10040 RVA: 0x00098CE4 File Offset: 0x00096EE4
	protected virtual void OnWindowClosed()
	{
	}

	// Token: 0x06002739 RID: 10041 RVA: 0x00098CE8 File Offset: 0x00096EE8
	protected virtual void OnRPOSClosed()
	{
	}

	// Token: 0x0600273A RID: 10042 RVA: 0x00098CEC File Offset: 0x00096EEC
	protected virtual void OnRPOSOpened()
	{
	}

	// Token: 0x0600273B RID: 10043 RVA: 0x00098CF0 File Offset: 0x00096EF0
	protected virtual void OnBumperClick(RPOSBumper.Instance bumper)
	{
		if (!this.bumpersDisabled)
		{
			this.showWithRPOS = !this.showWithRPOS;
			if (this._showWithRPOS)
			{
				this.BringToFront();
			}
		}
	}

	// Token: 0x0600273C RID: 10044 RVA: 0x00098D2C File Offset: 0x00096F2C
	protected virtual void SubTouch(GameObject go, RPOSWindow.SubTouchKind kind)
	{
		switch (kind)
		{
		case RPOSWindow.SubTouchKind.Press:
			if (go == this._dragger || go == this._background)
			{
				this.BringToFront();
			}
			break;
		case RPOSWindow.SubTouchKind.Click:
		case RPOSWindow.SubTouchKind.ClickCancel:
			this.BringToFront();
			break;
		}
	}

	// Token: 0x0600273D RID: 10045 RVA: 0x00098D8C File Offset: 0x00096F8C
	protected virtual void OnExternalClose()
	{
		this.HideOrClose(this.hidesWithExternalClose);
	}

	// Token: 0x0600273E RID: 10046 RVA: 0x00098D9C File Offset: 0x00096F9C
	protected virtual void CloseButtonClicked()
	{
		this.HideOrClose(this.hidesWithCloseButton);
	}

	// Token: 0x0600273F RID: 10047 RVA: 0x00098DAC File Offset: 0x00096FAC
	public void MovePixelXY(int x, int y)
	{
		base.transform.position = base.transform.TransformPoint((float)x, (float)y, 0f);
	}

	// Token: 0x06002740 RID: 10048 RVA: 0x00098DD8 File Offset: 0x00096FD8
	public void MovePixelX(int x)
	{
		this.MovePixelXY(x, 0);
	}

	// Token: 0x06002741 RID: 10049 RVA: 0x00098DE4 File Offset: 0x00096FE4
	public void MovePixelY(int y)
	{
		this.MovePixelXY(0, y);
	}

	// Token: 0x06002742 RID: 10050 RVA: 0x00098DF0 File Offset: 0x00096FF0
	protected void OnDrawGizmosSelected()
	{
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawWireCube(new Vector3(this._windowDimensions.x + this._windowDimensions.z / 2f, this._windowDimensions.y + this._windowDimensions.w / 2f), new Vector3(this._windowDimensions.z, this._windowDimensions.w));
		Gizmos.matrix = matrix;
	}

	// Token: 0x04001317 RID: 4887
	private RPOSWindowMessageCenter messageCenter;

	// Token: 0x04001318 RID: 4888
	private readonly UIEventListener.VoidDelegate buttonCallback;

	// Token: 0x04001319 RID: 4889
	private List<RPOSBumper.Instance> bumpers;

	// Token: 0x0400131A RID: 4890
	private UIEventListener closeButtonListener;

	// Token: 0x0400131B RID: 4891
	[Obsolete("RPOS ONLY")]
	internal int zzz__index = -1;

	// Token: 0x0400131C RID: 4892
	private bool _showWithRPOS;

	// Token: 0x0400131D RID: 4893
	private bool _showWithoutRPOS;

	// Token: 0x0400131E RID: 4894
	private bool _forceHide;

	// Token: 0x0400131F RID: 4895
	private bool _showing;

	// Token: 0x04001320 RID: 4896
	private bool _opened;

	// Token: 0x04001321 RID: 4897
	private bool _closed;

	// Token: 0x04001322 RID: 4898
	private bool _awake;

	// Token: 0x04001323 RID: 4899
	private bool _destroyed;

	// Token: 0x04001324 RID: 4900
	private bool _destroyAfterAwake;

	// Token: 0x04001325 RID: 4901
	private bool _inventoryHide;

	// Token: 0x04001326 RID: 4902
	private bool _lock_awake;

	// Token: 0x04001327 RID: 4903
	private bool _lock_open;

	// Token: 0x04001328 RID: 4904
	private bool _lock_close;

	// Token: 0x04001329 RID: 4905
	private bool _lock_show;

	// Token: 0x0400132A RID: 4906
	private bool _lock_destroy;

	// Token: 0x0400132B RID: 4907
	protected bool neverAutoShow;

	// Token: 0x0400132C RID: 4908
	[SerializeField]
	private UILabel _titleObj;

	// Token: 0x0400132D RID: 4909
	[SerializeField]
	private UIButton _closeButton;

	// Token: 0x0400132E RID: 4910
	[SerializeField]
	private UIPanel _myPanel;

	// Token: 0x0400132F RID: 4911
	[SerializeField]
	private GameObject _background;

	// Token: 0x04001330 RID: 4912
	[SerializeField]
	private GameObject _dragger;

	// Token: 0x04001331 RID: 4913
	[SerializeField]
	private string TitleText;

	// Token: 0x04001332 RID: 4914
	[SerializeField]
	protected bool autoShowWithRPOS;

	// Token: 0x04001333 RID: 4915
	[SerializeField]
	protected bool autoShowWithoutRPOS;

	// Token: 0x04001334 RID: 4916
	[SerializeField]
	protected bool hidesWithCloseButton;

	// Token: 0x04001335 RID: 4917
	[SerializeField]
	protected bool hidesWithExternalClose;

	// Token: 0x04001336 RID: 4918
	[SerializeField]
	protected bool destroyWithClose;

	// Token: 0x04001337 RID: 4919
	[SerializeField]
	protected UIPanel[] childPanels;

	// Token: 0x04001338 RID: 4920
	[SerializeField]
	private bool _isInventoryRelated;

	// Token: 0x04001339 RID: 4921
	[SerializeField]
	private UIWidget.Pivot _shrinkPivot = UIWidget.Pivot.Center;

	// Token: 0x0400133A RID: 4922
	[SerializeField]
	private Vector4 _windowDimensions = new Vector4(0f, 0f, 128f, 32f);

	// Token: 0x0400133B RID: 4923
	private bool bumpersDisabled;

	// Token: 0x0200041D RID: 1053
	protected enum SubTouchKind
	{
		// Token: 0x0400133D RID: 4925
		Press,
		// Token: 0x0400133E RID: 4926
		Click,
		// Token: 0x0400133F RID: 4927
		ClickCancel,
		// Token: 0x04001340 RID: 4928
		Release
	}
}
