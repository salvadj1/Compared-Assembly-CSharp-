using System;
using UnityEngine;

// Token: 0x02000588 RID: 1416
public class HelloScript : MonoBehaviour
{
	// Token: 0x06002E4A RID: 11850 RVA: 0x000B1800 File Offset: 0x000AFA00
	private void Start()
	{
		Debug.Log("HELLO!:" + this.helloString + "from object: " + base.gameObject.name);
	}

	// Token: 0x040018DE RID: 6366
	public string helloString;
}
