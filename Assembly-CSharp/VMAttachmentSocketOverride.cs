using System;

// Token: 0x02000565 RID: 1381
public class VMAttachmentSocketOverride : global::ViewModelAttachment
{
	// Token: 0x06002DB7 RID: 11703 RVA: 0x000AC6A4 File Offset: 0x000AA8A4
	private void OnDrawGizmosSelected()
	{
		this.socketOverride.DrawGizmos("socketOverride");
	}

	// Token: 0x040017B4 RID: 6068
	public global::Socket.CameraSpace socketOverride;
}
