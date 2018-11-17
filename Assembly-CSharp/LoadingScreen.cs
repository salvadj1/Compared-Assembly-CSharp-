using System;
using Facepunch.Progress;
using UnityEngine;

// Token: 0x020004AD RID: 1197
public class LoadingScreen : MonoBehaviour
{
	// Token: 0x060028F3 RID: 10483 RVA: 0x00095F58 File Offset: 0x00094158
	public static void Update(string strText)
	{
		global::LoadingScreen.Operations.Clean();
		Debug.Log("LoadingScreen: " + strText);
		global::LoadingScreen.labelString = strText;
		if (global::LoadingScreen.singleton)
		{
			global::LoadingScreen.singleton.infoText.Text = strText;
		}
	}

	// Token: 0x060028F4 RID: 10484 RVA: 0x00095FA4 File Offset: 0x000941A4
	public static void Show()
	{
		global::LoadingScreen.showing = true;
		if (global::LoadingScreen.singleton)
		{
			global::LoadingScreen.singleton.GetComponent<global::dfPanel>().Show();
		}
	}

	// Token: 0x060028F5 RID: 10485 RVA: 0x00095FD8 File Offset: 0x000941D8
	public static void Hide()
	{
		global::LoadingScreen.showing = false;
		if (global::LoadingScreen.singleton)
		{
			global::LoadingScreen.singleton.GetComponent<global::dfPanel>().Hide();
		}
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x0009600C File Offset: 0x0009420C
	public void LateUpdate()
	{
		float fillAmount;
		if (global::LoadingScreen.Operations.Update(out fillAmount))
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

	// Token: 0x060028F7 RID: 10487 RVA: 0x00096074 File Offset: 0x00094274
	private void Awake()
	{
		global::LoadingScreen.singleton = this;
		if (global::LoadingScreen.showing)
		{
			global::LoadingScreen.Show();
		}
		else
		{
			global::LoadingScreen.Hide();
		}
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x00096098 File Offset: 0x00094298
	private void Start()
	{
		if (!string.IsNullOrEmpty(global::LoadingScreen.labelString) && this.infoText)
		{
			this.infoText.Text = global::LoadingScreen.labelString;
		}
	}

	// Token: 0x040013A9 RID: 5033
	public global::dfRichTextLabel infoText;

	// Token: 0x040013AA RID: 5034
	public global::dfPanel progressBar;

	// Token: 0x040013AB RID: 5035
	public global::dfSprite progressIndicator;

	// Token: 0x040013AC RID: 5036
	public static readonly Facepunch.Progress.ProgressBar Operations = new Facepunch.Progress.ProgressBar();

	// Token: 0x040013AD RID: 5037
	private static string labelString;

	// Token: 0x040013AE RID: 5038
	private static bool showing;

	// Token: 0x040013AF RID: 5039
	private static global::LoadingScreen singleton;
}
