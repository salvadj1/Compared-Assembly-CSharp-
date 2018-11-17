using System;
using UnityEngine;

// Token: 0x02000769 RID: 1897
[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Object")]
public class UICheckboxControlledObject : MonoBehaviour
{
	// Token: 0x06004504 RID: 17668 RVA: 0x0010E9E0 File Offset: 0x0010CBE0
	private void OnActivate(bool isActive)
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, (!this.inverse) ? isActive : (!isActive));
		}
	}

	// Token: 0x0400254C RID: 9548
	public GameObject target;

	// Token: 0x0400254D RID: 9549
	public bool inverse;
}
