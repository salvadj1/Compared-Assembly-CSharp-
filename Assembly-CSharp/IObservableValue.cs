using System;

// Token: 0x02000703 RID: 1795
public interface IObservableValue
{
	// Token: 0x17000CE5 RID: 3301
	// (get) Token: 0x06004172 RID: 16754
	object Value { get; }

	// Token: 0x17000CE6 RID: 3302
	// (get) Token: 0x06004173 RID: 16755
	bool HasChanged { get; }
}
