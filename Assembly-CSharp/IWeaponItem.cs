using System;

// Token: 0x020006AC RID: 1708
public interface IWeaponItem : global::IHeldItem, global::IInventoryItem
{
	// Token: 0x17000B4A RID: 2890
	// (get) Token: 0x06003A2B RID: 14891
	bool canPrimaryAttack { get; }

	// Token: 0x06003A2C RID: 14892
	void PrimaryAttack(ref global::HumanController.InputSample sample);

	// Token: 0x17000B4B RID: 2891
	// (get) Token: 0x06003A2D RID: 14893
	bool canSecondaryAttack { get; }

	// Token: 0x06003A2E RID: 14894
	void SecondaryAttack(ref global::HumanController.InputSample sample);

	// Token: 0x06003A2F RID: 14895
	void Reload(ref global::HumanController.InputSample sample);

	// Token: 0x17000B4C RID: 2892
	// (get) Token: 0x06003A30 RID: 14896
	bool canAim { get; }

	// Token: 0x17000B4D RID: 2893
	// (get) Token: 0x06003A31 RID: 14897
	bool deployed { get; }

	// Token: 0x17000B4E RID: 2894
	// (get) Token: 0x06003A32 RID: 14898
	int possibleReloadCount { get; }

	// Token: 0x17000B4F RID: 2895
	// (get) Token: 0x06003A33 RID: 14899
	// (set) Token: 0x06003A34 RID: 14900
	float nextPrimaryAttackTime { get; set; }

	// Token: 0x17000B50 RID: 2896
	// (get) Token: 0x06003A35 RID: 14901
	// (set) Token: 0x06003A36 RID: 14902
	float nextSecondaryAttackTime { get; set; }

	// Token: 0x17000B51 RID: 2897
	// (get) Token: 0x06003A37 RID: 14903
	// (set) Token: 0x06003A38 RID: 14904
	float deployFinishedTime { get; set; }

	// Token: 0x06003A39 RID: 14905
	bool ValidatePrimaryMessageTime(double timestamp);
}
