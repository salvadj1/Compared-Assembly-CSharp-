using System;
using System.Runtime.Serialization;

// Token: 0x020003B6 RID: 950
[Serializable]
public class NetMainPrefabNameException : ArgumentOutOfRangeException
{
	// Token: 0x0600215D RID: 8541 RVA: 0x0007AD40 File Offset: 0x00078F40
	public NetMainPrefabNameException()
	{
	}

	// Token: 0x0600215E RID: 8542 RVA: 0x0007AD48 File Offset: 0x00078F48
	public NetMainPrefabNameException(string parameter) : base(parameter)
	{
	}

	// Token: 0x0600215F RID: 8543 RVA: 0x0007AD54 File Offset: 0x00078F54
	public NetMainPrefabNameException(string parameter, string message) : base(parameter, message)
	{
	}

	// Token: 0x06002160 RID: 8544 RVA: 0x0007AD60 File Offset: 0x00078F60
	public NetMainPrefabNameException(string parameter, string value, string message) : base(parameter, value, message)
	{
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x0007AD6C File Offset: 0x00078F6C
	public NetMainPrefabNameException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x0007AD78 File Offset: 0x00078F78
	protected NetMainPrefabNameException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
