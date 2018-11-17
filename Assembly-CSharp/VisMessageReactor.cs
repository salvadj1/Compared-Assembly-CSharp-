using System;
using UnityEngine;

// Token: 0x0200039E RID: 926
public sealed class VisMessageReactor : VisReactor
{
	// Token: 0x0600231D RID: 8989 RVA: 0x00087624 File Offset: 0x00085824
	private void Exec(string message, VisNode arg, VisMessageInfo.Kind kind)
	{
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x00087628 File Offset: 0x00085828
	private void Exec(string message, VisMessageInfo.Kind kind)
	{
		this.Exec(message, null, kind);
	}

	// Token: 0x0600231F RID: 8991 RVA: 0x00087634 File Offset: 0x00085834
	protected override void React_AwareEnter()
	{
		this.Exec(this.awareEnter, VisMessageInfo.Kind.SeeEnter);
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x00087644 File Offset: 0x00085844
	protected override void React_AwareExit()
	{
		this.Exec(this.awareEnter, VisMessageInfo.Kind.SeeExit);
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x00087654 File Offset: 0x00085854
	protected override void React_SeeAdd(VisNode node)
	{
		this.Exec(this.awareEnter, node, VisMessageInfo.Kind.SeeAdd);
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x00087664 File Offset: 0x00085864
	protected override void React_SeeRemove(VisNode node)
	{
		this.Exec(this.awareEnter, node, VisMessageInfo.Kind.SeeRemove);
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x00087674 File Offset: 0x00085874
	protected override void React_SpectatedEnter()
	{
		this.Exec(this.awareEnter, VisMessageInfo.Kind.SpectatedEnter);
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x00087684 File Offset: 0x00085884
	protected override void React_SpectatedExit()
	{
		this.Exec(this.awareEnter, VisMessageInfo.Kind.SpectatorExit);
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x00087694 File Offset: 0x00085894
	protected override void React_SpectatorAdd(VisNode node)
	{
		this.Exec(this.awareEnter, node, VisMessageInfo.Kind.SpectatorAdd);
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x000876A4 File Offset: 0x000858A4
	protected override void React_SpectatorRemove(VisNode node)
	{
		this.Exec(this.awareEnter, node, VisMessageInfo.Kind.SpectatorRemove);
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x000876B8 File Offset: 0x000858B8
	private new void Reset()
	{
		base.Reset();
	}

	// Token: 0x040010AC RID: 4268
	public GameObject messageReceiver;

	// Token: 0x040010AD RID: 4269
	public string awareEnter = "Vis_Sight_Enter";

	// Token: 0x040010AE RID: 4270
	public string seeAdd = "Vis_Sight_Add";

	// Token: 0x040010AF RID: 4271
	public string seeRemove = "Vis_Sight_Remove";

	// Token: 0x040010B0 RID: 4272
	public string awareExit = "Vis_Sight_Exit";

	// Token: 0x040010B1 RID: 4273
	public string spectatedEnter = "Vis_Spect_Enter";

	// Token: 0x040010B2 RID: 4274
	public string spectatorAdd = "Vis_Spect_Add";

	// Token: 0x040010B3 RID: 4275
	public string spectatorRemove = "Vis_Spect_Remove";

	// Token: 0x040010B4 RID: 4276
	public string spectatedExit = "Vis_Spect_Exit";
}
