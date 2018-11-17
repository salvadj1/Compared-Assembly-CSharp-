using System;
using UnityEngine;

// Token: 0x02000768 RID: 1896
[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Component")]
public class UICheckboxControlledComponent : MonoBehaviour
{
	// Token: 0x06004502 RID: 17666 RVA: 0x0010E98C File Offset: 0x0010CB8C
	private void OnActivate(bool isActive)
	{
		if (base.enabled && this.target != null)
		{
			this.target.enabled = ((!this.inverse) ? isActive : (!isActive));
		}
	}

	// Token: 0x0400254A RID: 9546
	public MonoBehaviour target;

	// Token: 0x0400254B RID: 9547
	public bool inverse;
}
