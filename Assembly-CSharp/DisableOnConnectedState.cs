using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class DisableOnConnectedState : MonoBehaviour
{
	// Token: 0x06000325 RID: 805 RVA: 0x0000F970 File Offset: 0x0000DB70
	public static void OnDisconnected()
	{
		global::DisableOnConnectedState.connectedStatus = false;
		Object[] array = global::Resources.FindObjectsOfTypeAll(typeof(global::DisableOnConnectedState));
		foreach (global::DisableOnConnectedState disableOnConnectedState in array)
		{
			if (disableOnConnectedState.gameObject == null)
			{
				return;
			}
			disableOnConnectedState.DoOnDisconnected();
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0000F9CC File Offset: 0x0000DBCC
	public static void OnConnected()
	{
		global::DisableOnConnectedState.connectedStatus = true;
		Object[] array = global::Resources.FindObjectsOfTypeAll(typeof(global::DisableOnConnectedState));
		foreach (global::DisableOnConnectedState disableOnConnectedState in array)
		{
			if (disableOnConnectedState.gameObject == null)
			{
				return;
			}
			disableOnConnectedState.DoOnConnected();
		}
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0000FA28 File Offset: 0x0000DC28
	private void Start()
	{
		if (global::DisableOnConnectedState.connectedStatus)
		{
			this.DoOnConnected();
		}
		else
		{
			this.DoOnDisconnected();
		}
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0000FA48 File Offset: 0x0000DC48
	protected void DoOnDisconnected()
	{
		base.gameObject.SetActive(this.disableWhenConnected);
		global::dfControl component = base.gameObject.GetComponent<global::dfControl>();
		if (component)
		{
			if (!this.disableWhenConnected)
			{
				component.Hide();
			}
			else
			{
				component.Show();
			}
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0000FA9C File Offset: 0x0000DC9C
	protected void DoOnConnected()
	{
		base.gameObject.SetActive(!this.disableWhenConnected);
		global::dfControl component = base.gameObject.GetComponent<global::dfControl>();
		if (component)
		{
			if (this.disableWhenConnected)
			{
				component.Hide();
			}
			else
			{
				component.Show();
			}
		}
	}

	// Token: 0x04000210 RID: 528
	protected static bool connectedStatus;

	// Token: 0x04000211 RID: 529
	public bool disableWhenConnected;
}
