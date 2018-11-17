using System;
using Facepunch.Progress;
using UnityEngine;

// Token: 0x020003FC RID: 1020
public class LoadingScreen : MonoBehaviour
{
	// Token: 0x0600257B RID: 9595 RVA: 0x00090120 File Offset: 0x0008E320
	public static void Update(string strText)
	{
		LoadingScreen.Operations.Clean();
		Debug.Log("LoadingScreen: " + strText);
		LoadingScreen.labelString = strText;
		if (LoadingScreen.singleton)
		{
			LoadingScreen.singleton.infoText.Text = strText;
		}
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x0009016C File Offset: 0x0008E36C
	public static void Show()
	{
		LoadingScreen.showing = true;
		if (LoadingScreen.singleton)
		{
			LoadingScreen.singleton.GetComponent<dfPanel>().Show();
		}
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x000901A0 File Offset: 0x0008E3A0
	public static void Hide()
	{
		LoadingScreen.showing = false;
		if (LoadingScreen.singleton)
		{
			LoadingScreen.singleton.GetComponent<dfPanel>().Hide();
		}
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x000901D4 File Offset: 0x0008E3D4
	public void LateUpdate()
	{
		float fillAmount;
		if (LoadingScreen.Operations.Update(out fillAmount))
		{
			if (!this.progressBar.IsVisible)
			{
				this.progressBar.Show();
			}
			this.progressIndicator.FillAmount = fillAmount;
		}
		else if (this.progressBar.IsVisible)
		{
			this.progressBar.Hide();
		}
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x0009023C File Offset: 0x0008E43C
	private void Awake()
	{
		LoadingScreen.singleton = this;
		if (LoadingScreen.showing)
		{
			LoadingScreen.Show();
		}
		else
		{
			LoadingScreen.Hide();
		}
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x00090260 File Offset: 0x0008E460
	private void Start()
	{
		if (!string.IsNullOrEmpty(LoadingScreen.labelString) && this.infoText)
		{
			this.infoText.Text = LoadingScreen.labelString;
		}
	}

	// Token: 0x0400122C RID: 4652
	public dfRichTextLabel infoText;

	// Token: 0x0400122D RID: 4653
	public dfPanel progressBar;

	// Token: 0x0400122E RID: 4654
	public dfSprite progressIndicator;

	// Token: 0x0400122F RID: 4655
	public static readonly ProgressBar Operations = new ProgressBar();

	// Token: 0x04001230 RID: 4656
	private static string labelString;

	// Token: 0x04001231 RID: 4657
	private static bool showing;

	// Token: 0x04001232 RID: 4658
	private static LoadingScreen singleton;
}
