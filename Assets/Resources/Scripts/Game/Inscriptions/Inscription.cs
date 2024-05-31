using UnityEngine;

[CreateAssetMenu]
public class Inscription : ScriptableObject
{
    public Sprite inscripDeactivedImage;
    public Sprite inscripImage;
    public Sprite inscripFullActivedImage;
    public string inscripName;
    public int[] inscripNum;
    [TextArea]
    public string[] inscripInfo;
    public InscriptionSkill inscriptionSkill;
}
