using System;
using UnityEngine;

// Token: 0x02000698 RID: 1688
public class ViewModelAttachment : MonoBehaviour
{
	// Token: 0x17000B48 RID: 2888
	// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000DB110 File Offset: 0x000D9310
	// (set) Token: 0x06003A75 RID: 14965 RVA: 0x000DB118 File Offset: 0x000D9318
	public ViewModel viewModel
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

	// Token: 0x06003A76 RID: 14966 RVA: 0x000DB184 File Offset: 0x000D9384
	private void OnDestroy()
	{
		if (this.boundViewModel)
		{
			this.boundViewModel.RemoveRenderers(this.renderers);
		}
	}

	// Token: 0x04001EE3 RID: 7907
	[SerializeField]
	private SkinnedMeshRenderer[] renderers;

	// Token: 0x04001EE4 RID: 7908
	[NonSerialized]
	private ViewModel boundViewModel;
}
