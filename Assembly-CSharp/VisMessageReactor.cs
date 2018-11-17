using System;
using UnityEngine;

// Token: 0x0200044B RID: 1099
public sealed class VisMessageReactor : global::VisReactor
{
	// Token: 0x0600267F RID: 9855 RVA: 0x0008CA20 File Offset: 0x0008AC20
	private void Exec(string message, global::VisNode arg, global::VisMessageInfo.Kind kind)
	{
	}

	// Token: 0x06002680 RID: 9856 RVA: 0x0008CA24 File Offset: 0x0008AC24
	private void Exec(string message, global::VisMessageInfo.Kind kind)
	{
		this.Exec(message, null, kind);
	}

	// Token: 0x06002681 RID: 9857 RVA: 0x0008CA30 File Offset: 0x0008AC30
	protected override void React_AwareEnter()
	{
		this.Exec(this.awareEnter, global::VisMessageInfo.Kind.SeeEnter);
	}

	// Token: 0x06002682 RID: 9858 RVA: 0x0008CA40 File Offset: 0x0008AC40
	protected override void React_AwareExit()
	{
		this.Exec(this.awareEnter, global::VisMessageInfo.Kind.SeeExit);
	}

	// Token: 0x06002683 RID: 9859 RVA: 0x0008CA50 File Offset: 0x0008AC50
	protected override void React_SeeAdd(global::VisNode node)
	{
		this.Exec(this.awareEnter, node, global::VisMessageInfo.Kind.SeeAdd);
	}

	// Token: 0x06002684 RID: 9860 RVA: 0x0008CA60 File Offset: 0x0008AC60
	protected override void React_SeeRemove(global::VisNode node)
	{
		this.Exec(this.awareEnter, node, global::VisMessageInfo.Kind.SeeRemove);
	}

	// Token: 0x06002685 RID: 9861 RVA: 0x0008CA70 File Offset: 0x0008AC70
	protected override void React_SpectatedEnter()
	{
		this.Exec(this.awareEnter, global::VisMessageInfo.Kind.SpectatedEnter);
	}

	// Token: 0x06002686 RID: 9862 RVA: 0x0008CA80 File Offset: 0x0008AC80
	protected override void React_SpectatedExit()
	{
		this.Exec(this.awareEnter, global::VisMessageInfo.Kind.SpectatorExit);
	}

	// Token: 0x06002687 RID: 9863 RVA: 0x0008CA90 File Offset: 0x0008AC90
	protected override void React_SpectatorAdd(global::VisNode node)
	{
		this.Exec(this.awareEnter, node, global::VisMessageInfo.Kind.SpectatorAdd);
	}

	// Token: 0x06002688 RID: 9864 RVA: 0x0008CAA0 File Offset: 0x0008ACA0
	protected override void React_SpectatorRemove(global::VisNode node)
	{
		this.Exec(this.awareEnter, node, global::VisMessageInfo.Kind.SpectatorRemove);
	}

	// Token: 0x06002689 RID: 9865 RVA: 0x0008CAB4 File Offset: 0x0008ACB4
	private new void Reset()
	{
		base.Reset();
	}

	// Token: 0x04001212 RID: 4626
	public GameObject messageReceiver;

	// Token: 0x04001213 RID: 4627
	public string awareEnter = "Vis_Sight_Enter";

	// Token: 0x04001214 RID: 4628
	public string seeAdd = "Vis_Sight_Add";

	// Token: 0x04001215 RID: 4629
	public string seeRemove = "Vis_Sight_Remove";

	// Token: 0x04001216 RID: 4630
	public string awareExit = "Vis_Sight_Exit";

	// Token: 0x04001217 RID: 4631
	public string spectatedEnter = "Vis_Spect_Enter";

	// Token: 0x04001218 RID: 4632
	public string spectatorAdd = "Vis_Spect_Add";

	// Token: 0x04001219 RID: 4633
	public string spectatorRemove = "Vis_Spect_Remove";

	// Token: 0x0400121A RID: 4634
	public string spectatedExit = "Vis_Spect_Exit";
}
