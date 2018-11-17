using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007FC RID: 2044
[AddComponentMenu("NGUI/UI/Root")]
[ExecuteInEditMode]
public class UIRoot : MonoBehaviour
{
	// Token: 0x06004966 RID: 18790 RVA: 0x0012DD18 File Offset: 0x0012BF18
	private void Awake()
	{
		UIRoot.mRoots.Add(this);
	}

	// Token: 0x06004967 RID: 18791 RVA: 0x0012DD28 File Offset: 0x0012BF28
	private void OnDestroy()
	{
		UIRoot.mRoots.Remove(this);
	}

	// Token: 0x06004968 RID: 18792 RVA: 0x0012DD38 File Offset: 0x0012BF38
	private void Start()
	{
		this.mTrans = base.transform;
		UIOrthoCamera componentInChildren = base.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
	}

	// Token: 0x06004969 RID: 18793 RVA: 0x0012DD9C File Offset: 0x0012BF9C
	private void Update()
	{
		this.manualHeight = Mathf.Max(2, (!this.automatic) ? this.manualHeight : Screen.height);
		float num = 2f / (float)this.manualHeight;
		Vector3 localScale = this.mTrans.localScale;
		if (Mathf.Abs(localScale.x - num) > 1.401298E-45f || Mathf.Abs(localScale.y - num) > 1.401298E-45f || Mathf.Abs(localScale.z - num) > 1.401298E-45f)
		{
			this.mTrans.localScale = new Vector3(num, num, num);
		}
	}

	// Token: 0x0600496A RID: 18794 RVA: 0x0012DE48 File Offset: 0x0012C048
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = UIRoot.mRoots.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.mRoots[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, 1);
			}
			i++;
		}
	}

	// Token: 0x0600496B RID: 18795 RVA: 0x0012DE94 File Offset: 0x0012C094
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			int i = 0;
			int count = UIRoot.mRoots.Count;
			while (i < count)
			{
				UIRoot uiroot = UIRoot.mRoots[i];
				if (uiroot != null)
				{
					uiroot.BroadcastMessage(funcName, param, 1);
				}
				i++;
			}
		}
	}

	// Token: 0x0400297D RID: 10621
	private static List<UIRoot> mRoots = new List<UIRoot>();

	// Token: 0x0400297E RID: 10622
	public bool automatic = true;

	// Token: 0x0400297F RID: 10623
	public int manualHeight = 800;

	// Token: 0x04002980 RID: 10624
	private Transform mTrans;
}
