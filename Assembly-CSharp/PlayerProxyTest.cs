using System;
using uLink;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class PlayerProxyTest : MonoBehaviour
{
	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060003B0 RID: 944 RVA: 0x00013524 File Offset: 0x00011724
	// (set) Token: 0x060003B1 RID: 945 RVA: 0x0001353C File Offset: 0x0001173C
	public bool treatAsProxy
	{
		get
		{
			return !this.isMine || this.isFaking;
		}
		set
		{
			if (this.isMine && this.isFaking != value)
			{
				if (!this.hasFaked)
				{
					this.initialDisableListValues = new bool[this.proxyDisableList.Length];
					this.hasFaked = true;
				}
				this.isFaking = value;
				if (value)
				{
					for (int i = 0; i < this.initialDisableListValues.Length; i++)
					{
						this.initialDisableListValues[i] = (this.proxyDisableList[i] && this.proxyDisableList[i].enabled);
					}
					if (this.body)
					{
						this.body.SetActive(true);
					}
					if (this.armorRenderer)
					{
						this.armorRenderer.enabled = true;
					}
					this.ProxyInit();
				}
				else
				{
					for (int j = 0; j < this.initialDisableListValues.Length; j++)
					{
						if (this.initialDisableListValues[j] && this.proxyDisableList[j])
						{
							this.proxyDisableList[j].enabled = true;
						}
					}
					this.MineInit();
				}
			}
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00013664 File Offset: 0x00011864
	private void MineInit()
	{
		if (this.body)
		{
			this.body.SetActive(false);
		}
		if (this.proxyCollider)
		{
			this.proxyCollider.SetActive(false);
		}
		if (this.armorRenderer)
		{
			this.armorRenderer.enabled = false;
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x000136C8 File Offset: 0x000118C8
	private void ProxyInit()
	{
		for (int i = 0; i < this.proxyDisableList.Length; i++)
		{
			if (this.proxyDisableList[i])
			{
				this.proxyDisableList[i].enabled = false;
			}
		}
		if (this.proxyCollider)
		{
			this.proxyCollider.SetActive(true);
		}
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0001372C File Offset: 0x0001192C
	private void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		if (info.networkView.isMine)
		{
			this.isMine = true;
			this.MineInit();
		}
	}

	// Token: 0x0400033C RID: 828
	[PrefetchChildComponent(NameMask = "Soldier")]
	public GameObject body;

	// Token: 0x0400033D RID: 829
	[PrefetchChildComponent(NameMask = "HB Hit")]
	public GameObject proxyCollider;

	// Token: 0x0400033E RID: 830
	[PrefetchComponent]
	public ArmorModelRenderer armorRenderer;

	// Token: 0x0400033F RID: 831
	public MonoBehaviour[] proxyDisableList;

	// Token: 0x04000340 RID: 832
	private bool[] initialDisableListValues;

	// Token: 0x04000341 RID: 833
	private bool isMine;

	// Token: 0x04000342 RID: 834
	private bool isFaking;

	// Token: 0x04000343 RID: 835
	private bool hasFaked;
}
