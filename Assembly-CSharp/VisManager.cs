using System;
using UnityEngine;

// Token: 0x0200044A RID: 1098
[AddComponentMenu("")]
public class VisManager : MonoBehaviour
{
	// Token: 0x170008ED RID: 2285
	// (get) Token: 0x0600267B RID: 9851 RVA: 0x0008C924 File Offset: 0x0008AB24
	public static bool guardedUpdate
	{
		get
		{
			return global::VisManager.isUpdatingVisiblity;
		}
	}

	// Token: 0x0600267C RID: 9852 RVA: 0x0008C92C File Offset: 0x0008AB2C
	private void Reset()
	{
		Debug.LogError("REMOVE ME NOW, I GET GENERATED AT RUN TIME", this);
	}

	// Token: 0x0600267D RID: 9853 RVA: 0x0008C93C File Offset: 0x0008AB3C
	private void Update()
	{
		if (!global::VisManager.isUpdatingVisiblity)
		{
			global::VisManager.isUpdatingVisiblity = true;
			try
			{
				global::VisNode.Process();
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("{0}\n-- Vis data potentially compromised\n", arg));
			}
			finally
			{
				global::VisManager.isUpdatingVisiblity = false;
			}
		}
	}

	// Token: 0x04001211 RID: 4625
	private static bool isUpdatingVisiblity;
}
