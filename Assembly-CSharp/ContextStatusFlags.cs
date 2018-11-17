using System;

// Token: 0x02000471 RID: 1137
[Flags]
public enum ContextStatusFlags
{
	// Token: 0x040014EE RID: 5358
	ObjectBusy = 1,
	// Token: 0x040014EF RID: 5359
	ObjectBroken = 2,
	// Token: 0x040014F0 RID: 5360
	ObjectEmpty = 4,
	// Token: 0x040014F1 RID: 5361
	ObjectOccupied = 8,
	// Token: 0x040014F2 RID: 5362
	SpriteFlag0 = 536870912,
	// Token: 0x040014F3 RID: 5363
	SpriteFlag1 = 1073741824
}
