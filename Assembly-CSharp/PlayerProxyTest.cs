using System;
using uLink;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class PlayerProxyTest : MonoBehaviour
{
	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000428 RID: 1064 RVA: 0x00014D14 File Offset: 0x00012F14
	// (set) Token: 0x06000429 RID: 1065 RVA: 0x00014D2C File Offset: 0x00012F2C
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

	// Token: 0x0600042A RID: 1066 RVA: 0x00014E54 File Offset: 0x00013054
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

	// Token: 0x0600042B RID: 1067 RVA: 0x00014EB8 File Offset: 0x000130B8
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

	// Token: 0x0600042C RID: 1068 RVA: 0x00014F1C File Offset: 0x0001311C
	private void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		if (info.networkView.isMine)
		{
			this.isMine = true;
			this.MineInit();
		}
	}

	// Token: 0x040003A7 RID: 935
	[PrefetchChildComponent(NameMask = "Soldier")]
	public GameObject body;

	// Token: 0x040003A8 RID: 936
	[PrefetchChildComponent(NameMask = "HB Hit")]
	public GameObject proxyCollider;

	// Token: 0x040003A9 RID: 937
	[PrefetchComponent]
	public global::ArmorModelRenderer armorRenderer;

	// Token: 0x040003AA RID: 938
	public MonoBehaviour[] proxyDisableList;

	// Token: 0x040003AB RID: 939
	private bool[] initialDisableListValues;

	// Token: 0x040003AC RID: 940
	private bool isMine;

	// Token: 0x040003AD RID: 941
	private bool isFaking;

	// Token: 0x040003AE RID: 942
	private bool hasFaked;
}
