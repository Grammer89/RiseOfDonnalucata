using UnityEngine;

public class LifeController : MonoBehaviour
{
   
    private SO_Creatura _creature;
    private int _actualLife;

    private void Awake()
    {
        _actualLife = _creature.HpMax;
        _creature = GetComponent<SO_Creatura>();
    }
    public void TakeDamge(int damage)
    {
        _actualLife = Mathf.Max(0, _actualLife -  damage);
    }

    public void Cure(int cure)
    {
        _actualLife = Mathf.Max(_actualLife + cure, _creature.HpMax);
    }    
}
