using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeModel;

namespace CrtajMeControllers
{
    public class PrikazController
    {

        public void RemovePrikaz(IViewPrikaz frm, string prikazName)
        {
            try
            {
                PrikazRepos.Instance().RemovePrikaz(prikazName);
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
        }

        /// <summary>
        /// Odstranjuje se forma iz subjecta
        /// </summary>
        /// <param name="frm"></param>
        public void RemoveForm(IViewPrikaz frm)
        {
            PrikazRepos.Instance().removeObserver(frm);
        }

        /// <summary>
        /// Dohvacanje svih prikaza u sustavu u listu
        /// </summary>
        /// <returns>Lista imena prikaza</returns>
        public List<string> GetAllPrikaz()
        {
            return PrikazRepos.Instance().ListPrikazByName();
        }

        public void ShowPrikaz(IVisualView graph, string prikazName)
        {
            try
            {
               Prikaz newPrikaz = PrikazRepos.Instance().GetPrikazByName(prikazName);
               foreach (Point i in newPrikaz.getPoints())
               {
                   graph.DrawPoint(i);
               }
               foreach (Line l in newPrikaz.getLines())
               {
                   graph.DrawLine(l);
               }

               graph.ShowView(newPrikaz.Name);
            }
            catch (CrtajMeException e)
            {
                graph.ShowErrorMessage(e.getMsg());
            }
        }

    }
}
