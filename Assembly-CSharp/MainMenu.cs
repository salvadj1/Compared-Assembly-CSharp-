using System;
using Facepunch.Cursor;
using Facepunch.Utility;
using uLink;
using UnityEngine;

// Token: 0x020004AE RID: 1198
public class MainMenu : MonoBehaviour
{
	// Token: 0x060028FA RID: 10490 RVA: 0x000960D4 File Offset: 0x000942D4
	public static bool IsVisible()
	{
		return global::MainMenu.singleton && global::MainMenu.singleton.GetComponent<global::dfPanel>().IsVisible;
	}

	// Token: 0x060028FB RID: 10491 RVA: 0x000960F8 File Offset: 0x000942F8
	private void Awake()
	{
		global::MainMenu.singleton = this;
		LockCursorManager.onEscapeKey += new EscapeKeyEventHandler(this.Show);
		this.screenServers.Hide();
		this.screenOptions.Hide();
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x00096128 File Offset: 0x00094328
	private void OnDestroy()
	{
		LockCursorManager.onEscapeKey -= new EscapeKeyEventHandler(this.Show);
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x0009613C File Offset: 0x0009433C
	public void LoadBackground()
	{
		Object.DontDestroyOnLoad(base.gameObject.transform.parent.gameObject);
		Application.LoadLevel("MenuBackground");
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x00096170 File Offset: 0x00094370
	private void Start()
	{
		this.cursorManager = LockCursorManager.CreateCursorUnlockNode(false, "Main Menu");
		this.Show();
		if (!Object.FindObjectOfType(typeof(global::ClientConnect)))
		{
			this.LoadBackground();
		}
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x000961B4 File Offset: 0x000943B4
	private void Update()
	{
		if (global::NetCull.isClientRunning)
		{
			if (Input.GetKeyDown(27))
			{
				if (global::MainMenu.IsVisible())
				{
					this.Hide();
				}
				else
				{
					this.Show();
				}
			}
		}
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x000961F8 File Offset: 0x000943F8
	public void Hide()
	{
		base.GetComponent<global::dfPanel>().Hide();
		this.cursorManager.On = false;
		this.blurCamera.enabled = false;
		global::LoadingScreen.Hide();
		global::HudEnabled.Enable();
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x00096234 File Offset: 0x00094434
	public void Show()
	{
		base.GetComponent<global::dfPanel>().Show();
		this.cursorManager.On = true;
		this.blurCamera.enabled = true;
		global::HudEnabled.Disable();
	}

	// Token: 0x06002902 RID: 10498 RVA: 0x0009626C File Offset: 0x0009446C
	private void HideAllBut(global::dfPanel but)
	{
		if (this.screenServers && this.screenServers != but)
		{
			this.screenServers.Hide();
		}
		if (this.screenOptions && this.screenOptions != but)
		{
			this.screenOptions.Hide();
		}
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x000962D4 File Offset: 0x000944D4
	public void ShowOptions()
	{
		this.HideAllBut(this.screenOptions);
		if (this.screenOptions)
		{
			if (this.screenOptions.IsVisible)
			{
				this.screenOptions.Hide();
			}
			else
			{
				this.screenOptions.Show();
			}
			this.screenOptions.SendToBack();
		}
	}

	// Token: 0x06002904 RID: 10500 RVA: 0x00096334 File Offset: 0x00094534
	public void ShowServerlist()
	{
		this.HideAllBut(this.screenServers);
		if (this.screenServers)
		{
			if (this.screenServers.IsVisible)
			{
				this.screenServers.Hide();
			}
			else
			{
				this.screenServers.Show();
			}
			this.screenServers.SendToBack();
		}
	}

	// Token: 0x06002905 RID: 10501 RVA: 0x00096394 File Offset: 0x00094594
	public void ShowInformation(string text)
	{
		global::ConsoleSystem.Run("notice.popup 5 \"\" " + Facepunch.Utility.String.QuoteSafe(text), false);
	}

	// Token: 0x06002906 RID: 10502 RVA: 0x000963B0 File Offset: 0x000945B0
	public void DoExit()
	{
		global::ConsoleSystem.Run("quit", false);
	}

	// Token: 0x06002907 RID: 10503 RVA: 0x000963C0 File Offset: 0x000945C0
	private void LogDisconnect(global::NetError error, uLink.NetworkDisconnection? disconnection = null)
	{
		if (error != global::NetError.NoError)
		{
			Debug.LogWarning(error);
		}
		if (disconnection != null)
		{
			Debug.Log(disconnection);
		}
	}

	// Token: 0x06002908 RID: 10504 RVA: 0x000963F8 File Offset: 0x000945F8
	private void uLink_OnDisconnectedFromServer(uLink.NetworkDisconnection netDisconnect)
	{
		global::NetError lastKickReason = global::ServerManagement.GetLastKickReason(true);
		this.LogDisconnect(lastKickReason, new uLink.NetworkDisconnection?(netDisconnect));
		global::DisableOnConnectedState.OnDisconnected();
		global::ConsoleSystem.Run("gameui.show", false);
		this.LoadBackground();
		if (lastKickReason != global::NetError.NoError)
		{
			this.ShowInformation("Disconnected (" + lastKickReason.NiceString() + ")");
		}
		else
		{
			this.ShowInformation("Disconnected from server.");
		}
		global::LoadingScreen.Hide();
	}

	// Token: 0x06002909 RID: 10505 RVA: 0x00096468 File Offset: 0x00094668
	private void uLink_OnFailedToConnect(uLink.NetworkConnectionError ulink_error)
	{
		this.LogDisconnect(ulink_error.ToNetError(), null);
		global::DisableOnConnectedState.OnDisconnected();
		global::ConsoleSystem.Run("gameui.show", false);
		this.LoadBackground();
		if (ulink_error.ToNetError() != global::NetError.NoError)
		{
			this.ShowInformation("Failed to connect (" + ulink_error.ToNetError().ToString() + ")");
		}
		else
		{
			this.ShowInformation("Failed to connect.");
		}
		global::LoadingScreen.Hide();
	}

	// Token: 0x040013B0 RID: 5040
	public Camera blurCamera;

	// Token: 0x040013B1 RID: 5041
	public global::dfPanel screenServers;

	// Token: 0x040013B2 RID: 5042
	public global::dfPanel screenOptions;

	// Token: 0x040013B3 RID: 5043
	public UnlockCursorNode cursorManager;

	// Token: 0x040013B4 RID: 5044
	public static global::MainMenu singleton;
}
