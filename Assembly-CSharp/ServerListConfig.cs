using System;
using JSON;
using UnityEngine;

// Token: 0x02000858 RID: 2136
public class ServerListConfig : MonoBehaviour
{
	// Token: 0x06004B38 RID: 19256 RVA: 0x001492D0 File Offset: 0x001474D0
	private void Start()
	{
		ServerListConfig.instance = this;
		this.config = base.GetComponent<GameConfig>();
	}

	// Token: 0x06004B39 RID: 19257 RVA: 0x001492E4 File Offset: 0x001474E4
	public static bool IsBadServer(string serverIP)
	{
		if (ServerListConfig.instance == null)
		{
			return false;
		}
		if (!ServerListConfig.instance.config.isLoaded)
		{
			return false;
		}
		JSON.Array array = ServerListConfig.instance.config.json.GetArray("servers_blacklist");
		foreach (Value value in array)
		{
			if (serverIP.StartsWith(value.Str))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004B3A RID: 19258 RVA: 0x00149398 File Offset: 0x00147598
	public static bool IsOfficialServer(string serverIP)
	{
		if (ServerListConfig.instance == null)
		{
			return false;
		}
		if (!ServerListConfig.instance.config.isLoaded)
		{
			return false;
		}
		JSON.Array array = ServerListConfig.instance.config.json.GetArray("servers_official");
		foreach (Value value in array)
		{
			if (serverIP.StartsWith(value.Str))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04002C28 RID: 11304
	public static ServerListConfig instance;

	// Token: 0x04002C29 RID: 11305
	private GameConfig config;
}
