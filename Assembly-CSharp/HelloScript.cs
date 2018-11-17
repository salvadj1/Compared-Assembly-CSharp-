using System;
using UnityEngine;

// Token: 0x020004CD RID: 1229
public class HelloScript : MonoBehaviour
{
	// Token: 0x06002A98 RID: 10904 RVA: 0x000A9A68 File Offset: 0x000A7C68
	private void Start()
	{
		Debug.Log("HELLO!:" + this.helloString + "from object: " + base.gameObject.name);
	}

	// Token: 0x04001721 RID: 5921
	public string helloString;
}
