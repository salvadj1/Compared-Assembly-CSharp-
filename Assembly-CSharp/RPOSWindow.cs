using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D1 RID: 1233
public class RPOSWindow : MonoBehaviour
{
	// Token: 0x06002A65 RID: 10853 RVA: 0x0009DC38 File Offset: 0x0009BE38
	public RPOSWindow()
	{
		this.buttonCallback = new global::UIEventListener.VoidDelegate(this.ButtonClickCallback);
	}

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x06002A66 RID: 10854 RVA: 0x0009DC8C File Offset: 0x0009BE8C
	// (remove) Token: 0x06002A67 RID: 10855 RVA: 0x0009DC98 File Offset: 0x0009BE98
	public event global::RPOSWindowMessageHandler WillOpen
	{
		add
		{
			this.AddMessageHandler(global::RPOSWindowMessage.WillOpen, value);
		}
		remove
		{
			this.RemoveMessageHandler(global::RPOSWindowMessage.WillOpen, value);
		}
	}

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x06002A68 RID: 10856 RVA: 0x0009DCA4 File Offset: 0x0009BEA4
	// (remove) Token: 0x06002A69 RID: 10857 RVA: 0x0009DCB0 File Offset: 0x0009BEB0
	public event global::RPOSWindowMessageHandler DidOpen
	{
		add
		{
			this.AddMessageHandler(global::RPOSWindowMessage.DidOpen, value);
		}
		remove
		{
			this.RemoveMessageHandler(global::RPOSWindowMessage.DidOpen, value);
		}
	}

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x06002A6A RID: 10858 RVA: 0x0009DCBC File Offset: 0x0009BEBC
	// (remove) Token: 0x06002A6B RID: 10859 RVA: 0x0009DCC8 File Offset: 0x0009BEC8
	public event global::RPOSWindowMessageHandler WillShow
	{
		add
		{
			this.AddMessageHandler(global::RPOSWindowMessage.WillShow, value);
		}
		remove
		{
			this.RemoveMessageHandler(global::RPOSWindowMessage.WillShow, value);
		}
	}

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x06002A6C RID: 10860 RVA: 0x0009DCD4 File Offset: 0x0009BED4
	// (remove) Token: 0x06002A6D RID: 10861 RVA: 0x0009DCE0 File Offset: 0x0009BEE0
	public event global::RPOSWindowMessageHandler DidShow
	{
		add
		{
			this.AddMessageHandler(global::RPOSWindowMessage.DidShow, value);
		}
		remove
		{
			this.RemoveMessageHandler(global::RPOSWindowMessage.DidShow, value);
		}
	}

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x06002A6E RID: 10862 RVA: 0x0009DCEC File Offset: 0x0009BEEC
	// (remove) Token: 0x06002A6F RID: 10863 RVA: 0x0009DCF8 File Offset: 0x0009BEF8
	public event global::RPOSWindowMessageHandler WillHide
	{
		add
		{
			this.AddMessageHandler(global::RPOSWindowMessage.WillHide, value);
		}
		remove
		{
			this.RemoveMessageHandler(global::RPOSWindowMessage.WillHide, value);
		}
	}

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x06002A70 RID: 10864 RVA: 0x0009DD04 File Offset: 0x0009BF04
	// (remove) Token: 0x06002A71 RID: 10865 RVA: 0x0009DD10 File Offset: 0x0009BF10
	public event global::RPOSWindowMessageHandler DidHide
	{
		add
		{
			this.AddMessageHandler(global::RPOSWindowMessage.DidHide, value);
		}
		remove
		{
			this.RemoveMessageHandler(global::RPOSWindowMessage.DidHide, value);
		}
	}

	// Token: 0x1400001E RID: 30
	// (add) Token: 0x06002A72 RID: 10866 RVA: 0x0009DD1C File Offset: 0x0009BF1C
	// (remove) Token: 0x06002A73 RID: 10867 RVA: 0x0009DD28 File Offset: 0x0009BF28
	public event global::RPOSWindowMessageHandler WillClose
	{
		add
		{
			this.AddMessageHandler(global::RPOSWindowMessage.WillClose, value);
		}
		remove
		{
			this.RemoveMessageHandler(global::RPOSWindowMessage.WillClose, value);
		}
	}

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x06002A74 RID: 10868 RVA: 0x0009DD34 File Offset: 0x0009BF34
	// (remove) Token: 0x06002A75 RID: 10869 RVA: 0x0009DD40 File Offset: 0x0009BF40
	public event global::RPOSWindowMessageHandler DidClose
	{
		add
		{
			this.AddMessageHandler(global::RPOSWindowMessage.DidClose, value);
		}
		remove
		{
			this.RemoveMessageHandler(global::RPOSWindowMessage.DidClose, value);
		}
	}

	// Token: 0x06002A76 RID: 10870 RVA: 0x0009DD4C File Offset: 0x0009BF4C
	private void FireEvent(global::RPOSWindowMessage message)
	{
		this.messageCenter.Fire(this, message);
	}

	// Token: 0x06002A77 RID: 10871 RVA: 0x0009DD5C File Offset: 0x0009BF5C
	public bool AddMessageHandler(global::RPOSWindowMessage message, global::RPOSWindowMessageHandler handler)
	{
		return !this._destroyed && !this._lock_destroy && !this._destroyAfterAwake && (this._awake || this._lock_awake) && this.messageCenter.Add(message, handler);
	}

	// Token: 0x06002A78 RID: 10872 RVA: 0x0009DDB0 File Offset: 0x0009BFB0
	public bool RemoveMessageHandler(global::RPOSWindowMessage message, global::RPOSWindowMessageHandler handler)
	{
		return (!this._awake || this._destroyed) && this.messageCenter.Remove(message, handler);
	}

	// Token: 0x06002A79 RID: 10873 RVA: 0x0009DDD8 File Offset: 0x0009BFD8
	[Obsolete("Use WindowAwake", true)]
	protected void Awake()
	{
		this._EnsureAwake();
	}

	// Token: 0x06002A7A RID: 10874 RVA: 0x0009DDE0 File Offset: 0x0009BFE0
	[Obsolete("Forwarder to SubTouch with SubTouchKind.Press if true else SubTouchKind.Release", true)]
	protected void OnChildPress(bool press)
	{
		this.SubTouch(global::UICamera.Cursor.Buttons.LeftValue.Pressed, (!press) ? global::RPOSWindow.SubTouchKind.Release : global::RPOSWindow.SubTouchKind.Press);
	}

	// Token: 0x06002A7B RID: 10875 RVA: 0x0009DE0C File Offset: 0x0009C00C
	[Obsolete("Forwarder to SubTouch with SubTouchKind.Click", true)]
	protected void OnChildClick(GameObject go)
	{
		this.SubTouch(go, global::RPOSWindow.SubTouchKind.Click);
	}

	// Token: 0x06002A7C RID: 10876 RVA: 0x0009DE18 File Offset: 0x0009C018
	[Obsolete("Forwarder to SubTouch with SubTouchKind.ClickCancel", true)]
	protected void OnChildClickMissed(GameObject go)
	{
		this.SubTouch(go, global::RPOSWindow.SubTouchKind.ClickCancel);
	}

	// Token: 0x06002A7D RID: 10877 RVA: 0x0009DE24 File Offset: 0x0009C024
	[Obsolete("Use WindowDestroy", true)]
	protected void OnDestroy()
	{
		if (this._awake)
		{
			this._EnsureDestroy();
		}
	}

	// Token: 0x06002A7E RID: 10878 RVA: 0x0009DE38 File Offset: 0x0009C038
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
					this._myPanel = base.GetComponent<global::UIPanel>();
					this.panelsEnabled = false;
					if (this._closeButton)
					{
						this.closeButtonListener = global::UIEventListener.Get(this._closeButton.gameObject);
						if (this.closeButtonListener)
						{
							global::UIEventListener uieventListener = this.closeButtonListener;
							uieventListener.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener.onClick, this.buttonCallback);
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
						global::RPOS.RegisterWindow(this);
					}
				}
			}
		}
	}

	// Token: 0x06002A7F RID: 10879 RVA: 0x0009E00C File Offset: 0x0009C20C
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
						global::UIEventListener uieventListener = this.closeButtonListener;
						uieventListener.onClick = (global::UIEventListener.VoidDelegate)Delegate.Remove(uieventListener.onClick, this.buttonCallback);
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
					global::RPOS.UnregisterWindow(this);
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

	// Token: 0x06002A80 RID: 10880 RVA: 0x0009E16C File Offset: 0x0009C36C
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
		if (global::RPOS.IsOpen)
		{
			Debug.Log("Was ready");
			this.RPOSOn();
		}
		this.CheckDisplay();
	}

	// Token: 0x06002A81 RID: 10881 RVA: 0x0009E1CC File Offset: 0x0009C3CC
	[Obsolete("For use by RPOS only!")]
	internal void RPOSOn()
	{
		this.OnRPOSOpened();
	}

	// Token: 0x06002A82 RID: 10882 RVA: 0x0009E1D4 File Offset: 0x0009C3D4
	[Obsolete("For use by RPOS only!")]
	internal void RPOSOff()
	{
		this.OnRPOSClosed();
	}

	// Token: 0x06002A83 RID: 10883 RVA: 0x0009E1DC File Offset: 0x0009C3DC
	[Obsolete("For use by RPOS only!")]
	internal bool CheckDisplay()
	{
		if (this._lock_show)
		{
			return false;
		}
		if (!this._showing)
		{
			if ((!this._showWithoutRPOS && (!this._showWithRPOS || !global::RPOS.IsOpen)) || (this._inventoryHide && this._isInventoryRelated))
			{
				return false;
			}
			this._showing = true;
			this.WindowShow();
		}
		else
		{
			if (!this._forceHide && (this._showWithoutRPOS || (this._showWithRPOS && global::RPOS.IsOpen)) && (!this._inventoryHide || !this._isInventoryRelated))
			{
				return false;
			}
			this._showing = false;
			this.WindowHide();
		}
		return true;
	}

	// Token: 0x06002A84 RID: 10884 RVA: 0x0009E2A8 File Offset: 0x0009C4A8
	public static void EnsureAwake(global::RPOSWindow window)
	{
		window._EnsureAwake();
	}

	// Token: 0x06002A85 RID: 10885 RVA: 0x0009E2B0 File Offset: 0x0009C4B0
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

	// Token: 0x06002A86 RID: 10886 RVA: 0x0009E380 File Offset: 0x0009C580
	private void ButtonClickCallback(GameObject button)
	{
		if (global::RPOSWindow.GameObjectEqual(button, this._closeButton))
		{
			this.CloseButtonClicked();
		}
		else if (this.bumpers != null)
		{
			int count = this.bumpers.Count;
			if (count > 0 && button)
			{
				global::UIButton component = button.GetComponent<global::UIButton>();
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

	// Token: 0x06002A87 RID: 10887 RVA: 0x0009E428 File Offset: 0x0009C628
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

	// Token: 0x17000956 RID: 2390
	// (set) Token: 0x06002A88 RID: 10888 RVA: 0x0009E444 File Offset: 0x0009C644
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
				foreach (global::UIPanel uipanel in this.childPanels)
				{
					if (uipanel)
					{
						uipanel.enabled = value;
					}
				}
			}
		}
	}

	// Token: 0x06002A89 RID: 10889 RVA: 0x0009E4AC File Offset: 0x0009C6AC
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
			this.FireEvent(global::RPOSWindowMessage.WillShow);
			this.OnWindowShow();
			this.FireEvent(global::RPOSWindowMessage.DidShow);
		}
		finally
		{
			this._lock_show = false;
		}
	}

	// Token: 0x06002A8A RID: 10890 RVA: 0x0009E524 File Offset: 0x0009C724
	private void WindowHide()
	{
		if (this._lock_show)
		{
			throw new InvalidOperationException("The window was already in the process of showing or hiding");
		}
		try
		{
			this._lock_show = true;
			this.FireEvent(global::RPOSWindowMessage.WillHide);
			this.OnWindowHide();
			this.FireEvent(global::RPOSWindowMessage.DidHide);
		}
		finally
		{
			this._lock_show = false;
		}
	}

	// Token: 0x06002A8B RID: 10891 RVA: 0x0009E58C File Offset: 0x0009C78C
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
				this.FireEvent(global::RPOSWindowMessage.WillClose);
				this._closed = true;
				this.OnWindowClosed();
				this._opened = false;
				this.FireEvent(global::RPOSWindowMessage.DidClose);
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

	// Token: 0x06002A8C RID: 10892 RVA: 0x0009E674 File Offset: 0x0009C874
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
			this.FireEvent(global::RPOSWindowMessage.WillOpen);
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
			this.FireEvent(global::RPOSWindowMessage.DidOpen);
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

	// Token: 0x06002A8D RID: 10893 RVA: 0x0009E72C File Offset: 0x0009C92C
	internal void AddBumper(global::RPOSBumper.Instance inst)
	{
		inst.label.text = this.title;
		global::UIEventListener listener = inst.listener;
		if (listener)
		{
			global::UIEventListener uieventListener = listener;
			uieventListener.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener.onClick, this.buttonCallback);
			if (this.bumpers == null)
			{
				this.bumpers = new List<global::RPOSBumper.Instance>();
			}
			this.bumpers.Add(inst);
		}
	}

	// Token: 0x06002A8E RID: 10894 RVA: 0x0009E79C File Offset: 0x0009C99C
	internal void RemoveBumper(global::RPOSBumper.Instance inst)
	{
		if (this.bumpers != null && this.bumpers.Remove(inst))
		{
			global::UIEventListener listener = inst.listener;
			if (listener)
			{
				global::UIEventListener uieventListener = listener;
				uieventListener.onClick = (global::UIEventListener.VoidDelegate)Delegate.Remove(uieventListener.onClick, this.buttonCallback);
			}
		}
	}

	// Token: 0x17000957 RID: 2391
	// (get) Token: 0x06002A8F RID: 10895 RVA: 0x0009E7F4 File Offset: 0x0009C9F4
	// (set) Token: 0x06002A90 RID: 10896 RVA: 0x0009E7FC File Offset: 0x0009C9FC
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

	// Token: 0x06002A91 RID: 10897 RVA: 0x0009E83C File Offset: 0x0009CA3C
	internal void zzz___INTERNAL_FOCUS()
	{
		if (!this.showWithRPOS)
		{
			this.showWithRPOS = true;
		}
		this.BringToFront();
	}

	// Token: 0x17000958 RID: 2392
	// (get) Token: 0x06002A92 RID: 10898 RVA: 0x0009E858 File Offset: 0x0009CA58
	public Vector4 windowDimensions
	{
		get
		{
			return this._windowDimensions;
		}
	}

	// Token: 0x17000959 RID: 2393
	// (get) Token: 0x06002A93 RID: 10899 RVA: 0x0009E860 File Offset: 0x0009CA60
	public global::UIWidget.Pivot shrinkPivot
	{
		get
		{
			return this._shrinkPivot;
		}
	}

	// Token: 0x1700095A RID: 2394
	// (get) Token: 0x06002A94 RID: 10900 RVA: 0x0009E868 File Offset: 0x0009CA68
	// (set) Token: 0x06002A95 RID: 10901 RVA: 0x0009E870 File Offset: 0x0009CA70
	public global::UILabel titleObj
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

	// Token: 0x1700095B RID: 2395
	// (get) Token: 0x06002A96 RID: 10902 RVA: 0x0009E87C File Offset: 0x0009CA7C
	// (set) Token: 0x06002A97 RID: 10903 RVA: 0x0009E884 File Offset: 0x0009CA84
	public global::UIButton closeButton
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

	// Token: 0x1700095C RID: 2396
	// (get) Token: 0x06002A98 RID: 10904 RVA: 0x0009E890 File Offset: 0x0009CA90
	// (set) Token: 0x06002A99 RID: 10905 RVA: 0x0009E898 File Offset: 0x0009CA98
	public global::UIPanel mainPanel
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

	// Token: 0x1700095D RID: 2397
	// (get) Token: 0x06002A9A RID: 10906 RVA: 0x0009E8A4 File Offset: 0x0009CAA4
	// (set) Token: 0x06002A9B RID: 10907 RVA: 0x0009E8AC File Offset: 0x0009CAAC
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

	// Token: 0x1700095E RID: 2398
	// (get) Token: 0x06002A9C RID: 10908 RVA: 0x0009E8B8 File Offset: 0x0009CAB8
	// (set) Token: 0x06002A9D RID: 10909 RVA: 0x0009E8C0 File Offset: 0x0009CAC0
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

	// Token: 0x1700095F RID: 2399
	// (get) Token: 0x06002A9E RID: 10910 RVA: 0x0009E8CC File Offset: 0x0009CACC
	public bool open
	{
		get
		{
			return this._opened && !this._closed;
		}
	}

	// Token: 0x17000960 RID: 2400
	// (get) Token: 0x06002A9F RID: 10911 RVA: 0x0009E8E8 File Offset: 0x0009CAE8
	public bool closed
	{
		get
		{
			return this._closed && !this._opened;
		}
	}

	// Token: 0x17000961 RID: 2401
	// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x0009E904 File Offset: 0x0009CB04
	public bool showing
	{
		get
		{
			return this._showing;
		}
	}

	// Token: 0x17000962 RID: 2402
	// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x0009E90C File Offset: 0x0009CB0C
	// (set) Token: 0x06002AA2 RID: 10914 RVA: 0x0009E924 File Offset: 0x0009CB24
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

	// Token: 0x17000963 RID: 2403
	// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x0009E940 File Offset: 0x0009CB40
	public bool showingWithRPOS
	{
		get
		{
			return this._showing && global::RPOS.IsOpen;
		}
	}

	// Token: 0x17000964 RID: 2404
	// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x0009E958 File Offset: 0x0009CB58
	// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x0009E970 File Offset: 0x0009CB70
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

	// Token: 0x17000965 RID: 2405
	// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x0009E98C File Offset: 0x0009CB8C
	public bool showingWithoutRPOS
	{
		get
		{
			return this._showing && !global::RPOS.IsOpen;
		}
	}

	// Token: 0x17000966 RID: 2406
	// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x0009E9A4 File Offset: 0x0009CBA4
	// (set) Token: 0x06002AA8 RID: 10920 RVA: 0x0009E9AC File Offset: 0x0009CBAC
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

	// Token: 0x17000967 RID: 2407
	// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x0009E9CC File Offset: 0x0009CBCC
	public bool ready
	{
		get
		{
			return this.zzz__index != -1;
		}
	}

	// Token: 0x17000968 RID: 2408
	// (get) Token: 0x06002AAA RID: 10922 RVA: 0x0009E9DC File Offset: 0x0009CBDC
	public int numBelow
	{
		get
		{
			return this.order;
		}
	}

	// Token: 0x17000969 RID: 2409
	// (get) Token: 0x06002AAB RID: 10923 RVA: 0x0009E9E4 File Offset: 0x0009CBE4
	public int numAbove
	{
		get
		{
			return global::RPOS.WindowCount - (this.order + 1);
		}
	}

	// Token: 0x1700096A RID: 2410
	// (get) Token: 0x06002AAC RID: 10924 RVA: 0x0009E9F4 File Offset: 0x0009CBF4
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

	// Token: 0x1700096B RID: 2411
	// (get) Token: 0x06002AAD RID: 10925 RVA: 0x0009EA14 File Offset: 0x0009CC14
	// (set) Token: 0x06002AAE RID: 10926 RVA: 0x0009EA20 File Offset: 0x0009CC20
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

	// Token: 0x1700096C RID: 2412
	// (get) Token: 0x06002AAF RID: 10927 RVA: 0x0009EA2C File Offset: 0x0009CC2C
	public bool isInventoryRelated
	{
		get
		{
			return this._isInventoryRelated;
		}
	}

	// Token: 0x06002AB0 RID: 10928 RVA: 0x0009EA34 File Offset: 0x0009CC34
	public bool BringToFront()
	{
		return global::RPOS.BringToFront(this);
	}

	// Token: 0x06002AB1 RID: 10929 RVA: 0x0009EA3C File Offset: 0x0009CC3C
	public bool SendToBack()
	{
		return global::RPOS.SendToBack(this);
	}

	// Token: 0x06002AB2 RID: 10930 RVA: 0x0009EA44 File Offset: 0x0009CC44
	public bool MoveUp()
	{
		global::RPOSWindow.EnsureAwake(this);
		return global::RPOS.MoveUp(this);
	}

	// Token: 0x06002AB3 RID: 10931 RVA: 0x0009EA54 File Offset: 0x0009CC54
	public bool MoveDown()
	{
		global::RPOSWindow.EnsureAwake(this);
		return global::RPOS.MoveDown(this);
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x0009EA64 File Offset: 0x0009CC64
	public bool IsAbove(global::RPOSWindow window)
	{
		return window.order < this.order;
	}

	// Token: 0x06002AB5 RID: 10933 RVA: 0x0009EA74 File Offset: 0x0009CC74
	public bool IsBelow(global::RPOSWindow window)
	{
		return window.order > this.order;
	}

	// Token: 0x1700096D RID: 2413
	// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x0009EA84 File Offset: 0x0009CC84
	public global::RPOSWindow prevWindow
	{
		get
		{
			return global::RPOS.GetWindowBelow(this);
		}
	}

	// Token: 0x1700096E RID: 2414
	// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x0009EA8C File Offset: 0x0009CC8C
	public global::RPOSWindow nextWindow
	{
		get
		{
			return global::RPOS.GetWindowAbove(this);
		}
	}

	// Token: 0x06002AB8 RID: 10936 RVA: 0x0009EA94 File Offset: 0x0009CC94
	public void ExternalClose()
	{
		this.OnExternalClose();
	}

	// Token: 0x06002AB9 RID: 10937 RVA: 0x0009EA9C File Offset: 0x0009CC9C
	protected void Hide()
	{
		this.showWithRPOS = false;
		this.showWithoutRPOS = false;
	}

	// Token: 0x06002ABA RID: 10938 RVA: 0x0009EAAC File Offset: 0x0009CCAC
	protected void SetWindowTitle(string title)
	{
		this.TitleText = title;
		this._titleObj.text = title.ToUpper();
		if (this.bumpers != null)
		{
			foreach (global::RPOSBumper.Instance instance in this.bumpers)
			{
				if (instance.label)
				{
					instance.label.text = title.ToUpper();
				}
			}
		}
	}

	// Token: 0x06002ABB RID: 10939 RVA: 0x0009EB50 File Offset: 0x0009CD50
	public void OnScroll(float delta)
	{
		Debug.Log("fuck you" + delta);
	}

	// Token: 0x06002ABC RID: 10940 RVA: 0x0009EB68 File Offset: 0x0009CD68
	protected virtual void WindowAwake()
	{
		this.SetWindowTitle(this.TitleText);
	}

	// Token: 0x06002ABD RID: 10941 RVA: 0x0009EB78 File Offset: 0x0009CD78
	protected virtual void WindowDestroy()
	{
	}

	// Token: 0x06002ABE RID: 10942 RVA: 0x0009EB7C File Offset: 0x0009CD7C
	protected virtual void OnWindowShow()
	{
		this.panelsEnabled = true;
	}

	// Token: 0x06002ABF RID: 10943 RVA: 0x0009EB88 File Offset: 0x0009CD88
	protected virtual void OnWindowHide()
	{
		this.panelsEnabled = false;
	}

	// Token: 0x06002AC0 RID: 10944 RVA: 0x0009EB94 File Offset: 0x0009CD94
	protected virtual void OnWindowOpened()
	{
		this.BringToFront();
	}

	// Token: 0x06002AC1 RID: 10945 RVA: 0x0009EBA0 File Offset: 0x0009CDA0
	protected virtual void OnWindowReOpen()
	{
		this.OnWindowOpened();
	}

	// Token: 0x06002AC2 RID: 10946 RVA: 0x0009EBA8 File Offset: 0x0009CDA8
	protected virtual void OnWindowClosed()
	{
	}

	// Token: 0x06002AC3 RID: 10947 RVA: 0x0009EBAC File Offset: 0x0009CDAC
	protected virtual void OnRPOSClosed()
	{
	}

	// Token: 0x06002AC4 RID: 10948 RVA: 0x0009EBB0 File Offset: 0x0009CDB0
	protected virtual void OnRPOSOpened()
	{
	}

	// Token: 0x06002AC5 RID: 10949 RVA: 0x0009EBB4 File Offset: 0x0009CDB4
	protected virtual void OnBumperClick(global::RPOSBumper.Instance bumper)
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

	// Token: 0x06002AC6 RID: 10950 RVA: 0x0009EBF0 File Offset: 0x0009CDF0
	protected virtual void SubTouch(GameObject go, global::RPOSWindow.SubTouchKind kind)
	{
		switch (kind)
		{
		case global::RPOSWindow.SubTouchKind.Press:
			if (go == this._dragger || go == this._background)
			{
				this.BringToFront();
			}
			break;
		case global::RPOSWindow.SubTouchKind.Click:
		case global::RPOSWindow.SubTouchKind.ClickCancel:
			this.BringToFront();
			break;
		}
	}

	// Token: 0x06002AC7 RID: 10951 RVA: 0x0009EC50 File Offset: 0x0009CE50
	protected virtual void OnExternalClose()
	{
		this.HideOrClose(this.hidesWithExternalClose);
	}

	// Token: 0x06002AC8 RID: 10952 RVA: 0x0009EC60 File Offset: 0x0009CE60
	protected virtual void CloseButtonClicked()
	{
		this.HideOrClose(this.hidesWithCloseButton);
	}

	// Token: 0x06002AC9 RID: 10953 RVA: 0x0009EC70 File Offset: 0x0009CE70
	public void MovePixelXY(int x, int y)
	{
		base.transform.position = base.transform.TransformPoint((float)x, (float)y, 0f);
	}

	// Token: 0x06002ACA RID: 10954 RVA: 0x0009EC9C File Offset: 0x0009CE9C
	public void MovePixelX(int x)
	{
		this.MovePixelXY(x, 0);
	}

	// Token: 0x06002ACB RID: 10955 RVA: 0x0009ECA8 File Offset: 0x0009CEA8
	public void MovePixelY(int y)
	{
		this.MovePixelXY(0, y);
	}

	// Token: 0x06002ACC RID: 10956 RVA: 0x0009ECB4 File Offset: 0x0009CEB4
	protected void OnDrawGizmosSelected()
	{
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawWireCube(new Vector3(this._windowDimensions.x + this._windowDimensions.z / 2f, this._windowDimensions.y + this._windowDimensions.w / 2f), new Vector3(this._windowDimensions.z, this._windowDimensions.w));
		Gizmos.matrix = matrix;
	}

	// Token: 0x04001497 RID: 5271
	private global::RPOSWindowMessageCenter messageCenter;

	// Token: 0x04001498 RID: 5272
	private readonly global::UIEventListener.VoidDelegate buttonCallback;

	// Token: 0x04001499 RID: 5273
	private List<global::RPOSBumper.Instance> bumpers;

	// Token: 0x0400149A RID: 5274
	private global::UIEventListener closeButtonListener;

	// Token: 0x0400149B RID: 5275
	[Obsolete("RPOS ONLY")]
	internal int zzz__index = -1;

	// Token: 0x0400149C RID: 5276
	private bool _showWithRPOS;

	// Token: 0x0400149D RID: 5277
	private bool _showWithoutRPOS;

	// Token: 0x0400149E RID: 5278
	private bool _forceHide;

	// Token: 0x0400149F RID: 5279
	private bool _showing;

	// Token: 0x040014A0 RID: 5280
	private bool _opened;

	// Token: 0x040014A1 RID: 5281
	private bool _closed;

	// Token: 0x040014A2 RID: 5282
	private bool _awake;

	// Token: 0x040014A3 RID: 5283
	private bool _destroyed;

	// Token: 0x040014A4 RID: 5284
	private bool _destroyAfterAwake;

	// Token: 0x040014A5 RID: 5285
	private bool _inventoryHide;

	// Token: 0x040014A6 RID: 5286
	private bool _lock_awake;

	// Token: 0x040014A7 RID: 5287
	private bool _lock_open;

	// Token: 0x040014A8 RID: 5288
	private bool _lock_close;

	// Token: 0x040014A9 RID: 5289
	private bool _lock_show;

	// Token: 0x040014AA RID: 5290
	private bool _lock_destroy;

	// Token: 0x040014AB RID: 5291
	protected bool neverAutoShow;

	// Token: 0x040014AC RID: 5292
	[SerializeField]
	private global::UILabel _titleObj;

	// Token: 0x040014AD RID: 5293
	[SerializeField]
	private global::UIButton _closeButton;

	// Token: 0x040014AE RID: 5294
	[SerializeField]
	private global::UIPanel _myPanel;

	// Token: 0x040014AF RID: 5295
	[SerializeField]
	private GameObject _background;

	// Token: 0x040014B0 RID: 5296
	[SerializeField]
	private GameObject _dragger;

	// Token: 0x040014B1 RID: 5297
	[SerializeField]
	private string TitleText;

	// Token: 0x040014B2 RID: 5298
	[SerializeField]
	protected bool autoShowWithRPOS;

	// Token: 0x040014B3 RID: 5299
	[SerializeField]
	protected bool autoShowWithoutRPOS;

	// Token: 0x040014B4 RID: 5300
	[SerializeField]
	protected bool hidesWithCloseButton;

	// Token: 0x040014B5 RID: 5301
	[SerializeField]
	protected bool hidesWithExternalClose;

	// Token: 0x040014B6 RID: 5302
	[SerializeField]
	protected bool destroyWithClose;

	// Token: 0x040014B7 RID: 5303
	[SerializeField]
	protected global::UIPanel[] childPanels;

	// Token: 0x040014B8 RID: 5304
	[SerializeField]
	private bool _isInventoryRelated;

	// Token: 0x040014B9 RID: 5305
	[SerializeField]
	private global::UIWidget.Pivot _shrinkPivot = global::UIWidget.Pivot.Center;

	// Token: 0x040014BA RID: 5306
	[SerializeField]
	private Vector4 _windowDimensions = new Vector4(0f, 0f, 128f, 32f);

	// Token: 0x040014BB RID: 5307
	private bool bumpersDisabled;

	// Token: 0x020004D2 RID: 1234
	protected enum SubTouchKind
	{
		// Token: 0x040014BD RID: 5309
		Press,
		// Token: 0x040014BE RID: 5310
		Click,
		// Token: 0x040014BF RID: 5311
		ClickCancel,
		// Token: 0x040014C0 RID: 5312
		Release
	}
}
