using System;
using UnityEngine;

// Token: 0x0200039D RID: 925
[AddComponentMenu("")]
public class VisManager : MonoBehaviour
{
	// Token: 0x1700088F RID: 2191
	// (get) Token: 0x06002319 RID: 8985 RVA: 0x00087528 File Offset: 0x00085728
	public static bool guardedUpdate
	{
		get
		{
			return VisManager.isUpdatingVisiblity;
		}
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x00087530 File Offset: 0x00085730
	private void Reset()
	{
		Debug.LogError("REMOVE ME NOW, I GET GENERATED AT RUN TIME", this);
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x00087540 File Offset: 0x00085740
	private void Update()
	{
		if (!VisManager.isUpdatingVisiblity)
		{
			VisManager.isUpdatingVisiblity = true;
			try
			{
				VisNode.Process();
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("{0}\n-- Vis data potentially compromised\n", arg));
			}
			finally
			{
				VisManager.isUpdatingVisiblity = false;
			}
		}
	}

	// Token: 0x040010AB RID: 4267
	private static bool isUpdatingVisiblity;
}
