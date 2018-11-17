using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000499 RID: 1177
public class GameTooltipManager : MonoBehaviour
{
	// Token: 0x06002885 RID: 10373 RVA: 0x00094140 File Offset: 0x00092340
	private void Start()
	{
		global::GameTooltipManager.Singleton = this;
		for (int i = 0; i < 16; i++)
		{
			global::GameTooltipManager.TooltipContainer tooltipContainer = new global::GameTooltipManager.TooltipContainer();
			GameObject gameObject = (GameObject)Object.Instantiate(this.tooltipPrefab);
			gameObject.transform.parent = base.transform;
			tooltipContainer.tooltip = gameObject.GetComponent<global::dfControl>();
			tooltipContainer.tooltip_label = gameObject.GetComponent<global::dfLabel>();
			tooltipContainer.lastSeen = 0;
			this.tooltips.Add(tooltipContainer);
		}
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x000941BC File Offset: 0x000923BC
	private void Update()
	{
		float num = (float)(Time.frameCount - 3);
		foreach (global::GameTooltipManager.TooltipContainer tooltipContainer in this.tooltips)
		{
			if ((float)tooltipContainer.lastSeen <= num)
			{
				if (tooltipContainer.tooltip.IsVisible)
				{
					tooltipContainer.tooltip.Hide();
				}
			}
		}
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x00094258 File Offset: 0x00092458
	protected global::GameTooltipManager.TooltipContainer GetTipContainer(GameObject obj)
	{
		int num = Time.frameCount - 3;
		foreach (global::GameTooltipManager.TooltipContainer tooltipContainer in this.tooltips)
		{
			if (tooltipContainer.lastSeen >= num)
			{
				if (tooltipContainer.target == obj)
				{
					return tooltipContainer;
				}
			}
		}
		foreach (global::GameTooltipManager.TooltipContainer tooltipContainer2 in this.tooltips)
		{
			if (tooltipContainer2.target == null)
			{
				return tooltipContainer2;
			}
			if (tooltipContainer2.lastSeen < num)
			{
				return tooltipContainer2;
			}
		}
		return null;
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x0009436C File Offset: 0x0009256C
	public void UpdateTip(GameObject obj, string text, Vector3 vPosition, Color color, float alpha, float fscale)
	{
		global::GameTooltipManager.TooltipContainer tipContainer = this.GetTipContainer(obj);
		if (tipContainer == null)
		{
			return;
		}
		if (!tipContainer.tooltip.IsVisible)
		{
			tipContainer.tooltip.Show();
		}
		global::dfGUIManager manager = tipContainer.tooltip.GetManager();
		Vector2 screenSize = manager.GetScreenSize();
		Camera renderCamera = manager.RenderCamera;
		Camera main = Camera.main;
		Vector3 vector = Camera.main.WorldToScreenPoint(vPosition);
		vector.x = screenSize.x * (vector.x / main.pixelWidth);
		vector.y = screenSize.y * (vector.y / main.pixelHeight);
		vector = manager.ScreenToGui(vector);
		vector.x -= tipContainer.tooltip.Width / 2f * tipContainer.tooltip.transform.localScale.x;
		vector.y -= tipContainer.tooltip.Height * tipContainer.tooltip.transform.localScale.y;
		tipContainer.tooltip.RelativePosition = vector;
		tipContainer.tooltip_label.Text = text;
		tipContainer.tooltip_label.Color = color;
		tipContainer.tooltip.Opacity = alpha;
		tipContainer.lastSeen = Time.frameCount;
		tipContainer.target = obj;
		tipContainer.tooltip.transform.localScale = new Vector3(fscale, fscale, fscale);
	}

	// Token: 0x04001378 RID: 4984
	public static global::GameTooltipManager Singleton;

	// Token: 0x04001379 RID: 4985
	public GameObject tooltipPrefab;

	// Token: 0x0400137A RID: 4986
	protected List<global::GameTooltipManager.TooltipContainer> tooltips = new List<global::GameTooltipManager.TooltipContainer>();

	// Token: 0x0200049A RID: 1178
	protected class TooltipContainer
	{
		// Token: 0x0400137B RID: 4987
		public GameObject target;

		// Token: 0x0400137C RID: 4988
		public global::dfControl tooltip;

		// Token: 0x0400137D RID: 4989
		public global::dfLabel tooltip_label;

		// Token: 0x0400137E RID: 4990
		public int lastSeen;
	}
}
