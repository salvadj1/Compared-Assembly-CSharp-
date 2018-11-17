using System;

// Token: 0x02000447 RID: 1095
public interface ICameraFX
{
	// Token: 0x0600280D RID: 10253
	void PreCull();

	// Token: 0x0600280E RID: 10254
	void PostRender();

	// Token: 0x0600280F RID: 10255
	void OnViewModelChange(ViewModel viewModel);
}
