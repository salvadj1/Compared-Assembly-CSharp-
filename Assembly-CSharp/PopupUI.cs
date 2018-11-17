using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004A4 RID: 1188
public class PopupUI : MonoBehaviour
{
	// Token: 0x060028C1 RID: 10433 RVA: 0x00094E68 File Offset: 0x00093068
	private void Start()
	{
		global::PopupUI.singleton = this;
		this.panelLocal = base.GetComponent<global::dfPanel>();
	}

	// Token: 0x060028C2 RID: 10434 RVA: 0x00094E7C File Offset: 0x0009307C
	public IEnumerator DoTests()
	{
		this.CreateNotice(10f, "", "You've woken up from 24 days of unconsciousness.");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "ONE");
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "You TWO.");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "TGHREEEE wank.");
		yield return new WaitForSeconds(1f);
		this.CreateInventory("10 x Wood");
		this.CreateNotice(3f, "", "FOUR wank.");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "FIVE wank.");
		yield return new WaitForSeconds(1f);
		this.CreateInventory("1 x Rock");
		yield return new WaitForSeconds(0.2f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(1.2f);
		this.CreateInventory("7 x Rock");
		yield return new WaitForSeconds(1.2f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(1.3f);
		this.CreateInventory("1 x Rock");
		yield return new WaitForSeconds(0.1f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(0.7f);
		this.CreateInventory("7 x Rock");
		yield return new WaitForSeconds(0.3f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(2.4f);
		this.CreateInventory("1 x Rock");
		yield return new WaitForSeconds(0.5f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(0.3f);
		this.CreateInventory("7 x Rock");
		yield return new WaitForSeconds(0.3f);
		this.CreateNotice(3f, "", "Big sweaty testicles");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "Dry testicles");
		yield break;
	}

	// Token: 0x060028C3 RID: 10435 RVA: 0x00094E98 File Offset: 0x00093098
	public void CreateNotice(float fSeconds, string strIcon, string strText)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(this.prefabNotice);
		this.panelLocal.AddControl(gameObject.GetComponent<global::dfPanel>());
		global::PopupNotice component = gameObject.GetComponent<global::PopupNotice>();
		component.Setup(fSeconds, strIcon, strText);
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x00094ED8 File Offset: 0x000930D8
	public void CreateInventory(string strText)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(this.prefabInventory);
		this.panelLocal.AddControl(gameObject.GetComponent<global::dfPanel>());
		global::PopupInventory component = gameObject.GetComponent<global::PopupInventory>();
		component.Setup(1.5f, strText);
	}

	// Token: 0x04001390 RID: 5008
	public static global::PopupUI singleton;

	// Token: 0x04001391 RID: 5009
	public Object prefabNotice;

	// Token: 0x04001392 RID: 5010
	public Object prefabInventory;

	// Token: 0x04001393 RID: 5011
	protected global::dfPanel panelLocal;
}
