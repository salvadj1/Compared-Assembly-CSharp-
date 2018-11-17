using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200040F RID: 1039
public class RPOSBumper : MonoBehaviour
{
	// Token: 0x06002673 RID: 9843 RVA: 0x0009569C File Offset: 0x0009389C
	private void Clear()
	{
		if (this.instances != null)
		{
			foreach (RPOSBumper.Instance instance in this.instances)
			{
				if (instance.window)
				{
					instance.window.RemoveBumper(instance);
				}
			}
			this.instances.Clear();
		}
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x00095730 File Offset: 0x00093930
	private void OnDestroy()
	{
		this.Clear();
	}

	// Token: 0x06002675 RID: 9845 RVA: 0x00095738 File Offset: 0x00093938
	public void Populate()
	{
		this.Clear();
		List<RPOSWindow> list = new List<RPOSWindow>(RPOS.GetBumperWindowList());
		int num = list.Count;
		for (int i = 0; i < num; i++)
		{
			if (list[i] && !string.IsNullOrEmpty(list[i].title))
			{
				list[i].EnsureAwake<RPOSWindow>();
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
			this.instances = new HashSet<RPOSBumper.Instance>();
		}
		foreach (RPOSWindow rposwindow in list)
		{
			RPOSBumper.Instance instance = new RPOSBumper.Instance();
			instance.window = rposwindow;
			Vector3 localScale = this.buttonPrefab.gameObject.transform.localScale;
			GameObject gameObject = NGUITools.AddChild(base.gameObject, this.buttonPrefab.gameObject);
			instance.label = gameObject.gameObject.GetComponentInChildren<UILabel>();
			instance.label.name = rposwindow.title + "BumperButton";
			Vector3 localPosition = gameObject.transform.localPosition;
			localPosition.x = num4 + (num2 + num3) * (float)num5;
			gameObject.transform.localPosition = localPosition;
			gameObject.transform.localScale = localScale;
			instance.button = gameObject.GetComponentInChildren<UIButton>();
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

	// Token: 0x040012C1 RID: 4801
	public UISlicedSprite background;

	// Token: 0x040012C2 RID: 4802
	public UIButton buttonPrefab;

	// Token: 0x040012C3 RID: 4803
	private HashSet<RPOSBumper.Instance> instances;

	// Token: 0x02000410 RID: 1040
	public class Instance
	{
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000959C0 File Offset: 0x00093BC0
		public UIEventListener listener
		{
			get
			{
				if (!this.onceGetListener)
				{
					this.onceGetListener = true;
					if (this.button)
					{
						this._listener = UIEventListener.Get(this.button.gameObject);
					}
				}
				return this._listener;
			}
		}

		// Token: 0x040012C4 RID: 4804
		public RPOSBumper bumper;

		// Token: 0x040012C5 RID: 4805
		public UIButton button;

		// Token: 0x040012C6 RID: 4806
		public UILabel label;

		// Token: 0x040012C7 RID: 4807
		public RPOSWindow window;

		// Token: 0x040012C8 RID: 4808
		private UIEventListener _listener;

		// Token: 0x040012C9 RID: 4809
		private bool onceGetListener;
	}
}
