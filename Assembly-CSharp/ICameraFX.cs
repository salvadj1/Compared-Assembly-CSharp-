using System;

// Token: 0x020004FD RID: 1277
public interface ICameraFX
{
	// Token: 0x06002B9D RID: 11165
	void PreCull();

	// Token: 0x06002B9E RID: 11166
	void PostRender();

	// Token: 0x06002B9F RID: 11167
	void OnViewModelChange(global::ViewModel viewModel);
}
