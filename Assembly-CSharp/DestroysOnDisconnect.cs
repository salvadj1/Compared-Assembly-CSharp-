using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public sealed class DestroysOnDisconnect : MonoBehaviour
{
	// Token: 0x060019FB RID: 6651 RVA: 0x000650BC File Offset: 0x000632BC
	private void Awake()
	{
		if (!this.inList)
		{
			this.inList = true;
			try
			{
				global::DestroysOnDisconnect.List.all.Add(this);
			}
			catch
			{
				this.inList = false;
				throw;
			}
		}
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x00065118 File Offset: 0x00063318
	private void OnDestroy()
	{
		if (this.inList)
		{
			try
			{
				if (!global::DestroysOnDisconnect.List.all.Remove(this))
				{
					Debug.LogWarning("serious problem, script reload?", this);
				}
			}
			finally
			{
				this.inList = false;
			}
		}
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x00065174 File Offset: 0x00063374
	private void DestroyManually()
	{
		if (this.inList)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x0006518C File Offset: 0x0006338C
	public static void ApplyToGameObject(GameObject gameObject)
	{
		global::DestroysOnDisconnect component = gameObject.GetComponent<global::DestroysOnDisconnect>();
		if (!component)
		{
			gameObject.AddComponent<global::DestroysOnDisconnect>();
		}
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x000651B4 File Offset: 0x000633B4
	private void uLink_OnDisconnectedFromServer(uLink.NetworkDisconnection blowme)
	{
		this.DestroyManually();
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x000651BC File Offset: 0x000633BC
	public static void OnDisconnectedFromServer()
	{
		if (global::DestroysOnDisconnect.ListClassInitialized && global::DestroysOnDisconnect.List.all.Count > 0)
		{
			foreach (global::DestroysOnDisconnect destroysOnDisconnect in global::DestroysOnDisconnect.List.all.ToArray())
			{
				if (destroysOnDisconnect)
				{
					Object.Destroy(destroysOnDisconnect.gameObject);
				}
			}
		}
	}

	// Token: 0x04000E3D RID: 3645
	private static bool ListClassInitialized;

	// Token: 0x04000E3E RID: 3646
	private bool inList;

	// Token: 0x020002E5 RID: 741
	private static class List
	{
		// Token: 0x06001A01 RID: 6657 RVA: 0x0006521C File Offset: 0x0006341C
		static List()
		{
			global::DestroysOnDisconnect.ListClassInitialized = true;
		}

		// Token: 0x04000E3F RID: 3647
		public static readonly List<global::DestroysOnDisconnect> all = new List<global::DestroysOnDisconnect>();
	}
}
