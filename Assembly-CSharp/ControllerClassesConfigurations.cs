using System;
using UnityEngine;

// Token: 0x0200013D RID: 317
[Serializable]
public class ControllerClassesConfigurations
{
	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000922 RID: 2338 RVA: 0x00026B00 File Offset: 0x00024D00
	internal string unassignedClassName
	{
		get
		{
			string text = this.cl_unassigned;
			string text2 = this.sv_unassigned;
			return (!string.IsNullOrEmpty(text)) ? text : ((!string.IsNullOrEmpty(text2)) ? text2 : null);
		}
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00026B40 File Offset: 0x00024D40
	internal string GetClassName(bool player, bool local)
	{
		if (player)
		{
			if (local)
			{
				return (!string.IsNullOrEmpty(this.localPlayer)) ? this.localPlayer : null;
			}
			return (!string.IsNullOrEmpty(this.remotePlayer)) ? this.remotePlayer : null;
		}
		else
		{
			if (local)
			{
				return (!string.IsNullOrEmpty(this.localAI)) ? this.localAI : null;
			}
			return (!string.IsNullOrEmpty(this.remoteAI)) ? this.remoteAI : null;
		}
	}

	// Token: 0x04000648 RID: 1608
	[SerializeField]
	public string localPlayer;

	// Token: 0x04000649 RID: 1609
	[SerializeField]
	public string remotePlayer;

	// Token: 0x0400064A RID: 1610
	[SerializeField]
	public string localAI;

	// Token: 0x0400064B RID: 1611
	[SerializeField]
	public string remoteAI;

	// Token: 0x0400064C RID: 1612
	[SerializeField]
	public string cl_unassigned;

	// Token: 0x0400064D RID: 1613
	[SerializeField]
	public string sv_unassigned;
}
