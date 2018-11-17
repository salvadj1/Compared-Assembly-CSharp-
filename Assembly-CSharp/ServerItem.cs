using System;
using UnityEngine;

// Token: 0x02000408 RID: 1032
public class ServerItem : MonoBehaviour
{
	// Token: 0x060025D6 RID: 9686 RVA: 0x00091D74 File Offset: 0x0008FF74
	public void Init(ref ServerBrowser.Server s)
	{
		this.server = s;
		this.textLabel.Text = this.server.name;
		this.textPlayers.Text = this.server.currentplayers.ToString() + " / " + this.server.maxplayers.ToString();
		this.textPing.Text = this.server.ping.ToString();
		dfScrollPanel component = base.transform.parent.GetComponent<dfScrollPanel>();
		if (component)
		{
			base.GetComponent<dfControl>().Width = component.Width;
			base.GetComponent<dfControl>().ResetLayout(true, false);
		}
		this.UpdateColours();
	}

	// Token: 0x060025D7 RID: 9687 RVA: 0x00091E30 File Offset: 0x00090030
	public void Connect()
	{
		Debug.Log("> net.connect " + this.server.address + ":" + this.server.port.ToString());
		ConsoleSystem.Run("net.connect " + this.server.address + ":" + this.server.port.ToString(), false);
	}

	// Token: 0x060025D8 RID: 9688 RVA: 0x00091EA0 File Offset: 0x000900A0
	public void SelectThis()
	{
		this.selectedItem = this;
	}

	// Token: 0x060025D9 RID: 9689 RVA: 0x00091EAC File Offset: 0x000900AC
	public void OnClickFave()
	{
		this.server.fave = !this.server.fave;
		this.UpdateColours();
		base.SendMessageUpwards("UpdateServerList");
		if (this.server.fave)
		{
			ConsoleSystem.Run("serverfavourite.add " + this.server.address + ":" + this.server.port.ToString(), false);
		}
		else
		{
			ConsoleSystem.Run("serverfavourite.remove " + this.server.address + ":" + this.server.port.ToString(), false);
		}
		ConsoleSystem.Run("serverfavourite.save", false);
	}

	// Token: 0x060025DA RID: 9690 RVA: 0x00091F68 File Offset: 0x00090168
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

	// Token: 0x0400126F RID: 4719
	public ServerItem selectedItem;

	// Token: 0x04001270 RID: 4720
	public dfButton textLabel;

	// Token: 0x04001271 RID: 4721
	public dfLabel textPlayers;

	// Token: 0x04001272 RID: 4722
	public dfLabel textPing;

	// Token: 0x04001273 RID: 4723
	public dfButton btnFave;

	// Token: 0x04001274 RID: 4724
	public ServerBrowser.Server server;
}
