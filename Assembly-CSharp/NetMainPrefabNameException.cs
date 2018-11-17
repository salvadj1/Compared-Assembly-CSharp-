using System;
using System.Runtime.Serialization;

// Token: 0x0200030D RID: 781
[Serializable]
public class NetMainPrefabNameException : ArgumentOutOfRangeException
{
	// Token: 0x06001E1B RID: 7707 RVA: 0x000762C0 File Offset: 0x000744C0
	public NetMainPrefabNameException()
	{
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x000762C8 File Offset: 0x000744C8
	public NetMainPrefabNameException(string parameter) : base(parameter)
	{
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x000762D4 File Offset: 0x000744D4
	public NetMainPrefabNameException(string parameter, string message) : base(parameter, message)
	{
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x000762E0 File Offset: 0x000744E0
	public NetMainPrefabNameException(string parameter, string value, string message) : base(parameter, value, message)
	{
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x000762EC File Offset: 0x000744EC
	public NetMainPrefabNameException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x000762F8 File Offset: 0x000744F8
	protected NetMainPrefabNameException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
