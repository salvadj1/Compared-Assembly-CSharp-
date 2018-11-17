using System;
using UnityEngine;

// Token: 0x0200084B RID: 2123
[AddComponentMenu("NGUI/Interaction/Checkbox Controlled Object")]
public class UICheckboxControlledObject : MonoBehaviour
{
	// Token: 0x06004965 RID: 18789 RVA: 0x00118360 File Offset: 0x00116560
	private void OnActivate(bool isActive)
	{
		if (this.target != null)
		{
			global::NGUITools.SetActive(this.target, (!this.inverse) ? isActive : (!isActive));
		}
	}

	// Token: 0x04002783 RID: 10115
	public GameObject target;

	// Token: 0x04002784 RID: 10116
	public bool inverse;
}
