using System;
using uLink;

// Token: 0x020000A7 RID: 167
public class Player : IDLocalCharacter
{
	// Token: 0x0600039F RID: 927 RVA: 0x0001293C File Offset: 0x00010B3C
	private void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		if (base.networkView.isMine)
		{
			GameTip componentInChildren = base.GetComponentInChildren<GameTip>();
			if (componentInChildren)
			{
				componentInChildren.enabled = false;
			}
		}
		if (!base.networkView.isMine)
		{
			GameTip componentInChildren2 = base.GetComponentInChildren<GameTip>();
			if (componentInChildren2 && base.playerClient)
			{
				componentInChildren2.text = base.playerClient.userName;
			}
		}
	}
}
