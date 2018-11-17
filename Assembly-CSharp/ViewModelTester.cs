using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200075E RID: 1886
public class ViewModelTester : MonoBehaviour
{
	// Token: 0x06003E70 RID: 15984 RVA: 0x000E3B90 File Offset: 0x000E1D90
	private IEnumerator Start()
	{
		for (int i = 0; i < 5; i++)
		{
			yield return null;
		}
		global::CameraFX.ReplaceViewModel(this.viewModel, false);
		yield break;
	}

	// Token: 0x040020DD RID: 8413
	public global::ViewModel viewModel;
}
