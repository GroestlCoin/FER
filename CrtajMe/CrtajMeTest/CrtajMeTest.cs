using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrtajMeModel;

namespace CrtajMeTest
{
    [TestClass]
    public class CrtajMeTest
    {

        [TestInitialize]
        public void InitTest()
        {
            new PrivateType(typeof(SkupRepos)).SetStaticField("_instance", null);
            new PrivateType(typeof(PrikazRepos)).SetStaticField("_instance", null);
            new PrivateType(typeof(UlazniObrazacRepos)).SetStaticField("_instance", null);
            new PrivateType(typeof(IzlazniObrazacRepos)).SetStaticField("_instance", null);
        }


        [TestMethod]
        public void UbaciSkup()
        {
            Skup noviSkup = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("novi", "TipFunkcija", 10);
            SkupRepos.Instance().AddSkup(noviSkup);
            Assert.AreEqual(noviSkup.Name, SkupRepos.Instance().GetSkupByName(noviSkup.Name).Name);
        }

        [TestMethod]
        public void IzbrisiSkup()
        {
            Skup noviSkup = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("novi", "TipFunkcija", 10);
            SkupRepos.Instance().AddSkup(noviSkup);
            Assert.AreEqual(noviSkup.Name, SkupRepos.Instance().GetSkupByName(noviSkup.Name).Name);
            SkupRepos.Instance().RemoveSkup("novi");
            Assert.AreEqual(0, SkupRepos.Instance().ListSkupoviByName().Count);
        }

        
        [TestMethod]
        public void UbaciPrikaz()
        {
            Prikaz noviPrikaz = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("test", 10, "TipFunkcija");
            Console.WriteLine(PrikazRepos.Instance().GetListaTipova()[0]);
            PrikazRepos.Instance().AddPrikaz(noviPrikaz);
            Assert.AreEqual("test", PrikazRepos.Instance().GetPrikazByName("test").Name);
            Assert.AreEqual("TipFunkcija", PrikazRepos.Instance().GetPrikazByName("test").Type);
            Assert.AreEqual(10, PrikazRepos.Instance().GetPrikazByName("test").Grade);
        }


        [TestMethod]
        public void ProvjeriDaLiSeUcitajuTipoviPrikaza()
        {

            Assert.IsTrue(PrikazRepos.Instance().GetListaTipova().Count > 0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(CrtajMeException))]
        public void UbaciPrikazUSkupRazlicitiTipovi()
        {
            Skup s = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("test", "TipFunkcija", 10);
            Prikaz p = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("prikaz", 1, "TipTocke");
            s.AddPrikaz(p);
        }


        [TestMethod]
        public void RacunajProsjekIPrihvatljivost()
        {
            Skup s = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("test", "TipFunkcija", 10);
            Prikaz p1 = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("prikaz1", 2, "TipFunkcija");
            Prikaz p2 = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("prikaz2", 4, "TipFunkcija");
            Prikaz p3 = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("prikaz3", 3, "TipFunkcija");

            s.AddPrikaz(p1);
            s.AddPrikaz(p2);
            s.AddPrikaz(p3);

            Assert.AreEqual(3, s.AverageGrade);
        }

        [TestMethod]
        public void IzbrisiPrikazIzSkupa()
        {
            Skup noviSkup = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("novi", "TipFunkcija", 10);
            Prikaz p = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("prikaz", 5, "TipFunkcija");
            noviSkup.AddPrikaz(p);
            Assert.AreEqual(1, noviSkup.GetListNameOfPrikaz().Count);
            noviSkup.DeletePrikaz(p);
            Assert.AreEqual(0, noviSkup.GetListNameOfPrikaz().Count);
        }

        
        [TestMethod]
        public void ValjaniUlazniObrazac()
        {
            System.IO.StreamWriter dat = new System.IO.StreamWriter("_testValjaniUlazni.txt");
            dat.WriteLine("InputPattern-Simple");
            dat.WriteLine("Prva linija ulaza");
            dat.Close();
            UlazniObrazac ulazni = CrtajMeModel.Factories.UlazniObrazacFactory.CreateNewUlazniObrazac("_testValjaniUlazni.txt");
            Assert.AreEqual(ulazni.generateInput(), "Prva linija ulaza\n");
            System.IO.File.Delete("_testValjaniUlazni.txt");
        }

        
        [TestMethod]
        [ExpectedException(typeof(CrtajMeException))]
        public void NevaljanUlazniObrazac()
        {
            System.IO.StreamWriter dat = new System.IO.StreamWriter("_testNevaljanUlazni.txt");
            dat.WriteLine("InputPattern-HardParsingProblem");
            dat.WriteLine("Prva linija ulaza");
            dat.Close();
            UlazniObrazac ulazni = CrtajMeModel.Factories.UlazniObrazacFactory.CreateNewUlazniObrazac("_testNevaljanUlazni.txt");
            System.IO.File.Delete("_testNevaljanUlazni.txt");
        }


        
        [TestMethod]
        public void ValjanIzlazniObrazac()
        {
            System.IO.StreamWriter dat = new System.IO.StreamWriter("_testValjaniIzlaz.txt");
            dat.WriteLine("OutputPattern-Simple");
            dat.Close();
            IzlazniObrazac izlazni = CrtajMeModel.Factories.IzlazniObrazacFactory.CreateNewIzlazniObrazac("_testValjaniIzlaz.txt");
            System.IO.File.Delete("_testValjaniIzlaz.txt");
        }
        
        [TestMethod]
        [ExpectedException(typeof(CrtajMeException))]
        public void NevaljanIzlazniObrazac()
        {
            System.IO.StreamWriter dat = new System.IO.StreamWriter("_testValjaniIzlaz.txt");
            dat.WriteLine("OutputPattern-Simple-Extend");
            dat.Close();
            IzlazniObrazac izlazni = CrtajMeModel.Factories.IzlazniObrazacFactory.CreateNewIzlazniObrazac("_testValjaniIzlaz.txt");
            System.IO.File.Delete("_testValjaniIzlaz.txt");
        }

        [TestMethod]
        public void KomunikacijaPrikaz()
        {
            Prikaz p = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("prikaz", 5, "TipTocke");
            p.SaveNumber(1);
            p.SaveNumber(2);
            p.SaveNumber(3);
            p.SaveNumber(4);
            Point t1 = p.getPoints()[0];
            Point t2 = p.getPoints()[1];
            Assert.IsTrue(t1.X==1 && t1.Y==2 && t2.X==3 && t2.Y==4);
        }

        [TestMethod]
        [ExpectedException(typeof(CrtajMeException))]
        public void TesitranjeNicega()
        {
            Tester.Config("prazno", new TipFunkcija("ime", 0), null, null);
            Tester.ResetTester();
            Tester.StartTesting();
        }
     
     }
}
