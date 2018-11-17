using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class HeatZone : MonoBehaviour
{
	// Token: 0x060002C4 RID: 708 RVA: 0x0000E954 File Offset: 0x0000CB54
	public void SetOn(bool on)
	{
		this._isOn = on;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000E960 File Offset: 0x0000CB60
	public Metabolism GetFromCollider(Collider other)
	{
		return other.gameObject.GetComponent<Metabolism>();
	}

	// Token: 0x040001B4 RID: 436
	private bool _isOn;
}
