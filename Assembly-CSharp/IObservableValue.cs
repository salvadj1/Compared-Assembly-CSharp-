using System;

// Token: 0x020007D7 RID: 2007
public interface IObservableValue
{
	// Token: 0x17000D6D RID: 3437
	// (get) Token: 0x0600459A RID: 17818
	object Value { get; }

	// Token: 0x17000D6E RID: 3438
	// (get) Token: 0x0600459B RID: 17819
	bool HasChanged { get; }
}
