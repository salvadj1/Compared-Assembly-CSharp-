using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003E9 RID: 1001
public class GameTooltipManager : MonoBehaviour
{
	// Token: 0x06002513 RID: 9491 RVA: 0x0008E754 File Offset: 0x0008C954
	private void Start()
	{
		GameTooltipManager.Singleton = this;
		for (int i = 0; i < 16; i++)
		{
			GameTooltipManager.TooltipContainer tooltipContainer = new GameTooltipManager.TooltipContainer();
			GameObject gameObject = (GameObject)Object.Instantiate(this.tooltipPrefab);
			gameObject.transform.parent = base.transform;
			tooltipContainer.tooltip = gameObject.GetComponent<dfControl>();
			tooltipContainer.tooltip_label = gameObject.GetComponent<dfLabel>();
			tooltipContainer.lastSeen = 0;
			this.tooltips.Add(tooltipContainer);
		}
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x0008E7D0 File Offset: 0x0008C9D0
	private void Update()
	{
		float num = (float)(Time.frameCount - 3);
		foreach (GameTooltipManager.TooltipContainer tooltipContainer in this.tooltips)
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

	// Token: 0x06002515 RID: 9493 RVA: 0x0008E86C File Offset: 0x0008CA6C
	protected GameTooltipManager.TooltipContainer GetTipContainer(GameObject obj)
	{
		int num = Time.frameCount - 3;
		foreach (GameTooltipManager.TooltipContainer tooltipContainer in this.tooltips)
		{
			if (tooltipContainer.lastSeen >= num)
			{
				if (tooltipContainer.target == obj)
				{
					return tooltipContainer;
				}
			}
		}
		foreach (GameTooltipManager.TooltipContainer tooltipContainer2 in this.tooltips)
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

	// Token: 0x06002516 RID: 9494 RVA: 0x0008E980 File Offset: 0x0008CB80
	public void UpdateTip(GameObject obj, string text, Vector3 vPosition, Color color, float alpha, float fscale)
	{
		GameTooltipManager.TooltipContainer tipContainer = this.GetTipContainer(obj);
		if (tipContainer == null)
		{
			return;
		}
		if (!tipContainer.tooltip.IsVisible)
		{
			tipContainer.tooltip.Show();
		}
		dfGUIManager manager = tipContainer.tooltip.GetManager();
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

	// Token: 0x040011FE RID: 4606
	public static GameTooltipManager Singleton;

	// Token: 0x040011FF RID: 4607
	public GameObject tooltipPrefab;

	// Token: 0x04001200 RID: 4608
	protected List<GameTooltipManager.TooltipContainer> tooltips = new List<GameTooltipManager.TooltipContainer>();

	// Token: 0x020003EA RID: 1002
	protected class TooltipContainer
	{
		// Token: 0x04001201 RID: 4609
		public GameObject target;

		// Token: 0x04001202 RID: 4610
		public dfControl tooltip;

		// Token: 0x04001203 RID: 4611
		public dfLabel tooltip_label;

		// Token: 0x04001204 RID: 4612
		public int lastSeen;
	}
}
