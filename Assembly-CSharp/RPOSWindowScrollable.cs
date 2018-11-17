using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004D7 RID: 1239
public class RPOSWindowScrollable : global::RPOSWindow
{
	// Token: 0x06002ADA RID: 10970 RVA: 0x0009F168 File Offset: 0x0009D368
	protected override void OnWindowShow()
	{
		base.OnWindowShow();
		if (this.autoResetScrolling)
		{
			this.ResetScrolling();
		}
	}

	// Token: 0x06002ADB RID: 10971 RVA: 0x0009F184 File Offset: 0x0009D384
	protected void ResetScrolling()
	{
		this.ResetScrolling(false);
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x0009F190 File Offset: 0x0009D390
	protected virtual void ResetScrolling(bool retainCurrentValue)
	{
		global::UIScrollBar uiscrollBar = null;
		global::UIScrollBar uiscrollBar2 = null;
		if (this.myDraggablePanel)
		{
			if (!retainCurrentValue)
			{
				uiscrollBar = ((!this.vertical) ? null : this.myDraggablePanel.verticalScrollBar);
				uiscrollBar2 = ((!this.horizontal) ? null : this.myDraggablePanel.horizontalScrollBar);
			}
			if (!this.didManualStart)
			{
				this.myDraggablePanel.ManualStart();
				this.didManualStart = true;
			}
			this.myDraggablePanel.calculateBoundsEveryChange = false;
			this.NextFrameRecalculateBounds();
		}
		else if (!retainCurrentValue)
		{
			uiscrollBar = ((!this.vertical || this.horizontal) ? null : base.GetComponentInChildren<global::UIScrollBar>());
			uiscrollBar2 = ((!this.horizontal || this.vertical) ? null : base.GetComponentInChildren<global::UIScrollBar>());
		}
		if (!retainCurrentValue)
		{
			if (this.vertical && uiscrollBar)
			{
				uiscrollBar.scrollValue = this.initialScrollValue.y;
				uiscrollBar.ForceUpdate();
			}
			if (this.horizontal && uiscrollBar2)
			{
				uiscrollBar2.scrollValue = this.initialScrollValue.x;
				uiscrollBar2.ForceUpdate();
			}
		}
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x0009F2D4 File Offset: 0x0009D4D4
	protected void NextFrameRecalculateBounds()
	{
		this.cancelCalculationNextFrame = false;
		if (!this.queuedCalculationNextFrame)
		{
			base.StartCoroutine(this.Routine_NextFrameRecalculateBounds());
		}
	}

	// Token: 0x06002ADE RID: 10974 RVA: 0x0009F2F8 File Offset: 0x0009D4F8
	private IEnumerator Routine_NextFrameRecalculateBounds()
	{
		yield return null;
		this.queuedCalculationNextFrame = false;
		if (!this.cancelCalculationNextFrame && this.myDraggablePanel)
		{
			this.myDraggablePanel.CalculateBoundsIfNeeded();
		}
		yield break;
	}

	// Token: 0x040014D5 RID: 5333
	public global::UIDraggablePanel myDraggablePanel;

	// Token: 0x040014D6 RID: 5334
	public bool horizontal;

	// Token: 0x040014D7 RID: 5335
	public bool vertical = true;

	// Token: 0x040014D8 RID: 5336
	protected bool autoResetScrolling = true;

	// Token: 0x040014D9 RID: 5337
	private bool didManualStart;

	// Token: 0x040014DA RID: 5338
	private bool queuedCalculationNextFrame;

	// Token: 0x040014DB RID: 5339
	private bool cancelCalculationNextFrame;

	// Token: 0x040014DC RID: 5340
	protected Vector2 initialScrollValue;
}
