using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class HeatZone : MonoBehaviour
{
	// Token: 0x06000336 RID: 822 RVA: 0x0000FEFC File Offset: 0x0000E0FC
	public void SetOn(bool on)
	{
		this._isOn = on;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0000FF08 File Offset: 0x0000E108
	public global::Metabolism GetFromCollider(Collider other)
	{
		return other.gameObject.GetComponent<global::Metabolism>();
	}

	// Token: 0x04000216 RID: 534
	private bool _isOn;
}
