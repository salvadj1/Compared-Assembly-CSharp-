using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class AuthorPalletObject
{
	// Token: 0x06000047 RID: 71 RVA: 0x00002EE4 File Offset: 0x000010E4
	public bool Validate(AuthorCreation creation)
	{
		return this.validator == null || this.validator(creation, this);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00002F04 File Offset: 0x00001104
	public bool Create(AuthorCreation creation, out AuthorPeice peice)
	{
		if (this.creator == null)
		{
			peice = null;
			return false;
		}
		peice = this.creator(creation, this);
		return peice;
	}

	// Token: 0x04000027 RID: 39
	public AuthorPalletObject.Validator validator;

	// Token: 0x04000028 RID: 40
	public AuthorPalletObject.Creator creator;

	// Token: 0x04000029 RID: 41
	public GUIContent guiContent;

	// Token: 0x0200085E RID: 2142
	// (Invoke) Token: 0x06004B50 RID: 19280
	public delegate bool Validator(AuthorCreation creation, AuthorPalletObject obj);

	// Token: 0x0200085F RID: 2143
	// (Invoke) Token: 0x06004B54 RID: 19284
	public delegate AuthorPeice Creator(AuthorCreation creation, AuthorPalletObject obj);
}
