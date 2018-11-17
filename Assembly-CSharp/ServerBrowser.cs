using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using POSIX;
using Rust.Steam;
using UnityEngine;

// Token: 0x020004B6 RID: 1206
public class ServerBrowser : MonoBehaviour
{
	// Token: 0x0600292F RID: 10543 RVA: 0x00096C64 File Offset: 0x00094E64
	private void Start()
	{
		for (int i = 0; i < this.servers.Length; i++)
		{
			this.servers[i] = new List<global::ServerBrowser.Server>();
		}
		this.AddServerCallback = new global::ServerBrowser.funcServerAdd(this.Add_Server);
		this.AddServerGC = GCHandle.Alloc(this.AddServerCallback);
		this.FinServerCallback = new global::ServerBrowser.funcServerFinish(this.RefreshFinished);
		this.RefreshFinishedGC = GCHandle.Alloc(this.FinServerCallback);
		base.BroadcastMessage("CategoryChanged", this.serverType);
		this.pagination.OnPageSwitch += this.OnPageSwitched;
		for (int j = 0; j < 50; j++)
		{
			this.NewServerItem();
		}
		this.ClearServers();
	}

	// Token: 0x06002930 RID: 10544 RVA: 0x00096D2C File Offset: 0x00094F2C
	public void OnPageSwitched(int iNewPage)
	{
		this.pageNumber = iNewPage;
		this.UpdateServerList();
	}

	// Token: 0x06002931 RID: 10545 RVA: 0x00096D3C File Offset: 0x00094F3C
	public void SwitchCategory(int catID)
	{
		if (this.serverType == catID)
		{
			return;
		}
		this.pageNumber = 0;
		this.currentServerChecksum = string.Empty;
		this.serverType = catID;
		base.BroadcastMessage("CategoryChanged", this.serverType);
		this.ClearServers();
		this.UpdateServerList();
	}

	// Token: 0x06002932 RID: 10546 RVA: 0x00096D94 File Offset: 0x00094F94
	private void OnEnable()
	{
		base.StartCoroutine(this.ServerListUpdater());
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x00096DA4 File Offset: 0x00094FA4
	public void ClearList()
	{
		this.pageNumber = 0;
		foreach (List<global::ServerBrowser.Server> list in this.servers)
		{
			list.Clear();
		}
		foreach (global::ServerCategory serverCategory in this.categoryButtons)
		{
			if (serverCategory)
			{
				serverCategory.UpdateServerCount(0);
			}
		}
		this.ClearServers();
		this.playerCount = 0;
		this.serverCount = 0;
		this.slotCount = 0;
		this.detailsLabel.Text = "...";
	}

	// Token: 0x06002934 RID: 10548 RVA: 0x00096E44 File Offset: 0x00095044
	public void ClearServers()
	{
		global::ServerItem[] componentsInChildren = this.serverContainer.GetComponentsInChildren<global::ServerItem>();
		foreach (global::ServerItem serverItem in componentsInChildren)
		{
			serverItem.gameObject.GetComponent<global::dfControl>().Hide();
			this.pooledServerItems.Enqueue(serverItem.gameObject);
		}
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x00096E98 File Offset: 0x00095098
	public void RefreshServerList()
	{
		this.refreshButton.IsEnabled = false;
		this.refreshButton.Opacity = 0.2f;
		global::SteamClient.Needed();
		this.ClearList();
		this.detailsLabel.Text = "Updating..";
		this.serverRefresh = global::ServerBrowser.SteamServers_Fetch(1069, this.AddServerCallback, this.FinServerCallback);
		if (this.serverRefresh == IntPtr.Zero)
		{
			Debug.Log("Error! Couldn't refresh servers!!");
		}
	}

	// Token: 0x06002936 RID: 10550 RVA: 0x00096F18 File Offset: 0x00095118
	public void OnFirstOpen()
	{
		if (this.firstOpened)
		{
			return;
		}
		if (!base.GetComponent<global::dfPanel>().IsVisible)
		{
			return;
		}
		this.firstOpened = true;
		this.RefreshServerList();
	}

	// Token: 0x06002937 RID: 10551 RVA: 0x00096F50 File Offset: 0x00095150
	private bool ShouldIgnoreServer(global::ServerBrowser.Server item)
	{
		string text = item.name.ToLower();
		if (text.Contains("[color"))
		{
			return true;
		}
		if (text.Contains("[sprite"))
		{
			return true;
		}
		if (text.Contains("--"))
		{
			return true;
		}
		if (text.Contains("%%"))
		{
			return true;
		}
		if (!char.IsLetterOrDigit(text[0]))
		{
			return true;
		}
		if (!char.IsLetterOrDigit(text[text.Length - 1]))
		{
			return true;
		}
		foreach (char c in text)
		{
			if (!char.IsLetterOrDigit(c))
			{
				if (c != '\'')
				{
					if (c != '[')
					{
						if (c != ']')
						{
							if (c != '|')
							{
								if (c != ' ')
								{
									if (c != '-')
									{
										if (c != '(')
										{
											if (c != '%')
											{
												if (c != ')')
												{
													if (c != '_')
													{
														if (c != '@')
														{
															if (c != '+')
															{
																if (c != '&')
																{
																	if (c != ':')
																	{
																		if (c != '/')
																		{
																			if (c != '.')
																			{
																				if (c != '?')
																				{
																					if (c != '#')
																					{
																						if (c != '!')
																						{
																							if (c != ',')
																							{
																								return true;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return item.currentplayers > item.maxplayers || item.currentplayers > 500;
	}

	// Token: 0x06002938 RID: 10552 RVA: 0x0009713C File Offset: 0x0009533C
	private void Add_Server(int iMaxPlayers, int iCurrentPlayers, int iPing, uint iLastPlayed, [MarshalAs(UnmanagedType.LPStr)] [In] string strHostname, [MarshalAs(UnmanagedType.LPStr)] [In] string strAddress, int iPort, int iQueryPort, [MarshalAs(UnmanagedType.LPStr)] [In] string tags, bool bPassworded, int iType)
	{
		string strName = strAddress + ":" + iPort.ToString();
		global::ServerBrowser.Server server = new global::ServerBrowser.Server();
		server.name = strHostname;
		server.address = strAddress;
		server.maxplayers = iMaxPlayers;
		server.currentplayers = iCurrentPlayers;
		server.ping = iPing;
		server.lastplayed = iLastPlayed;
		server.port = iPort;
		server.queryport = iQueryPort;
		server.fave = global::FavouriteList.Contains(strName);
		if (server.name.Length > 64)
		{
			server.name = server.name.Substring(0, 64);
		}
		if (this.ShouldIgnoreServer(server))
		{
			return;
		}
		this.playerCount += iCurrentPlayers;
		this.serverCount++;
		this.slotCount += iMaxPlayers;
		this.needsServerListUpdate = true;
		int num = (int)((float)this.playerCount / (float)this.slotCount * 100f);
		this.detailsLabel.Text = string.Concat(new string[]
		{
			"Found ",
			this.playerCount.ToString(),
			" players on ",
			this.serverCount.ToString(),
			" servers. We are at ",
			num.ToString(),
			"% capacity."
		});
		if (iType == 3)
		{
			this.servers[5].Add(server);
			this.categoryButtons[5].UpdateServerCount(this.servers[5].Count);
			return;
		}
		if (iType == 4)
		{
			int num2 = (int)POSIX.Time.ElapsedSecondsSince((int)server.lastplayed);
			string str = string.Empty;
			if (num2 < 60)
			{
				str = num2.ToString() + " seconds ago";
			}
			else if (num2 < 3600)
			{
				str = (num2 / 60).ToString() + " minutes ago";
			}
			else if (num2 < 172800)
			{
				str = (num2 / 60 / 60).ToString() + " hours ago";
			}
			else
			{
				str = (num2 / 60 / 60 / 24).ToString() + " days ago";
			}
			global::ServerBrowser.Server server2 = server;
			server2.name = server2.name + " (" + str + ")";
			this.servers[4].Add(server);
			this.categoryButtons[4].UpdateServerCount(this.servers[4].Count);
			return;
		}
		if (tags.Contains("official"))
		{
			this.servers[0].Add(server);
			this.categoryButtons[0].UpdateServerCount(this.servers[0].Count);
			return;
		}
		string[] array = tags.Split(new char[]
		{
			','
		});
		foreach (string text in array)
		{
			if (!text.StartsWith("mp"))
			{
				if (!text.StartsWith("cp"))
				{
					if (text.StartsWith("sg:"))
					{
						string s = text.Substring(3);
						ulong iGroupID;
						if (ulong.TryParse(s, NumberStyles.HexNumber, null, out iGroupID))
						{
							if (!Rust.Steam.SteamGroups.MemberOf(iGroupID))
							{
								return;
							}
							this.servers[3].Add(server);
							this.categoryButtons[3].UpdateServerCount(this.servers[3].Count);
							return;
						}
					}
				}
			}
		}
		if (tags.Contains("modded"))
		{
			this.servers[2].Add(server);
			this.categoryButtons[2].UpdateServerCount(this.servers[2].Count);
			return;
		}
		if (strHostname.Contains("oxide", true))
		{
			return;
		}
		if (strHostname.Contains("rust++", true))
		{
			return;
		}
		this.servers[1].Add(server);
		this.categoryButtons[1].UpdateServerCount(this.servers[1].Count);
	}

	// Token: 0x06002939 RID: 10553 RVA: 0x00097538 File Offset: 0x00095738
	private int GetMaxServers()
	{
		int num = (int)this.serverContainer.Height;
		return num / 34;
	}

	// Token: 0x0600293A RID: 10554 RVA: 0x00097558 File Offset: 0x00095758
	public void UpdateServerList()
	{
		this.needsServerListUpdate = false;
		int num = this.GetMaxServers();
		num = Math.Min(this.servers[this.serverType].Count, num);
		int num2 = this.pageNumber * num;
		if (this.servers[this.serverType].Count == 0)
		{
			return;
		}
		if (num2 < 0)
		{
			return;
		}
		if (num2 > this.servers[this.serverType].Count)
		{
			return;
		}
		int iPages = (int)Mathf.Ceil((float)this.servers[this.serverType].Count / (float)num);
		if (this.serverType == 4)
		{
			this.servers[this.serverType].Sort((global::ServerBrowser.Server x, global::ServerBrowser.Server y) => (x.lastplayed == y.lastplayed) ? string.Compare(x.name, y.name) : y.lastplayed.CompareTo(x.lastplayed));
		}
		else
		{
			if (this.orderType == 0)
			{
				this.servers[this.serverType].Sort((global::ServerBrowser.Server x, global::ServerBrowser.Server y) => (x.fave == y.fave) ? string.Compare(x.name, y.name) : y.fave.CompareTo(x.fave));
			}
			if (this.orderType == 1)
			{
				this.servers[this.serverType].Sort((global::ServerBrowser.Server x, global::ServerBrowser.Server y) => (x.fave == y.fave) ? ((x.currentplayers == y.currentplayers) ? string.Compare(x.name, y.name) : y.currentplayers.CompareTo(x.currentplayers)) : y.fave.CompareTo(x.fave));
			}
			if (this.orderType == 2)
			{
				this.servers[this.serverType].Sort((global::ServerBrowser.Server x, global::ServerBrowser.Server y) => (x.fave == y.fave) ? ((x.ping == y.ping) ? string.Compare(x.name, y.name) : x.ping.CompareTo(y.ping)) : y.fave.CompareTo(x.fave));
			}
		}
		if (num2 + num > this.servers[this.serverType].Count)
		{
			num = this.servers[this.serverType].Count - num2;
		}
		List<global::ServerBrowser.Server> range = this.servers[this.serverType].GetRange(num2, num);
		this.pagination.Setup(iPages, this.pageNumber);
		string text = string.Empty;
		foreach (global::ServerBrowser.Server server in range)
		{
			text += server.address;
		}
		if (text == this.currentServerChecksum)
		{
			return;
		}
		this.ClearServers();
		Vector3 position;
		position..ctor(0f, 0f, 0f);
		this.currentServerChecksum = text;
		bool flag = false;
		foreach (global::ServerBrowser.Server server2 in range)
		{
			global::ServerBrowser.Server server3 = server2;
			if (flag && !server2.fave)
			{
				position.y -= 2f;
			}
			flag = server2.fave;
			GameObject gameObject = this.NewServerItem();
			gameObject.GetComponent<global::ServerItem>().Init(ref server3);
			global::dfControl component = gameObject.GetComponent<global::dfControl>();
			component.Width = this.serverContainer.Width;
			component.Position = position;
			component.Show();
			position.y -= 34f;
		}
		this.serverContainer.Invalidate();
	}

	// Token: 0x0600293B RID: 10555 RVA: 0x000978B0 File Offset: 0x00095AB0
	private IEnumerator ServerListUpdater()
	{
		for (;;)
		{
			if (this.needsServerListUpdate)
			{
				this.UpdateServerList();
			}
			yield return new WaitForSeconds(0.2f);
		}
		yield break;
	}

	// Token: 0x0600293C RID: 10556 RVA: 0x000978CC File Offset: 0x00095ACC
	private void RefreshFinished()
	{
		this.refreshButton.IsEnabled = true;
		this.refreshButton.Opacity = 1f;
	}

	// Token: 0x0600293D RID: 10557 RVA: 0x000978EC File Offset: 0x00095AEC
	private GameObject NewServerItem()
	{
		if (this.pooledServerItems.Count > 0)
		{
			return this.pooledServerItems.Dequeue();
		}
		GameObject gameObject = (GameObject)Object.Instantiate(this.serverItem);
		global::dfControl component = gameObject.GetComponent<global::dfControl>();
		this.serverContainer.AddControl(component);
		return gameObject;
	}

	// Token: 0x0600293E RID: 10558 RVA: 0x0009793C File Offset: 0x00095B3C
	public void ChangeOrder(int iType)
	{
		this.orderType = iType;
		this.UpdateServerList();
	}

	// Token: 0x0600293F RID: 10559 RVA: 0x0009794C File Offset: 0x00095B4C
	public void OrderByName()
	{
		this.ChangeOrder(0);
	}

	// Token: 0x06002940 RID: 10560 RVA: 0x00097958 File Offset: 0x00095B58
	public void OrderByPlayers()
	{
		this.ChangeOrder(1);
	}

	// Token: 0x06002941 RID: 10561 RVA: 0x00097964 File Offset: 0x00095B64
	public void OrderByPing()
	{
		this.ChangeOrder(2);
	}

	// Token: 0x06002942 RID: 10562
	[DllImport("librust")]
	public static extern IntPtr SteamServers_Fetch(int serverVersion, global::ServerBrowser.funcServerAdd fnc, global::ServerBrowser.funcServerFinish fnsh);

	// Token: 0x06002943 RID: 10563
	[DllImport("librust")]
	public static extern void SteamServers_Destroy(IntPtr ptr);

	// Token: 0x040013BE RID: 5054
	public const int ServerItemHeight = 34;

	// Token: 0x040013BF RID: 5055
	public const int SERVERTYPE_OFFICIAL = 0;

	// Token: 0x040013C0 RID: 5056
	public const int SERVERTYPE_COMMUNITY = 1;

	// Token: 0x040013C1 RID: 5057
	public const int SERVERTYPE_MODDED = 2;

	// Token: 0x040013C2 RID: 5058
	public const int SERVERTYPE_WHITELIST = 3;

	// Token: 0x040013C3 RID: 5059
	public const int SERVERTYPE_HISTORY = 4;

	// Token: 0x040013C4 RID: 5060
	public const int SERVERTYPE_FRIENDS = 5;

	// Token: 0x040013C5 RID: 5061
	public GameObject serverItem;

	// Token: 0x040013C6 RID: 5062
	public global::ServerCategory[] categoryButtons;

	// Token: 0x040013C7 RID: 5063
	public global::dfPanel serverContainer;

	// Token: 0x040013C8 RID: 5064
	public global::Pagination pagination;

	// Token: 0x040013C9 RID: 5065
	public global::dfControl refreshButton;

	// Token: 0x040013CA RID: 5066
	public global::dfRichTextLabel detailsLabel;

	// Token: 0x040013CB RID: 5067
	public string currentServerChecksum;

	// Token: 0x040013CC RID: 5068
	[NonSerialized]
	public List<global::ServerBrowser.Server>[] servers = new List<global::ServerBrowser.Server>[6];

	// Token: 0x040013CD RID: 5069
	[NonSerialized]
	public Queue<GameObject> pooledServerItems = new Queue<GameObject>();

	// Token: 0x040013CE RID: 5070
	[NonSerialized]
	public int serverType;

	// Token: 0x040013CF RID: 5071
	private global::ServerBrowser.funcServerAdd AddServerCallback;

	// Token: 0x040013D0 RID: 5072
	private GCHandle AddServerGC;

	// Token: 0x040013D1 RID: 5073
	private global::ServerBrowser.funcServerFinish FinServerCallback;

	// Token: 0x040013D2 RID: 5074
	private GCHandle RefreshFinishedGC;

	// Token: 0x040013D3 RID: 5075
	private IntPtr serverRefresh;

	// Token: 0x040013D4 RID: 5076
	private bool firstOpened;

	// Token: 0x040013D5 RID: 5077
	private bool needsServerListUpdate;

	// Token: 0x040013D6 RID: 5078
	private int playerCount;

	// Token: 0x040013D7 RID: 5079
	private int serverCount;

	// Token: 0x040013D8 RID: 5080
	private int slotCount;

	// Token: 0x040013D9 RID: 5081
	private int orderType = 2;

	// Token: 0x040013DA RID: 5082
	private int pageNumber;

	// Token: 0x020004B7 RID: 1207
	public class Server
	{
		// Token: 0x040013DF RID: 5087
		public bool passworded;

		// Token: 0x040013E0 RID: 5088
		public string name;

		// Token: 0x040013E1 RID: 5089
		public string address;

		// Token: 0x040013E2 RID: 5090
		public int maxplayers;

		// Token: 0x040013E3 RID: 5091
		public int currentplayers;

		// Token: 0x040013E4 RID: 5092
		public int ping;

		// Token: 0x040013E5 RID: 5093
		public uint lastplayed;

		// Token: 0x040013E6 RID: 5094
		public int port;

		// Token: 0x040013E7 RID: 5095
		public int queryport;

		// Token: 0x040013E8 RID: 5096
		public bool fave;
	}

	// Token: 0x020004B8 RID: 1208
	// (Invoke) Token: 0x0600294A RID: 10570
	public delegate void funcServerAdd(int iMaxPlayers, int iCurrentPlayers, int iPing, uint iLastPlayed, [MarshalAs(UnmanagedType.LPStr)] [In] string strHostname, [MarshalAs(UnmanagedType.LPStr)] [In] string strAddress, int iPort, int iQueryPort, [MarshalAs(UnmanagedType.LPStr)] [In] string tags, bool bPassworded, int iType);

	// Token: 0x020004B9 RID: 1209
	// (Invoke) Token: 0x0600294E RID: 10574
	public delegate void funcServerFinish();
}
