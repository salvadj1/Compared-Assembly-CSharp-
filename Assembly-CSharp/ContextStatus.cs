using System;

// Token: 0x02000528 RID: 1320
public static class ContextStatus
{
	// Token: 0x06002C71 RID: 11377 RVA: 0x000A64A4 File Offset: 0x000A46A4
	public static global::ContextStatusFlags GetSpriteFlags(this global::ContextStatusFlags statusFlags)
	{
		return statusFlags & (global::ContextStatusFlags.SpriteFlag0 | global::ContextStatusFlags.SpriteFlag1);
	}

	// Token: 0x06002C72 RID: 11378 RVA: 0x000A64B0 File Offset: 0x000A46B0
	public static global::ContextStatusFlags CopyWithSpriteSetting(this global::ContextStatusFlags statusFlags, global::ContextStatusFlags SPRITE_SETTING)
	{
		return (statusFlags & ~(global::ContextStatusFlags.SpriteFlag0 | global::ContextStatusFlags.SpriteFlag1)) | (SPRITE_SETTING & (global::ContextStatusFlags.SpriteFlag0 | global::ContextStatusFlags.SpriteFlag1));
	}

	// Token: 0x04001677 RID: 5751
	public const global::ContextStatusFlags ObjectBusy = global::ContextStatusFlags.ObjectBusy;

	// Token: 0x04001678 RID: 5752
	public const global::ContextStatusFlags ObjectBroken = global::ContextStatusFlags.ObjectBroken;

	// Token: 0x04001679 RID: 5753
	public const global::ContextStatusFlags ObjectEmpty = global::ContextStatusFlags.ObjectEmpty;

	// Token: 0x0400167A RID: 5754
	public const global::ContextStatusFlags ObjectOccupied = global::ContextStatusFlags.ObjectOccupied;

	// Token: 0x0400167B RID: 5755
	public const global::ContextStatusFlags SPRITE_DEFAULT = (global::ContextStatusFlags)0;

	// Token: 0x0400167C RID: 5756
	public const global::ContextStatusFlags SPRITE_FRACTION = global::ContextStatusFlags.SpriteFlag0;

	// Token: 0x0400167D RID: 5757
	public const global::ContextStatusFlags SPRITE_NEVER = global::ContextStatusFlags.SpriteFlag1;

	// Token: 0x0400167E RID: 5758
	public const global::ContextStatusFlags SPRITE_ALWAYS = global::ContextStatusFlags.SpriteFlag0 | global::ContextStatusFlags.SpriteFlag1;

	// Token: 0x0400167F RID: 5759
	public const global::ContextStatusFlags MASK_SPRITE = global::ContextStatusFlags.SpriteFlag0 | global::ContextStatusFlags.SpriteFlag1;
}
