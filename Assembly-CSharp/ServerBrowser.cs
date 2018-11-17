using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using POSIX;
using Rust.Steam;
using UnityEngine;

// Token: 0x02000405 RID: 1029
public class ServerBrowser : MonoBehaviour
{
	// Token: 0x060025B7 RID: 9655 RVA: 0x00090E2C File Offset: 0x0008F02C
	private void Start()
	{
		for (int i = 0; i < this.servers.Length; i++)
		{
			this.servers[i] = new List<ServerBrowser.Server>();
		}
		this.AddServerCallback = new ServerBrowser.funcServerAdd(this.Add_Server);
		this.AddServerGC = GCHandle.Alloc(this.AddServerCallback);
		this.FinServerCallback = new ServerBrowser.funcServerFinish(this.RefreshFinished);
		this.RefreshFinishedGC = GCHandle.Alloc(this.FinServerCallback);
		base.BroadcastMessage("CategoryChanged", this.serverType);
		this.pagination.OnPageSwitch += this.OnPageSwitched;
		for (int j = 0; j < 50; j++)
		{
			this.NewServerItem();
		}
		this.ClearServers();
	}

	// Token: 0x060025B8 RID: 9656 RVA: 0x00090EF4 File Offset: 0x0008F0F4
	public void OnPageSwitched(int iNewPage)
	{
		this.pageNumber = iNewPage;
		this.UpdateServerList();
	}

	// Token: 0x060025B9 RID: 9657 RVA: 0x00090F04 File Offset: 0x0008F104
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

	// Token: 0x060025BA RID: 9658 RVA: 0x00090F5C File Offset: 0x0008F15C
	private void OnEnable()
	{
		base.StartCoroutine(this.ServerListUpdater());
	}

	// Token: 0x060025BB RID: 9659 RVA: 0x00090F6C File Offset: 0x0008F16C
	public void ClearList()
	{
		this.pageNumber = 0;
		foreach (List<ServerBrowser.Server> list in this.servers)
		{
			list.Clear();
		}
		foreach (ServerCategory serverCategory in this.categoryButtons)
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

	// Token: 0x060025BC RID: 9660 RVA: 0x0009100C File Offset: 0x0008F20C
	public void ClearServers()
	{
		ServerItem[] componentsInChildren = this.serverContainer.GetComponentsInChildren<ServerItem>();
		foreach (ServerItem serverItem in componentsInChildren)
		{
			serverItem.gameObject.GetComponent<dfControl>().Hide();
			this.pooledServerItems.Enqueue(serverItem.gameObject);
		}
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x00091060 File Offset: 0x0008F260
	public void RefreshServerList()
	{
		this.refreshButton.IsEnabled = false;
		this.refreshButton.Opacity = 0.2f;
		SteamClient.Needed();
		this.ClearList();
		this.detailsLabel.Text = "Updating..";
		this.serverRefresh = ServerBrowser.SteamServers_Fetch(1069, this.AddServerCallback, this.FinServerCallback);
		if (this.serverRefresh == IntPtr.Zero)
		{
			Debug.Log("Error! Couldn't refresh servers!!");
		}
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x000910E0 File Offset: 0x0008F2E0
	public void OnFirstOpen()
	{
		if (this.firstOpened)
		{
			return;
		}
		if (!base.GetComponent<dfPanel>().IsVisible)
		{
			return;
		}
		this.firstOpened = true;
		this.RefreshServerList();
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x00091118 File Offset: 0x0008F318
	private bool ShouldIgnoreServer(ServerBrowser.Server item)
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
		return item.currentplayers > item.maxplayers || item.currentplayers > 500 || ServerListConfig.IsBadServer(item.address);
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x00091318 File Offset: 0x0008F518
	private void Add_Server(int iMaxPlayers, int iCurrentPlayers, int iPing, uint iLastPlayed, [MarshalAs(UnmanagedType.LPStr)] [In] string strHostname, [MarshalAs(UnmanagedType.LPStr)] [In] string strAddress, int iPort, int iQueryPort, [MarshalAs(UnmanagedType.LPStr)] [In] string tags, bool bPassworded, int iType)
	{
		string strName = strAddress + ":" + iPort.ToString();
		ServerBrowser.Server server = new ServerBrowser.Server();
		server.name = strHostname;
		server.address = strAddress;
		server.maxplayers = iMaxPlayers;
		server.currentplayers = iCurrentPlayers;
		server.ping = iPing;
		server.lastplayed = iLastPlayed;
		server.port = iPort;
		server.queryport = iQueryPort;
		server.fave = FavouriteList.Contains(strName);
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
			int num2 = (int)Time.ElapsedSecondsSince((int)server.lastplayed);
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
			ServerBrowser.Server server2 = server;
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
							if (!SteamGroups.MemberOf(iGroupID))
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

	// Token: 0x060025C1 RID: 9665 RVA: 0x00091714 File Offset: 0x0008F914
	private int GetMaxServers()
	{
		int num = (int)this.serverContainer.Height;
		return num / 34;
	}

	// Token: 0x060025C2 RID: 9666 RVA: 0x00091734 File Offset: 0x0008F934
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
			this.servers[this.serverType].Sort((ServerBrowser.Server x, ServerBrowser.Server y) => (x.lastplayed == y.lastplayed) ? string.Compare(x.name, y.name) : y.lastplayed.CompareTo(x.lastplayed));
		}
		else
		{
			if (this.orderType == 0)
			{
				this.servers[this.serverType].Sort((ServerBrowser.Server x, ServerBrowser.Server y) => (x.fave == y.fave) ? string.Compare(x.name, y.name) : y.fave.CompareTo(x.fave));
			}
			if (this.orderType == 1)
			{
				this.servers[this.serverType].Sort((ServerBrowser.Server x, ServerBrowser.Server y) => (x.fave == y.fave) ? ((x.currentplayers == y.currentplayers) ? string.Compare(x.name, y.name) : y.currentplayers.CompareTo(x.currentplayers)) : y.fave.CompareTo(x.fave));
			}
			if (this.orderType == 2)
			{
				this.servers[this.serverType].Sort((ServerBrowser.Server x, ServerBrowser.Server y) => (x.fave == y.fave) ? ((x.ping == y.ping) ? string.Compare(x.name, y.name) : x.ping.CompareTo(y.ping)) : y.fave.CompareTo(x.fave));
			}
		}
		if (num2 + num > this.servers[this.serverType].Count)
		{
			num = this.servers[this.serverType].Count - num2;
		}
		List<ServerBrowser.Server> range = this.servers[this.serverType].GetRange(num2, num);
		this.pagination.Setup(iPages, this.pageNumber);
		string text = string.Empty;
		foreach (ServerBrowser.Server server in range)
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
		foreach (ServerBrowser.Server server2 in range)
		{
			ServerBrowser.Server server3 = server2;
			if (flag && !server2.fave)
			{
				position.y -= 2f;
			}
			flag = server2.fave;
			GameObject gameObject = this.NewServerItem();
			gameObject.GetComponent<ServerItem>().Init(ref server3);
			dfControl component = gameObject.GetComponent<dfControl>();
			component.Width = this.serverContainer.Width;
			component.Position = position;
			component.Show();
			position.y -= 34f;
		}
		this.serverContainer.Invalidate();
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x00091A8C File Offset: 0x0008FC8C
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

	// Token: 0x060025C4 RID: 9668 RVA: 0x00091AA8 File Offset: 0x0008FCA8
	private void RefreshFinished()
	{
		this.refreshButton.IsEnabled = true;
		this.refreshButton.Opacity = 1f;
	}

	// Token: 0x060025C5 RID: 9669 RVA: 0x00091AC8 File Offset: 0x0008FCC8
	private GameObject NewServerItem()
	{
		if (this.pooledServerItems.Count > 0)
		{
			return this.pooledServerItems.Dequeue();
		}
		GameObject gameObject = (GameObject)Object.Instantiate(this.serverItem);
		dfControl component = gameObject.GetComponent<dfControl>();
		this.serverContainer.AddControl(component);
		return gameObject;
	}

	// Token: 0x060025C6 RID: 9670 RVA: 0x00091B18 File Offset: 0x0008FD18
	public void ChangeOrder(int iType)
	{
		this.orderType = iType;
		this.UpdateServerList();
	}

	// Token: 0x060025C7 RID: 9671 RVA: 0x00091B28 File Offset: 0x0008FD28
	public void OrderByName()
	{
		this.ChangeOrder(0);
	}

	// Token: 0x060025C8 RID: 9672 RVA: 0x00091B34 File Offset: 0x0008FD34
	public void OrderByPlayers()
	{
		this.ChangeOrder(1);
	}

	// Token: 0x060025C9 RID: 9673 RVA: 0x00091B40 File Offset: 0x0008FD40
	public void OrderByPing()
	{
		this.ChangeOrder(2);
	}

	// Token: 0x060025CA RID: 9674
	[DllImport("librust")]
	public static extern IntPtr SteamServers_Fetch(int serverVersion, ServerBrowser.funcServerAdd fnc, ServerBrowser.funcServerFinish fnsh);

	// Token: 0x060025CB RID: 9675
	[DllImport("librust")]
	public static extern void SteamServers_Destroy(IntPtr ptr);

	// Token: 0x04001241 RID: 4673
	public const int ServerItemHeight = 34;

	// Token: 0x04001242 RID: 4674
	public const int SERVERTYPE_OFFICIAL = 0;

	// Token: 0x04001243 RID: 4675
	public const int SERVERTYPE_COMMUNITY = 1;

	// Token: 0x04001244 RID: 4676
	public const int SERVERTYPE_MODDED = 2;

	// Token: 0x04001245 RID: 4677
	public const int SERVERTYPE_WHITELIST = 3;

	// Token: 0x04001246 RID: 4678
	public const int SERVERTYPE_HISTORY = 4;

	// Token: 0x04001247 RID: 4679
	public const int SERVERTYPE_FRIENDS = 5;

	// Token: 0x04001248 RID: 4680
	public GameObject serverItem;

	// Token: 0x04001249 RID: 4681
	public ServerCategory[] categoryButtons;

	// Token: 0x0400124A RID: 4682
	public dfPanel serverContainer;

	// Token: 0x0400124B RID: 4683
	public Pagination pagination;

	// Token: 0x0400124C RID: 4684
	public dfControl refreshButton;

	// Token: 0x0400124D RID: 4685
	public dfRichTextLabel detailsLabel;

	// Token: 0x0400124E RID: 4686
	public string currentServerChecksum;

	// Token: 0x0400124F RID: 4687
	[NonSerialized]
	public List<ServerBrowser.Server>[] servers = new List<ServerBrowser.Server>[6];

	// Token: 0x04001250 RID: 4688
	[NonSerialized]
	public Queue<GameObject> pooledServerItems = new Queue<GameObject>();

	// Token: 0x04001251 RID: 4689
	[NonSerialized]
	public int serverType;

	// Token: 0x04001252 RID: 4690
	private ServerBrowser.funcServerAdd AddServerCallback;

	// Token: 0x04001253 RID: 4691
	private GCHandle AddServerGC;

	// Token: 0x04001254 RID: 4692
	private ServerBrowser.funcServerFinish FinServerCallback;

	// Token: 0x04001255 RID: 4693
	private GCHandle RefreshFinishedGC;

	// Token: 0x04001256 RID: 4694
	private IntPtr serverRefresh;

	// Token: 0x04001257 RID: 4695
	private bool firstOpened;

	// Token: 0x04001258 RID: 4696
	private bool needsServerListUpdate;

	// Token: 0x04001259 RID: 4697
	private int playerCount;

	// Token: 0x0400125A RID: 4698
	private int serverCount;

	// Token: 0x0400125B RID: 4699
	private int slotCount;

	// Token: 0x0400125C RID: 4700
	private int orderType = 2;

	// Token: 0x0400125D RID: 4701
	private int pageNumber;

	// Token: 0x02000406 RID: 1030
	public class Server
	{
		// Token: 0x04001262 RID: 4706
		public bool passworded;

		// Token: 0x04001263 RID: 4707
		public string name;

		// Token: 0x04001264 RID: 4708
		public string address;

		// Token: 0x04001265 RID: 4709
		public int maxplayers;

		// Token: 0x04001266 RID: 4710
		public int currentplayers;

		// Token: 0x04001267 RID: 4711
		public int ping;

		// Token: 0x04001268 RID: 4712
		public uint lastplayed;

		// Token: 0x04001269 RID: 4713
		public int port;

		// Token: 0x0400126A RID: 4714
		public int queryport;

		// Token: 0x0400126B RID: 4715
		public bool fave;
	}

	// Token: 0x020008D7 RID: 2263
	// (Invoke) Token: 0x06004D34 RID: 19764
	public delegate void funcServerAdd(int iMaxPlayers, int iCurrentPlayers, int iPing, uint iLastPlayed, [MarshalAs(UnmanagedType.LPStr)] [In] string strHostname, [MarshalAs(UnmanagedType.LPStr)] [In] string strAddress, int iPort, int iQueryPort, [MarshalAs(UnmanagedType.LPStr)] [In] string tags, bool bPassworded, int iType);

	// Token: 0x020008D8 RID: 2264
	// (Invoke) Token: 0x06004D38 RID: 19768
	public delegate void funcServerFinish();
}
