using System;
using UnityEngine;

// Token: 0x020004BC RID: 1212
public class ServerItem : MonoBehaviour
{
	// Token: 0x0600295C RID: 10588 RVA: 0x00097C38 File Offset: 0x00095E38
	public void Init(ref global::ServerBrowser.Server s)
	{
		this.server = s;
		this.textLabel.Text = this.server.name;
		this.textPlayers.Text = this.server.currentplayers.ToString() + " / " + this.server.maxplayers.ToString();
		this.textPing.Text = this.server.ping.ToString();
		global::dfScrollPanel component = base.transform.parent.GetComponent<global::dfScrollPanel>();
		if (component)
		{
			base.GetComponent<global::dfControl>().Width = component.Width;
			base.GetComponent<global::dfControl>().ResetLayout(true, false);
		}
		this.UpdateColours();
	}

	// Token: 0x0600295D RID: 10589 RVA: 0x00097CF4 File Offset: 0x00095EF4
	public void Connect()
	{
		Debug.Log("> net.connect " + this.server.address + ":" + this.server.port.ToString());
		global::ConsoleSystem.Run("net.connect " + this.server.address + ":" + this.server.port.ToString(), false);
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x00097D64 File Offset: 0x00095F64
	public void SelectThis()
	{
		this.selectedItem = this;
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x00097D70 File Offset: 0x00095F70
	public void OnClickFave()
	{
		this.server.fave = !this.server.fave;
		this.UpdateColours();
		base.SendMessageUpwards("UpdateServerList");
		if (this.server.fave)
		{
			global::ConsoleSystem.Run("serverfavourite.add " + this.server.address + ":" + this.server.port.ToString(), false);
		}
		else
		{
			global::ConsoleSystem.Run("serverfavourite.remove " + this.server.address + ":" + this.server.port.ToString(), false);
		}
		global::ConsoleSystem.Run("serverfavourite.save", false);
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x00097E2C File Offset: 0x0009602C
	protected void UpdateColours()
	{
		if (this.server.fave)
		{
			this.btnFave.Opacity = 1f;
		}
		else
		{
			this.btnFave.Opacity = 0.2f;
		}
	}

	// Token: 0x040013EF RID: 5103
	public global::ServerItem selectedItem;

	// Token: 0x040013F0 RID: 5104
	public global::dfButton textLabel;

	// Token: 0x040013F1 RID: 5105
	public global::dfLabel textPlayers;

	// Token: 0x040013F2 RID: 5106
	public global::dfLabel textPing;

	// Token: 0x040013F3 RID: 5107
	public global::dfButton btnFave;

	// Token: 0x040013F4 RID: 5108
	public global::ServerBrowser.Server server;
}
