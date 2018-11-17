using System;
using UnityEngine;

// Token: 0x0200084A RID: 2122
[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Component")]
public class UICheckboxControlledComponent : MonoBehaviour
{
	// Token: 0x06004963 RID: 18787 RVA: 0x0011830C File Offset: 0x0011650C
	private void OnActivate(bool isActive)
	{
		if (base.enabled && this.target != null)
		{
			this.target.enabled = ((!this.inverse) ? isActive : (!isActive));
		}
	}

	// Token: 0x04002781 RID: 10113
	public MonoBehaviour target;

	// Token: 0x04002782 RID: 10114
	public bool inverse;
}
