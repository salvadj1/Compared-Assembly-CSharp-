using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class RPOSWindowScrollable : RPOSWindow
{
	// Token: 0x06002750 RID: 10064 RVA: 0x000992A4 File Offset: 0x000974A4
	protected override void OnWindowShow()
	{
		base.OnWindowShow();
		if (this.autoResetScrolling)
		{
			this.ResetScrolling();
		}
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x000992C0 File Offset: 0x000974C0
	protected void ResetScrolling()
	{
		this.ResetScrolling(false);
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x000992CC File Offset: 0x000974CC
	protected virtual void ResetScrolling(bool retainCurrentValue)
	{
		UIScrollBar uiscrollBar = null;
		UIScrollBar uiscrollBar2 = null;
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
			uiscrollBar = ((!this.vertical || this.horizontal) ? null : base.GetComponentInChildren<UIScrollBar>());
			uiscrollBar2 = ((!this.horizontal || this.vertical) ? null : base.GetComponentInChildren<UIScrollBar>());
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

	// Token: 0x06002753 RID: 10067 RVA: 0x00099410 File Offset: 0x00097610
	protected void NextFrameRecalculateBounds()
	{
		this.cancelCalculationNextFrame = false;
		if (!this.queuedCalculationNextFrame)
		{
			base.StartCoroutine(this.Routine_NextFrameRecalculateBounds());
		}
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x00099434 File Offset: 0x00097634
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

	// Token: 0x04001355 RID: 4949
	public UIDraggablePanel myDraggablePanel;

	// Token: 0x04001356 RID: 4950
	public bool horizontal;

	// Token: 0x04001357 RID: 4951
	public bool vertical = true;

	// Token: 0x04001358 RID: 4952
	protected bool autoResetScrolling = true;

	// Token: 0x04001359 RID: 4953
	private bool didManualStart;

	// Token: 0x0400135A RID: 4954
	private bool queuedCalculationNextFrame;

	// Token: 0x0400135B RID: 4955
	private bool cancelCalculationNextFrame;

	// Token: 0x0400135C RID: 4956
	protected Vector2 initialScrollValue;
}
