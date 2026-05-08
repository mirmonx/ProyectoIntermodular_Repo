using UnityEngine;

public class UIEditarPerfil : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelNombreUsuario;
    public GameObject panelNombreMascota;

    public GameObject panelFotoPerfil;
    public GameObject panelFotoMascota;

    public void CerrarTodosLosPaneles()
    {
        panelNombreUsuario.SetActive(false);
        panelNombreMascota.SetActive(false);
        panelFotoPerfil.SetActive(false);
        panelFotoMascota.SetActive(false);
    }

    // =========================
    // TOGGLES
    // =========================

    public void ToggleNombreUsuario()
    {
        panelNombreUsuario.SetActive(
            !panelNombreUsuario.activeSelf
        );
    }

    public void ToggleNombreMascota()
    {
        panelNombreMascota.SetActive(
            !panelNombreMascota.activeSelf
        );
    }

    public void ToggleFotoPerfil()
    {
        panelFotoPerfil.SetActive(
            !panelFotoPerfil.activeSelf
        );
    }

    public void ToggleFotoMascota()
    {
        panelFotoMascota.SetActive(
            !panelFotoMascota.activeSelf
        );
    }
}