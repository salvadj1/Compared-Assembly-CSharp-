using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000699 RID: 1689
public class ViewModelTester : MonoBehaviour
{
	// Token: 0x06003A78 RID: 14968 RVA: 0x000DB1B0 File Offset: 0x000D93B0
	private IEnumerator Start()
	{
		for (int i = 0; i < 5; i++)
		{
			yield return null;
		}
		CameraFX.ReplaceViewModel(this.viewModel, false);
		yield break;
	}

	// Token: 0x04001EE5 RID: 7909
	public ViewModel viewModel;
}
