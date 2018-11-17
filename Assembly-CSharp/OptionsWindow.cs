using System;
using UnityEngine;

// Token: 0x020004B3 RID: 1203
public class OptionsWindow : MonoBehaviour
{
	// Token: 0x0600291E RID: 10526 RVA: 0x00096A04 File Offset: 0x00094C04
	private void Start()
	{
	}

	// Token: 0x0600291F RID: 10527 RVA: 0x00096A08 File Offset: 0x00094C08
	public void DoApply()
	{
		base.BroadcastMessage("UpdateConVars");
		global::ConsoleSystem.Run("config.save", false);
	}

	// Token: 0x06002920 RID: 10528 RVA: 0x00096A24 File Offset: 0x00094C24
	public void DoOK()
	{
		this.DoApply();
	}

	// Token: 0x06002921 RID: 10529 RVA: 0x00096A2C File Offset: 0x00094C2C
	public void OnWindowVisibleChanged()
	{
		if (base.GetComponent<global::dfPanel>().IsVisible)
		{
			base.BroadcastMessage("UpdateFromConVar");
		}
	}
}
