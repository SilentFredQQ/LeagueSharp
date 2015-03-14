using LeagueSharp;
using LeagueSharp.Common;

namespace SilentKassadin
{
    internal class ManaManager
    {
        private readonly Obj_AI_Hero _player;
        private Menu _menu;

        public ManaManager()
        {
            _player = ObjectManager.Player;
        }

        /// <summary>
        ///     Adds the options to the main menu
        /// </summary>
        /// <param name="attachMenu"></param>
        public void AddToMenu(ref Menu attachMenu)
        {
            _menu = attachMenu;
            CreateMenu();

            //Game.PrintChat(string.Format("{0} loaded by {1}", "Mana Manager", "iJabba"));
        }

        /// <summary>
        ///     Actually creates the menu
        /// </summary>
        public void CreateMenu()
        {
            var manaMenu = new Menu("Mana Manager", "manaManager");
            {
                manaMenu.AddItem(new MenuItem("minHarassMana", "Min Mana for harass").SetValue(new Slider(40)));
                manaMenu.AddItem(new MenuItem("minLaneclearMana", "Min Mana for wave & jungleclear").SetValue(new Slider(40)));
            }

            _menu.AddSubMenu(manaMenu);
        }

        public bool CanUseSpell(Spell spell)
        {
            return _player.Mana >= _player.Spellbook.GetSpell(spell.Slot).ManaCost;
        }

        public bool CanDoCombo()
        {
            return _player.Mana >=
                   _player.Spellbook.GetSpell(SpellSlot.Q).ManaCost + _player.Spellbook.GetSpell(SpellSlot.W).ManaCost +
                   _player.Spellbook.GetSpell(SpellSlot.E).ManaCost + _player.Spellbook.GetSpell(SpellSlot.R).ManaCost;
        }

        public bool CanHarass()
        {
            return !(_player.ManaPercentage() <= _menu.Item("minHarassMana").GetValue<Slider>().Value);
        }

        public bool CanLaneclear()
        {
            return !(_player.ManaPercentage() <= _menu.Item("minLaneclearMana").GetValue<Slider>().Value);
        }
    }
}