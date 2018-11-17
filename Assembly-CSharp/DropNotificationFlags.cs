using System;

// Token: 0x020007C4 RID: 1988
[Flags]
public enum DropNotificationFlags
{
	// Token: 0x0400278E RID: 10126
	DragDrop = 1,
	// Token: 0x0400278F RID: 10127
	DragLand = 2,
	// Token: 0x04002790 RID: 10128
	DragReverse = 4,
	// Token: 0x04002791 RID: 10129
	AltDrop = 8,
	// Token: 0x04002792 RID: 10130
	AltLand = 16,
	// Token: 0x04002793 RID: 10131
	AltReverse = 32,
	// Token: 0x04002794 RID: 10132
	MidDrop = 64,
	// Token: 0x04002795 RID: 10133
	MidLand = 128,
	// Token: 0x04002796 RID: 10134
	MidReverse = 256,
	// Token: 0x04002797 RID: 10135
	DragHover = 512,
	// Token: 0x04002798 RID: 10136
	LandHover = 1024,
	// Token: 0x04002799 RID: 10137
	ReverseHover = 2048,
	// Token: 0x0400279A RID: 10138
	RegularHover = 4096,
	// Token: 0x0400279B RID: 10139
	DragLandOutside = 8192
}
