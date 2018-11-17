using System;
using UnityEngine;

// Token: 0x0200075D RID: 1885
public class ViewModelAttachment : MonoBehaviour
{
	// Token: 0x17000BCA RID: 3018
	// (get) Token: 0x06003E6C RID: 15980 RVA: 0x000E3AF0 File Offset: 0x000E1CF0
	// (set) Token: 0x06003E6D RID: 15981 RVA: 0x000E3AF8 File Offset: 0x000E1CF8
	public global::ViewModel viewModel
	{
		get
		{
			return this.boundViewModel;
		}
		set
		{
			if (!object.ReferenceEquals(this.boundViewModel, value))
			{
				if (this.boundViewModel)
				{
					this.boundViewModel.RemoveRenderers(this.renderers);
					this.boundViewModel = null;
				}
				if (value)
				{
					this.boundViewModel = value;
					this.boundViewModel.AddRenderers(this.renderers);
				}
			}
		}
	}

	// Token: 0x06003E6E RID: 15982 RVA: 0x000E3B64 File Offset: 0x000E1D64
	private void OnDestroy()
	{
		if (this.boundViewModel)
		{
			this.boundViewModel.RemoveRenderers(this.renderers);
		}
	}

	// Token: 0x040020DB RID: 8411
	[SerializeField]
	private SkinnedMeshRenderer[] renderers;

	// Token: 0x040020DC RID: 8412
	[NonSerialized]
	private global::ViewModel boundViewModel;
}
