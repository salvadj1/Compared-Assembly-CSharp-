using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008EE RID: 2286
[AddComponentMenu("NGUI/UI/Root")]
[ExecuteInEditMode]
public class UIRoot : MonoBehaviour
{
	// Token: 0x06004E15 RID: 19989 RVA: 0x00137C7C File Offset: 0x00135E7C
	private void Awake()
	{
		global::UIRoot.mRoots.Add(this);
	}

	// Token: 0x06004E16 RID: 19990 RVA: 0x00137C8C File Offset: 0x00135E8C
	private void OnDestroy()
	{
		global::UIRoot.mRoots.Remove(this);
	}

	// Token: 0x06004E17 RID: 19991 RVA: 0x00137C9C File Offset: 0x00135E9C
	private void Start()
	{
		this.mTrans = base.transform;
		global::UIOrthoCamera componentInChildren = base.GetComponentInChildren<global::UIOrthoCamera>();
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

	// Token: 0x06004E18 RID: 19992 RVA: 0x00137D00 File Offset: 0x00135F00
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

	// Token: 0x06004E19 RID: 19993 RVA: 0x00137DAC File Offset: 0x00135FAC
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = global::UIRoot.mRoots.Count;
		while (i < count)
		{
			global::UIRoot uiroot = global::UIRoot.mRoots[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, 1);
			}
			i++;
		}
	}

	// Token: 0x06004E1A RID: 19994 RVA: 0x00137DF8 File Offset: 0x00135FF8
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			int i = 0;
			int count = global::UIRoot.mRoots.Count;
			while (i < count)
			{
				global::UIRoot uiroot = global::UIRoot.mRoots[i];
				if (uiroot != null)
				{
					uiroot.BroadcastMessage(funcName, param, 1);
				}
				i++;
			}
		}
	}

	// Token: 0x04002BCB RID: 11211
	private static List<global::UIRoot> mRoots = new List<global::UIRoot>();

	// Token: 0x04002BCC RID: 11212
	public bool automatic = true;

	// Token: 0x04002BCD RID: 11213
	public int manualHeight = 800;

	// Token: 0x04002BCE RID: 11214
	private Transform mTrans;
}
