using System;
using Facepunch.Cursor;
using Facepunch.Utility;
using uLink;
using UnityEngine;

// Token: 0x020003FD RID: 1021
public class MainMenu : MonoBehaviour
{
	// Token: 0x06002582 RID: 9602 RVA: 0x0009029C File Offset: 0x0008E49C
	public static bool IsVisible()
	{
		return MainMenu.singleton && MainMenu.singleton.GetComponent<dfPanel>().IsVisible;
	}

	// Token: 0x06002583 RID: 9603 RVA: 0x000902C0 File Offset: 0x0008E4C0
	private void Awake()
	{
		MainMenu.singleton = this;
		LockCursorManager.onEscapeKey += new EscapeKeyEventHandler(this.Show);
		this.screenServers.Hide();
		this.screenOptions.Hide();
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x000902F0 File Offset: 0x0008E4F0
	private void OnDestroy()
	{
		LockCursorManager.onEscapeKey -= new EscapeKeyEventHandler(this.Show);
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x00090304 File Offset: 0x0008E504
	public void LoadBackground()
	{
		Object.DontDestroyOnLoad(base.gameObject.transform.parent.gameObject);
		Application.LoadLevel("MenuBackground");
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x00090338 File Offset: 0x0008E538
	private void Start()
	{
		this.cursorManager = LockCursorManager.CreateCursorUnlockNode(false, "Main Menu");
		this.Show();
		if (!Object.FindObjectOfType(typeof(ClientConnect)))
		{
			this.LoadBackground();
		}
	}

	// Token: 0x06002587 RID: 9607 RVA: 0x0009037C File Offset: 0x0008E57C
	private void Update()
	{
		if (NetCull.isClientRunning)
		{
			if (Input.GetKeyDown(27))
			{
				if (MainMenu.IsVisible())
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

	// Token: 0x06002588 RID: 9608 RVA: 0x000903C0 File Offset: 0x0008E5C0
	public void Hide()
	{
		base.GetComponent<dfPanel>().Hide();
		this.cursorManager.On = false;
		this.blurCamera.enabled = false;
		LoadingScreen.Hide();
		HudEnabled.Enable();
	}

	// Token: 0x06002589 RID: 9609 RVA: 0x000903FC File Offset: 0x0008E5FC
	public void Show()
	{
		base.GetComponent<dfPanel>().Show();
		this.cursorManager.On = true;
		this.blurCamera.enabled = true;
		HudEnabled.Disable();
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x00090434 File Offset: 0x0008E634
	private void HideAllBut(dfPanel but)
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

	// Token: 0x0600258B RID: 9611 RVA: 0x0009049C File Offset: 0x0008E69C
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

	// Token: 0x0600258C RID: 9612 RVA: 0x000904FC File Offset: 0x0008E6FC
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

	// Token: 0x0600258D RID: 9613 RVA: 0x0009055C File Offset: 0x0008E75C
	public void ShowInformation(string text)
	{
		ConsoleSystem.Run("notice.popup 5 \"\" " + Facepunch.Utility.String.QuoteSafe(text), false);
	}

	// Token: 0x0600258E RID: 9614 RVA: 0x00090578 File Offset: 0x0008E778
	public void DoExit()
	{
		ConsoleSystem.Run("quit", false);
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x00090588 File Offset: 0x0008E788
	private void LogDisconnect(NetError error, NetworkDisconnection? disconnection = null)
	{
		if (error != NetError.NoError)
		{
			Debug.LogWarning(error);
		}
		if (disconnection != null)
		{
			Debug.Log(disconnection);
		}
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x000905C0 File Offset: 0x0008E7C0
	private void uLink_OnDisconnectedFromServer(NetworkDisconnection netDisconnect)
	{
		NetError lastKickReason = ServerManagement.GetLastKickReason(true);
		this.LogDisconnect(lastKickReason, new NetworkDisconnection?(netDisconnect));
		DisableOnConnectedState.OnDisconnected();
		ConsoleSystem.Run("gameui.show", false);
		this.LoadBackground();
		if (lastKickReason != NetError.NoError)
		{
			this.ShowInformation("Disconnected (" + lastKickReason.NiceString() + ")");
		}
		else
		{
			this.ShowInformation("Disconnected from server.");
		}
		LoadingScreen.Hide();
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x00090630 File Offset: 0x0008E830
	private void uLink_OnFailedToConnect(NetworkConnectionError ulink_error)
	{
		this.LogDisconnect(ulink_error.ToNetError(), null);
		DisableOnConnectedState.OnDisconnected();
		ConsoleSystem.Run("gameui.show", false);
		this.LoadBackground();
		if (ulink_error.ToNetError() != NetError.NoError)
		{
			this.ShowInformation("Failed to connect (" + ulink_error.ToNetError().ToString() + ")");
		}
		else
		{
			this.ShowInformation("Failed to connect.");
		}
		LoadingScreen.Hide();
	}

	// Token: 0x04001233 RID: 4659
	public Camera blurCamera;

	// Token: 0x04001234 RID: 4660
	public dfPanel screenServers;

	// Token: 0x04001235 RID: 4661
	public dfPanel screenOptions;

	// Token: 0x04001236 RID: 4662
	public UnlockCursorNode cursorManager;

	// Token: 0x04001237 RID: 4663
	public static MainMenu singleton;
}
