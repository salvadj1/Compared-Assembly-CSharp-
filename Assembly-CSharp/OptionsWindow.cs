using System;
using UnityEngine;

// Token: 0x02000402 RID: 1026
public class OptionsWindow : MonoBehaviour
{
	// Token: 0x060025A6 RID: 9638 RVA: 0x00090BCC File Offset: 0x0008EDCC
	private void Start()
	{
	}

	// Token: 0x060025A7 RID: 9639 RVA: 0x00090BD0 File Offset: 0x0008EDD0
	public void DoApply()
	{
		base.BroadcastMessage("UpdateConVars");
		ConsoleSystem.Run("config.save", false);
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x00090BEC File Offset: 0x0008EDEC
	public void DoOK()
	{
		this.DoApply();
	}

	// Token: 0x060025A9 RID: 9641 RVA: 0x00090BF4 File Offset: 0x0008EDF4
	public void OnWindowVisibleChanged()
	{
		if (base.GetComponent<dfPanel>().IsVisible)
		{
			base.BroadcastMessage("UpdateFromConVar");
		}
	}
}
