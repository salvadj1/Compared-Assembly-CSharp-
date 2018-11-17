using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
[Serializable]
public class ControllerClassesConfigurations
{
	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002A87C File Offset: 0x00028A7C
	internal string unassignedClassName
	{
		get
		{
			string text = this.cl_unassigned;
			string text2 = this.sv_unassigned;
			return (!string.IsNullOrEmpty(text)) ? text : ((!string.IsNullOrEmpty(text2)) ? text2 : null);
		}
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0002A8BC File Offset: 0x00028ABC
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

	// Token: 0x04000757 RID: 1879
	[SerializeField]
	public string localPlayer;

	// Token: 0x04000758 RID: 1880
	[SerializeField]
	public string remotePlayer;

	// Token: 0x04000759 RID: 1881
	[SerializeField]
	public string localAI;

	// Token: 0x0400075A RID: 1882
	[SerializeField]
	public string remoteAI;

	// Token: 0x0400075B RID: 1883
	[SerializeField]
	public string cl_unassigned;

	// Token: 0x0400075C RID: 1884
	[SerializeField]
	public string sv_unassigned;
}
