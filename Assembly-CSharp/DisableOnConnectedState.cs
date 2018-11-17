using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class DisableOnConnectedState : MonoBehaviour
{
	// Token: 0x060002B3 RID: 691 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
	public static void OnDisconnected()
	{
		DisableOnConnectedState.connectedStatus = false;
		Object[] array = Resources.FindObjectsOfTypeAll(typeof(DisableOnConnectedState));
		foreach (DisableOnConnectedState disableOnConnectedState in array)
		{
			if (disableOnConnectedState.gameObject == null)
			{
				return;
			}
			disableOnConnectedState.DoOnDisconnected();
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0000E424 File Offset: 0x0000C624
	public static void OnConnected()
	{
		DisableOnConnectedState.connectedStatus = true;
		Object[] array = Resources.FindObjectsOfTypeAll(typeof(DisableOnConnectedState));
		foreach (DisableOnConnectedState disableOnConnectedState in array)
		{
			if (disableOnConnectedState.gameObject == null)
			{
				return;
			}
			disableOnConnectedState.DoOnConnected();
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0000E480 File Offset: 0x0000C680
	private void Start()
	{
		if (DisableOnConnectedState.connectedStatus)
		{
			this.DoOnConnected();
		}
		else
		{
			this.DoOnDisconnected();
		}
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
	protected void DoOnDisconnected()
	{
		base.gameObject.SetActive(this.disableWhenConnected);
		dfControl component = base.gameObject.GetComponent<dfControl>();
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

	// Token: 0x060002B7 RID: 695 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
	protected void DoOnConnected()
	{
		base.gameObject.SetActive(!this.disableWhenConnected);
		dfControl component = base.gameObject.GetComponent<dfControl>();
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

	// Token: 0x040001AE RID: 430
	protected static bool connectedStatus;

	// Token: 0x040001AF RID: 431
	public bool disableWhenConnected;
}
