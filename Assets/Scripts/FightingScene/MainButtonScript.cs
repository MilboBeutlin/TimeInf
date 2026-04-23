using UnityEngine;
using UnityEngine.UI;

public class MainButtonScript : MonoBehaviour
{
    private ButtonManager bM;

    void Start()
    {
        bM = GetComponentInParent<ButtonManager>();
    }

    public void Attacks()
    {
        bM.Attacks();
        Destroy(this.gameObject);
    }

    public void MainButtons()
    {
        bM.MainButtons();
        Destroy(this.gameObject);
    }

    public void Items()
    {
        bM.Items();
        Destroy(this.gameObject);
    }

}
