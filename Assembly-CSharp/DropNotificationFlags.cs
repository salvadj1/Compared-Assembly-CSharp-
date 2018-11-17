using System;

// Token: 0x020008B1 RID: 2225
[Flags]
public enum DropNotificationFlags
{
	// Token: 0x040029C8 RID: 10696
	DragDrop = 1,
	// Token: 0x040029C9 RID: 10697
	DragLand = 2,
	// Token: 0x040029CA RID: 10698
	DragReverse = 4,
	// Token: 0x040029CB RID: 10699
	AltDrop = 8,
	// Token: 0x040029CC RID: 10700
	AltLand = 16,
	// Token: 0x040029CD RID: 10701
	AltReverse = 32,
	// Token: 0x040029CE RID: 10702
	MidDrop = 64,
	// Token: 0x040029CF RID: 10703
	MidLand = 128,
	// Token: 0x040029D0 RID: 10704
	MidReverse = 256,
	// Token: 0x040029D1 RID: 10705
	DragHover = 512,
	// Token: 0x040029D2 RID: 10706
	LandHover = 1024,
	// Token: 0x040029D3 RID: 10707
	ReverseHover = 2048,
	// Token: 0x040029D4 RID: 10708
	RegularHover = 4096,
	// Token: 0x040029D5 RID: 10709
	DragLandOutside = 8192
}
