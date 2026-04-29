using UnityEngine;

namespace CalculateDamages
{
    public static class CalculateDamage
    {
        public static int Calculate(ActionType action, //Tipo di azione
                                    OrderBattle fromTarget, OrderBattle toTarget, //Chi fa l'azione e chi la subisce
                                    SpecialActionWithSpendMp specialActionWithMp, //Azione Speciale
                                    ItemInstance item) //Oggetto da utilizzare
        {
            int damage = 0;
            int rollAttacker = Random.Range(1, 21);
            switch (action)
            {
                case (ActionType.SimpleAttack):
                    {
                        if (rollAttacker == 1) return damage;
                        if (rollAttacker == 20) return fromTarget.Creature.Frz * 2;

                        rollAttacker += DetermineModifier(fromTarget.Creature.Frz);
                        if (rollAttacker >= (toTarget.Creature.Res + DetermineModifier(toTarget.Creature.Res)))
                        {
                            damage = fromTarget.Creature.Frz - toTarget.Creature.Res + DetermineModifier(fromTarget.Creature.Frz);
                        }

                        break;
                    }
                case (ActionType.Defense):
                    {
                        damage += 5;
                        break;
                    }
                case (ActionType.SpecialAction):
                    {

                        rollAttacker += DetermineModifier(fromTarget.Creature.Mag);
                        if (specialActionWithMp._specialActionHero == SpecialActionHero.Shield)
                        {
                            damage = 0;
                            return damage;
                        }
                        if (specialActionWithMp._specialActionHero == SpecialActionHero.Cure)
                        {
                            damage = fromTarget.Creature.Mag + DetermineModifier(fromTarget.Creature.Mag);
                            return damage;
                        }
                        if (rollAttacker >= (toTarget.Creature.Spr + DetermineModifier(toTarget.Creature.Spr)))
                        {
                            damage = fromTarget.Creature.Mag - toTarget.Creature.Spr + DetermineModifier(fromTarget.Creature.Mag);
                        }

                      

                        if (rollAttacker == 1) return 0;
                        if (rollAttacker == 20) return fromTarget.Creature.Mag * 2;
                        break;
                    }
                case (ActionType.UseObject):
                    {
                        if (item.Data.ItemType == ItemType.Potion)
                        {
                            damage += (int)item.Data.HealAmount;
                        }
                        else if (item.Data.ItemType == ItemType.Revify)
                        {
                            damage += (int)(toTarget.Creature.HpMax * 0.25f);
                        }
                        break;
                    }
            }
            return damage;
        }

        private static int DetermineModifier(int value)
        {

            if (value == 1) return -5;
            if (value == 2 || value == 3) return -4;
            if (value == 4 || value == 5) return -3;
            if (value == 6 || value == 7) return -2;
            if (value == 8 || value == 9) return -1;
            if (value == 10 || value == 11) return 0;
            if (value == 12 || value == 13) return 1;
            if (value == 14 || value == 15) return 2;
            if (value == 16 || value == 17) return 3;
            if (value == 18 || value == 19) return 4;
            if (value == 20 || value == 21) return 5;
            if (value == 22 || value == 23) return 6;
            if (value == 24 || value == 25) return 7;
            if (value == 26 || value == 27) return 8;
            if (value == 28 || value == 29) return 9;
            if (value == 30) return 10;
            return 0;

        }

    }
}
