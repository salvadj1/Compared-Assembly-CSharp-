using System;

// Token: 0x02000472 RID: 1138
public static class ContextStatus
{
	// Token: 0x060028E1 RID: 10465 RVA: 0x000A0524 File Offset: 0x0009E724
	public static ContextStatusFlags GetSpriteFlags(this ContextStatusFlags statusFlags)
	{
		return statusFlags & (ContextStatusFlags.SpriteFlag0 | ContextStatusFlags.SpriteFlag1);
	}

	// Token: 0x060028E2 RID: 10466 RVA: 0x000A0530 File Offset: 0x0009E730
	public static ContextStatusFlags CopyWithSpriteSetting(this ContextStatusFlags statusFlags, ContextStatusFlags SPRITE_SETTING)
	{
		return (statusFlags & ~(ContextStatusFlags.SpriteFlag0 | ContextStatusFlags.SpriteFlag1)) | (SPRITE_SETTING & (ContextStatusFlags.SpriteFlag0 | ContextStatusFlags.SpriteFlag1));
	}

	// Token: 0x040014F4 RID: 5364
	public const ContextStatusFlags ObjectBusy = ContextStatusFlags.ObjectBusy;

	// Token: 0x040014F5 RID: 5365
	public const ContextStatusFlags ObjectBroken = ContextStatusFlags.ObjectBroken;

	// Token: 0x040014F6 RID: 5366
	public const ContextStatusFlags ObjectEmpty = ContextStatusFlags.ObjectEmpty;

	// Token: 0x040014F7 RID: 5367
	public const ContextStatusFlags ObjectOccupied = ContextStatusFlags.ObjectOccupied;

	// Token: 0x040014F8 RID: 5368
	public const ContextStatusFlags SPRITE_DEFAULT = (ContextStatusFlags)0;

	// Token: 0x040014F9 RID: 5369
	public const ContextStatusFlags SPRITE_FRACTION = ContextStatusFlags.SpriteFlag0;

	// Token: 0x040014FA RID: 5370
	public const ContextStatusFlags SPRITE_NEVER = ContextStatusFlags.SpriteFlag1;

	// Token: 0x040014FB RID: 5371
	public const ContextStatusFlags SPRITE_ALWAYS = ContextStatusFlags.SpriteFlag0 | ContextStatusFlags.SpriteFlag1;

	// Token: 0x040014FC RID: 5372
	public const ContextStatusFlags MASK_SPRITE = ContextStatusFlags.SpriteFlag0 | ContextStatusFlags.SpriteFlag1;
}
