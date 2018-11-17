using System;
using uLink;

// Token: 0x020000BA RID: 186
public class Player : global::IDLocalCharacter
{
	// Token: 0x06000417 RID: 1047 RVA: 0x0001412C File Offset: 0x0001232C
	private void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		if (base.networkView.isMine)
		{
			global::GameTip componentInChildren = base.GetComponentInChildren<global::GameTip>();
			if (componentInChildren)
			{
				componentInChildren.enabled = false;
			}
		}
		if (!base.networkView.isMine)
		{
			global::GameTip componentInChildren2 = base.GetComponentInChildren<global::GameTip>();
			if (componentInChildren2 && base.playerClient)
			{
				componentInChildren2.text = base.playerClient.userName;
			}
		}
	}
}
