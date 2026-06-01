using UnityEngine;

public class Enemy_Game : MonoBehaviour
{
    [SerializeField] public Gegner type;
    [SerializeField] public gameObject darkness;
    [SerializeField] public gameObject torches;
    [SerializeField] public Model model;

    void OnBecameVisible()
    {
        if(type = Gegner.ShadowEnemy && model.GetCurrentPlayerItems.ContainsKey(Items.Feuerzeug))
        {
            darkness.SetActive(false);
            torches.SetActive(true);
        }
    }


}
