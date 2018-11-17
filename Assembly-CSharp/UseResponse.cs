using System;

// Token: 0x020001EF RID: 495
public enum UseResponse : sbyte
{
	// Token: 0x04000859 RID: 2137
	Pass_Unchecked,
	// Token: 0x0400085A RID: 2138
	Pass_Checked,
	// Token: 0x0400085B RID: 2139
	Fail_Checked_OutOfOrder = -128,
	// Token: 0x0400085C RID: 2140
	Fail_Checked_UserIncompatible,
	// Token: 0x0400085D RID: 2141
	Fail_Checked_BadConfiguration,
	// Token: 0x0400085E RID: 2142
	Fail_Checked_BadResult,
	// Token: 0x0400085F RID: 2143
	Fail_CheckException = -16,
	// Token: 0x04000860 RID: 2144
	Fail_EnterException,
	// Token: 0x04000861 RID: 2145
	Fail_Vacancy = -10,
	// Token: 0x04000862 RID: 2146
	Fail_Redundant,
	// Token: 0x04000863 RID: 2147
	Fail_UserDead,
	// Token: 0x04000864 RID: 2148
	Fail_Destroyed,
	// Token: 0x04000865 RID: 2149
	Fail_NotIUseable,
	// Token: 0x04000866 RID: 2150
	Fail_InvalidOperation,
	// Token: 0x04000867 RID: 2151
	Fail_NullOrMissingUser
}
