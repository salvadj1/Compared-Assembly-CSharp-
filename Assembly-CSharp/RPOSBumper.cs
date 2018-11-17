using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004C4 RID: 1220
public class RPOSBumper : MonoBehaviour
{
	// Token: 0x060029FD RID: 10749 RVA: 0x0009B560 File Offset: 0x00099760
	private void Clear()
	{
		if (this.instances != null)
		{
			foreach (global::RPOSBumper.Instance instance in this.instances)
			{
				if (instance.window)
				{
					instance.window.RemoveBumper(instance);
				}
			}
			this.instances.Clear();
		}
	}

	// Token: 0x060029FE RID: 10750 RVA: 0x0009B5F4 File Offset: 0x000997F4
	private void OnDestroy()
	{
		this.Clear();
	}

	// Token: 0x060029FF RID: 10751 RVA: 0x0009B5FC File Offset: 0x000997FC
	public void Populate()
	{
		this.Clear();
		List<global::RPOSWindow> list = new List<global::RPOSWindow>(global::RPOS.GetBumperWindowList());
		int num = list.Count;
		for (int i = 0; i < num; i++)
		{
			if (list[i] && !string.IsNullOrEmpty(list[i].title))
			{
				list[i].EnsureAwake<global::RPOSWindow>();
			}
			else
			{
				list.RemoveAt(i--);
				num--;
			}
		}
		float num2 = 75f * this.buttonPrefab.gameObject.transform.localScale.x;
		float num3 = 5f;
		float num4 = (float)num * num2 * -0.5f;
		int num5 = 0;
		if (this.instances == null)
		{
			this.instances = new HashSet<global::RPOSBumper.Instance>();
		}
		foreach (global::RPOSWindow rposwindow in list)
		{
			global::RPOSBumper.Instance instance = new global::RPOSBumper.Instance();
			instance.window = rposwindow;
			Vector3 localScale = this.buttonPrefab.gameObject.transform.localScale;
			GameObject gameObject = global::NGUITools.AddChild(base.gameObject, this.buttonPrefab.gameObject);
			instance.label = gameObject.gameObject.GetComponentInChildren<global::UILabel>();
			instance.label.name = rposwindow.title + "BumperButton";
			Vector3 localPosition = gameObject.transform.localPosition;
			localPosition.x = num4 + (num2 + num3) * (float)num5;
			gameObject.transform.localPosition = localPosition;
			gameObject.transform.localScale = localScale;
			instance.button = gameObject.GetComponentInChildren<global::UIButton>();
			instance.bumper = this;
			rposwindow.AddBumper(instance);
			this.instances.Add(instance);
			num5++;
		}
		Vector3 localScale2 = this.background.transform.localScale;
		localScale2.x = (float)num * num2 + ((float)num - 1f * num3);
		this.background.gameObject.transform.localScale = localScale2;
		this.background.gameObject.transform.localPosition = new Vector3(localScale2.x * -0.5f, base.transform.localPosition.y, 0f);
	}

	// Token: 0x04001441 RID: 5185
	public global::UISlicedSprite background;

	// Token: 0x04001442 RID: 5186
	public global::UIButton buttonPrefab;

	// Token: 0x04001443 RID: 5187
	private HashSet<global::RPOSBumper.Instance> instances;

	// Token: 0x020004C5 RID: 1221
	public class Instance
	{
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x0009B884 File Offset: 0x00099A84
		public global::UIEventListener listener
		{
			get
			{
				if (!this.onceGetListener)
				{
					this.onceGetListener = true;
					if (this.button)
					{
						this._listener = global::UIEventListener.Get(this.button.gameObject);
					}
				}
				return this._listener;
			}
		}

		// Token: 0x04001444 RID: 5188
		public global::RPOSBumper bumper;

		// Token: 0x04001445 RID: 5189
		public global::UIButton button;

		// Token: 0x04001446 RID: 5190
		public global::UILabel label;

		// Token: 0x04001447 RID: 5191
		public global::RPOSWindow window;

		// Token: 0x04001448 RID: 5192
		private global::UIEventListener _listener;

		// Token: 0x04001449 RID: 5193
		private bool onceGetListener;
	}
}
