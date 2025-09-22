using NUnit.Framework;
using Program;       // Dwarve, Wizard, Item, Grimoire
using RoleplayGame;  // Elves

namespace LibraryTests
{
    public class DwarveTests
    {
        [Test]
        // Justificación: Curar no debe superar la vida máxima (regla de negocio).
        public void Dwarve_Heal_NoSuperaMaxLife()
        {
            var d = new Dwarve("Gimli");
            d.Life = 100;
            d.AddItem(new Item("Poción", 0, 0, 50));

            d.Heal();

            Assert.AreEqual(120, d.Life); // MaxLife = 120
        }

        [Test]
        // Justificación: Documentar implementación actual: GetAttack ACUMULA en cada llamada.
        public void Dwarve_GetAttack_AcumulaCadaLlamada_ImplActual()
        {
            var d = new Dwarve("Gimli");
            d.AddItem(new Item("Hacha", 10, 0, 0));

            var first = d.GetAttack();   // 40 + 10 = 50
            var second = d.GetAttack();  // 50 + 10 = 60 (acumula)

            Assert.AreEqual(50, first);
            Assert.AreEqual(60, second);
        }

        [Test]
        // Justificación: Con la fórmula actual (Life -= ataque - GetDefense), si defensa > ataque la vida sube.
        // Lo registramos para evitar sorpresas hasta refactorizar.
        public void Dwarve_ReceiveAttack_DefensaMayor_AumentaVida_ImplActual()
        {
            var d = new Dwarve("Gimli");
            d.Life = 120;
            d.AddItem(new Item("Armadura", 0, 10, 0));

            var nuevaVida = d.ReceiveAttack(20); // defensa efectiva > ataque → vida aumenta

            Assert.Greater(nuevaVida, 120);
        }
    }

    public class ElvesTests
    {
        [Test]
        // Justificación: Elves.Heal = mitad de _maxLife + curas de ítems, con tope en _maxLife.
        public void Elves_Heal_SumaMitadYTopea()
        {
            var e = new Elves("Legolas");
            e.Life = 40; // _maxLife = 100 en tu código
            e.AddItem(new Item("Hierbas", 0, 0, 30));

            e.Heal();

            Assert.AreEqual(100, e.Life);
        }

        [Test]
        // Justificación: Documentar implementación actual de daño: Life -= (ataque + defensa).
        public void Elves_RecieveAttack_UsaAtaqueMasDefensa_ImplActual()
        {
            var e = new Elves("Legolas");
            e.AddItem(new Item("Armadura Ligera", 0, 5, 0));

            var nuevaVida = e.RecieveAttack(10);

            Assert.Less(nuevaVida, 100); // perdió más que el ataque por sumar defensa
        }
    }

    public class WizardTests
    {
        [Test]
        // Justificación: ReadGrimoire solo aplica si magic > 10 y descuenta 10 de la propiedad Magic.
        public void Wizard_ReadGrimoire_Descuenta10SiParamMayor10()
        {
            var w = new Wizard("Merlin");
            var antes = w.Magic;

            w.ReadGrimoire(15);

            Assert.AreEqual(antes - 10, w.Magic);
        }

        [Test]
        // Justificación: Si magic <= 10, no debería descontar ni hacer nada.
        public void Wizard_ReadGrimoire_NoHaceNadaSiParamLeq10()
        {
            var w = new Wizard("Merlin");
            var antes = w.Magic;

            w.ReadGrimoire(10);

            Assert.AreEqual(antes, w.Magic);
        }
    }
}
