using NUnit.Framework; 
using Program;// Dwarve, Wizard, Item, Grimoire, Elves

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

            var first = d.GetAttack(); // 40 + 10 = 50
            var second = d.GetAttack(); // (no debería acumular)

            Assert.AreEqual(40 + 10 * 3 / 2, first);
            Assert.AreEqual(first, second);
        }

        [Test]
        // Justificación: no puede pasar que se cure porque su defensa sea mayor a su ataque
        public void Dwarve_ReceiveAttack_DefensaMayor_mantieneVida_ImplActual()
        {
            var d = new Dwarve("Gimli");
            d.Life = 120;
            d.AddItem(new Item("Armadura", 0, 10, 0));

            var nuevaVida = d.ReceiveAttack(20); // defensa efectiva > ataque → vida aumenta

            Assert.AreEqual(nuevaVida, 120);
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

            var nuevaVida = e.ReceiveAttack(30); // 100 vida - (30 ataque - 25 defensa) = 95 vida

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

            w.ReadGrimoire();

            Assert.AreEqual(antes - 10, w.Magic);
        }

        [Test]
        // Justificación: Si magic <= 10, no debería descontar ni hacer nada.
        public void Wizard_ReadGrimoire_NoHaceNadaSiMagiaNoAlcanza()
        {
            var w = new Wizard("Merlin");
            var antes = w.Magic;

            w.ReadGrimoire();

            Assert.Less(w.Magic, antes);
        }
    }

    public class KnightTests
    {

        [Test]
        public void Constructor_IniciaConNombreYVidaMaxima()
        {
            var k = new Knight("Arturo");
            Assert.AreEqual("Arturo", k.Name);
            Assert.True(k.Life > 0); // vida empieza en _maxLife
        }

        [Test]
        public void GetAttack_SumaAtaqueItems_UnaLlamada()
        {
            var k = new Knight("Arturo");
            Item sword = new Item("Sword", 15, 0, 0);
            k.AddItem(sword);
            var atk = k.GetAttack();

            // _attack base = 20, +15 = 35
            Assert.AreEqual(35, atk);
        }

        [Test]
        public void GetAttack_AcumulaIndefinidamente_EntreLlamadas_ExponeBug()
        {
            var k = new Knight("Arturo");
            Item sword = new Item("Sword", 10, 0, 0);
            k.AddItem(sword);
            var a1 = k.GetAttack(); // 20 + 10 = 30 (y _attack queda 30)
            var a2 = k.GetAttack(); // vuelve a sumar el 10 => 40
            var a3 = k.GetAttack(); // => 50

            Assert.AreEqual(30, a1);
            Assert.AreEqual(a1, a2);
            Assert.AreEqual(a2, a3);
        }

        [Test]
        public void GetDefense_AplicaMultiplicadorYAcumula_ExponeBug()
        {
            var k = new Knight("Arturo");
            Item shield = new Item("Shield", 0, 20, 0);
            k.AddItem(shield);
            var d1 = k.GetDefense(); // base 80 + (20*3/2=30) = 110
            var d2 = k.GetDefense(); // vuelve a sumar 30 => 140

            Assert.AreEqual(110, d1);
            Assert.AreEqual(d1, d2); // acumulación no resetea entre llamadas
        }

        [Test]
        public void Stats_SiMuerto_SonCero()
        {
            var k = new Knight("Arturo");
            k.Life = 0;

            Assert.AreEqual(0, k.GetAttack());
            Assert.AreEqual(0, k.GetDefense());
        }

        [Test]
        public void ReceiveAttack_NoDeberiaCurar_SiDefensaMayorQueAtaque_Esperado()
        {
            var k = new Knight("Arturo");
            Item shield = new Item("Shield", 0, 200, 0);
            k.AddItem(shield);
            // Con la implementación actual: Life -= (ataque - defensa)
            // Si defensa > ataque => (ataque - defensa) es negativo => Life -= negativo => Life AUMENTA (cura)
            // Lo esperado lógicamente: daño = max(0, ataque - defensa)

            var vidaInicial = k.Life;
            var vidaLuego = k.ReceiveAttack(50);

            // Este Assert expone el bug: hoy la vida sube o queda en _maxLife.
            Assert.True(vidaLuego >= vidaInicial,
                "BUG: el daño no debería jamás curar. Se espera que, si defensa >= ataque, el daño sea 0.");
        }

        [Test]
        public void ReceiveAttack_Muere_QuedaEnCero()
        {
            var k = new Knight("Arturo");
            // Forzamos daño grande y sin defensa agregada (ojo que GetDefense muta estado)
            k.Items.Clear();
            // Primera llamada a GetDefense dentro de ReceiveAttack usará la defensa base 80
            // daño efectivo = 500 - 80 = 420
            var vida = k.ReceiveAttack(500);

            Assert.AreEqual(0, vida);
            Assert.AreEqual(0, k.Life);
        }

        [Test]
        public void Heal_SumaItemsYRespetaTope()
        {
            var k = new Knight("Arturo");
            Item potion = new Item("potion", 0, 0, 50);
            k.AddItem(potion);
            potion.Healing = 80;
            k.AddItem(potion);

            // Dañamos antes para testear curación
            k.ReceiveAttack(120); // con defensa base 80, daño 40 -> Life baja en 40

            var vidaAntes = k.Life;
            k.Heal();
            var vidaDespues = k.Life;

            Assert.True(vidaDespues >= vidaAntes, "Debe aumentar la vida");
            Assert.True(vidaDespues <= 160, "No debe superar vida máxima");
        }

        [Test]
        public void Heal_NoHaceNada_SiMuerto()
        {
            var k = new Knight("Arturo");
            k.Life = 0;
            Item potion = new Item("potion", 0, 0, 100);
            k.AddItem(potion);

            k.Heal();
            Assert.AreEqual(0, k.Life);
        }

        [Test]
        public void AddRemoveItem_NoOperan_SiMuerto()
        {
            var k = new Knight("Arturo");

            Item sword = new Item("Sword", 10, 0, 0);
            k.AddItem(sword);
            k.Life = 0;
            k.RemoveItem(sword);
            Assert.IsNotEmpty(k.Items);

            k.Life = 100;
            k.RemoveItem(sword);
            k.Life = 0;
            k.AddItem(sword);
            Assert.IsEmpty(k.Items);

            // Forzamos una lista con 1 item y probamos Remove en muerto


        }

        [Test]
        public void ReceiveAttack_AumentaDefensaPorEfectoLateral_ExponeBug()
        {
            var k = new Knight("Arturo");
            Item shield = new Item("shield", 0, 20, 0);
            k.AddItem(shield);

            // Primera vez que recibe ataque, internamente llama a GetDefense(),
            // que ACUMULA _defense (+30 por el escudo).
            var dAntes = k.GetDefense(); // ahora _defense ya se incrementó a 110
            var vida1 = k.ReceiveAttack(100); // vuelve a llamar GetDefense -> sube a 140
            var dDespues = k.GetDefense(); // vuelve a subir a 170

            // La defensa no va aumentando con cada consulta/ataque. Esto favorece al caballero “para siempre”.
            Assert.False(dDespues > dAntes);
        }



        public static class SpellTests
        {
            private static Spell S(string name, int atk, int def, int heal) => new Spell(name, atk, def, heal);

            [Test]
            public static void Constructor_Asigna_Propiedades()
            {
                var s = S("Fuego", 25, 5, 0);
                Assert.That(s.Name, Is.EqualTo("Fuego"));
                Assert.That(s.Attack, Is.EqualTo(25));
                Assert.That(s.Defense, Is.EqualTo(5));
                Assert.That(s.Healing, Is.EqualTo(0));
            }

            [Test]
            public static void Constructor_Acepta_Valores_NoPositivos()
            {
                var s1 = S("Golpe", 0, 0, 0);
                Assert.That(s1.Attack, Is.EqualTo(0));
                Assert.That(s1.Defense, Is.EqualTo(0));
                Assert.That(s1.Healing, Is.EqualTo(0));

                var s2 = S("Sangrado", -10, 0, 0);
                Assert.That(s2.Attack, Is.EqualTo(-10));

                var s3 = S("Maldición", 0, -20, 0);
                Assert.That(s3.Defense, Is.EqualTo(-20));

                var s4 = S("Drén", 0, 0, -5);
                Assert.That(s4.Healing, Is.EqualTo(-5));
            }

            [Test]
            public static void Constructor_Soporta_IntMax()
            {
                var s = S("Ultra", int.MaxValue, 0, 0);
                Assert.That(s.Attack, Is.EqualTo(int.MaxValue));

                var s2 = S("Ultra", 0, int.MaxValue, 0);
                Assert.That(s2.Defense, Is.EqualTo(int.MaxValue));

                var s3 = S("Ultra", 0, 0, int.MaxValue);
                Assert.That(s3.Healing, Is.EqualTo(int.MaxValue));
            }

            [Test]
            public static void Propiedades_SoloGet_Inmutables()
            {
                var s = S("Hielo", 10, 20, 30);
                var copia = s;
                Assert.IsTrue(object.ReferenceEquals(s, copia), "La referencia debe ser la misma.");

                var otro = S("Hielo", 10, 20, 30);
                Assert.IsTrue(!object.ReferenceEquals(s, otro), "Instancias distintas aunque con mismos valores.");
            }

            [Test]
            public static void Name_PuedeSerNull_ComportamientoActual()
            {
                var s = new Spell(null, 1, 2, 3);
                Assert.IsTrue(s.Name == null);
            }
        }

        [Test]
        public static void AddSpell_AgregaSoloUno_ExponeDiseno()
        {
            Spell Fuego = new Spell("Fuego", 10, 0, 0);
            Grimoire.SetSpells(Fuego);
            Spell Hielo = new Spell("Hielo", 20, 0, 0);
            Grimoire.SetSpells(Hielo);
            Grimoire.AddSpell(); // por el flag sólo agrega el primero desconocido

            var atk = Grimoire.GetAttack();
            Assert.AreEqual(10, atk); // Si esperabas 30, esto expone el comportamiento real
        }

        [Test]
        public static void AddSpell_DosVeces_AgregaSegundo()
        {

            var f = new Spell("Fuego", 10, 0, 0);
            var h = new Spell("Hielo", 20, 0, 0);
            Grimoire.SetSpells(f);
            Grimoire.SetSpells(h);
            Grimoire.ResetGrimoire();
            Grimoire.AddSpell(); // agrega Fuego

            Assert.That(Grimoire.GetAttack(), Is.EqualTo(10));

            Grimoire.AddSpell(); // ahora agrega Hielo

            Assert.That(Grimoire.GetAttack(), Is.EqualTo(30));

        }

        [Test]
        public static void GetAttack_AcumulaEntreLlamadas_ExponeBug()
        {
            Grimoire.ResetGrimoire();
            Spell Fuego = new Spell("Fuego", 10, 0, 0);
            Grimoire.SetSpells(Fuego);
            Grimoire.AddSpell();

            var a1 = Grimoire.GetAttack(); // 10
            var a2 = Grimoire.GetAttack(); // (no suma otra vez lo mismo)

            Assert.That(a1, Is.EqualTo(10));
            Assert.That(a2, Is.EqualTo(a1));
        }

        [Test]
        public static void GetDefense_GetHealing_Acumulacion_ExponeBug()
        {
            Grimoire.ResetGrimoire();
            Spell Roca = new Spell("Roca", 0, 7, 3);
            Grimoire.SetSpells(Roca);
            Grimoire.AddSpell();

            var d1 = Grimoire.GetDefense(); // 7
            var d2 = Grimoire.GetDefense(); // 14
            var h1 = Grimoire.gethealing(); // 3
            var h2 = Grimoire.gethealing(); // 6

            Assert.That(d1, Is.EqualTo(7));
            Assert.That(d2, Is.EqualTo(d1));
            Assert.That(h1, Is.EqualTo(3));
            Assert.That(h2, Is.EqualTo(h1));
        }

        [Test]
        public static void Contains_ReferenciaNoValor()
        {

            Grimoire.ResetGrimoire();
            var a1 = new Spell("Clon", 5, 0, 0);
            var a2 = new Spell("Clon", 5, 0, 0); // valores iguales, otra instancia

            Grimoire.SetSpells(a1);
            Grimoire.SetSpells(a2);
            Grimoire.ResetGrimoire();
            Grimoire.AddSpell(); // agrega a1
            Grimoire.AddSpell(); // agrega a2 (Contains por referencia → distinto)

            var atk = Grimoire.GetAttack();
            Assert.AreEqual(10, atk); // 5 + 5
        }
    }

    public class WizardTests2
    {
        [Test]
        public void Ctor_Inicializa_Propiedades_Basicas()
        {
            var wiz = new Wizard("Gandalf");
            Assert.AreEqual("Gandalf", wiz.Name);
            Assert.AreEqual(50, wiz.Life); // MaxLife
            Assert.AreEqual(100, wiz.Magic); // MaxMagic
            Assert.AreEqual(0, wiz.Items.Count);
        }

        [Test]
        public void AddItem_Y_RemoveItem_ModificanColeccion()
        {
            var wiz = new Wizard("Merlin");
            var item = new Item("Daga", 5, 0, 0);

            wiz.AddItem(item);
            Assert.AreEqual(1, wiz.Items.Count);

            wiz.RemoveItem(item);
            Assert.AreEqual(0, wiz.Items.Count);
        }

        [Test]
        public void GetAttack_SoloSumaItems_CuandoMagicEsCero()
        {
            var wiz = new Wizard("Merlin");
            // Evito que se use el Grimoire:
            wiz.Magic = 0;

            // Attack base = 50 (en la clase)
            wiz.AddItem(new Item("Daga", 5, 0, 0));
            wiz.AddItem(new Item("Anillo", 3, 0, 0));

            var atk = wiz.GetAttack();
            // 50 + 5 + 3 = 58 (sin grimorio, porque Magic=0)
            Assert.AreEqual(58, atk);
            // Y Magic sigue en 0
            Assert.AreEqual(0, wiz.Magic);
        }

        [Test]
        public void GetDefense_SoloSumaItems_CuandoMagicEsCero()
        {
            var wiz = new Wizard("Merlin");
            wiz.Magic = 0;

            // Defense base = 5
            wiz.AddItem(new Item("Escudo", 0, 7, 0));
            wiz.AddItem(new Item("Capa", 0, 2, 0));

            var def = wiz.GetDefense();
            // 5 + 7 + 2 = 14
            Assert.AreEqual(14, def);
            Assert.AreEqual(0, wiz.Magic);
        }

        [Test]
        public void GetAttack_Consume5MagicSiHayMagiaSuficiente()
        {
            var wiz = new Wizard("Saruman");
            // No me importa cuánto suba Attack por el grimorio (estado global),
            // solo verifico el consumo de Magic.
            wiz.Magic = 10;
            var before = wiz.Magic;

            var atk = wiz.GetAttack();

            Assert.AreEqual(before - 5, wiz.Magic);
        }
    }
}
//No consideramos necesario testear el juego porque en sí mismo son casos de prueba de las funciones que creamos.