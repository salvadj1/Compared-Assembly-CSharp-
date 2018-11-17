using System;

// Token: 0x02000222 RID: 546
public enum UseResponse : sbyte
{
	// Token: 0x0400097C RID: 2428
	Pass_Unchecked,
	// Token: 0x0400097D RID: 2429
	Pass_Checked,
	// Token: 0x0400097E RID: 2430
	Fail_Checked_OutOfOrder = -128,
	// Token: 0x0400097F RID: 2431
	Fail_Checked_UserIncompatible,
	// Token: 0x04000980 RID: 2432
	Fail_Checked_BadConfiguration,
	// Token: 0x04000981 RID: 2433
	Fail_Checked_BadResult,
	// Token: 0x04000982 RID: 2434
	Fail_CheckException = -16,
	// Token: 0x04000983 RID: 2435
	Fail_EnterException,
	// Token: 0x04000984 RID: 2436
	Fail_Vacancy = -10,
	// Token: 0x04000985 RID: 2437
	Fail_Redundant,
	// Token: 0x04000986 RID: 2438
	Fail_UserDead,
	// Token: 0x04000987 RID: 2439
	Fail_Destroyed,
	// Token: 0x04000988 RID: 2440
	Fail_NotIUseable,
	// Token: 0x04000989 RID: 2441
	Fail_InvalidOperation,
	// Token: 0x0400098A RID: 2442
	Fail_NullOrMissingUser
}
