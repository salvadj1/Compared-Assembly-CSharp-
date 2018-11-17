using System;

// Token: 0x020004AA RID: 1194
public class VMAttachmentSocketOverride : ViewModelAttachment
{
	// Token: 0x06002A05 RID: 10757 RVA: 0x000A490C File Offset: 0x000A2B0C
	private void OnDrawGizmosSelected()
	{
		this.socketOverride.DrawGizmos("socketOverride");
	}

	// Token: 0x040015F7 RID: 5623
	public Socket.CameraSpace socketOverride;
}
