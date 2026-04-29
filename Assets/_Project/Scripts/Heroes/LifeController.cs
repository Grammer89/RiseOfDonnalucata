using UnityEngine;

public class LifeController : MonoBehaviour
{
   
    [SerializeField] private SO_Creatura _creature;
    private int _actualLife;


    public SO_Creatura Creature => _creature;
    private void Awake()
    {
        _actualLife = _creature.HpMax;
       
    }
    public void TakeDamge(int damage)
    {
        _actualLife = Mathf.Max(0, _actualLife -  damage);
    }

    public void Cure(int cure)
    {
        _actualLife = Mathf.Max(_actualLife + cure, _creature.HpMax);
    }
    
    public bool IsAlive()
    {
        return _actualLife > 0;
    }
}
