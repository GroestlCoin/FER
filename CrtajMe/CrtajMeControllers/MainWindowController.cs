using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeModel;
using System.Threading;

namespace CrtajMeControllers
{
    public class MainWindowConroller
    {
        /// <summary>
        /// Dummy objekt za generiranje random brojeva
        /// </summary>
        private Random rnd = new Random();
        /// <summary>
        /// Generiraju se random prikazi u svrhu pokaza rada
        /// </summary>
        private void GenerateRandomPrikaz()
        {
            foreach (string name in PrikazRepos.Instance().ListPrikazByName())
            {
                Prikaz p = PrikazRepos.Instance().GetPrikazByName(name);
                for (double i = 0; i < 5; i+=0.1)
                {
                    p.SaveNumber(i);
                    p.SaveNumber(2*rnd.NextDouble() * Math.Sin(i) + 2);
                }
            }
        }

        /// <summary>
        /// Generiranje primjera
        /// </summary>
        private void GenerateObjects()
        {
            /* Pocetak generiranja umjetnih objekata */

            Prikaz noviP = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("Sinus", 5, "TipTocke");
            PrikazRepos.Instance().AddPrikaz(noviP);

            noviP = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("Kosinus", 1, "TipFunkcija");
            PrikazRepos.Instance().AddPrikaz(noviP);

            noviP = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("Konvergencija", 2, "TipFunkcija");
            PrikazRepos.Instance().AddPrikaz(noviP);

            noviP = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz("PSO-dinamika", 3, "TipTocke");
            PrikazRepos.Instance().AddPrikaz(noviP);

            Skup novi;

            novi = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("Genetski algoritam", "TipTocke", 2);
            SkupRepos.Instance().AddSkup(novi);

            novi = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("SWARM", "TipTocke", 3);
            SkupRepos.Instance().AddSkup(novi);

            novi = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("Iscrtavanje funkcije", "TipFunkcija", 8);
            SkupRepos.Instance().AddSkup(novi);

            novi = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("Random", "TipTocke", 2.5);
            SkupRepos.Instance().AddSkup(novi);

            novi = CrtajMeModel.Factories.SkupFactory.CreateNewSkup("Performance", "TipTocke", 3.5);
            SkupRepos.Instance().AddSkup(novi);

            GenerateRandomPrikaz();
        }

        public MainWindowConroller(IMainForm frm)
        {

            UlazniObrazacRepos.Instance().addObserver(frm);
            IzlazniObrazacRepos.Instance().addObserver(frm);
            GenerateObjects();
       }

        /// <summary>
        /// Dohvacanje svih prikaza u sustavu u listu
        /// </summary>
        /// <returns>Lista imena prikaza</returns>
        public List<string> GetAllPrikaz()
        {
            return PrikazRepos.Instance().ListPrikazByName();
        }

        public void ShowSkupoviView(IViewSkup frm)
        {
            SkupRepos.Instance().addObserver(frm);
            frm.ShowView();
        }

        /// <summary>
        /// Dohvacaju se svi tipovi prikaza koji su trenutno u sustavu
        /// </summary>
        /// <returns></returns>
        public List<string> GetPrikazTypes()
        {
            return PrikazRepos.Instance().GetListaTipova();
        }

        public void ShowResultsView(IViewResults frm)
        {
            SkupRepos.Instance().addObserver(frm);
            PrikazRepos.Instance().addObserver(frm);
            frm.ShowView();
        }

        /// <summary>
        /// Odstranjuje se forma iz subjecta
        /// </summary>
        /// <param name="frm"></param>
        public void RemoveForm(IViewResults frm)
        {
            SkupRepos.Instance().removeObserver(frm);
        }


        public void ShowPrikazView(IViewPrikaz frm)
        {
            PrikazRepos.Instance().addObserver(frm);
            frm.ShowView();
        }

        public List<string> UlazniObrazacList()
        {
            return UlazniObrazacRepos.Instance().List();
        }

        public List<string> IzlazniObrazacList()
        {
            return IzlazniObrazacRepos.Instance().List();
        }

        public void LoadUlazniObrazac(IMainForm frm, string name)
        {
            try
            {
                UlazniObrazac obrazac = CrtajMeModel.Factories.UlazniObrazacFactory.CreateNewUlazniObrazac(name);
                UlazniObrazacRepos.Instance().Add(obrazac);
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
      }

        public void LoadIzlazniObrazac(IMainForm frm, string name)
        {
            try
            {
                IzlazniObrazac obrazac = CrtajMeModel.Factories.IzlazniObrazacFactory.CreateNewIzlazniObrazac(name);
                IzlazniObrazacRepos.Instance().Add(obrazac);
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
        }

        /// <summary>
        /// Dretva koja zapravo provodi pravo testiranje
        /// </summary>
        private void TestThread(object list_att)
        {
            List<object> att = (List<object>)list_att;

            try
            {
                IVisualView graph = (IVisualView) att[1];
                Prikaz newPrikaz = (Prikaz) att[2];

                Tester.StartTesting();
                foreach (Point i in newPrikaz.getPoints())
                {
                    graph.DrawPoint(i);
                }
                foreach (Line l in newPrikaz.getLines())
                {
                    graph.DrawLine(l);
                }

                PrikazRepos.Instance().AddPrikaz(newPrikaz);
                graph.ShowView(newPrikaz.Name);
            }
            catch (CrtajMeException e)
            {
                IMainForm frm = (IMainForm)att[0];
                frm.ShowErrorMessage(e.getMsg());
            }
        }

        /// <summary>
        /// Glavna metoda koja testira
        /// </summary>
        /// <param name="ulazniName">Ime ulaznog obrasca</param>
        /// <param name="izlazniName">Ime izlazng obrasca</param>
        /// <param name="tipName">Tip prikaza</param>
        public void RunTest(IMainForm frm, IVisualView graph, string applicationName, string ulazniName, string izlazniName, string tipName, string prikazName)
        {
            try
            {
                UlazniObrazac input = UlazniObrazacRepos.Instance().GetObrazac(ulazniName);
                IzlazniObrazac output = IzlazniObrazacRepos.Instance().GetObrazac(izlazniName);
                Prikaz newPrikaz = CrtajMeModel.Factories.PrikazFactory.CreateNewPrikaz(prikazName, 0, tipName);
                Tester.Config(applicationName, newPrikaz, input, output);
                List<object> attList = new List<object>();
                attList.Add(frm);
                attList.Add(graph);
                attList.Add(newPrikaz);
                Thread thr = new Thread(new ParameterizedThreadStart(this.TestThread));
                thr.Start(attList);
                while (!thr.IsAlive);
 
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
       }

        public void StopTest(IMainForm frm)
        {
            try
            {
                Tester.StopApplication();
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
        }
    
    }
}
