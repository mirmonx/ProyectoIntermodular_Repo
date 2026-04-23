using UnityEngine;

public class Navegador : MonoBehaviour
{
    public GameObject BarraNav;

    void Start()
    {
        BarraNav.SetActive(false);
    }

    public void AbrirBarraNav()
    {
        BarraNav.SetActive(true);
    }

    public void CerrarBarraNav()
    {
        BarraNav.SetActive(false);
    }

}